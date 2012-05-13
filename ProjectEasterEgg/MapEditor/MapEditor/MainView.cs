#region Using Statements
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Mindstep.EasterEgg.Commons;
using System;
using System.Linq;
using System.Collections.Generic;
using Mindstep.EasterEgg.Commons.Graphic;
using Mindstep.EasterEgg.Commons.SaveLoad;
#endregion

namespace Mindstep.EasterEgg.MapEditor
{
    /// <summary>
    /// Example control inherits from GraphicsDeviceControl, which allows it to
    /// render using a GraphicsDevice. This control shows how to draw animating
    /// 3D graphics inside a WinForms application. It hooks the Application.Idle
    /// event, using this to invalidate the control, which will cause the animation
    /// to constantly redraw.
    /// </summary>
    class MainView : GraphicsDeviceControl
    {
        private static readonly Color SELECTED_TEXTURE_COLOR = Color.LimeGreen;

        private SpriteBatch spriteBatch;
        private Texture2D textureBlock;
        private Texture2D textureDrawing;
        private Texture2D textureGridFilled;
        private Texture2D textureGridStriped;
        private Texture2D textureWireframe;
        private Texture2D textureWireframeBack;
        private Texture2D textureWireframeFilled;

        private MainForm mainForm;
        private bool draggingTextures;
        private bool panning;
        private System.Drawing.Point lastMouseLocation;
        private SamplerState samplerState;

        private Camera camera;
        public float Zoom { get { return camera.Zoom; } }
        private SpriteFont spriteFont;
        public bool drawTextureIndices = true;
        private Point mouseCoordAtMouseDown;
        private List<Texture2DWithDoublePos> selectedTextures = new List<Texture2DWithDoublePos>();
        private List<SaveBlock> selectedBlocks = new List<SaveBlock>();
        private ContextMenu blockContextMenu;
        private ContextMenu textureContextMenu;
        private bool mouseHasMovedSinceMouseDown = true;

        private static Dictionary<BlockType, Color> blockTypeColor;
        private static Dictionary<BlockDrawState, Texture2D> blockDrawStateTexture;
        private bool erasingBlocks;
        private bool drawingBlocks;
        private Texture2DWithPos textureBeingProjectedDown;
        private MenuItem menuItemSelectBlocksToProjectOnto;





        public void Initialize(MainForm mainForm)
        {
            this.mainForm = mainForm;
            camera = new Camera(new float[] { .25f, .5f, .75f, 1, 2, 4, 6, 8, 12, 16, 24, 32 }, 3, new Point(Width / 2, Height / 2));
            spriteBatch = new SpriteBatch(mainForm.GraphicsDevice);
            spriteFont = mainForm.Content.Load<SpriteFont>("hudFont");

            textureBlock = mainForm.Content.Load<Texture2D>("block31");
            textureDrawing = mainForm.Content.Load<Texture2D>("block31drawing");
            textureGridFilled = mainForm.Content.Load<Texture2D>("block31filledGrid");
            textureGridStriped = mainForm.Content.Load<Texture2D>("block31stripedGrid");
            textureWireframe = mainForm.Content.Load<Texture2D>("block31wireframe");
            textureWireframeBack = mainForm.Content.Load<Texture2D>("block31wireframeBack");
            textureWireframeFilled = mainForm.Content.Load<Texture2D>("block31wireframeFilled");
            blockContextMenu = new ContextMenu(new MenuItem[]{
                new MenuItem("Edit Block Details", blockContextMenu_EditBlockDetails),
            });

            menuItemSelectBlocksToProjectOnto = new MenuItem("Select blocks to project onto", TextureContextMenuSelectBlocksToProjectOnto);
            textureContextMenu = new ContextMenu(new MenuItem[]{
                new MenuItem("Bring To Front", TextureContextMenuBringToFront),
                new MenuItem("Bring Forward", TextureContextMenuBringForward),
                new MenuItem("Send Backward", TextureContextMenuSendBackward),
                new MenuItem("Send To Back", TextureContextMenuSendToBack),
                new MenuItem("-"),
                menuItemSelectBlocksToProjectOnto,
                new MenuItem("Delete", TextureContextMenuDelete),
            });

            MouseDown += new MouseEventHandler(MainView_MouseDown);
            MouseUp += new MouseEventHandler(MainView_MouseUp);
            MouseMove += new MouseEventHandler(MainView_MouseMove);
            KeyDown += new KeyEventHandler(MainView_KeyDown);
            Resize += new EventHandler(MainView_Resize);

            samplerState = new SamplerState();
            samplerState.Filter = TextureFilter.PointMipLinear;
            samplerState.AddressU = TextureAddressMode.Clamp;
            samplerState.AddressV = TextureAddressMode.Clamp;

            blockTypeColor = new Dictionary<BlockType, Color>();
            blockTypeColor.Add(BlockType.SOLID, Color.Green);
            blockTypeColor.Add(BlockType.WALKABLE, Color.Olive);
            blockTypeColor.Add(BlockType.STAIRS_UP, Color.Brown);
            blockTypeColor.Add(BlockType.STAIRS_DOWN, Color.Blue);
            blockTypeColor.Add(BlockType.SPAWN_LOCATION, Color.White);

            blockDrawStateTexture = new Dictionary<BlockDrawState, Texture2D>();
            blockDrawStateTexture.Add(BlockDrawState.None, mainForm.transparentOneByOneTexture);
            blockDrawStateTexture.Add(BlockDrawState.Solid, textureBlock);
            blockDrawStateTexture.Add(BlockDrawState.Wireframe, textureWireframe);

            settings.Add(EditingMode.Block, new Settings(Color.Black, BlockDrawState.Wireframe, .3f));
            settings.Add(EditingMode.Texture, new Settings(Color.DarkRed, BlockDrawState.Solid, 1));
            settings.Add(EditingMode.TextureProjection, new Settings(Color.DarkBlue, BlockDrawState.Solid, 1));

            CurrentEditingMode = EditingMode.Block;
        }





