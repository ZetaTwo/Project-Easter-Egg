#region Using Statements
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Mindstep.EasterEgg.Commons;
using System;
#endregion

namespace Mindstep.EasterEgg.MapEditor
{
    /// <summary>
    /// Example control inherits from GraphicsDeviceControl, which allows it to
    /// render using a GraphicsDevice. This control shows how to draw animating
    /// 3D graphics inside a WinForms application. It hooks the Application.Idle
    /// event, using this to invalidate the control, which will cause the animation
    /// to constantly redraw.
    /// </summary>
    class TopView : GraphicsDeviceControl
    {
        private SpriteBatch spriteBatch;
        private SpriteEffects spriteEffect;
        private Texture2D top;
        private Texture2D topGrid;
        private Point viewPos;
        private int gridSize;
        private int blockSize;
        private float scale;
        public MainForm MainForm;


        /// <summary>
        /// Initializes the control.
        /// </summary>
        protected override void Initialize()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteEffect = SpriteEffects.None;

            ContentManager Content = new ContentManager(Services, "MapEditorContent");
            top = Content.Load<Texture2D>("top");
            topGrid = Content.Load<Texture2D>("topGrid");
            viewPos = new Point(Width/2, Height/2);
            gridSize = topGrid.Width;

            // Hook the idle event to constantly redraw our animation.
            Application.Idle += delegate { Invalidate(); };
        }


        /// <summary>
        /// Draws the control.
        /// </summary>
        protected override void Draw()
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            for (int x = viewPos.X%Width-Width; x < Width; x+=topGrid.Width)
            {
                for (int y = viewPos.Y%Height-Height; y < Height; y += topGrid.Height)
                {
                    spriteBatch.Draw(topGrid, new Vector2(x, y), null, Color.White, 0, Vector2.Zero, 1, spriteEffect, 1f);
                }
            }

            foreach (Block block in MainForm.Blocks)
            {
                Vector2 coords = new Vector2(block.Offset.X, block.Offset.Y);
                int height = block.Offset.Z-MainForm.CurrentHeight;
                if (height == 0)
                {
                    spriteBatch.Draw(top, coords, null, Color.Green, 0, Vector2.Zero, 1, spriteEffect, 0);
                }
                else
                {
                    spriteBatch.Draw(top, coords, null, Color.Red, 0, Vector2.Zero, 1, spriteEffect, .5f);
                }
            }
            spriteBatch.End();
        }

        public System.Drawing.Point getClosestGridPoint(System.Drawing.Point rp)
        {
            return new System.Drawing.Point(rp.X + gridSize - viewPos.X % gridSize - rp.X % gridSize, rp.Y - viewPos.Y % gridSize - rp.Y % gridSize);
        }

        internal void toggleBlock(System.Drawing.Point p)
        {
            Position newPos = new Position(p.X, p.Y, MainForm.CurrentHeight);
            for (int i=0; i<MainForm.Blocks.Count; i++)
            {
                if (MainForm.Blocks[i].Offset.Equals(newPos))
                {
                    MainForm.Blocks.RemoveAt(i);
                    return;
                }
            }

            //no matching block found, so create one
            MainForm.Blocks.Add(new Block(newPos));
        }
    }
}
