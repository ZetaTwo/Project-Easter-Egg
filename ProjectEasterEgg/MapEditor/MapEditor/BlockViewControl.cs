using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Mindstep.EasterEgg.Commons;
using Microsoft.Xna.Framework.Graphics;
using Mindstep.EasterEgg.Commons.Graphic;
using SDPoint = System.Drawing.Point;
using Mindstep.EasterEgg.Commons.SaveLoad;

namespace Mindstep.EasterEgg.MapEditor
{
    public partial class BlockViewControl : GraphicsDeviceControl
    {
        protected static Dictionary<BlockDrawState, Texture2D> blockDrawStateTexture;
        protected static Dictionary<BlockType, Color> blockTypeColor;
        static BlockViewControl()
        {
            blockTypeColor = new Dictionary<BlockType, Color>();
            blockTypeColor.Add(BlockType.SOLID, Color.Green);
            blockTypeColor.Add(BlockType.WALKABLE, Color.Olive);
            blockTypeColor.Add(BlockType.STAIRS, Color.Brown);
            blockTypeColor.Add(BlockType.SPAWN_LOCATION, Color.White);
        }





        private bool panning;
        private bool mouseHasMovedSinceMouseDown = true;
        protected SDPoint lastMouseLocation;

        protected Texture2D textureBlock;
        protected Texture2D textureDrawing;
        protected Texture2D textureGridFilled;
        protected Texture2D textureGridStriped;
        protected Texture2D textureWireframe;
        protected Texture2D textureWireframeBack;
        protected Texture2D textureWireframeFilled;

        private SpriteFont spriteFont;
        private SpriteBatch spriteBatch;
        private SamplerState samplerState;

        /// <summary>
        /// A new MouseEventHandler to hide panning events
        /// </summary>
        new public event MouseEventHandler MouseDown;
        /// <summary>
        /// A new MouseEventHandler to hide panning events
        /// </summary>
        new public event MouseEventHandler MouseMove;
        /// <summary>
        /// An event that is raised whenever MouseUp is raised after a
        /// MouseDown without any intermediate MouseMoves
        /// </summary>
        public event MouseEventHandler MouseUpWithoutMoving;

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

        private Settings settings;
        internal Settings Settings
        {
            get
            {
                return settings;
            }
            set
            {
                settings = value;
            }
        }

        protected BlockViewWrapperControl Wrapper;





        public BlockViewControl()
        {
            InitializeComponent();

            Settings = new Settings(Color.Orange, BlockDrawState.Solid, 1f);
        }

        virtual public void Initialize(MainForm mainForm, BlockViewWrapperControl wrapper)
        {
            this.MainForm = mainForm;
            this.Wrapper = wrapper;

            spriteBatch = new SpriteBatch(mainForm.GraphicsDevice);
            spriteFont = MainForm.Content.Load<SpriteFont>("hudFont");

            samplerState = new SamplerState();
            samplerState.Filter = TextureFilter.PointMipLinear;
            samplerState.AddressU = TextureAddressMode.Clamp;
            samplerState.AddressV = TextureAddressMode.Clamp;

            textureBlock = MainForm.Content.Load<Texture2D>("block31");
            textureDrawing = MainForm.Content.Load<Texture2D>("block31drawing");
            textureGridFilled = MainForm.Content.Load<Texture2D>("block31filledGrid");
            textureGridStriped = MainForm.Content.Load<Texture2D>("block31stripedGrid");
            textureWireframe = MainForm.Content.Load<Texture2D>("block31wireframe");
            textureWireframeBack = MainForm.Content.Load<Texture2D>("block31wireframeBack");
            textureWireframeFilled = MainForm.Content.Load<Texture2D>("block31wireframeFilled");

            setupBlockDrawStateTextures();

            base.MouseDown += new MouseEventHandler(BlockViewControl_MouseDown);
            base.MouseMove += new MouseEventHandler(BlockViewControl_MouseMove);
            MouseWheel += new MouseEventHandler(BlockViewControl_MouseWheel);
            MouseUp += new MouseEventHandler(BlockViewControl_MouseUp);
            Resize += new EventHandler(BlockViewControl_Resize);
        }

        private void setupBlockDrawStateTextures()
        {
            if (blockDrawStateTexture == null) //singleton
            {
                blockDrawStateTexture = new Dictionary<BlockDrawState, Texture2D>();
                blockDrawStateTexture.Add(BlockDrawState.None, MainForm.transparentOneByOneTexture);
                blockDrawStateTexture.Add(BlockDrawState.Solid, textureBlock);
                blockDrawStateTexture.Add(BlockDrawState.Wireframe, textureWireframe);
            }
        }




        public BlockDrawState BlockDrawState
        {
            get { return settings.blockDrawState; }
            set
            {
                Settings.blockDrawState = value;
                MainForm.UpdatedSettings();
            }
        }

