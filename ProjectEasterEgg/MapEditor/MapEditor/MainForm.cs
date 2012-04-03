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

    
    /// <summary>
    /// Custom form provides the main user interface for the program.
    /// In this sample we used the designer to add a splitter pane to the form,
    /// which contains a SpriteFontControl and a SpinningTriangleControl.
    /// </summary>
    public partial class MainForm : Form
    {
        public int CurrentHeight { get { return topViewHeight; } }

        public MainForm()
        {
            InitializeComponent();
            topView.MainForm = this;
        }

        public List<Block> Blocks = new List<Block>();
        private int topViewHeight = 0;

        private void upButton_Click(object sender, System.EventArgs e)
        {
            topViewHeight++;
            layer.Text = topViewHeight.ToString();
        }

        private void downButton_Click(object sender, System.EventArgs e)
        {
            topViewHeight--;
            layer.Text = topViewHeight.ToString();
        }

        private void topView_Click(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                topView.toggleBlock(topView.getClosestGridPoint(e.Location));
            }
        }

        private void MainForm_Scroll(object sender, ScrollEventArgs e)
        {
            System.Console.WriteLine(e.NewValue);
        }

        private void topView_MouseMove(object sender, MouseEventArgs e)
        {
            System.Drawing.Point p = topView.getClosestGridPoint(e.Location);
            coords.Text = string.Format("X: {0,-5} Y: {0,-5:G}", p.X, p.Y);
        }
    }
}
