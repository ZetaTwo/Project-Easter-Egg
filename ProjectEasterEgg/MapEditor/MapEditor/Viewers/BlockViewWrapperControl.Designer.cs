namespace Mindstep.EasterEgg.MapEditor.Viewers
{
    partial class BlockViewWrapperControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BlockViewWrapperControl));
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripEditBlocks = new System.Windows.Forms.ToolStripButton();
            this.toolStripEditTextures = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripDrawBlockSolid = new System.Windows.Forms.ToolStripButton();
            this.toolStripDrawBlockWireframe = new System.Windows.Forms.ToolStripButton();
            this.toolStripDrawBlockNone = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonSelectBackgroundColor = new System.Windows.Forms.ToolStripButton();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.textureOpacityTrackBar = new Mindstep.EasterEgg.MapEditor.ToolStripTrackBarItem();
            this.backgroundColorDialog = new System.Windows.Forms.ColorDialog();
            this.toolStrip2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.toolStrip3.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip2
            // 
            this.toolStrip2.AllowMerge = false;
            this.toolStrip2.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.toolStrip2.CanOverflow = false;
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripEditBlocks,
            this.toolStripEditTextures,
            this.toolStripSeparator1,
            this.toolStripDrawBlockSolid,
            this.toolStripDrawBlockWireframe,
            this.toolStripDrawBlockNone});
            this.toolStrip2.Location = new System.Drawing.Point(310, 57);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Padding = new System.Windows.Forms.Padding(0);
            this.toolStrip2.Size = new System.Drawing.Size(146, 27);
            this.toolStrip2.TabIndex = 17;
            // 
            // toolStripEditBlocks
            // 
            this.toolStripEditBlocks.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripEditBlocks.Image = ((System.Drawing.Image)(resources.GetObject("toolStripEditBlocks.Image")));
            this.toolStripEditBlocks.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripEditBlocks.Margin = new System.Windows.Forms.Padding(1, 1, 1, 2);
            this.toolStripEditBlocks.Name = "toolStripEditBlocks";
            this.toolStripEditBlocks.Size = new System.Drawing.Size(24, 24);
            this.toolStripEditBlocks.Text = "toolStripButton1";
            this.toolStripEditBlocks.ToolTipText = "Enter block editing mode";
            this.toolStripEditBlocks.Click += new System.EventHandler(this.toolStripEditBlocks_Click);
            // 
            // toolStripEditTextures
            // 
            this.toolStripEditTextures.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripEditTextures.Image = ((System.Drawing.Image)(resources.GetObject("toolStripEditTextures.Image")));
            this.toolStripEditTextures.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripEditTextures.Margin = new System.Windows.Forms.Padding(1, 1, 1, 2);
            this.toolStripEditTextures.Name = "toolStripEditTextures";
            this.toolStripEditTextures.Size = new System.Drawing.Size(24, 24);
            this.toolStripEditTextures.Text = "toolStripButton2";
            this.toolStripEditTextures.ToolTipText = "Enter texture editing mode";
            this.toolStripEditTextures.Click += new System.EventHandler(this.toolStripEditTextures_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
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
            this.toolStripDrawBlockSolid.Click += new System.EventHandler(this.toolStripDrawBlockSolid_Click);
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
            this.toolStripDrawBlockWireframe.Click += new System.EventHandler(this.toolStripDrawBlockWireframe_Click);
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
            this.toolStripDrawBlockNone.Click += new System.EventHandler(this.toolStripDrawBlockNone_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.AllowMerge = false;
            this.toolStrip1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.toolStrip1.CanOverflow = false;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonSelectBackgroundColor});
            this.toolStrip1.Location = new System.Drawing.Point(201, 57);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0);
            this.toolStrip1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStrip1.Size = new System.Drawing.Size(28, 27);
            this.toolStrip1.TabIndex = 18;
            this.toolStrip1.Text = "toolStrip";
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
            // toolStrip3
            // 
            this.toolStrip3.AllowMerge = false;
            this.toolStrip3.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.toolStrip3.CanOverflow = false;
            this.toolStrip3.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip3.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip3.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.textureOpacityTrackBar});
            this.toolStrip3.Location = new System.Drawing.Point(503, 57);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.Padding = new System.Windows.Forms.Padding(0);
            this.toolStrip3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStrip3.Size = new System.Drawing.Size(156, 27);
            this.toolStrip3.TabIndex = 19;
            this.toolStrip3.Text = "toolStrip2";
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
            this.textureOpacityTrackBar.Text = "textureOpacityTrackBar";
            this.textureOpacityTrackBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.textureOpacityTrackBar.Value = 0;
            this.textureOpacityTrackBar.Scroll += new System.EventHandler(this.textureOpacityTrackBar_Scroll);
            this.textureOpacityTrackBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.textureOpacityTrackBar_MouseUp);
            // 
            // backgroundColorDialog
            // 
            this.backgroundColorDialog.AnyColor = true;
            this.backgroundColorDialog.Color = System.Drawing.Color.Red;
            this.backgroundColorDialog.FullOpen = true;
            // 
            // BlockViewWrapperControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.toolStrip3);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.toolStrip2);
            this.Name = "BlockViewWrapperControl";
            this.Size = new System.Drawing.Size(826, 524);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton toolStripEditBlocks;
        private System.Windows.Forms.ToolStripButton toolStripEditTextures;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripDrawBlockSolid;
        private System.Windows.Forms.ToolStripButton toolStripDrawBlockWireframe;
        private System.Windows.Forms.ToolStripButton toolStripDrawBlockNone;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonSelectBackgroundColor;
        private System.Windows.Forms.ToolStrip toolStrip3;
        private ToolStripTrackBarItem textureOpacityTrackBar;
        private System.Windows.Forms.ColorDialog backgroundColorDialog;
    }
}
