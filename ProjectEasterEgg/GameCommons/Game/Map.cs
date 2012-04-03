using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace Mindstep.EasterEgg.Engine.Physics
{
    public class Map : IEntityDrawable
    {
        private List<IEntityDrawable> drawableObjects = new List<IEntityDrawable>();

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (IEntityDrawable drawable in drawableObjects)
            {
                drawable.Draw(spriteBatch);
            }
        }

        public void AddUpdate(IEntityDrawable entity)
        {
            drawableObjects.Add(entity);
        }

        public void RemoveUpdate(IEntityDrawable entity)
        {
            drawableObjects.Remove(entity);
        }
    }
}
