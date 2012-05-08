using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Mindstep.EasterEgg.Commons.DTO;

namespace Mindstep.EasterEgg.Engine.Graphics
{
    public class Animation
    {
        private AnimationDTO animationData;

        public readonly string Name;

        private Frame[] frames;
        public Frame[] Frames { get { return frames; } }





        public Animation(AnimationDTO animationData, GraphicsDevice graphicsDevice)
            : this(animationData)
        {
            Initialize(graphicsDevice);
        }

        public Animation(AnimationDTO animationData)
        {
            this.Name = animationData.Name;
            this.animationData = animationData;
        }

        public Animation(string name, Frame[] frames)
        {
            this.Name = name;
            this.frames = frames;
        }

        internal void Initialize(GraphicsDevice graphicsDevice)
        {
            this.frames = animationData.Frames.Select(frameDTO => new Frame(frameDTO, graphicsDevice)).ToArray();
            animationData = null;
        }
    }
}