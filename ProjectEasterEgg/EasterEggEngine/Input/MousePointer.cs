using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Mindstep.EasterEgg.Engine.Input
{
    class MousePointer : GameEntity
    {
        Vector2 position;
        public Vector2 Position
        {
            get { return position; }
        }

        public MousePointer(EggEngine engine)
            : base(engine)
        { }

        public override void Draw()
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            position += Engine.Input.MouseDelta;
        }
    }

    class MousePointerScript : Script
    {
        MousePointer pointer;

        public MousePointerScript(MousePointer _pointer)
        {
            pointer = _pointer;
        }

        public override IEnumerator<float> ScriptContent()
        {
            pointer.Update();

            yield return 0;
        }
    }
}
