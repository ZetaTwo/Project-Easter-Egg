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
        private bool panning;
        private System.Drawing.Point lastMouseLocation;
        private SamplerState samplerState;

        private Camera camera;
        public float Zoom { get { return camera.Zoom; } }
        private Texture2DWithPos dragging;
        private SpriteFont spriteFont;
        public bool drawTextureIndices = true;
        private Point textureCoordAtMouseDown;
        private Point mouseCoordAtMouseDown;

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

            samplerState = new SamplerState();
            samplerState.Filter = TextureFilter.PointMipLinear;
            samplerState.AddressU = TextureAddressMode.Clamp;
            samplerState.AddressV = TextureAddressMode.Clamp;

            // Hook the idle event to constantly redraw our animation.
            Application.Idle += delegate { Invalidate(); };
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
                        case 0:
                            color = Color.Green;
                            break;
                        case 1:
                            color = Color.Blue;
                            break;
                        case 2:
                            color = Color.Brown;
                            break;
                        case 3:
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

            for (int i = 0; i < mainForm.AnimationManager.CurrentFrame.Textures.Count; i++)
            {

                Texture2DWithPos tex = mainForm.AnimationManager.CurrentFrame.Textures[i];
                float depth = 0.1f * (1 - (float)i / mainForm.AnimationManager.CurrentFrame.Textures.Count);
                spriteBatch.Draw(tex.Texture, tex.Coord.ToVector2(), null, Color.White, 0, Vector2.Zero, 1, spriteEffect, depth / camera.Zoom);
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
                if (dragging != null) //pressing the other mouse button resets the textures position
                {
                    dragging.Coord = textureCoordAtMouseDown;
                    dragging = null;
                }
                else
                {
                    panning = true;
                }
            }
            else if (e.Button == MouseButtons.Left)
            {
                if (panning) //pressing the other mouse button cancels the panning and the dragging doesn't start
                {
                    panning = false;
                }
                else
                {
                    for (int i = mainForm.AnimationManager.CurrentFrame.Textures.Count - 1; i >= 0; i--)
                    {
                        Texture2DWithPos tex = mainForm.AnimationManager.CurrentFrame.Textures[i];
                        Point mousePosInProjSpace = CoordinateTransform.ScreenToProjSpace(e.Location.toXnaPoint(), camera);
                        if (tex.Rectangle.Contains(mousePosInProjSpace))
                        {
                            dragging = tex;
                            textureCoordAtMouseDown = dragging.Coord;
                            mouseCoordAtMouseDown = e.Location.toXnaPoint();
                            break;
                        }
                    }
                }
            }
            lastMouseLocation = e.Location;
        }

        private void MainView_MouseUp(object sender, MouseEventArgs e)
        {
            panning = false;
            dragging = null;
        }

        private void MainView_MouseMove(object sender, MouseEventArgs e)
        {
            if (panning || dragging != null) {
                int movedX = e.Location.X - lastMouseLocation.X;
                int movedY = e.Location.Y - lastMouseLocation.Y;
                if (panning)
                {
                    camera.Offset = camera.Offset.Add(new Point(movedX, movedY));
                }
                else
                {
                    Point changeInProjectionSpace = e.Location.toXnaPoint().Subtract(mouseCoordAtMouseDown).Divide(camera.Zoom);
                    dragging.Coord = textureCoordAtMouseDown.Add(changeInProjectionSpace);
                }
                lastMouseLocation = e.Location;
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
        }
    }
}
