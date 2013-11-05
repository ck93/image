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
            sqrtTable = new double[1024,1024];
            for (int i = 0; i < 1024; i++)
            {
                int i2 = (i - 512) * (i - 512);
                for (int j = 0; j < 1024; j++)
                {
                    sqrtTable[i, j] = Math.Sqrt(i2 + (j - 512) * (j - 512));
                }
            }
            sqrtTable2 = new double[50];
            for (int i = 0; i < 50; i++)
            {
                sqrtTable2[i] = Math.Sqrt(i);
            }
            atanTable = new double[1024, 1024];
            for (int i = 0; i < 1024; i++)
            {
                for (int j = 0; j < 1024; j++)
                {
                    atanTable[i, j] = Math.Atan2(i - 512, j - 512);
                }
            }
            sinTable = new double[100000];
            for (int i = 0; i < 100000; i++)
            {
                sinTable[i] = Math.Sin((double)i / 10000);
            }
            comboBoxEx1.SelectedIndex = 1;
            comboBoxEx2.SelectedIndex = 0;
            drop_phrase = new double[20];
            drop_times = new int[20];
            drop_x = new int[20];
            drop_y = new int[20];
            
        }
        Bitmap image;
        string picPath = null;//记录原始图片路径
        int width = 512;
        int height = 512;
        const double PI = 3.1416;
        const double TWO_PI = 6.2832;
        Stopwatch sw = new Stopwatch();
        int[,] R;
        int[,] G;
        int[,] B;
        int click_x = 0;//鼠标点击位置的x坐标
        int click_y = 0;//鼠标点击位置的y坐标
        int frame = 0;//帧数
        double phrase = 0;//相位
        bool attenuation;//是否衰减（无动画效果时衰减，点击起涟漪不衰减）
        int drops = 0;//雨滴数
        double[] drop_phrase;//记录各雨滴相位
        int[] drop_times;//记录各雨滴已计算次数
        int[] drop_x;//记录各雨滴x坐标
        int[] drop_y;//记录各雨滴y坐标
        //int posi = 0;//记录快艇位置
        //double[,] buffer;//上一帧各点的偏移量
        //double[,] current;//当前帧各点的偏移量
        double[,] sqrtTable;//开平方表，用于计算距离
        double[] sqrtTable2;//开平方表，存储1到49的开方
        double[,] atanTable;//反正切表
        double[] sinTable;
        void GetRGB()//读取图片，数据存储到三个矩阵
        {
            Rectangle rect = new Rectangle(0, 0, width, height);
            BitmapData bmpData = image.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            IntPtr iPtr = bmpData.Scan0;
            int iBytes = bmpData.Stride * height;
            byte[] PixelValues = new byte[iBytes];
            System.Runtime.InteropServices.Marshal.Copy(iPtr, PixelValues, 0, iBytes);
            image.UnlockBits(bmpData);
            R = new int[height, width];
            G = new int[height, width];
            B = new int[height, width];
            int iPoint = 0;
            for (int i = 0; i < height; i++)
            {
                iPoint = i * bmpData.Stride;
                for (int j = 0; j < width; j++)
                {
                    //Windows 中三基色的排列顺序是 BGR 而不是 RGB
                    B[i, j] = Convert.ToInt32(PixelValues[iPoint++]);
                    G[i, j] = Convert.ToInt32(PixelValues[iPoint++]);
                    R[i, j] = Convert.ToInt32(PixelValues[iPoint++]);
                }
            }
            label5.Text = width.ToString() + "×" + height.ToString();
        }

        Bitmap FromRGB(int[,] R, int[,] G, int[,] B)//由矩阵信息还原图片
        {
            Bitmap Result = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            Rectangle rect = new Rectangle(0, 0, width, height);
            BitmapData bmpData = Result.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            IntPtr iPtr = bmpData.Scan0;
            int iBytes = bmpData.Stride * height;
            byte[] PixelValues = new byte[iBytes];
            int iPoint = 0;
            for (int i = 0; i < height; i++)
            {
                iPoint = i * bmpData.Stride;
                for (int j = 0; j < width; j++)
                {                    
                    PixelValues[iPoint++] = Convert.ToByte(B[i, j]);
                    PixelValues[iPoint++] = Convert.ToByte(G[i, j]);
                    PixelValues[iPoint++] = Convert.ToByte(R[i, j]);
                }
            }
            System.Runtime.InteropServices.Marshal.Copy(PixelValues, 0, iPtr, iBytes);
            Result.UnlockBits(bmpData);
            return Result;
        }
        int Nearest(int[,] color, double[] site)//最近邻插值
        {  
            int x = (int)(site[0] + 0.5);
            int y = (int)(site[1] + 0.5);
            return color[x, y];
        }
        void Biliner(int[,] R, int[,] G, int[,] B, int[,] newR, int[,] newG, int[,] newB, int i, int j, double[] site)//双线性插值
        {
            double x0 = site[0];
            double y0 = site[1];
            int x = (int)x0;
            int y = (int)y0;
            int px = (int)((x0 - x) * 2048);
            int py = (int)((y0 - y) * 2048);
            int ix = 2048 - px;
            int iy = 2048 - py;
            int w1 = px * py;
            int w2 = px * iy;
            int w3 = ix * py;
            int w4 = ix * iy;
            //return (px * (py * color[x + 1, y + 1] + iy * color[x + 1, y]) + ix * (py * color[x, y + 1] + iy * color[x, y])) >> 22;
            newR[i, j] = (w1 * R[x + 1, y + 1] + w2 * R[x + 1, y] + w3 * R[x, y + 1] + w4 * R[x, y]) >> 22;
            newG[i, j] = (w1 * G[x + 1, y + 1] + w2 * G[x + 1, y] + w3 * G[x, y + 1] + w4 * G[x, y]) >> 22;
            newB[i, j] = (w1 * B[x + 1, y + 1] + w2 * B[x + 1, y] + w3 * B[x, y + 1] + w4 * B[x, y]) >> 22;
            //double x0 = site[0];
            //double y0 = site[1];
            //int x = (int)x0;
            //int y = (int)y0;            
            //return Convert.ToInt16((x0 - x) * ((y0 - y) * color[x + 1, y + 1] + (y + 1 - y0) * color[x + 1, y]) + (x + 1 - x0) * ((y0 - y) * color[x, y + 1] + (y + 1 - y0) * color[x, y]));
            //return Convert.ToInt16((x0 - x) * ((y0 - y) * color[x + 1, y + 1] + (y + 1 - y0) * color[x + 1, y]) + (x + 1 - x0) * ((y0 - y) * color[x, y + 1] + (y + 1 - y0) * color[x, y]));
        }
        double Cubic(int[,] color, double[]site, int row)//三次样条插值
        {
            
            int x = (int)site[0];
            int y = (int)site[1];
            //int x0 = (int)((site[0] - x) * 512);
            double x0 = (site[0] - x);
            double x02 = x0 * x0;
            double temp = 0;
            temp += color[x - 1, y + row] * (-0.5 + x0 - 0.5 * x02) * x0;
            temp += color[x, y + row] * (1 + (1.5 * x0 - 2.5) * x02);
            temp += color[x + 1, y + row] * (0.5 * x0 + (2 - 1.5 * x0) * x02);
            temp += color[x + 2, y + row] * (0.5 * x0 - 0.5) * x02;
            //temp += color[x, y + row];
            //temp += (color[x + 1, y + row] - color[x - 1, y + row]) * 0.5 * x0;
            //temp += (color[x - 1, y + row] - 2.5 * color[x, y + row] + 2 * color[x + 1, y + row] - 0.5 * color[x + 2, y + row]) * x02;
            //temp += (1.5 * (color[x, y + row] - color[x + 1, y + row]) + 0.5 * (color[x + 2, y + row] - color[x - 1, y + row])) * x02 * x0;
            //temp += (long)color[x, y + row] << 27;
            //temp += (long)((-color[x - 1, y + row] + color[x + 1, y + row]) * x0) << 17;
            //temp += (long)((2 * color[x - 1, y + row] - 5 * color[x, y + row] + 4 * color[x + 1, y + row] - color[x + 2, y + row]) * x02) << 8;
            //temp += (long)(-color[x - 1, y + row] + 3 * color[x, y + row] - 3 * color[x + 1, y + row] + color[x + 2, y + row]) * x02 * x0;
            return temp;
        }
        int Bicubic(int[,] color, double[] site)//双三次样条插值
        {
            //sw.Start();
            double y0 = site[1] - (int)site[1];
            double y02 = y0 * y0;
            //int y0 = (int)((site[1] - (int)site[1]) * 512);
            //int y02 = y0 * y0;
            //long f0 = Cubic(color, site, -1);
            //long f1 = Cubic(color, site, 0);
            //long f2 = Cubic(color, site, 1);
            //long f3 = Cubic(color, site, 2);
            //long result = 0;
            //result += f1 << 27;
            //result += ((-f0 + f2) * y0) << 17;
            //result += ((2 * f0 - 5 * f1 + 4 * f2 - f3) * y02) << 8;
            //result += ((-f0 + 3 * f1 - 3 * f2 + f3) * y02 * y0);
            //result >>= 54;
            double f0 = Cubic(color, site, -1);
            double f1 = Cubic(color, site, 0);
            double f2 = Cubic(color, site, 1);
            double f3 = Cubic(color, site, 2);             
            double result = 0;           
            result += f1;
            result += (f2 - f0) * 0.5 * y0;
            result += (f0 - 2.5 * f1 + 2 * f2 - 0.5 * f3) * y02;
            result += (1.5 * (f1 - f2) + 0.5 * (f3 - f0)) * y02 * y0;
            //result += f0 * (-0.5 + y0 - 0.5 * y02) * y0;
            //result += f1 * (1 + (1.5 * y0 - 2.5) * y02);
            //result += f2 * (0.5 * y0 + (2 - 1.5 * y0) * y02);
            //result += f3 * (0.5 * y0 - 0.5) * y02;
            if (result < 0)
                result = 0;
            if (result > 255)
                result = 255;
            //sw.Stop();
            return Convert.ToInt16(result);
        }
        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                picPath = openFileDialog1.FileName;
                image = new Bitmap(picPath);
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

        int getlimit(int x, int y)
        {
            int limit = x;
            if (height - x < limit)
                limit = height - x;
            if (y < limit)
                limit = y;
            if (width - y < limit)
                limit = width - y;
            return limit;
        }
        double[] rotate(int center_x, int center_y, int x, int y, int strengh, int scale, int limit)//变换后图像中某点对应原来图像中的位置
        {
            double[] site = new double[2];
            //double r = Math.Sqrt((center_x - x) * (center_x - x) + (center_y - y) * (center_y - y));
            //double angle = Math.Atan2((double)(y - center_y), (double)(x - center_x));            
            //double delta = Math.Max(limit - (int)(Math.Sqrt(48 - scale) * (int)r / 4), 0) / (double)limit; 
            double r = sqrtTable[center_x - x + 512, center_y - y + 512];
            double angle = atanTable[y - center_y + 512, x - center_x + 512];
            double delta = Math.Max(limit - (int)(sqrtTable2[48 - scale] * (int)r / 4), 0) / (double)limit;
            if (switchButton1.Value == true)
                angle -= strengh * delta * delta * delta / 10;
            else
                angle += strengh * delta * delta * delta / 10;
            site[0] = center_x + r * Math.Cos(angle);
            //site[1] = center_y + r * Math.Sin(angle);
            site[1] = center_y - r * sinTable[(int)(10000 * (angle + PI))];
            return site;
        }
        Bitmap twist(int x, int y, int method, int strengh, int scale)
        {
            int limit = getlimit(x, y);
            int[,] newR = new int[height, width];
            int[,] newG = new int[height, width];
            int[,] newB = new int[height, width];
            
            switch (method)
            {
                case 0:
                    for (int i = 0; i < height; i++)
                        for (int j = 0; j < width; j++)
                        {
                            //sw.Start();
                            double[] site = rotate(x, y, i, j, strengh, scale, limit);
                            //sw.Stop(); 
                            if (site[1] > -0.5 && site[1] < width - 0.5 && site[0] > -0.5 && site[0] < height - 0.5)
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
                            if (site[1] > -0.5 && site[1] < width - 1.5 && site[0] > -0.5 && site[0] < height - 1.5)
                            {
                                //sw.Start();
                                //newR[i, j] = Biliner(R, site);
                                //newG[i, j] = Biliner(G, site);
                                //newB[i, j] = Biliner(B, site);
                                Biliner(R, G, B, newR, newG, newB, i, j, site);
                                //sw.Stop();
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
                            if (site[1] > 1 && site[1] < width - 2 && site[0] > 1 && site[0] < height - 2)
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
        Bitmap RainDrop()
        {
            int[,] newR = new int[height, width];
            int[,] newG = new int[height, width];
            int[,] newB = new int[height, width];
            int dx, dy, r2;
            int wavelength = 30;
            double[] delta_x = new double[20];
            double[] delta_y = new double[20];
            int[] limit = new int[20];            
            if (drop_times[0] > 149)
            {
                drops--;
                if (drops == 0)
                    timer4.Dispose();
                for (int n = 0; n < drops; n++)
                {
                    drop_phrase[n] = drop_phrase[n + 1];
                    drop_times[n] = drop_times[n + 1];
                    drop_x[n] = drop_x[n + 1];
                    drop_y[n] = drop_y[n + 1];
                }
            }
            for (int i = 0; i < drops; i++)
                limit[i] = -(int)(drop_phrase[i] * wavelength / (2 * PI));
            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                {                    
                    for(int k = 0; k < drops; k++)
                    {
                        dx = i - drop_x[k];
                        dy = j - drop_y[k];
                        r2 = dx * dx + dy * dy;                        
                        if (r2 > limit[k] * limit[k])
                        {
                            delta_x[k] = 0;
                            delta_y[k] = 0;
                        }
                        else
                        {
                            double r = sqrtTable[dx + 512, dy + 512];
                            double amount = wavelength * Math.Sin(2 * PI * r / wavelength + PI + drop_phrase[k]) / 4;
                            amount -= amount * drop_times[k] / 150;
                            delta_x[k] = dx * amount / r;
                            delta_y[k] = dy * amount / r;
                        }
                    }
                    double[] site = new double[2];
                    site[0] = i;
                    site[1] = j;
                    for (int n = 0; n < drops; n++)
                    {
                        site[0] += delta_x[n];
                        site[1] += delta_y[n];
                    }
                    if (site[1] > -0.5 && site[1] < width - 1.5 && site[0] > -0.5 && site[0] < height - 1.5)
                    {
                        Biliner(R, G, B, newR, newG, newB, i, j, site);
                    }
                    else
                    {
                        newR[i, j] = R[i, j];
                        newG[i, j] = G[i, j];
                        newB[i, j] = B[i, j];
                    }
                    
                }
            Bitmap newpic = FromRGB(newR, newG, newB);
            return newpic;
        }
        double[] Ripple(int x, int y, int dx, int dy, int limit, double phrase, int wavelength) 
        {           
            double[] site = new double[2];
            sw.Start();
            double r = sqrtTable[dx + 512, dy + 512];            
            // 计算该点振幅
            sw.Stop();
            double amount = wavelength * Math.Sin(TWO_PI * r / wavelength + PI + phrase) / 4;
            //int temp = (int)((TWO_PI * r / wavelength + PI + phrase) * 10000) % 62832;
            //double amount = wavelength * sinTable[temp] / 4;
            
            //double amount = wavelength * sinTable[(int)((2 * PI * r / wavelength + PI + phrase) % (2 * PI) * 16384)] / 4;
            // 计算衰减
            if (attenuation)
            {
                double delta = (limit - r) / limit;
                amount = amount * Math.Sqrt(delta);
            }
            // 得到偏移位置    
            
            site[0] = x + dx * amount / r;  
            site[1] = y + dy * amount / r;
            
            return site;
        }
        Bitmap wave(int x, int y, int method, double phrase, int wavelength)
        {
            int[,] newR = new int[height, width];
            int[,] newG = new int[height, width];
            int[,] newB = new int[height, width];
            int dx, dy, r2;
            int limit = width + height;
            if (comboBoxEx2.SelectedIndex == 1)
            {
                limit = -(int)(phrase * wavelength / (2 * PI));
                attenuation = false;
            }
            else if (comboBoxEx2.SelectedIndex == 0)
            {
                limit = limit * slider5.Value >> 7;
                attenuation = true;
            }
            switch (method)
            {
                case 0:
                    for (int i = 0; i < height; i++)
                        for (int j = 0; j < width; j++)
                        {
                            dx = i - x;
                            dy = j - y;
                            r2 = (i - x) * (i - x) + (j - y) * (j - y);
                            if (r2 < limit * limit)
                            {
                                double[] site = Ripple(i, j, dx, dy, limit, phrase, wavelength);
                                if (site[1] > -0.5 && site[1] < width - 0.5 && site[0] > -0.5 && site[0] < height - 0.5)
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
                            dx = i - x;
                            dy = j - y;
                            r2 = (i - x) * (i - x) + (j - y) * (j - y);
                            if (r2 < limit * limit)
                            {
                                
                                double[] site = Ripple(i, j, dx, dy, limit, phrase, wavelength);
                                
                                if (site[1] > -0.5 && site[1] < width - 1.5 && site[0] > -0.5 && site[0] < height - 1.5)
                                {
                                    
                                    //newR[i, j] = Biliner(R, site);
                                    //newG[i, j] = Biliner(G, site);
                                    //newB[i, j] = Biliner(B, site);
                                    Biliner(R, G, B, newR, newG, newB, i, j, site);
                                    
                                }
                                else
                                {
                                    newR[i, j] = R[i, j];
                                    newG[i, j] = G[i, j];
                                    newB[i, j] = B[i, j];
                                }
                                
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
                            dx = i - x;
                            dy = j - y;
                            r2 = (i - x) * (i - x) + (j - y) * (j - y);
                            if (r2 < limit * limit)
                            {
                                double[] site = Ripple(i, j, dx, dy, limit, phrase, wavelength);
                                if (site[1] > 1 && site[1] < width - 2 && site[0] > 1 && site[0] < height - 2)
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
            
            int y = e.X * width / 512;
            int x = e.Y * height / 512;
            click_x = x;
            click_y = y;
            if (superTabControl1.SelectedTab == superTabItem1)
                pictureBox1.Image = twist(x, y, comboBoxEx1.SelectedIndex, slider1.Value, slider2.Value);
            else
            {
                phrase = 0;
                if (comboBoxEx2.SelectedIndex == 0)
                    pictureBox1.Image = wave(x, y, comboBoxEx1.SelectedIndex, 0, slider4.Value);
                else if (comboBoxEx2.SelectedIndex == 1)
                    timer3.Start();
                else if (comboBoxEx2.SelectedIndex == 2)
                {
                    drops %= 10;
                    drop_phrase[drops] = 0;
                    drop_times[drops] = 0;
                    drop_x[drops] = x;
                    drop_y[drops] = y;
                    drops++;                    
                    timer4.Start();
                }
                else if (comboBoxEx2.SelectedIndex == 3)
                {
                    Random ro = new Random();
                    drops = 1;
                    drop_x[0] = ro.Next(10, height - 10);
                    drop_y[0] = ro.Next(10, width - 10);
                    drop_phrase[0] = 0;
                    drop_times[0] = 0;
                    timer4.Start();
                    timer5.Start();
                }
                else if (comboBoxEx2.SelectedIndex == 4)
                {
                    timer4.Start();
                    timer6.Start();
                }
            }
            string str1 = sw.ElapsedMilliseconds.ToString();
            FileStream fs = new FileStream(@"d:\1.txt", FileMode.Append, FileAccess.Write);
            StreamWriter stw = new StreamWriter(fs);
            stw.WriteLine(str1);
            stw.Close();
            fs.Close();
            sw.Reset();
        }

        private void slider1_ValueChanged(object sender, EventArgs e)
        {
            if (click_x == 0 && click_y == 0)
                return;
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
            if (click_x == 0 && click_y == 0)
                return;
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
            if (click_x == 0 && click_y == 0)
            {
                MessageBox.Show("请先点击扭曲中心");
                return;
            }
            Bitmap near, liner, cubic;
            if (superTabItem1.IsSelected)
            {
                near = twist(click_x, click_y, 0, slider1.Value, slider2.Value);
                liner = twist(click_x, click_y, 1, slider1.Value, slider2.Value);
                cubic = twist(click_x, click_y, 2, slider1.Value, slider2.Value);
            }
            else
            {
                near = wave(click_x, click_y, 0, 0, slider4.Value);
                liner = wave(click_x, click_y, 1, 0, slider4.Value);
                cubic = wave(click_x, click_y, 2, 0, slider4.Value);
            }
            near.Save("near.bmp");            
            liner.Save("liner.bmp");            
            liner.Save("cubic.bmp");
            Form2 f2 = new Form2();
            f2.origin = checkBoxItem1.Checked;
            f2.near = checkBoxItem2.Checked;
            f2.liner = checkBoxItem3.Checked;
            f2.cubic = checkBoxItem4.Checked;
            f2.originPicPath = picPath;
            int selectedNum = 0;
            if (f2.origin)
                selectedNum++;
            if (f2.near)
                selectedNum++;
            if (f2.liner)
                selectedNum++;
            if (f2.cubic)
                selectedNum++;
            if (selectedNum < 2)
            {
                MessageBox.Show("请选择2个需要对比的项目");
                return;
            }
            else if (selectedNum > 2)
            {
                MessageBox.Show("选择的项目过多，只能2个需要对比的项目");
                return;
            }
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
            //if (superTabControl1.SelectedTab == superTabItem1)
            //    pictureBox1.Image = twist(click_x, click_y, comboBoxEx1.SelectedIndex, slider1.Value, slider2.Value);
            //else
            //    pictureBox1.Image = wave(click_x, click_y, comboBoxEx1.SelectedIndex, 0, slider4.Value);
            //pictureBox1.Update();
        }

        private void slider4_ValueChanged(object sender, EventArgs e)
        {
            pictureBox1.Image = wave(click_x, click_y, comboBoxEx1.SelectedIndex, 0, slider4.Value);
        }

        private void slider5_ValueChanged(object sender, EventArgs e)
        {
            pictureBox1.Image = wave(click_x, click_y, comboBoxEx1.SelectedIndex, 0, slider4.Value);
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            phrase -= PI / 20;
            pictureBox1.Image = wave(click_x, click_y, comboBoxEx1.SelectedIndex, phrase, slider4.Value);
            pictureBox1.Update();
        }

        private void buttonX5_Click(object sender, EventArgs e)
        {
            timer3.Stop();
            timer4.Stop();
            timer5.Stop();
            drops = 0;
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < drops; i++)
            {
                drop_times[i]++;
                drop_phrase[i] -= PI / 16;
            }
            pictureBox1.Image = RainDrop();
            pictureBox1.Update();
            //Graphics g = pictureBox1.CreateGraphics();
            //Bitmap ship = Properties.Resources.快艇;
            //g.DrawImage(ship, posi * 3, 250);
            //posi++;
        }

        private void comboBoxEx2_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBoxEx2.SelectedIndex)
            {
                case 0:
                    timer3.Dispose();
                    timer4.Dispose();
                    timer5.Dispose();
                    timer6.Dispose();
                    drops = 0;
                    break;
                case 1:
                    timer4.Dispose();
                    timer5.Dispose();
                    timer6.Dispose();
                    drops = 0;
                    break;
                case 2:
                    timer3.Dispose();
                    timer5.Dispose();
                    timer6.Dispose();
                    drops = 0;
                    break;
                case 3:
                    timer3.Dispose();
                    timer4.Dispose();
                    timer6.Dispose();
                    drops = 0;
                    break;
                case 4:
                    timer3.Dispose();
                    timer4.Dispose();
                    timer5.Dispose();
                    drops = 0;
                    break;
            }
        }

        private void timer5_Tick(object sender, EventArgs e)
        {
            if (drops < 10)
            {
                Random ro = new Random();
                drop_x[drops] = ro.Next(10, height - 10);
                drop_y[drops] = ro.Next(10, width - 10);
                drop_phrase[drops] = 0;
                drop_times[drops] = 0;
                drops++;
            }
        }

        private void timer6_Tick(object sender, EventArgs e)
        {            
            drop_x[drops] = height / 2;
            drop_y[drops] = width * drops / 20;
            drop_phrase[drops] = 0;
            drop_times[drops] = 0;
            drops++;
            if (drops == 20)
                timer6.Stop();            
        }

    }
}
