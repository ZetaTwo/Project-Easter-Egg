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
        public float Zoom { get { return zoomLevels[zoomIndex]; } }

        private Matrix zoomMatrix;
        public Matrix ZoomMatrix { get { return zoomMatrix; } }

        private Matrix offsetMatrix;
        public Matrix OffsetMatrix { get { return offsetMatrix; } }

        private Matrix zoomAndOffsetMatrix;
        public Matrix ZoomAndOffsetMatrix { get { return zoomAndOffsetMatrix; } }

        private Point offset;
        public Point Offset
        {
            get { return offset; }
            set
            {
                offset = value;
                offsetChanged();
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

        public void ZoomIn()
        {
            zoomIndex = Math.Min(zoomIndex + 1, zoomLevels.Length - 1);
            zoomChanged();
        }

        public void ZoomOut()
        {
            zoomIndex = Math.Max(zoomIndex - 1, 0);
            zoomChanged();
        }

        private void zoomChanged()
        {
            zoomMatrix = Matrix.CreateScale(Zoom);
            zoomOrOffsetChanged();
        }

        private void offsetChanged()
        {
            offsetMatrix = Matrix.CreateTranslation(offset.X, offset.Y, 0);
            zoomOrOffsetChanged();
        }

        private void zoomOrOffsetChanged()
        {
            zoomAndOffsetMatrix = ZoomMatrix * OffsetMatrix;
        }
    }
}
