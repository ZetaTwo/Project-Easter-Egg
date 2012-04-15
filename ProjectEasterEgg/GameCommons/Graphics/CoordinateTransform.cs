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

        public static Vector3 ScreenToObjectSpace(Point point, Camera camera, int layer = 0)
        {
            return ProjToObjectSpace(ScreenToProjSpace(point, camera), layer);
        }
        
        public static Point ScreenToProjSpace(Point point, Camera camera)
        {
            int x = (int)((-camera.Offset.X + point.X) / camera.Zoom);
            int y = (int)((-camera.Offset.Y + point.Y) / camera.Zoom);
            return new Point(x, y);
        }

        public static Vector3 ProjToObjectSpace(Point objPoint, int layer = 0)
        {
            return new Vector3((Constants.TILE_WIDTH * (objPoint.Y - layer * Constants.BLOCK_HEIGHT) + Constants.TILE_HEIGHT * objPoint.X) / (Constants.TILE_WIDTH * Constants.TILE_HEIGHT),
                               (Constants.TILE_WIDTH * (objPoint.Y - layer * Constants.BLOCK_HEIGHT) - Constants.TILE_HEIGHT * objPoint.X) / (Constants.TILE_WIDTH * Constants.TILE_HEIGHT),
                               layer);
        }

        /*
        public static Vector2 ScreenToObjectSpace(Point screenCoords, Camera camera)
        {
            int x = (-map.X + map.Y) * (int)(TILE_WIDTH / 2);
            int y = (map.X + map.Y) * (int)(TILE_HEIGHT / 2) - map.Z * BLOCK_HEIGHT;

            return new Vector2(x, y);
        }*/
    }
}
