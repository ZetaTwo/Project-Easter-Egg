namespace Mindstep.EasterEgg.MapEditor.Viewers
{
    partial class TextureEditingControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TextureEditingControl));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.drawTextureIndices = new System.Windows.Forms.ToolStripButton();
            this.frameListPanel = new Mindstep.EasterEgg.MapEditor.FrameListPanelWrapper();
            this.button1 = new System.Windows.Forms.Button();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
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
            this.drawTextureIndices});
            this.toolStrip1.Location = new System.Drawing.Point(145, 40);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0);
            this.toolStrip1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStrip1.Size = new System.Drawing.Size(32, 27);
            this.toolStrip1.TabIndex = 19;
            this.toolStrip1.Text = "toolStrip";
            // 
            // drawTextureIndices
            // 
            this.drawTextureIndices.CheckOnClick = true;
            this.drawTextureIndices.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.drawTextureIndices.Image = ((System.Drawing.Image)(resources.GetObject("drawTextureIndices.Image")));
            this.drawTextureIndices.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.drawTextureIndices.Margin = new System.Windows.Forms.Padding(5, 1, 1, 2);
            this.drawTextureIndices.Name = "drawTextureIndices";
            this.drawTextureIndices.Size = new System.Drawing.Size(24, 24);
            this.drawTextureIndices.Text = "Draw Texture Indices";
            this.drawTextureIndices.CheckedChanged += new System.EventHandler(this.drawTextureIndices_CheckedChanged);
            // 
            // frameListPanel
            // 
            this.frameListPanel.BackColor = System.Drawing.Color.Black;
            this.frameListPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.frameListPanel.Location = new System.Drawing.Point(0, 306);
            this.frameListPanel.Name = "frameListPanel";
            this.frameListPanel.Size = new System.Drawing.Size(647, 88);
            this.frameListPanel.TabIndex = 24;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.Gray;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Image = global::Mindstep.EasterEgg.MapEditor.Properties.Resources.play;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button1.Location = new System.Drawing.Point(620, 306);
            this.button1.Margin = new System.Windows.Forms.Padding(0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(27, 27);
            this.button1.TabIndex = 25;
            this.button1.Text = " &";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // TextureEditingControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkBlue;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.frameListPanel);
            this.Controls.Add(this.toolStrip1);
            this.Name = "TextureEditingControl";
            this.Size = new System.Drawing.Size(647, 394);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TextureEditingControl_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TextureEditingControl_MouseMove);
            this.MouseUpWithoutMoving += new System.Windows.Forms.MouseEventHandler(this.TextureEditingControl_MouseUpWithoutMoving);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextureEditingControl_KeyDown);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TextureEditingControl_MouseUp);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton drawTextureIndices;
        private FrameListPanelWrapper frameListPanel;
        private System.Windows.Forms.Button button1;
    }
}
