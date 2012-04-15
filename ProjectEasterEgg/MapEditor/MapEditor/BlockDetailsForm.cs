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
        private IEnumerable<SaveBlock> blocks;
        private const string MULTIPLE_ENTRY_TEXT = "(multiple values)";

        public BlockDetailsForm(IEnumerable<SaveBlock> blocks, Point popupLocation)
        {
            this.blocks = blocks;
            StartPosition = FormStartPosition.Manual;
            Location = popupLocation;

            InitializeComponent();
            BlockTypesDropDown.Items.AddRange(Enum.GetNames(typeof(BlockType)));

            string blockTypeText, scriptNameText;
            if (blocks.All(block => block.type == blocks.First().type))
            {
                blockTypeText = System.Enum.GetName(typeof(BlockType), blocks.First().type);
            }
            else
            {
                blockTypeText = MULTIPLE_ENTRY_TEXT;
            }

            if (blocks.All(block => block.script == blocks.First().script) ||
                blocks.All(block => string.IsNullOrWhiteSpace(block.script)))
            {
                scriptNameText = blocks.First().script;
            }
            else
            {
                scriptNameText = MULTIPLE_ENTRY_TEXT;
            }

            CurrentBlockType.Text = BlockTypesDropDown.Text = blockTypeText;
            CurrentScriptName.Text = ScriptNameBox.Text = scriptNameText;
            Show();
        }



        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Abort;
            Close();
        }



        private void okButton_Click(object sender, EventArgs e)
        {
            if (BlockTypesDropDown.Text != MULTIPLE_ENTRY_TEXT)
            {
                BlockType blockType = (BlockType)Enum.Parse(typeof(BlockType), BlockTypesDropDown.Text);
                blocks.ToList().ForEach(block => block.type = blockType);
            }

            if (ScriptNameBox.Text != MULTIPLE_ENTRY_TEXT)
            {
                blocks.ToList().ForEach(block => block.script = ScriptNameBox.Text);
            }

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
