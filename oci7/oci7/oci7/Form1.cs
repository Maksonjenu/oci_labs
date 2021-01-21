using Emgu.CV;
using Emgu.CV.Structure;
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

namespace oci7
{
    public partial class Form1 : Form
    {
        
        Image<Bgr, byte> sourceImage;
        Image<Bgr, byte> secondImage;
        logic logic = new logic();
        byte type = 0;
        MKeyPoint[] points;
        VideoCapture video;
        bool webCamIs = false;

        public Form1()
        {
            InitializeComponent();
            imagbox3.Visible = false;
            rb1.Checked = true;
        }

    

        private void load_Click(object sender, EventArgs e)
        {
            if (webCamIs == true)
            {
                webCamIs = false;
                video.Stop();
            }
            imagbox3.Visible = false;
            sourceImage = logic.openImg();
            imagbox1.Image = sourceImage;
            
        }

        private void load2_Click(object sender, EventArgs e)
        {
            imagbox3.Visible = false;
            secondImage = logic.openImg();
            imagbox2.Image = secondImage;
            
        }

        private void rb1_CheckedChanged(object sender, EventArgs e)
        {
            type = 0;
        }

        private void rb2_CheckedChanged(object sender, EventArgs e)
        {
            type = 1;
        }

        private void rb3_CheckedChanged(object sender, EventArgs e)
        {
            type = 2;
        }

        private void dotsb_Click(object sender, EventArgs e)
        {
            imagbox3.Visible = false;
            points = logic.pointDetector(sourceImage, type);
            imagbox1.Image = logic.pointDraw(sourceImage, points);
        }

        private void function1_Click(object sender, EventArgs e)
        {
            imagbox3.Visible = false;
            points = logic.pointDetector(sourceImage, type);
            imagbox1.Image = logic.pointDraw(sourceImage, points);
            imagbox2.Image = logic.pDrawer(logic.fPoints(points, sourceImage, secondImage), secondImage);
        }

        private void homob_Click(object sender, EventArgs e)
        {
            imagbox3.Visible = false;
            imagbox2.Image = logic.Homographica(sourceImage, secondImage, type);
        }

        private void comph_Click(object sender, EventArgs e)
        {
            imagbox3.Visible = false;
            imagbox2.Image = logic.PointHomograph(sourceImage, secondImage);
        }

        private void pointcomp_Click(object sender, EventArgs e)
        {
            imagbox3.Visible = true;
            imagbox3.Image = logic.pointComp(sourceImage, secondImage);
        }

        private void webcambut_Click(object sender, EventArgs e)
        {
            video = new VideoCapture();
            webCamIs = true;
            video.ImageGrabbed += ProcessFrame;
            video.Start();
        }

        private void ProcessFrame(object sender, EventArgs e)
        {
            
                var frame = new Mat();
                video.Retrieve(frame);
                Image<Bgr, byte> image = frame.ToImage<Bgr, byte>();
                imagbox1.Image = image;
            }

        private void ProcessFrame_2(object sender, EventArgs e)
        {
            var frame = new Mat();
            video.Retrieve(frame);
            Image<Bgr, byte> image = frame.ToImage<Bgr, byte>();
            try
            {
                
                imagbox2.Image = logic.Homographica(sourceImage, image,0);
            }
            catch (Exception)
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var frame = video.QueryFrame();
            sourceImage = frame.ToImage<Bgr, byte>();
            webCamIs = false;
            video.Stop();
            imagbox1.Image = sourceImage;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            video = new VideoCapture();
            webCamIs = true;
            video.ImageGrabbed += ProcessFrame_2;
            video.Start();
        }
    }
    
}
