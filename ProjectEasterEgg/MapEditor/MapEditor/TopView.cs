#region Using Statements
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
#endregion

namespace MapEditor
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
        public bool draggingBlock;
        private Texture2D topGrid;
        private Point viewPos;
        private int gridSize;
        private int blockSize;
        private float scale;


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
            viewPos = new Point(-1, -1);
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

            for (int x = viewPos.X; x < Width; x+=topGrid.Width)
            {
                for (int y = viewPos.Y; y < Height; y += topGrid.Height)
                {
                    spriteBatch.Draw(topGrid, new Vector2(x, y), null, Color.White, 0, Vector2.Zero, 1, spriteEffect, 1f);
                }
            }

            if (draggingBlock)
            {
                System.Drawing.Point p = PointToClient(MousePosition);
                Vector2 closestPoint = new Vector2(p.X + viewPos.X - p.X % gridSize, p.Y + viewPos.Y - p.Y % gridSize);
                //Vector2 pos = new Vector2(p.X - top.Width / 2, p.Y - top.Height / 2);
                spriteBatch.Draw(top, closestPoint, null, Color.White, 0, Vector2.Zero, 1, spriteEffect, 0.5f);
            }
            spriteBatch.End();
        }

        internal void placeBlock()
        {
            
        }
    }
}
