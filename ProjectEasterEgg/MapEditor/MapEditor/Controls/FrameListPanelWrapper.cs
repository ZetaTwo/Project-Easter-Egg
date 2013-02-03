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
    public partial class FrameListPanelWrapper : UserControl
    {
        private MainForm mainForm;

        public FrameListPanelWrapper()
        {
            InitializeComponent();
        }

        internal void Initialize(MainForm mainForm)
        {
            this.mainForm = mainForm;
            frameListPanel1.Initialize(mainForm.ModelManager);
        }
    }
}
