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
    public class EggModelImporter : ContentImporter<GameMapDTO>
    {
        public override GameMapDTO Import(string filename, ContentImporterContext context)
        {
            GameMapDTO gameMap = new GameMapDTO();
            SaveModel<BitmapWithPos> model = EggModelLoader.Load(filename);
            
            // imports
            foreach (SaveSubModel<BitmapWithPos> subModels in model.subModels)
            {
                //TODO: Add support for sub models
            }

            List<GameBlockDTO> gameBlocks = new List<GameBlockDTO>();
            BoundingBoxInt bounds = new BoundingBoxInt(model.blocks.ToPositions());
            //blocks
            {
                gameMap.Max = bounds.Max - bounds.Min;
                gameMap.WorldMatrix = Creators.CreateWorldMatrix<GameBlockDTO>(gameMap.Max + Position.One);

                foreach (SaveBlock block in model.blocks)
                {
                    GameBlockDTO gameBlock = new GameBlockDTO();
                    gameBlocks.Add(gameBlock);
                    gameBlock.Position = block.Position - bounds.Min;
                    gameBlock.scriptName = block.script;
                    gameBlock.Type = block.type;
                    Position pos = gameBlock.Position;
                    //TODO: the physics matrix shouldn't be defined here
                    gameMap.WorldMatrix[pos.X][pos.Y][pos.Z] = gameBlock;
                }
            }

            //animations
            foreach (SaveAnimation<BitmapWithPos> animation in model.animations)
            {
                foreach (GameBlockDTO block in gameBlocks)
                {
                    block.Animations[animation.Name] = new AnimationDTO(animation.Name, animation.Facing);
                }

                foreach (SaveFrame<BitmapWithPos> frame in animation.Frames)
                {
                    foreach (GameBlockDTO block in gameBlocks)
                    {
                        block.Animations[animation.Name].Frames.Add(new FrameDTO(frame.Duration));
                    }

                    foreach (BitmapWithPos bitmap in frame.Images.FrontToBack())
                    {
                        foreach (GameBlockDTO block in gameBlocks.GetRange(model.blocks.IndexOf(bitmap.projectedOnto)))
                        {
                            FrameDTO blockFrame = block.Animations[animation.Name].Frames.Last();
                            Point projCoords = CoordinateTransform.ObjectToProjSpace(block.Position + bounds.Min).ToXnaPoint();
                            blockFrame.getGraphics().eat(bitmap.bitmap, projCoords.Subtract(bitmap.pos));
                            blockFrame.updateDataToBeSaved();
                        }
                    }
                }
            }
            return gameMap;
        }
    }
}
