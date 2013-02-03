namespace Mindstep.EasterEgg.MapEditor
{
    partial class FrameListPanelWrapper
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
            this.frameListPanel1 = new Mindstep.EasterEgg.MapEditor.FrameListPanel();
            this.SuspendLayout();
            // 
            // frameListPanel1
            // 
            this.frameListPanel1.BackColor = System.Drawing.Color.Black;
            this.frameListPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.frameListPanel1.FrameMargin = new System.Windows.Forms.Padding(0, 0, 5, 5);
            this.frameListPanel1.FrameRatio = 0.75F;
            this.frameListPanel1.Location = new System.Drawing.Point(0, 0);
            this.frameListPanel1.Name = "frameListPanel1";
            this.frameListPanel1.Padding = new System.Windows.Forms.Padding(5);
            this.frameListPanel1.Size = new System.Drawing.Size(763, 165);
            this.frameListPanel1.TabIndex = 0;
            this.frameListPanel1.WrapContents = false;
            // 
            // FrameListPanelWrapper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.frameListPanel1);
            this.Name = "FrameListPanelWrapper";
            this.Size = new System.Drawing.Size(763, 165);
            this.ResumeLayout(false);

        }

        #endregion

        private FrameListPanel frameListPanel1;


    }
}
