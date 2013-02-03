using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Microsoft.Xna.Framework.Graphics;
using Mindstep.EasterEgg.Commons;
using Mindstep.EasterEgg.Commons.Graphic;
using SDPoint = System.Drawing.Point;
using Microsoft.Xna.Framework;
using Mindstep.EasterEgg.Commons.SaveLoad;

namespace Mindstep.EasterEgg.MapEditor.Viewers
{
    public partial class BlockViewWrapperControl : UserControl
    {
        protected static Dictionary<BlockType, Color> blockTypeColor;
        static BlockViewWrapperControl()
        {
            blockTypeColor = new Dictionary<BlockType, Color>();
            blockTypeColor.Add(BlockType.SOLID, Color.Green);
            blockTypeColor.Add(BlockType.WALKABLE, Color.Olive);
            blockTypeColor.Add(BlockType.STAIRS, Color.Brown);
            blockTypeColor.Add(BlockType.SPAWN_LOCATION, Color.White);
        }

        protected Dictionary<BlockDrawState, Texture2D> blockDrawStateTexture = new Dictionary<BlockDrawState, Texture2D>();
        private Dictionary<EditingMode, BlockViewControl> blockViewers = new Dictionary<EditingMode, BlockViewControl>();

        private MainForm mainForm;
        public MainForm MainForm
        {
            get
            {
                return mainForm;
            }
            set
            {
                mainForm = value;
                EditingMode = editingMode;
            }
        }

        private Camera camera;
        public Camera Camera
        {
            get
            {
                return camera;
            }
            protected set
            {
                camera = value;
            }
        }

        public BlockViewControl BlockViewer { get { return blockViewers[editingMode]; } }
        private EditingMode editingMode;
        private TextureProjectionControl textureProjectionControl;
        public EditingMode EditingMode
        {
            get { return editingMode; }
            set
            {
                if (MainForm != null)
                {
                    //BlockViewControl old = BlockViewer;
                    MainForm.RemoveToolStrip(BlockViewer.ToolStrips);
                    BlockViewer.Visible = false;
                    editingMode = value;
                    BlockViewer.Visible = true;
                    MainForm.AddToolStrip(BlockViewer.ToolStrips);
                    BlockViewer.Focus();
                    //old.Enabled = old.Visible = false;
                    //old.Enabled = false;
                    //Controls.Add(BlockViewer);
                }
                else
                {
                    editingMode = value;
                }
                UpdatedSettings();
            }
        }
        public void enterTextureProjectionMode(Texture2DWithPos textureToProjectDown)
        {
            textureProjectionControl.enterTextureProjectionMode(textureToProjectDown);
            EditingMode = EditingMode.TextureProjection;
        }





        public BlockViewWrapperControl()
        {
            InitializeComponent();

            setupBlockViewers();
            ValidateChildren();
            Invalidated += (sender, e) => BlockViewer.Invalidate();
        }


        public void Initialize(MainForm mainForm)
        {
            this.MainForm = mainForm;
            MainForm.AddToolStrip(toolStrip1);
            MainForm.AddToolStrip(toolStrip2);
            MainForm.AddToolStrip(toolStrip3);

            camera = new Camera(new float[] { .25f, .5f, .75f, 1, 2, 4, 6, 8, 12, 16, 24, 32 }, 3, new Point(Width / 2, Height / 2));

            initializeBlockViewers();
            EditingMode = EditingMode; //Reset EditingMode after MainForm has been set.
            UpdatedSettings();
        }
        private void setupBlockViewers()
        {
            textureProjectionControl = new TextureProjectionControl();
            blockViewers.Add(EditingMode.Block, new BlockEditingControl());
            blockViewers.Add(EditingMode.Texture, new TextureEditingControl());
            blockViewers.Add(EditingMode.TextureProjection, textureProjectionControl);
            foreach (BlockViewControl viewer in blockViewers.Values)
            {
                viewer.Dock = DockStyle.Fill;
                Controls.Add(viewer);
                viewer.Visible = false;
            }
        }
        private void initializeBlockViewers()
        {
            foreach (BlockViewControl viewer in blockViewers.Values)
            {
                viewer.Initialize(MainForm, this);
            }
        }





        /// <summary>
        /// new OnMouseWheel to make it public
        /// </summary>
        /// <param name="e"></param>
        new public void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
        }

        internal void UpdatedSettings()
        {
            toolStripDrawBlockSolid.Checked = BlockViewer.BlockDrawState == BlockDrawState.Solid;
            toolStripDrawBlockWireframe.Checked = BlockViewer.BlockDrawState == BlockDrawState.Wireframe;
            toolStripDrawBlockNone.Checked = BlockViewer.BlockDrawState == BlockDrawState.None;

            toolStripEditBlocks.Checked = EditingMode == EditingMode.Block;
            toolStripEditTextures.Checked = EditingMode == EditingMode.Texture ||
                EditingMode == EditingMode.TextureProjection;

            //something related to background color could be changed here too

            textureOpacityTrackBar.TrackBar.Value = (int)(BlockViewer.TextureOpacity * textureOpacityTrackBar.TrackBar.Maximum);

            Invalidate();
        }



        private void toolStripDrawBlockSolid_Click(object sender, EventArgs e)
        {
            BlockViewer.BlockDrawState = BlockDrawState.Solid;
        }

        private void toolStripDrawBlockWireframe_Click(object sender, EventArgs e)
        {
            BlockViewer.BlockDrawState = BlockDrawState.Wireframe;
        }

        private void toolStripDrawBlockNone_Click(object sender, EventArgs e)
        {
            BlockViewer.BlockDrawState = BlockDrawState.None;
        }

        private void toolStripEditBlocks_Click(object sender, EventArgs e)
        {
            EditingMode = EditingMode.Block;
        }

        private void toolStripEditTextures_Click(object sender, EventArgs e)
        {
            EditingMode = EditingMode.Texture;
        }

        private void textureOpacityTrackBar_Scroll(object sender, EventArgs e)
        {
            BlockViewer.TextureOpacity = (float)textureOpacityTrackBar.TrackBar.Value / textureOpacityTrackBar.TrackBar.Maximum;
        }

        private void textureOpacityTrackBar_MouseUp(object sender, MouseEventArgs e)
        {
            BlockViewer.Focus();
        }

        private void toolStripButtonSelectBackgroundColor_Click(object sender, EventArgs e)
        {
            backgroundColorDialog.ShowDialog();
            BlockViewer.BackColor = backgroundColorDialog.Color;
        }
    }
}
