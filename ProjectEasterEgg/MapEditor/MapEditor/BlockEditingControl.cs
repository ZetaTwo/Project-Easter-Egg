using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Microsoft.Xna.Framework.Graphics;
using Mindstep.EasterEgg.Commons.Graphic;
using Mindstep.EasterEgg.Commons.SaveLoad;
using Mindstep.EasterEgg.Commons;
using Microsoft.Xna.Framework;

namespace Mindstep.EasterEgg.MapEditor
{
    public partial class BlockEditingControl : BlockViewControl
    {
        private List<SaveBlock> selectedBlocks = new List<SaveBlock>();
        private ContextMenu blockContextMenu;

        private bool erasingBlocks;
        private bool drawingBlocks;

        private int currentLayer = 0;
        internal int CurrentLayer
        {
            get { return currentLayer; }
            set
            {
                currentLayer = value;
                Invalidate();
            }
        }





        public BlockEditingControl()
        {
            blockContextMenu = new ContextMenu(new MenuItem[]{
                new MenuItem("Edit Block Details", blockContextMenu_EditBlockDetails),
            });

            Settings = new Settings(Color.Black, BlockDrawState.Wireframe, .3f);
        }

        override public void Initialize(MainForm mainForm, BlockViewWrapperControl wrapper)
        {
            base.Initialize(mainForm, wrapper);

            MouseDown += new MouseEventHandler(BlockEditingControl_MouseDown);
            MouseUp += new MouseEventHandler(BlockEditingControl_MouseUp);
            MouseMove += new MouseEventHandler(BlockEditingControl_MouseMove);
            KeyDown += new KeyEventHandler(BlockEditingControl_KeyDown);
            MouseUpWithoutMoving += new MouseEventHandler(BlockEditingControl_MouseUpWithoutMoving);
        }
        





        private void BlockEditingControl_MouseDown(object sender, MouseEventArgs e)
        {
            selectedBlocks.Clear();
            if (e.Button == MouseButtons.Left)
            {
                if (existsBlockAt(e.Location))
                {
                    deleteBlockAt(e.Location);
                    drawingBlocks = false;
                    erasingBlocks = true;
                }
                else
                {
                    createBlockAt(e.Location);
                    drawingBlocks = true;
                    erasingBlocks = false;
                }
            }
            else if (e.Button == MouseButtons.XButton1)
            {
                CurrentLayer--;
            }
            else if (e.Button == MouseButtons.XButton2)
            {
                CurrentLayer++;
            }
        }

        private void BlockEditingControl_MouseUp(object sender, MouseEventArgs e)
        {
            drawingBlocks = erasingBlocks = false;
        }

        private void BlockEditingControl_MouseMove(object sender, MouseEventArgs e)
        {
            MainForm.SetDisplayCoords(CoordinateTransform.ScreenToObjectSpace(e.Location.ToXnaPoint(), Wrapper.Camera, currentLayer).ToPosition());

            if (drawingBlocks)
            {
                createBlockAt(e.Location);
            }
            else if (erasingBlocks)
            {
                deleteBlockAt(e.Location);
            }
        }

        private void BlockEditingControl_MouseUpWithoutMoving(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Point mousePosInProjSpace = CoordinateTransform.ScreenToProjSpace(e.Location.ToXnaPoint(), Wrapper.Camera);
                SaveBlock hitBlock = getHitBlock(MainForm.CurrentModel.blocks, mousePosInProjSpace.ToSDPoint());

                if (hitBlock != null)
                {
                    selectedBlocks.Clear();
                    selectedBlocks.Add(hitBlock);
                    Invalidate();
                    blockContextMenu.Show(this, e.Location);
                    MainForm.UpdatedThings();
                    Invalidate();
                }
                //updateSelectedBlocks(e.Location, getClickOperation());
                //if (selectedBlocks.Count != 0)
                //{
                //    blockContextMenu.Show(this, e.Location);
                //}
            }
        }

