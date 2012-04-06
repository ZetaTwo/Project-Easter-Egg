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
        string[] blockTypes = Enum.GetNames(typeof(BlockType));

        public BlockDetailsForm(SaveBlock block)
        {
            this.block = block;
            InitializeComponent();
            scriptName = block.script;
            blockType = block.type;
            

            Console.WriteLine(blockType);
            CurrentScriptName.Text = scriptName;
            CurrentBlockType.Text = blockTypes[block.type];

            ScriptNameBox.Text = scriptName;
            BlockTypesDropDown.Text = blockTypes[block.type];


            BlockTypesDropDown.Items.AddRange(blockTypes);
            Show();
        }


        private void BlockTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Abort;
            Close();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            block.script = scriptName;

            //No way of getting the number of the one item in the dropdown so i did this
            for (int i = 0; i < 4; i++)
            {
                if (BlockTypesDropDown.Text == blockTypes[i])
                {
                    block.type = i;
                    break;
                }
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void ScriptNameBox_TextChanged(object sender, EventArgs e)
        {
            scriptName = ScriptNameBox.Text;
        }
    }
}
