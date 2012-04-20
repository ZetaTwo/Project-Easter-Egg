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

namespace Mindstep.EasterEgg.MapEditor
{
    public partial class MainForm : Form
    {
        private static string TITLE = "Easter Egg Editor";
        private string lastSavedDoc;
        private int currentHeight = 0;
        public int CurrentLayer
        {
            get { return currentHeight; }
            set
            {
                if (CurrentEditingMode == EditingMode.Block)
                {
                    currentHeight = value;
                    UpdatedGraphics();
                }
            }
        }

        private SaveModel<Texture2DWithPos> model;
        public SaveModel<Texture2DWithPos> CurrentModel { get { return model; } }
        private SaveAnimation<Texture2DWithPos> currentAnimation;
        public SaveAnimation<Texture2DWithPos> CurrentAnimation { get { return currentAnimation; } }
        public SaveFrame<Texture2DWithPos> CurrentFrame { get { return currentAnimation.CurrentFrame; } }
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

        private Color backgroundColor = Color.Black;
        public Color BackgroundColor { get { return backgroundColor; } }

        public MainForm()
        {
            Services = new ServiceContainer();
            Content = new ContentManager(Services, "MapEditorContent");
            SetupContentManager();
            SpriteBatchExtensions.Initialize(this);
            model = new SaveModel<Texture2DWithPos>("untitled");


            initializeTextures();
            InitializeComponent();
            CurrentBlockDrawState = BlockDrawState.Solid;
            CurrentEditingMode = EditingMode.Block;
            mainView.Initialize(this);
            MouseWheel += new MouseEventHandler(mouseWheel);
            RefreshTitle();
            currentAnimation = new SaveAnimation<Texture2DWithPos>("still");
            currentAnimation.Facing = Facing.POSITIVE_Y;

            toolStrip.Items.Add(new ToolStripControlHost(trackBarTextureOpacity));
        }

        private void initializeTextures()
        {
            transparentOneByOneTexture = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            transparentOneByOneTexture.SetData<Color>(new Color[] { Color.Transparent });
            whiteOneByOneTexture = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            whiteOneByOneTexture.SetData<Color>(new Color[]{Color.White});
        }

        private void SetupContentManager()
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

        private void upButton_Click(object sender, System.EventArgs e)
        {
            CurrentLayer++;
        }

        private void downButton_Click(object sender, System.EventArgs e)
        {
            CurrentLayer--;
        }

        #region zoom (mousewheel)
        private void mouseWheel(object sender, MouseEventArgs e)
        {
            if (GetChildAtPoint(e.Location) == mainView)
            {
                System.Drawing.Point newLocation = e.Location.ToXnaPoint().Subtract(mainView.Location.ToXnaPoint()).ToSDPoint();
                mainView.MainView_MouseWheel(sender, new MouseEventArgs(e.Button, e.Clicks, newLocation.X, newLocation.Y, e.Delta));
            }
        }
        #endregion

        public void RefreshTitle()
        {
            Text = TITLE + " - " + model.name + (changedSinceLastSave ? "*" : "") + " [" + Math.Round(mainView.Zoom * 100, 0) + "%]";
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
            EggModelSaver.Save(CurrentModel, fileName);
            lastSavedDoc = fileName;
            changedSinceLastSave = false;
            RefreshTitle();
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
            changedSinceLastSave = false;
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
        private BlockDrawState blockDrawState;

        private void importImage(string fileName)
        {
            Texture2DWithPos tex = new Texture2DWithPos(fileName);

            CurrentEditingMode = EditingMode.Texture;
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
            lastImportedTextureOffset = lastImportedTextureOffset.Add(20, 20);
            tex.pos = lastImportedTextureOffset;
            using (Stream texStream = new FileStream(fileName, FileMode.Open))
            {
                tex.Texture = Texture2D.FromStream(GraphicsDevice, texStream);
            }
            CurrentFrame.Images.AddToFront(tex);
            UpdatedThings();
        }
        #endregion

        internal bool DrawTextureIndices { get { return drawTextureIndices.Checked; } }

        internal void UpdatedThings()
        {
            if (!changedSinceLastSave)
            {
                changedSinceLastSave = true;
                RefreshTitle();
            }
            mainView.Invalidate();
        }
        internal void UpdatedGraphics()
        {
            mainView.Invalidate();
        }

        private void trackBarTextureOpacity_Scroll(object sender, EventArgs e)
        {
            UpdatedGraphics();
        }

        public float TextureOpacity { get { return (float)trackBarTextureOpacity.Value / trackBarTextureOpacity.Maximum; } }

        private void trackBarTextureOpacity_MouseUp(object sender, MouseEventArgs e)
        {
            mainView.Focus();
        }

        private void toolStripBlockDrawStateChanged(object sender, EventArgs e)
        {
            blockDrawStateChanged();
        }

        private void blockDrawStateChanged()
        {
            throw new NotImplementedException();
        }

        private void toolStripDrawBlockSolid_Click(object sender, EventArgs e)
        {
            CurrentBlockDrawState = BlockDrawState.Solid;
        }

        private void toolStripDrawBlockWireframe_Click(object sender, EventArgs e)
        {
            CurrentBlockDrawState = BlockDrawState.Wireframe;
        }

        private void toolStripDrawBlockNone_Click(object sender, EventArgs e)
        {
            CurrentBlockDrawState = BlockDrawState.None;
        }

        internal BlockDrawState CurrentBlockDrawState
        {
            get { return blockDrawState; }
            private set
            {
                toolStripDrawBlockSolid.Checked = value == BlockDrawState.Solid;
                toolStripDrawBlockWireframe.Checked = value == BlockDrawState.Wireframe;
                toolStripDrawBlockNone.Checked = value == BlockDrawState.None;
                blockDrawState = value;
                UpdatedGraphics();
            }
        }

        private EditingMode editingMode;
        internal EditingMode CurrentEditingMode
        {
            get { return editingMode; }
            set
            {
                toolStripEditBlocks.Checked = value == EditingMode.Block;
                toolStripEditTextures.Checked = value == EditingMode.Texture ||
                    value == EditingMode.TextureProjection;
                editingMode = value;
                UpdatedGraphics();
            }
        }

        private void toolStripEditBlocks_Click(object sender, EventArgs e)
        {
            CurrentEditingMode = EditingMode.Block;
        }

        private void toolStripEditTextures_Click(object sender, EventArgs e)
        {
            CurrentEditingMode = EditingMode.Texture;
        }

        private void toolStripButtonSelectBackgroundColor_Click(object sender, EventArgs e)
        {
            backgroundColorDialog.ShowDialog();
            backgroundColor = backgroundColorDialog.Color.ToXnaColor();
            UpdatedGraphics();
        }

        private void menuStrip_MenuActivate(object sender, EventArgs e)
        {
            menuStrip.Visible = true;
        }

        private void menuStrip_MenuDeactivate(object sender, EventArgs e)
        {
            menuStrip.Visible = false;
        }

        private void drawTextureIndices_Click(object sender, EventArgs e)
        {
            UpdatedGraphics();
        }
    }
}
