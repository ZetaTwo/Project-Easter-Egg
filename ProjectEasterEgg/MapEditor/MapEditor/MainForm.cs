#region File Description
//-----------------------------------------------------------------------------
// MainForm.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System.Windows.Forms;
using System.Linq;
#endregion

namespace Mindstep.EasterEgg.MapEditor
{
    // System.Drawing and the XNA Framework both define Color types.
    // To avoid conflicts, we define shortcut names for them both.
    using GdiColor = System.Drawing.Color;
    using XnaColor = Microsoft.Xna.Framework.Color;
    using Microsoft.Xna.Framework;
    using Mindstep.EasterEgg.Commons;
    using System.Collections.Generic;
    using System;
using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using System.IO;
    using Mindstep.EasterEgg.MapEditor.Animations;

    
    /// <summary>
    /// Custom form provides the main user interface for the program.
    /// In this sample we used the designer to add a splitter pane to the form,
    /// which contains a SpriteFontControl and a SpinningTriangleControl.
    /// </summary>
    public partial class MainForm : Form
    {
        private static string TITLE = "Easter Egg Editor - ";
        private string lastSavedDoc;
        private int currentHeight = 0;
        public int CurrentHeight
        {
            get { return currentHeight; }
            set
            {
                currentHeight = value;
                layer.Text = currentHeight.ToString();
            }
        }

        public List<SaveBlock> SaveBlocks = new List<SaveBlock>();
        public AnimationManager AnimationManager = new AnimationManager();
        public Frame CurrentFrame { get { return AnimationManager.CurrentAnimation.CurrentFrame; } }
        public Animation CurrentAnimation { get { return AnimationManager.CurrentAnimation; } }
        public bool changedSinceLastSave;

        public readonly ServiceContainer Services;
        public readonly ContentManager Content;
        private GraphicsDeviceService graphicsDeviceService;

        /// <summary>
        /// Gets a GraphicsDevice that can be used to draw onto this control.
        /// </summary>
        public GraphicsDevice GraphicsDevice
        {
            get { return graphicsDeviceService.GraphicsDevice; }
        }

        public MainForm()
        {
            Services = new ServiceContainer();
            Content = new ContentManager(Services, "MapEditorContent");
            SetupContentManager();

            InitializeComponent();
            topView.Initialize(this);
            mainView.Initialize(this);
            MouseWheel += new MouseEventHandler(mouseWheel);
            RefreshTitle();
            AnimationManager.setCurrentAnimation("still");
        }

        private void SetupContentManager()
        {
            // Don't initialize the graphics device if we are running in the designer.
            if (!DesignMode)
            {
                graphicsDeviceService = GraphicsDeviceService.AddRef(Handle,
                                                                     ClientSize.Width,
                                                                     ClientSize.Height);

                // Register the service, so components like ContentManager can find it.
                Services.AddService<IGraphicsDeviceService>(graphicsDeviceService);
            }
        }

        private void upButton_Click(object sender, System.EventArgs e)
        {
            CurrentHeight++;
        }

        private void downButton_Click(object sender, System.EventArgs e)
        {
            CurrentHeight--;
        }

        #region zoom (mousewheel)
        private void mouseWheel(object sender, MouseEventArgs e)
        {
            if (GetChildAtPoint(e.Location) == topViewPanel)
            {
                topView.MainView_MouseWheel(sender, e);
            }
            else if (GetChildAtPoint(e.Location) == mainView)
            {
                mainView.MainView_MouseWheel(sender, e);
            }
        }
        #endregion

        public void RefreshTitle()
        {
            string doc = lastSavedDoc;
            if (doc == null)
            {
                doc = "Untitled";
            }
            if (changedSinceLastSave)
            {
                doc += "*";
            }
            Text = TITLE + doc.Split(' ').Last() + " [" + Math.Round(mainView.Zoom*100, 0) + "%]";
        }


        private void topView_MouseLeave(object sender, System.EventArgs e)
        {
            coords.Text = "";
        }

        private void showTopView_CheckChanged(object sender, System.EventArgs e)
        {
            topViewPanel.Visible = showTopView.Checked;
        }

        internal void setTopViewCoordLabel(string s)
        {
            coords.Text = s;
        }

        #region save
        private void saveToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            if (lastSavedDoc == null)
            {
                saveAsClicked();
            }
            else
            {
                saveClicked();
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            saveAsClicked();
        }

        private void saveClicked()
        {
            if (SaveBlocks.Count == 0)
            {
                MessageBox.Show("You can't save an empty model!", "Save error");
            }
            else
            {
                save();
            }
        }

        private void saveAsClicked()
        {
            if (SaveBlocks.Count == 0)
            {
                MessageBox.Show("You can't save an empty model!", "Save error");
            }
            else
            {
                saveFileDialog.ShowDialog();
            }
        }

        private void saveFileDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            save();
        }

        private void save()
        {
            EggModelExporter.SaveModel(SaveBlocks, AnimationManager.Animations, saveFileDialog.FileName);
            lastSavedDoc = saveFileDialog.FileName;
            changedSinceLastSave = false;
            RefreshTitle();
        }
        #endregion

        #region import
        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            importFileDialog.ShowDialog();
        }

        private void importFileDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            importImage(importFileDialog.FileNames);
        }

        private void importImage(string[] fileNames)
        {
            foreach (string fileName in fileNames)
            {
                importImage(fileName);
            }
        }

        private Point lastImportedTextureOffset = new Point(100, 100);
        private void importImage(string fileName)
        {
            Texture2DWithPos tex = new Texture2DWithPos(fileName);

            foreach (Texture2DWithPos existingTex in AnimationManager.Animations.GetAllTextures())
            {
                if (existingTex.RelativePath == tex.RelativePath && existingTex.OriginalPath != tex.OriginalPath)
                {
                    MessageBox.Show("Texture relative path name collision: '"+tex.RelativePath+
                        "', while original path name was not the same: '"+tex.OriginalPath+
                        "'!", "It should be possible to click on a texture and "+
                        "specify it's name.\nMake such a box popup here!");
                    return;
                }
            }
            Point textureOffset;
            if (lastImportedTextureOffset.X > 400)
            {
                textureOffset = (lastImportedTextureOffset.ToVector2() + new Vector2(50, 50)).ToPoint();
            }
            else
            {
                textureOffset = new Point(100, 100);
            }
            tex.Coord = textureOffset;//ScreenToProjectionSpace(textureOffset);
            
            tex.Texture = Texture2D.FromStream(GraphicsDevice, new FileStream(fileName, FileMode.Open));
            CurrentFrame.Textures.Add(tex);
            Updated();
        }
        #endregion

        internal bool DrawTextureIndices()
        {
            return drawTextureIndices.Checked;
        }

        internal void Updated()
        {
            mainView.Invalidate();
            topView.Invalidate();
        }
    }
}
