using System.Windows.Forms;
using System.Linq;
using Microsoft.Xna.Framework;
using Mindstep.EasterEgg.Commons;
using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Mindstep.EasterEgg.Commons.SaveLoad;
using SD = System.Drawing;
using System.Text.RegularExpressions;
using System.ComponentModel;

namespace Mindstep.EasterEgg.MapEditor
{
    public partial class MainForm : Form
    {
        private static string TITLE = "Easter Egg Editor";

        public readonly ModelManager ModelManager = new ModelManager();

        public Texture2D whiteOneByOneTexture;
        public Texture2D transparentOneByOneTexture;

        public readonly ServiceContainer Services;
        public readonly ContentManager Content;
        private GraphicsDeviceService graphicsDeviceService;

        /// <summary>
        /// Gets a GraphicsDevice that can be used to draw onto this control.
        /// </summary>
        public GraphicsDevice GraphicsDevice
        {
            get { return graphicsDeviceService.GraphicsDevice; }
        }





        private MainForm(bool dummy)
        {
            Services = new ServiceContainer();
            Content = new ContentManager(Services, "ModelEditorContent");
            setupContentManager();
            SpriteBatchExtensions.Initialize(this);

            initializeTextures();
            InitializeComponent();
            menuStrip.Visible = false;
            flowLayoutPanel2.AutoSize = true; //TODO: is this line needed?
            blockViewWrapperControl.Initialize(this);
            MouseWheel += mouseWheel;

            ModelManager.SelectedModelChanged += (sender, e) => UpdateTitle();
            ModelManager.ModelNameChanged     += (sender, e) => UpdateTitle();
            ModelManager.ModelNeedsSaving     += (sender, e) => UpdateTitle();
        }

        public MainForm()
            : this(false)
        {
            Model model = new Model();
            ModelManager.Models.Add(model);
            ModelManager.Animations.Add(new Animation());
            ModelManager.Frames.Add(new SaveFrame<Texture2DWithPos>());

            model.ChangedSinceLastSave = false;
        }

        public MainForm(string fileName)
            : this(false)
        {
            open(fileName);
        }

        private void initializeTextures()
        {
            transparentOneByOneTexture = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            transparentOneByOneTexture.SetData<Color>(new Color[] { Color.Transparent });
            whiteOneByOneTexture = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            whiteOneByOneTexture.SetData<Color>(new Color[]{Color.White});
        }

        private void setupContentManager()
        {
            // Don't initialize the graphics device if we are running in the designer.
            if (!DesignMode)
            {
                graphicsDeviceService = GraphicsDeviceService.AddRef(Handle,
                                                                     ClientSize.Width,
                                                                     ClientSize.Height);

                // Register the service, so components like ContentManager can find it.
                Services.AddService<IGraphicsDeviceService>(graphicsDeviceService);
            }
        }





        #region delegate mousewheel input
        private void mouseWheel(object sender, MouseEventArgs e)
        {
            if (GetChildAtPoint(e.Location) == blockViewWrapperControl)
            {
                System.Drawing.Point newLocation = e.Location.ToXnaPoint().Subtract(blockViewWrapperControl.Location.ToXnaPoint()).ToSDPoint();
                blockViewWrapperControl.OnMouseWheel(new MouseEventArgs(e.Button, e.Clicks, newLocation.X, newLocation.Y, e.Delta));
            }
        }
        #endregion

        public void UpdateTitle()
        {
            string path = ModelManager.SelectedModel.Path;
            Text = TITLE + " - " + (path==null?"untitled":path) +
                (ModelManager.SelectedModel.ChangedSinceLastSave ? "*" : "") +
                " [" + Math.Round(blockViewWrapperControl.Camera.Zoom * 100, 0) + "%]";
        }

        #region save/open/import
        private void saveToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            save(ModelManager.SelectedModel.Path == null);
        }

