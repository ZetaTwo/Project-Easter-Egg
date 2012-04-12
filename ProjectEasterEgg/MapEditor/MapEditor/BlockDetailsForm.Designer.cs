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
            this.blockTypeLabel = new System.Windows.Forms.Label();
            this.BlockTypesDropDown = new System.Windows.Forms.ComboBox();
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.scriptNameLabel = new System.Windows.Forms.Label();
            this.ScriptNameBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.CurrentBlockType = new System.Windows.Forms.Label();
            this.CurrentScriptName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // blockTypeLabel
            // 
            this.blockTypeLabel.AutoSize = true;
            this.blockTypeLabel.Location = new System.Drawing.Point(12, 48);
            this.blockTypeLabel.Name = "blockTypeLabel";
            this.blockTypeLabel.Size = new System.Drawing.Size(57, 13);
            this.blockTypeLabel.TabIndex = 2;
            this.blockTypeLabel.Text = "Block type";
            // 
            // BlockTypesDropDown
            // 
            this.BlockTypesDropDown.AllowDrop = true;
            this.BlockTypesDropDown.FormattingEnabled = true;
            this.BlockTypesDropDown.ImeMode = System.Windows.Forms.ImeMode.On;
            this.BlockTypesDropDown.Location = new System.Drawing.Point(12, 64);
            this.BlockTypesDropDown.Name = "BlockTypesDropDown";
            this.BlockTypesDropDown.Size = new System.Drawing.Size(121, 21);
            this.BlockTypesDropDown.TabIndex = 3;
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(117, 107);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 4;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(201, 107);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 5;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // scriptNameLabel
            // 
            this.scriptNameLabel.AutoSize = true;
            this.scriptNameLabel.Location = new System.Drawing.Point(12, 9);
            this.scriptNameLabel.Name = "scriptNameLabel";
            this.scriptNameLabel.Size = new System.Drawing.Size(104, 13);
            this.scriptNameLabel.TabIndex = 6;
            this.scriptNameLabel.Text = "Script to run on click";
            // 
            // ScriptNameBox
            // 
            this.ScriptNameBox.Location = new System.Drawing.Point(12, 25);
            this.ScriptNameBox.Name = "ScriptNameBox";
            this.ScriptNameBox.Size = new System.Drawing.Size(121, 20);
            this.ScriptNameBox.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(148, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Current:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(148, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Current:";
            // 
            // CurrentBlockType
            // 
            this.CurrentBlockType.AutoSize = true;
            this.CurrentBlockType.Location = new System.Drawing.Point(198, 67);
            this.CurrentBlockType.Name = "CurrentBlockType";
            this.CurrentBlockType.Size = new System.Drawing.Size(35, 13);
            this.CurrentBlockType.TabIndex = 10;
            this.CurrentBlockType.Text = "label4";
            // 
            // CurrentScriptName
            // 
            this.CurrentScriptName.AutoSize = true;
            this.CurrentScriptName.Location = new System.Drawing.Point(198, 28);
            this.CurrentScriptName.Name = "CurrentScriptName";
            this.CurrentScriptName.Size = new System.Drawing.Size(35, 13);
            this.CurrentScriptName.TabIndex = 11;
            this.CurrentScriptName.Text = "label5";
            // 
            // BlockDetailsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(288, 142);
            this.Controls.Add(this.CurrentScriptName);
            this.Controls.Add(this.CurrentBlockType);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ScriptNameBox);
            this.Controls.Add(this.scriptNameLabel);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.BlockTypesDropDown);
            this.Controls.Add(this.blockTypeLabel);
            this.Name = "BlockDetailsForm";
            this.Text = "Change Block Details";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label blockTypeLabel;
        private System.Windows.Forms.ComboBox BlockTypesDropDown;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Label scriptNameLabel;
        private System.Windows.Forms.TextBox ScriptNameBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label CurrentBlockType;
        private System.Windows.Forms.Label CurrentScriptName;
    }
}