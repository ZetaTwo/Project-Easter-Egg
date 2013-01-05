using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Mindstep.EasterEgg.MapEditor
{
    public partial class PanelWithBorder : UserControl
    {
        Color borderColor = Color.Blue;
        int borderWidth = 5;

        public int BorderWidth
        {
            get { return borderWidth; }
            set
            {
                borderWidth = value;
                Invalidate();
                PerformLayout();
            }
        }

        public PanelWithBorder()
        {
            InitializeComponent();
        }

        public override Rectangle DisplayRectangle
        {
            get
            {
                return new Rectangle(borderWidth, borderWidth, Bounds.Width - borderWidth * 2, Bounds.Height - borderWidth * 2);
            }
        }

        public Color BorderColor
        {
            get { return borderColor; }
            set { borderColor = value; Invalidate(); }
        }

        new public BorderStyle BorderStyle
        {
            get { return borderWidth == 0 ? BorderStyle.None : BorderStyle.FixedSingle; }
            set { }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            if (this.BorderStyle == BorderStyle.FixedSingle)
            {
                using (Pen p = new Pen(borderColor, borderWidth))
                {
                    Rectangle r = ClientRectangle;
                    // now for the funky stuff...
                    // to get the rectangle drawn correctly, we actually need to 
                    // adjust the rectangle as .net centers the line, based on width, 
                    // on the provided rectangle.
                    r.Inflate(-Convert.ToInt32(borderWidth / 2.0 + .5), -Convert.ToInt32(borderWidth / 2.0 + .5));
                    e.Graphics.DrawRectangle(p, r);
                }
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            SetDisplayRectLocation(borderWidth, borderWidth);
        }
    }
}
