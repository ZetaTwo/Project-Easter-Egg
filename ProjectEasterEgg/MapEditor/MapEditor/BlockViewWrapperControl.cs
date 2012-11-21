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

namespace Mindstep.EasterEgg.MapEditor
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

        private EditingMode editingMode;
        public EditingMode EditingMode
        {
            get { return editingMode; }
            set
            {
                editingMode = value;
                blockViewer = blockViewers[editingMode];
            }
        }

        private BlockViewControl blockViewer;
        public BlockViewControl BlockViewer { get { return blockViewer; } }





        public BlockViewWrapperControl()
        {
            InitializeComponent();

            setupBlockViewers();
        }


        public void Initialize(MainForm mainForm)
        {
            this.MainForm = mainForm;

            camera = new Camera(new float[] { .25f, .5f, .75f, 1, 2, 4, 6, 8, 12, 16, 24, 32 }, 3, new Point(Width / 2, Height / 2));

            initializeBlockViewers();
        }
        private void setupBlockViewers()
        {
            blockViewers.Add(EditingMode.Block, new BlockEditingControl());
        }
        private void initializeBlockViewers()
        {
            foreach (BlockViewControl viewer in blockViewers.Values)
            {
                viewer.Dock = DockStyle.Fill;

                viewer.Initialize(MainForm, this);
                Controls.Add(viewer);
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
    }
}
