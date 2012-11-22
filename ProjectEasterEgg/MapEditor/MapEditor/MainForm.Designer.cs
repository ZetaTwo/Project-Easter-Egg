namespace Mindstep.EasterEgg.MapEditor
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.drawTextureIndices = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSelectBackgroundColor = new System.Windows.Forms.ToolStripButton();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItemFile = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.importFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.backgroundColorDialog = new System.Windows.Forms.ColorDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripEditBlocks = new System.Windows.Forms.ToolStripButton();
            this.toolStripEditTextures = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripDrawBlockSolid = new System.Windows.Forms.ToolStripButton();
            this.toolStripDrawBlockWireframe = new System.Windows.Forms.ToolStripButton();
            this.toolStripDrawBlockNone = new System.Windows.Forms.ToolStripButton();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripCoordX = new System.Windows.Forms.ToolStripLabel();
            this.toolStripCoordY = new System.Windows.Forms.ToolStripLabel();
            this.toolStripCoordZ = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.blockViewWrapperControl = new Mindstep.EasterEgg.MapEditor.BlockViewWrapperControl();
            this.textureOpacityTrackBar = new Mindstep.EasterEgg.MapEditor.ToolStripTrackBarItem();
            this.toolStrip.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.AllowMerge = false;
            this.toolStrip.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.toolStrip.CanOverflow = false;
            this.toolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.drawTextureIndices,
            this.toolStripButtonSelectBackgroundColor,
            this.toolStripSeparator1,
            this.textureOpacityTrackBar});
            this.toolStrip.Location = new System.Drawing.Point(149, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Padding = new System.Windows.Forms.Padding(0);
            this.toolStrip.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStrip.Size = new System.Drawing.Size(228, 27);
            this.toolStrip.TabIndex = 15;
            this.toolStrip.Text = "toolStrip";
            // 
            // drawTextureIndices
            // 
            this.drawTextureIndices.Checked = true;
            this.drawTextureIndices.CheckOnClick = true;
            this.drawTextureIndices.CheckState = System.Windows.Forms.CheckState.Checked;
            this.drawTextureIndices.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.drawTextureIndices.Image = ((System.Drawing.Image)(resources.GetObject("drawTextureIndices.Image")));
            this.drawTextureIndices.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.drawTextureIndices.Margin = new System.Windows.Forms.Padding(5, 1, 1, 2);
            this.drawTextureIndices.Name = "drawTextureIndices";
            this.drawTextureIndices.Size = new System.Drawing.Size(24, 24);
            this.drawTextureIndices.Text = "Draw Texture Indices";
            this.drawTextureIndices.Click += new System.EventHandler(this.drawTextureIndices_Click);
            // 
            // toolStripButtonSelectBackgroundColor
            // 
            this.toolStripButtonSelectBackgroundColor.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.toolStripButtonSelectBackgroundColor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSelectBackgroundColor.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSelectBackgroundColor.Image")));
            this.toolStripButtonSelectBackgroundColor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSelectBackgroundColor.Margin = new System.Windows.Forms.Padding(1, 1, 1, 2);
            this.toolStripButtonSelectBackgroundColor.Name = "toolStripButtonSelectBackgroundColor";
            this.toolStripButtonSelectBackgroundColor.Size = new System.Drawing.Size(24, 24);
            this.toolStripButtonSelectBackgroundColor.Text = "toolStripButton1";
            this.toolStripButtonSelectBackgroundColor.ToolTipText = "Select Background Color";
            this.toolStripButtonSelectBackgroundColor.Click += new System.EventHandler(this.toolStripButtonSelectBackgroundColor_Click);
            // 
            // menuStrip
            // 
            this.menuStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemFile});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(43, 24);
            this.menuStrip.TabIndex = 16;
            this.menuStrip.Text = "menuStrip";
            this.menuStrip.Visible = false;
            this.menuStrip.MenuActivate += new System.EventHandler(this.menuStrip_MenuActivate);
            this.menuStrip.MenuDeactivate += new System.EventHandler(this.menuStrip_MenuDeactivate);
            // 
            // toolStripMenuItemFile
            // 
            this.toolStripMenuItemFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripMenuItem1,
            this.openToolStripMenuItem,
            this.importToolStripMenuItem});
            this.toolStripMenuItemFile.Name = "toolStripMenuItemFile";
            this.toolStripMenuItemFile.Size = new System.Drawing.Size(35, 20);
            this.toolStripMenuItemFile.Text = "File";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.saveAsToolStripMenuItem.Text = "Save As";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(178, 6);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.openToolStripMenuItem.Text = "Open Model";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.importToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.importToolStripMenuItem.Text = "Import";
            this.importToolStripMenuItem.Click += new System.EventHandler(this.importToolStripMenuItem_Click);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Filter = "Egg model files (*.egg)|*.egg";
            this.saveFileDialog.Title = "Save As";
            this.saveFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog_FileOk);
            // 
            // importFileDialog
            // 
            this.importFileDialog.Filter = "png (*.png)|*.png|Egg model files (*.egg)|*.egg";
            this.importFileDialog.Multiselect = true;
            this.importFileDialog.Title = "Import Textures and Sub Models";
            this.importFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.importFileDialog_FileOk);
            // 
            // backgroundColorDialog
            // 
            this.backgroundColorDialog.AnyColor = true;
            this.backgroundColorDialog.Color = System.Drawing.Color.Red;
            this.backgroundColorDialog.FullOpen = true;
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "Egg model files (*.egg)|*.egg";
            this.openFileDialog.Title = "Open Model";
            this.openFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog_FileOk);
            // 
            // toolStripContainer1
            // 
            this.toolStripContainer1.BottomToolStripPanelVisible = false;
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.AutoScroll = true;
            this.toolStripContainer1.ContentPanel.Controls.Add(this.blockViewWrapperControl);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(1092, 692);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.RightToolStripPanelVisible = false;
            this.toolStripContainer1.Size = new System.Drawing.Size(1092, 719);
            this.toolStripContainer1.TabIndex = 21;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.menuStrip);
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip);
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip2);
            // 
            // toolStrip1
            // 
            this.toolStrip1.AllowMerge = false;
            this.toolStrip1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.toolStrip1.CanOverflow = false;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripEditBlocks,
            this.toolStripEditTextures,
            this.toolStripSeparator5,
            this.toolStripDrawBlockSolid,
            this.toolStripDrawBlockWireframe,
            this.toolStripDrawBlockNone});
            this.toolStrip1.Location = new System.Drawing.Point(3, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0);
            this.toolStrip1.Size = new System.Drawing.Size(146, 27);
            this.toolStrip1.TabIndex = 16;
            // 
            // toolStripEditBlocks
            // 
            this.toolStripEditBlocks.CheckOnClick = true;
            this.toolStripEditBlocks.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripEditBlocks.Image = ((System.Drawing.Image)(resources.GetObject("toolStripEditBlocks.Image")));
            this.toolStripEditBlocks.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripEditBlocks.Margin = new System.Windows.Forms.Padding(1, 1, 1, 2);
            this.toolStripEditBlocks.Name = "toolStripEditBlocks";
            this.toolStripEditBlocks.Size = new System.Drawing.Size(24, 24);
            this.toolStripEditBlocks.Text = "toolStripButton1";
            this.toolStripEditBlocks.ToolTipText = "Enter block editing mode";
            // 
            // toolStripEditTextures
            // 
            this.toolStripEditTextures.CheckOnClick = true;
            this.toolStripEditTextures.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripEditTextures.Image = ((System.Drawing.Image)(resources.GetObject("toolStripEditTextures.Image")));
            this.toolStripEditTextures.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripEditTextures.Margin = new System.Windows.Forms.Padding(1, 1, 1, 2);
            this.toolStripEditTextures.Name = "toolStripEditTextures";
            this.toolStripEditTextures.Size = new System.Drawing.Size(24, 24);
            this.toolStripEditTextures.Text = "toolStripButton2";
            this.toolStripEditTextures.ToolTipText = "Enter texture editing mode";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripDrawBlockSolid
            // 
            this.toolStripDrawBlockSolid.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDrawBlockSolid.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDrawBlockSolid.Image")));
            this.toolStripDrawBlockSolid.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDrawBlockSolid.Margin = new System.Windows.Forms.Padding(1, 1, 1, 2);
            this.toolStripDrawBlockSolid.Name = "toolStripDrawBlockSolid";
            this.toolStripDrawBlockSolid.Size = new System.Drawing.Size(24, 24);
            this.toolStripDrawBlockSolid.Text = "toolStripButton1";
            this.toolStripDrawBlockSolid.ToolTipText = "Draw blocks as solid";
            // 
            // toolStripDrawBlockWireframe
            // 
            this.toolStripDrawBlockWireframe.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDrawBlockWireframe.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDrawBlockWireframe.Image")));
            this.toolStripDrawBlockWireframe.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDrawBlockWireframe.Margin = new System.Windows.Forms.Padding(1, 1, 1, 2);
            this.toolStripDrawBlockWireframe.Name = "toolStripDrawBlockWireframe";
            this.toolStripDrawBlockWireframe.Size = new System.Drawing.Size(24, 24);
            this.toolStripDrawBlockWireframe.Text = "toolStripButton2";
            this.toolStripDrawBlockWireframe.ToolTipText = "Draw blocks as wireframes";
            // 
            // toolStripDrawBlockNone
            // 
            this.toolStripDrawBlockNone.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDrawBlockNone.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDrawBlockNone.Image")));
            this.toolStripDrawBlockNone.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDrawBlockNone.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.toolStripDrawBlockNone.Name = "toolStripDrawBlockNone";
            this.toolStripDrawBlockNone.Size = new System.Drawing.Size(24, 25);
            this.toolStripDrawBlockNone.Text = "toolStripButton1";
            this.toolStripDrawBlockNone.ToolTipText = "Don\'t draw blocks";
            // 
            // toolStrip2
            // 
            this.toolStrip2.AllowMerge = false;
            this.toolStrip2.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.toolStrip2.CanOverflow = false;
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripCoordX,
            this.toolStripCoordY,
            this.toolStripCoordZ});
            this.toolStrip2.Location = new System.Drawing.Point(377, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Padding = new System.Windows.Forms.Padding(0);
            this.toolStrip2.Size = new System.Drawing.Size(97, 25);
            this.toolStrip2.TabIndex = 17;
            // 
            // toolStripCoordX
            // 
            this.toolStripCoordX.AutoSize = false;
            this.toolStripCoordX.ForeColor = System.Drawing.Color.White;
            this.toolStripCoordX.Margin = new System.Windows.Forms.Padding(0, 6, 0, 2);
            this.toolStripCoordX.Name = "toolStripCoordX";
            this.toolStripCoordX.Size = new System.Drawing.Size(30, 13);
            this.toolStripCoordX.Text = "X:0";
            this.toolStripCoordX.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripCoordY
            // 
            this.toolStripCoordY.AutoSize = false;
            this.toolStripCoordY.ForeColor = System.Drawing.Color.White;
            this.toolStripCoordY.Margin = new System.Windows.Forms.Padding(0, 6, 0, 2);
            this.toolStripCoordY.Name = "toolStripCoordY";
            this.toolStripCoordY.Size = new System.Drawing.Size(35, 13);
            this.toolStripCoordY.Text = "Y:0";
            this.toolStripCoordY.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripCoordZ
            // 
            this.toolStripCoordZ.AutoSize = false;
            this.toolStripCoordZ.ForeColor = System.Drawing.Color.White;
            this.toolStripCoordZ.Margin = new System.Windows.Forms.Padding(0, 6, 0, 2);
            this.toolStripCoordZ.Name = "toolStripCoordZ";
            this.toolStripCoordZ.Size = new System.Drawing.Size(30, 13);
            this.toolStripCoordZ.Text = "Z:0";
            this.toolStripCoordZ.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // blockViewWrapperControl
            // 
            this.blockViewWrapperControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.blockViewWrapperControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.blockViewWrapperControl.EditingMode = Mindstep.EasterEgg.MapEditor.EditingMode.Block;
            this.blockViewWrapperControl.Location = new System.Drawing.Point(0, 0);
            this.blockViewWrapperControl.MainForm = null;
            this.blockViewWrapperControl.Name = "blockViewWrapperControl";
            this.blockViewWrapperControl.Size = new System.Drawing.Size(1092, 692);
            this.blockViewWrapperControl.TabIndex = 20;
            // 
            // textureOpacityTrackBar
            // 
            this.textureOpacityTrackBar.AutoSize = false;
            this.textureOpacityTrackBar.Cursor = System.Windows.Forms.Cursors.Default;
            this.textureOpacityTrackBar.LargeChange = 100;
            this.textureOpacityTrackBar.Maximum = 1000;
            this.textureOpacityTrackBar.Minimum = 0;
            this.textureOpacityTrackBar.Name = "toolStripTrackBarItem1";
            this.textureOpacityTrackBar.Size = new System.Drawing.Size(154, 24);
            this.textureOpacityTrackBar.SmallChange = 1;
            this.textureOpacityTrackBar.TabIndex = 0;
            this.textureOpacityTrackBar.TabStop = true;
            this.textureOpacityTrackBar.Text = "toolStripTrackBarItem1";
            this.textureOpacityTrackBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.textureOpacityTrackBar.Value = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1092, 719);
            this.Controls.Add(this.toolStripContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Easter Egg Editor";
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton drawTextureIndices;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFile;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog importFileDialog;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButtonSelectBackgroundColor;
        private System.Windows.Forms.ColorDialog backgroundColorDialog;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private BlockViewWrapperControl blockViewWrapperControl;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private ToolStripTrackBarItem textureOpacityTrackBar;
        private System.Windows.Forms.ToolStripButton toolStripEditBlocks;
        private System.Windows.Forms.ToolStripButton toolStripEditTextures;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton toolStripDrawBlockSolid;
        private System.Windows.Forms.ToolStripButton toolStripDrawBlockWireframe;
        private System.Windows.Forms.ToolStripButton toolStripDrawBlockNone;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripLabel toolStripCoordX;
        private System.Windows.Forms.ToolStripLabel toolStripCoordY;
        private System.Windows.Forms.ToolStripLabel toolStripCoordZ;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}

