using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Mindstep.EasterEgg.Commons;
using Microsoft.Xna.Framework;
using Mindstep.EasterEgg.Commons.SaveLoad;
using SD = System.Drawing;
using Microsoft.Xna.Framework.Graphics;

namespace Mindstep.EasterEgg.MapEditor.Viewers
{
    public partial class TextureProjectionControl : BlockViewControl
    {
        private Texture2DWithPos textureBeingProjectedDown;





        public TextureProjectionControl()
        {
            InitializeComponent();
        }
        public override void Initialize(MainForm MainForm, BlockViewWrapperControl wrapper)
        {
            base.Initialize(MainForm, wrapper);

            MouseDown += new MouseEventHandler(TextureProjectionControl_MouseDown);
        }
        internal void enterTextureProjectionMode(Texture2DWithPos textureToProjectDown)
        {
            textureBeingProjectedDown = textureToProjectDown;
        }





        void TextureProjectionControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                SaveBlock hitBlock = getHitBlock(MainForm.ModelManager.SelectedModel.Blocks,
                    CoordinateTransform.ScreenToProjSpace(e.Location.ToXnaPoint(), Wrapper.Camera).ToSDPoint());
                if (!textureBeingProjectedDown.projectedOnto.Remove(hitBlock))
                {
                    textureBeingProjectedDown.projectedOnto.Add(hitBlock);
                }
                Invalidate();
            }
        }

        override protected void drawBlocks(BoundingBoxInt boundingBox)
        {
            foreach (SaveBlock saveBlock in MainForm.ModelManager.SelectedModel.Blocks)
            {
                if (textureBeingProjectedDown.projectedOnto.Contains(saveBlock))
                {
                    drawBlock(textureWireframeBack, boundingBox, blockTypeColor[saveBlock.Type], saveBlock.Position, .01f);
                    //TODO: cut out relevant piece of the texture and draw it inside the block
                    drawBlock(textureWireframe, boundingBox, blockTypeColor[saveBlock.Type], saveBlock.Position, -.01f);
                }
                else
                {
                    drawBlock(textureBlock, boundingBox, blockTypeColor[saveBlock.Type], saveBlock.Position);
                }
            }
        }

        private void buttonDone_Click(object sender, EventArgs e)
        {
            Wrapper.EditingMode = EditingMode.Texture;
        }
    }
}
