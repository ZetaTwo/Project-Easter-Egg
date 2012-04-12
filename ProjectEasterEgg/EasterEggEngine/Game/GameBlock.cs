using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mindstep.EasterEgg.Commons;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Mindstep.EasterEgg.Engine.Graphics;
using Mindstep.EasterEgg.Commons.DTO;
using Mindstep.EasterEgg.Commons.Physics;

namespace Mindstep.EasterEgg.Engine.Game
{
    public class GameBlock : Block
    {
        private Dictionary<string, AnimationDTO> animationsData;

        protected EggEngine engine;
        public EggEngine Engine
        {
            get { return engine; }
        }

        public Dictionary<string, Animation> Animations;

        string scriptName = null;
        public bool Interactable
        {
            get { return scriptName != null; }
        }




        public GameBlock(GameBlockDTO blockData)
            : this(blockData.Type, blockData.Position, blockData.scriptName)
        {
            animationsData = blockData.Animations;
        }

        public GameBlock(BlockType blockType, Position position)
            : this(blockType, position, null)
        { }

        public GameBlock(BlockType type, Position position, string scriptName)
            : base(position)
        {
            if (scriptName != null)
            {
                this.scriptName = "ScriptBlock" + scriptName;
            }
            this.type = type;
        }




        public void Initialize(EggEngine engine)
        {
            this.engine = engine;

            Animations = new Dictionary<string, Animation>();
            foreach (AnimationDTO animationData in animationsData.Values)
            {
                Animations[animationData.Name] = new Animation(animationData, engine.GraphicsDevice);
            }
        }

        private BlockType type;
        public BlockType Type
        {
            get { return type; }
        }

        public void Interact(BlockAction action)
        {
            ScriptBlock script = Engine.Script.Library.GetBlockScript(scriptName, this, action);
            Engine.Script.AddScript(script);
        }

        public void Draw(SpriteBatch spriteBatch, BoundingBoxInt bounds)
        {
            float depth = bounds.getRelativeDepthOf(Position);
            Vector2 screenCoords = CoordinateTransform.ObjectToProjectionSpace(Position);
            spriteBatch.Draw(Animations["still"].Frames[0], screenCoords, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, depth);

            /*spriteBatch.Draw(texture, CoordinateTransform.ObjectToProjectionSpace(bounds.Min + Position),
                null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, bounds.getRelativeDepthOf(Position));*/
        }
       
    }
}
