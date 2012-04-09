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

// TODO: replace this with the type you want to import.
using TImport = Mindstep.EasterEgg.Commons.DTO.GameMapDTO;
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
    public class EggModelImporter : ContentImporter<TImport>
    {
        public override TImport Import(string filename, ContentImporterContext context)
        {
            TImport gameMap = new TImport();
            List<GameBlockDTO> blocks = new List<GameBlockDTO>();

            Package modelFile = Package.Open(filename);

            PackagePart modelIndex = modelFile.GetPart(new Uri("/model.xml", UriKind.Relative));
            XmlTextReader xmlReader = new XmlTextReader(modelIndex.GetStream());
            //XDocument doc = doc.
            //Debugger.Launch();
            xmlReader.MoveToContent();
            Position min = Position.One; //will be replaced when reading bounds
            while (xmlReader.Read())
            {
                if (xmlReader.NodeType == XmlNodeType.Element)
                {
                    switch (xmlReader.Name)
                    {
                        case "bounds":
                            xmlReader.ReadToDescendant("min"); //Read min
                            min = xmlReader.ReadElementContentAsString().LoadPosition();

                            xmlReader.ReadToNextSibling("max"); //Read max
                            Position max = xmlReader.ReadElementContentAsString().LoadPosition();

                            gameMap.Max = max - min;

                            gameMap.WorldMatrix = Creators.CreateWorldMatrix<GameBlockDTO>(gameMap.Max + Position.One);

                            break;
                        case "imports":
                            //TODO: Add suport for sub models
                            XmlReader importsReader = xmlReader.ReadSubtree();
                            break;
                        case "blocks":
                            XmlReader blocksReader = xmlReader.ReadSubtree();
                            while (blocksReader.Read())
                            {
                                switch (blocksReader.Name)
                                {
                                    case "block":
                                        //xmlReader.MoveToAttribute("offset");
                                        Position pos = blocksReader.GetAttribute("offset").LoadPosition() - min;

                                        GameBlockDTO block = new GameBlockDTO();
                                        gameMap.WorldMatrix[pos.X][pos.Y][pos.Z] = block;
                                        blocks.Add(block);
                                        block.Position = pos;
                                        //block.scriptName = blocksReader.GetAttribute("script");
                                        block.scriptName = "Example";
                                        block.Type = BlockType.SOLID;
                                        break;
                                    default:
                                        break;
                                }
                            }
                            break;
                        case "animations":
                            XmlReader animationsReader = xmlReader.ReadSubtree();
                            while (animationsReader.Read())
                            {
                                if (animationsReader.Name == "animations" ||
                                    animationsReader.Name == "")
                                {
                                    continue;
                                }
                                else if (animationsReader.Name != "animation")
                                {
                                    throw new XmlTagException("animations", "animation", animationsReader.Name);
                                }

                                string animationName = animationsReader.GetAttribute("name");

                                foreach (GameBlockDTO block in blocks)
                                {
                                    if (!block.Animations.ContainsKey(animationName))
                                    {
                                        //block.Animations.Add(animationName, new AnimationDTO(animationName));
                                        block.Animations[animationName] = new AnimationDTO(animationName);
                                    }
                                }

                                XmlReader framesReader = animationsReader.ReadSubtree();
                                while (framesReader.Read())
                                {
                                    if (framesReader.Name == "animation" ||
                                        framesReader.Name == "")
                                    {
                                        continue;
                                    }
                                    else if (framesReader.Name != "frame")
                                    {
                                        throw new XmlTagException("animation", "frame", framesReader.Name);
                                    }

                                    ImageWithPos image = combineTextures(getTextures(framesReader.ReadSubtree(), modelFile));
                                    int duration = -5;// int.Parse(framesReader.GetAttribute("duration"));

                                    foreach (GameBlockDTO block in blocks)
                                    {
                                        FrameDTO frame = new FrameDTO();
                                        frame.Duration = duration;
                                        frame.bitmapData = eat(image, block.Position+min);
                                        //frame.bitmap = eat(image, block.Position);
                                        //if (!block.Animations.ContainsKey(animationName))
                                        //{
                                        //    block.Animations.Add(animationName, new AnimationDTO(animationName));
                                        //}
                                        block.Animations[animationName].Frames.Add(frame);
                                    }
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
            }

            xmlReader.Close();
            modelFile.Close();

            return gameMap;
        }

        /// <summary>
        /// used together with System.Drawing.Graphics.DrawPolygon to draw the outline of a block
        /// </summary>
        private Point[] blockBorder = new Point[]{
            new Point(61,0),
            new Point(124,31),
            new Point(124,108),
            new Point(61,139),
            new Point(0,108),
            new Point(0,31),
        };
        /// <summary>
        /// used together with System.Drawing.Graphics.FillPolygon to fill a block
        /// </summary>
        private Point[] blockFill = new Point[]{
            new Point(61,-1),
            new Point(125,31),
            new Point(125,108),
            new Point(61,140),
            new Point(62,140),
            new Point(-1,108),
            new Point(-1,31),
            new Point(62,-1),
        };
        private byte[] eat(ImageWithPos image, Position map)
        {
            Point projCoords = CoordinateTransform.ObjectToProjectionSpace(map).ToXnaPoint();
            SD.Bitmap newBitmap = new SD.Bitmap(Constants.PROJ_WIDTH, Constants.PROJ_HEIGHT);
            using (SD.Graphics graphics = SD.Graphics.FromImage(newBitmap))
            {
                Point p = image.pos.Subtract(projCoords);
                graphics.Clear(SD.Color.Transparent);
                graphics.FillPolygon(SD.Brushes.White, offset(blockFill, new Point()));
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

        private SD.Point[] offset(Point[] points, Point offset)
        {
            SD.Point[] newPoints = new SD.Point[points.Length];
            for (int i = 0; i < points.Length; i++)
            {
                newPoints[i] = points[i].Add(offset).ToSDPoint();
            }
            return newPoints;
        }

        //TODO: this won't be needed if we restructure the format to say which textures
        //should be projected onto which blocks
        private ImageWithPos combineTextures(List<ImageWithPos> images)
        {
            Rectangle bounds = getAllBounds(images);
            SD.Bitmap bitmap = new SD.Bitmap(bounds.Width, bounds.Height);
            using (SD.Graphics graphics = SD.Graphics.FromImage(bitmap))
            {
                graphics.Clear(SD.Color.Transparent);
                foreach (ImageWithPos image in images)
                {
                    graphics.DrawImageUnscaled(image.bitmap, image.pos.X-bounds.X, image.pos.Y-bounds.Y);
                }
            }

            return new ImageWithPos(bitmap, bounds.Location);
        }

        private List<ImageWithPos> getTextures(XmlReader textureReader, Package modelFile)
        {
            List<ImageWithPos> images = new List<ImageWithPos>();
            while (textureReader.Read())
            {
                if (textureReader.Name == "frame" ||
                    textureReader.Name == "")
                {
                    continue;
                }
                else if (textureReader.Name != "texture")
                {
                    throw new XmlTagException("frame", "texture", textureReader.Name);
                }

                string textureName = textureReader.GetAttribute("name");
                Stream textureStream = getTextureStream(textureName, modelFile);

                Point textureCoords = textureReader.GetAttribute("coord").LoadPoint();
                images.Add(new ImageWithPos(textureStream, textureCoords));
            }
            return images;
        }

        private static Rectangle getAllBounds(List<ImageWithPos> images)
        {
            if (images.Count == 0)
            {
                return Rectangle.Empty;
            }
            
            Rectangle allBounds = images[0].bounds;
            foreach (ImageWithPos image in images)
            {
                allBounds = Rectangle.Union(allBounds, image.bounds);
            }
            return allBounds;
        }

        private static Stream getTextureStream(string textureName, Package modelFile)
        {
            return modelFile.GetPart(new Uri("/textures/"+textureName, UriKind.Relative)).GetStream();
        }
    }

    class ImageWithPos
    {
        public readonly SD.Bitmap bitmap;
        public readonly Point pos;

        public Rectangle bounds { get { return new Rectangle(pos.X, pos.Y, bitmap.Width, bitmap.Height); } }

        public ImageWithPos(Stream stream, Point pos)
            : this(new SD.Bitmap(stream), pos)
        { }

        public ImageWithPos(SD.Bitmap image, Point pos)
        {
            this.bitmap = image;
            this.pos = pos;
        }
    }

    class XmlTagException : InvalidContentException
    {
        public XmlTagException(string parent, string allowed, string contained)
            : base("<"+parent+"> tags can only contain <"+allowed+"> tags, but it contained: <"+contained+"> tag!")
        { }
    }
}