        private Dictionary<EditingMode, Settings> settings = new Dictionary<EditingMode, Settings>();

        private int currentLayer = 0;
        internal int CurrentLayer
        {
            get { return currentLayer; }
            set
            {
                if (CurrentEditingMode == EditingMode.Block)
                {
                    currentLayer = value;
                    mainForm.UpdatedGraphics();
                }
            }
        }

        private EditingMode editingMode;
        internal EditingMode CurrentEditingMode
        {
            get { return editingMode; }
            set
            {
                if (value == EditingMode.TextureProjection)
                {
                    throw new ArgumentException("You are not allowed to " +
                        "set editing mode to TextureProjection manually");
                }
                editingMode = value;
                mainForm.UpdatedSettings();
            }
        }

        internal BlockDrawState CurrentBlockDrawState
        {
            get { return settings[CurrentEditingMode].blockDrawState; }
            set
            {
                settings[CurrentEditingMode].blockDrawState = value;
                mainForm.UpdatedSettings();
            }
        }

        internal Color CurrentBackgroundColor
        {
            get { return settings[CurrentEditingMode].backgroundColor; }
            set
            {
                settings[CurrentEditingMode].backgroundColor = value;
                mainForm.UpdatedSettings();
            }
        }
        public float TextureOpacity
        {
            get { return settings[CurrentEditingMode].opacity; }
            set
            {
                settings[CurrentEditingMode].opacity = value;
                mainForm.UpdatedSettings();
            }
        }

        private void enterTextureProjectionMode(Texture2DWithPos textureToProjectDown)
        {
            textureBeingProjectedDown = textureToProjectDown;
            editingMode = EditingMode.TextureProjection;
            mainForm.UpdatedSettings();
        }

        private void exitTextureProjectionMode()
        {
            textureBeingProjectedDown = null;
            editingMode = EditingMode.Texture;
            mainForm.UpdatedSettings();
        }

        /// <summary>
        /// Draws the control.
        /// </summary>
        protected override void Draw()
        {
            GraphicsDevice.Clear(settings[CurrentEditingMode].backgroundColor);
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.NonPremultiplied, samplerState, null, null, null, camera.ZoomAndOffsetMatrix);

            BoundingBoxInt boundingBox = new BoundingBoxInt(mainForm.CurrentModel.blocks.ToPositions());

            List<Position> tiles = new List<Position>();
            for (int x = -5; x < 10; x += 1)
            {
                for (int y = -5; y < 10; y += 1)
                {
                    Position tilePos = new Position(x, y, -1);
                    tiles.Add(tilePos);
                    boundingBox.addPos(tilePos);
                }
            }

