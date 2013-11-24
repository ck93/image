using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Image
{
    public partial class Form2 : Form
    {
        public bool origin;
        public bool near;
        public bool liner;
        public bool cubic;
        public string originPicPath = null;
        public Form2()
        {
            InitializeComponent();
            //this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseWheel);            
        }
        /*private void pictureBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.X > 0 && e.Y > 0)
            {
                pictureBox1.Width += e.Delta;
                pictureBox1.Height += e.Delta;
            }
        }*/ 
        private void Form2_Load(object sender, EventArgs e)
        {
            
            if (origin)
            {
                if (originPicPath != null)
                    pictureBox1.Image = new Bitmap(originPicPath);
                else
                    pictureBox1.Image = new Bitmap(Properties.Resources.LENA);
                label1.Text = "原图";
                if (near)
                {
                    pictureBox2.Image = new Bitmap("near.bmp");
                    label2.Text = "最近邻插值";
                }
                else if (liner)
                {
                    pictureBox2.Image = new Bitmap("liner.bmp");
                    label2.Text = "双线性插值";
                }
                else
                {
                    pictureBox2.Image = new Bitmap("cubic.bmp");
                    label2.Text = "双三次样条插值";
                }
            }
            else
            {
                if (near && liner)
                {
                    pictureBox1.Image = new Bitmap("near.bmp");
                    pictureBox2.Image = new Bitmap("liner.bmp");
                    label1.Text = "最近邻插值";
                    label2.Text = "双线性插值";
                }
                else if (near && cubic)
                {
                    pictureBox1.Image = new Bitmap("near.bmp");
                    pictureBox2.Image = new Bitmap("cubic.bmp");
                    label1.Text = "最近邻插值";
                    label2.Text = "双三次样条性插值";
                }
                else if (liner && cubic)
                {
                    pictureBox1.Image = new Bitmap("liner.bmp");
                    pictureBox2.Image = new Bitmap("cubic.bmp");
                    label1.Text = "双线性插值";
                    label2.Text = "双三次样条插值";
                }
            }
            label1.Update();
            label2.Update();
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            pictureBox1.Image.Dispose();
            pictureBox2.Image.Dispose();
            File.Delete("near.bmp");
            File.Delete("liner.bmp");
            File.Delete("cubic.bmp");
        }
    }
}
