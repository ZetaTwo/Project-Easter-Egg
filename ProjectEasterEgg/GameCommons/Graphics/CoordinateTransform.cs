using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Mindstep.EasterEgg.Commons.Graphics;

namespace Mindstep.EasterEgg.Commons
{
    public static class CoordinateTransform
    {
        public static Vector2 ObjectToProjectionSpace(Position map)
        {
            int x = (-map.X + map.Y) * (int)(Constants.TILE_WIDTH / 2);
            int y = (map.X + map.Y) * (int)(Constants.TILE_HEIGHT / 2) - map.Z * Constants.BLOCK_HEIGHT;

            return new Vector2(x,y);
        }

        public static Vector2 ObjectToScreenSpace(Position map, Camera camera)
        {
            Vector4 mulResult = Mindstep.EasterEgg.Commons.Extensions.matrixMul(new Vector4(ObjectToProjectionSpace(map), 0, 0), camera.ZoomAndOffsetMatrix);
            return new Vector2(mulResult.X, mulResult.Y);
        }

        public static Point ScreenToProjSpace(Point point, Camera camera)
        {
            int x = (int)((-camera.Offset.X + point.X) / camera.Zoom);
            int y = (int)((-camera.Offset.Y + point.Y) / camera.Zoom);
            return new Point(x, y);
        }

        /*
        public static Vector2 ScreenToObjectSpace(Point screenCoords, Camera camera)
        {
            int x = (-map.X + map.Y) * (int)(TILE_WIDTH / 2);
            int y = (map.X + map.Y) * (int)(TILE_HEIGHT / 2) - map.Z * BLOCK_HEIGHT;

            return new Vector2(x, y);
        }*/

        public static Vector2 ToVector2(this Point point)
        {
            return new Vector2(point.X, point.Y);
        }

        public static Vector2 ToVector2(this System.Drawing.Point point)
        {
            return new Vector2(point.X, point.Y);
        }

        public static Point ToXnaPoint(this System.Drawing.Point point)
        {
            return new Point(point.X, point.Y);
        }


        public static System.Drawing.Point ToSDPoint(this Point point)
        {
            return new System.Drawing.Point(point.X, point.Y);
        }

        public static System.Drawing.Point ToPoint(this Vector2 point)
        {
            return new System.Drawing.Point((int)point.X, (int)point.Y);
        }

        public static Point ToXnaPoint(this Vector2 point)
        {
            return new Point((int)point.X, (int)point.Y);
        }

        public static Vector3 FromScreen(Vector2 screen, int layer = 0)
        {
            return new Vector3((Constants.TILE_WIDTH * (screen.Y - layer * Constants.BLOCK_HEIGHT) + Constants.TILE_HEIGHT * screen.X) / (Constants.TILE_WIDTH * Constants.TILE_HEIGHT),
                               (Constants.TILE_WIDTH * (screen.Y - layer * Constants.BLOCK_HEIGHT) - Constants.TILE_HEIGHT * screen.X) / (Constants.TILE_WIDTH * Constants.TILE_HEIGHT),
                               layer);
        }
    }
}
