#region Using Statements
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Mindstep.EasterEgg.Commons;
using System;
using System.Collections.Generic;
using Mindstep.EasterEgg.MapEditor.Animations;
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
        private Vector2 offset;

        private MainForm mainForm;
        private int tileHeight;
        private int tileWidth;
        private int blockHeight;
        private bool panning;
        private System.Drawing.Point lastMouseLocation;
        private SamplerState samplerState;

        public Zoom Zoom = new Zoom();
        private Texture2DWithPos dragging;
        private SpriteFont spriteFont;
        public bool drawTextureIndices = true;

        public void Initialize(MainForm mainForm)
        {
            this.mainForm = mainForm;
            spriteBatch = new SpriteBatch(mainForm.GraphicsDevice);
            spriteEffect = SpriteEffects.None;
            spriteFont = mainForm.Content.Load<SpriteFont>("hudFont");

            Load("mainBlock31", "mainGrid31");
            MouseDown += new MouseEventHandler(MainView_MouseDown);
            MouseUp += new MouseEventHandler(MainView_MouseUp);
            MouseMove += new MouseEventHandler(MainView_MouseMove);

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
            block = mainForm.Content.Load<Texture2D>(blockS);
            grid = mainForm.Content.Load<Texture2D>(gridS);
            if (!block.Bounds.Equals(grid.Bounds))
            {
                throw new Exception("mainBlock and mainGrid image size mismatch.");
            }

            blockHeight = block.Height - (block.Width + 1) / 2;
            tileWidth = block.Width;
            tileHeight = block.Height - blockHeight;
            offset = new Vector2(0, 0);

            // Hook the idle event to constantly redraw our animation.
            Application.Idle += delegate { Invalidate(); };
        }


        /// <summary>
        /// Draws the control.
        /// </summary>
        protected override void Draw()
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, samplerState, null, null, null, Zoom.Matrix * Matrix.CreateTranslation(offset.X, offset.Y, 0));

            BoundingBoxInt boundingBox = new BoundingBoxInt(mainForm.SaveBlocks.ToPositions());

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

            foreach (Position pos in mainForm.SaveBlocks.ToPositions())
            {
                Color color;
                if (pos.Z == mainForm.CurrentHeight)
                {
                    color = Color.Green;
                }
                else
                {
                    color = Color.Red;
                }
                drawBlock(block, boundingBox, color, pos);
            }

            for (int i = 0; i < mainForm.AnimationManager.CurrentFrame.Textures.Count; i++)
            {

                Texture2DWithPos tex = mainForm.AnimationManager.CurrentFrame.Textures[i];
                float depth = 0.1f * (1 - (float)i / mainForm.AnimationManager.CurrentFrame.Textures.Count);
                spriteBatch.Draw(tex.Texture, tex.Coord.ToVector2(), null, Color.White, 0, Vector2.Zero, 1, spriteEffect, depth);
                if (mainForm.DrawTextureIndices())
                {
                    spriteBatch.DrawString(spriteFont, i.ToString(), tex.Coord.ToVector2(), Color.Green);
                }
            }
            spriteBatch.End();
        }

        private void drawBlock(Texture2D image, BoundingBoxInt boundingBox, Microsoft.Xna.Framework.Color color, Position pos)
        {
            float depth = boundingBox.getRelativeDepthOf(pos);
            Vector2 projCoords = CoordinateTransform.ObjectToProjectionSpace(pos);
            spriteBatch.Draw(image, projCoords, null, color, 0, Vector2.Zero, 1, spriteEffect, depth / Zoom);
        }

        private void MainView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                panning = true;
            }
            else if (e.Button == MouseButtons.Left)
            {
                for (int i = mainForm.AnimationManager.CurrentFrame.Textures.Count - 1; i >= 0; i--)
                {
                    Texture2DWithPos tex = mainForm.AnimationManager.CurrentFrame.Textures[i];
                    if (tex.Rectangle.Contains((e.Location.toVector2()-offset).ToPoint()))
                    {
                        dragging = tex;
                        break;
                    }
                }
            }
            lastMouseLocation = e.Location;
        }

        private void MainView_MouseUp(object sender, MouseEventArgs e)
        {
            panning = false;
            dragging = null;
        }

        public Vector2 ScreenToProjectionSpace(Point screenCoord)
        {
            return screenCoord.ToVector2() + offset;
        }

        private void MainView_MouseMove(object sender, MouseEventArgs e)
        {
            if (panning || dragging != null) {
                int movedX = e.Location.X - lastMouseLocation.X;
                int movedY = e.Location.Y - lastMouseLocation.Y;
                if (panning)
                {
                    offset.X += movedX;
                    offset.Y += movedY;
                }
                else
                {
                    dragging.Coord.X += movedX;
                    dragging.Coord.Y += movedY;
                }
                lastMouseLocation = e.Location;
            }
        }

        public void MainView_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                Zoom.In();
                mainForm.RefreshTitle();
            }
            else if (e.Delta < 0)
            {
                Zoom.Out();
                mainForm.RefreshTitle();
            }
        }
    }
}
