namespace Mindstep.EasterEgg.MapEditor
{
    partial class BlockDetailsForm
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
            this.BlockValue = new System.Windows.Forms.Label();
            this.BlockTypesDropDown = new System.Windows.Forms.ComboBox();
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.ScriptNameBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.CurrentBlockType = new System.Windows.Forms.Label();
            this.CurrentScriptName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // BlockValue
            // 
            this.BlockValue.AutoSize = true;
            this.BlockValue.Location = new System.Drawing.Point(12, 57);
            this.BlockValue.Name = "BlockValue";
            this.BlockValue.Size = new System.Drawing.Size(61, 13);
            this.BlockValue.TabIndex = 2;
            this.BlockValue.Text = "Block Type";
            // 
            // BlockTypesDropDown
            // 
            this.BlockTypesDropDown.AllowDrop = true;
            this.BlockTypesDropDown.FormattingEnabled = true;
            this.BlockTypesDropDown.ImeMode = System.Windows.Forms.ImeMode.On;
            this.BlockTypesDropDown.Location = new System.Drawing.Point(12, 73);
            this.BlockTypesDropDown.Name = "BlockTypesDropDown";
            this.BlockTypesDropDown.Size = new System.Drawing.Size(121, 21);
            this.BlockTypesDropDown.TabIndex = 3;
            this.BlockTypesDropDown.SelectedIndexChanged += new System.EventHandler(this.BlockTypes_SelectedIndexChanged);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(113, 211);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 4;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(205, 211);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 5;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "ScriptName";
            // 
            // ScriptNameBox
            // 
            this.ScriptNameBox.Location = new System.Drawing.Point(12, 34);
            this.ScriptNameBox.Name = "ScriptNameBox";
            this.ScriptNameBox.Size = new System.Drawing.Size(100, 20);
            this.ScriptNameBox.TabIndex = 7;
            this.ScriptNameBox.TextChanged += new System.EventHandler(this.ScriptNameBox_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(157, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Current:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(157, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Current: ";
            // 
            // CurrentBlockType
            // 
            this.CurrentBlockType.AutoSize = true;
            this.CurrentBlockType.Location = new System.Drawing.Point(205, 72);
            this.CurrentBlockType.Name = "CurrentBlockType";
            this.CurrentBlockType.Size = new System.Drawing.Size(35, 13);
            this.CurrentBlockType.TabIndex = 10;
            this.CurrentBlockType.Text = "label4";
            // 
            // CurrentScriptName
            // 
            this.CurrentScriptName.AutoSize = true;
            this.CurrentScriptName.Location = new System.Drawing.Point(205, 34);
            this.CurrentScriptName.Name = "CurrentScriptName";
            this.CurrentScriptName.Size = new System.Drawing.Size(35, 13);
            this.CurrentScriptName.TabIndex = 11;
            this.CurrentScriptName.Text = "label5";
            // 
            // BlockDetailsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Controls.Add(this.CurrentScriptName);
            this.Controls.Add(this.CurrentBlockType);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ScriptNameBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.BlockTypesDropDown);
            this.Controls.Add(this.BlockValue);
            this.Name = "BlockDetailsForm";
            this.Text = "BlockDetailsForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label BlockValue;
        private System.Windows.Forms.ComboBox BlockTypesDropDown;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ScriptNameBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label CurrentBlockType;
        private System.Windows.Forms.Label CurrentScriptName;
    }
}