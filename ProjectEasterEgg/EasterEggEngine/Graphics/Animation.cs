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
        public readonly Frame[] Frames;
        public readonly string Name;

        public Animation(AnimationDTO animationData, GraphicsDevice graphicsDevice)
            : this(animationData.Name, toRealFrames(animationData.Frames, graphicsDevice))
        { }

        public Animation(string name, Frame[] frames)
        {
            this.Name = name;
            this.Frames = frames;
        }

        private static Frame[] toRealFrames(IEnumerable<FrameDTO> framesData, GraphicsDevice graphicsDevice)
        {
            Frame[] realFrames = new Frame[framesData.Count()];

            int i=0;
            foreach (FrameDTO frame in framesData) {
                realFrames[i++] = new Frame(frame, graphicsDevice);
            }

            return realFrames;
        }
    }
}
