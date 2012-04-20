using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Packaging;
using System.Xml.Linq;
using Microsoft.Xna.Framework.Graphics;
using System.Drawing;

namespace Mindstep.EasterEgg.Commons.SaveLoad
{
    public class EggModelLoader
    {
        public static SaveModel<BitmapWithPos> Load(string wholePath)
        {
            wholePath = wholePath.Replace('\\', '/');
            int lastSlashIndex = wholePath.LastIndexOf('/');
            string directoryPath = wholePath.Substring(0, lastSlashIndex);
            string modelName = wholePath.Substring(lastSlashIndex+1);
            modelName = modelName.Substring(0, modelName.Length - 4);
            return Load(directoryPath, modelName);
        }

        /// <summary>
        /// Imports a model from the file
        /// <code>directoryPath+"/"+modelName+".egg"</code>
        /// including sub models
        /// </summary>
        /// <param name="directoryPath"></param>
        /// <param name="modelName"></param>
        /// <returns></returns>
        public static SaveModel<BitmapWithPos> Load(string directoryPath, string modelName)
        {
            SaveModel<BitmapWithPos> model = new SaveModel<BitmapWithPos>(modelName);
            using (Package modelFile = Package.Open(directoryPath + "/" + modelName + ".egg"))
            {
                using (PackagedBitmapsManager bitmapManager = new PackagedBitmapsManager(modelFile, "/textures/"))
                {
                    PackagePart modelXML = modelFile.GetPart(new Uri("/model.xml", UriKind.Relative));
                    XDocument doc = XDocument.Load(modelXML.GetStream());
                    XElement root = doc.Element("model");

                    // blocks
                    {
                        foreach (XElement blockElement in root.Element("blocks").Elements("block"))
                        {
                            Position pos = blockElement.Attribute("offset").Value.LoadPosition();

                            SaveBlock block = new SaveBlock(pos);
                            model.blocks.Add(block);
                            block.type = blockElement.Attribute("type").Value.LoadBlockType();
                            XAttribute scriptAttribute = blockElement.Attribute("script");
                            if (scriptAttribute != null)
                            {
                                block.script = Constants.SCRIPT_BLOCK_PREFIX + scriptAttribute.Value;
                            }
                        }
                    }

                    // imports
                    foreach (XElement modelElement in root.Element("imports").Elements("model"))
                    {
                        Position offset = modelElement.Attribute("offset").Value.LoadPosition();
                        model.subModels.Add(new SaveSubModel<BitmapWithPos>(Load(directoryPath, modelElement.Value), offset));
                    }

                    // animations
                    foreach (XElement animationElement in root.Element("animations").Elements("animation"))
                    {
                        SaveAnimation<BitmapWithPos> animation = new SaveAnimation<BitmapWithPos>(animationElement.Attribute("name").Value);
                        animation.Facing = animationElement.Attribute("facing").Value.LoadFacing();
                        model.animations.Add(animation);

                        foreach (XElement frameElement in animationElement.Elements("frame"))
                        {
                            SaveFrame<BitmapWithPos> frame = new SaveFrame<BitmapWithPos>(frameElement.Attribute("duration").Value.LoadInt());
                            animation.Frames.Add(frame);

                            foreach (XElement imageElement in frameElement.Elements("image"))
                            {
                                BitmapWithPos bitmapWithPos = new BitmapWithPos();
                                frame.Images.AddToFront(bitmapWithPos);
                                bitmapWithPos.pos = imageElement.Attribute("coord").Value.LoadPoint();
                                bitmapWithPos.name = imageElement.Attribute("name").Value;
                                bitmapWithPos.projectedOnto.AddRange(imageElement.Elements("projectedOnto").Select(e => model.blocks[e.Value.LoadInt()]));
                                bitmapWithPos.bitmap = bitmapManager[bitmapWithPos.name];
                            }
                        }
                    }
                }
            }
            return model;
        }
    }
}
