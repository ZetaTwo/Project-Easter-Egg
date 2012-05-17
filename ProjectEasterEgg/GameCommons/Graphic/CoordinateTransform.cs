using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Mindstep.EasterEgg.Commons.Graphic;

namespace Mindstep.EasterEgg.Commons
{
    public static class CoordinateTransform
    {
        private const int TILE_WIDTH_OVER_2 = Constants.TILE_WIDTH / 2;
        private const int TILE_HEIGHT_OVER_2 = Constants.TILE_HEIGHT / 2;

        public static Vector2 ObjectToProjectionSpace(Vector3 map)
        {
            return ObjectToProjectionSpace(map.ToPosition());
        }
        public static Vector2 ObjectToProjectionSpace(Position map)
        {
            int x = (-map.X + map.Y) * TILE_WIDTH_OVER_2;
            int y = (map.X + map.Y) * TILE_HEIGHT_OVER_2 - map.Z * Constants.BLOCK_HEIGHT;

            return new Vector2(x, y);
        }

        public static Vector2 ObjectToBlockDrawCoordsInProjectionSpace(Vector3 map)
        {
            return ObjectToProjectionSpace(map) + Constants.blockDrawOffset;
        }

        public static Vector3 ScreenToObjectSpace(Point point, Camera camera, int layer)
        {
            return ProjToObjectSpace(ScreenToProjSpace(point, camera), layer);
        }
        
        public static Point ScreenToProjSpace(Point point, Camera camera)
        {
            int x = ((-camera.Offset.X + point.X) / camera.Zoom).RoundDown();
            int y = ((-camera.Offset.Y + point.Y) / camera.Zoom).RoundDown();
            return new Point(x, y);
        }

        public static Vector3 ProjToObjectSpace(Point projPoint, int layer)
        {
            float m = (float)projPoint.X / TILE_WIDTH_OVER_2;
            float n = (float)(projPoint.Y + layer * Constants.BLOCK_HEIGHT) / TILE_HEIGHT_OVER_2;

            return new Vector3((n - m) / 2, (n + m) / 2, layer);
        }

        //public static Vector3 ProjToObjectSpace(Point objPoint, int layer = 0)
        //{
        //    float x = (Constants.TILE_WIDTH * (objPoint.Y - layer * Constants.BLOCK_HEIGHT) + Constants.TILE_HEIGHT * objPoint.X) / (Constants.TILE_WIDTH * Constants.TILE_HEIGHT);
        //    float y = (Constants.TILE_WIDTH * (objPoint.Y - layer * Constants.BLOCK_HEIGHT) - Constants.TILE_HEIGHT * objPoint.X) / (Constants.TILE_WIDTH * Constants.TILE_HEIGHT);
        //    return new Vector3(x, y, layer);
        //}
    }
}
