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

        private string scriptName;
        private int blockType;
        private SaveBlock block;

        public BlockDetailsForm(SaveBlock block)
        {
            this.block = block;
            InitializeComponent();

            BlockTypesDropDown.Items.AddRange(Enum.GetNames(typeof(BlockType)));
            Show();
        }


        private void BlockTypes_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Abort;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            block.script = scriptName;
            block.type = blockType;
            DialogResult = DialogResult.OK;
        }

        private void ScriptNameBox_TextChanged(object sender, EventArgs e)
        {
            scriptName = ScriptNameBox.Text;
        }
    }
}
