namespace Mindstep.EasterEgg.MapEditor
{
    partial class BlockEditingControl
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripCoordX = new System.Windows.Forms.ToolStripLabel();
            this.toolStripCoordY = new System.Windows.Forms.ToolStripLabel();
            this.toolStripCoordZ = new System.Windows.Forms.ToolStripLabel();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.HotTrack;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripCoordX,
            this.toolStripCoordY,
            this.toolStripCoordZ});
            this.toolStrip1.Location = new System.Drawing.Point(183, 54);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(107, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
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
            // BlockEditingControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.toolStrip1);
            this.Name = "BlockEditingControl";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripLabel toolStripCoordX;
        private System.Windows.Forms.ToolStripLabel toolStripCoordY;
        private System.Windows.Forms.ToolStripLabel toolStripCoordZ;
        private System.Windows.Forms.ToolStrip toolStrip1;
    }
}
