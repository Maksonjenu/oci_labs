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
using Emgu.CV.ImgHash;
using Emgu.CV.OCR;
using oci_1;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        logic log = new logic();

        Tesseract rus = new Tesseract();
        Tesseract eng = new Tesseract();

        private VideoCapture capture;

        Image<Bgr, byte> sourceImage;

        List<Rectangle> words = new List<Rectangle>();

        private int vframe = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sourceImage = log.openImg();
            imageBox1.Image = sourceImage;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            rus = new Tesseract("D:\\", "rus", OcrEngineMode.Default);
            eng = new Tesseract("D:\\", "eng", OcrEngineMode.Default);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            imageBox2.Image = log.monohromImage(sourceImage, trackBar1.Value);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            imageBox2.Image = log.dilitImg(sourceImage, trackBar1.Value, trackBar2.Value);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            words.Clear();
            listBox1.Items.Clear();
            imageBox2.Image = log.findROIbutBETTER(sourceImage, trackBar1.Value, trackBar2.Value, trackBar3.Value, words);
            for (int i = 0; i < words.Count; i++)
            {
                sourceImage.ROI = words[i];
                var roiCopy = sourceImage.Copy();
                sourceImage.ROI = Rectangle.Empty;

                rus.SetImage(roiCopy);
                rus.Recognize();
                string text = rus.GetUTF8Text();
                listBox1.Items.Add(text);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            words.Clear();
            listBox2.Items.Clear();
            imageBox2.Image = log.findROIbutBETTER(sourceImage, trackBar1.Value, trackBar2.Value, trackBar3.Value, words);
            for (int i = 0; i < words.Count; i++)
            {
                sourceImage.ROI = words[i];
                var roiCopy = sourceImage.Copy();
                sourceImage.ROI = Rectangle.Empty;

                eng.SetImage(roiCopy);
                eng.Recognize();
                string text = eng.GetUTF8Text();
                listBox2.Items.Add(text);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            sourceImage.ROI = words[listBox1.SelectedIndex];
            var roiCopy = sourceImage.Copy();
            sourceImage.ROI = Rectangle.Empty;
            imageBox2.Image = roiCopy;
            label2.Text = Convert.ToString(listBox1.SelectedItem);
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            sourceImage.ROI = words[listBox2.SelectedIndex];
            var roiCopy = sourceImage.Copy();
            sourceImage.ROI = Rectangle.Empty;
            imageBox2.Image = roiCopy;
            label2.Text = Convert.ToString(listBox2.SelectedItem);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            imageBox2.Image = log.DetectFaceWithoutMask(sourceImage);
        }


        private void ProcessFrame_web(object sender, EventArgs e)
        {
            var frame = new Mat();
            capture.Retrieve(frame);
            Image<Bgr, byte> image = frame.ToImage<Bgr, byte>();
            imageBox1.Image = image; //вывод кадра в нужном окне
            sourceImage = image;
            imageBox2.Image = log.DetectFace(sourceImage, temp);
        }


        private void ProcessFrame_vid(object sender, EventArgs e)
        {
            var frame = new Mat();
            capture.Retrieve(frame);

            Image<Bgr, byte> image = frame.ToImage<Bgr, byte>();
            imageBox1.Image = image;
            sourceImage = image;

        }

        Image<Bgr, byte> temp;
        private void button7_Click(object sender, EventArgs e)
        {
            temp = log.openImg();
            capture = new VideoCapture();
            capture.ImageGrabbed += ProcessFrame_web;
            capture.Start();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            capture = new VideoCapture(log.openVideo());
            timer1.Enabled = true;
            capture.ImageGrabbed += timer1_Tick;
            capture.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            vframe++;
            if (vframe >= capture.GetCaptureProperty(CapProp.FrameCount))
            {
                timer1.Enabled = false;
                capture.ImageGrabbed -= timer1_Tick;
              
            }
            else
            {
                var frame = new Mat();
                capture.Retrieve(frame);

                Image<Bgr, byte> image = frame.ToImage<Bgr, byte>();
                imageBox1.Image = image;
                sourceImage = image;

                int tr1 = 0;
                int tr2 = 0;
                int tr3 = 0;

                string text = "";

                words.Clear();
                Action action_clean = () =>

                {
                    listBox2.Items.Clear();
                    tr1 = trackBar1.Value;
                    tr2 = trackBar2.Value;
                    tr3 = trackBar3.Value;

                };

                Action action_add = () =>
                {
                    listBox2.Items.Add(text);
                };
                    Invoke(action_clean);
                
                imageBox2.Image = log.findROIbutBETTER(sourceImage, tr1, tr2, tr3, words); // нужно доработать бред
                for (int i = 0; i < words.Count; i++)
                {
                    sourceImage.ROI = words[i];
                    var roiCopy = sourceImage.Copy();
                    sourceImage.ROI = Rectangle.Empty;

                    eng.SetImage(roiCopy);
                    eng.Recognize();
                    text = eng.GetUTF8Text();
                    Invoke(action_add);
                }

            }
        }
    }
}
