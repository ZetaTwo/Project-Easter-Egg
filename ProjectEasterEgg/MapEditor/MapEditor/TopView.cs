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
    /// event, using this to MainForm.Updated() the control, which will cause the animation
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
        private MainForm MainForm;

        /// <summary>
        /// Initializes the control.
        /// </summary>
        public void Initialize(MainForm mainForm)
        {
            this.MainForm = mainForm;
            spriteBatch = new SpriteBatch(MainForm.GraphicsDevice);
            spriteEffect = SpriteEffects.None;

            block = MainForm.Content.Load<Texture2D>("topBlock");
            grid = MainForm.Content.Load<Texture2D>("topGrid");
            offsetToBlock00 = new Point(Width/2, Height/2);
            gridSize = grid.Width;

            // Hook the idle event to constantly redraw our animation.
            MouseClick += new MouseEventHandler(TopView_MouseClick);
            MouseMove += new MouseEventHandler(TopView_MouseMove);
            MouseLeave += new EventHandler(TopView_MouseLeave);
        }

        private void TopView_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                toggleBlock(e.Location.toXnaPoint());
            }
            else if (e.Button == MouseButtons.Right)
            {
                SaveBlock block = getBlockAt(PointToPosition(e.Location.toXnaPoint()));
                if (block != null)
                {
                    new BlockDetailsForm(block, PointToScreen(e.Location));
                }
            }
            MainForm.Updated();
        }

        private void TopView_MouseMove(object sender, MouseEventArgs e)
        {
            Point p = getClosestBlockCoord(e.Location.toXnaPoint());
            MainForm.setTopViewCoordLabel("X:" + p.X + "   Y:" + p.Y);
            MainForm.Updated();
        }

        private void TopView_MouseLeave(object sender, EventArgs e)
        {
            MainForm.setTopViewCoordLabel("");
        }

        /// <summary>
        /// Draws the control.
        /// </summary>
        protected override void Draw()
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            int offsetX = offsetToBlock00.X/gridSize;
            int offsetY = offsetToBlock00.Y/gridSize;
            for (int x = 0; x <= Width/gridSize+1; x++)
            {
                for (int y = 0; y <= Height/gridSize+1; y++)
                {
                    drawBox(grid, new Position(x - offsetX-1, y - offsetY-1, 0), Color.White, 1);
                }
            }

            foreach (SaveBlock saveBlock in MainForm.SaveBlocks)
            {
                Color color = Color.Red;
                int height = saveBlock.Position.Z-MainForm.CurrentHeight;
                if (height == 0)
                {
                    switch (saveBlock.type)
                    {
                        case BlockType.SOLID:
                            color = Color.Green;
                            break;
                        case BlockType.STAIRS_DOWN:
                            color = Color.Blue;
                            break;
                        case BlockType.STAIRS_UP:
                            color = Color.Brown;
                            break;
                        case BlockType.WALKABLE:
                            color = Color.Olive;
                            break;
                    }
                    drawBox(block, saveBlock.Position, color, .5f);
                }
                else
                {
                    drawBox(block, saveBlock.Position, color, .5f);
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
            MainForm.changedSinceLastSave = true;
            MainForm.RefreshTitle();

            Position newPos = PointToPosition(p);

            SaveBlock saveBlock = getBlockAt(newPos);
            if (saveBlock == null)
            {
                //no matching block found, so create one
                saveBlock = new SaveBlock(newPos);
                MainForm.SaveBlocks.Add(saveBlock);
            }
            else
            {
                MainForm.SaveBlocks.Remove(saveBlock);
            }
            MainForm.Updated();
        }

        private Position PointToPosition(Point p)
        {
            Point positionCoords = getClosestBlockCoord(p);
            return new Position(positionCoords.X, positionCoords.Y, MainForm.CurrentHeight);
        }

        internal SaveBlock getBlockAt(Position pos)
        {
            //X and Y screen coordinates are swapped relative
            //the way we want to represent this top view
            for (int i=0; i<MainForm.SaveBlocks.Count; i++)
            {
                if (MainForm.SaveBlocks[i].Position == pos)
                {
                    return MainForm.SaveBlocks[i];
                }
            }
            return null;
        }

        public void MainView_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                MainForm.CurrentHeight++;
            }
            else if (e.Delta < 0)
            {
                MainForm.CurrentHeight--;
            }
            MainForm.Updated();
        }
    }
}
