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
using Emgu.CV.Structure;
using Emgu.CV.Util;
using oci_1;

namespace laba5
{
    public partial class Form1 : Form
    {
        logic log = new logic();
        public Image<Bgr,byte> sourceImage;
        int c = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sourceImage = log.openImg();
            imageBox1.Image = sourceImage;
        }

        private void button2_Click(object sender, EventArgs e)  //rescalse
        {
            double k = double.Parse(textBox1.Text);
            imageBox2.Image = log.makeBILINEARSH(sourceImage,k);
        }

        private void imageBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (sourceImage != null)
            {
                var copyImage = sourceImage.Copy();

                int x = (int)(e.Location.X / imageBox1.ZoomScale);
                int y = (int)(e.Location.Y / imageBox1.ZoomScale);

                log.pts[c] = new Point(x, y);
                c++;
                if (c >= 4)
                    c = 0;

                //Point center = new Point(x, y);
                int radius = 2;
                int thickness = 2;
                var color = new Bgr(Color.Blue).MCvScalar;

                for (int i = 0; i < 4; i++)
                    CvInvoke.Circle(copyImage, new Point((int)log.pts[i].X, (int)log.pts[i].Y), radius, color, thickness);

                imageBox1.Image = copyImage;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            imageBox2.Image = log.Homography(sourceImage);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            imageBox2.Image = log.Shearing(sourceImage,Convert.ToDouble(textBox2.Text));
        }

        private void button5_Click(object sender, EventArgs e)
        {
            imageBox2.Image = log.Rotation(sourceImage, Convert.ToDouble(textBox3.Text));
        }

        private void button6_Click(object sender, EventArgs e)
        {
            imageBox2.Image = log.Reflection(sourceImage, Convert.ToDouble(textBox4.Text), Convert.ToDouble(textBox5.Text));
        }
    }
}
