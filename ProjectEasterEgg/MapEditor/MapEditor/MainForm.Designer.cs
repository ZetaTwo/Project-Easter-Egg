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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.showTopView = new System.Windows.Forms.ToolStripButton();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.topViewPanel = new System.Windows.Forms.Panel();
            this.coords = new System.Windows.Forms.Label();
            this.downButton = new System.Windows.Forms.Button();
            this.layerLabel = new System.Windows.Forms.Label();
            this.upButton = new System.Windows.Forms.Button();
            this.layer = new System.Windows.Forms.TextBox();
            this.topView = new Mindstep.EasterEgg.MapEditor.TopView();
            this.mainView = new Mindstep.EasterEgg.MapEditor.MainView();
            this.toolStrip1.SuspendLayout();
            this.topViewPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showTopView});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(792, 25);
            this.toolStrip1.TabIndex = 15;
            this.toolStrip1.Text = "toolStrip1";
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
            // menuStrip1
            // 
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(792, 24);
            this.menuStrip1.TabIndex = 16;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // topViewPanel
            // 
            this.topViewPanel.Controls.Add(this.topView);
            this.topViewPanel.Controls.Add(this.coords);
            this.topViewPanel.Controls.Add(this.downButton);
            this.topViewPanel.Controls.Add(this.layerLabel);
            this.topViewPanel.Controls.Add(this.upButton);
            this.topViewPanel.Controls.Add(this.layer);
            this.topViewPanel.Location = new System.Drawing.Point(560, 49);
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
            // downButton
            // 
            this.downButton.Image = ((System.Drawing.Image)(resources.GetObject("downButton.Image")));
            this.downButton.Location = new System.Drawing.Point(3, 16);
            this.downButton.Name = "downButton";
            this.downButton.Size = new System.Drawing.Size(24, 24);
            this.downButton.TabIndex = 8;
            this.downButton.UseVisualStyleBackColor = true;
            this.downButton.Click += new System.EventHandler(this.downButton_Click);
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
            // upButton
            // 
            this.upButton.Image = ((System.Drawing.Image)(resources.GetObject("upButton.Image")));
            this.upButton.Location = new System.Drawing.Point(3, 72);
            this.upButton.Name = "upButton";
            this.upButton.Size = new System.Drawing.Size(24, 24);
            this.upButton.TabIndex = 9;
            this.upButton.UseVisualStyleBackColor = true;
            this.upButton.Click += new System.EventHandler(this.upButton_Click);
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
            // topView
            // 
            this.topView.Location = new System.Drawing.Point(33, 3);
            this.topView.Name = "topView";
            this.topView.Size = new System.Drawing.Size(196, 184);
            this.topView.TabIndex = 7;
            this.topView.Text = "top view";
            this.topView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.topView_Click);
            this.topView.MouseMove += new System.Windows.Forms.MouseEventHandler(this.topView_MouseMove);
            // 
            // mainView
            // 
            this.mainView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainView.Location = new System.Drawing.Point(0, 49);
            this.mainView.Name = "mainView";
            this.mainView.Size = new System.Drawing.Size(792, 517);
            this.mainView.TabIndex = 17;
            this.mainView.Text = "mainView";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(792, 566);
            this.Controls.Add(this.topViewPanel);
            this.Controls.Add(this.mainView);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Easter Egg Editor";
            this.Scroll += new System.Windows.Forms.ScrollEventHandler(this.MainForm_Scroll);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.topViewPanel.ResumeLayout(false);
            this.topViewPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SpriteFontControl spriteFontControl;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton showTopView;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private MainView mainView;
        private System.Windows.Forms.Panel topViewPanel;
        private TopView topView;
        private System.Windows.Forms.Label coords;
        private System.Windows.Forms.Button downButton;
        private System.Windows.Forms.Label layerLabel;
        private System.Windows.Forms.Button upButton;
        private System.Windows.Forms.TextBox layer;
    }
}

