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


namespace oci_1
{
    public partial class Form1 : Form
    {
        private Image<Bgr, byte> sourceImage;
        private VideoCapture capture;
        private logic logic = new logic();
        private bool camera = false;
        private double cannyThreshold;
        private double cannyThresholdLinking;
        public Form1()
        {
            InitializeComponent();
        }

        

        private void button1_Click(object sender, EventArgs e)  //кнопа добавит пикчу
        {
            camera = false;
            if (capture != null) 
            capture.Stop();
            
            sourceImage = logic.openImg();
            imageBox1.Image = sourceImage;
        }


        private void button2_Click(object sender, EventArgs e)
        {
          
            imageBox2.Image = logic.makeMagick(sourceImage, cannyThreshold, cannyThresholdLinking, Convert.ToInt32(textBox1.Text));
          

            
        }

      

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            cannyThreshold = hScrollBar1.Value;
            textBox2.Text = hScrollBar1.Value.ToString();
            if (checkBox1.Checked)
                imageBox2.Image = logic.makeMagick(sourceImage, cannyThreshold, cannyThresholdLinking, Convert.ToInt32(textBox1.Text));
        }

        private void hScrollBar2_Scroll(object sender, ScrollEventArgs e)
        {
            cannyThresholdLinking = hScrollBar2.Value;
            textBox3.Text = hScrollBar2.Value.ToString();
            if (checkBox1.Checked)
                imageBox2.Image = logic.makeMagick(sourceImage, cannyThreshold, cannyThresholdLinking, Convert.ToInt32(textBox1.Text));
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            camera = true;
            capture = new VideoCapture();           
            capture.ImageGrabbed += ProcessFrame_web;
            capture.Start(); 

        }
        private void ProcessFrame_web(object sender, EventArgs e)
        {
            var frame = new Mat();
            capture.Retrieve(frame); 
            Image<Bgr, byte> image = frame.ToImage<Bgr, byte>();
            imageBox1.Image = image; //вывод кадра в нужном окне
            sourceImage = image;
                imageBox2.Image = logic.makeMagick(sourceImage, cannyThreshold, cannyThresholdLinking, Convert.ToInt32(textBox1.Text));
        }

        private void ProcessFrame_vid(object sender, EventArgs e) // добавить подобие условия для видео как для вебки, разобраться с концом видеофайла
        {
            var frame = new Mat();
            capture.Retrieve(frame);
            Image<Bgr, byte> image = frame.ToImage<Bgr, byte>();
            imageBox1.Image = image; //вывод кадра в нужном окне
            sourceImage = image;
            imageBox2.Image = logic.makeMagick(sourceImage, cannyThreshold, cannyThresholdLinking, Convert.ToInt32(textBox1.Text));
            

        }



        private void button5_Click(object sender, EventArgs e) //video
        {
            capture = new VideoCapture(logic.openVideo());
            capture.ImageGrabbed += ProcessFrame_vid;
            capture.Start();
            textBox4.Text = capture.GetCaptureProperty(CapProp.FrameCount).ToString(); //количество кадров в видео
            textBox5.Text = capture.GetCaptureProperty(CapProp.XiFramerate).ToString(); //кадроы в секунду в герцах
            textBox6.Text = capture.GetCaptureProperty(CapProp.FrameHeight).ToString(); //высота кадров видео
            textBox7.Text = capture.GetCaptureProperty(CapProp.FrameWidth).ToString(); //ширина кадров видео
        }
    }
}
