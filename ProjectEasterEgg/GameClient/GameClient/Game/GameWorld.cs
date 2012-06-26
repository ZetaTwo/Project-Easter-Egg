using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mindstep.EasterEgg.Engine;
using Mindstep.EasterEgg.Engine.Game;
using Mindstep.EasterEgg.Engine.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Mindstep.EasterEgg.Commons.DTO;
using Mindstep.EasterEgg.Commons.Game;
using Mindstep.EasterEgg.Commons;
using Mindstep.EasterEgg.Engine.Interfaces;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Mindstep.EasterEgg.Game.Game
{
    class GameWorld : World, IUpdate
    {
        private GameMovableModel character;

        public override void Initialize(EggEngine engine)
        {
            base.Initialize(engine);

            //Add game content here

            GameModelDTO mapDTO = Engine.Content.Load<GameModelDTO>("Models/seventhmodel");
            CurrentMap = new GameMap(mapDTO, Engine);

            GameModelDTO characterDTO = Engine.Content.Load<GameModelDTO>("Models/character1");
            character = new GameMovableModel(characterDTO, Engine);

            CurrentMap.Spawn(character, "spawn");
            CurrentMap.Camera.Initialize(Engine);
            CurrentMap.Camera.Following = character;
            CurrentMap.AddUpdate(character);

            //character.

            pointer = new MousePointer();
            pointer.Initialize(Engine);
            pointer.Cursor = Engine.Content.Load<Texture2D>("Cursors/magnifying-glass2").ToCursor(new Point(9,9));
            pointer.Cursor = System.Windows.Forms.Cursors.Cross;
            AddUpdate(pointer);
            AddDraw(pointer);
        }

        public override void Update(GameTime gameTime)
        {
            //if (currentBlock.Interactable)
            //{
            //    currentBlock.Interact(action);
            //    return;
            //}
            //else if (currentBlock.Type != BlockType.EMPTY)
            //{
            //    ClickBlock(currentPosition, entry);
            //    return;
            //}

            if (Engine.Input.Mouse.ButtonPressed(MouseButton.Left))
            {
                Engine.Physics.GetBlocksUnderPoint(Engine.Input.Mouse.LocationInProjSpace);
            }

            if (Engine.Input.Mouse.ButtonPressed(MouseButton.Right))
            {
                Engine.Physics.GetBlocksUnderPoint(Engine.Input.Mouse.LocationInProjSpace);
            }
            base.Update(gameTime);
        }
    }
}
