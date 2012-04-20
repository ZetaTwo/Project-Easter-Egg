using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework.Graphics;

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
            return new SaveSubModel<Texture2DWithPos>(model.ToTexture2D(graphicsDevice), model.offset);
        }

        public static SaveModel<Texture2DWithPos> ToTexture2D(this SaveModel<BitmapWithPos> model, GraphicsDevice graphicsDevice)
        {
            SaveModel<Texture2DWithPos> newModel = new SaveModel<Texture2DWithPos>(model.name);
            newModel.blocks.AddRange(model.blocks);
            newModel.subModels.AddRange(model.subModels.Select(subModel => subModel.ToTexture2D(graphicsDevice)));
            newModel.animations.AddRange(model.animations.Select(animation => animation.ToTexture2D(graphicsDevice)));
            return newModel;
        }

        public static SaveAnimation<Texture2DWithPos> ToTexture2D(this SaveAnimation<BitmapWithPos> animation, GraphicsDevice graphicsDevice)
        {
            SaveAnimation<Texture2DWithPos> newAnimation = new SaveAnimation<Texture2DWithPos>(animation.Name);
            newAnimation.Facing = animation.Facing;
            newAnimation.Frames.AddRange(animation.Frames.Select(frame => frame.ToTexture2D(graphicsDevice)));
            return newAnimation;
        }

        public static SaveFrame<Texture2DWithPos> ToTexture2D(this SaveFrame<BitmapWithPos> frame, GraphicsDevice graphicsDevice)
        {
            SaveFrame<Texture2DWithPos> newFrame = new SaveFrame<Texture2DWithPos>(frame.Duration);
            newFrame.Images.AddToFront(frame.Images.BackToFront().Select(bitmap => bitmap.ToTexture2D(graphicsDevice)));
            return newFrame;
        }

        public static Texture2DWithPos ToTexture2D(this BitmapWithPos bitmap, GraphicsDevice graphicsDevice)
        {
            Texture2DWithPos texture = new Texture2DWithPos();
            texture.pos = bitmap.pos;
            texture.projectedOnto.AddRange(bitmap.projectedOnto);
            MemoryStream stream = new MemoryStream();
            bitmap.SaveTo(stream);
            texture.Texture = Texture2D.FromStream(graphicsDevice, stream);
            return texture;
        }
    }
}
