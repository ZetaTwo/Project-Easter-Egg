#region Using Statements
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Mindstep.EasterEgg.Commons;
using System;
using System.Collections.Generic;
#endregion

namespace Mindstep.EasterEgg.MapEditor
{
    class Zoom {
        private static float[] zooms = { .5f, .75f, 1, 2, 4, 6, 8, 12, 16, 24, 32 };
        private int zoomIndex = 2;
        public Matrix Matrix = Matrix.Identity;
        
        public void In()
        {
            zoomIndex = Math.Min(zoomIndex + 1, zooms.Length - 1);
            Matrix = Matrix.CreateScale(this);
        }

        public void Out()
        {
            zoomIndex = Math.Max(zoomIndex - 1, 0);
            Matrix = Matrix.CreateScale(this);
        }

        public static implicit operator float(Zoom z) {
            return zooms[z.zoomIndex];
        }
    }

    /// <summary>
    /// Example control inherits from GraphicsDeviceControl, which allows it to
    /// render using a GraphicsDevice. This control shows how to draw animating
    /// 3D graphics inside a WinForms application. It hooks the Application.Idle
    /// event, using this to invalidate the control, which will cause the animation
    /// to constantly redraw.
    /// </summary>
    class MainView : GraphicsDeviceControl
    {
        private SpriteBatch spriteBatch;
        private SpriteEffects spriteEffect;
        private Texture2D block;
        private Texture2D grid;
        private Vector2 center;
        private Vector2 offset;

        public MainForm MainForm;
        private int tileHeight;
        private int tileWidth;
        private int blockHeight;
        private bool draggingView;
        private System.Drawing.Point lastMouseLocation;
        private SamplerState samplerState;

        public Zoom Zoom = new Zoom();


        protected override void Initialize()
        {
            Load("mainBlock31", "mainGrid31");
            MouseDown += new MouseEventHandler(MainView_MouseDown);
            MouseUp += new MouseEventHandler(MainView_MouseUp);
            MouseMove += new MouseEventHandler(MainView_MouseMove);
            DragDrop += new DragEventHandler(MainView_DragDrop);
            DragEnter += new DragEventHandler(MainView_DragEnter);

            samplerState = new SamplerState();
            samplerState.Filter = TextureFilter.PointMipLinear;
            samplerState.AddressU = TextureAddressMode.Clamp;
            samplerState.AddressV = TextureAddressMode.Clamp;
        }

        /// <summary>
        /// Initializes the control.
        /// </summary>
        public void Load(string blockS, string gridS)
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteEffect = SpriteEffects.None;

            ContentManager Content = new ContentManager(Services, "MapEditorContent");
            block = Content.Load<Texture2D>(blockS);
            grid = Content.Load<Texture2D>(gridS);
            if (!block.Bounds.Equals(grid.Bounds))
            {
                throw new Exception("mainBlock and mainGrid image size mismatch.");
            }

            blockHeight = block.Height - (block.Width + 1) / 2;
            tileWidth = block.Width;
            tileHeight = block.Height - blockHeight;
            center = new Vector2((Width-tileWidth) / 2, (Height-block.Height) / 2);
            offset = new Vector2(0, 0);

            // Hook the idle event to constantly redraw our animation.
            Application.Idle += delegate { Invalidate(); };
        }


        /// <summary>
        /// Draws the control.
        /// </summary>
        protected override void Draw()
        {
            GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.Black);
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, samplerState, null, null, null, Zoom.Matrix);

            BoundingBoxInt boundingBox = new BoundingBoxInt(MainForm.BlockPositions);

            List<Position> tiles = new List<Position>();
            for (int x = -5; x < 10; x += 1)
            {
                for (int y = -5; y < 10; y += 1)
                {
                    Position tilePos = new Position(x, y, -1);
                    tiles.Add(tilePos);
                    boundingBox.addPos(tilePos);
                }
            }

            foreach (Position tilePos in tiles)
            {
                drawBlock(grid, boundingBox, Color.White, tilePos);
            }

            foreach (Position pos in MainForm.BlockPositions)
            {
                Color color;
                if (pos.Z == MainForm.CurrentHeight)
                {
                    color = Color.Green;
                }
                else
                {
                    color = Color.Red;
                }
                drawBlock(block, boundingBox, color, pos);
            }
            spriteBatch.End();
        }

        private void drawBlock(Texture2D image, BoundingBoxInt boundingBox, Microsoft.Xna.Framework.Color color, Position pos)
        {
            float depth = boundingBox.getDepth(pos);
            Vector2 screenCoords = Transform.ToScreen(pos, tileHeight, tileWidth, blockHeight, (center+offset).toPoint()).toVector2();
            spriteBatch.Draw(image, screenCoords, null, color, 0, Vector2.Zero, 1, spriteEffect, depth/Zoom);
        }

        private void MainView_MouseDown(object sender, MouseEventArgs e)
        {
            draggingView = true;
            lastMouseLocation = e.Location;
        }

        private void MainView_MouseUp(object sender, MouseEventArgs e)
        {
            draggingView = false;
        }

        private void MainView_MouseMove(object sender, MouseEventArgs e)
        {
            if (draggingView)
            {
                offset.X += (e.Location.X - lastMouseLocation.X) / Zoom ;
                offset.Y += (e.Location.Y - lastMouseLocation.Y) / Zoom ;
                lastMouseLocation = e.Location;
            }
        }

        public void MainView_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                Zoom.In();
                MainForm.RefreshTitle();
            }
            else if (e.Delta < 0)
            {
                Zoom.Out();
                MainForm.RefreshTitle();
            }
        }

        public void MainView_DragEnter(object sender, DragEventArgs e)
        {
            // As we are interested in Image data only
            // we will check this as follows
            if (e.Data.GetDataPresent(typeof(System.Drawing.Bitmap)))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
            System.Console.WriteLine(e.Data);
        }

        public void MainView_DragDrop(object sender, DragEventArgs e)
        {
            System.Console.WriteLine("drop!");
        }
    }
}