        public Color BackgroundColor
        {
            get { return settings.backgroundColor; }
            set
            {
                Settings.backgroundColor = value;
                MainForm.UpdatedSettings();
            }
        }
        public float TextureOpacity
        {
            get { return settings.opacity; }
            set
            {
                Settings.opacity = value;
                MainForm.UpdatedSettings();
            }
        }





        private void BlockViewControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                panning = true;
            }
            else if (panning && e.Button == MouseButtons.Left)
            {
                panning = false; //pressing the other mouse button only cancels the panning
            }
            else if (MouseDown != null)
            {
                //call any inheriting objects that have subscribed
                MouseDown(sender, e);
            }

            mouseHasMovedSinceMouseDown = false;
            lastMouseLocation = e.Location;
        }

        private void BlockViewControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (panning)
            {
                Point movement = e.Location.ToXnaPoint().Subtract(lastMouseLocation.ToXnaPoint());
                Wrapper.Camera.Offset = Wrapper.Camera.Offset.Add(movement);
                Invalidate();
            }
            else if (MouseMove != null)
            {
                MouseMove(sender, e); //call any inheriting objects that have subscribed
            }

            mouseHasMovedSinceMouseDown = true;
            lastMouseLocation = e.Location;
        }

        private void BlockViewControl_MouseUp(object sender, MouseEventArgs e)
        {
            panning = false;

            if (!mouseHasMovedSinceMouseDown && MouseUpWithoutMoving != null)
            {
                MouseUpWithoutMoving(sender, e);
            }
        }

        private void BlockViewControl_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                Wrapper.Camera.ZoomIn(e.Location.ToXnaPoint());
                MainForm.RefreshTitle();
                Invalidate();
            }
            else if (e.Delta < 0)
            {
                Wrapper.Camera.ZoomOut(e.Location.ToXnaPoint());
                MainForm.RefreshTitle();
                Invalidate();
            }
        }

        private void BlockViewControl_Resize(object sender, EventArgs e)
        {
            System.Console.WriteLine("BlockView resized");
        }

        /// <summary>
        /// new OnMouseWheel to make it public
        /// </summary>
        /// <param name="e"></param>
        new public void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
        }





        override protected void Draw()
        {
            GraphicsDevice.Clear(BackgroundColor);
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.NonPremultiplied, samplerState, null, null, null, Wrapper.Camera.ZoomAndOffsetMatrix);

            BoundingBoxInt boundingBox = getBoundingBox();

            drawGrid(boundingBox);
            drawBlocks(boundingBox);

#if false //debug draw mouse coords
                Vector3 v = CoordinateTransform.ScreenToObjectSpace(lastMouseLocation.ToXnaPoint(), Camera, CurrentLayer);
                Vector2 u = CoordinateTransform.ObjectToProjectionSpace(v);
                spriteBatch.DrawRectangle(new Rectangle((int)u.X, (int)u.Y, 1, 1), Color.Orchid, 5);
                spriteBatch.DrawString(spriteFont, v.ToString() + "\n" + v.ToPosition().ToString(), u, Color.Orange);
#endif
            spriteBatch.End();
        }

        virtual protected void drawGrid(BoundingBoxInt boundingBox)
        {
            for (int x = boundingBox.Min.X; x <= boundingBox.Max.X; x++)
            {
                for (int y = boundingBox.Min.Y; y <= boundingBox.Max.Y; y++)
                {
                    drawBlock(textureGridStriped, boundingBox, Color.White, new Position(x, y, -1), -0.01f);
                }
            }
        }

        virtual protected BoundingBoxInt getBoundingBox()
        {
            BoundingBoxInt boundingBox = new BoundingBoxInt(MainForm.CurrentModel.blocks.ToPositions());
            boundingBox.addPos(new Position(-3, -3, -1));
            boundingBox.addPos(new Position(3, 3, -1));
            const int margin = 2;
            boundingBox.addPos(boundingBox.Max + new Position(margin, margin, 0));
            boundingBox.addPos(boundingBox.Min - new Position(margin, margin, 0));
            return boundingBox;
        }

        virtual protected void drawBlocks(BoundingBoxInt boundingBox)
        {
            foreach (SaveBlock saveBlock in MainForm.CurrentModel.blocks)
            {
                drawBlock(textureBlock, boundingBox, blockTypeColor[saveBlock.type], saveBlock.Position);
            }
        }

        protected void drawBlock(Texture2D image, BoundingBoxInt boundingBox, Color color, Position pos, float depthOffset = 0)
        {
            float depth = boundingBox.getRelativeDepthOf(pos);
            Vector2 projCoords = CoordinateTransform.ObjectToProjectionSpace(pos);
            spriteBatch.Draw(image, projCoords + Constants.blockDrawOffset, null, color, 0, Vector2.Zero, 1, SpriteEffects.None, (depth + depthOffset) / Wrapper.Camera.Zoom);
        }
    }
}
