using oci_1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Util;
using Emgu.CV.Structure;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {

        logic log = new logic();
        Image<Bgr, byte> sourceImage;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sourceImage = log.openImg();
            imageBox1.Image = sourceImage;
        
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (trackBar1.Value == 2)
            
                trackBar1.Value = 1;

            if (trackBar1.Value == 4)

                trackBar1.Value = 5;

            label1.Text = trackBar1.Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            imageBox2.Image = log.findROI(sourceImage, trackBar1.Value, trackBar2.Value);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            imageBox2.Image = log.findEdges(sourceImage, trackBar1.Value, trackBar2.Value, trackBar3.Value);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            imageBox2.Image = log.findTriangles(sourceImage, trackBar1.Value, trackBar2.Value, trackBar3.Value, trackBar4.Value*10);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            imageBox2.Image = log.findRectangles(sourceImage, trackBar1.Value, trackBar2.Value, trackBar3.Value, trackBar4.Value*10,trackBar5.Value);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            imageBox2.Image = log.findCircles(sourceImage, trackBar6.Value * 10, trackBar7.Value, trackBar8.Value, trackBar9.Value*10); 
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            label2.Text = trackBar2.Value.ToString();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            imageBox2.Image = sourceImage.Canny(trackBar1.Value, trackBar2.Value);
        }
    }
}
