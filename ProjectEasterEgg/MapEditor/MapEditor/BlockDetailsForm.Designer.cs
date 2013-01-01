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
            this.blockTypesDropDown = new System.Windows.Forms.ComboBox();
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.scriptNameLabel = new System.Windows.Forms.Label();
            this.scriptNameBox = new System.Windows.Forms.TextBox();
            this.oldScriptNamePrefix = new System.Windows.Forms.Label();
            this.oldBlockTypePrefix = new System.Windows.Forms.Label();
            this.oldBlockType = new System.Windows.Forms.Label();
            this.oldScriptName = new System.Windows.Forms.Label();
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
            // blockTypesDropDown
            // 
            this.blockTypesDropDown.AllowDrop = true;
            this.blockTypesDropDown.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.blockTypesDropDown.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.blockTypesDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.blockTypesDropDown.FormattingEnabled = true;
            this.blockTypesDropDown.ImeMode = System.Windows.Forms.ImeMode.On;
            this.blockTypesDropDown.Location = new System.Drawing.Point(12, 64);
            this.blockTypesDropDown.Name = "blockTypesDropDown";
            this.blockTypesDropDown.Size = new System.Drawing.Size(121, 21);
            this.blockTypesDropDown.TabIndex = 3;
            this.blockTypesDropDown.SelectedValueChanged += new System.EventHandler(this.blockTypesDropDown_SelectedValueChanged);
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
            // scriptNameBox
            // 
            this.scriptNameBox.Location = new System.Drawing.Point(12, 25);
            this.scriptNameBox.Name = "scriptNameBox";
            this.scriptNameBox.Size = new System.Drawing.Size(121, 20);
            this.scriptNameBox.TabIndex = 7;
            this.scriptNameBox.TextChanged += new System.EventHandler(this.scriptNameBox_TextChanged);
            // 
            // oldScriptNamePrefix
            // 
            this.oldScriptNamePrefix.AutoSize = true;
            this.oldScriptNamePrefix.Location = new System.Drawing.Point(148, 28);
            this.oldScriptNamePrefix.Name = "oldScriptNamePrefix";
            this.oldScriptNamePrefix.Size = new System.Drawing.Size(26, 13);
            this.oldScriptNamePrefix.TabIndex = 8;
            this.oldScriptNamePrefix.Text = "Old:";
            // 
            // oldBlockTypePrefix
            // 
            this.oldBlockTypePrefix.AutoSize = true;
            this.oldBlockTypePrefix.Location = new System.Drawing.Point(148, 67);
            this.oldBlockTypePrefix.Name = "oldBlockTypePrefix";
            this.oldBlockTypePrefix.Size = new System.Drawing.Size(26, 13);
            this.oldBlockTypePrefix.TabIndex = 9;
            this.oldBlockTypePrefix.Text = "Old:";
            // 
            // oldBlockType
            // 
            this.oldBlockType.AutoSize = true;
            this.oldBlockType.Location = new System.Drawing.Point(198, 67);
            this.oldBlockType.Name = "oldBlockType";
            this.oldBlockType.Size = new System.Drawing.Size(56, 13);
            this.oldBlockType.TabIndex = 10;
            this.oldBlockType.Text = "block type";
            // 
            // oldScriptName
            // 
            this.oldScriptName.AutoSize = true;
            this.oldScriptName.Location = new System.Drawing.Point(198, 28);
            this.oldScriptName.Name = "oldScriptName";
            this.oldScriptName.Size = new System.Drawing.Size(61, 13);
            this.oldScriptName.TabIndex = 11;
            this.oldScriptName.Text = "script name";
            // 
            // BlockDetailsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(288, 142);
            this.Controls.Add(this.oldScriptName);
            this.Controls.Add(this.oldBlockType);
            this.Controls.Add(this.oldBlockTypePrefix);
            this.Controls.Add(this.oldScriptNamePrefix);
            this.Controls.Add(this.scriptNameBox);
            this.Controls.Add(this.scriptNameLabel);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.blockTypesDropDown);
            this.Controls.Add(this.blockTypeLabel);
            this.Name = "BlockDetailsForm";
            this.Text = "Change Block Details";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label blockTypeLabel;
        private System.Windows.Forms.ComboBox blockTypesDropDown;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Label scriptNameLabel;
        private System.Windows.Forms.TextBox scriptNameBox;
        private System.Windows.Forms.Label oldScriptNamePrefix;
        private System.Windows.Forms.Label oldBlockTypePrefix;
        private System.Windows.Forms.Label oldBlockType;
        private System.Windows.Forms.Label oldScriptName;
    }
}