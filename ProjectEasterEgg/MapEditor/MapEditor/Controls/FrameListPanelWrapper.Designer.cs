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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrameListPanelWrapper));
            this.button1 = new System.Windows.Forms.Button();
            this.frameListPanel1 = new Mindstep.EasterEgg.MapEditor.FrameListPanel();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button1.BackColor = System.Drawing.Color.Gray;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button1.Location = new System.Drawing.Point(726, 69);
            this.button1.Margin = new System.Windows.Forms.Padding(0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(27, 27);
            this.button1.TabIndex = 2;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
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
            this.frameListPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.frameListPanel1_Paint);
            // 
            // UserControl1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.frameListPanel1);
            this.Name = "UserControl1";
            this.Size = new System.Drawing.Size(763, 165);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UserControl1_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private FrameListPanel frameListPanel1;
        private System.Windows.Forms.Button button1;


    }
}
