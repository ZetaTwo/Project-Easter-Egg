namespace Mindstep.EasterEgg.MapEditor
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
            this.toolStripImageList1 = new Mindstep.EasterEgg.MapEditor.FrameListPanel();
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
            // toolStripImageList1
            // 
            this.toolStripImageList1.BackColor = System.Drawing.Color.Black;
            this.toolStripImageList1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStripImageList1.FrameMargin = new System.Windows.Forms.Padding(0, 0, 5, 5);
            this.toolStripImageList1.FrameRatio = 0.75F;
            this.toolStripImageList1.Location = new System.Drawing.Point(0, 314);
            this.toolStripImageList1.Name = "toolStripImageList1";
            this.toolStripImageList1.Padding = new System.Windows.Forms.Padding(5);
            this.toolStripImageList1.Size = new System.Drawing.Size(647, 80);
            this.toolStripImageList1.TabIndex = 23;
            this.toolStripImageList1.WrapContents = false;
            // 
            // TextureEditingControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkBlue;
            this.Controls.Add(this.toolStripImageList1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "TextureEditingControl";
            this.Size = new System.Drawing.Size(647, 394);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton drawTextureIndices;
        private FrameListPanel toolStripImageList1;
    }
}
