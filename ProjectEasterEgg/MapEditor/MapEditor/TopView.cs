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
        private Texture2D block;
        private Texture2D grid;
        private Point offsetToBlock00;
        private int gridSize;
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
            block = Content.Load<Texture2D>("topBlock");
            grid = Content.Load<Texture2D>("topGrid");
            offsetToBlock00 = new Point(Width/2, Height/2);
            gridSize = grid.Width;

            // Hook the idle event to constantly redraw our animation.
            Application.Idle += delegate { Invalidate(); };
        }


        /// <summary>
        /// Draws the control.
        /// </summary>
        protected override void Draw()
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();

            int offsetX = offsetToBlock00.X/gridSize;
            int offsetY = offsetToBlock00.Y/gridSize;
            for (int x = 0; x <= Width/gridSize+1; x++)
            {
                for (int y = 0; y <= Height/gridSize+1; y++)
                {
                    drawBox(grid, new Position(x - offsetX-1, y - offsetY-1, 0), Color.White, 1);
                }
            }

            foreach (Block b in MainForm.Blocks)
            {
                int height = b.Offset.Z-MainForm.CurrentHeight;
                if (height == 0)
                {
                    drawBox(block, b.Offset, Color.Green, 0);
                }
                else
                {
                    drawBox(block, b.Offset, Color.Red, .5f);
                }
            }
            spriteBatch.End();
        }

        private void drawBox(Texture2D grid, Position pos, Color color, float depth)
        {
            Vector2 coords = getScreenCoord(pos);
            spriteBatch.Draw(grid, coords, null, color, 0, Vector2.Zero, 1, spriteEffect, depth);
        }

        private Vector2 getScreenCoord(Position pos)
        {
            int x = pos.X * gridSize + offsetToBlock00.X;
            int y = pos.Y * gridSize + offsetToBlock00.Y;
            return new Vector2(x, y);
        }

        public Point getClosestBlockCoord(Point p)
        {
            int x = p.X;
            x -= offsetToBlock00.X % gridSize;
            x -= x % gridSize;
            x /= gridSize;
            x -= offsetToBlock00.X/gridSize;
            
            int y = p.Y;
            y -= offsetToBlock00.Y % gridSize;
            y -= y % gridSize;
            y /= gridSize;
            y -= offsetToBlock00.Y / gridSize;

            return new Point(x, y);
        }

        internal void toggleBlock(Point p)
        {
            //X and Y screen coordinates are swapped relative
            //the way we want to represent this top view
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
