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

        internal static void SaveModel(IEnumerable<Position> blockPositions, IEnumerable<Texture2DWithPos> textures, string path)
        {

        }

        private static IEnumerable<SaveBlock> SplitTextures(IEnumerable<Position> blockPositions, IEnumerable<Texture2DWithPos> textures)
        {
            List<SaveBlock> saveBlocks = new List<SaveBlock>();
            int i = 0;
            foreach (Position pos in blockPositions)
            {
                SaveBlock saveBlock = new SaveBlock();
                saveBlock.Position = pos;
                saveBlock.id = i++;
                saveBlock.type = 1;
                saveBlock.Texture = textures.First().Texture;
                saveBlocks.Add(saveBlock);
            }
            return saveBlocks;
        }

        internal static void CompileModel(IEnumerable<Position> blockPositions, IEnumerable<Texture2DWithPos> textures, string path)
        {
            IEnumerable<SaveBlock> blocks = SplitTextures(blockPositions, textures);
            XDocument doc = new XDocument();
            XElement root = new XElement("model");
            doc.Add(root);

            {
                BoundingBoxInt boundingBox = new BoundingBoxInt(blocks.ToPositions());
                XElement boundingElement = new XElement("bounds");
                root.Add(boundingElement);
                XElement minElement = new XElement("min");
                XElement maxElement = new XElement("max");
                boundingElement.Add(minElement);
                boundingElement.Add(maxElement);
                minElement.Value = boundingBox.Min.GetSaveString();
                maxElement.Value = boundingBox.Max.GetSaveString();
            }

            {
                XElement imports = new XElement("imports");
                root.Add(imports);
                /* foreach model, add
                 * <imports>
                 *   <model offset="0 0 5">katt<model>
                 * </imports>
                 */
            }

            {
                XElement animations = new XElement("animations");
                root.Add(animations);
                XElement animation = new XElement("animation");
                animation.SetAttributeValue("name", "still");
                animations.Add(animation);

                XElement frame = new XElement("frame");
                frame.SetAttributeValue("duration", 0);
                animation.Add(frame);

                foreach (SaveBlock block in blocks)
                {
                    XElement blockElement = new XElement("block");
                    blockElement.SetAttributeValue("id", block.id);
                    blockElement.SetAttributeValue("image", block.id);
                    frame.Add(blockElement);
                }
            }
            
            {
                XElement blocksElement = new XElement("blocks");
                root.Add(blocksElement);
                
                int i=0;
                foreach (SaveBlock block in blocks) {
                    XElement blockElement = new XElement("block");
                    blocksElement.Add(blockElement);
                    
                    blockElement.SetAttributeValue("id", i++);
                    blockElement.SetAttributeValue("offset", block.Position.GetSaveString());
                    blockElement.SetAttributeValue("type", block.type);
                    if (!string.IsNullOrEmpty(block.script)) {
                        blockElement.SetAttributeValue("script", "ScriptBlock"+block.script);
                    }
                }
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
                foreach (SaveBlock block in blocks)
                {
                    PackagePart blockImage = package.CreatePart(new Uri("/frames/" + i++ + ".png", UriKind.Relative), "image/png");
                    Texture2D tex = block.Texture;
                    tex.SaveAsPng(blockImage.GetStream(), tex.Width, tex.Height);
                    package.CreateRelationship(blockImage.Uri, TargetMode.Internal, ResourceRelationshipType);
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