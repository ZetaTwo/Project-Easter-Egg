using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Mindstep.EasterEgg.Commons.Graphics
{
    public class Camera
    {
        private static float[] DEFAULT_ZOOM_LEVELS = { .5f, .75f, 1, 2, 4, 6, 8, 12, 16, 24, 32 };
        private const int DEFAULT_ZOOM_INDEX = 2;
        private static Point DEFAULT_OFFSET = Point.Zero;
        private float[] zoomLevels;
        private int zoomIndex;
        /// <summary>
        /// Gets the current zoom level, f, where f = 1 means no zoom
        /// </summary>
        public float Zoom { get { return zoomLevels[zoomIndex]; } }

        private Matrix zoomMatrix;
        /// <summary>
        /// Gets the precalculated zoom matrix
        /// </summary>
        public Matrix ZoomMatrix { get { return zoomMatrix; } }

        private Matrix offsetMatrix;
        /// <summary>
        /// Gets the precalculated offset matrix
        /// </summary>
        public Matrix OffsetMatrix { get { return offsetMatrix; } }

        private Matrix zoomAndOffsetMatrix;
        /// <summary>
        /// Gets the precalculated zoom*offset matrix
        /// </summary>
        public Matrix ZoomAndOffsetMatrix { get { return zoomAndOffsetMatrix; } }

        private Point offset;
        /// <summary>
        /// A position in projection space describing
        /// the upper left corner of the screen.
        /// 
        /// Screen space = the distance in screen pixels
        /// from the upper left corner of the drawing
        /// </summary>
        public Point Offset
        {
            get { return offset; }
            set
            {
                offset = value;
                zoomOrOffsetChanged();
            }
        }





        public Camera()
            : this(DEFAULT_ZOOM_LEVELS, DEFAULT_ZOOM_INDEX, DEFAULT_OFFSET) 
        { }

        public Camera(float[] zoomLevels, int zoomIndex)
            : this(zoomLevels, zoomIndex, DEFAULT_OFFSET)
        { }

        public Camera(Point offset)
            : this(DEFAULT_ZOOM_LEVELS, DEFAULT_ZOOM_INDEX, offset)
        { }

        public Camera(float[] zoomLevels, int zoomIndex, Point offset)
        {
            this.zoomLevels = zoomLevels;
            this.zoomIndex = zoomIndex;
            this.zoomIndex = Extensions.Clamp(zoomIndex, 0, zoomLevels.Length);
            this.Offset = offset;
        }




        /// <summary>
        /// Zooms in around the upper left corner of the screen
        /// </summary>
        public void ZoomIn()
        {
            //ZoomIn(Point.Zero);
        }

        /// <summary>
        /// Zooms out around the upper left corner of the screen
        /// </summary>
        public void ZoomOut()
        {
            //ZoomOut(Point.Zero);
        }

        /// <summary>
        /// Zooms in around the given point
        /// </summary>
        /// <param name="around">A Point in screen space, AKA:
        /// the distance in screen pixels from the upper
        /// left corner of the drawing</param>
        /// <param name="screenWidth"></param>
        /// <param name="screenHeight"></param>
        public void ZoomIn(Point around, int screenWidth, int screenHeight)
        {
            if (zoomIndex < zoomLevels.Length - 1)
            {
                float oldZoom = zoomLevels[zoomIndex];
                float newZoom = zoomLevels[zoomIndex+1];
                float zoomChange = newZoom / oldZoom;
                int newWidth = (int)(screenWidth * zoomChange);
                int newHeight = (int)(screenHeight * zoomChange);

                Point a = around.Subtract(offset);
                Point b = a.Multiply(newWidth).Divide(screenWidth);
                Point x = a.Subtract(b);

                zoomIndex++;
                Offset = Offset.Add(x);
            }
        }

        /// <summary>
        /// Zooms out around the given point
        /// </summary>
        /// <param name="point">A Point in screen space, AKA:
        /// the distance in screen pixels from the upper
        /// right corner of the drawing</param>
        public void ZoomOut(Point around, int screenWidth, int screenHeight)
        {
            if (zoomIndex > 0)
            {
                float oldZoom = zoomLevels[zoomIndex];
                float newZoom = zoomLevels[zoomIndex - 1];
                float zoomChange = newZoom / oldZoom;
                int newWidth = (int)(screenWidth * zoomChange);
                int newHeight = (int)(screenHeight * zoomChange);

                Point a = around.Subtract(offset);
                Point b = a.Multiply(newWidth).Divide(screenWidth);
                Point x = a.Subtract(b);

                zoomIndex--;
                Offset = Offset.Add(x);
            }
        }

        private void zoomOrOffsetChanged()
        {
            zoomMatrix = Matrix.CreateScale(Zoom);
            offsetMatrix = Matrix.CreateTranslation(offset.X, offset.Y, 0);
            zoomAndOffsetMatrix = ZoomMatrix * OffsetMatrix;
        }
    }
}
