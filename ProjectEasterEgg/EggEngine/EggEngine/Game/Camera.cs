using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Mindstep.EasterEgg.Game;
using Mindstep.EasterEgg.Commons;

namespace Mindstep.EasterEgg.Engine.Game
{
    public class Camera : GameEntity
    {
        enum Mode { FOLLOW, LAZY_FOLLOW, FIXED };
        Mode _cameraMode = Mode.LAZY_FOLLOW;
        private Mode CameraMode
        {
            get { return _cameraMode; }
            set { _cameraMode = value; }
        }

        public Matrix Transform
        {
            get
            {
                return Matrix.CreateTranslation(new Vector3(
                  CoordinateTransform.ObjectToProjSpace(-_player.Position) +
                  new Vector2(Engine.GraphicsDevice.Viewport.Width / 2,
                              Engine.GraphicsDevice.Viewport.Height / 2) -
                  Vector2.One * 64, 0));
            }
        }

        Player _player;

        public Camera(Player player)
        {
            _player = player;
        }
    }
}
