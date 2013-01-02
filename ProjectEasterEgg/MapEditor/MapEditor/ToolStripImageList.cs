using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms.Design;
using System.Windows.Forms;
using SD = System.Drawing;

namespace Mindstep.EasterEgg.MapEditor
{
    [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.ToolStrip | ToolStripItemDesignerAvailability.StatusStrip)]
    public partial class ToolStripImageList : ToolStripControlHost
    {
        private FlowLayoutPanel Panel;

        public ToolStripImageList()
            : base(new FlowLayoutPanel())
        {
            Panel = (FlowLayoutPanel)Control;
            Panel.SizeChanged += new EventHandler(Panel_SizeChanged);
            Panel.BackColor = System.Drawing.Color.Pink;
            Panel.Size = new SD.Size(30, 30);
        }

        public void AddFrame(SaveFrame<Texture2DWithPos>)
        {

        }

        void Panel_SizeChanged(object sender, EventArgs e)
        {
            Console.WriteLine("panel size changed");
        }
    }

    private class Frame : PictureBox
    {
        public Frame()
        {

        }
    }
}
