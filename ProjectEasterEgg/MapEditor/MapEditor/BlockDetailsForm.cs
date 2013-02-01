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
using Mindstep.EasterEgg.MapEditor.Viewers;

namespace Mindstep.EasterEgg.MapEditor
{
    public partial class BlockDetailsForm : Form
    {
        private List<SaveBlock> oldBlocks = new List<SaveBlock>();
        private List<SaveBlock> blocks = new List<SaveBlock>();
        private const string MULTIPLE_ENTRY_TEXT = "(multiple values)";
        private const string NO_SCRIPT_NAME_TEXT = "(none)";
        private BlockViewWrapperControl wrapper;
        private bool setupDone = false;

        public BlockDetailsForm(IEnumerable<SaveBlock> blocks, Point popupLocation, BlockViewWrapperControl Wrapper)
        {
            this.oldBlocks.AddRange(blocks.Clone());
            this.blocks.AddRange(blocks);
            this.wrapper = Wrapper;
            StartPosition = FormStartPosition.Manual;
            Location = popupLocation;

            InitializeComponent();
            blockTypesDropDown.Items.AddRange(Enum.GetNames(typeof(BlockType)));
            blockTypesDropDown.Items.Remove(Enum.GetName(typeof(BlockType), BlockType.OUT_OF_BOUNDS));
            blockTypesDropDown.Items.Remove(Enum.GetName(typeof(BlockType), BlockType.EMPTY));

            if (this.blocks.All(block => block.type == this.blocks.First().type))
            {
                oldBlockType.Text = blockTypesDropDown.Text = System.Enum.GetName(typeof(BlockType), this.blocks.First().type);
            }
            else
            {
                oldBlockType.Text = blockTypesDropDown.Text = MULTIPLE_ENTRY_TEXT;
            }

            if (this.blocks.All(block => string.IsNullOrWhiteSpace(block.script)))
            {
                scriptNameBox.Text = "";
                oldScriptName.Text = NO_SCRIPT_NAME_TEXT;
            }
            else if (this.blocks.All(block => block.script == this.blocks.First().script))
            {
                oldScriptName.Text = scriptNameBox.Text = this.blocks.First().script;
            }
            else
            {
                oldScriptName.Text = scriptNameBox.Text = MULTIPLE_ENTRY_TEXT;
            }

            setupDone = true;
            updateBlocks();
        }



        private void updateBlocks()
        {
            if (setupDone)
            {
                if (blockTypesDropDown.Text != MULTIPLE_ENTRY_TEXT)
                {
                    BlockType blockType = (BlockType)Enum.Parse(typeof(BlockType), blockTypesDropDown.Text);
                    blocks.ToList().ForEach(block => block.type = blockType);
                }

                if (scriptNameBox.Text != MULTIPLE_ENTRY_TEXT)
                {
                    blocks.ToList().ForEach(block => block.script = scriptNameBox.Text);
                }
                if (oldScriptName.Text != NO_SCRIPT_NAME_TEXT)
                {
                    oldScriptNamePrefix.Visible = oldScriptName.Visible = scriptNameBox.Text != oldScriptName.Text;
                }
                oldBlockTypePrefix.Visible = oldBlockType.Visible = blockTypesDropDown.Text != oldBlockType.Text;
            }
        }
        private void resetBlocks()
        {
            for (int i = 0; i < blocks.Count; i++)
            {
                blocks[i].type = oldBlocks[i].type;
                blocks[i].script = oldBlocks[i].script;
            }
        }

        private void scriptNameBox_TextChanged(object sender, EventArgs e)
        {
            updateBlocks();
            wrapper.Invalidate();
        }
        private void blockTypesDropDown_SelectedValueChanged(object sender, EventArgs e)
        {
            updateBlocks();
            wrapper.Invalidate();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            updateBlocks();
            DialogResult = DialogResult.OK;
            Close();
        }
        private void cancelButton_Click(object sender, EventArgs e)
        {
            resetBlocks();
            DialogResult = DialogResult.Abort;
            Close();
        }
    }
}
