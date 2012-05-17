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

namespace Mindstep.EasterEgg.Game.Game
{
    class GameWorld : World, IUpdate
    {
        private GameCharacter character;

        public override void Initialize(EggEngine _engine)
        {
            base.Initialize(_engine);

            //Add game content here

            GameModelDTO mapDTO = Engine.Content.Load<GameModelDTO>("Models/seventhmodel");
            CurrentMap = new GameMap(mapDTO, Engine);

            GameModelDTO characterDTO = Engine.Content.Load<GameModelDTO>("Models/character1");
            character = new GameCharacter(characterDTO, Engine);

            CurrentMap.Spawn(character, "spawn");
            CurrentMap.Camera.Initialize(Engine);
            CurrentMap.Camera.Following = character;
            CurrentMap.AddUpdate(character);

            //character.

            pointer = new MousePointer();
            pointer.Initialize(Engine);
            AddUpdate(pointer);
            AddDraw(pointer);
        }

        public new void Update(GameTime gameTime)
        {
            if (Engine.Input.Mouse.ButtonPressed(MouseButton.Left))
            {
                Engine.Physics.ClickWorld(Engine.Input.Mouse.LocationInProjSpace, CurrentMap.Camera, BlockAction.INTERACT);
            }

            if (Engine.Input.Mouse.ButtonPressed(MouseButton.Right))
            {
                Engine.Physics.ClickWorld(Engine.Input.Mouse.LocationInProjSpace, CurrentMap.Camera, BlockAction.INSPECT);
            }
            base.Update(gameTime);
        }
    }
}
