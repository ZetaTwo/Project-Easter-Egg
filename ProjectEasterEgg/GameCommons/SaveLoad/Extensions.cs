using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using SD = System.Drawing;
using System.Drawing.Imaging;

namespace Mindstep.EasterEgg.Commons.SaveLoad
{
    public static class Extensions
    {
        public static void Write(this Stream stream, string stringToWrite)
        {
            byte[] bytes = UnicodeEncoding.UTF8.GetBytes(stringToWrite);
            stream.Write(bytes, 0, bytes.Length);
        }

        public static SaveSubModel<Texture2DWithPos> ToTexture2D(this SaveSubModel<BitmapWithPos> model, GraphicsDevice graphicsDevice)
        {
            return new SaveSubModel<Texture2DWithPos>(model.ToTexture2D(graphicsDevice), model.Offset);
        }

        public static SaveModel<Texture2DWithPos> ToTexture2D(this SaveModel<BitmapWithPos> modelIn, GraphicsDevice graphicsDevice)
        {
            SaveModel<Texture2DWithPos> modelOut = new SaveModel<Texture2DWithPos>(modelIn.Name);
            modelOut.Blocks.AddRange(modelIn.Blocks);
            foreach (SaveSubModel<BitmapWithPos> subModel in modelIn.SubModels)
            {
                modelOut.SubModels.Add(subModel.ToTexture2D(graphicsDevice));
            }
            foreach (SaveAnimation<BitmapWithPos> animation in modelIn.Animations)
            {
                modelOut.Animations.Add(animation.ToTexture2D(graphicsDevice));
            }
            return modelOut;
        }

        public static SaveAnimation<Texture2DWithPos> ToTexture2D(this SaveAnimation<BitmapWithPos> animation, GraphicsDevice graphicsDevice)
        {
            SaveAnimation<Texture2DWithPos> newAnimation = new SaveAnimation<Texture2DWithPos>(animation.Name);
            newAnimation.Facing = animation.Facing;
            foreach (SaveFrame<BitmapWithPos> frame in animation.Frames)
            {
                newAnimation.Frames.Add(frame.ToTexture2D(graphicsDevice));
            }
            return newAnimation;
        }

        public static SaveFrame<Texture2DWithPos> ToTexture2D(this SaveFrame<BitmapWithPos> frame, GraphicsDevice graphicsDevice)
        {
            SaveFrame<Texture2DWithPos> newFrame = new SaveFrame<Texture2DWithPos>(frame.Duration);
            foreach (BitmapWithPos bitmap in frame.Images.BackToFront())
            {
                newFrame.Images.AddToFront(bitmap.ToTexture2D(graphicsDevice));
            }
            return newFrame;
        }

        public static Texture2DWithPos ToTexture2D(this BitmapWithPos bitmapWithPos, GraphicsDevice graphicsDevice)
        {
            Texture2DWithPos texture = new Texture2DWithPos(bitmapWithPos.bitmap.CloneFix(), graphicsDevice);
            texture.Position = bitmapWithPos.Position;
            texture.name = bitmapWithPos.name;
            texture.projectedOnto.AddRange(bitmapWithPos.projectedOnto);
            return texture;
        }

        public static Texture2D ToTexture2D(this SD.Bitmap bitmap, GraphicsDevice graphicsDevice)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                bitmap.SaveTo(stream);
                return Texture2D.FromStream(graphicsDevice, stream);
            }
        }

        public static void SaveTo(this SD.Bitmap bitmap, Stream stream)
        {
            bitmap.Save(stream, ImageFormat.Png);
        }

        /// <summary>
        /// Clones the whole Bitmap, and it just works, 
        /// whereas a bitmap that Bitmap.Clone() has been called on,
        /// or a bitmap created with Bitmap.Clone(), cannot be used to create
        /// a System.Drawing.Graphics.FromImage().
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public static SD.Bitmap CloneFix(this SD.Bitmap bitmap)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                bitmap.SaveTo(stream);
                return new SD.Bitmap(stream);
            }
        }
    }
}
