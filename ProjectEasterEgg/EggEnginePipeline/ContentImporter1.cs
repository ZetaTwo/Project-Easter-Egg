using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;

// TODO: replace this with the type you want to import.
using TImport = Mindstep.EasterEgg.Engine.Game.GameMap;
using System.IO.Packaging;
using System.Xml;
using Mindstep.EasterEgg.Commons;
using Mindstep.EasterEgg.Engine.Game;

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
    [ContentImporter(".egg", DisplayName = "Egg Model Importer", DefaultProcessor = "AbcProcessor")]
    public class ContentImporter1 : ContentImporter<TImport>
    {
        public override TImport Import(string filename, ContentImporterContext context)
        {
            TImport gameMap = null;

            Package modelFile = Package.Open(filename);

            PackagePart modelIndex = modelFile.GetPart(new Uri("/model.xml", UriKind.Relative));
            XmlTextReader xmlReader = new XmlTextReader(modelIndex.GetStream());

            while (xmlReader.Read())
            {
                switch (xmlReader.Name)
                {
                    case "bounds":
                        xmlReader.ReadToDescendant("min"); //Read min
                        Position min = PositionFromString(xmlReader.ReadElementContentAsString());
                        
                        xmlReader.ReadToDescendant("max"); //Read max
                        Position max = PositionFromString(xmlReader.ReadElementContentAsString());

                        gameMap = new TImport(min, max);
                        break;
                    case "imports":
                        //TODO: Add suport for sub models
                        break;
                    case "animations":
                        //TODO: Add support for textures
                        break;
                    case "blocks":
                        XmlReader blocksReader = xmlReader.ReadSubtree();
                        while(blocksReader.Read())
                        {
                            xmlReader.MoveToAttribute("offset");
                            Position pos = PositionFromString(xmlReader.ReadContentAsString());

                            gameMap.WorldMatrix[pos.X][pos.Y][pos.Z] = new GameBlock(BlockType.SOLID, pos);
                        }
                        break;
                    default:
                        break;
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
