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





        public MainForm()
        {
            Services = new ServiceContainer();
            Content = new ContentManager(Services, "ModelEditorContent");
            setupContentManager();
            SpriteBatchExtensions.Initialize(this);
            ModelManager.Models.Add(new SaveModelWithInfo());
            ModelManager.CurrentModel = ModelManager.Models[0];
            //ModelManager.CurrentModel = new SaveModel<Texture2DWithPos>("untitled");


            initializeTextures();
            InitializeComponent();
            menuStrip.Visible = false;
            flowLayoutPanel2.AutoSize = true;
            blockViewWrapperControl.Initialize(this);
            MouseWheel += new MouseEventHandler(mouseWheel);

            UpdateTitle();
        }

        public MainForm(string fileName)
            : this()
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
            Text = TITLE + " - " + ModelManager.CurrentModel.Name +
                (ModelManager.CurrentModel.changedSinceLastSave ? "*" : "") +
                " [" + Math.Round(blockViewWrapperControl.Camera.Zoom * 100, 0) + "%]";
        }

        #region save/open/import
        private void saveToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            save(ModelManager.CurrentModel.path == null);
        }

        private void saveAsToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            save(true);
        }

        private void save(bool saveAs)
        {
            if (ModelManager.CurrentModel.blocks.Count == 0 &&
                ModelManager.CurrentModel.subModels.Count == 0)
            {
                MessageBox.Show("You can't save an empty model!", "Save error");
            }
            else if (saveAs)
            {
                saveFileDialog.ShowDialog();
            }
            else
            {
                writeSave(ModelManager.CurrentModel.path);
            }
        }

        private void saveFileDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            writeSave(saveFileDialog.FileName);
        }

        private void writeSave(string fileName)
        {
            while (true)
            {
                try
                {
                    EggModelSaver.Save(ModelManager.CurrentModel, fileName);
                    ModelManager.CurrentModel.changedSinceLastSave = false;
                    ModelManager.CurrentModel.path = fileName;
                    ModelManager.CurrentModel.Name = Regex.Match(fileName,
                        "([^/\\\\]*)(\\.egg)?$", RegexOptions.RightToLeft).Groups[1].Value;
                    UpdateTitle();
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
            if (ModelManager.CurrentModel.changedSinceLastSave &&
                !(ModelManager.CurrentModel.blocks.Count == 0 &&
                ModelManager.CurrentModel.subModels.Count == 0))
            {
                DialogResult dialogResult = MessageBox.Show("Save changes to " + ModelManager.CurrentModel.Name + "?",
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
            SaveModelWithInfo model = EggModelLoader.Load(path).ToTexture2D<SaveModelWithInfo>(GraphicsDevice);
            ModelManager.Models.Add(model);
            ModelManager.CurrentModel = model;
            model.path = path;
            model.changedSinceLastSave = false;
            blockViewWrapperControl.EditingMode = EditingMode.Texture;
            UpdateTitle();
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
            tex.pos = lastImportedTextureOffset;

            blockViewWrapperControl.EditingMode = EditingMode.Texture;
            foreach (Texture2DWithPos existingTex in ModelManager.CurrentModel.animations.GetAllTextures())
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
            ModelManager.CurrentFrame.Images.AddToFront(tex);
            ChangedSomethingThatNeedsToBeSaved();
        }
        #endregion

        internal void ChangedSomethingThatNeedsToBeSaved()
        {
            if (!ModelManager.CurrentModel.changedSinceLastSave)
            {
                ModelManager.CurrentModel.changedSinceLastSave = true;
                UpdateTitle();
            }
        }

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
