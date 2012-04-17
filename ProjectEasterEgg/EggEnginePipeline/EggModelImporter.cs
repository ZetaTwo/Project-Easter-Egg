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
            //Debugger.Launch();
            GameMapDTO gameMap = new GameMapDTO();
            using (Package modelFile = Package.Open(filename))
            {
                BitmapManager bitmapManager = new BitmapManager(modelFile, "/textures/");

                PackagePart modelXML = modelFile.GetPart(new Uri("/model.xml", UriKind.Relative));
                XDocument doc = XDocument.Load(modelXML.GetStream());
                XElement root = doc.Element("model");

                List<GameBlockDTO> blocks = new List<GameBlockDTO>();
                BoundingBoxInt bounds;

                // blocks
                {
                    foreach (XElement blockElement in root.Element("blocks").Elements("block"))
                    {
                        Position pos = blockElement.Attribute("offset").Value.LoadPosition();

                        GameBlockDTO block = new GameBlockDTO();
                        blocks.Add(block);
                        block.Position = pos;
                        block.Type = blockElement.Attribute("type").Value.LoadBlockType();
                        XAttribute scriptAttribute = blockElement.Attribute("script");
                        if (scriptAttribute != null)
                        {
                            block.scriptName = Constants.SCRIPT_BLOCK_PREFIX + scriptAttribute.Value;
                        }
                    }
                    bounds = new BoundingBoxInt(blocks.ToPositions());
                    gameMap.Max = bounds.Max - bounds.Min;
                    gameMap.WorldMatrix = Creators.CreateWorldMatrix<GameBlockDTO>(gameMap.Max + Position.One);
                    foreach (GameBlockDTO block in blocks)
                    {
                        block.Position -= bounds.Min;
                        Position pos = block.Position;
                        //TODO: the physics matrix shouldn't be defined here
                        gameMap.WorldMatrix[pos.X][pos.Y][pos.Z] = block;
                    }
                }
                
                // imports
                foreach (XElement modelElement in root.Element("imports").Elements("model"))
                {
                    //TODO: Add suport for sub models
                }
                
                // animations
                foreach (XElement animationElement in root.Element("animations").Elements("animation"))
                {
                    string animationName = animationElement.Attribute("name").Value;
                    Facing facing = animationElement.Attribute("facing").Value.LoadFacing();

                    foreach (GameBlockDTO block in blocks)
                    {
                        block.Animations[animationName] = new AnimationDTO(animationName);
                    }

                    foreach (XElement frameElement in animationElement.Elements("frame"))
                    {
                        int duration = frameElement.Attribute("duration").Value.LoadInt();

                        blocks.ForEach(block => block.Animations[animationName].Frames.Add(new FrameDTO(duration)));
                        
                        foreach (XElement imageElement in frameElement.Elements("image"))
                        {
                            string name = imageElement.Attribute("name").Value;
                            Point imageCoord = imageElement.Attribute("coord").Value.LoadPoint();
                            IEnumerable<GameBlockDTO> blocksProjectedOnto =
                                imageElement.Elements("projectedOnto").Select(e => blocks[e.Value.LoadInt()]);

                            SD.Bitmap image = bitmapManager[name];

                            foreach (GameBlockDTO block in blocksProjectedOnto)
                            {
                                FrameDTO frame = block.Animations[animationName].Frames.Last();
                                Point projCoords = CoordinateTransform.ObjectToProjSpace(block.Position + bounds.Min).ToXnaPoint();
                                frame.getGraphics().eat(image, projCoords.Subtract(imageCoord));
                                frame.updateDataToBeSaved();
                            }
                        }
                    }
                }
            }
            return gameMap;
        }
    }
}
