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

namespace MapEditor
{
    // System.Drawing and the XNA Framework both define Color types.
    // To avoid conflicts, we define shortcut names for them both.
    using GdiColor = System.Drawing.Color;
    using XnaColor = Microsoft.Xna.Framework.Color;

    
    /// <summary>
    /// Custom form provides the main user interface for the program.
    /// In this sample we used the designer to add a splitter pane to the form,
    /// which contains a SpriteFontControl and a SpinningTriangleControl.
    /// </summary>
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            vertexColor1.SelectedIndex = 1;
            vertexColor2.SelectedIndex = 2;
            vertexColor3.SelectedIndex = 4;
        }


        /// <summary>
        /// Event handler updates the spinning triangle control when
        /// one of the three vertex color combo boxes is altered.
        /// </summary>
        void vertexColor_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            // Which vertex was changed?
            int vertexIndex;

            if (sender == vertexColor1)
                vertexIndex = 0;
            else if (sender == vertexColor2)
                vertexIndex = 1;
            else if (sender == vertexColor3)
                vertexIndex = 2;
            else
                return;

            // Which color was selected?
            ComboBox combo = (ComboBox)sender;

            string colorName = combo.SelectedItem.ToString();

            GdiColor gdiColor = GdiColor.FromName(colorName);

            XnaColor xnaColor = new XnaColor(gdiColor.R, gdiColor.G, gdiColor.B);
        }

        private void mouseWheel(object sender, MouseEventArgs e)
        {
            System.Console.WriteLine(e.Delta);
        }

        private void spinningTriangleControl_MouseDown(object sender, MouseEventArgs e)
        {
            System.Console.WriteLine(e.Delta);
        }

        private void blocksize_ValueChanged(object sender, System.EventArgs e)
        {
            System.Console.WriteLine("change grid size if locked");
        }

        private void createblock_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                topView.draggingBlock = true;
            }
        }

        private void createblock_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                topView.draggingBlock = false;
                topView.placeBlock();
            }
        }
    }
}
