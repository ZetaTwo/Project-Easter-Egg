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
    public class GameModel
    {
        public readonly List<GameModel> subModels = new List<GameModel>();

        public GameBlock[] blocks;
        protected BoundingBoxInt relativeBounds;

        public Position position = Position.Zero;
        public readonly Dictionary<string, Animation> Animations = new Dictionary<string,Animation>();
        protected readonly Dictionary<string, Position> spawnLocations;


        //public GameModel(GameModelDTO modelData, Position offset)
        //    : this(modelData, new BoundingBoxInt(modelData.max + offset, modelData.min + offset))
        //{
        //    foreach (GameBlock block in blocks)
        //    {
        //        block.Position += offset;
        //    }

        //    foreach (GameModel subModel in subModels)
        //    {
        //        subModel.position += offset;
        //    }
        //}

        public GameModel(GameModelDTO modelData)
        {
            this.blocks = modelData.blocks.Select(block => new GameBlock(block)).ToArray();
            this.subModels.AddRange(modelData.subModels.Select(modelDTO => new GameModel(modelDTO)));
            this.relativeBounds = new BoundingBoxInt(modelData.min, modelData.max);
            foreach (AnimationDTO animationDTO in modelData.animations)
            {
                this.Animations.Add(animationDTO.Name, new Animation(animationDTO));
            }
            this.spawnLocations = modelData.spawnLocations;
        }

        public void Draw(SpriteBatch spriteBatch, BoundingBoxInt worldBounds)
        {
            Frame currentFrame = Animations["still"].Frames[0];
            for (int i = 0; i < blocks.Length; i++)
            {
                if (currentFrame.textures[i] != null)
                {
                    float depth = worldBounds.getRelativeDepthOf(blocks[i].Position);
                    //Vector2 screenCoords = (CoordinateTransform.ObjectToProjectionSpace(Position) * 1.3f).ToPoint().ToVector2();
                    Vector2 screenCoords = CoordinateTransform.ObjectToProjSpace(position + blocks[i].Position);
                    spriteBatch.Draw(currentFrame.textures[i], screenCoords, null,
                        Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, depth);
                }
            }

            foreach (GameModel subModel in subModels)
            {
                subModel.Draw(spriteBatch, worldBounds);
            }
        }

        internal void Initialize(EggEngine Engine)
        {
            foreach (Animation animation in Animations.Values)
            {
                animation.Initialize(Engine.GraphicsDevice);
            }

            foreach (GameBlock block in blocks)
            {
                block.Initialize(Engine);
            }

            foreach (GameModel subModel in subModels)
            {
                subModel.Initialize(Engine);
            }
        }
    }
}
