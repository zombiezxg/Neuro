using System;
using System.Drawing;
using System.Windows.Forms;

namespace CustomControls
{
    public class BoxTrackBar : UserControl
    {
        private int minimum = 0;
        private int maximum = 100;
        private int value = 0;
        private bool dragging = false;
        private int trackLineHeight = 3; // Set the track line height to be thinner
        private int thumbSize = 10; // Set the thumb size to be larger than the track line

        public int Minimum
        {
            get { return minimum; }
            set { minimum = value; Invalidate(); }
        }

        public int Maximum
        {
            get { return maximum; }
            set { maximum = value; Invalidate(); }
        }

        public int Value
        {
            get { return this.value; }
            set
            {
                if (value >= Minimum && value <= Maximum)
                {
                    this.value = value;
                    Invalidate(); // Redraw the control when the value changes
                }
            }
        }

        public BoxTrackBar()
        {
            this.DoubleBuffered = true; // To reduce flickering
            this.MouseDown += BoxTrackBar_MouseDown;
            this.MouseMove += BoxTrackBar_MouseMove;
            this.MouseUp += BoxTrackBar_MouseUp;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            int trackY = this.Height / 2; // Center of the track bar vertically

            // Calculate the position of the thumb
            float percent = (float)(Value - Minimum) / (Maximum - Minimum);
            float thumbX = percent * (this.Width - thumbSize) + thumbSize / 2;

            // Draw the background track line
            using (Pen bgPen = new Pen(Color.FromArgb(80, 255, 255, 255), trackLineHeight)) // semi-transparent white
            {
                g.DrawLine(bgPen, thumbSize / 2, trackY, this.Width - thumbSize / 2, trackY);
            }

            // Draw the filled track line
            using (Pen fillPen = new Pen(Color.Green, trackLineHeight)) // change to the color you want for the filled part
            {
                g.DrawLine(fillPen, thumbSize / 2, trackY, thumbX, trackY);
            }

            // Draw the thumb as a circle
            using (SolidBrush brush = new SolidBrush(Color.White)) // thumb color
            {
                g.FillEllipse(brush, thumbX - thumbSize / 2, trackY - thumbSize / 2, thumbSize, thumbSize);
            }
        }







        private void BoxTrackBar_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            UpdateValueFromMouse(e.X);
        }

        private void BoxTrackBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                UpdateValueFromMouse(e.X);
            }
        }

        private void BoxTrackBar_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void UpdateValueFromMouse(int mouseX)
        {
            // Translate the mouse coordinate to a value within the trackbar's range
            int newWidth = this.Width - thumbSize; // Adjust for thumb size
            int newValue = (int)((float)(mouseX - thumbSize / 2) / newWidth * (Maximum - Minimum)) + Minimum;

            // Set the value, ensuring it's within the allowed range
            this.Value = Math.Max(Minimum, Math.Min(Maximum, newValue));
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // BoxTrackBar
            // 
            this.Name = "BoxTrackBar";
            this.Load += new System.EventHandler(this.BoxTrackBar_Load);
            this.ResumeLayout(false);

        }

        private void BoxTrackBar_Load(object sender, EventArgs e)
        {

        }
    }
}
