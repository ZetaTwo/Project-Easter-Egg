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
    public partial class FrameListPanel : FlowLayoutPanel
    {
        new public bool AutoSize { get { return false; } set { } } //disable AutoSize...

        private Padding frameMargin;
        public Padding FrameMargin
        {
            get { return frameMargin; }
            set
            {
                frameMargin = value;
                foreach (PicturePanel frame in Controls.OfType<PicturePanel>())
                {
                    frame.Margin = frameMargin;
                }
                PerformLayout();
            }
        }

        private float ratio;
        private ModelManager modelManager;
        [DefaultValueAttribute(typeof(float), "0.75f")]
        public float FrameRatio
        {
            get { return ratio; }
            set
            {
                ratio = value;
                PerformLayout();
            }
        }

        private AddFramePanel panelAddNewFrame;





        public FrameListPanel()
        {
            base.AutoSize = false;
            Layout += new LayoutEventHandler(Panel_SizeChanged);
            ControlAdded += new ControlEventHandler(FrameListPanel_ControlAdded);

            panelAddNewFrame = new AddFramePanel(this);
            Controls.Add(panelAddNewFrame);
        }
        public void Initialize(ModelManager modelManager)
        {
            this.modelManager = modelManager;
            modelManager.AnimationChanged +=
                new EventHandler<ModificationEventArgs<SaveAnimationWithInfo>>(modelManager_AnimationChanged);
            modelManager.FrameChanged +=
                new EventHandler<ModificationEventArgs<SaveFrame<Texture2DWithPos>>>(modelManager_FrameChanged);
            modelManager.FrameAdded += new EventHandler<AddedEventArgs<SaveFrame<Texture2DWithPos>>>(modelManager_FrameAdded);
            modelManager.FrameRemoved += new EventHandler<RemovedEventArgs<SaveFrame<Texture2DWithPos>>>(modelManager_FrameRemoved);
        }

        void modelManager_FrameAdded(object sender, AddedEventArgs<SaveFrame<Texture2DWithPos>> e)
        {
            PicturePanel panel = new PicturePanel(this, e.Element);
            Controls.Add(panel);
            Controls.SetChildIndex(panel, modelManager.Frames.IndexOf(e.Element));
        }

        void modelManager_FrameRemoved(object sender, RemovedEventArgs<SaveFrame<Texture2DWithPos>> e)
        {
            Controls.Remove(getFramePanel(e.Element));
        }

        private void modelManager_AnimationChanged(object sender, ModificationEventArgs<SaveAnimationWithInfo> e)
        {
            Controls.Clear();
            Controls.Add(panelAddNewFrame);

            if (e.After != null)
            {
                foreach (var frame in e.After.Frames)
                {
                    Controls.Add(new PicturePanel(this, frame));
                }
            }
        }

        private void modelManager_FrameChanged(object sender, ModificationEventArgs<SaveFrame<Texture2DWithPos>> e)
        {
            if (getFramePanel(e.Before) != null)
            {
                getFramePanel(e.Before).Highlighted = false;
            }
            if (getFramePanel(e.After) != null)
            {
                getFramePanel(e.After).Highlighted = true;
            }
        }

        private void FrameListPanel_ControlAdded(object sender, ControlEventArgs e)
        {
            if (e.Control != panelAddNewFrame)
            {
                Controls.Remove(panelAddNewFrame);
                Controls.Add(panelAddNewFrame);
            }
        }

        private PicturePanel getFramePanel(SaveFrame<Texture2DWithPos> saveFrame)
        {
            foreach (PicturePanel frame in Controls.OfType<PicturePanel>())
            {
                if (frame.SaveFrame == saveFrame)
                {
                    return frame;
                }
            }
            return null;
        }

        void Panel_SizeChanged(object sender, EventArgs e)
        {
            SD.Size size = Size - Padding.Size;
            SD.Size newSize;

            if (size.Width > size.Height / FrameRatio)
            {
                newSize = new SD.Size((int)(size.Height / FrameRatio), size.Height);
                this.WrapContents = false;
            }
            else
            {
                newSize = new SD.Size(size.Width, (int)(size.Width * FrameRatio));
                this.WrapContents = true;
            }

            foreach (EmptyFramePanel frame in Controls.OfType<EmptyFramePanel>())
            {
                frame.Size = newSize;
            }
        }





        private class EmptyFramePanel : PanelWithBorder
        {
            public readonly static SD.Color DefaultBorderColor = SD.Color.FromArgb(64,255,255,255);
            public readonly static SD.Color HoverBorderColor = SD.Color.FromArgb(192, 255, 255, 255);
            protected FrameListPanel frameListPanel;

            public EmptyFramePanel(FrameListPanel frameListPanel)
            {
                this.frameListPanel = frameListPanel;

                Margin = frameListPanel.FrameMargin;
                BorderColor = DefaultBorderColor;
                BorderWidth = 4;

                MouseEnter += new EventHandler(frame_MouseEnter);
                MouseLeave += new EventHandler(frame_MouseLeave);
            }

            void frame_MouseEnter(object sender, EventArgs e)
            {
                BorderColor = HoverBorderColor;
            }
            void frame_MouseLeave(object sender, EventArgs e)
            {
                BorderColor = DefaultBorderColor;
            }
        }

        private class AddFramePanel : EmptyFramePanel
        {
            new public readonly static SD.Color DefaultBackColor = SD.Color.FromArgb(255,64,64,64);

            public AddFramePanel(FrameListPanel frameListPanel)
                : base(frameListPanel)
            {
                BackColor = DefaultBackColor;
                BackgroundImage = global::Mindstep.EasterEgg.MapEditor.Properties.Resources.plus;
                BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
                MouseClick +=new MouseEventHandler(AddFrameFrame_MouseClick);
            }

            virtual public void AddFrameFrame_MouseClick(object sender, MouseEventArgs e)
            {
                frameListPanel.modelManager.Frames.Add(new SaveFrame<Texture2DWithPos>());
            }
        }

        private class PicturePanel : EmptyFramePanel
        {
            new public readonly static SD.Color DefaultBackColor = SD.Color.Black;
            public readonly static SD.Color SelectedBackColor = SD.Color.SkyBlue;

            private Panel buttonDelete;
            public SaveFrame<Texture2DWithPos> SaveFrame;

            public bool IsSelected { get { return SaveFrame == frameListPanel.modelManager.SelectedFrame; } }

            public bool Highlighted
            {
                set { BackColor = value ? SelectedBackColor : DefaultBackColor; }
                get { return BackColor == SelectedBackColor; }
            }

            public PicturePanel(FrameListPanel frameListPanel, SaveFrame<Texture2DWithPos> saveFrame)
                : base(frameListPanel)
            {
                this.SaveFrame = saveFrame;

                MouseClick += new MouseEventHandler(PictureFrame_MouseClick);
                BackColor = DefaultBackColor;
                
                buttonDelete = new Panel();
                buttonDelete.BackgroundImage = global::Mindstep.EasterEgg.MapEditor.Properties.Resources.cross;
                buttonDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
                buttonDelete.Size = buttonDelete.BackgroundImage.Size;
                buttonDelete.Anchor = AnchorStyles.Right | AnchorStyles.Top;
                buttonDelete.Left = Size.Width - buttonDelete.Size.Width;
                buttonDelete.MouseEnter += new EventHandler((sender, e) => buttonDelete.BackColor = SD.Color.DimGray);
                buttonDelete.MouseLeave += new EventHandler((sender, e) => buttonDelete.BackColor = SD.Color.Transparent);
                buttonDelete.MouseClick += new MouseEventHandler(buttonDelete_MouseClick);
                Controls.Add(buttonDelete);
            }

            public void PictureFrame_MouseClick(object sender, MouseEventArgs e)
            {
                frameListPanel.modelManager.SelectedFrame = SaveFrame;
            }

            void buttonDelete_MouseClick(object sender, MouseEventArgs e)
            {
                bool wasSelected = IsSelected;
                int index = frameListPanel.modelManager.Frames.IndexOf(SaveFrame);
                int count = frameListPanel.modelManager.Frames.Count;
                // Remove clicked frame from the list
                frameListPanel.modelManager.Frames.Remove(SaveFrame);

                // And select a new appropriate frame if this frame was selected
                if (wasSelected)
                {
                    if (count == 1) //frame was the only one
                    {           //create new frame
                        frameListPanel.modelManager.SelectedFrame = new SaveFrame<Texture2DWithPos>();
                    }
                    else if (index == count-1) //frame was at the last index
                    {           //choose previous frame
                        frameListPanel.modelManager.SelectedFrame = frameListPanel.modelManager.Frames.Last();
                    }
                    else
                    {           //choose next frame
                        frameListPanel.modelManager.SelectedFrame = frameListPanel.modelManager.Frames[index];
                    }
                }
            }
        }
    }
}
