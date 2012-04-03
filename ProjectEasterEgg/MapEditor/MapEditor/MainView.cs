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


        /// <summary>
        /// Initializes the control.
        /// </summary>
        protected override void Initialize()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteEffect = SpriteEffects.None;

            ContentManager Content = new ContentManager(Services, "MapEditorContent");
            block = Content.Load<Texture2D>("mainBlock");
            grid = Content.Load<Texture2D>("mainGrid");
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

            for (int x = -5; x < 10; x += 1)
            {
                for (int y = -5; y < 10; y += 1)
                {
                    drawBlock(grid, new Position(x, y, -1));
                }
            }

            foreach (Block b in MainForm.Blocks)
            {
                drawBlock(block, b.Offset);
            }
            spriteBatch.End();
        }

        private void drawBlock(Texture2D image, Position pos)
        {
            BoundingBoxInt boundingBox = new BoundingBoxInt(MainForm.Blocks.ToPositions());
            float depth = boundingBox.getDepth(pos);
            Vector2 screenCoords = Transform.ToScreen(pos, tileHeight, tileWidth, blockHeight, center).toVector2();
            spriteBatch.Draw(image, screenCoords, null, Color.Red, 0, Vector2.Zero, 1, spriteEffect, depth);
        }
    }
}
