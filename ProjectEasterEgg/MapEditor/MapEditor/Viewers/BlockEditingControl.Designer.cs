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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BlockEditingControl));
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonShowGrid = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripCoordX = new System.Windows.Forms.ToolStripLabel();
            this.toolStripCoordY = new System.Windows.Forms.ToolStripLabel();
            this.toolStripCoordZ = new System.Windows.Forms.ToolStripLabel();
            this.toolStrip2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
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
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonShowGrid});
            this.toolStrip2.Location = new System.Drawing.Point(171, 55);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Padding = new System.Windows.Forms.Padding(0);
            this.toolStrip2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStrip2.Size = new System.Drawing.Size(28, 27);
            this.toolStrip2.TabIndex = 19;
            this.toolStrip2.Text = "toolStrip";
            // 
            // toolStripButtonShowGrid
            // 
            this.toolStripButtonShowGrid.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.toolStripButtonShowGrid.Checked = true;
            this.toolStripButtonShowGrid.CheckOnClick = true;
            this.toolStripButtonShowGrid.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripButtonShowGrid.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonShowGrid.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonShowGrid.Image")));
            this.toolStripButtonShowGrid.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonShowGrid.Margin = new System.Windows.Forms.Padding(1, 1, 1, 2);
            this.toolStripButtonShowGrid.Name = "toolStripButtonShowGrid";
            this.toolStripButtonShowGrid.Size = new System.Drawing.Size(24, 24);
            this.toolStripButtonShowGrid.Text = "toolStripButton1";
            this.toolStripButtonShowGrid.ToolTipText = "Show grid";
            this.toolStripButtonShowGrid.CheckedChanged += new System.EventHandler(this.toolStripButtonShowGrid_CheckedChanged);
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
            this.toolStripCoordX,
            this.toolStripCoordY,
            this.toolStripCoordZ});
            this.toolStrip1.Location = new System.Drawing.Point(230, 57);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0);
            this.toolStrip1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStrip1.Size = new System.Drawing.Size(97, 25);
            this.toolStrip1.TabIndex = 20;
            this.toolStrip1.Text = "toolStrip";
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
            this.Controls.Add(this.toolStrip2);
            this.Name = "BlockEditingControl";
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton toolStripButtonShowGrid;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripCoordX;
        private System.Windows.Forms.ToolStripLabel toolStripCoordY;
        private System.Windows.Forms.ToolStripLabel toolStripCoordZ;
    }
}
