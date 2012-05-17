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
    public class GameModel : Child, IPositionable
    {
        public readonly List<GameModel> subModels = new List<GameModel>();

        public GameBlock[] blocks;
        protected BoundingBoxInt relativeBounds;

        /// <summary>
        /// Do not fiddle with this variable, it's only to be touched at creation and by the WorldMatrix
        /// </summary>
        internal Position position = Position.Zero;
        public Position Position { get { return position; } }
        public readonly Dictionary<string, Animation> Animations = new Dictionary<string,Animation>();
        protected readonly Dictionary<string, Position> spawnLocations;

        protected GameModel parent;
        public GameModel Parent
        {
            get { return parent; }
            set { parent = value; }
        }





        public GameModel(GameModelDTO modelData, GameModel parent = null)
        {
            this.blocks = modelData.blocks.Select(block => new GameBlock(this, block)).ToArray();
            this.subModels.AddRange(modelData.subModels.Select(modelDTO => new GameModel(modelDTO, null)));
            this.relativeBounds = new BoundingBoxInt(modelData.min, modelData.max);
            foreach (AnimationDTO animationDTO in modelData.animations)
            {
                this.Animations.Add(animationDTO.Name, new Animation(animationDTO));
            }
            this.spawnLocations = modelData.spawnLocations;
        }

        public GameModel(GameModelDTO modelData, EggEngine engine, GameModel parent = null)
            : this(modelData, parent)
        {
            Initialize(engine);
        }

        internal void Initialize(EggEngine engine)
        {
            foreach (Animation animation in Animations.Values)
            {
                animation.Initialize(engine.GraphicsDevice);
            }

            foreach (GameBlock block in blocks)
            {
                block.Initialize(engine);
            }

            foreach (GameModel subModel in subModels)
            {
                subModel.Initialize(engine);
            }
        }





        public void Draw(SpriteBatch spriteBatch, BoundingBoxInt worldBounds)
        {
            Frame currentFrame = Animations["still"].Frames[0];
            for (int i = 0; i < blocks.Length; i++)
            {
                Position absolutePosition = this.AbsolutePosition();
                if (currentFrame.textures[i] != null)
                {
                    float depth = worldBounds.getRelativeDepthOf(blocks[i].Position + absolutePosition);
                    //Vector2 screenCoords = (CoordinateTransform.ObjectToProjectionSpace(Position) * 1.3f).ToPoint().ToVector2();
                    Vector2 screenCoords = CoordinateTransform.ObjectToProjectionSpace(absolutePosition + blocks[i].Position);
                    spriteBatch.Draw(currentFrame.textures[i], screenCoords, null,
                        Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, depth);
                }
            }

            foreach (GameModel subModel in subModels)
            {
                subModel.Draw(spriteBatch, worldBounds);
            }
        }

        /// <summary>
        /// Get the positions of all blocks in this model and all its submodels,
        /// relative this models position, that is, not offseted.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Position> getAllRelativeBlockPositions()
        {
            foreach (Position blockOffset in blocks.ToPositions())
            {
                yield return blockOffset;
            }

            foreach (GameModel subModel in subModels)
            {
                foreach (Position blockOffset in subModel.getAllRelativeBlockPositions())
                {
                    yield return subModel.Position + blockOffset;
                }
            }
        }
    }
}
