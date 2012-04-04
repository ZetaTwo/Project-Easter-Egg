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

        public List<Position> BlockPositions = new List<Position>();
        public List<Texture2DWithPos> Textures = new List<Texture2DWithPos>();
        public bool changedSinceLastSave;

        public MainForm()
        {
            InitializeComponent();
            topView.MainForm = this;
            mainView.MainForm = this;
            MouseWheel += new MouseEventHandler(mouseWheel);
            RefreshTitle();
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

        private void toolStripButton1_CheckedChanged(object sender, System.EventArgs e)
        {
            if (toolStripButton1.Checked)
            {
                mainView.Load("mainBlock31", "mainGrid31");
            }
            else
            {
                mainView.Load("mainBlock31odd", "mainGrid31odd");
            }
        }



        #region save buttons
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
            if (BlockPositions.Count == 0)
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
            if (BlockPositions.Count == 0)
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
            Exporter.CompileModel(BlockPositions, Textures, saveFileDialog.FileName);
            lastSavedDoc = saveFileDialog.FileName;
            changedSinceLastSave = false;
            RefreshTitle();
        }
        #endregion

        private void mainView_DragDrop(object sender, DragEventArgs e)
        {
            System.Console.WriteLine("asd");
        }

        internal void setTopViewCoordLabel(string s)
        {
            coords.Text = s;
        }
    }
}
