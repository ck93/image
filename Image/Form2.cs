using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Image
{
    public partial class Form2 : Form
    {
        public bool origin;
        public bool near;
        public bool bin;
        public Form2()
        {
            InitializeComponent();
            
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            if (origin == true)
            {
                pictureBox1.Image = new Bitmap(Properties.Resources.LENA);
                label1.Text = "原图";
                if (near == true)
                {
                    pictureBox2.Image = new Bitmap("near.bmp");
                    label2.Text = "最近邻插值";
                }
                else
                {
                    pictureBox2.Image = new Bitmap("bin.bmp");
                    label2.Text = "双线性插值";
                }
            }
            else
            {
                pictureBox1.Image = new Bitmap("near.bmp");
                pictureBox2.Image = new Bitmap("bin.bmp");
                label1.Text = "最近邻插值";
                label2.Text = "双线性插值";
            }
            label1.Update();
            label2.Update();
        }
        

    }
}
