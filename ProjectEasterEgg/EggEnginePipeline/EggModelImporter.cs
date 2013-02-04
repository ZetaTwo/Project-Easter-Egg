using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using System.IO.Packaging;
using System.Xml;
using Mindstep.EasterEgg.Commons;
using System.Windows.Media.Imaging;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Mindstep.EasterEgg.Commons.DTO;
using Mindstep.EasterEgg.Commons.SaveLoad;
using SD = System.Drawing;

using System.Drawing.Imaging;
using System.Xml.Linq;
using Mindstep.EasterEgg.Commons.Physics;

namespace EggEnginePipeline
{
    /// <summary>
    /// This class will be instantiated by the XNA Framework Content Pipeline
    /// to import a file from disk into the specified type, TImport.
    /// 
    /// This should be part of a Content Pipeline Extension Library project.
    /// 
    /// TODO: change the ContentImporter attribute to specify the correct file
    /// extension, display name, and default processor for this importer.
    /// </summary>
    [ContentImporter(".egg", DisplayName = "Egg Model Importer", DefaultProcessor = "PassThroughProcessor")]
    public class EggModelImporter : ContentImporter<GameModelDTO>
    {
        public override GameModelDTO Import(string filename, ContentImporterContext context)
        {
            GameModelDTO model = new GameModelDTO();
            SaveModel<BitmapWithPos> saveModel = EggModelLoader.Load(filename);
            
            // imports
            foreach (SaveSubModel<BitmapWithPos> subModels in saveModel.SubModels)
            {
                //TODO: Add support for sub models
            }

            BoundingBoxInt bounds = new BoundingBoxInt(saveModel.Blocks.ToPositions());
            //blocks
            {
                foreach (SaveBlock block in saveModel.Blocks)
                {
                    if (block.Type == BlockType.SPAWN_LOCATION)
                    {
                        model.spawnLocations.Add(block.Script, block.Position);
                    }
                    else
                    {
                        GameBlockDTO gameBlock = new GameBlockDTO();
                        gameBlock.Position = block.Position;
                        gameBlock.scriptName = block.Script;
                        gameBlock.Type = block.Type;

                        model.blocks.Add(gameBlock);
                    }
                }

                saveModel.Blocks.RemoveAll(block => block.Type == BlockType.SPAWN_LOCATION);
            }

            //Debugger.Launch();

            //animations
            foreach (SaveAnimation<BitmapWithPos> saveAnimation in saveModel.Animations)
            {
                AnimationDTO animation = new AnimationDTO(saveAnimation.Name, saveAnimation.Facing);

                foreach (SaveFrame<BitmapWithPos> saveFrame in saveAnimation.Frames)
                {
                    FrameDTO frame = new FrameDTO(saveFrame.Duration);

                    foreach (BitmapWithPos bitmap in saveFrame.Images.BackToFront())
                    {
                        for (int i=0; i< model.blocks.Count; i++) //TODO: temp
                        {
                            if (!frame.textures.ContainsKey(i))
                            {
                                frame.textures.Add(i, new SaveBlockImage());
                            }
                        }
                        foreach (SaveBlock saveBlock in bitmap.projectedOnto) {
                            int index = saveModel.Blocks.IndexOf(saveBlock);
                            GameBlockDTO block = model.blocks[index];

                            if (!frame.textures.ContainsKey(index)) {
                                frame.textures.Add(index, new SaveBlockImage());
                            }
                            frame.textures[index].stealPixelsFrom(bitmap, block.Position);
                        }
                    }
                    animation.Frames.Add(frame);
                }
                model.animations.Add(animation);
            }
            model.animations.ForEach(animation => animation.Frames.ForEach(frame => frame.updateDataToBeSaved()));

            model.min = bounds.Min;
            model.max = bounds.Max;
            return model;
        }
    }
}
