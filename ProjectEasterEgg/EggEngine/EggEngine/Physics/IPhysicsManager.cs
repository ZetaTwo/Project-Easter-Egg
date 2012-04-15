using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Mindstep.EasterEgg.Engine.Game;

namespace Mindstep.EasterEgg.Engine.Physics
{
    public interface IPhysicsManager
    {
        void ClickWorld(Vector2 screen, BlockAction action);
        Path<GameBlock> FindPath(GameBlock start, GameBlock destination);
    }
}
