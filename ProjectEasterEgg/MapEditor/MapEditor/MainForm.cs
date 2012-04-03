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
            mainView.MainForm = this;
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

        private void topView_MouseMove(object sender, MouseEventArgs e)
        {
            Point p = topView.getClosestBlockCoord(e.Location.toXnaPoint());
            coords.Text = "X:" + p.X + "   Y:" + p.Y;
        }

        private void topView_Click(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                topView.toggleBlock(topView.getClosestBlockCoord(e.Location.toXnaPoint()));
            }
        }

        private void MainForm_Scroll(object sender, ScrollEventArgs e)
        {
            System.Console.WriteLine(e.NewValue);
        }

        private void topView_MouseLeave(object sender, System.EventArgs e)
        {
            coords.Text = "";
        }

        private void showTopView_CheckChanged(object sender, System.EventArgs e)
        {
            topViewPanel.Visible = showTopView.Checked;
        }
    }
}
