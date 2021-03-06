﻿using System;
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
using SDPoint = System.Drawing.Point;

namespace Mindstep.EasterEgg.MapEditor.Viewers
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
            InitializeComponent();
            blockContextMenu = new ContextMenu(new MenuItem[]{
                new MenuItem("Edit Block Details", blockContextMenu_EditBlockDetails),
            });
        }

        override public void Initialize(MainForm mainForm, BlockViewWrapperControl wrapper)
        {
            base.Initialize(mainForm, wrapper);
            ToolStrips.Add(toolStrip1);
            ToolStrips.Add(toolStrip2);
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
            SetDisplayCoords(CoordinateTransform.ScreenToObjectSpace(e.Location.ToXnaPoint(), Wrapper.Camera, currentLayer).ToPosition());

            if (drawingBlocks)
            {
                createBlockAt(e.Location);
            }
            else if (erasingBlocks)
            {
                deleteBlockAt(e.Location);
            }
        }

        private void SetDisplayCoords(Position position)
        {
            toolStripCoordX.Text = "X:" + position.X;
            toolStripCoordY.Text = "Y:" + position.Y;
            toolStripCoordZ.Text = "Z:" + position.Z;
        }

        private void BlockEditingControl_MouseUpWithoutMoving(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Point mousePosInProjSpace = CoordinateTransform.ScreenToProjSpace(e.Location.ToXnaPoint(), Wrapper.Camera);
                SaveBlock hitBlock = getHitBlock(MainForm.ModelManager.SelectedModel.Blocks, mousePosInProjSpace.ToSDPoint());

                if (hitBlock != null)
                {
                    selectedBlocks.Clear();
                    selectedBlocks.Add(hitBlock);
                    Invalidate();
                    blockContextMenu.Show(this, e.Location);
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
            BlockDetailsForm form = new BlockDetailsForm(selectedBlocks, MousePosition, Wrapper);
            form.Show();
            selectedBlocks.Clear();
            Invalidate();
        }

        private void toolStripButtonShowGrid_CheckedChanged(object sender, EventArgs e)
        {
            Invalidate();
        }





        private void createBlockAt(SDPoint mouseLocation)
        {
            Position pos = posUnderPointInCurrentLayer(mouseLocation);
            if (!MainForm.ModelManager.SelectedModel.Blocks.Any(block => block.Position == pos))
            {
                MainForm.ModelManager.SelectedModel.Blocks.Add(new SaveBlock(pos));
            }
            Invalidate();
        }

        private void deleteBlockAt(SDPoint mouseLocation)
        {
            Position pos = posUnderPointInCurrentLayer(mouseLocation);
            foreach (Animation animation in MainForm.ModelManager.SelectedModel.Animations)
            {
                foreach (SaveFrame<Texture2DWithPos> frame in animation.Frames)
                {
                    foreach (Texture2DWithPos tex in frame.Images.BackToFront())
                    {
                        tex.projectedOnto.RemoveAll(block => block.Position == pos);
                    }
                }
            }
            MainForm.ModelManager.SelectedModel.Blocks.RemoveAll(block => block.Position == pos);
            Invalidate();
        }

        private bool existsBlockAt(SDPoint mouseLocation)
        {
            Position pos = posUnderPointInCurrentLayer(mouseLocation);
            return MainForm.ModelManager.SelectedModel.Blocks.Any(block => block.Position == pos);
        }

        private Position posUnderPointInCurrentLayer(SDPoint mouseLocation)
        {
            return CoordinateTransform.ScreenToObjectSpace(
                mouseLocation.ToXnaPoint(), Wrapper.Camera, CurrentLayer).ToPosition();
        }

        private SaveBlock getHitBlockInCurrentLayer(SDPoint mouseLocation)
        {
            Point mousePosInProjSpace = CoordinateTransform.ScreenToProjSpace(mouseLocation.ToXnaPoint(), Wrapper.Camera);

            return getHitBlock(
                MainForm.ModelManager.SelectedModel.Blocks.Where(block => block.Position.Z == CurrentLayer),
                mousePosInProjSpace.ToSDPoint());
        }





        override protected void drawGrid(BoundingBoxInt boundingBox)
        {
            for (int x = boundingBox.Min.X; x <= boundingBox.Max.X; x++)
            {
                for (int y = boundingBox.Min.Y; y <= boundingBox.Max.Y; y++)
                {
                    drawBlock(textureGridStriped, boundingBox, Color.White, new Position(x, y, -1), -0.0001f);
                    if (toolStripButtonShowGrid.CheckState == CheckState.Checked)
                    {
                        drawBlock(textureGridFilled, boundingBox, Color.Green, new Position(x, y, CurrentLayer - 1), 0.0001f);
                    }
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
            foreach (SaveBlock saveBlock in MainForm.ModelManager.SelectedModel.Blocks)
            {
                switch (Math.Sign(saveBlock.Position.Z - CurrentLayer))
                {
                    case 1: //above
                        drawBlock(blockDrawStateTexture[BlockDrawState], boundingBox, blockTypeColor[saveBlock.Type], saveBlock.Position);
                        break;
                    case 0: //at
                        drawBlock(textureDrawing, boundingBox, blockTypeColor[saveBlock.Type], saveBlock.Position);
                        break;
                    case -1: //below
                        drawBlock(textureBlock, boundingBox, blockTypeColor[saveBlock.Type], saveBlock.Position);
                        break;
                }
                if (!string.IsNullOrWhiteSpace(saveBlock.Script))
                {
                    Vector2 drawCoords = CoordinateTransform.ObjectToProjectionSpace(saveBlock.Position);
                    drawCoords -= (spriteFont.MeasureString(saveBlock.Script)/2).Ceiling();
                    spriteBatch.DrawString(spriteFont, saveBlock.Script, drawCoords,
                        Color.Cyan, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                }
            }
        }
    }
}
