using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO.Packaging;
using System.IO;

namespace Mindstep.EasterEgg.Commons.SaveLoad
{
    public class PackagedBitmapsManager : IDisposable
    {
        private Dictionary<string, Bitmap> bitmaps = new Dictionary<string, Bitmap>();
        private Package modelFile;
        private string imageNamePrefix;

        public PackagedBitmapsManager(Package modelFile, string imageNamePrefix)
        {
            this.modelFile = modelFile;
            this.imageNamePrefix = imageNamePrefix;
        }

        public Bitmap this[string imageName]
        {
            get
            {
                if (!bitmaps.ContainsKey(imageName))
                {
                    Stream bitmapStream = modelFile.GetPart(new Uri(imageNamePrefix + imageName, UriKind.Relative)).GetStream();
                    bitmaps[imageName] = new Bitmap(bitmapStream);
                }
                return bitmaps[imageName];
            }
        }

        public void Dispose()
        {
            modelFile.Close();
        }
    }
}
