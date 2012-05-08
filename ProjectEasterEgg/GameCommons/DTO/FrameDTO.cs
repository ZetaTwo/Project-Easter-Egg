using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using Mindstep.EasterEgg.Commons.Graphic;
using System.Drawing.Drawing2D;
using Xna = Microsoft.Xna.Framework;
using Mindstep.EasterEgg.Commons.SaveLoad;

namespace Mindstep.EasterEgg.Commons.DTO
{
    public class FrameDTO
    {
        public int duration;
        public readonly Dictionary<int, SaveBlockImage> textures = new Dictionary<int, SaveBlockImage>();

        public FrameDTO()
            : this(0)
        { }

        public FrameDTO(int duration)
        {
            this.duration = duration;
        }

        public void updateDataToBeSaved()
        {
            foreach (SaveBlockImage blockImage in textures.Values)
            {
                blockImage.updateDataToBeSaved();
            }
        }
    }
}
