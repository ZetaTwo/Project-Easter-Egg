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

namespace Mindstep.EasterEgg.Engine.Game
{
    public class GameCharacter : GameModel, IEntityUpdate
    {
        private EggEngine Engine;
        private bool moving;
        private long begunMovingAt;
        private Position movingTo;

        public GameCharacter(GameModelDTO modelDTO, EggEngine engine)
            : base(modelDTO, engine)
        {
            this.Engine = engine;
        }

        public bool MoveOffset(Position offset, GameTime startTime)
        {
            if (!moving &&
                this.ParentMap().WorldMatrix.beginMoveModel(this, Position + offset))
            {
                moving = true;
                begunMovingAt = (long)startTime.TotalGameTime.TotalMilliseconds;
                movingTo = Position + offset;
                return true;
            }
            return false;
        }

        public void Update(GameTime gameTime)
        {
            if (Engine.Input.Keyboard.KeyPressed(Keys.Up))
            {
                MoveOffset(Position.NW, gameTime);
            }
            if (Engine.Input.Keyboard.KeyPressed(Keys.Right))
            {
                MoveOffset(Position.NE, gameTime);
            }
            if (Engine.Input.Keyboard.KeyPressed(Keys.Down))
            {
                MoveOffset(Position.SE, gameTime);
            }
            if (Engine.Input.Keyboard.KeyPressed(Keys.Left))
            {
                MoveOffset(Position.SW, gameTime);
            }
            
            if (moving &&
                gameTime.TotalGameTime.TotalMilliseconds > begunMovingAt + 300)
            {
                this.ParentMap().WorldMatrix.endMoveModel(this, movingTo);
                moving = false;
            }
        }


        public new void Initialize(EggEngine _engine)
        {
            throw new NotImplementedException();
        }
    }
}
