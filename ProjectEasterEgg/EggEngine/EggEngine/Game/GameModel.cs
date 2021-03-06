﻿using System;
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

        public readonly Dictionary<string, Animation> Animations = new Dictionary<string, Animation>();
        protected readonly Dictionary<string, Position> spawnLocations;

        protected GameModel parent;
        private EggEngine Engine;
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
            this.Engine = engine;
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





        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, BoundingBoxInt worldBounds)
        {
            Draw(gameTime, spriteBatch, worldBounds, Vector2.Zero, 0);
        }

        //protected virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch, BoundingBoxInt worldBounds, float depthOffset)
        //{
        //    Frame currentFrame = Animations["still"].Frames[0];
        //    Vector3 fractionalRenderPosition = RenderPosition(gameTime);
        //    Position wholeRenderPosition = fractionalRenderPosition.Ceiling();
        //    for (int i = 0; i < blocks.Length; i++)
        //    {
        //        if (currentFrame.textures[i] != null)
        //        {
        //            float depth = worldBounds.getRelativeDepthOf(wholeRenderPosition + blocks[i].Position, depthOffset);
        //            //Vector2 screenCoords = (CoordinateTransform.ObjectToProjectionSpace(realPosition) * 1.3f).ToPoint().ToVector2();
        //            Vector2 screenCoords = CoordinateTransform.ObjectToProjectionSpace(fractionalRenderPosition + blocks[i].Position.ToVector3());
        //            spriteBatch.Draw(currentFrame.textures[i], screenCoords, null,
        //                Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, depth);
        //        }
        //    }

        //    foreach (GameModel subModel in subModels)
        //    {
        //        subModel.Draw(gameTime, spriteBatch, worldBounds, depthOffset);
        //    }
        //}

        protected virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch, BoundingBoxInt worldBounds, Vector2 offset, float depthOffset)
        {
            Frame currentFrame = Animations["still"].Frames[0];
            for (int i = 0; i < blocks.Length; i++)
            {
                if (currentFrame.textures[i] != null)
                {
                    float depth = worldBounds.getRelativeDepthOf(RenderPosition(gameTime) + blocks[i].Position, depthOffset);
                    Vector2 screenCoords = CoordinateTransform.ObjectToBlockDrawCoordsInProjectionSpace(Position + blocks[i].Position) + offset;
                    Color color = Color.White;
                    var a = Engine.Physics.GetBlocksUnderPoint(Engine.Input.Mouse.LocationInProjSpace, gameTime).FirstOrDefault();
                    if (a != null && a.Item1 == blocks[i])
                    {
                        color = Color.Red;
                    }
                    spriteBatch.Draw(currentFrame.textures[i], screenCoords, null,
                        color, 0, Vector2.Zero, 1, SpriteEffects.None, depth);
                }
            }

            foreach (GameModel subModel in subModels)
            {
                subModel.Draw(gameTime, spriteBatch, worldBounds, offset, depthOffset);
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

        public virtual Position RenderPosition(GameTime gameTime)
        {
            return this.AbsolutePosition();
        }
    }
}
