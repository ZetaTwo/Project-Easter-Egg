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
        public readonly Texture2D Texture;

        public Frame(FrameDTO frameData, GraphicsDevice graphicsDevice)
            : this(frameData.Duration, BitmapDataToTexture2D(frameData.bitmapData, graphicsDevice))
        { }

        public Frame(int duration, Texture2D texture)
        {
            this.Duration = duration;
            this.Texture = texture;
        }

        public static implicit operator Texture2D(Frame frame)
        {
            return frame.Texture;
        }

        private static Texture2D BitmapDataToTexture2D(byte[] data, GraphicsDevice graphicsDevice) {
            if (data == null)
            {
                return null;
            }

            Texture2D tex = new Texture2D(graphicsDevice, Constants.PROJ_WIDTH, Constants.PROJ_HEIGHT);
            tex.SetData(data);
            //using (MemoryStream stream = new MemoryStream())
            //{
                //int bufferSize = data.Height * data.Stride;

                ////create data buffer 
                //byte[] bytes = new byte[bufferSize];

                //// copy bitmap data into buffer
                //Marshal.Copy(data.Scan0, bytes, 0, bytes.Length);

                //// copy our buffer to the texture
                //tex.SetData(bytes);

                //data.Save(stream, ImageFormat.Png);
                //stream.Seek(0, SeekOrigin.Begin);
                //tex = Texture2D.FromStream(graphicsDevice, stream);
            //}
            return tex;
        }
    }
}
