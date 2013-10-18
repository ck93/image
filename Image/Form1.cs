using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace Image
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Bitmap image;
        int width = 512;
        int height = 512;
        int[,] R;
        int[,] G;
        int[,] B;
        void GetRGB()
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
                    // 注意，Windows 中三基色的排列顺序是 BGR 而不是 RGB！
                    B[i, j] = Convert.ToInt32(PixelValues[iPoint++]);
                    G[i, j] = Convert.ToInt32(PixelValues[iPoint++]);
                    R[i, j] = Convert.ToInt32(PixelValues[iPoint++]);
                }
            }
        }
        Bitmap FromRGB()
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
                pictureBox1.Image = FromRGB();
                
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
    }
}
