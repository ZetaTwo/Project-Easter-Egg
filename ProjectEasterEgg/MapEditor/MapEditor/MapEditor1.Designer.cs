namespace MapEditor
{
    partial class MapEditor1
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
            this.topView = new MapEditor.TopView();
            this.SuspendLayout();
            // 
            // topView
            // 
            this.topView.Location = new System.Drawing.Point(413, 13);
            this.topView.Name = "topView";
            this.topView.Size = new System.Drawing.Size(167, 155);
            this.topView.TabIndex = 0;
            // 
            // MapEditor1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 383);
            this.Controls.Add(this.topView);
            this.Name = "MapEditor1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private TopView topView;
    }
}

