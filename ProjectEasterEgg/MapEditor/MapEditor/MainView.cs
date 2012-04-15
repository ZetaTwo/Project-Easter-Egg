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
using Mindstep.EasterEgg.MapEditor.Animations;
using Mindstep.EasterEgg.Commons.Graphics;
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
        private Texture2D block;
        private Texture2D grid;

        private MainForm mainForm;
        private bool dragging;
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

        public void Initialize(MainForm mainForm)
        {
            this.mainForm = mainForm;
            camera = new Camera(new Point(Width/2, Height/2));
            spriteBatch = new SpriteBatch(mainForm.GraphicsDevice);
            spriteFont = mainForm.Content.Load<SpriteFont>("hudFont");

            block = mainForm.Content.Load<Texture2D>("mainBlock31");
            grid = mainForm.Content.Load<Texture2D>("mainGrid31");
            if (!block.Bounds.Equals(grid.Bounds))
            {
                throw new Exception("mainBlock and mainGrid image size mismatch.");
            }

            blockContextMenu = new ContextMenu(new MenuItem[]{
                new MenuItem("Edit Block Details", BlockContextMenuEditDetails),
            });
            textureContextMenu = new ContextMenu(new MenuItem[]{
                new MenuItem("Bring To Front", TextureContextMenuBringToFront)});

            MouseDown += new MouseEventHandler(MainView_MouseDown);
            MouseUp += new MouseEventHandler(MainView_MouseUp);
            MouseMove += new MouseEventHandler(MainView_MouseMove);
            KeyDown += new KeyEventHandler(MainView_KeyDown);

            samplerState = new SamplerState();
            samplerState.Filter = TextureFilter.PointMipLinear;
            samplerState.AddressU = TextureAddressMode.Clamp;
            samplerState.AddressV = TextureAddressMode.Clamp;
        }


        /// <summary>
        /// Draws the control.
        /// </summary>
        protected override void Draw()
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.NonPremultiplied, samplerState, null, null, null, camera.ZoomAndOffsetMatrix);
            
            BoundingBoxInt boundingBox = new BoundingBoxInt(mainForm.SaveBlocks.ToPositions());

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

            foreach (Position tilePos in tiles)
            {
                drawBlock(grid, boundingBox, Color.White, tilePos);
            }

            foreach (SaveBlock saveBlock in mainForm.SaveBlocks)
            {

                Color color = Color.Green;
                if (saveBlock.Position.Z == mainForm.CurrentHeight)
                {
                    switch (saveBlock.type)
                    {
                        case BlockType.SOLID:
                            color = Color.Green;
                            break;
                        case BlockType.STAIRS_DOWN:
                            color = Color.Blue;
                            break;
                        case BlockType.STAIRS_UP:
                            color = Color.Brown;
                            break;
                        case BlockType.WALKABLE:
                            color = Color.Olive;
                            break;
                    }

                }
                else
                {
                    color = Color.Red;
                }
                drawBlock(block, boundingBox, color, saveBlock.Position);
            }

            for (int i = 0; i < mainForm.CurrentFrame.Textures.Count; i++)
            {
                Texture2DWithPos tex = mainForm.CurrentFrame.Textures[i];
                float depth = 0.1f * (1 - (float)i / mainForm.CurrentFrame.Textures.Count);
                spriteBatch.Draw(tex.Texture, tex.Coord.ToVector2(), null,
                    new Color(1, 1, 1, mainForm.TextureOpacity), 0,
                    Vector2.Zero, 1, SpriteEffects.None, depth / camera.Zoom);
                if (mainForm.DrawTextureIndices)
                {
                    spriteBatch.DrawString(spriteFont, i.ToString(), tex.Coord.ToVector2(), Color.Green);
                }
            }

            foreach (Texture2DWithDoublePos selectedTexture in selectedTextures)
            {
                spriteBatch.DrawRectangle(selectedTexture.t.Bounds, SELECTED_TEXTURE_COLOR, 1);
            }

            spriteBatch.End();
        }

        private void drawBlock(Texture2D image, BoundingBoxInt boundingBox, Color color, Position pos)
        {
            float depth = boundingBox.getRelativeDepthOf(pos);
            Vector2 projCoords = CoordinateTransform.ObjectToProjectionSpace(pos);
            spriteBatch.Draw(image, projCoords, null, color, 0, Vector2.Zero, 1, SpriteEffects.None, depth / camera.Zoom);
        }

        private void MainView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (dragging) //pressing the other mouse button only cancels the dragging
                {
                    dragging = false;
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
                    handleLeftMouseDown(e.Location);
                }
            }
            lastMouseLocation = e.Location;
            mainForm.Updated();
        }

        private void TextureContextMenuBringToFront(object sender, EventArgs e)
        {
            panning = false;
        }

        private void BlockContextMenuEditDetails(object sender, EventArgs e)
        {
            panning = false;
            new BlockDetailsForm(selectedBlocks, lastMouseLocation);
        }

        private void MainView_MouseUp(object sender, MouseEventArgs e)
        {
            if (!mouseHasMovedSinceMouseDown)
            {
                handleRightMouseUp(e.Location);
            }
            panning = false;
            dragging = false;
        }

        private void handleRightMouseUp(System.Drawing.Point mouseLocation)
        {
            bool toggle = false; //TODO: add support for Ctrl to toggle selection of a texture.
            switch (mainForm.EditingMode)
            {
                case EditingModes.Block:
                    updateSelectedBlocks(mouseLocation, toggle);
                    if (selectedBlocks.Count != 0)
                    {
                        blockContextMenu.Show(this, mouseLocation);
                    }
                    break;
                case EditingModes.Texture:
                    updateSelectedTextures(mouseLocation, toggle);
                    if (selectedTextures.Count != 0)
                    {
                        textureContextMenu.Show(this, mouseLocation);
                    }
                    break;
            }
        }

        private void handleLeftMouseDown(System.Drawing.Point mouseLocation)
        {
            bool toggle = false;
            switch (mainForm.EditingMode)
            {
                case EditingModes.Block:
                    break;
                case EditingModes.Texture:
                    mouseCoordAtMouseDown = mouseLocation.ToXnaPoint();
                    updateSelectedTextures(mouseLocation, toggle);
                    selectedTextures.ForEach(t => t.CoordAtMouseDown = t.t.Coord);
                    dragging = selectedTextures.Count != 0;
                    break;
            }
        }

        private void MainView_MouseMove(object sender, MouseEventArgs e)
        {
            if (panning || dragging) {
                mouseHasMovedSinceMouseDown = true;
                Point moved = e.Location.ToXnaPoint().Subtract(lastMouseLocation.ToXnaPoint());
                if (panning)
                {
                    camera.Offset = camera.Offset.Add(moved);
                }
                else
                {
                    Point changeInProjectionSpace = e.Location.ToXnaPoint().Subtract(mouseCoordAtMouseDown).Divide(camera.Zoom);
                    selectedTextures.ForEach(tex => tex.t.Coord = tex.CoordAtMouseDown.Add(changeInProjectionSpace));
                }
                lastMouseLocation = e.Location;
                mainForm.Updated();
            }
        }

        public void MainView_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                camera.ZoomIn(e.Location.ToXnaPoint(), Width, Height);
                mainForm.RefreshTitle();
            }
            else if (e.Delta < 0)
            {
                camera.ZoomOut(e.Location.ToXnaPoint(), Width, Height);
                mainForm.RefreshTitle();
            }
            mainForm.Updated();
        }

        public void MainView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                foreach (Texture2DWithDoublePos tex in selectedTextures)
                {
                    mainForm.CurrentFrame.Textures.Remove(tex.t);
                }
                selectedTextures.Clear();
                mainForm.Updated();
            }
        }


        /// <summary>
        /// Updates the field TODO "selectedBlocks" to include the first block that
        /// contains "point". If no block meet this requirement, "selectedTextures" is cleared.
        /// </summary>
        /// <param name="mouseLocation"></param>
        /// <param name="toggle">If true, an already selected texture will be deselected if hit.</param>
        private void updateSelectedBlocks(System.Drawing.Point mouseLocation, bool toggle)
        {
            throw new NotImplementedException();
            Position pos = CoordinateTransform.ScreenToObjectSpace(mouseLocation.ToXnaPoint(), camera, mainForm.CurrentHeight).ToPosition();
            Point mousePosInProjSpace = CoordinateTransform.ScreenToProjSpace(mouseLocation.ToXnaPoint(), camera);

            for (int i = mainForm.CurrentFrame.Textures.Count - 1; i >= 0; i--)
            {
                Texture2DWithPos hitTexture = mainForm.CurrentFrame.Textures[i];
                if (hitTexture.Bounds.Contains(mousePosInProjSpace) &&
                    hitTexture.Texture.GetPixelColor(mousePosInProjSpace.Subtract(hitTexture.Coord)).A == 255)
                {
                    foreach (Texture2DWithDoublePos selectedTexture in selectedTextures)
                    {
                        if (hitTexture == selectedTexture.t)
                        {
                            if (toggle)
                            {
                                selectedTextures.Remove(selectedTexture);
                            }
                            return;
                        }
                    }
                    selectedTextures.Add(new Texture2DWithDoublePos(hitTexture));
                    return;
                }
            }

            selectedTextures.Clear();
            mainForm.Updated();
        }

        /// <summary>
        /// Updates the field "selectedTextures" to include the first texture that
        /// contains "point" and isn't transparent at that pixel. If no texture meet
        /// these requirements, "selectedTextures" is cleared.
        /// </summary>
        /// <param name="mouseLocation"></param>
        /// <param name="toggle">If true, an already selected texture will be deselected if hit.</param>
        private void updateSelectedTextures(System.Drawing.Point mouseLocation, bool toggle)
        {
            Point mousePosInProjSpace = CoordinateTransform.ScreenToProjSpace(mouseLocation.ToXnaPoint(), camera);

            for (int i = mainForm.CurrentFrame.Textures.Count - 1; i >= 0; i--)
            {
                Texture2DWithPos hitTexture = mainForm.CurrentFrame.Textures[i];
                if (hitTexture.Bounds.Contains(mousePosInProjSpace) &&
                    hitTexture.Texture.GetPixelColor(mousePosInProjSpace.Subtract(hitTexture.Coord)).A == 255)
                {
                    foreach (Texture2DWithDoublePos selectedTexture in selectedTextures)
                    {
                        if (hitTexture == selectedTexture.t)
                        {
                            if (toggle)
                            {
                                selectedTextures.Remove(selectedTexture);
                            }
                            return;
                        }
                    }
                    selectedTextures.Add(new Texture2DWithDoublePos(hitTexture));
                    return;
                }
            }

            selectedTextures.Clear();
            mainForm.Updated();
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
