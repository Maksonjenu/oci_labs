﻿using System;
using System.Collections.Generic;
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
    class logic
    {
        public Emgu.CV.Image<Bgr, byte> openImg()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Файлы изображений (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png)";
            var result = openFileDialog.ShowDialog(); // открытие диалога выбора файла

            if (result == DialogResult.OK) // открытие выбранного файла
            {
                string fileName = openFileDialog.FileName;
                return new Image<Bgr, byte>(fileName).Resize(600, 450, Inter.Linear);
            }
            return null;
        }

        public string openVideo()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            var result = openFileDialog.ShowDialog(); 
            if (result == DialogResult.OK) 
            {
                string fileName = openFileDialog.FileName;
                return fileName;
            }
            return null;
        }

        public Emgu.CV.Image<Bgr, byte> makeMagick(Image<Bgr, byte> sourceImage, double cannyThreshold, double cannyThresholdLinking, int dilagate)
        {
            Image<Gray, byte> grayImage = sourceImage.Convert<Gray, byte>();

            var tempImage = grayImage.PyrDown();
            var destImage = tempImage.PyrUp();
            Image<Gray, byte> cannyEdges = destImage.Canny(cannyThreshold, cannyThresholdLinking);

            cannyEdges._Dilate(dilagate);

            var cannyEdgesBgr = cannyEdges.Convert<Bgr, byte>();
            var resultImage = sourceImage.Sub(cannyEdgesBgr); // попиксельное вычитание
                                                              //обход по каналам
            for (int channel = 0; channel < resultImage.NumberOfChannels; channel++)
                for (int x = 0; x < resultImage.Width; x++)
                    for (int y = 0; y < resultImage.Height; y++) // обход по пискелям
                    {
                        // получение цвета пикселя
                        byte color = resultImage.Data[y, x, channel];
                        if (color <= 50)
                            color = 0;
                        //else if (color <= 100)
                        //    color = 25;
                        else if (color <= 150)
                            color = 180;
                        else if (color <= 200)
                            color = 210;
                        else
                            color = 255;
                        resultImage.Data[y, x, channel] = color; // изменение цвета пикселя
                    }

            return resultImage;
        }

        public Emgu.CV.Image<Bgr, byte> chanelsChange(Image<Bgr, byte> sourceImage, int type)
        {
            var channel = sourceImage.Split()[type];

            Image<Bgr, byte> destImage = sourceImage.CopyBlank();

            VectorOfMat vm = new VectorOfMat();

            if (type == 0)
            {
                vm.Push(channel);
                vm.Push(channel.CopyBlank());
                vm.Push(channel.CopyBlank());
            }
            else if (type == 1)
            {
                vm.Push(channel.CopyBlank());
                vm.Push(channel.CopyBlank());
                vm.Push(channel);
            }
            else if (type == 2)
            {
                vm.Push(channel.CopyBlank());
                vm.Push(channel);
                vm.Push(channel.CopyBlank());
            }

            CvInvoke.Merge(vm, destImage);

            return destImage.Resize(640, 480, Inter.Linear);
        }

        public Emgu.CV.Image<Bgr, byte> contrast(Image<Bgr, byte> sourceImage, double value)
        {
            var contrastImage = sourceImage.Copy();

            for (int ch = 0; ch < 3; ch++)
                for (int y = 0; y < contrastImage.Height; y++)
                    for (int x = 0; x < contrastImage.Width; x++)
                    {
                        if ((contrastImage.Data[y, x, ch] * value) > 255)
                            contrastImage.Data[y, x, ch] = 255;
                        else
                            contrastImage.Data[y, x, ch] = Convert.ToByte(contrastImage.Data[y, x, ch] * value);
                    }

            return contrastImage;
        }

        public Emgu.CV.Image<Bgr, byte> makeGray(Image<Bgr, byte> sourceImage)
        {
            var grayImage = sourceImage.Copy();

            for (int x = 0; x < grayImage.Width; x++)
            {
                for (int y = 0; y < grayImage.Height; y++)
                {
                    for (int c = 0; c <= 2; c++)
                    {
                        grayImage.Data[y, x, c] = Convert.ToByte(0.1 * grayImage.Data[y, x, 0] + 0.6 * grayImage.Data[y, x, 1] + 0.3 * grayImage.Data[y, x, 2]);
                    }
                }
            }
            return grayImage;
        }

        public Image<Bgr, byte> makeSepia(Image<Bgr, byte> sourceImage)
        {
            var resultImage = new Image<Bgr, byte>(sourceImage.Size);
            for (int x = 0; x < sourceImage.Width; x++)
                for (int y = 0; y < sourceImage.Height; y++)
                {
                    var red = sourceImage[y, x].Red;
                    var green = sourceImage[y, x].Green;
                    var blue = sourceImage[y, x].Blue;

                    var color = red * 0.393 + green * 0.769 + blue * 0.189;
                    if (color > 255) color = 255;
                    resultImage.Data[y, x, 2] = Convert.ToByte(color);

                    color = red * 0.349 + green * 0.686 + blue * 0.168;
                    if (color > 255) color = 255;
                    resultImage.Data[y, x, 1] = Convert.ToByte(color);

                    color = red * 0.272 + green * 0.534 + blue * 0.131;
                    if (color > 255) color = 255;
                    resultImage.Data[y, x, 0] = Convert.ToByte(color);
                }
            return resultImage;
        }

        public Image<Hsv, byte> makeHsv(Image<Bgr, byte> sourceImage)
         {
                var hsvImage = sourceImage.Convert<Hsv, byte>();
                return hsvImage;
         }

        public Image<Hsv, byte> remakeHSV(Image<Bgr, byte> sourceImage, int chn, int val)
        {
            var hsvImage = makeHsv(sourceImage);
            for (int i = 0; i < hsvImage.Width; i++)
            {
                for (int j = 0; j < hsvImage.Height; j++)
                {
                    if (hsvImage.Data[j, i, chn] + val > 255)
                    {
                        hsvImage.Data[j, i, chn] = 255;
                    }
                    else
                    {
                        hsvImage.Data[j, i, chn] += Convert.ToByte(val);
                    }
                }
            }
            return hsvImage;
        }

          
        public Image<Bgr, byte> Plus(Image<Bgr, byte> image, Image<Bgr, byte> secimage, int sum, double cf1, double cf2)
        {
            var brightimage = image.Copy();
                for (int x = 0; x < brightimage.Width; x++)
                {
                    for (int y = 0; y < brightimage.Height; y++)
                    {
                        for (int c = 0; c <= 2; c++)
                        {
                            if (secimage != null)
                            {
                                if ((image.Data[y, x, c] * cf1 + secimage.Data[y, x, c] * cf2) > 255)
                                {
                                    brightimage.Data[y, x, c] = 255;
                                }
                                else
                                {
                                    brightimage.Data[y, x, c] = Convert.ToByte(image.Data[y, x, c] * cf1 + secimage.Data[y, x, c] * cf2);
                                }
                            }
                            else
                            {
                                if ((brightimage.Data[y, x, c] + sum) > 255)
                                {
                                    brightimage.Data[y, x, c] = 255;
                                }
                                else
                                {
                                    brightimage.Data[y, x, c] += Convert.ToByte(sum);
                                }
                            }
                        }
                    }
                }
            return brightimage;
        }
           
        

        public Image<Bgr, byte> Minus(Image<Bgr, byte> image, Image<Bgr, byte> secimage, int sum, double cf1, double cf2)
        {
            var brightimage = image.Copy();
            for (int x = 0; x < brightimage.Width; x++)
            {
                for (int y = 0; y < brightimage.Height; y++)
                {
                    for (int c = 0; c <= 2; c++)
                    {
                        if (secimage != null)
                        {
                            if (Convert.ToInt16(image.Data[y, x, c] * cf1 - secimage.Data[y, x, c] * cf2) < 0)
                            {
                                brightimage.Data[y, x, c] = 0;
                            }
                            else
                            {
                                brightimage.Data[y, x, c] = Convert.ToByte(image.Data[y, x, c] * cf1 - secimage.Data[y, x, c] * cf2);
                            }
                        }
                        else
                        {
                            if ((Convert.ToInt32(brightimage.Data[y, x, c]) + sum) < 0)
                            {
                                brightimage.Data[y, x, c] = 0;
                            }
                            else
                            {
                                brightimage.Data[y, x, c] = Convert.ToByte(Convert.ToInt16(brightimage.Data[y, x, c]) + sum);
                            }
                        }
                    }
                }
            }
            return brightimage;
        }

        public Image<Bgr, byte> Peresech(Image<Bgr, byte> image, Image<Bgr, byte> secimage, double sum, double cf1, double cf2)
        {
            var contrastimage = image.Copy();
            for (int x = 0; x < contrastimage.Width; x++)
            {
                for (int y = 0; y < contrastimage.Height; y++)
                {
                    for (int c = 0; c <= 2; c++)
                    {
                        if (secimage != null)
                        {
                            if (Convert.ToDouble((image.Data[y, x, c] * cf1 / 10) * (secimage.Data[y, x, c] * cf2 / 10)) > 255)
                            {
                                contrastimage.Data[y, x, c] = 255;
                            }
                            else
                            {
                                contrastimage.Data[y, x, c] = Convert.ToByte((image.Data[y, x, c] * cf1 / 10) * (secimage.Data[y, x, c] * cf2 / 10));
                            }
                        }
                        else
                        {
                            if ((contrastimage.Data[y, x, c] * Convert.ToDouble(sum / 10)) > 255)
                            {
                                contrastimage.Data[y, x, c] = 255;
                            }
                            else
                            {
                                contrastimage.Data[y, x, c] = Convert.ToByte(contrastimage.Data[y, x, c] * Convert.ToDouble(sum / 10));
                            }
                        }
                    }
                }
            }
            return contrastimage;
        }
    
          

        public Image<Bgr, byte> makeOperate(Image<Bgr, byte> sourceImage, int type)
        {
            var resultImage = new Image<Bgr, byte>(sourceImage.Size);
            switch (type)
            {
                case 0:
                    {
                        return Plus(sourceImage,openImg(),1,0.5,0.5);
                        
                    }
                case 1:
                    {
                        return Minus(sourceImage, openImg(), 1, 0.5, 0.5);
                       
                    }
                case 2:
                    {
                        return Peresech(sourceImage, openImg(), 1, 0.5, 0.5);
                       
                    }
            }
            return null;

        }

        public Image<Bgr, byte> makeBlur(Image<Bgr, byte> sourceImage)
        {
            var blurimg = sourceImage;
            var finalimage = blurimg.CopyBlank();

            for (int i = 1; i <= blurimg.Width-2; i++)
            {
                for (int j = 1; j <= blurimg.Height-2; j++)
                {
                    for (int c = 0; c <= 2; c++)
                    {
                        List<byte> blurl = new List<byte>();
                        for (int x = -1; x < 2; x++)
                        {
                            for (int y = -1; y < 2; y++)
                            {
                                blurl.Add(blurimg.Data[j + x, i + y, c]);
                            }
                        }
                        blurl.Sort();
                        finalimage.Data[j, i, c] = blurl[4];
                    }
                }
            }
            return finalimage;
        }

        public Image<Gray, byte> Sharpen(Image<Bgr, byte> sourceImage, int[,] wnd)
        {
        

            var bl = makeGray(sourceImage);

            var resultImage = new Image<Gray, byte>(bl.Size);
            for (int x = 1; x < bl.Width - 1; x++)
                for (int y = 1; y < bl.Height - 1; y++)
                {
                    double cB = 0;
                    for (int i = -1; i <= 1; i++)
                        for (int j = -1; j <= 1; j++)
                        {
                            cB += sourceImage.Data[y + i, x + j, 0] * wnd[i + 1, j + 1];
                        }
                    if (cB > 254)
                        cB = 255;
                    if (cB < 0)
                        cB = 0;
                    resultImage.Data[y, x, 0] = Convert.ToByte(cB);

                }
            return resultImage;
        }

        public Image<Bgr,byte> makeAcva (Image<Bgr,byte> sourceImage)
        {
            var temp = makeOperate(sourceImage, 0);
            var temp1 = makeBlur(temp);
            return temp1;

        }

        public Image<Gray, byte> makeGrayde(Image<Bgr, byte> sourceImage)
        {
            var edges = makeGray(sourceImage);
            Image<Gray, byte> grayedges = edges.Convert<Gray, byte>();
            grayedges = grayedges.ThresholdAdaptive(new Gray(255), AdaptiveThresholdType.MeanC, ThresholdType.Binary, 3, new Gray(0.03));
            return grayedges;
        }

        public Image<Bgr, byte> makeCartoon(Image<Bgr, byte> sourceImage)
        {
            var carimg = makeBlur(sourceImage);
            Image<Gray, byte> grayedge = makeGrayde(carimg);
            for (int i = 0; i < grayedge.Width; i++)
            {
                for (int j = 0; j < grayedge.Height; j++)
                {
                    if (grayedge.Data[j, i, 0] > 0)
                    {

                    }
                    else
                    {
                        carimg.Data[j, i, 0] = 0;
                        carimg.Data[j, i, 1] = 0;
                        carimg.Data[j, i, 2] = 0;
                    }
                }
            }
            return carimg;
        }

        public Image<Bgr, byte> matrixFiltr(Image<Bgr, byte> sourceImage, int[,] mat)
        {
            var sharpimage = sourceImage.Copy();
            var finalimage = sharpimage.CopyBlank();
            for (int i = 1; i < sharpimage.Width-2; i++)
            {
                for (int j = 1; j < sharpimage.Height-2; j++)
                {
                    for (int c = 0; c <= 2; c++)
                    {
                        int result = 0;
                        for (int x = -1; x < 2; x++)
                        {
                            for (int y = -1; y < 2; y++)
                            {
                                result += sharpimage.Data[j + y, i + x, c] * mat[x + 1, y + 1];
                            }
                        }

                        if (result > 255) result = 255; else if (result < 0) result = 0;
                        finalimage.Data[j, i, c] = Convert.ToByte(result);
                    }
                }
            }
            return finalimage;
        }

    }
}
