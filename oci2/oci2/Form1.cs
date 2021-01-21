using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using oci_1;

namespace oci2
{
    public partial class Form1 : Form
    {
        logic logic = new logic();
        private Image<Bgr, byte> sourceImage; //глобальная переменная

        public Form1()
        {
            InitializeComponent();
            radioButton2.Checked = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
           sourceImage =  logic.openImg();
            if (sourceImage != null)
            sourceImage.Resize(640, 480, Inter.Linear);
            imageBox1.Image = sourceImage;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int type = 0;
            if (radioButton1.Checked)
                type = 0;
            if (radioButton2.Checked)
                type = 1;
            if (radioButton3.Checked)
                type = 2;
            if (sourceImage != null)
            imageBox2.Image = logic.chanelsChange(sourceImage, type);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            double value = Convert.ToDouble(textBox1.Text);
            imageBox2.Image = logic.contrast(sourceImage, value);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && number != 8 && number != 44) //цифры, клавиша BackSpace и запятая а ASCII
            {
                e.Handled = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            imageBox2.Image = logic.makeGray(sourceImage);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            imageBox2.Image = logic.makeSepia(sourceImage);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int type = 0;
            if (radioButton1.Checked)
                type = 0;
            if (radioButton2.Checked)
                type = 1;
            if (radioButton3.Checked)
                type = 2;

            imageBox2.Image = logic.makeHsv(sourceImage);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            imageBox2.Image = logic.makeOperate(sourceImage,0);

        }

        private void button8_Click(object sender, EventArgs e)
        {
            imageBox2.Image = logic.makeOperate(sourceImage, 1);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            imageBox2.Image = logic.makeOperate(sourceImage,  2);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            imageBox2.Image = logic.makeBlur(sourceImage);
        }

      

        private void button12_Click(object sender, EventArgs e)
        {
            imageBox2.Image = logic.makeAcva(sourceImage);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            int[,] wnd = new int[,]
        {
                { Convert.ToInt32(textBox2.Text), Convert.ToInt32(textBox3.Text), Convert.ToInt32(textBox4.Text)},
                { Convert.ToInt32(textBox5.Text),  Convert.ToInt32(textBox6.Text), Convert.ToInt32(textBox7.Text)},
                { Convert.ToInt32(textBox8.Text), Convert.ToInt32(textBox9.Text), Convert.ToInt32(textBox10.Text)}
        };
            imageBox2.Image = logic.Sharpen(sourceImage, wnd);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (trackBar2.Value > 255)
            {
                imageBox2.Image = logic.Plus(sourceImage, null, trackBar2.Value - 255, 1, 1);
            }
            else
            {
                imageBox2.Image = logic.Minus(sourceImage, null, trackBar2.Value - 255, 1, 1);
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            int type = 0;
            if (radioButton1.Checked)
                type = 0;
            if (radioButton2.Checked)
                type = 1+1;
            if (radioButton3.Checked)
                type = 2-1;
            imageBox2.Image = logic.remakeHSV(sourceImage, type, trackBar1.Value);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            imageBox2.Image = logic.makeGrayde(sourceImage);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            imageBox2.Image = logic.makeCartoon(sourceImage);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            int[,] mass = new int[3, 3]
            {
                {0,0,0},
                {-4,4,0 },
                { 0,0,0}
            };
            imageBox2.Image = logic.matrixFiltr(sourceImage, mass);
        }

        private void button18_Click(object sender, EventArgs e)
        {
            int[,] mass = new int[3, 3]
           {
                {-4,-2,0},
                {-2,1,2 },
                { 0,2,4}
           };
            imageBox2.Image = logic.matrixFiltr(sourceImage, mass);
        }
    }
}