            Position currentLayerOffset = new Position(0, 0, CurrentLayer);
            foreach (Position tilePos in tiles)
            {
                drawBlock(textureGridStriped, boundingBox, Color.White, tilePos, -0.01f);
                if (CurrentEditingMode == EditingMode.Block)
                {
                    drawBlock(textureGridFilled, boundingBox, Color.Green, tilePos + currentLayerOffset, 0.01f);
                }
            }

            switch (CurrentEditingMode)
            {
                case EditingMode.Block:
                    foreach (SaveBlock saveBlock in mainForm.CurrentModel.blocks)
                    {
                        switch (Math.Sign(saveBlock.Position.Z - CurrentLayer))
                        {
                            case 1: //above
                                drawBlock(blockDrawStateTexture[CurrentBlockDrawState], boundingBox, blockTypeColor[saveBlock.type], saveBlock.Position);
                                break;
                            case 0: //at
                                drawBlock(textureDrawing, boundingBox, blockTypeColor[saveBlock.type], saveBlock.Position);
                                break;
                            case -1: //below
                                drawBlock(textureBlock, boundingBox, blockTypeColor[saveBlock.type], saveBlock.Position);
                                break;
                        }
                    }
                    foreach (SaveBlock saveblock in selectedBlocks)
                    {
                        drawBlock(textureWireframeFilled, boundingBox, Color.White, saveblock.Position, -.02f);
                    }
                    break;
                case EditingMode.Texture:
                    foreach (SaveBlock saveBlock in mainForm.CurrentModel.blocks)
                    {
                        drawBlock(textureBlock, boundingBox, blockTypeColor[saveBlock.type], saveBlock.Position);
                    }
                    break;
                case EditingMode.TextureProjection:
                    foreach (SaveBlock saveBlock in mainForm.CurrentModel.blocks)
                    {
                        if (textureBeingProjectedDown.projectedOnto.Contains(saveBlock))
                        {
                            drawBlock(textureWireframeBack, boundingBox, blockTypeColor[saveBlock.type], saveBlock.Position, .01f);
                            //TODO: cut out relevant piece of the texture and draw it inside the block
                            drawBlock(textureWireframe, boundingBox, blockTypeColor[saveBlock.type], saveBlock.Position, -.01f);
                        }
                        else
                        {
                            drawBlock(textureBlock, boundingBox, blockTypeColor[saveBlock.type], saveBlock.Position);
                        }
                    }
                    break;
            }

