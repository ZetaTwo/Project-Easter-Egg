using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mindstep.EasterEgg.Commons;
using System.Xml.Linq;
using Mindstep.EasterEgg.Commons.SaveLoad;
using System.IO.Packaging;
using System.IO;
using Microsoft.Xna.Framework.Graphics;

namespace Mindstep.EasterEgg.MapEditor
{
    static class Exporter
    {
        private static string ResourceRelationshipType =
            "http://schemas.openxmlformats.org/package/2006/relationships/metadata/core-properties";

        public static void Save(IEnumerable<Block> blocks)
        {
            String path = "Content/save0.xml";
        }

        internal static void SaveModel(List<Block> Blocks, string path)
        {
            
        }

        internal static void CompileModel(List<Block> blocks, string path)
        {
            XDocument doc = new XDocument();
            XElement root = new XElement("model");
            doc.Add(root);
            {
                XElement animations = new XElement("animations");
                root.Add(animations);
                XElement animation = new XElement("animation");
                animation.SetAttributeValue("name", "still");
                animations.Add(animation);

                XElement frame = new XElement("frame");
                frame.SetAttributeValue("duration", 0);
                frame.SetValue("0");
                animation.Add(frame);
            }
            {
                XElement blocksElement = new XElement("blocks");
                root.Add(blocksElement);
                
                int i=0;
                foreach (Block block in blocks) {
                    XElement blockElement = new XElement("block");
                    blocksElement.Add(blockElement);
                    
                    blockElement.SetAttributeValue("id", i++);
                    blockElement.SetAttributeValue("offset", block.Position.GetSaveString());
                }
            }
            {
                BoundingBoxInt boundingBox = new BoundingBoxInt(blocks.ToPositions());
                XElement boundingElement = new XElement("bounds");
                root.AddFirst(boundingElement);
                XElement minElement = new XElement("min");
                XElement maxElement = new XElement("max");
                boundingElement.Add(minElement);
                boundingElement.Add(maxElement);
                minElement.Value = boundingBox.Min.GetSaveString();
                maxElement.Value = boundingBox.Max.GetSaveString();
            }
            
            //doc.Save(path);
            
            // Create the Package
            // (If the package file already exists, FileMode.Create will
            //  automatically delete it first before creating a new one.
            //  The 'using' statement insures that 'package' is
            //  closed and disposed when it goes out of scope.)
            using (Package package =
                Package.Open(path, FileMode.Create))
            {
                // Add the Document part to the Package
                //PackagePart packagePartDocument = package.CreatePart(new Uri("lol"), "image/png");
                PackagePart packagePartDocument = package.CreatePart(new Uri("/model.xml", UriKind.Relative), System.Net.Mime.MediaTypeNames.Text.Xml);

                packagePartDocument.GetStream().Write(doc.ToString());
                
                // Add a Package Relationship to the Document Part
                package.CreateRelationship(packagePartDocument.Uri, TargetMode.Internal, ResourceRelationshipType);

                int i=0;
                foreach (Block block in blocks)
                {
                    PackagePart blockImage = package.CreatePart(new Uri("/frames/" + "i" + ".png", UriKind.Relative), "image/png");
                    //Texture2D a;
                    //a.SaveAsPng(blockImage.GetStream(), a.Width, a.Height);
                    package.CreateRelationship(blockImage.Uri, TargetMode.Internal, ResourceRelationshipType);
                    i++;
                }
            }
            System.Console.WriteLine("Saved to: " + path);
        }
    }
}

/*
<?xml?>
<root>
    <frames>
	 <frame id="1">base/bottom.png</frame>
	</frames>
	<animations>
		<animation name="burning">
		 <frame duration="1">1</frame>
		 <frame duration="2">2</frame>
		</animation>
		<animation name="notburning" durations="1" />
	</animations>
	<boxes>
		<box id="0" size="6" offset="0 0 0">
		<box id="1" size="6" offset="0 0 6">
	</boxes>
</root>
*/