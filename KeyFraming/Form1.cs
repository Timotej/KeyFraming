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
        Cube cube;
        List<Cube> allFrames;
        int currentFrame = 0;
        bool animate = false;

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
            slider = new Slider(50, 450, 550, 450, Color.Black);
            cube = new Cube();

            textBox1.Text = "30";
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Pen p = new Pen(Color.Blue);
            p.Width = 2;
            e.Graphics.DrawRectangle(p, 0, 0, 600, 400);
            
            slider.drawSlider(e.Graphics);

            if (animate)
            {
                allFrames[currentFrame].drawCube(e.Graphics);
            }
            else
            {
                slider.kfc.keyframes[slider.current].drawCube(e.Graphics);
            }

        }



        private void timer1_Tick(object sender, EventArgs e)
        {
            if (currentFrame < allFrames.Count-1)
            {
                currentFrame++;
            }
            else {
                timer1.Enabled = false;
                animate = false;
            }
            Invalidate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int fps;
            bool result = Int32.TryParse(textBox1.Text, out fps);
            if (result)
            {
                timer1.Interval = 1000 / fps;
                slider.kfc.fps = fps;
            }
            allFrames = slider.kfc.generateAllFrames();
            animate = true;
            currentFrame = 0;
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

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.X >= 0 && e.X <= 600 - cube.size && e.Y >= 0 && e.Y <= 400 - cube.size) 
            {

                if (e.Button == MouseButtons.Left && ModifierKeys == Keys.Control)
                {
                    slider.kfc.keyframes[slider.current].setNewOrigin(e.X, e.Y);
                    Invalidate();
                }
            }
        }
    }
}
