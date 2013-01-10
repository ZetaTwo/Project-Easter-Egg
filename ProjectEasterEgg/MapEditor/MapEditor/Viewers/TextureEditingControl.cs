﻿using System;
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

namespace Mindstep.EasterEgg.MapEditor
{
    public partial class TextureEditingControl : BlockViewControl
    {
        private static readonly Color SELECTED_TEXTURE_COLOR = Color.LimeGreen;

        private Point mouseCoordAtMouseDown;

        private ContextMenu textureContextMenu;
        private MenuItem menuItemSelectBlocksToProjectOnto;
        private List<SaveBlock> selectedBlocks = new List<SaveBlock>();

        private List<Texture2DWithDoublePos> selectedTextures = new List<Texture2DWithDoublePos>();






        public TextureEditingControl()
        {
            InitializeComponent();
            ValidateChildren();
            menuItemSelectBlocksToProjectOnto = new MenuItem("Select blocks to project onto",
                TextureContextMenuSelectBlocksToProjectOnto);
            textureContextMenu = new ContextMenu(new MenuItem[]{
                new MenuItem("Bring To Front", TextureContextMenuBringToFront),
                new MenuItem("Bring Forward", TextureContextMenuBringForward),
                new MenuItem("Send Backward", TextureContextMenuSendBackward),
                new MenuItem("Send To Back", TextureContextMenuSendToBack),
                new MenuItem("-"),
                menuItemSelectBlocksToProjectOnto,
                new MenuItem("Delete", TextureContextMenuDelete),
            });
        }
        public override void Initialize(MainForm mainForm, BlockViewWrapperControl wrapper)
        {
            base.Initialize(mainForm, wrapper);
            ToolStrips.Add(toolStrip1);
            toolStripImageList1.Initialize(mainForm.ModelManager);
            mainForm.ModelManager.FrameChanged += new EventHandler<ModificationEventArgs<SaveFrame<Texture2DWithPos>>>(
                (sender, e) => { selectedTextures.Clear(); Invalidate(); });

            MouseDown += new MouseEventHandler(TextureEditingControl_MouseDown);
            MouseUp += new MouseEventHandler(TextureEditingControl_MouseUp);
            MouseMove += new MouseEventHandler(TextureEditingControl_MouseMove);
            MouseUpWithoutMoving += new MouseEventHandler(TextureEditingControl_MouseUpWithoutMoving);
            KeyDown += new KeyEventHandler(TextureEditingControl_KeyDown);
        }

        #region Context menus
        private void TextureContextMenuBringToFront(object sender, EventArgs e)
        {
            MainForm.ModelManager.CurrentFrame.Images.BringToFront(selectedTextures.GetUnderlyingTextures2DWithDoublePos());
            MainForm.UpdatedThings();
        }

        private void TextureContextMenuBringForward(object sender, EventArgs e)
        {
            MainForm.ModelManager.CurrentFrame.Images.BringForward(selectedTextures.GetUnderlyingTextures2DWithDoublePos());
            MainForm.UpdatedThings();
        }

        private void TextureContextMenuSendBackward(object sender, EventArgs e)
        {
            MainForm.ModelManager.CurrentFrame.Images.SendBackward(selectedTextures.GetUnderlyingTextures2DWithDoublePos());
            MainForm.UpdatedThings();
        }

        private void TextureContextMenuSendToBack(object sender, EventArgs e)
        {
            MainForm.ModelManager.CurrentFrame.Images.SendToBack(selectedTextures.GetUnderlyingTextures2DWithDoublePos());
            MainForm.UpdatedThings();
        }

        private void TextureContextMenuSelectBlocksToProjectOnto(object sender, EventArgs e)
        {
            Wrapper.enterTextureProjectionMode(selectedTextures.Single().t);
        }
        private void TextureContextMenuDelete(object sender, EventArgs e)
        {
            MainForm.ModelManager.CurrentFrame.Images.Remove(selectedTextures.GetUnderlyingTextures2DWithDoublePos());
            selectedTextures.Clear();
            MainForm.UpdatedThings();
        }
        #endregion





        void TextureEditingControl_MouseUpWithoutMoving(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                updateSelectedTextures(e.Location, Helper.getClickOperation());
                if (selectedTextures.Count != 0)
                {
                    menuItemSelectBlocksToProjectOnto.Enabled = selectedTextures.Count == 1;
                    textureContextMenu.Show(this, e.Location);
                }
            }
        }





