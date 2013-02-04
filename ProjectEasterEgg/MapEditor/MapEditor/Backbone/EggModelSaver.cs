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

namespace Mindstep.EasterEgg.MapEditor
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
        public static void Save(Model model, string fileName)
        {
            XDocument doc = new XDocument();
            XElement root = new XElement("model");
            doc.Add(root);

            BoundingBoxInt boundingBox = new BoundingBoxInt(model.Blocks.ToPositions());
            List<SaveBlock> orderedBlocks = model.Blocks.OrderBy(block => boundingBox.getRelativeDepthOf(block.Position)).ToList();

            { // imports
                XElement imports = new XElement("imports");
                root.Add(imports);
                foreach (SaveSubModel<Texture2DWithPos> subModel in model.SubModels)
                {
                    XElement subModelElement = new XElement("model");
                    imports.Add(subModelElement);
                    subModelElement.SetAttributeValue("offset", subModel.Offset.GetSaveString());
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
                    blockElement.SetAttributeValue("type", block.Type.GetSaveString());
                    if (!string.IsNullOrWhiteSpace(block.Script))
                    {
                        blockElement.SetAttributeValue("script", block.Script);
                    }
                }
            }

            HashSet<Texture2DWithPos> allTextures = new HashSet<Texture2DWithPos>();
            { // animations

                XElement animationsElement = new XElement("animations");
                root.Add(animationsElement);
                foreach (Animation animation in model.Animations)
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
                            textureElement.SetAttributeValue("coord", tex.Position.GetSaveString());
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

            using (ZipOutputStream zipStream = new ZipOutputStream(new FileStream(fileName, FileMode.Create)))
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