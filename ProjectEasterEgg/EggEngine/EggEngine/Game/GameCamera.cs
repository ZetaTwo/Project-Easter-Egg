using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Mindstep.EasterEgg.Game;
using Mindstep.EasterEgg.Commons;
using Mindstep.EasterEgg.Commons.Game;
using Mindstep.EasterEgg.Commons.Graphic;

namespace Mindstep.EasterEgg.Engine.Game
{
    public class GameCamera : Camera
    {
        private GameModel following;
        public GameModel Following
        {
            get { return following; }
            set
            {
                following = value;
                cameraMode = Mode.FOLLOW;
            }
        }

        enum Mode { FOLLOW, LAZY_FOLLOW, FIXED };
        Mode cameraMode = Mode.FOLLOW;
        private EggEngine Engine;
        private Mode CameraMode
        {
            get { return cameraMode; }
            set { cameraMode = value; }
        }

        public Matrix ZoomAndOffsetMatrix { get { return zoomAndOffsetMatrix; } }


        public GameCamera(GameModel following)
        {
            this.Following = following;
        }

        public GameCamera(Point point)
            : base(point)
        { }

        public GameCamera()
        { }

        public void Initialize(EggEngine engine)
        {
            this.Engine = engine;
        }

        public void PrepareForDraw()
        {
            if (CameraMode == Mode.FOLLOW)
            {
                Point offset = CoordinateTransform.ObjectToProjectionSpace(
                    Following.position.ToVector3()).ToXnaPoint().Multiply(-1)
                    .Add(Engine.Window.ClientBounds.Center)
                    .Subtract(Engine.Window.ClientBounds.Location);
                if (Offset != offset)
                {
                    Offset = offset;
                }
            }
        }
    }
}
