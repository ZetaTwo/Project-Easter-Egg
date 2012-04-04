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
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.topViewPanel = new System.Windows.Forms.Panel();
            this.topView = new Mindstep.EasterEgg.MapEditor.TopView();
            this.coords = new System.Windows.Forms.Label();
            this.upButton = new System.Windows.Forms.Button();
            this.layerLabel = new System.Windows.Forms.Label();
            this.downButton = new System.Windows.Forms.Button();
            this.layer = new System.Windows.Forms.TextBox();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.mainView = new Mindstep.EasterEgg.MapEditor.MainView();
            this.importFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.toolStrip.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.topViewPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showTopView,
            this.drawTextureIndices});
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
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.S)));
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.saveAsToolStripMenuItem.Text = "Save As";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.importToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
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
            // topView
            // 
            this.topView.Location = new System.Drawing.Point(33, 3);
            this.topView.Name = "topView";
            this.topView.Size = new System.Drawing.Size(196, 184);
            this.topView.TabIndex = 7;
            this.topView.Text = "top view";
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
            // importFileDialog
            // 
            this.importFileDialog.Filter = "PNG (*.png)|*.png";
            this.importFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.importFileDialog_FileOk);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1092, 766);
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
    }
}

