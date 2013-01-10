using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mindstep.EasterEgg.Commons;
using System.Xml.Linq;
using Mindstep.EasterEgg.Commons.SaveLoad;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using ICSharpCode.SharpZipLib.Zip;

namespace Mindstep.EasterEgg.Commons.SaveLoad
{
    public static class EggModelSaver
    {
        //private static string ResourceRelationshipType =
        //    "http://schemas.openxmlformats.org/package/2006/relationships/metadata/core-properties";

        /// <summary>
        /// Save the model to the given path.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="path">Path to save to, including file name</param>
        /// <exception cref="Exception">All possible save exceptions?</exception>
        public static void Save(SaveModel<Texture2DWithPos> model, string path)
        {
            XDocument doc = new XDocument();
            XElement root = new XElement("model");
            doc.Add(root);

            BoundingBoxInt boundingBox = new BoundingBoxInt(model.blocks.ToPositions());
            List<SaveBlock> orderedBlocks = model.blocks.OrderBy(block => boundingBox.getRelativeDepthOf(block.Position)).ToList();

            { // imports
                XElement imports = new XElement("imports");
                root.Add(imports);
                foreach (SaveSubModel<Texture2DWithPos> subModel in model.subModels)
                {
                    XElement subModelElement = new XElement("model");
                    imports.Add(subModelElement);
                    subModelElement.SetAttributeValue("offset", subModel.offset.GetSaveString());
                    subModelElement.Value = subModel.Name;
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
                        blockElement.SetAttributeValue("script", block.script);
                    }
                }
            }

            HashSet<Texture2DWithPos> allTextures = new HashSet<Texture2DWithPos>();
            { // animations

                XElement animationsElement = new XElement("animations");
                root.Add(animationsElement);
                foreach (SaveAnimation<Texture2DWithPos> animation in model.animations)
                {
                    XElement animationElement = new XElement("animation");
                    animationsElement.Add(animationElement);
                    animationElement.SetAttributeValue("name", animation.Name);
                    animationElement.SetAttributeValue("facing", animation.Facing.GetSaveString());

                    foreach (SaveFrame<Texture2DWithPos> frame in animation.Frames)
                    {
                        XElement frameElement = new XElement("frame");
                        frameElement.SetAttributeValue("duration", frame.Duration);
                        animationElement.Add(frameElement);

                        foreach (Texture2DWithPos tex in frame.Images.BackToFront())
                        {
                            XElement textureElement = new XElement("image");
                            frameElement.Add(textureElement);
                            textureElement.SetAttributeValue("name", tex.name);
                            textureElement.SetAttributeValue("coord", tex.pos.GetSaveString());
                            allTextures.Add(tex);
                            
                            foreach (SaveBlock block in tex.projectedOnto)
                            {
                                XElement projectedOntoElement = new XElement("projectedOnto");
                                projectedOntoElement.Value = orderedBlocks.IndexOf(block).GetSaveString();
                                textureElement.Add(projectedOntoElement);
                            }
                        }
                    }
                }
            }

            using (ZipOutputStream zipStream = new ZipOutputStream(new FileStream(path, FileMode.Create)))
            {
                zipStream.PutNextEntry(new ZipEntry("model.xml"));
                zipStream.Write(doc.ToString());

                foreach (ImageWithPos tex in allTextures)
                {
                    zipStream.PutNextEntry(new ZipEntry("textures/" + tex.name));
                    tex.SaveTo(zipStream);
                }
            }
        }
    }
}