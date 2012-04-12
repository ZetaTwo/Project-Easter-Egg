#region Using Statements
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Mindstep.EasterEgg.Commons;
using System;
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
        private SpriteBatch spriteBatch;
        private SpriteEffects spriteEffect;
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

        public void Initialize(MainForm mainForm)
        {
            this.mainForm = mainForm;
            camera = new Camera(new Point(Width/2, Height/2));
            spriteBatch = new SpriteBatch(mainForm.GraphicsDevice);
            spriteEffect = SpriteEffects.None;
            spriteFont = mainForm.Content.Load<SpriteFont>("hudFont");

            block = mainForm.Content.Load<Texture2D>("mainBlock31");
            grid = mainForm.Content.Load<Texture2D>("mainGrid31");
            if (!block.Bounds.Equals(grid.Bounds))
            {
                throw new Exception("mainBlock and mainGrid image size mismatch.");
            }

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
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, samplerState, null, null, null, camera.ZoomAndOffsetMatrix);

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
                Color color;
                if (!selectedTextures.TrueForAll(t => t.t != tex))
                {
                    color = Color.Violet;
                }
                else
                {
                    color = Color.White;
                }
                spriteBatch.Draw(tex.Texture, tex.Coord.ToVector2(), null, color, 0, Vector2.Zero, 1, spriteEffect, depth / camera.Zoom);
                if (mainForm.DrawTextureIndices())
                {
                    spriteBatch.DrawString(spriteFont, i.ToString(), tex.Coord.ToVector2(), Color.Green);
                }
            }

            spriteBatch.End();
        }

        private void drawBlock(Texture2D image, BoundingBoxInt boundingBox, Microsoft.Xna.Framework.Color color, Position pos)
        {
            float depth = boundingBox.getRelativeDepthOf(pos);
            Vector2 projCoords = CoordinateTransform.ObjectToProjectionSpace(pos);
            spriteBatch.Draw(image, projCoords, null, color, 0, Vector2.Zero, 1, spriteEffect, depth / camera.Zoom);
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
                    Point mousePosInProjSpace = CoordinateTransform.ScreenToProjSpace(e.Location.toXnaPoint(), camera);
                    mouseCoordAtMouseDown = e.Location.toXnaPoint();

                    //TODO: add support for Ctrl to toggle selection of a texture.

                    foreach (Texture2DWithDoublePos tex in selectedTextures)
                    {
                        if (tex.t.Bounds.Contains(mousePosInProjSpace))
                        {
                            dragging = true;
                        }
                    }

                    if (!dragging) //no currenly selected texture hit
                    {
                        selectedTextures.Clear();
                        for (int i = mainForm.CurrentFrame.Textures.Count - 1; i >= 0; i--)
                        {
                            Texture2DWithPos tex = mainForm.CurrentFrame.Textures[i];
                            if (tex.Bounds.Contains(mousePosInProjSpace))
                            {
                                dragging = true;
                                selectedTextures.Add(new Texture2DWithDoublePos(tex));
                                break;
                            }
                        }
                    }

                    if (dragging)
                    {
                        selectedTextures.ForEach(t => t.CoordAtMouseDown = t.t.Coord);
                    }
                    else //if still no texture has been hit
                    {
                        selectedTextures.Clear();
                    }
                }
            }
            lastMouseLocation = e.Location;
            mainForm.Updated();
        }

        private void MainView_MouseUp(object sender, MouseEventArgs e)
        {
            panning = false;
            dragging = false;
        }

        private void MainView_MouseMove(object sender, MouseEventArgs e)
        {
            if (panning || dragging) {
                int movedX = e.Location.X - lastMouseLocation.X;
                int movedY = e.Location.Y - lastMouseLocation.Y;
                if (panning)
                {
                    camera.Offset = camera.Offset.Add(new Point(movedX, movedY));
                }
                else
                {
                    Point changeInProjectionSpace = e.Location.toXnaPoint().Subtract(mouseCoordAtMouseDown).Divide(camera.Zoom);
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
                camera.ZoomIn(e.Location.toXnaPoint(), Width, Height);
                mainForm.RefreshTitle();
            }
            else if (e.Delta < 0)
            {
                camera.ZoomOut(e.Location.toXnaPoint(), Width, Height);
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
