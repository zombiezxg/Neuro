
using CustomControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsInput;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace menu
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll")]

        static extern short GetAsyncKeyState(Keys vKey);



        [DllImport("user32.dll", SetLastError = true)]
        private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, IntPtr dwExtraInfo);

        private const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const uint MOUSEEVENTF_LEFTUP = 0x0004;
        private int ZONE;
        private int width = Screen.PrimaryScreen.Bounds.Width;
        private int height = Screen.PrimaryScreen.Bounds.Height;
        private InputSimulator inputSimulator = new InputSimulator();
        bool mouseDown;
        private Point offset;
        public Form1()
        {
            InitializeComponent();
 
        }

        public static void PerformLeftClick(int x, int y)
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, IntPtr.Zero); // Simulate left mouse button down
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, IntPtr.Zero);   // Simulate left mouse button up
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            Misc.Location = new Point(16, 90);
            Main.Location = new Point(16, 90);
            Settings.Location = new Point(16, 90);

            Misc.Size = new Size(379, 241);
            Main.Size = new Size(379, 241);
            Settings.Size = new Size(379, 241);

            Main.Visible = true;
            Misc.Visible = false;
            Settings.Visible = false;
            timer2.Start();
            checkBox5.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Create an instance of WatermarkForm

            // Show the WatermarkForm as a separate window
      
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void boxTrackBar1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            button1.ForeColor = Color.White;
            button2.ForeColor = Color.Gray;
            button3.ForeColor = Color.Gray;
            Main.Visible = true;
            Misc.Visible = false;
            Settings.Visible = false;


        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void watermark1_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            button2.ForeColor = Color.White;
            button1.ForeColor = Color.Gray;
            button3.ForeColor = Color.Gray;
            Main.Visible = false;
            Misc.Visible = true;
            Settings.Visible = false;
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            button3.ForeColor = Color.White;
            button2.ForeColor = Color.Gray;
            button1.ForeColor = Color.Gray;
            Main.Visible = false;
            Misc.Visible = false;
            Settings.Visible = true;
        }


        private void mouseMove_Event(object sender, MouseEventArgs e)
        {
            if (mouseDown == true)
            {
                Point currentScreenPos = PointToScreen(e.Location);
                Location = new Point(currentScreenPos.X - offset.X, currentScreenPos.Y - offset.Y);
            }
        }

        private void mouseUp_Event(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void mouseDown_Event(object sender, MouseEventArgs e)
        {
            offset.X = e.X;
            offset.Y = e.Y;
            mouseDown = true;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                Form2 form2 = new Form2();

                form2.Visible = true;
                


            }
            else
            {
                
            }

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void Settings_Paint(object sender, PaintEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ZONE = boxTrackBar3.Value; // Update ZONE based on TrackBar value
            int tolerance = boxTrackBar2.Value; // Get the tolerance from the TrackBar

            Rectangle grabZone = new Rectangle(width / 2 - ZONE, height / 2 - ZONE, ZONE * 2, ZONE * 2);

            using (Bitmap screenshot = CaptureScreen(grabZone))
            {
                if (IsTargetColorPresent(screenshot, tolerance)) // Use the updated tolerance
                {   
                    
                    inputSimulator.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.VK_J);

                }
                else
                {

                }
            }
        }

        private Bitmap CaptureScreen(Rectangle captureZone)
        {
            Bitmap bmp = new Bitmap(captureZone.Width, captureZone.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.CopyFromScreen(captureZone.Left, captureZone.Top, 0, 0, bmp.Size);
            }
            return bmp;
        }


        // Modify IsTargetColorPresent to accept tolerance as a parameter
        private bool IsTargetColorPresent(Bitmap bmp, int tolerance)
        {
            Color targetColor = Color.FromArgb(250, 100, 250); // Example: purple

            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    Color pixelColor = bmp.GetPixel(x, y);
                    if (IsColorMatch(pixelColor, targetColor, tolerance))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool IsColorMatch(Color color, Color targetColor, int tolerance)
        {
            return Math.Abs(color.R - targetColor.R) < tolerance &&
                   Math.Abs(color.G - targetColor.G) < tolerance &&
                   Math.Abs(color.B - targetColor.B) < tolerance;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                timer1.Start();
            }
            else
            {
                timer1.Stop();
            }
        }

        private void boxTrackBar2_Load(object sender, EventArgs e)
        {
            
        }

        private void boxTrackBar1_Load_1(object sender, EventArgs e)
        {
            
        }

        private void boxTrackBar3_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (GetAsyncKeyState(Keys.G)< 0)
            {
                if (checkBox5.Checked == false)
                {
                    checkBox5.Checked = true;
                }
                else
                {
                    checkBox5.Checked = false;
                }

            }

            

            
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked)
            {
                this.Visible = false;

            }
            else { 
                this.Visible = true; 
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
    
}