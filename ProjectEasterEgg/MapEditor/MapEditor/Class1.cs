using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mindstep.EasterEgg.Commons;

namespace Mindstep.EasterEgg.MapEditor
{
    static class Exporter
    {
        public static void Save(IEnumerable<Block> blocks)
        {
            String path = "Content/save0.xml";
        }

        internal static void Save(List<Block> Blocks, string p)
        {
            throw new NotImplementedException();
        }
    }
}
