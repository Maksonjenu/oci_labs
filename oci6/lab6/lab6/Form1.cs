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

namespace lab6
{
    public partial class Form1 : Form
    {

        VideoCapture video;
        int vidFrame = 0;
        Image<Gray, byte> background;
        logic logic = new logic();
        byte typs = 0;
        BackgroundSubtractorMOG2 backgrSubstr = new BackgroundSubtractorMOG2(1000, 32, true);
        bool webCamIs = false;


        public Form1()
        {
            InitializeComponent();
            radioButton1.Checked = true;
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            vidFrame++;
            if (vidFrame >= video.GetCaptureProperty(CapProp.FrameCount))
            {
                timer1.Enabled = false;
            }
            else
            {
                var frame = video.QueryFrame();
                Image<Bgr, byte> image = frame.ToImage<Bgr, byte>();

                if (typs == 0)
                {
                    imageBox1.Image = image;
                }
                else
                    if (background != null && typs == 1)
                    {
                    imageBox1.Image = logic.diffusal(image, background);
                    }
                else
                if (typs == 2)
                {
                    var foregroundMask = image.Convert<Gray, byte>().CopyBlank();
                    backgrSubstr.Apply(image.Convert<Gray, byte>(), foregroundMask);
                    var filtrMask = logic.FilterMask(foregroundMask, image);
                    imageBox1.Image = filtrMask;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (webCamIs == true)
            {
                webCamIs = false;
                video.Stop();
            }
            vidFrame = 0;
            backgrSubstr.Clear();
            video = new VideoCapture(logic.openVideo());
            timer1.Enabled = true;
            background = null;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            typs = 0;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            typs = 2;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (background != null)
            {
                typs = 1;
            }
            else
            {
                typs = 0;
                radioButton1.Checked = true;
                label1.Text = "brg is null";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            backgrSubstr.Clear();
            webCamIs = true;
            timer1.Enabled = false;
            video = new VideoCapture();
            video.ImageGrabbed += ProcessFrame;
            video.Start();
        }
        private void ProcessFrame(object sender, EventArgs e)
        {
            if (webCamIs == true)
            {
                var frame = new Mat();
                video.Retrieve(frame);
                Image<Bgr, byte> image = frame.ToImage<Bgr, byte>();
                if (typs == 0)
                {
                    imageBox1.Image = image;
                }
                else
                if (background != null && typs == 1)
                {
                    imageBox1.Image = logic.diffusal(image, background);
                }
                else
                if (typs== 2)
                {
                    var foregroundMask = image.Convert<Gray, byte>().CopyBlank();
                    backgrSubstr.Apply(image.Convert<Gray, byte>(), foregroundMask);
                    var filteredMask = logic.FilterMask(foregroundMask, image);
                    imageBox1.Image = filteredMask;
                }
            }
        }

       

        private void button3_Click(object sender, EventArgs e)
        {
            var frame = video.QueryFrame();
            background = frame.ToImage<Gray, byte>();
        }
    }
}
