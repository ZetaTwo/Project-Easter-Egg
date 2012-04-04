using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mindstep.EasterEgg.Commons;
using Microsoft.Xna.Framework.Graphics;
using EggEnginePipeline;
using Microsoft.Xna.Framework;

namespace Mindstep.EasterEgg.Engine.Game
{
    public class GameBlock : Block
    {
        EggEngine engine;
        public EggEngine Engine
        {
            get { return engine; }
        }

        string textureName;
        Texture2D texture;

        string scriptName = null;
        public bool Interactable
        {
            get { return scriptName != null; ; }
        }

        public GameBlock(BlockType type, Position position, string texture)
            : base(position)
        {
            textureName = texture;
            this.type = type;
        }

        public GameBlock(GameBlockDTO blockData)
            : this(blockData.Type, blockData.Position, blockData.Texture)
        {

        }

        public GameBlock(BlockType blockType, Position position)
            : this(blockType, position, null)
        {
        }

        public void Initialize(EggEngine engine)
        {
            this.engine = engine;

            texture = Engine.Content.Load<Texture2D>(textureName);
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
            spriteBatch.Draw(texture, screenCoords, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, depth);

            /*spriteBatch.Draw(texture, CoordinateTransform.ObjectToProjectionSpace(bounds.Min + Position),
                null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, bounds.getRelativeDepthOf(Position));*/
        }
       
    }
}
