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

namespace Mindstep.EasterEgg.Engine.Game
{
    public class GameMovableModel : GameModel, IEntityUpdate
    {
        private EggEngine Engine;
        private bool moving;
        private long begunMovingAt;
        private Position moveOffset;
        private float moveSpeed = 500;


        

        
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
            }
        }

        protected override void Draw(GameTime gameTime, SpriteBatch spriteBatch, BoundingBoxInt worldBounds, float depthOffset)
        {
            if (moving)
            {
                depthOffset -= 0.04f;
                // I don't know why -0.04, but it works :D // Björn
            }
            base.Draw(gameTime, spriteBatch, worldBounds, depthOffset);
        }

        public override Vector3 RenderPosition(GameTime gameTime)
        {
            return this.AbsolutePosition().ToVector3() + getCurrentMoveOffset(gameTime);
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
                return ((float)(gameTime.TotalGameTime.TotalMilliseconds - begunMovingAt) / moveOffset.Length() / moveSpeed).upperLimit(1);
            }
            else
            {
                return 0;
            }
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
    }
}