            if (CurrentEditingMode == EditingMode.TextureProjection)
            {
                spriteBatch.Draw(textureBeingProjectedDown.Texture, textureBeingProjectedDown.pos.ToVector2(), null,
                    new Color(1, 1, 1, TextureOpacity), 0,
                    Vector2.Zero, 1, SpriteEffects.None, 0);
            }
            else
            {
                float i = 0;
                foreach (Texture2DWithPos tex in mainForm.CurrentFrame.Images.BackToFront())
                {
                    float depth = (1 - i / mainForm.CurrentFrame.Images.Count) * .1f;
                    spriteBatch.Draw(tex.Texture, tex.pos.ToVector2(), null,
                        new Color(1, 1, 1, TextureOpacity), 0,
                        Vector2.Zero, 1, SpriteEffects.None, depth / camera.Zoom);
                    if (mainForm.DrawTextureIndices)
                    {
                        spriteBatch.DrawString(spriteFont, i.ToString(), tex.pos.ToVector2(),
                            Color.Green, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                    }
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

#if false //debug draw mouse coords
                Vector3 v = CoordinateTransform.ScreenToObjectSpace(lastMouseLocation.ToXnaPoint(), camera, CurrentLayer);
                Vector2 u = CoordinateTransform.ObjectToProjectionSpace(v);
                spriteBatch.DrawRectangle(new Rectangle((int)u.X, (int)u.Y, 1, 1), Color.Orchid, 5);
                spriteBatch.DrawString(spriteFont, v.ToString() + "\n" + v.ToPosition().ToString(), u, Color.Orange);
#endif
            spriteBatch.End();
        }

        private void drawBlock(Texture2D image, BoundingBoxInt boundingBox, Color color, Position pos, float depthOffset = 0)
        {
            float depth = boundingBox.getRelativeDepthOf(pos);
            Vector2 projCoords = CoordinateTransform.ObjectToProjSpace(pos);
            spriteBatch.Draw(image, projCoords + Constants.blockDrawOffset, null, color, 0, Vector2.Zero, 1, SpriteEffects.None, (depth + depthOffset) / camera.Zoom);
        }







        private void MainView_MouseDown(object sender, MouseEventArgs e)
        {
            selectedBlocks.Clear();
            if (e.Button == MouseButtons.Right)
            {
                if (CurrentEditingMode == EditingMode.TextureProjection)
                {
                    exitTextureProjectionMode();
                    //TODO: remove this, an Accept button should popup when projecting textures instead.
                }
                else if (draggingTextures) //pressing the other mouse button only cancels the dragging
                {
                    draggingTextures = false;
                }
                else
                {
                    panning = true;
                    mouseHasMovedSinceMouseDown = false;
                }
            }
            else if (e.Button == MouseButtons.Left)
            {
                if (panning) //pressing the other mouse button only cancels the panning
                {
                    panning = false;
                }
                else
                {
                    switch (CurrentEditingMode)
                    {
                        case EditingMode.Block:
                            if (blockAt(e.Location))
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
                            break;
                        case EditingMode.Texture:
                            mouseCoordAtMouseDown = e.Location.ToXnaPoint();
                            updateSelectedTextures(e.Location, getClickOperation());
                            selectedTextures.ForEach(t => t.CoordAtMouseDown = t.t.pos);
                            draggingTextures = selectedTextures.Count != 0;
                            break;
                        case EditingMode.TextureProjection:
                            SaveBlock hitBlock = getHitBlock(mainForm.CurrentModel.blocks,
                                CoordinateTransform.ScreenToProjSpace(e.Location.ToXnaPoint(), camera).ToSDPoint());
                            if (!textureBeingProjectedDown.projectedOnto.Remove(hitBlock))
                            {
                                textureBeingProjectedDown.projectedOnto.Add(hitBlock);
                            }
                            mainForm.UpdatedThings();
                            break;
                    }

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
            
            lastMouseLocation = e.Location;
            mainForm.UpdatedGraphics();
        }

        private void MainView_MouseMove(object sender, MouseEventArgs e)
        {
            if (!(e.Button == System.Windows.Forms.MouseButtons.Left ||
                e.Button == System.Windows.Forms.MouseButtons.Right ||
                e.Button == System.Windows.Forms.MouseButtons.Middle))
            { //no buttons down
                return;
            }

            //since MouseMove was called, the mouse must have moved
            mouseHasMovedSinceMouseDown = true;

            Point movement = e.Location.ToXnaPoint().Subtract(lastMouseLocation.ToXnaPoint());
            if (panning)
            {
                camera.Offset = camera.Offset.Add(movement);
            }
            else if (draggingTextures)
            {
                Point changeInProjectionSpace = e.Location.ToXnaPoint().Subtract(mouseCoordAtMouseDown).Divide(camera.Zoom);
                selectedTextures.ForEach(tex => tex.t.pos = tex.CoordAtMouseDown.Add(changeInProjectionSpace));
            }
            else if (CurrentEditingMode == EditingMode.Block && drawingBlocks)
            {
                createBlockAt(e.Location);
            }
            else if (CurrentEditingMode == EditingMode.Block && erasingBlocks)
            {
                deleteBlockAt(e.Location);
            }

            lastMouseLocation = e.Location;
            mainForm.UpdatedGraphics();
            //mainForm.SaveBlocks.Clear();
            //mainForm.SaveBlocks.Add(new SaveBlock(CoordinateTransform.ScreenToObjectSpace(e.Location.ToXnaPoint(), camera, mainForm.CurrentLayer).ToPosition()));
        }

        private void MainView_MouseUp(object sender, MouseEventArgs e)
        {
            panning = false;
            if (draggingTextures)
            {
                mainForm.UpdatedThings();
            }
            draggingTextures = false;
            if (!mouseHasMovedSinceMouseDown)
            {
                MainView_MouseUpWithoutMoving(sender, e);
            }
        }

        private void MainView_MouseUpWithoutMoving(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                switch (CurrentEditingMode)
                {
                    case EditingMode.Block:
                        Point mousePosInProjSpace = CoordinateTransform.ScreenToProjSpace(e.Location.ToXnaPoint(), camera);
                        SaveBlock hitBlock = getHitBlock(mainForm.CurrentModel.blocks, mousePosInProjSpace.ToSDPoint());

                        if (hitBlock != null)
                        {
                            selectedBlocks.Clear();
                            selectedBlocks.Add(hitBlock);
                            mainForm.UpdatedGraphics();
                            blockContextMenu.Show(this, e.Location);
                            mainForm.UpdatedThings();
                        }
                        //updateSelectedBlocks(e.Location, getClickOperation());
                        //if (selectedBlocks.Count != 0)
                        //{
                        //    blockContextMenu.Show(this, e.Location);
                        //}
                        break;
                    case EditingMode.Texture:
                        updateSelectedTextures(e.Location, getClickOperation());
                        if (selectedTextures.Count != 0)
                        {
                            menuItemSelectBlocksToProjectOnto.Enabled = selectedTextures.Count == 1;
                            textureContextMenu.Show(this, e.Location);
                        }
                        break;
                }
            }
        }

        public void MainView_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                camera.ZoomIn(e.Location.ToXnaPoint());
                mainForm.RefreshTitle();
            }
            else if (e.Delta < 0)
            {
                camera.ZoomOut(e.Location.ToXnaPoint());
                mainForm.RefreshTitle();
            }
            mainForm.UpdatedGraphics();
        }

        public void MainView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete
                && selectedTextures.Count != 0)
            {
                mainForm.CurrentFrame.Images.Remove(selectedTextures.GetUnderlyingTextures2DWithDoublePos());
                selectedTextures.Clear();
                mainForm.UpdatedThings();
            }
            if (e.KeyCode == Keys.W)
            {
                CurrentLayer++;
            }
            if (e.KeyCode == Keys.S)
            {
                CurrentLayer--;
            }
        }

