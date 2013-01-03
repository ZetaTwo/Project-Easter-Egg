using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms.Design;
using System.Windows.Forms;
using SD = System.Drawing;
using Mindstep.EasterEgg.Commons.SaveLoad;

namespace Mindstep.EasterEgg.MapEditor
{
    [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.ToolStrip | ToolStripItemDesignerAvailability.StatusStrip)]
    public partial class ToolStripImageList : ToolStripControlHost
    {
        private FlowLayoutPanel panel;

        public ToolStripImageList()
            : base(new FlowLayoutPanel())
        {
            panel = (FlowLayoutPanel)Control;
            panel.SizeChanged += new EventHandler(Panel_SizeChanged);
            panel.BackColor = System.Drawing.Color.Pink;
            panel.Size = new SD.Size(50, 300);
            panel.Controls.Add(new Frame());
            panel.Controls.Add(new Frame());
            panel.Controls.Add(new Frame());
            panel.Controls.Add(new Frame());
        }

        public void AddFrame(SaveFrame<Texture2DWithPos> frame)
        {

        }

        void Panel_SizeChanged(object sender, EventArgs e)
        {
            SD.Size newSize = new SD.Size(panel.Height*4/3, panel.Height);
            foreach (Frame frame in panel.Controls.OfType<Frame>())
            {
                frame.Size = newSize;
            }
            Console.WriteLine("panel size changed");
        }
    }

    class Frame : PictureBox
    {
        //public readonly SaveFrame<Texture2DWithDoublePos> frame;

        public Frame()
        {
            this.Size = new SD.Size(40, 40);
            BackColor = SD.Color.Lime;
        }
    }
}