        private void saveAsToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            save(true);
        }

        private void save(bool saveAs)
        {
            if (ModelManager.SelectedModel.Blocks.Count == 0 &&
                ModelManager.SelectedModel.SubModels.Count == 0)
            {
                MessageBox.Show("You can't save an empty model!", "Save error");
            }
            else if (saveAs)
            {
                saveFileDialog.ShowDialog();
            }
            else
            {
                writeSave(ModelManager.SelectedModel, ModelManager.SelectedModel.Path);
            }
        }

        private void saveFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            writeSave(ModelManager.SelectedModel, saveFileDialog.FileName);
        }

        private void writeSave(Model model, string fileName)
        {
            while (true)
            {
                try
                {
                    model.Save(fileName);
                    return;
                }
                catch (Exception e)
                {
                    if (MessageBox.Show("Unable to save to '"+fileName+"':\n"+e.Message, "Error saving file",
                                        MessageBoxButtons.RetryCancel, MessageBoxIcon.Stop)
                                        != System.Windows.Forms.DialogResult.Retry)
                    {
                        return;
                    }
                }
            }
        }


        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog();
        }
        private void openFileDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (ModelManager.SelectedModel.ChangedSinceLastSave &&
                !(ModelManager.SelectedModel.Blocks.Count == 0 &&
                ModelManager.SelectedModel.SubModels.Count == 0))
            {
                DialogResult dialogResult = MessageBox.Show("Save changes to " + ModelManager.SelectedModel.Path + "?",
                    TITLE, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                switch (dialogResult)
                {
                    case DialogResult.Yes:
                        save(true);
                        break;
                    case DialogResult.No:
                        break;
                    case DialogResult.Cancel:
                        return;
                }
            }
            open(openFileDialog.FileName);
        }

        private void open(string path)
        {
            Model model = new Model(EggModelLoader.Load(path).ToTexture2D(GraphicsDevice));
            ModelManager.Models.Add(model);
            ModelManager.SelectedModel = model;
            //TODO: are the following two lines needed?
            ModelManager.SelectedAnimation = ModelManager.Animations[0];
            ModelManager.SelectedFrame = ModelManager.Frames[0];
            blockViewWrapperControl.EditingMode = EditingMode.Texture;
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            importFileDialog.ShowDialog();
        }

        private void importFileDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            importImage(importFileDialog.FileNames);
        }

        private void importImage(string[] fileNames)
        {
            foreach (string fileName in fileNames)
            {
                importImage(fileName);
            }
        }

        private Point lastImportedTextureOffset = Point.Zero;
        private void importImage(string fileName)
        {
            lastImportedTextureOffset = lastImportedTextureOffset.Add(20, 20);

            Texture2DWithPos tex;
            using (Stream fileStream = new FileStream(fileName, FileMode.Open))
            {
                tex = new Texture2DWithPos(new SD.Bitmap(fileStream), GraphicsDevice, fileName);
            }
            tex.Position = lastImportedTextureOffset;

            blockViewWrapperControl.EditingMode = EditingMode.Texture;
            foreach (Texture2DWithPos existingTex in((IEnumerable<Animation>)ModelManager.Animations).GetAllTextures())
            {
                if (existingTex.name == tex.name && existingTex.OriginalPath != tex.OriginalPath)
                {
                    MessageBox.Show("Texture relative path name collision: '"+tex.name+
                        "', while original path name was not the same: '"+tex.OriginalPath+
                        "'!", "It should be possible to click on a texture and "+
                        "specify it's name.\nMake such a box popup here!");
                    return;
                }
            }
            ModelManager.SelectedFrame.Images.AddToFront(tex);
        }
        #endregion

        private void menuStrip_MenuActivate(object sender, EventArgs e)
        {
            menuStrip.Visible = true;
        }
        private void menuStrip_MenuDeactivate(object sender, EventArgs e)
        {
            menuStrip.Visible = false;
        }



        private void toolStripUpdated()
        {
            List<ToolStrip> list = new List<ToolStrip>();
            foreach (Control toolStrip in flowLayoutPanel2.Controls)
            {
                if (toolStrip is ToolStrip && !(toolStrip is MenuStrip))
                {
                    list.Add((ToolStrip)toolStrip);
                }
            }
            list.Reverse();
            //toolStripContainer1.SuspendLayout();
            foreach (ToolStrip toolStrip in list)
            {
                toolStrip.Visible = false;
            }
            foreach (ToolStrip toolStrip in list)
            {
                toolStrip.Top = 0;
                toolStrip.Left = 0;
                toolStrip.Visible = true;
            }
            //toolStripContainer1.ResumeLayout(true);
        }

        internal void AddToolStrip(Control toolStrip)
        {
            this.flowLayoutPanel2.Controls.Add(toolStrip);
            toolStripUpdated();
        }
        internal void RemoveToolStrip(Control toolStrip)
        {
            this.flowLayoutPanel2.Controls.Remove(toolStrip);
            toolStripUpdated();
        }
        internal void AddToolStrip(IEnumerable<Control> toolStrips)
        {
            foreach (Control toolStrip in toolStrips.OrderBy(t => t.Left))
            {
                this.flowLayoutPanel2.Controls.Add(toolStrip);
            }
            toolStripUpdated();
        }
        internal void RemoveToolStrip(IEnumerable<Control> toolStrips)
        {
            foreach (Control toolStrip in toolStrips)
            {
                this.flowLayoutPanel2.Controls.Remove(toolStrip);
            }
            toolStripUpdated();
        }
    }
}
