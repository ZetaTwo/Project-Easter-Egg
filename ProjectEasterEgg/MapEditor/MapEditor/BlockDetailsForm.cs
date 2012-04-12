using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Mindstep.EasterEgg.Commons;

namespace Mindstep.EasterEgg.MapEditor
{
    public partial class BlockDetailsForm : Form
    {
        private SaveBlock block;

        public BlockDetailsForm(SaveBlock block, Point popupLocation)
        {
            this.block = block;
            StartPosition = FormStartPosition.Manual;
            Location = popupLocation;

            InitializeComponent();

            BlockTypesDropDown.Items.AddRange(Enum.GetNames(typeof(BlockType)));

            CurrentScriptName.Text = ScriptNameBox.Text = block.script;
            CurrentBlockType.Text = BlockTypesDropDown.Text = System.Enum.GetName(typeof(BlockType), block.type);
            Show();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Abort;
            Close();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            block.script = ScriptNameBox.Text;

            block.type = (BlockType)Enum.Parse(typeof(BlockType), BlockTypesDropDown.Text);

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