        #region Context menus
        private void TextureContextMenuBringToFront(object sender, EventArgs e)
        {
            mainForm.CurrentFrame.Images.BringToFront(selectedTextures.GetUnderlyingTextures2DWithDoublePos());
            mainForm.UpdatedThings();
        }

        private void TextureContextMenuBringForward(object sender, EventArgs e)
        {
            mainForm.CurrentFrame.Images.BringForward(selectedTextures.GetUnderlyingTextures2DWithDoublePos());
            mainForm.UpdatedThings();
        }

        private void TextureContextMenuSendBackward(object sender, EventArgs e)
        {
            mainForm.CurrentFrame.Images.SendBackward(selectedTextures.GetUnderlyingTextures2DWithDoublePos());
            mainForm.UpdatedThings();
        }

        private void TextureContextMenuSendToBack(object sender, EventArgs e)
        {
            mainForm.CurrentFrame.Images.SendToBack(selectedTextures.GetUnderlyingTextures2DWithDoublePos());
            mainForm.UpdatedThings();
        }

        private void TextureContextMenuSelectBlocksToProjectOnto(object sender, EventArgs e)
        {
            enterTextureProjectionMode(selectedTextures.Single().t);
        }
        private void TextureContextMenuDelete(object sender, EventArgs e)
        {
            mainForm.CurrentFrame.Images.Remove(selectedTextures.GetUnderlyingTextures2DWithDoublePos());
            selectedTextures.Clear();
            mainForm.UpdatedThings();
        }

        private void blockContextMenu_EditBlockDetails(object sender, EventArgs e)
        {
            new BlockDetailsForm(selectedBlocks, lastMouseLocation);
            selectedBlocks.Clear();
            mainForm.UpdatedThings();
        }
        #endregion



        private void createBlockAt(System.Drawing.Point mouseLocation)
        {
            Position pos = posUnderPoint(mouseLocation);
            if (!mainForm.CurrentModel.blocks.Any(block => block.Position == pos))
            {
                mainForm.CurrentModel.blocks.Add(new SaveBlock(pos));
            }
            mainForm.UpdatedThings();
        }

        private void deleteBlockAt(System.Drawing.Point mouseLocation)
        {
            Position pos = posUnderPoint(mouseLocation);
            mainForm.CurrentModel.blocks.RemoveAll(block => block.Position == pos);
            mainForm.UpdatedThings();
        }

        private bool blockAt(System.Drawing.Point mouseLocation)
        {
            Position pos = posUnderPoint(mouseLocation);
            return mainForm.CurrentModel.blocks.Any(block => block.Position == pos);
        }

        private Position posUnderPoint(System.Drawing.Point mouseLocation)
        {
            return CoordinateTransform.ScreenToObjectSpace(
                mouseLocation.ToXnaPoint(), camera, CurrentLayer).ToPosition();
        }

