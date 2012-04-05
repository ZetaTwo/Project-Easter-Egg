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
using Mindstep.EasterEgg.MapEditor.Animations;

namespace Mindstep.EasterEgg.MapEditor
{
    static class Exporter
    {
        private static string ResourceRelationshipType =
            "http://schemas.openxmlformats.org/package/2006/relationships/metadata/core-properties";

        internal static void SaveModel(IEnumerable<SaveBlock> blocks, IEnumerable<Animation> animations, string path)
        {
            XDocument doc = new XDocument();
            XElement root = new XElement("model");
            doc.Add(root);

            {
                BoundingBoxInt boundingBox = new BoundingBoxInt(blocks.ToPositions());
                blocks = blocks.OrderBy(block => boundingBox.getRelativeDepthOf(block.Position));


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

            HashSet<Texture2DWithPos> allTextures = new HashSet<Texture2DWithPos>();
            {

                XElement animationsElement = new XElement("animations");
                root.Add(animationsElement);
                foreach (Animation animation in animations)
                {
                    XElement animationElement = new XElement("animation");
                    animationsElement.Add(animationElement);
                    animationElement.SetAttributeValue("name", animation.Name);

                    foreach (KeyValuePair<int, Frame> frameKeyValuePair in animation.Frames)
                    {
                        Frame frame = frameKeyValuePair.Value;
                        XElement frameElement = new XElement("frame");
                        animationElement.Add(frameElement);
                        frameElement.SetAttributeValue("duration", frame.Duration);

                        foreach (Texture2DWithPos tex in frame.Textures)
                        {
                            XElement textureElement = new XElement("texture");
                            frameElement.Add(textureElement);
                            textureElement.SetAttributeValue("name", tex.Name);
                            textureElement.SetAttributeValue("coord", tex.Coord.GetSaveString());
                            allTextures.Add(tex);
                        }
                    }
                }
            }
            
            {

                XElement blocksElement = new XElement("blocks");
                root.Add(blocksElement);

                int i=0;
                foreach (SaveBlock block in blocks) {
                    XElement blockElement = new XElement("block");
                    blocksElement.Add(blockElement);
                    
                    blockElement.SetAttributeValue("offset", block.Position.GetSaveString());
                    blockElement.SetAttributeValue("type", block.type);
                    if (!string.IsNullOrEmpty(block.script)) {
                        blockElement.SetAttributeValue("script", "ScriptBlock"+block.script);
                    }
                }
            }

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

                foreach (Texture2DWithPos tex in allTextures) {
                    PackagePart blockImage = package.CreatePart(new Uri("/textures/" + tex.Name.Split('/').Last(), UriKind.Relative), "image/png");
                    tex.Texture.SaveAsPng(blockImage.GetStream(), tex.Texture.Width, tex.Texture.Height);
                    package.CreateRelationship(blockImage.Uri, TargetMode.Internal, ResourceRelationshipType);
                }
            }
            System.Console.WriteLine("Saved to: " + path);
        }

    }
}

/*
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

internal static void CompileModel(IEnumerable<SaveBlock> blockPositions, IEnumerable<Texture2DWithPos> textures, string path)
{
    IEnumerable<SaveBlock> blocks = SplitTextures(blockPositions.ToPositions(), textures);
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
         *
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
*/