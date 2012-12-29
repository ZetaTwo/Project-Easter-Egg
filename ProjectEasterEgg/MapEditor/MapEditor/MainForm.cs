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

namespace Mindstep.EasterEgg.MapEditor
{
    public partial class MainForm : Form
    {
        private static string TITLE = "Easter Egg Editor";

        private SaveModel<Texture2DWithPos> model;
        public SaveModel<Texture2DWithPos> CurrentModel { get { return model; } }
        public SaveAnimation<Texture2DWithPos> CurrentAnimation { get { return CurrentModel.CurrentAnimation; } }
        public SaveFrame<Texture2DWithPos> CurrentFrame { get { return CurrentAnimation.CurrentFrame; } }

        private string lastSavedDoc;
        private bool changedSinceLastSave;
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
            model = new SaveModel<Texture2DWithPos>("untitled");


            initializeTextures();
            InitializeComponent();
            blockViewWrapperControl.Initialize(this);
            MouseWheel += new MouseEventHandler(mouseWheel);

            RefreshTitle();
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

        public void RefreshTitle()
        {
            Text = TITLE + " - " + model.name + (changedSinceLastSave ? "*" : "") + " [" + Math.Round(blockViewWrapperControl.Camera.Zoom * 100, 0) + "%]";
        }

        #region save/open/import
        private void saveToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(lastSavedDoc))
            {
                saveAsClicked();
            }
            else
            {
                saveClicked();
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            saveAsClicked();
        }

        private void saveClicked()
        {
            if (CurrentModel.blocks.Count == 0 &&
                CurrentModel.subModels.Count == 0)
            {
                MessageBox.Show("You can't save an empty model!", "Save error");
            }
            else
            {
                save(lastSavedDoc);
            }
        }

        private void saveAsClicked()
        {
            if (CurrentModel.blocks.Count == 0 &&
                CurrentModel.subModels.Count == 0)
            {
                MessageBox.Show("You can't save an empty model!", "Save error");
            }
            else
            {
                saveFileDialog.ShowDialog();
            }
        }

        private void saveFileDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            save(saveFileDialog.FileName);
        }

        private void save(string fileName)
        {
            bool savedSuccessfully;
            
            do
            {
                savedSuccessfully = EggModelSaver.Save(CurrentModel, fileName);
            }
            while (!savedSuccessfully &&
                MessageBox.Show("Unable to save to: " + fileName, "Error saving file",
                MessageBoxButtons.RetryCancel, MessageBoxIcon.Stop)
                == System.Windows.Forms.DialogResult.Retry);
            
            if (savedSuccessfully)
            {
                lastSavedDoc = fileName;
                changedSinceLastSave = false;
                RefreshTitle();
            }
        }


        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog();
        }
        private void openFileDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (changedSinceLastSave &&
                !(CurrentModel.blocks.Count == 0 &&
                CurrentModel.subModels.Count == 0))
            {
                DialogResult dialogResult = MessageBox.Show("Save changes to " + model.name + "?",
                    TITLE, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                switch (dialogResult)
                {
                    case DialogResult.Yes:
                        saveAsClicked();
                        break;
                    case DialogResult.No:
                        break;
                    case DialogResult.Cancel:
                        return;
                }
            }
            open(openFileDialog.FileName);
        }

        private void open(string fileName)
        {
            model = EggModelLoader.Load(fileName).ToTexture2D(GraphicsDevice);
            lastSavedDoc = fileName;
            blockViewWrapperControl.EditingMode = EditingMode.Texture;
            changedSinceLastSave = false;
            RefreshTitle();
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
            foreach (Texture2DWithPos existingTex in model.animations.GetAllTextures())
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
            CurrentFrame.Images.AddToFront(tex);
            UpdatedThings();
        }
        #endregion

        internal void UpdatedThings()
        {
            if (!changedSinceLastSave)
            {
                changedSinceLastSave = true;
                RefreshTitle();
            }
            //blockViewWrapperControl.Invalidate();
        }
        internal void UpdatedGraphics()
        {
            blockViewWrapperControl.Invalidate();
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
            List<Control> list = new List<Control>();
            foreach (Control toolStrip in toolStripContainer1.TopToolStripPanel.Controls)
            {
                list.Add(toolStrip);
            }
            list.Reverse();
            foreach (Control toolStrip in list)
            {
                toolStrip.Top = 0;
                toolStrip.Left = 0;
            }
        }
        internal void AddToolStrip(Control toolStrip)
        {
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(toolStrip);
            toolStripUpdated();
        }
        internal void RemoveToolStrip(Control toolStrip)
        {
            this.toolStripContainer1.TopToolStripPanel.Controls.Remove(toolStrip);
        }
        internal void AddToolStrip(IEnumerable<Control> toolStrips)
        {
            foreach (Control toolStrip in toolStrips.OrderBy(t => t.Left))
            {
                AddToolStrip(toolStrip);
            }
        }
        internal void RemoveToolStrip(IEnumerable<Control> toolStrips)
        {
            foreach (Control toolStrip in toolStrips)
            {
                RemoveToolStrip(toolStrip);
            }
            toolStripUpdated();
        }
    }
}
