using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using SD = System.Drawing;

namespace Mindstep.EasterEgg.MapEditor
{
    class MyTrackBar : TrackBar
    {
        protected override void CreateHandle() { if (!IsDisposed) base.CreateHandle(); }
    }

    [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.ToolStrip | ToolStripItemDesignerAvailability.StatusStrip)]
    public class ToolStripTrackBarItem : ToolStripControlHost
    {
        public readonly TrackBar TrackBar;
        new public bool AutoSize { get { return false; } set { } } //disable AutoSize...


        public ToolStripTrackBarItem()
            : base(new MyTrackBar())
        {
            TrackBar = (TrackBar)Control;

            TrackBar.AutoSize = false;
            base.AutoSize = false;
            
            TrackBar.Scroll += (sender, e) => { if (Scroll != null) Scroll(sender, e); };
            TrackBar.MouseUp += (sender, e) => { if (MouseUp != null) MouseUp(sender, e); };
        }



        new public string Name { get { return TrackBar.Name; } set { base.Name = TrackBar.Name = value; } }
        public TickStyle TickStyle { get { return TrackBar.TickStyle; } set { TrackBar.TickStyle = value; } }
        new public SD.Color BackColor { get { return TrackBar.BackColor; } set { base.BackColor = TrackBar.BackColor = value; } }
        public Cursor Cursor { get { return TrackBar.Cursor; } set { TrackBar.Cursor = value; } }

        //public SD.Point Location { get { return TrackBar.Location; } set { TrackBar.Location = value; } }
        new public SD.Size Size { get { return TrackBar.Size; } set { base.Size = TrackBar.Size = value; } }

        public int Value { get { return TrackBar.Value; } set { TrackBar.Value = value; } }
        public int Maximum { get { return TrackBar.Maximum; } set { TrackBar.Maximum = value; } }
        public int Minimum { get { return TrackBar.Minimum; } set { TrackBar.Minimum = value; } }
        public int LargeChange { get { return TrackBar.LargeChange; } set { TrackBar.LargeChange = value; } }
        public int SmallChange { get { return TrackBar.SmallChange; } set { TrackBar.SmallChange = value; } }
        public int TabIndex { get { return TrackBar.TabIndex; } set { TrackBar.TabIndex = value; } }
        public bool TabStop { get { return TrackBar.TabStop; } set { TrackBar.TabStop = value; } }

        public event EventHandler Scroll;
        new public event MouseEventHandler MouseUp;
    }
}
