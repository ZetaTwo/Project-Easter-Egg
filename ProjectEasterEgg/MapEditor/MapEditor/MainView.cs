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
        private Point center { get { return new Point(Width / 2, Height / 2); } }
        private float scale;
        public MainForm MainForm;
        private int tileHeight;
        private int tileWidth;
        private int blockHeight;


        protected override void Initialize()
        {
            Load("mainBlock31", "mainGrid31");
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
            tileHeight = block.Bounds.Height - blockHeight;

            // Hook the idle event to constantly redraw our animation.
            Application.Idle += delegate { Invalidate(); };
        }


        /// <summary>
        /// Draws the control.
        /// </summary>
        protected override void Draw()
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            BoundingBoxInt boundingBox = new BoundingBoxInt();

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

            boundingBox.addPos(MainForm.Blocks.ToPositions());

            foreach (Block b in MainForm.Blocks)
            {
                Color color;
                if (b.Position.Z == MainForm.CurrentHeight)
                {
                    color = Color.Green;
                }
                else
                {
                    color = Color.Red;
                }
                drawBlock(block, boundingBox, color, b.Position);
            }
            spriteBatch.End();
        }

        private void drawBlock(Texture2D image, BoundingBoxInt boundingBox, Color color, Position pos)
        {
            float depth = boundingBox.getDepth(pos);
            Vector2 screenCoords = Transform.ToScreen(pos, tileHeight, tileWidth, blockHeight, center).toVector2();
            spriteBatch.Draw(image, screenCoords, null, color, 0, Vector2.Zero, 1, spriteEffect, depth);
        }
    }
}
