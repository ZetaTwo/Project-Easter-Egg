namespace Mindstep.EasterEgg.MapEditor
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
            this.showTopView = new System.Windows.Forms.ToolStripButton();
            this.drawTextureIndices = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripEditBlocks = new System.Windows.Forms.ToolStripButton();
            this.toolStripEditTextures = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripDrawBlockSolid = new System.Windows.Forms.ToolStripButton();
            this.toolStripDrawBlockWireframe = new System.Windows.Forms.ToolStripButton();
            this.toolStripDrawBlockNone = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripTextureOpacityLabel = new System.Windows.Forms.ToolStripLabel();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.topViewPanel = new System.Windows.Forms.Panel();
            this.coords = new System.Windows.Forms.Label();
            this.upButton = new System.Windows.Forms.Button();
            this.layerLabel = new System.Windows.Forms.Label();
            this.downButton = new System.Windows.Forms.Button();
            this.layer = new System.Windows.Forms.TextBox();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.importFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.trackBarTextureOpacity = new System.Windows.Forms.TrackBar();
            this.topView = new Mindstep.EasterEgg.MapEditor.TopView();
            this.mainView = new Mindstep.EasterEgg.MapEditor.MainView();
            this.toolStrip.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.topViewPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarTextureOpacity)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showTopView,
            this.drawTextureIndices,
            this.toolStripSeparator1,
            this.toolStripEditBlocks,
            this.toolStripEditTextures,
            this.toolStripSeparator2,
            this.toolStripDrawBlockSolid,
            this.toolStripDrawBlockWireframe,
            this.toolStripDrawBlockNone,
            this.toolStripSeparator3,
            this.toolStripTextureOpacityLabel});
            this.toolStrip.Location = new System.Drawing.Point(0, 24);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStrip.Size = new System.Drawing.Size(1092, 25);
            this.toolStrip.TabIndex = 15;
            this.toolStrip.Text = "toolStrip1";
            // 
            // showTopView
            // 
            this.showTopView.Checked = true;
            this.showTopView.CheckOnClick = true;
            this.showTopView.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showTopView.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.showTopView.Image = ((System.Drawing.Image)(resources.GetObject("showTopView.Image")));
            this.showTopView.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.showTopView.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.showTopView.Name = "showTopView";
            this.showTopView.Size = new System.Drawing.Size(23, 22);
            this.showTopView.Text = "Show Top View";
            this.showTopView.CheckedChanged += new System.EventHandler(this.showTopView_CheckChanged);
            // 
            // drawTextureIndices
            // 
            this.drawTextureIndices.Checked = true;
            this.drawTextureIndices.CheckOnClick = true;
            this.drawTextureIndices.CheckState = System.Windows.Forms.CheckState.Checked;
            this.drawTextureIndices.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.drawTextureIndices.Image = ((System.Drawing.Image)(resources.GetObject("drawTextureIndices.Image")));
            this.drawTextureIndices.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.drawTextureIndices.Name = "drawTextureIndices";
            this.drawTextureIndices.Size = new System.Drawing.Size(23, 22);
            this.drawTextureIndices.Text = "Draw Texture Indices";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripEditBlocks
            // 
            this.toolStripEditBlocks.CheckOnClick = true;
            this.toolStripEditBlocks.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripEditBlocks.Image = ((System.Drawing.Image)(resources.GetObject("toolStripEditBlocks.Image")));
            this.toolStripEditBlocks.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripEditBlocks.Name = "toolStripEditBlocks";
            this.toolStripEditBlocks.Size = new System.Drawing.Size(23, 22);
            this.toolStripEditBlocks.Text = "toolStripButton1";
            this.toolStripEditBlocks.ToolTipText = "Enter block editing mode";
            this.toolStripEditBlocks.Click += new System.EventHandler(this.toolStripEditBlocks_Click);
            // 
            // toolStripEditTextures
            // 
            this.toolStripEditTextures.CheckOnClick = true;
            this.toolStripEditTextures.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripEditTextures.Image = ((System.Drawing.Image)(resources.GetObject("toolStripEditTextures.Image")));
            this.toolStripEditTextures.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripEditTextures.Name = "toolStripEditTextures";
            this.toolStripEditTextures.Size = new System.Drawing.Size(23, 22);
            this.toolStripEditTextures.Text = "toolStripButton2";
            this.toolStripEditTextures.ToolTipText = "Enter texture editing mode";
            this.toolStripEditTextures.Click += new System.EventHandler(this.toolStripEditTextures_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripDrawBlockSolid
            // 
            this.toolStripDrawBlockSolid.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDrawBlockSolid.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDrawBlockSolid.Image")));
            this.toolStripDrawBlockSolid.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDrawBlockSolid.Name = "toolStripDrawBlockSolid";
            this.toolStripDrawBlockSolid.Size = new System.Drawing.Size(23, 22);
            this.toolStripDrawBlockSolid.Text = "toolStripButton1";
            this.toolStripDrawBlockSolid.ToolTipText = "Draw blocks as solid";
            this.toolStripDrawBlockSolid.Click += new System.EventHandler(this.toolStripDrawBlockSolid_Click);
            // 
            // toolStripDrawBlockWireframe
            // 
            this.toolStripDrawBlockWireframe.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDrawBlockWireframe.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDrawBlockWireframe.Image")));
            this.toolStripDrawBlockWireframe.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDrawBlockWireframe.Name = "toolStripDrawBlockWireframe";
            this.toolStripDrawBlockWireframe.Size = new System.Drawing.Size(23, 22);
            this.toolStripDrawBlockWireframe.Text = "toolStripButton2";
            this.toolStripDrawBlockWireframe.ToolTipText = "Draw blocks as wireframes";
            this.toolStripDrawBlockWireframe.Click += new System.EventHandler(this.toolStripDrawBlockWireframe_Click);
            // 
            // toolStripDrawBlockNone
            // 
            this.toolStripDrawBlockNone.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDrawBlockNone.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDrawBlockNone.Image")));
            this.toolStripDrawBlockNone.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDrawBlockNone.Name = "toolStripDrawBlockNone";
            this.toolStripDrawBlockNone.Size = new System.Drawing.Size(23, 22);
            this.toolStripDrawBlockNone.Text = "toolStripButton1";
            this.toolStripDrawBlockNone.ToolTipText = "Don\'t draw blocks";
            this.toolStripDrawBlockNone.Click += new System.EventHandler(this.toolStripDrawBlockNone_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripTextureOpacityLabel
            // 
            this.toolStripTextureOpacityLabel.Name = "toolStripTextureOpacityLabel";
            this.toolStripTextureOpacityLabel.Size = new System.Drawing.Size(85, 22);
            this.toolStripTextureOpacityLabel.Text = "Texture Opacity";
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1092, 24);
            this.menuStrip.TabIndex = 16;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.importToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "File";
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
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.importToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.importToolStripMenuItem.Text = "Import";
            this.importToolStripMenuItem.Click += new System.EventHandler(this.importToolStripMenuItem_Click);
            // 
            // topViewPanel
            // 
            this.topViewPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.topViewPanel.Controls.Add(this.topView);
            this.topViewPanel.Controls.Add(this.coords);
            this.topViewPanel.Controls.Add(this.upButton);
            this.topViewPanel.Controls.Add(this.layerLabel);
            this.topViewPanel.Controls.Add(this.downButton);
            this.topViewPanel.Controls.Add(this.layer);
            this.topViewPanel.Location = new System.Drawing.Point(860, 49);
            this.topViewPanel.Name = "topViewPanel";
            this.topViewPanel.Size = new System.Drawing.Size(232, 208);
            this.topViewPanel.TabIndex = 18;
            // 
            // coords
            // 
            this.coords.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.coords.Location = new System.Drawing.Point(131, 190);
            this.coords.Name = "coords";
            this.coords.Size = new System.Drawing.Size(98, 13);
            this.coords.TabIndex = 12;
            // 
            // upButton
            // 
            this.upButton.Image = ((System.Drawing.Image)(resources.GetObject("upButton.Image")));
            this.upButton.Location = new System.Drawing.Point(3, 16);
            this.upButton.Name = "upButton";
            this.upButton.Size = new System.Drawing.Size(24, 24);
            this.upButton.TabIndex = 8;
            this.upButton.UseVisualStyleBackColor = true;
            this.upButton.Click += new System.EventHandler(this.upButton_Click);
            // 
            // layerLabel
            // 
            this.layerLabel.AutoSize = true;
            this.layerLabel.Location = new System.Drawing.Point(0, 0);
            this.layerLabel.Name = "layerLabel";
            this.layerLabel.Size = new System.Drawing.Size(33, 13);
            this.layerLabel.TabIndex = 11;
            this.layerLabel.Text = "Layer";
            // 
            // downButton
            // 
            this.downButton.Image = ((System.Drawing.Image)(resources.GetObject("downButton.Image")));
            this.downButton.Location = new System.Drawing.Point(3, 72);
            this.downButton.Name = "downButton";
            this.downButton.Size = new System.Drawing.Size(24, 24);
            this.downButton.TabIndex = 9;
            this.downButton.UseVisualStyleBackColor = true;
            this.downButton.Click += new System.EventHandler(this.downButton_Click);
            // 
            // layer
            // 
            this.layer.Location = new System.Drawing.Point(3, 46);
            this.layer.Name = "layer";
            this.layer.ReadOnly = true;
            this.layer.Size = new System.Drawing.Size(24, 20);
            this.layer.TabIndex = 10;
            this.layer.Text = "0";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Filter = "Egg model files (*.egg)|*.egg";
            this.saveFileDialog.Title = "Save As";
            this.saveFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog_FileOk);
            // 
            // importFileDialog
            // 
            this.importFileDialog.Filter = "PNG (*.png)|*.png";
            this.importFileDialog.Multiselect = true;
            this.importFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.importFileDialog_FileOk);
            // 
            // trackBarTextureOpacity
            // 
            this.trackBarTextureOpacity.AutoSize = false;
            this.trackBarTextureOpacity.BackColor = System.Drawing.SystemColors.Control;
            this.trackBarTextureOpacity.Cursor = System.Windows.Forms.Cursors.Default;
            this.trackBarTextureOpacity.LargeChange = 10;
            this.trackBarTextureOpacity.Location = new System.Drawing.Point(274, 24);
            this.trackBarTextureOpacity.Maximum = 1000;
            this.trackBarTextureOpacity.Name = "trackBarTextureOpacity";
            this.trackBarTextureOpacity.Size = new System.Drawing.Size(168, 25);
            this.trackBarTextureOpacity.TabIndex = 19;
            this.trackBarTextureOpacity.TabStop = false;
            this.trackBarTextureOpacity.Value = 1000;
            this.trackBarTextureOpacity.Scroll += new System.EventHandler(this.trackBarTextureOpacity_Scroll);
            this.trackBarTextureOpacity.MouseUp += new System.Windows.Forms.MouseEventHandler(this.trackBarTextureOpacity_MouseUp);
            // 
            // topView
            // 
            this.topView.Location = new System.Drawing.Point(33, 3);
            this.topView.Name = "topView";
            this.topView.Size = new System.Drawing.Size(196, 184);
            this.topView.TabIndex = 7;
            this.topView.Text = "top view";
            // 
            // mainView
            // 
            this.mainView.AllowDrop = true;
            this.mainView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainView.Location = new System.Drawing.Point(0, 49);
            this.mainView.Name = "mainView";
            this.mainView.Size = new System.Drawing.Size(1092, 717);
            this.mainView.TabIndex = 17;
            this.mainView.Text = "mainView";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1092, 766);
            this.Controls.Add(this.trackBarTextureOpacity);
            this.Controls.Add(this.topViewPanel);
            this.Controls.Add(this.mainView);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Easter Egg Editor";
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.topViewPanel.ResumeLayout(false);
            this.topViewPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarTextureOpacity)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton showTopView;
        private System.Windows.Forms.MenuStrip menuStrip;
        private MainView mainView;
        private System.Windows.Forms.Panel topViewPanel;
        private TopView topView;
        private System.Windows.Forms.Label coords;
        private System.Windows.Forms.Button upButton;
        private System.Windows.Forms.Label layerLabel;
        private System.Windows.Forms.Button downButton;
        private System.Windows.Forms.TextBox layer;
        private System.Windows.Forms.ToolStripButton drawTextureIndices;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog importFileDialog;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripEditBlocks;
        private System.Windows.Forms.ToolStripButton toolStripEditTextures;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripDrawBlockSolid;
        private System.Windows.Forms.ToolStripButton toolStripDrawBlockWireframe;
        private System.Windows.Forms.TrackBar trackBarTextureOpacity;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripLabel toolStripTextureOpacityLabel;
        private System.Windows.Forms.ToolStripButton toolStripDrawBlockNone;
    }
}

