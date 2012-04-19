using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Mindstep.EasterEgg.Commons.SaveLoad
{
    public static class Extensions
    {
        public static void Write(this Stream stream, string stringToWrite)
        {
            byte[] bytes = UnicodeEncoding.UTF8.GetBytes(stringToWrite);
            stream.Write(bytes, 0, bytes.Length);
        }
    }
}
