using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Mindstep.EasterEgg.Commons.DTO;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using Mindstep.EasterEgg.Commons;

namespace Mindstep.EasterEgg.Engine.Graphics
{
    public class Frame
    {
        public readonly int Duration;
        public readonly Texture2D[] textures;

        public Frame(FrameDTO frameData, GraphicsDevice graphicsDevice)
            : this(frameData.duration)
        {
            textures = new Texture2D[frameData.textures.Count];
            foreach (KeyValuePair<int, SaveBlockImage> pair in frameData.textures)
            {
                textures[pair.Key] = BitmapDataToTexture2D(pair.Value.BitmapBytes, graphicsDevice);
            }
        }

        public Frame(int duration)
        {
            this.Duration = duration;
        }

        private static Texture2D BitmapDataToTexture2D(byte[] data, GraphicsDevice graphicsDevice) {
            if (data == null)
            {
                return null;
            }

            Texture2D tex = new Texture2D(graphicsDevice, Constants.PROJ_WIDTH, Constants.PROJ_HEIGHT);
            tex.SetData(data);
            return tex;
        }
    }
}