        private SaveBlock getHitBlockInCurrentLayer(System.Drawing.Point mouseLocation)
        {
            Point mousePosInProjSpace = CoordinateTransform.ScreenToProjSpace(mouseLocation.ToXnaPoint(), camera);

            mainForm.UpdatedGraphics();
            return getHitBlock(
                mainForm.CurrentModel.blocks.Where(block => block.Position.Z == CurrentLayer),
                mousePosInProjSpace.ToSDPoint());
        }

        private SaveBlock getHitBlock(IEnumerable<SaveBlock> outOf, System.Drawing.Point pointInProjSpace)
        {
            BoundingBoxInt boundingBox = new BoundingBoxInt(outOf.ToPositions());

            foreach (SaveBlock block in outOf.OrderBy(block => boundingBox.getRelativeDepthOf(block.Position)))
            {
                System.Drawing.Region blockRegion = BlockRegions.WholeBlock.Offset(
                    CoordinateTransform.ObjectToProjSpace(block.Position).ToXnaPoint().Add(
                    Constants.blockDrawOffset.ToXnaPoint()));
                if (blockRegion.IsVisible(pointInProjSpace))
                {
                    return block;
                }
            }
            return null;
        }

        ///// <summary>
        ///// Updates the field TODO "selectedBlocks" to include the first block that
        ///// contains "point". If no block meet this requirement, "selectedTextures" is cleared.
        ///// </summary>
        ///// <param name="mousePos"></param>
        ///// <param name="clickOperation">The operation to be performed on the hit block.</param>
        //private void updateSelectedBlocks(System.Drawing.Point mousePos, ClickOperation clickOperation)
        //{
        //    Position pos = CoordinateTransform.ScreenToObjectSpace(mousePos.ToXnaPoint(), camera, mainForm.CurrentLayer).ToPosition();
        //    Point mousePosInProjSpace = CoordinateTransform.ScreenToProjSpace(mousePos.ToXnaPoint(), camera);

        //    SaveBlock hitAlreadyExistingBlock = getHitBlock(
        //        mainForm.CurrentModel.blocks.Where(block => block.Position.Z >= mainForm.CurrentLayer),
        //        mousePosInProjSpace.ToSDPoint());

        //    SaveBlock newBlock = new SaveBlock(pos);

        //    switch (clickOperation)
        //    {
        //        case ClickOperation.Add:
        //            if (hitAlreadyExistingBlock == null)
        //            {
        //                mainForm.CurrentModel.blocks.Add(newBlock);
        //                selectedBlocks.Add(newBlock);
        //            }
        //            break;
        //        case ClickOperation.Replace:
        //            selectedBlocks.Clear();
        //            if (hitAlreadyExistingBlock == null)
        //            {
        //                mainForm.CurrentModel.blocks.Add(newBlock);
        //                selectedBlocks.Add(newBlock);
        //            }
        //            else
        //            {
        //            }
        //            break;
        //    }
        //    mainForm.Updated();
        //}

        /// <summary>
        /// Updates the field "selectedTextures" to include the first texture that
        /// contains "point" and isn't transparent at that pixel. If no texture meet
        /// these requirements, "selectedTextures" is cleared.
        /// </summary>
        /// <param name="mouseLocation"></param>
        /// <param name="clickOperation">The operation to be performed on the hit textures.</param>
        private void updateSelectedTextures(System.Drawing.Point mouseLocation, ClickOperation clickOperation)
        {
            Point mousePosInProjSpace = CoordinateTransform.ScreenToProjSpace(mouseLocation.ToXnaPoint(), camera);

            foreach (Texture2DWithPos hitTexture in mainForm.CurrentFrame.Images.FrontToBack())
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
                    mainForm.UpdatedThings();
                    return;
                }
            }

            selectedTextures.Clear();
            mainForm.UpdatedGraphics();
        }

        private static ClickOperation getClickOperation()
        {
            if (ModifierKeys == Keys.Shift)
            {
                return ClickOperation.Toggle;
            }
            if (ModifierKeys == Keys.Control)
            {
                return ClickOperation.Copy;
            }
            return ClickOperation.Replace;
        }

        private void MainView_Resize(object sender, EventArgs e)
        {
            System.Console.WriteLine("mainview resized");
        }
    }

    class Texture2DWithDoublePos
    {
        public Texture2DWithPos t;
        public Point CoordAtMouseDown;

        public Texture2DWithDoublePos(Texture2DWithPos t)
        {
            this.t = t;
        }
    }
}
