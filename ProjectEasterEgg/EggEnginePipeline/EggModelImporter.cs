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
using Mindstep.EasterEgg.Engine.Game;

// TODO: replace this with the type you want to import.
using TImport = EggEnginePipeline.GameMapDTO;


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
            //System.Diagnostics.Debugger.Launch();
            TImport gameMap = new TImport();

            Package modelFile = Package.Open(filename);

            PackagePart modelIndex = modelFile.GetPart(new Uri("/model.xml", UriKind.Relative));
            XmlTextReader xmlReader = new XmlTextReader(modelIndex.GetStream());

            xmlReader.MoveToContent();
            while (xmlReader.Read())
            {
                if (xmlReader.NodeType == XmlNodeType.Element)
                {
                    switch (xmlReader.Name)
                    {
                        case "bounds":
                            xmlReader.ReadToDescendant("min"); //Read min
                            gameMap.Min = PositionFromString(xmlReader.ReadElementContentAsString());

                            xmlReader.ReadToNextSibling("max"); //Read max
                            gameMap.Max = PositionFromString(xmlReader.ReadElementContentAsString());

                            gameMap.WorldMatrix = GameMap.CreateWorldMatrix<GameBlockDTO>(gameMap.Max - gameMap.Min + new Position(1, 1, 1));

                            break;
                        case "imports":
                            //TODO: Add suport for sub models
                            XmlReader importsReader = xmlReader.ReadSubtree();
                            break;
                        case "animations":
                            //TODO: Add support for textures
                            XmlReader animationsReader = xmlReader.ReadSubtree();
                            break;
                        case "blocks":
                            XmlReader blocksReader = xmlReader.ReadSubtree();
                            while (blocksReader.Read())
                            {
                                switch (blocksReader.Name)
                                {
                                    case "block":
                                        //xmlReader.MoveToAttribute("offset");
                                        Position pos = - gameMap.Min + PositionFromString(blocksReader.GetAttribute("offset"));

                                        gameMap.WorldMatrix[pos.X][pos.Y][pos.Z] = new GameBlockDTO();
                                        gameMap.WorldMatrix[pos.X][pos.Y][pos.Z].Position = pos;
                                        gameMap.WorldMatrix[pos.X][pos.Y][pos.Z].scriptName = blocksReader.GetAttribute("script");
                                        gameMap.WorldMatrix[pos.X][pos.Y][pos.Z].Type = BlockType.SOLID;
                                        gameMap.WorldMatrix[pos.X][pos.Y][pos.Z].Texture = "block31";
                                        break;
                                    default:
                                        break;
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

        private Position PositionFromString(string positionString)
        {
            string[] pos = positionString.Split(' ');
            return new Position(int.Parse(pos[0]), int.Parse(pos[1]), int.Parse(pos[2]));

        }
    }
}
