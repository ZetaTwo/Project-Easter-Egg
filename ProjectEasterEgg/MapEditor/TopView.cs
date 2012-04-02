#region Using Statements
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
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
        BasicEffect effect;
        Stopwatch timer;


        // Vertex positions and colors used to display a spinning triangle.
        public readonly VertexPositionColor[] Vertices =
        {
            new VertexPositionColor(new Vector3(-1, -1, 0), Color.Black),
            new VertexPositionColor(new Vector3( 1, -1, 0), Color.Black),
            new VertexPositionColor(new Vector3( 0,  1, 0), Color.Black),
        };
        private SpriteBatch spriteBatch;
        private SpriteEffects spriteEffect;
        private Texture2D box6;


        /// <summary>
        /// Initializes the control.
        /// </summary>
        protected override void Initialize()
        {
            // Create our effect.
            effect = new BasicEffect(GraphicsDevice);
            effect.VertexColorEnabled = true;

            spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteEffect = new SpriteEffects();

            // Start the animation timer.
            timer = Stopwatch.StartNew();

            // Hook the idle event to constantly redraw our animation.
            Application.Idle += delegate { Invalidate(); };
        }

        internal void LoadContent(ContentManager Content)
        {
            System.Console.WriteLine("hej");
            box6 = Content.Load<Texture2D>("D:/Dropbox/Projekt/Project-Easter-Egg/ProjectEasterEgg/MapEditor/MapEditorContent/compiled/box6/animations/still/0/0.png");
        }

        /// <summary>
        /// Draws the control.
        /// </summary>
        protected override void Draw()
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // Spin the triangle according to how much time has passed.
            float time = (float)timer.Elapsed.TotalSeconds;

            float yaw = time * 0.7f;
            float pitch = time * 0.8f;
            float roll = time * 0.9f;

            // Set transform matrices.
            float aspect = GraphicsDevice.Viewport.AspectRatio;

            effect.World = Matrix.CreateFromYawPitchRoll(yaw, pitch, roll);

            effect.View = Matrix.CreateLookAt(new Vector3(0, 0, -5),
                                              Vector3.Zero, Vector3.Up);

            effect.Projection = Matrix.CreatePerspectiveFieldOfView(1, aspect, 1, 10);

            // Set renderstates.
            GraphicsDevice.RasterizerState = RasterizerState.CullNone;

            // Draw the triangle.
            effect.CurrentTechnique.Passes[0].Apply();

            GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList,
                                              Vertices, 0, 1);

            System.Console.WriteLine("draw");
            spriteBatch.Begin();
            Vector2 pos = new Vector2(50, 50);
            //spriteBatch.Draw(getBoxTexture(5), pos, null, Color.White, 0, null, 1, spriteEffect, 0.5f);
            spriteBatch.Draw(box6, pos, Color.White);
            spriteBatch.End();
        }
        /*
        private Texture2D createBoxTexture(int size)
        {
            int width = 4 * size + 1;
            int height = width + size / 2;
            Texture2D texture = new Texture2D(GraphicsDevice, width, height);
            int[] data = new int[width * height];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (y < size) // top ^
                    {
                        data[x * width + y] = 12651;
                    }
                }
            }
            texture.SetData<int>(data);
            return texture;
        }*/
    }
}
