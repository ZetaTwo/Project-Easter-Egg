using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mindstep.EasterEgg.Commons;
using Mindstep.EasterEgg.Commons.Physics;

namespace Mindstep.EasterEgg.Engine.Game
{
    public interface Child
    {
        GameModel Parent { get; }
    }

    public static class Extensions
    {
        public static bool hasParent(this Child child, GameModel model)
        {
            if (child.Parent == null)
            {
                return false;
            }
            else if (child.Parent == model)
            {
                return true;
            }
            else
            {
                return child.Parent.hasParent(model);
            }
        }

        public static GameMap ParentMap(this Child child)
        {
            if (child.Parent == null)
            {
                throw new Exception("No parent map found to child: " + child);
            }
            else if (child.Parent is GameMap)
            {
                return (GameMap)child.Parent;
            }
            else
            {
                return child.Parent.ParentMap();
            }
        }

        public static Position AbsolutePosition<PositionableChild>(this PositionableChild asd)
            where PositionableChild : Child, IPositionable
        {
            if (asd.Parent == null)
            {
                return asd.Position;
            }
            else
            {
                return asd.Position + asd.Parent.AbsolutePosition();
            }
        }
    }
}
