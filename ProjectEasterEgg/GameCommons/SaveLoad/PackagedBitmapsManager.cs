using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;

namespace Mindstep.EasterEgg.Commons.SaveLoad
{
    public class PackagedBitmapsManager : IDisposable
    {
        private Dictionary<string, Bitmap> bitmaps = new Dictionary<string, Bitmap>();
        private ZipFile modelFile;
        private string imageNamePrefix;

        public PackagedBitmapsManager(ZipFile modelFile, string imageNamePrefix)
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
                    Stream bitmapStream = modelFile.GetInputStream(modelFile.GetEntry(imageNamePrefix + imageName));
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
