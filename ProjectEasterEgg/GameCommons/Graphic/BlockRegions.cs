using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Mindstep.EasterEgg.Commons;
using XNA = Microsoft.Xna.Framework;
using System.Diagnostics;

namespace Mindstep.EasterEgg.Commons.Graphic
{
    public static class BlockRegions
    {
        public static readonly Region WholeBlock;
        public static readonly Region InnerBlock;
        public static readonly Region Top;
        public static readonly Region Left;
        public static readonly Region Right;
        public static readonly Region OuterBorder;
        public static readonly Region InnerBorder;

        static BlockRegions()
        {
            int n = Constants.PROJ_WIDTH / 4;

            WholeBlock = new Region();
            WholeBlock.MakeEmpty();
            for (int i = 0; i <= n; i++)
            {
                WholeBlock.Union(new Rectangle(n * 2 - 1 - 2 * i, i, i * 4 + 2, Constants.PROJ_HEIGHT - 2 * i));
            }
            WholeBlock.Intersect(new Rectangle(0, 0, Constants.PROJ_WIDTH, Constants.PROJ_HEIGHT));
            InnerBlock = new Region();
            InnerBlock.MakeEmpty();
            for (int i = 0; i <= n; i++)
            {
                InnerBlock.Union(new Rectangle(n * 2 - 1 - 2 * i, i + 1, i * 4 + 2, Constants.PROJ_HEIGHT - 2 - 2 * i));
            }
            InnerBlock.Intersect(new Rectangle(1, 1, Constants.PROJ_WIDTH - 2, Constants.PROJ_HEIGHT - 2));

            OuterBorder = WholeBlock.Clone();
            OuterBorder.Exclude(InnerBlock);

            Top = InnerBlock.Clone();
            Top.Translate(0, -Constants.BLOCK_HEIGHT);
            Top.Intersect(InnerBlock);

            Left = InnerBlock.Clone();
            Left.Exclude(Top);
            Top.Translate(0, 1);
            Left.Exclude(Top);
            Top.Translate(0, -1);

            Right = Left.Clone();
            Left.Intersect(new Rectangle(0, 0, Constants.PROJ_WIDTH / 2, Constants.PROJ_HEIGHT));
            Right.Intersect(new Rectangle(Constants.PROJ_WIDTH / 2 + 1, 0, Constants.PROJ_WIDTH / 2, Constants.PROJ_HEIGHT));

            InnerBorder = InnerBlock.Clone();
            InnerBorder.Exclude(Top);
            InnerBorder.Exclude(Left);
            InnerBorder.Exclude(Right);
        }

        public static Region Offset(this Region region, int x, int y)
        {
            Region newRegion = region.Clone();
            newRegion.Translate(x, y);
            return newRegion;
        }

        public static Region Offset(this Region region, XNA.Point offset)
        {
            return region.Offset(offset.X, offset.Y);
        }
    }
}
