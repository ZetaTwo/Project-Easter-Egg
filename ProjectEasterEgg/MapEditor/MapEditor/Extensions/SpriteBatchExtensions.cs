using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Mindstep.EasterEgg.MapEditor
{
    public static class SpriteBatchExtensions
    {
        private static MainForm mainForm;

        public static void Initialize(MainForm mainForm)
        {
            SpriteBatchExtensions.mainForm = mainForm;
        }

        public static void DrawLineSegment(this SpriteBatch spriteBatch, Vector2 point1, Vector2 point2, Color color, int lineWidth)
        {
            float angle = (float)Math.Atan2(point2.Y - point1.Y, point2.X - point1.X);
            float length = Vector2.Distance(point1, point2);
            spriteBatch.Draw(mainForm.whiteOneByOneTexture, point1, null, color,
                angle, Vector2.Zero, new Vector2(length, lineWidth),
                SpriteEffects.None, 0f);
        }

        public static void DrawPolygon(this SpriteBatch spriteBatch, Vector2[] vertex, int count, Color color, int lineWidth)
        {
            if (count > 0)
            {
                for (int i = 0; i < count - 1; i++)
                {
                    DrawLineSegment(spriteBatch, vertex[i], vertex[i + 1], color, lineWidth);
                }
                DrawLineSegment(spriteBatch, vertex[count - 1], vertex[0], color, lineWidth);
            }
        }

        public static void DrawRectangle(this SpriteBatch spriteBatch, int top, int bottom, int left, int right, Color color, int lineWidth)
        {
            Vector2[] vertex = new Vector2[4];
            vertex[0] = new Vector2(left, top);
            vertex[1] = new Vector2(right, top);
            vertex[2] = new Vector2(right, bottom);
            vertex[3] = new Vector2(left, bottom);

            DrawPolygon(spriteBatch, vertex, 4, color, lineWidth);
        }

        public static void DrawRectangle(this SpriteBatch spriteBatch, Rectangle rectangle, Color color, int lineWidth)
        {
            spriteBatch.DrawRectangle(rectangle.Top, rectangle.Bottom, rectangle.Left, rectangle.Right, color, lineWidth);
        }
    }
}
