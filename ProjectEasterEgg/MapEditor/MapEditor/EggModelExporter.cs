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
    static class EggModelExporter
    {
        private static string ResourceRelationshipType =
            "http://schemas.openxmlformats.org/package/2006/relationships/metadata/core-properties";

        internal static void SaveModel(IEnumerable<SaveBlock> saveBlocks, List<Animation> animations, string path)
        {
            XDocument doc = new XDocument();
            XElement root = new XElement("model");
            doc.Add(root);

            BoundingBoxInt boundingBox = new BoundingBoxInt(saveBlocks.ToPositions());
            List<SaveBlock> orderedBlocks = saveBlocks.OrderBy(block => boundingBox.getRelativeDepthOf(block.Position)).ToList();

            { // imports
                XElement imports = new XElement("imports");
                root.Add(imports);
                /* foreach model, add
                 * <imports>
                 *   <model offset="0 0 5">katt<model>
                 * </imports>
                 */
            }

            { // blocks
                XElement blocksElement = new XElement("blocks");
                root.Add(blocksElement);

                foreach (SaveBlock block in orderedBlocks)
                {
                    XElement blockElement = new XElement("block");
                    blocksElement.Add(blockElement);

                    blockElement.SetAttributeValue("offset", block.Position.GetSaveString());
                    blockElement.SetAttributeValue("type", block.type.GetSaveString());
                    if (!string.IsNullOrWhiteSpace(block.script))
                    {
                        blockElement.SetAttributeValue("script", Constants.SCRIPT_BLOCK_PREFIX + block.script);
                    }
                }
            }

            HashSet<Texture2DWithPos> allTextures = new HashSet<Texture2DWithPos>();
            { // animations

                XElement animationsElement = new XElement("animations");
                root.Add(animationsElement);
                foreach (Animation animation in animations)
                {
                    XElement animationElement = new XElement("animation");
                    animationsElement.Add(animationElement);
                    animationElement.SetAttributeValue("name", animation.Name);
                    animationElement.SetAttributeValue("facing", animation.Facing.GetSaveString());

                    foreach (Frame frame in animation.Frames)
                    {
                        XElement frameElement = new XElement("frame");
                        frameElement.SetAttributeValue("duration", frame.Duration);
                        animationElement.Add(frameElement);

                        foreach (Texture2DWithPos tex in frame.Textures.BackToFront())
                        {
                            XElement textureElement = new XElement("image");
                            frameElement.Add(textureElement);
                            textureElement.SetAttributeValue("name", tex.RelativePath);
                            textureElement.SetAttributeValue("coord", tex.Coord.GetSaveString());
                            allTextures.Add(tex);
                            //TODO: foreach (SaveBlock block in tex.projectedOnto)
                            foreach (SaveBlock block in orderedBlocks)
                            {
                                XElement projectedOntoElement = new XElement("projectedOnto");
                                projectedOntoElement.Value = orderedBlocks.IndexOf(block).GetSaveString();
                                textureElement.Add(projectedOntoElement);
                            }
                        }
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
                PackagePart packagePartDocument = package.CreatePart(new Uri("/model.xml", UriKind.Relative), System.Net.Mime.MediaTypeNames.Text.Xml);

                packagePartDocument.GetStream().Write(doc.ToString());

                // Add a Package Relationship to the Document Part
                package.CreateRelationship(packagePartDocument.Uri, TargetMode.Internal, ResourceRelationshipType);

                foreach (Texture2DWithPos tex in allTextures) {
                    PackagePart blockImage = package.CreatePart(new Uri("/textures/" + tex.RelativePath, UriKind.Relative), "image/png");
                    tex.Texture.SaveAsPng(blockImage.GetStream(), tex.Texture.Width, tex.Texture.Height);
                    package.CreateRelationship(blockImage.Uri, TargetMode.Internal, ResourceRelationshipType);
                }
            }
            System.Console.WriteLine("Saved to: " + path);
        }

    }
}