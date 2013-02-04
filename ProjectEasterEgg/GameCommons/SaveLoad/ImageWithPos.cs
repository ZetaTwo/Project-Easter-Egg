using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.IO;

namespace Mindstep.EasterEgg.Commons.SaveLoad
{
    public abstract class ImageWithPos : Modifiable
    {
        public event EventHandler Modified;

        public string name;
        private Point pos;
        public Point Position
        {
            get { return pos; }
            set
            {
                pos = value;
                if (Modified != null) Modified(this, null);
            }
        }
        public abstract Rectangle Bounds { get; }

        public ImageWithPos()
        {
        }

        public ImageWithPos(Point pos)
            :this()
        {
            this.Position = pos;
        }

        public abstract void SaveTo(Stream stream);

        public override int GetHashCode()
        {
            return name.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }

            // If parameter cannot be cast to Point return false.
            ImageWithPos texture2DWithPos = obj as ImageWithPos;
            if ((object)texture2DWithPos == null)
            {
                return false;
            }

            // Return true if the fields match:
            return name == texture2DWithPos.name;
        }
    }
}
