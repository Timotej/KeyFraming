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
        public Form1()
        {
            InitializeComponent();
            timer1.Enabled = false;
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
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
    }
}
