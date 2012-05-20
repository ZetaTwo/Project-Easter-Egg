using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Mindstep.EasterEgg.Commons.Graphic
{
    public class Camera
    {
        protected static float[] DEFAULT_ZOOM_LEVELS = { .5f, .75f, 1, 2, 4, 6, 8, 12, 16, 24, 32 };
        protected const int DEFAULT_ZOOM_INDEX = 2;
        protected static Point DEFAULT_OFFSET = Point.Zero;
        protected float[] zoomLevels;
        protected int zoomIndex;
        /// <summary>
        /// Gets the current zoom level, f, where f = 1 means no zoom
        /// </summary>
        public float Zoom { get { return zoomLevels[zoomIndex]; } }

        protected Matrix zoomMatrix;
        /// <summary>
        /// Gets the precalculated zoom matrix
        /// </summary>
        public Matrix ZoomMatrix { get { return zoomMatrix; } }

        protected Matrix offsetMatrix;
        /// <summary>
        /// Gets the precalculated offset matrix
        /// </summary>
        public Matrix OffsetMatrix { get { return offsetMatrix; } }

        protected Matrix zoomAndOffsetMatrix;
        /// <summary>
        /// Gets the precalculated zoom*offset matrix
        /// </summary>
        public Matrix ZoomAndOffsetMatrix { get { return zoomAndOffsetMatrix; } }

        protected Point offset;
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
            : this(DEFAULT_ZOOM_LEVELS, DEFAULT_ZOOM_INDEX)
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
            this.zoomIndex = zoomIndex.Clamp(0, zoomLevels.Length);
            this.Offset = offset;
        }




        /// <summary>
        /// Zooms in around the given point
        /// </summary>
        /// <param name="around">A Point in screen space, AKA:
        /// the distance in screen pixels from the upper
        /// left corner of the drawing</param>
        /// <param name="screenWidth"></param>
        /// <param name="screenHeight"></param>
        public void ZoomIn(Point around)
        {
            if (zoomIndex < zoomLevels.Length - 1)
            {
                float oldZoom = Zoom;
                zoomIndex++;
                float newZoom = Zoom;
                float zoomChange = newZoom / oldZoom;

                Point a = around.Subtract(Offset);
                Point b = a.Multiply(zoomChange);

                Offset = Offset.Add(a).Subtract(b);
            }
        }

        /// <summary>
        /// Zooms out around the given point
        /// </summary>
        /// <param name="point">A Point in screen space, AKA:
        /// the distance in screen pixels from the upper
        /// right corner of the drawing</param>
        public void ZoomOut(Point around)
        {
            if (zoomIndex > 0)
            {
                float oldZoom = Zoom;
                zoomIndex--;
                float newZoom = Zoom;
                float zoomChange = newZoom / oldZoom;

                Point a = around.Subtract(Offset);
                Point b = a.Multiply(zoomChange);

                Offset = Offset.Add(a).Subtract(b);
            }
        }

        protected void zoomOrOffsetChanged()
        {
            zoomMatrix = Matrix.CreateScale(Zoom);
            offsetMatrix = Matrix.CreateTranslation(Offset.X, Offset.Y, 0);
            zoomAndOffsetMatrix = ZoomMatrix * OffsetMatrix;
        }
    }
}
