using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Mindstep.EasterEgg.Commons;
using Mindstep.EasterEgg.Commons.SaveLoad;

namespace Mindstep.EasterEgg.MapEditor
{
    public partial class BlockDetailsForm : Form
    {
        private List<SaveBlock> blocks = new List<SaveBlock>();
        private const string MULTIPLE_ENTRY_TEXT = "(multiple values)";

        public BlockDetailsForm(IEnumerable<SaveBlock> blocks, Point popupLocation)
        {
            this.blocks.AddRange(blocks);
            StartPosition = FormStartPosition.Manual;
            Location = popupLocation;

            InitializeComponent();
            BlockTypesDropDown.Items.AddRange(Enum.GetNames(typeof(BlockType)));
            BlockTypesDropDown.Items.Remove(Enum.GetName(typeof(BlockType), BlockType.OUT_OF_BOUNDS));
            BlockTypesDropDown.Items.Remove(Enum.GetName(typeof(BlockType), BlockType.EMPTY));

            string blockTypeText, scriptNameText;
            if (this.blocks.All(block => block.type == this.blocks.First().type))
            {
                blockTypeText = System.Enum.GetName(typeof(BlockType), this.blocks.First().type);
            }
            else
            {
                blockTypeText = MULTIPLE_ENTRY_TEXT;
            }

            if (this.blocks.All(block => block.script == this.blocks.First().script) ||
                this.blocks.All(block => string.IsNullOrWhiteSpace(block.script)))
            {
                scriptNameText = this.blocks.First().script;
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
