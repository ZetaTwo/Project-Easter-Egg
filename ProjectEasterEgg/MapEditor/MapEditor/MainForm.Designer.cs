namespace MapEditor
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
            this.createblock = new System.Windows.Forms.Button();
            this.blocksizelabel = new System.Windows.Forms.Label();
            this.blocksize = new System.Windows.Forms.NumericUpDown();
            this.vertexColor3 = new System.Windows.Forms.ComboBox();
            this.vertexColor2 = new System.Windows.Forms.ComboBox();
            this.vertexColor1 = new System.Windows.Forms.ComboBox();
            this.topView = new MapEditor.TopView();
            ((System.ComponentModel.ISupportInitialize)(this.blocksize)).BeginInit();
            this.SuspendLayout();
            // 
            // createblock
            // 
            this.createblock.Location = new System.Drawing.Point(705, 190);
            this.createblock.Name = "createblock";
            this.createblock.Size = new System.Drawing.Size(75, 23);
            this.createblock.TabIndex = 6;
            this.createblock.Text = "Create block";
            this.createblock.UseVisualStyleBackColor = true;
            this.createblock.MouseDown += new System.Windows.Forms.MouseEventHandler(this.createblock_MouseDown);
            this.createblock.MouseUp += new System.Windows.Forms.MouseEventHandler(this.createblock_MouseUp);
            // 
            // blocksizelabel
            // 
            this.blocksizelabel.AutoSize = true;
            this.blocksizelabel.Location = new System.Drawing.Point(726, 216);
            this.blocksizelabel.Name = "blocksizelabel";
            this.blocksizelabel.Size = new System.Drawing.Size(54, 13);
            this.blocksizelabel.TabIndex = 5;
            this.blocksizelabel.Text = "block size";
            // 
            // blocksize
            // 
            this.blocksize.Location = new System.Drawing.Point(744, 232);
            this.blocksize.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.blocksize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.blocksize.Name = "blocksize";
            this.blocksize.Size = new System.Drawing.Size(36, 20);
            this.blocksize.TabIndex = 4;
            this.blocksize.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.blocksize.ValueChanged += new System.EventHandler(this.blocksize_ValueChanged);
            // 
            // vertexColor3
            // 
            this.vertexColor3.DropDownHeight = 500;
            this.vertexColor3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.vertexColor3.FormattingEnabled = true;
            this.vertexColor3.IntegralHeight = false;
            this.vertexColor3.Items.AddRange(new object[] {
            "BurlyWood",
            "Chartreuse",
            "Coral",
            "CornflowerBlue",
            "Cornsilk",
            "Firebrick",
            "Fuchsia",
            "Goldenrod",
            "Indigo",
            "Tan",
            "Teal",
            "Thistle",
            "Tomato"});
            this.vertexColor3.Location = new System.Drawing.Point(677, 540);
            this.vertexColor3.Name = "vertexColor3";
            this.vertexColor3.Size = new System.Drawing.Size(103, 21);
            this.vertexColor3.TabIndex = 3;
            this.vertexColor3.SelectedIndexChanged += new System.EventHandler(this.vertexColor_SelectedIndexChanged);
            // 
            // vertexColor2
            // 
            this.vertexColor2.DropDownHeight = 500;
            this.vertexColor2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.vertexColor2.FormattingEnabled = true;
            this.vertexColor2.IntegralHeight = false;
            this.vertexColor2.Items.AddRange(new object[] {
            "BurlyWood",
            "Chartreuse",
            "Coral",
            "CornflowerBlue",
            "Cornsilk",
            "Firebrick",
            "Fuchsia",
            "Goldenrod",
            "Indigo",
            "Tan",
            "Teal",
            "Thistle",
            "Tomato"});
            this.vertexColor2.Location = new System.Drawing.Point(677, 513);
            this.vertexColor2.Name = "vertexColor2";
            this.vertexColor2.Size = new System.Drawing.Size(103, 21);
            this.vertexColor2.TabIndex = 2;
            this.vertexColor2.SelectedIndexChanged += new System.EventHandler(this.vertexColor_SelectedIndexChanged);
            // 
            // vertexColor1
            // 
            this.vertexColor1.DropDownHeight = 500;
            this.vertexColor1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.vertexColor1.FormattingEnabled = true;
            this.vertexColor1.IntegralHeight = false;
            this.vertexColor1.Items.AddRange(new object[] {
            "BurlyWood",
            "Chartreuse",
            "Coral",
            "CornflowerBlue",
            "Cornsilk",
            "Firebrick",
            "Fuchsia",
            "Goldenrod",
            "Indigo",
            "Tan",
            "Teal",
            "Thistle",
            "Tomato"});
            this.vertexColor1.Location = new System.Drawing.Point(677, 486);
            this.vertexColor1.Name = "vertexColor1";
            this.vertexColor1.Size = new System.Drawing.Size(103, 21);
            this.vertexColor1.TabIndex = 1;
            this.vertexColor1.SelectedIndexChanged += new System.EventHandler(this.vertexColor_SelectedIndexChanged);
            // 
            // topView
            // 
            this.topView.Location = new System.Drawing.Point(596, 0);
            this.topView.Name = "topView";
            this.topView.Size = new System.Drawing.Size(196, 184);
            this.topView.TabIndex = 7;
            this.topView.Text = "top view";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 573);
            this.Controls.Add(this.createblock);
            this.Controls.Add(this.topView);
            this.Controls.Add(this.vertexColor3);
            this.Controls.Add(this.blocksizelabel);
            this.Controls.Add(this.blocksize);
            this.Controls.Add(this.vertexColor1);
            this.Controls.Add(this.vertexColor2);
            this.Name = "MainForm";
            this.Text = "WinForms Graphics Device";
            ((System.ComponentModel.ISupportInitialize)(this.blocksize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SpriteFontControl spriteFontControl;
        private System.Windows.Forms.ComboBox vertexColor1;
        private System.Windows.Forms.ComboBox vertexColor3;
        private System.Windows.Forms.ComboBox vertexColor2;
        private System.Windows.Forms.NumericUpDown blocksize;
        private System.Windows.Forms.Label blocksizelabel;
        private System.Windows.Forms.Button createblock;
        private TopView topView;
    }
}

