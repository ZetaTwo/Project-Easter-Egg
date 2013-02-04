using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using Xna = Microsoft.Xna.Framework;
using Mindstep.EasterEgg.Commons.SaveLoad;

namespace Mindstep.EasterEgg.Commons.Graphic
{
    public class BlockImage
    {
        protected readonly Bitmap bitmap;
        protected readonly Graphics graphics;

        public BlockImage()
        {
            bitmap = new Bitmap(Constants.PROJ_WIDTH, Constants.PROJ_HEIGHT);
            graphics = System.Drawing.Graphics.FromImage(bitmap);
            graphics.Clear(Color.Transparent);
            graphics.SetClip(BlockRegions.WholeBlock, CombineMode.Replace);
            graphics.FillRegion(Brushes.Black, BlockRegions.OuterBorder);
            graphics.FillRegion(Brushes.White, BlockRegions.Top);
            graphics.FillRegion(Brushes.LightGray, BlockRegions.Left);
            graphics.FillRegion(Brushes.Gray, BlockRegions.Right);
            graphics.FillRegion(Brushes.Pink, BlockRegions.InnerBorder);
        }

        /// <summary>
        /// Steals all the pixels from the given image
        /// within a block outlined by BlockRegion.WholeBlock offseted by myPos - from.pos
        /// </summary>
        /// <param name="from"></param>
        /// <param name="myPos">used to calculate the offset of the from image</param>
        public void stealPixelsFrom(BitmapWithPos from, Position myPos)
        {
            Xna.Point pos = CoordinateTransform.ObjectToBlockDrawCoordsInProjectionSpace(myPos.ToVector3()).ToXnaPoint();
            stealPixelsFrom(from, pos);
        }

        /// <summary>
        /// Steals all the pixels from the given image
        /// within a block outlined by BlockRegion.WholeBlock offseted by myPos - from.pos
        /// </summary>
        /// <param name="from"></param>
        /// <param name="myPos">used to calculate the offset of the from image</param>
        public void stealPixelsFrom(BitmapWithPos from, Xna.Point myPos)
        {
            Xna.Point offset = myPos.Subtract(from.Position);
            graphics.DrawImage(from.bitmap, offset.Multiply(-1).ToSDPoint());
            using (Graphics fromGraphics = Graphics.FromImage(from.bitmap))
            {
                fromGraphics.CompositingMode = CompositingMode.SourceOver;
                fromGraphics.FillRegion(Brushes.Transparent, BlockRegions.WholeBlock.Offset(offset));
            }
        }
    }
}