        /// <summary>
        /// Updates the field "selectedTextures" to include the first texture that
        /// contains "point" and isn't transparent at that pixel. If no texture meet
        /// these requirements, "selectedTextures" is cleared.
        /// </summary>
        /// <param name="mouseLocation"></param>
        /// <param name="clickOperation">The operation to be performed on the hit textures.</param>
        private void updateSelectedTextures(SD.Point mouseLocation, ClickOperation clickOperation)
        {
            Point mousePosInProjSpace = CoordinateTransform.ScreenToProjSpace(mouseLocation.ToXnaPoint(), Wrapper.Camera);

            foreach (Texture2DWithPos hitTexture in MainForm.ModelManager.CurrentFrame.Images.FrontToBack())
            {
                if (hitTexture.Bounds.Contains(mousePosInProjSpace) &&
                    hitTexture.Texture.GetPixelColor(mousePosInProjSpace.Subtract(hitTexture.pos)).A != 0)
                {
                    Texture2DWithDoublePos hitAlreadySelectedTexture = null;
                    foreach (Texture2DWithDoublePos selectedTexture in selectedTextures)
                    {
                        if (hitTexture == selectedTexture.t)
                        {
                            hitAlreadySelectedTexture = selectedTexture;
                            break;
                        }
                    }
                    switch (clickOperation)
                    {
                        case ClickOperation.Add:
                            if (hitAlreadySelectedTexture == null)
                            {
                                selectedTextures.Add(new Texture2DWithDoublePos(hitTexture));
                            }
                            break;
                        case ClickOperation.Replace:
                            if (hitAlreadySelectedTexture == null)
                            {
                                selectedTextures.Clear();
                                selectedTextures.Add(new Texture2DWithDoublePos(hitTexture));
                            }
                            break;
                        case ClickOperation.Subtract:
                            if (hitAlreadySelectedTexture != null)
                            {
                                selectedTextures.Remove(hitAlreadySelectedTexture);
                            }
                            break;
                        case ClickOperation.Toggle:
                            if (hitAlreadySelectedTexture != null)
                            {
                                selectedTextures.Remove(hitAlreadySelectedTexture);
                            }
                            else
                            {
                                selectedTextures.Add(new Texture2DWithDoublePos(hitTexture));
                            }
                            break;
                    }
                    MainForm.UpdatedThings();
                    return;
                }
            }

            selectedTextures.Clear();
            Invalidate();
        }

        void TextureEditingControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseCoordAtMouseDown = e.Location.ToXnaPoint();
                updateSelectedTextures(e.Location, Helper.getClickOperation());
                selectedTextures.ForEach(t => t.CoordAtMouseDown = t.t.pos);
            }
        }

        void TextureEditingControl_MouseUp(object sender, MouseEventArgs e)
        {
            Invalidate(); //TODO: needed?
        }

        void TextureEditingControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point changeInProjectionSpace = e.Location.ToXnaPoint().Subtract(mouseCoordAtMouseDown).Divide(Wrapper.Camera.Zoom);
                selectedTextures.ForEach(tex => tex.t.pos = tex.CoordAtMouseDown.Add(changeInProjectionSpace));
                Invalidate();
            }
        }

        void TextureEditingControl_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    Wrapper.EditingMode = EditingMode.Texture;
            //}
            if (e.KeyCode == Keys.Delete && selectedTextures.Count != 0)
            {
                MainForm.ModelManager.CurrentFrame.Images.Remove(selectedTextures.GetUnderlyingTextures2DWithDoublePos());
                selectedTextures.Clear();
                MainForm.UpdatedThings();
            }
        }

        protected override void drawTextures()
        {
            base.drawTextures();

            if (drawTextureIndices.Checked)
            {
                float i = 0;
                foreach (Texture2DWithPos tex in MainForm.ModelManager.CurrentFrame.Images.BackToFront())
                {
                    spriteBatch.DrawString(spriteFont, i.ToString(), tex.pos.ToVector2(),
                        Color.Green, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                    i++;
                }
            }

            foreach (Texture2DWithDoublePos selectedTexture in selectedTextures)
            {
                int borderWidth = 1;
                Rectangle r = selectedTexture.t.Bounds;
                r.Inflate(borderWidth, borderWidth);
                spriteBatch.DrawRectangle(r, SELECTED_TEXTURE_COLOR, borderWidth);
            }
        }

        private void drawTextureIndices_CheckedChanged(object sender, EventArgs e)
        {
            Invalidate();
        }
    }
}
