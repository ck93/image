using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Threading;
using System.Diagnostics;
using System.IO;

namespace Image
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            image = Properties.Resources.LENA;
            GetRGB();
            comboBoxEx1.SelectedIndex = 0;
        }
        Bitmap image;
        int width = 512;
        int height = 512;
        const double pi = 3.1416;
        Stopwatch sw = new Stopwatch();
        int[,] R;
        int[,] G;
        int[,] B;
        int click_x = 0;
        int click_y = 0;
        int frame = 0;
        void GetRGB()//读取图片，数据存储到三个矩阵
        {
            Rectangle rect = new Rectangle(0, 0, width, height);
            BitmapData bmpData = image.LockBits(rect, ImageLockMode.ReadWrite, image.PixelFormat);
            IntPtr iPtr = bmpData.Scan0;
            int iBytes = width * height * 3;
            byte[] PixelValues = new byte[iBytes];
            System.Runtime.InteropServices.Marshal.Copy(iPtr, PixelValues, 0, iBytes);
            image.UnlockBits(bmpData);
            R = new int[height, width];
            G = new int[height, width];
            B = new int[height, width];
            int iPoint = 0;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    //Windows 中三基色的排列顺序是 BGR 而不是 RGB
                    B[i, j] = Convert.ToInt32(PixelValues[iPoint++]);
                    G[i, j] = Convert.ToInt32(PixelValues[iPoint++]);
                    R[i, j] = Convert.ToInt32(PixelValues[iPoint++]);
                }
            }
        }

        Bitmap FromRGB(int[,] R, int[,] G, int[,] B)//由矩阵信息还原图片
        {
            Bitmap Result = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            Rectangle rect = new Rectangle(0, 0, width, height);
            BitmapData bmpData = Result.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            IntPtr iPtr = bmpData.Scan0;
            int iStride = bmpData.Stride;
            int iBytes = width * height * 3;
            byte[] PixelValues = new byte[iBytes];
            int iPoint = 0;
            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                {
                    PixelValues[iPoint] = Convert.ToByte(B[i, j]);
                    PixelValues[iPoint + 1] = Convert.ToByte(G[i, j]);
                    PixelValues[iPoint + 2] = Convert.ToByte(R[i, j]);
                    iPoint += 3;
                }
            System.Runtime.InteropServices.Marshal.Copy(PixelValues, 0, iPtr, iBytes);
            Result.UnlockBits(bmpData);
            return Result;
        }
        int max(int m, int n)
        {
            return (m > n ? m : n);
        }
        double[] rotate(int center_x, int center_y, int x, int y, int strengh, int scale, int limit)//变换后图像中某点对应原来图像中的位置
        {           
            double[] site = new double[2];            
            double r = Math.Sqrt((center_x - x) * (center_x - x) + (center_y - y) * (center_y - y));
            double angle = Math.Atan2((double)(y - center_y), (double)(x - center_x));            
            double delta = max(limit - (int)(Math.Sqrt(48 - scale) * (int)r / 4), 0) / (double)limit;            
            if (switchButton1.Value == true)
                angle -= strengh * Math.Pow(delta, 3) / 10;               
            else
                angle += strengh * Math.Pow(delta, 3) / 10;               
            site[0] = center_x + r * Math.Cos(angle);
            site[1] = center_y + r * Math.Sin(angle);            
            return site;
        }
        int Nearest(int[,] color, double[] site)
        {            
            int x = Convert.ToInt32(site[0]);
            int y = Convert.ToInt32(site[1]);            
            return color[x, y];
        }
        int Biliner(int[,] color, double[] site)
        {
            double x0 = site[0];
            double y0 = site[1];
            int x = (int)x0;
            int y = (int)y0;
            return Convert.ToInt16((x0 - x) * (y0 - y) * color[x + 1, y + 1] + (x0 - x) * (y + 1 - y0) * color[x + 1, y] + (x + 1 - x0) * (y0 - y) * color[x, y + 1] + (x + 1 - x0) * (y + 1 - y0) * color[x, y]);
        }
        double Cubic(int[,] color, double[]site, int row)//三次样条插值
        {            
            int x = (int)site[0];
            int y = (int)site[1];
            double x0 = site[0] - x;
            double y0 = site[1] - y;
            //double x0 = site[0];
            //double y0 = site[1];
            double temp = 0;
            temp += color[x, y + row];
            temp += (-0.5 * color[x - 1, y + row] + 0.5 * color[x + 1, y + row]) * x0;
            temp += (color[x - 1, y + row] - 2.5 * color[x, y + row] + 2 * color[x + 1, y + row] - 0.5 * color[x + 2, y + row]) * x0 * x0;
            temp += (-0.5 * color[x - 1, y + row] + 1.5 * color[x, y + row] - 1.5 * color[x + 1, y + row] + 0.5 * color[x + 2, y + row]) * x0 * x0 *x0;
            return temp;
        }
        int Bicubic(int[,] color, double[] site)//双三次样条插值
        {
            double y0 = site[1] - (int)site[1];
            //double y0 = site[1];
            double f0 = Cubic(color, site, -1);
            double f1 = Cubic(color, site, 0);
            double f2 = Cubic(color, site, 1);
            double f3 = Cubic(color, site, 2);            
            double result = 0;
            result += f1;
            result += (-0.5 * f0 + 0.5 * f2) *y0;
            result += (f0 - 2.5 * f1 + 2 * f2 - 0.5 * f3) * y0 * y0;
            result += (-0.5 * f0 + 1.5 * f1 - 1.5 * f2 + 0.5 * f3) * y0 * y0 * y0;
            if (result < 0)
                result = 0;
            if (result > 255)
                result = 255;
            return Convert.ToInt16(result);
        }
        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string pic = openFileDialog1.FileName;
                image = new Bitmap(pic);
                if (image == null)
                {
                    MessageBox.Show("图片加载失败！", "错误");
                    return;
                }
                width = image.Width;
                height = image.Height;
                GetRGB();
                pictureBox1.Image = FromRGB(R, G, B);
                
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image.Save(saveFileDialog1.FileName);
                MessageBox.Show("图片保存成功","成功");
            }
        }

        Bitmap twist(int x, int y, int method, int strengh, int scale)
        {
            int limit = x;
            if (width - x < limit)
                limit = width - x;
            if (y < limit)
                limit = y;
            if (height - y < limit)
                limit = height - y;
            int[,] newR = new int[height, width];
            int[,] newG = new int[height, width];
            int[,] newB = new int[height, width];
            
            switch (method)
            {
                case 0:
                    for (int i = 0; i < height; i++)
                        for (int j = 0; j < width; j++)
                        {                    
                            double[] site = rotate(x, y, i, j, strengh, scale, limit);                                      
                            if (site[0] > -0.5 && site[0] < width - 0.5 && site[1] > -0.5 && site[1] < height - 0.5)
                            {                                          
                                newR[i, j] = Nearest(R, site);
                                newG[i, j] = Nearest(G, site);
                                newB[i, j] = Nearest(B, site);                                                       
                            }                       
                            else
                            {
                                newR[i, j] = R[i, j];
                                newG[i, j] = G[i, j];
                                newB[i, j] = B[i, j];
                            }
                            
                        }
                    break;
                case 1:
                    for (int i = 0; i < height; i++)
                        for (int j = 0; j < width; j++)
                        {
                            double[] site = rotate(x, y, i, j, strengh, scale, limit);
                            if (site[0] > -0.5 && site[0] < width - 1.5 && site[1] > -0.5 && site[1] < height - 1.5)
                            {
                                newR[i, j] = Biliner(R, site);
                                newG[i, j] = Biliner(G, site);
                                newB[i, j] = Biliner(B, site);
                            }
                            else
                            {
                                newR[i, j] = R[i, j];
                                newG[i, j] = G[i, j];
                                newB[i, j] = B[i, j];
                            }

                        }
                    break;
                case 2:
                    for (int i = 0; i < height; i++)
                        for (int j = 0; j < width; j++)
                        {
                            double[] site = rotate(x, y, i, j, strengh, scale, limit);
                            if (site[0] > 1 && site[0] < width - 2 && site[1] > 1 && site[1] < height - 2)
                            {
                                newR[i, j] = Bicubic(R, site);
                                newG[i, j] = Bicubic(G, site);
                                newB[i, j] = Bicubic(B, site);
                            }
                            else
                            {
                                newR[i, j] = R[i, j];
                                newG[i, j] = G[i, j];
                                newB[i, j] = B[i, j];
                            }

                        }
                    break;
            }
            Bitmap newpic = FromRGB(newR, newG, newB);
            return newpic;
        }
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            int y = e.X * width / 500;
            int x = e.Y * height / 500;
            click_x = x;
            click_y = y;
            pictureBox1.Image = twist(x, y, comboBoxEx1.SelectedIndex, slider1.Value, slider2.Value);
            //MessageBox.Show(sw.ElapsedMilliseconds.ToString());
            //sw.Reset();
        }

        private void slider1_ValueChanged(object sender, EventArgs e)
        {
            if (!checkBoxX1.Checked)
                pictureBox1.Image = twist(click_x, click_y, comboBoxEx1.SelectedIndex, slider1.Value, slider2.Value);
            else
            {
                int dest = slider1.Value;
                if (click_x == 0 && click_y == 0)
                {
                    click_x = width / 2;
                    click_y = height / 2;
                }
                timer2.Stop();
                timer1.Start();               
            }
        }

        private void switchButton1_ValueChanged(object sender, EventArgs e)
        {
            pictureBox1.Image = twist(click_x, click_y, comboBoxEx1.SelectedIndex, slider1.Value, slider2.Value);            
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            Bitmap originpic = FromRGB(R, G, B);
            pictureBox1.Image = originpic;
        }

        private void slider2_ValueChanged(object sender, EventArgs e)
        {
            if (!checkBoxX1.Checked)
                pictureBox1.Image = twist(click_x, click_y, comboBoxEx1.SelectedIndex, slider1.Value, slider2.Value);
            else
            {
                int dest = slider2.Value;
                if (click_x == 0 && click_y == 0)
                {
                    click_x = width / 2;
                    click_y = height / 2;
                }
                timer1.Stop();
                timer2.Start();
            }
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            Bitmap near = twist(click_x, click_y, 0, slider1.Value, slider2.Value);
            near.Save("near.bmp");
            Bitmap liner = twist(click_x, click_y, 1, slider1.Value, slider2.Value);
            liner.Save("liner.bmp");
            Bitmap cubic = twist(click_x, click_y, 1, slider1.Value, slider2.Value);
            liner.Save("cubic.bmp");
            Form2 f2 = new Form2();
            f2.origin = checkBoxItem1.Checked;
            f2.near = checkBoxItem2.Checked;
            f2.liner = checkBoxItem3.Checked;
            f2.cubic = checkBoxItem4.Checked;
            f2.ShowDialog();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            frame ++;
            pictureBox1.Image = twist(click_x, click_y, comboBoxEx1.SelectedIndex, frame, slider2.Value);
            pictureBox1.Update();
            if (frame >= slider1.Value)
            {
                frame = 0;
                timer1.Stop();
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            frame ++;
            pictureBox1.Image = twist(click_x, click_y, comboBoxEx1.SelectedIndex, slider2.Value, frame);
            pictureBox1.Update();
            if (frame >= slider2.Value)
            {
                frame = 0;
                timer2.Stop();
            }
        }

        private void comboBoxEx1_SelectedIndexChanged(object sender, EventArgs e)
        {
            pictureBox1.Image = twist(click_x, click_y, comboBoxEx1.SelectedIndex, slider1.Value, slider2.Value);
            pictureBox1.Update();
        }


    }
}