        private void BlockEditingControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
            {
                CurrentLayer++;
            }
            if (e.KeyCode == Keys.S)
            {
                CurrentLayer--;
            }
        }

        private void blockContextMenu_EditBlockDetails(object sender, EventArgs e)
        {
            new BlockDetailsForm(selectedBlocks, lastMouseLocation);
            selectedBlocks.Clear();
            MainForm.UpdatedThings();
            Invalidate();
        }





        private void createBlockAt(System.Drawing.Point mouseLocation)
        {
            Position pos = posUnderPointInCurrentLayer(mouseLocation);
            if (!MainForm.CurrentModel.blocks.Any(block => block.Position == pos))
            {
                MainForm.CurrentModel.blocks.Add(new SaveBlock(pos));
            }
            MainForm.UpdatedThings();
            Invalidate();
        }

        private void deleteBlockAt(System.Drawing.Point mouseLocation)
        {
            Position pos = posUnderPointInCurrentLayer(mouseLocation);
            foreach (SaveAnimation<Texture2DWithPos> animation in MainForm.CurrentModel.animations)
            {
                foreach (SaveFrame<Texture2DWithPos> frame in animation.Frames)
                {
                    foreach (Texture2DWithPos tex in frame.Images.BackToFront())
                    {
                        tex.projectedOnto.RemoveAll(block => block.Position == pos);
                    }
                }
            }
            MainForm.CurrentModel.blocks.RemoveAll(block => block.Position == pos);
            MainForm.UpdatedThings();
            Invalidate();
        }

        private bool existsBlockAt(System.Drawing.Point mouseLocation)
        {
            Position pos = posUnderPointInCurrentLayer(mouseLocation);
            return MainForm.CurrentModel.blocks.Any(block => block.Position == pos);
        }

        private Position posUnderPointInCurrentLayer(System.Drawing.Point mouseLocation)
        {
            return CoordinateTransform.ScreenToObjectSpace(
                mouseLocation.ToXnaPoint(), Wrapper.Camera, CurrentLayer).ToPosition();
        }

        private SaveBlock getHitBlockInCurrentLayer(System.Drawing.Point mouseLocation)
        {
            Point mousePosInProjSpace = CoordinateTransform.ScreenToProjSpace(mouseLocation.ToXnaPoint(), Wrapper.Camera);

            return getHitBlock(
                MainForm.CurrentModel.blocks.Where(block => block.Position.Z == CurrentLayer),
                mousePosInProjSpace.ToSDPoint());
        }

        private SaveBlock getHitBlock(IEnumerable<SaveBlock> outOf, System.Drawing.Point pointInProjSpace)
        {
            BoundingBoxInt boundingBox = new BoundingBoxInt(outOf.ToPositions());

            foreach (SaveBlock block in outOf.OrderBy(block => boundingBox.getRelativeDepthOf(block.Position)))
            {
                System.Drawing.Region blockRegion = BlockRegions.WholeBlock.Offset(
                    CoordinateTransform.ObjectToProjectionSpace(block.Position).ToXnaPoint().Add(
                    Constants.blockDrawOffset.ToXnaPoint()));
                if (blockRegion.IsVisible(pointInProjSpace))
                {
                    return block;
                }
            }
            return null;
        }





        override protected void drawGrid(BoundingBoxInt boundingBox)
        {
            for (int x = boundingBox.Min.X; x <= boundingBox.Max.X; x++)
            {
                for (int y = boundingBox.Min.Y; y <= boundingBox.Max.Y; y++)
                {
                    drawBlock(textureGridStriped, boundingBox, Color.White, new Position(x, y, -1), -0.0001f);
                    drawBlock(textureGridFilled, boundingBox, Color.Green, new Position(x, y, CurrentLayer - 1), 0.0001f);
                }
            }
        }

        override protected BoundingBoxInt getBoundingBox()
        {
            BoundingBoxInt boundingBox = base.getBoundingBox();
            boundingBox.addPos(new Position(0, 0, CurrentLayer));
            return boundingBox;
        }

        override protected void drawBlocks(BoundingBoxInt boundingBox)
        {
            foreach (SaveBlock saveBlock in MainForm.CurrentModel.blocks)
            {
                switch (Math.Sign(saveBlock.Position.Z - CurrentLayer))
                {
                    case 1: //above
                        drawBlock(blockDrawStateTexture[Settings.blockDrawState], boundingBox, blockTypeColor[saveBlock.type], saveBlock.Position);
                        break;
                    case 0: //at
                        drawBlock(textureDrawing, boundingBox, blockTypeColor[saveBlock.type], saveBlock.Position);
                        break;
                    case -1: //below
                        drawBlock(textureBlock, boundingBox, blockTypeColor[saveBlock.type], saveBlock.Position);
                        break;
                }
            }
        }
    }
}
