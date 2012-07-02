using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mindstep.EasterEgg.Commons;
using Mindstep.EasterEgg.Commons.DTO;
using Microsoft.Xna.Framework;
using Mindstep.EasterEgg.Commons.Game;
using Mindstep.EasterEgg.Engine.Input;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Mindstep.EasterEgg.Engine.Graphics;
using System.Linq.Expressions;

namespace Mindstep.EasterEgg.Engine.Game
{
    public class GameMovableModel : GameModel, IEntityUpdate
    {
        private EggEngine Engine;
        private bool moving;
        private long begunMovingAt;
        private Position moveOffset;
        private float moveSpeed = 500;

        protected LinkedList<Position> PathCheckpoints;
        public Action<GameTime> DoneMoving;


        

        
        public GameMovableModel(GameModelDTO modelDTO, EggEngine engine)
            : base(modelDTO, engine)
        {
            this.Engine = engine;
        }

        public new void Initialize(EggEngine _engine)
        {
            throw new NotImplementedException();
        }





        public void Update(GameTime gameTime)
        {
            if (Engine.Input.Keyboard.IsKeyDown(Keys.D8)) MoveOffsetIfNotAlreadyMoving(Position.N, gameTime);
            else if (Engine.Input.Keyboard.IsKeyDown(Keys.D9)) MoveOffsetIfNotAlreadyMoving(Position.NE, gameTime);
            else if (Engine.Input.Keyboard.IsKeyDown(Keys.O)) MoveOffsetIfNotAlreadyMoving(Position.E, gameTime);
            else if (Engine.Input.Keyboard.IsKeyDown(Keys.L)) MoveOffsetIfNotAlreadyMoving(Position.SE, gameTime);
            else if (Engine.Input.Keyboard.IsKeyDown(Keys.K)) MoveOffsetIfNotAlreadyMoving(Position.S, gameTime);
            else if (Engine.Input.Keyboard.IsKeyDown(Keys.J)) MoveOffsetIfNotAlreadyMoving(Position.SW, gameTime);
            else if (Engine.Input.Keyboard.IsKeyDown(Keys.U)) MoveOffsetIfNotAlreadyMoving(Position.W, gameTime);
            else if (Engine.Input.Keyboard.IsKeyDown(Keys.D7)) MoveOffsetIfNotAlreadyMoving(Position.NW, gameTime);
            else if (Engine.Input.Keyboard.IsKeyDown(Keys.F)) MoveOffsetIfNotAlreadyMoving(Position.W * 3, gameTime);
            
            if (moving && getT(gameTime) == 1)
            {
                this.ParentMap().WorldMatrix.endMoveModel(this, Position + moveOffset);
                moving = false;
                DoneMoving(gameTime);//TODO: don't send current gameTime, but the exact time the model stopped moving. to reduce probable stuttering
            }
        }

        protected override void Draw(GameTime gameTime, SpriteBatch spriteBatch, BoundingBoxInt worldBounds, Vector2 offset, float depthOffset)
        {
            if (moving)
            {
                depthOffset -= 0.04f;
                // I don't know why -0.04, but it works :D // Björn
            }
            base.Draw(gameTime, spriteBatch, worldBounds, offset + getCurrentDrawOffset(gameTime), depthOffset);
        }

        public override Position RenderPosition(GameTime gameTime)
        {
            return this.AbsolutePosition() + getCurrentMoveOffset(gameTime).Ceiling();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="destination">An absolute Position</param>
        /// <param name="startTime"></param>
        /// <returns>True if the character is now/can soon start walking towards the destination</returns>
        public bool WalkTo(Position destination, GameTime startTime)
        {
            Position start = Position;
            if (moving)
            {
                start += moveOffset;
            }
            PathCheckpoints = Engine.Physics.FindPath(this, start, destination);

            if (PathCheckpoints == null)
            {
                return false;
            }
            else
            {
                DoneMoving = (gameTime) =>
                {
                    if (PathCheckpoints.Count == 0)
                    {
                        DoneMoving = null;
                    }
                    else
                    {
                        MoveOffsetIfNotAlreadyMoving(PathCheckpoints.First.Value, gameTime);
                        PathCheckpoints.RemoveFirst();
                    }
                };
                if (!moving)
                {
                    DoneMoving(startTime);
                }
                return true;
            }
        }

        public bool MoveOffsetIfNotAlreadyMoving(Position moveOffset, GameTime startTime)
        {
            if (!moving &&
                this.ParentMap().WorldMatrix.beginMoveModel(this, Position + moveOffset))
            {
                moving = true;
                begunMovingAt = (long)startTime.TotalGameTime.TotalMilliseconds;
                this.moveOffset = moveOffset;
                return true;
            }
            return false;
        }

        private float getT(GameTime gameTime)
        {
            if (moving)
            {
                return getTnoUpperLimit(gameTime).upperLimit(1);
            }
            else
            {
                return 0;
            }
        }

        private float getTnoUpperLimit(GameTime gameTime)
        {
            return (float)(gameTime.TotalGameTime.TotalMilliseconds - begunMovingAt) / moveOffset.Length() / moveSpeed;
        }

        public Vector3 getCurrentMoveOffset(GameTime gameTime)
        {
            if (moving)
            {
                return moveOffset.ToVector3() * getT(gameTime);
            }
            else
            {
                return Vector3.Zero;
            }
        }

        public Vector2 getCurrentDrawOffset(GameTime gameTime)
        {
            if (moving)
            {
                if (Math.Abs(moveOffset.X) == Math.Abs(moveOffset.Y))
                {
                    return (CoordinateTransform.ObjectToProjectionSpace(moveOffset) * getT(gameTime)).Round();
                }
                else
                {
                    return CoordinateTransform.ObjectToProjectionSpace(getCurrentMoveOffset(gameTime));
                }
            }
            else
            {
                return Vector2.Zero;
            }
        }
    }
}
