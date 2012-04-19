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

namespace Mindstep.EasterEgg.Commons.SaveLoad
{
    public static class EggModelSaver
    {
        private static string ResourceRelationshipType =
            "http://schemas.openxmlformats.org/package/2006/relationships/metadata/core-properties";

        public static void Save<T>(SaveModel<T> model, string path) where T : ImageWithPos
        {
            XDocument doc = new XDocument();
            XElement root = new XElement("model");
            doc.Add(root);

            BoundingBoxInt boundingBox = new BoundingBoxInt(model.blocks.ToPositions());
            List<SaveBlock> orderedBlocks = model.blocks.OrderBy(block => boundingBox.getRelativeDepthOf(block.Position)).ToList();

            { // imports
                XElement imports = new XElement("imports");
                root.Add(imports);
                foreach (SaveSubModel<T> subModel in model.subModels)
                {
                    XElement subModelElement = new XElement("model");
                    imports.Add(subModelElement);
                    subModelElement.SetAttributeValue("offset", subModel.offset.GetSaveString());
                    subModelElement.Value = subModel.name;
                }
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

            HashSet<T> allTextures = new HashSet<T>();
            { // animations

                XElement animationsElement = new XElement("animations");
                root.Add(animationsElement);
                foreach (SaveAnimation<T> animation in model.animations)
                {
                    XElement animationElement = new XElement("animation");
                    animationsElement.Add(animationElement);
                    animationElement.SetAttributeValue("name", animation.Name);
                    animationElement.SetAttributeValue("facing", animation.Facing.GetSaveString());

                    foreach (SaveFrame<T> frame in animation.Frames)
                    {
                        XElement frameElement = new XElement("frame");
                        frameElement.SetAttributeValue("duration", frame.Duration);
                        animationElement.Add(frameElement);

                        foreach (T tex in frame.Images.BackToFront())
                        {
                            XElement textureElement = new XElement("image");
                            frameElement.Add(textureElement);
                            textureElement.SetAttributeValue("name", tex.name);
                            textureElement.SetAttributeValue("coord", tex.pos.GetSaveString());
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
            //  The 'using' statement ensures that 'package' is
            //  closed and disposed when it goes out of scope.)
            using (Package package =
                Package.Open(path, FileMode.Create))
            {
                // Add the Document part to the Package
                PackagePart packagePartDocument = package.CreatePart(new Uri("/model.xml", UriKind.Relative), System.Net.Mime.MediaTypeNames.Text.Xml);

                packagePartDocument.GetStream().Write(doc.ToString());

                // Add a Package Relationship to the Document Part
                package.CreateRelationship(packagePartDocument.Uri, TargetMode.Internal, ResourceRelationshipType);

                foreach (ImageWithPos tex in allTextures) {
                    PackagePart blockImage = package.CreatePart(new Uri("/textures/" + tex.name, UriKind.Relative), "image/png");
                    tex.SaveTo(blockImage.GetStream());
                    package.CreateRelationship(blockImage.Uri, TargetMode.Internal, ResourceRelationshipType);
                }
            }
            System.Console.WriteLine("Saved to: " + path);
        }
    }
}