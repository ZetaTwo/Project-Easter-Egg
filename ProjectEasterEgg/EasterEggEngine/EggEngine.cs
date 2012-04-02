using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Mindstep.EasterEgg.Engine
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class EggEngine : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public ISoundManager Sound
        {
            get
            {
                return Services.GetService(typeof(ISoundManager)) as ISoundManager;
            }
        }

        public IGraphicsManager Graphics
        {
            get
            {
                return Services.GetService(typeof(IGraphicsManager)) as IGraphicsManager;
            }
        }

        public IPhysicsManager Physics
        {
            get
            {
                return Services.GetService(typeof(IPhysicsManager)) as IPhysicsManager;
            }
        }

        public IInputManager Input
        {
            get
            {
                return Services.GetService(typeof(IInputManager)) as IInputManager;
            }
        }

        public IStorageManager Storage
        {
            get
            {
                return Services.GetService(typeof(IStorageManager)) as IStorageManager;
            }
        }

        public IScriptEngine Script
        {
            get
            {
                return Services.GetService(typeof(IScriptEngine)) as IScriptEngine;
            }
        }

        public EggEngine(Script startScript)
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            ScriptEngine scriptEngine = new ScriptEngine();
            scriptEngine.AddScript(startScript);
            Services.AddService(typeof(IScriptEngine), scriptEngine);

            Services.AddService(typeof(IPhysicsManager), new PhysicsManager());
            Services.AddService(typeof(ISoundManager), new SoundManager());
            Services.AddService(typeof(IInputManager), new InputManager());
            Services.AddService(typeof(IGraphicsManager), new GraphicsManager());
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            Input.Update();
            Script.Update();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            Graphics.Draw();

            base.Draw(gameTime);
        }
    }
}
