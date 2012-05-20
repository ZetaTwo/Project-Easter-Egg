using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Mindstep.EasterEgg.Engine.Physics;
using Mindstep.EasterEgg.Engine.Input;

namespace Mindstep.EasterEgg.Engine
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class EggEngine : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        World world;

        public ISoundManager Sound
        {
            get
            {
                return Services.GetService(typeof(ISoundManager)) as ISoundManager;
            }
        }

        public PhysicsManager Physics
        {
            get
            {
                return Services.GetService(typeof(PhysicsManager)) as PhysicsManager;
            }
        }

        public InputManager Input
        {
            get
            {
                return Services.GetService(typeof(InputManager)) as InputManager;
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

        public World World
        {
            get
            {
                return world;
            }
        }

        public EggEngine(World _world)
        {
            world = _world;
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            Services.AddService(typeof(IScriptEngine), new ScriptEngine(this));
            Services.AddService(typeof(PhysicsManager), new PhysicsManager(this));
            Services.AddService(typeof(ISoundManager), new SoundManager(this));
            Services.AddService(typeof(InputManager), new InputManager());
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

            world.Initialize(this);
            Input.Initialize(this);
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
            Input.Update(gameTime);
            world.Update(gameTime);
            Script.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Red);

            // TODO: Add your drawing code here
            world.Draw(gameTime, spriteBatch);

            base.Draw(gameTime);
        }
    }
}
