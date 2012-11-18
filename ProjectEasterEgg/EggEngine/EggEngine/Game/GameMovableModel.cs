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
using Mindstep.EasterEgg.Engine;

namespace Mindstep.EasterEgg.Engine.Game
{
    public class GameMovableModel : GameModel, IEntityUpdate
    {
        private EggEngine Engine;
        private bool moving;
        private float moveSpeed = 380;

        private long begunMovingAt;
        private Position moveOffset;
        private long totalTimeMsToFinishCurrentMove { get { return (long)(moveOffset.Length() * moveSpeed); } }

        protected LinkedList<Position> PathCheckpoints;
        //public Action<GameTime> DoneMoving;


        

        
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
            long startTime = gameTime.TotalMsLong();
            if (Engine.Input.Keyboard.IsKeyDown(Keys.D8)) MoveOffsetIfNotAlreadyMoving(Position.N, startTime);
            else if (Engine.Input.Keyboard.IsKeyDown(Keys.D9)) MoveOffsetIfNotAlreadyMoving(Position.NE, startTime);
            else if (Engine.Input.Keyboard.IsKeyDown(Keys.O)) MoveOffsetIfNotAlreadyMoving(Position.E, startTime);
            else if (Engine.Input.Keyboard.IsKeyDown(Keys.L)) MoveOffsetIfNotAlreadyMoving(Position.SE, startTime);
            else if (Engine.Input.Keyboard.IsKeyDown(Keys.K)) MoveOffsetIfNotAlreadyMoving(Position.S, startTime);
            else if (Engine.Input.Keyboard.IsKeyDown(Keys.J)) MoveOffsetIfNotAlreadyMoving(Position.SW, startTime);
            else if (Engine.Input.Keyboard.IsKeyDown(Keys.U)) MoveOffsetIfNotAlreadyMoving(Position.W, startTime);
            else if (Engine.Input.Keyboard.IsKeyDown(Keys.D7)) MoveOffsetIfNotAlreadyMoving(Position.NW, startTime);
            else if (Engine.Input.Keyboard.IsKeyDown(Keys.F)) MoveOffsetIfNotAlreadyMoving(Position.W * 3, startTime);

            checkIfDoneMoving(gameTime);
        }

        protected override void Draw(GameTime gameTime, SpriteBatch spriteBatch, BoundingBoxInt worldBounds, Vector2 offset, float depthOffset)
        {
            if (moving)
            {
                depthOffset -= 0.04f; // I don't know why -0.04, but it works :D // Björn
            }
            base.Draw(gameTime, spriteBatch, worldBounds, offset + getCurrentDrawOffset(gameTime), depthOffset);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="destination">An absolute Position</param>
        /// <param name="gameTime"></param>
        /// <returns>True if the character (is now|will soon) start walking towards the destination</returns>
        public bool WalkTo(Position destination, GameTime gameTime)
        {
            Position start = Position;
            if (moving)
            {
                start += moveOffset;
            }
            PathCheckpoints = Engine.Physics.FindPath(this, start, destination);

            return checkIfDoneMoving(gameTime);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameTime"></param>
        /// <returns>true if completely done moving (no more queued up moves either)</returns>
        private bool checkIfDoneMoving(GameTime gameTime)
        {
            long startTimeForNextMove = gameTime.TotalMsLong();
            if (moving && gameTime.TotalMsLong() - begunMovingAt >= totalTimeMsToFinishCurrentMove)
            {
                this.ParentMap().WorldMatrix.endMoveModel(this, Position + moveOffset);
                moving = false;
                startTimeForNextMove = begunMovingAt + totalTimeMsToFinishCurrentMove;
            }

            if (!moving && PathCheckpoints != null && PathCheckpoints.Count > 0)
            {
                MoveOffsetIfNotAlreadyMoving(PathCheckpoints.First.Value, startTimeForNextMove);
                PathCheckpoints.RemoveFirst();
                return false;
            }
            else
            {
                return true;
            }
        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="moveOffset"></param>
        /// <param name="startTime"></param>
        /// <returns>True if the moving was initiated and false if already moving OR
        /// the destination position was already occupied</returns>
        public bool MoveOffsetIfNotAlreadyMoving(Position moveOffset, long startTime)
        {
            if (!moving &&
                this.ParentMap().WorldMatrix.beginMoveModel(this, Position + moveOffset))
            {
                moving = true;
                begunMovingAt = startTime;
                this.moveOffset = moveOffset;
                return true;
            }
            return false;
        }



        private float getT(GameTime gameTime)
        {
            if (moving)
            {
                return getTNoUpperLimit(gameTime).upperLimit(1);
            }
            else
            {
                return 0;
            }
        }

        private float getTNoUpperLimit(GameTime gameTime)
        {
            return (float)(gameTime.TotalGameTime.TotalMilliseconds - begunMovingAt) / totalTimeMsToFinishCurrentMove;
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

        public override Position RenderPosition(GameTime gameTime)
        {
            return this.AbsolutePosition() + getCurrentMoveOffset(gameTime).Ceiling();
        }
    }
}
