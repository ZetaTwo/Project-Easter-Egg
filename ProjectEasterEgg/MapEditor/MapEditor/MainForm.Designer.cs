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
            this.upButton = new System.Windows.Forms.Button();
            this.downButton = new System.Windows.Forms.Button();
            this.layer = new System.Windows.Forms.TextBox();
            this.layerLabel = new System.Windows.Forms.Label();
            this.coords = new System.Windows.Forms.Label();
            this.topView = new Mindstep.EasterEgg.MapEditor.TopView();
            this.SuspendLayout();
            // 
            // upButton
            // 
            this.upButton.Image = ((System.Drawing.Image)(resources.GetObject("upButton.Image")));
            this.upButton.Location = new System.Drawing.Point(562, 29);
            this.upButton.Name = "upButton";
            this.upButton.Size = new System.Drawing.Size(28, 28);
            this.upButton.TabIndex = 8;
            this.upButton.UseVisualStyleBackColor = true;
            this.upButton.Click += new System.EventHandler(this.upButton_Click);
            // 
            // downButton
            // 
            this.downButton.Image = ((System.Drawing.Image)(resources.GetObject("downButton.Image")));
            this.downButton.Location = new System.Drawing.Point(562, 89);
            this.downButton.Name = "downButton";
            this.downButton.Size = new System.Drawing.Size(28, 28);
            this.downButton.TabIndex = 9;
            this.downButton.UseVisualStyleBackColor = true;
            this.downButton.Click += new System.EventHandler(this.downButton_Click);
            // 
            // layer
            // 
            this.layer.Location = new System.Drawing.Point(562, 63);
            this.layer.Name = "layer";
            this.layer.ReadOnly = true;
            this.layer.Size = new System.Drawing.Size(28, 20);
            this.layer.TabIndex = 10;
            this.layer.Text = "0";
            // 
            // layerLabel
            // 
            this.layerLabel.AutoSize = true;
            this.layerLabel.Location = new System.Drawing.Point(555, 13);
            this.layerLabel.Name = "layerLabel";
            this.layerLabel.Size = new System.Drawing.Size(33, 13);
            this.layerLabel.TabIndex = 11;
            this.layerLabel.Text = "Layer";
            // 
            // coords
            // 
            this.coords.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.coords.Location = new System.Drawing.Point(694, 187);
            this.coords.Name = "coords";
            this.coords.Size = new System.Drawing.Size(98, 13);
            this.coords.TabIndex = 12;
            // 
            // topView
            // 
            this.topView.Location = new System.Drawing.Point(596, 0);
            this.topView.Name = "topView";
            this.topView.Size = new System.Drawing.Size(196, 184);
            this.topView.TabIndex = 7;
            this.topView.Text = "top view";
            this.topView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.topView_Click);
            this.topView.MouseLeave += new System.EventHandler(this.topView_MouseLeave);
            this.topView.MouseMove += new System.Windows.Forms.MouseEventHandler(this.topView_MouseMove);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 573);
            this.Controls.Add(this.coords);
            this.Controls.Add(this.layerLabel);
            this.Controls.Add(this.layer);
            this.Controls.Add(this.downButton);
            this.Controls.Add(this.upButton);
            this.Controls.Add(this.topView);
            this.Name = "MainForm";
            this.Text = "WinForms Graphics Device";
            this.Scroll += new System.Windows.Forms.ScrollEventHandler(this.MainForm_Scroll);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SpriteFontControl spriteFontControl;
        private TopView topView;
        private System.Windows.Forms.Button upButton;
        private System.Windows.Forms.Button downButton;
        private System.Windows.Forms.TextBox layer;
        private System.Windows.Forms.Label layerLabel;
        private System.Windows.Forms.Label coords;
    }
}

