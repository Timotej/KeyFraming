using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeyFraming
{
    public partial class Form1 : Form
    {
        Slider slider;

        public Form1()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, false);
            this.SetStyle(ControlStyles.Opaque, false);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);

            timer1.Enabled = false;
            slider = new Slider(20, 400, 600, 400, Color.Black);
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            slider.drawSlider(e.Graphics);
        }



        private void timer1_Tick(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int fps;
            bool result = Int32.TryParse(textBox1.Text, out fps);
            if (result)
            {
                timer1.Interval = 1000 / fps;
            }
            
            timer1.Enabled = true;
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) {
                slider.addSliderPoint(e.X, e.Y);
            }
            if (e.Button == MouseButtons.Right)
            {
                slider.removeSliderPoint(e.X, e.Y);
            }
            Invalidate();
        }
    }
}
