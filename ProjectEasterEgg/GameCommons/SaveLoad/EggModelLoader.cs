﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Microsoft.Xna.Framework.Graphics;
using System.Drawing;
using ICSharpCode.SharpZipLib.Zip;
using System.IO;

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

            using (ZipFile modelFile = new ZipFile(directoryPath + "/" + modelName + ".egg"))
            {
                using (PackagedBitmapsManager bitmapManager = new PackagedBitmapsManager(modelFile, "textures/"))
                {
                    ZipEntry xmlEntry = modelFile.GetEntry("model.xml");
                    Stream xmlStream =  modelFile.GetInputStream(xmlEntry);
                    XDocument doc = XDocument.Load(xmlStream);
                    XElement root = doc.Element("model");

                    // blocks
                    {
                        foreach (XElement blockElement in root.Element("blocks").Elements("block"))
                        {
                            Position pos = blockElement.Attribute("offset").Value.LoadPosition();

                            SaveBlock block = new SaveBlock(pos);
                            model.Blocks.Add(block);
                            block.Type = blockElement.Attribute("type").Value.LoadBlockType();
                            XAttribute scriptAttribute = blockElement.Attribute("script");
                            if (scriptAttribute != null)
                            {
                                block.Script = scriptAttribute.Value;
                            }
                        }
                    }

                    // imports
                    foreach (XElement modelElement in root.Element("imports").Elements("model"))
                    {
                        Position offset = modelElement.Attribute("offset").Value.LoadPosition();
                        model.SubModels.Add(new SaveSubModel<BitmapWithPos>(Load(directoryPath, modelElement.Value), offset));
                    }

                    // animations
                    foreach (XElement animationElement in root.Element("animations").Elements("animation"))
                    {
                        SaveAnimation<BitmapWithPos> animation = new SaveAnimation<BitmapWithPos>(animationElement.Attribute("name").Value);
                        animation.Facing = animationElement.Attribute("facing").Value.LoadFacing();
                        model.Animations.Add(animation);

                        foreach (XElement frameElement in animationElement.Elements("frame"))
                        {
                            SaveFrame<BitmapWithPos> frame = new SaveFrame<BitmapWithPos>(frameElement.Attribute("duration").Value.LoadInt());
                            animation.Frames.Add(frame);

                            foreach (XElement imageElement in frameElement.Elements("image"))
                            {
                                BitmapWithPos bitmapWithPos = new BitmapWithPos();
                                frame.Images.AddToFront(bitmapWithPos);
                                bitmapWithPos.Position = imageElement.Attribute("coord").Value.LoadPoint();
                                bitmapWithPos.name = imageElement.Attribute("name").Value;
                                bitmapWithPos.projectedOnto.AddRange(imageElement.Elements("projectedOnto").Select(e => model.Blocks[e.Value.LoadInt()]));
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
