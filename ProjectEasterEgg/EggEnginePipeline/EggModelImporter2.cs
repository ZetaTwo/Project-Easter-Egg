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

                        foreach (XElement imageElement in frameElement.Elements("image"))
                        {
                            string name = imageElement.Attribute("name").Value;
                            Point coord = imageElement.Attribute("coord").Value.LoadPoint();
                            IEnumerable<GameBlockDTO> blocksProjectedOnto =
                                imageElement.Elements("projectedOnto").Select(e => blocks[e.Value.LoadInt()]);

                            SD.Bitmap image = bitmapManager[name];
                            ImageWithPos imageWithPos = new ImageWithPos(image, coord);

                            foreach (GameBlockDTO block in blocksProjectedOnto)
                            {
                                FrameDTO frame = new FrameDTO();
                                frame.Duration = duration;
                                frame.bitmapData = eat(imageWithPos, block.Position + bounds.Min); // we must add min here
                                // because we translate the block positions with -min relative to the save file
                                block.Animations[animationName].Frames.Add(frame);
                            }
                        }
                    }
                }
            }
            return gameMap;
        }

        /// <summary>
        /// used together with System.Drawing.Graphics.DrawPolygon to draw the outline of a block
        /// </summary>
        private static readonly Point[] blockBorder = new Point[]{
            new Point(61,0),
            new Point(124,31),
            new Point(124,108),
            new Point(61,139),
            new Point(0,108),
            new Point(0,31),
        };
        /// <summary>
        /// used together with System.Drawing.Graphics.DrawPolygon to draw the outline of a block
        /// </summary>
        private static readonly Point[] blockInnerBorder = new Point[]{
            new Point(123,31),
            new Point(62,62),
            new Point(62,138),
            new Point(62,62),
            new Point(1,32),
        };
        /// <summary>
        /// used together with System.Drawing.Graphics.FillPolygon to fill a block
        /// </summary>
        private static readonly Point[] blockFill = new Point[]{
            new Point(61,-1),
            new Point(125,31),
            new Point(125,108),
            new Point(61,140),
            new Point(62,140),
            new Point(-1,108),
            new Point(-1,31),
            new Point(62,-1),
        };

        private static byte[] eat(ImageWithPos image, Position map)
        {
            Point projCoords = CoordinateTransform.ObjectToProjectionSpace(map).ToXnaPoint();
            SD.Bitmap newBitmap = new SD.Bitmap(Constants.PROJ_WIDTH, Constants.PROJ_HEIGHT);
            using (SD.Graphics graphics = SD.Graphics.FromImage(newBitmap))
            {
                Point p = image.pos.Subtract(projCoords);
                graphics.Clear(SD.Color.Transparent);
                graphics.FillPolygon(SD.Brushes.White, offset(blockFill, Point.Zero));
                graphics.DrawLines(SD.Pens.Gray, offset(blockInnerBorder, Point.Zero));
                graphics.DrawPolygon(SD.Pens.Black, offset(blockBorder, Point.Zero));
                graphics.DrawImage(image.bitmap, p.ToSDPoint());
                //TODO: remove parts of the image that are outside the block
            }
            BitmapData data = newBitmap.LockBits(new SD.Rectangle(0, 0, newBitmap.Width, newBitmap.Height), ImageLockMode.ReadOnly, SD.Imaging.PixelFormat.Format32bppArgb);
            byte[] bytes = new byte[data.Height * data.Stride];
            Marshal.Copy(data.Scan0, bytes, 0, bytes.Length);

            //TODO: clear the block from the original image
            //using (SD.Graphics graphics = SD.Graphics.FromImage(image.bitmap))
            //{
            //    graphics.FillPolygon(SD.Brushes.Transparent, offset(blockFill, projCoords));
            //}

            return bytes;
        }

        private static SD.Point[] offset(Point[] points, Point offset)
        {
            SD.Point[] newPoints = new SD.Point[points.Length];
            for (int i = 0; i < points.Length; i++)
            {
                newPoints[i] = points[i].Add(offset).ToSDPoint();
            }
            return newPoints;
        }
    }
}
