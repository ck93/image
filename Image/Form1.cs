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
        
        int[,] R;
        int[,] G;
        int[,] B;
        int click_x;
        int click_y;
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
            double delta = max(limit - (int)Math.Sqrt(11 - scale) * (int)r, 0) / (double)limit;
            if (switchButton1.Value == true)
                angle -= strengh * Math.Pow(delta, 3) / 10;               
            else
                angle += strengh * Math.Pow(delta, 3) / 10;               
            site[0] = center_x + r * Math.Cos(angle);
            site[1] = center_y + r * Math.Sin(angle);
            return site;
        }
        int near(int[,] color, double[] site)
        {
            int x = (int)(site[0] * 2 - (int)site[0]);
            int y = (int)(site[1] * 2 - (int)site[1]);
            return color[x, y];
            //return color[(int)site[0],(int)site[1]];
        }
        int Biliner(int[,] color, double[] site)
        {
            double x0 = site[0];
            double y0 = site[1];
            int x = (int)x0;
            int y = (int)y0;
            return (int)((x0 - x) * (y0 - y) * color[x,y] + (x0 - x) * (y + 1 - y0) * color[x,y+1]+ (x + 1 - x0) * (y0 - y) * color[x+1,y] + (x + 1 - x0) * (y + 1 - y0) * color[x+1,y+1]);
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

        void twist(int x,int y)
        {
            int strengh = slider1.Value;
            int scale = slider2.Value;
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
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    double[] site = rotate(x, y, i, j, strengh, scale, limit);
                    if (site[0] > -0.5 && site[0] < width - 1.5 && site[1] > -0.5 && site[1] < height - 1.5)
                    {                        
                        switch (comboBoxEx1.SelectedIndex)
                        {
                            case 0:
                                newR[i, j] = near(R, site);
                                newG[i, j] = near(G, site);
                                newB[i, j] = near(B, site);
                                break;
                            case 1:
                                newR[i, j] = Biliner(R, site);
                                newG[i, j] = Biliner(G, site);
                                newB[i, j] = Biliner(B, site);
                                break;
                        }
                        
                    }
                    else
                    {
                        newR[i, j] = R[i, j];
                        newG[i, j] = G[i, j];
                        newB[i, j] = B[i, j];
                    }

                }
            }
            Bitmap newpic = FromRGB(newR, newG, newB);
            pictureBox1.Image = newpic;
        }
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            int y = e.X * width / 500;
            int x = e.Y * height / 500;
            click_x = x;
            click_y = y;
        }

        private void slider1_ValueChanged(object sender, EventArgs e)
        {
            twist(click_x,click_y);
        }

        private void switchButton1_ValueChanged(object sender, EventArgs e)
        {
            twist(click_x, click_y);
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            Bitmap originpic = FromRGB(R, G, B);
            pictureBox1.Image = originpic;
        }

        private void slider2_ValueChanged(object sender, EventArgs e)
        {
            twist(click_x, click_y);
        }
    }
}
