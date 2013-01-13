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

        private AddFramePanel frameAddNewFrame;





        public FrameListPanel()
        {
            base.AutoSize = false;
            Layout += new LayoutEventHandler(Panel_SizeChanged);
            ControlAdded += new ControlEventHandler(FrameListPanel_ControlAdded);

            frameAddNewFrame = new AddFramePanel(this);
            Controls.Add(frameAddNewFrame);
        }
        public void Initialize(ModelManager modelManager)
        {
            this.modelManager = modelManager;
            CurrentFrame = AddNewFrame();
        }

        void FrameListPanel_ControlAdded(object sender, ControlEventArgs e)
        {
            if (e.Control != frameAddNewFrame)
            {
                Controls.Remove(frameAddNewFrame);
                Controls.Add(frameAddNewFrame);
            }
        }



        private PicturePanel CurrentFrame
        {
            set
            {
                modelManager.CurrentFrame = value.SaveFrame;
                value.BackColor = PicturePanel.SelectedBackColor;
            }
            get
            {
                foreach (PicturePanel frame in Controls.OfType<PicturePanel>())
                {
                    if (frame.IsCurrentFrame)
                    {
                        return frame;
                    }
                }
                return null;
            }
        }
        //public SaveFrame<Texture2DWithPos> CurrentSaveFrame
       // {
        //    get { return modelManager.CurrentFrame; }
        //}

        private PicturePanel AddNewFrame()
        {
            SaveFrame<Texture2DWithPos> saveFrame = new SaveFrame<Texture2DWithPos>();
            modelManager.CurrentAnimation.Frames.Add(saveFrame);
            PicturePanel frame = new PicturePanel(this, saveFrame);
            Controls.Add(frame);
            return frame;
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
                frameListPanel.AddNewFrame();
            }
        }

        private class PicturePanel : EmptyFramePanel
        {
            new public readonly static SD.Color DefaultBackColor = SD.Color.Black;
            public readonly static SD.Color SelectedBackColor = SD.Color.SkyBlue;

            private Panel buttonDelete;
            public SaveFrame<Texture2DWithPos> SaveFrame;
            //public readonly SaveFrame<Texture2DWithDoublePos> frame;

            public bool IsCurrentFrame { get { return SaveFrame == frameListPanel.modelManager.CurrentFrame; } }

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
                frameListPanel.CurrentFrame.BackColor = DefaultBackColor;
                frameListPanel.CurrentFrame = this;
                BackColor = SelectedBackColor;
            }

            void buttonDelete_MouseClick(object sender, MouseEventArgs e)
            {
                // Remove clicked frame from the list
                int index = frameListPanel.modelManager.CurrentAnimation.Frames.IndexOf(SaveFrame);
                int count = frameListPanel.modelManager.CurrentAnimation.Frames.Count;
                frameListPanel.modelManager.CurrentAnimation.Frames.Remove(SaveFrame);
                frameListPanel.Controls.Remove(this);

                // And select a new appropriate frame if this frame was selected
                if (IsCurrentFrame)
                {
                    if (count == 1) //frame was the only one
                    {           //create new frame
                        frameListPanel.CurrentFrame = frameListPanel.AddNewFrame();
                    }
                    else if (count == index+1) //frame was at the last index
                    {           //choose previous frame
                        frameListPanel.CurrentFrame = frameListPanel.getFramePanel(
                            frameListPanel.modelManager.CurrentAnimation.Frames[index - 1]);
                    }
                    else 
                    {           //choose next frame
                        frameListPanel.CurrentFrame = frameListPanel.getFramePanel(
                            frameListPanel.modelManager.CurrentAnimation.Frames[index]);
                    }
                }
            }
        }
    }
}
