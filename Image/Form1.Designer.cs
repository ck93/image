namespace Image
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.buttonX2 = new DevComponents.DotNetBar.ButtonX();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.buttonX3 = new DevComponents.DotNetBar.ButtonX();
            this.buttonX4 = new DevComponents.DotNetBar.ButtonX();
            this.checkBoxItem1 = new DevComponents.DotNetBar.CheckBoxItem();
            this.checkBoxItem2 = new DevComponents.DotNetBar.CheckBoxItem();
            this.checkBoxItem3 = new DevComponents.DotNetBar.CheckBoxItem();
            this.checkBoxItem4 = new DevComponents.DotNetBar.CheckBoxItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.superTabControl1 = new DevComponents.DotNetBar.SuperTabControl();
            this.superTabControlPanel2 = new DevComponents.DotNetBar.SuperTabControlPanel();
            this.superTabItem2 = new DevComponents.DotNetBar.SuperTabItem();
            this.superTabControlPanel1 = new DevComponents.DotNetBar.SuperTabControlPanel();
            this.comboBoxEx1 = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem1 = new DevComponents.Editors.ComboItem();
            this.comboItem2 = new DevComponents.Editors.ComboItem();
            this.comboItem3 = new DevComponents.Editors.ComboItem();
            this.checkBoxX1 = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.slider2 = new DevComponents.DotNetBar.Controls.Slider();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.switchButton1 = new DevComponents.DotNetBar.Controls.SwitchButton();
            this.slider1 = new DevComponents.DotNetBar.Controls.Slider();
            this.superTabItem1 = new DevComponents.DotNetBar.SuperTabItem();
            this.slider3 = new DevComponents.DotNetBar.Controls.Slider();
            this.slider4 = new DevComponents.DotNetBar.Controls.Slider();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.superTabControl1)).BeginInit();
            this.superTabControl1.SuspendLayout();
            this.superTabControlPanel2.SuspendLayout();
            this.superTabControlPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Image = global::Image.Properties.Resources.LENA;
            this.pictureBox1.Location = new System.Drawing.Point(30, 30);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(500, 500);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.BackColor = System.Drawing.Color.Transparent;
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonX1.Location = new System.Drawing.Point(555, 44);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor(6);
            this.buttonX1.Size = new System.Drawing.Size(82, 33);
            this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX1.TabIndex = 1;
            this.buttonX1.Text = "载入图像";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "图像文件|*.bmp;*.jpg";
            // 
            // buttonX2
            // 
            this.buttonX2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX2.BackColor = System.Drawing.Color.Transparent;
            this.buttonX2.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonX2.Location = new System.Drawing.Point(657, 44);
            this.buttonX2.Name = "buttonX2";
            this.buttonX2.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor(6);
            this.buttonX2.Size = new System.Drawing.Size(82, 33);
            this.buttonX2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX2.TabIndex = 3;
            this.buttonX2.Text = "保存图像";
            this.buttonX2.Click += new System.EventHandler(this.buttonX2_Click);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "bmp";
            this.saveFileDialog1.Filter = "BMP文件|*.bmp|JPEG文件|*.jpg";
            // 
            // buttonX3
            // 
            this.buttonX3.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX3.BackColor = System.Drawing.Color.Transparent;
            this.buttonX3.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonX3.Location = new System.Drawing.Point(657, 104);
            this.buttonX3.Name = "buttonX3";
            this.buttonX3.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor(6);
            this.buttonX3.Size = new System.Drawing.Size(82, 33);
            this.buttonX3.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX3.TabIndex = 2;
            this.buttonX3.Text = "还原";
            this.buttonX3.Click += new System.EventHandler(this.buttonX3_Click);
            // 
            // buttonX4
            // 
            this.buttonX4.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX4.BackColor = System.Drawing.Color.Transparent;
            this.buttonX4.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonX4.Location = new System.Drawing.Point(555, 104);
            this.buttonX4.Name = "buttonX4";
            this.buttonX4.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor(6);
            this.buttonX4.Size = new System.Drawing.Size(82, 33);
            this.buttonX4.SplitButton = true;
            this.buttonX4.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX4.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.checkBoxItem1,
            this.checkBoxItem2,
            this.checkBoxItem3,
            this.checkBoxItem4});
            this.buttonX4.TabIndex = 9;
            this.buttonX4.Text = "对比";
            this.buttonX4.Click += new System.EventHandler(this.buttonX4_Click);
            // 
            // checkBoxItem1
            // 
            this.checkBoxItem1.AutoCollapseOnClick = false;
            this.checkBoxItem1.GlobalItem = false;
            this.checkBoxItem1.Name = "checkBoxItem1";
            this.checkBoxItem1.Text = "原图";
            // 
            // checkBoxItem2
            // 
            this.checkBoxItem2.AutoCollapseOnClick = false;
            this.checkBoxItem2.GlobalItem = false;
            this.checkBoxItem2.Name = "checkBoxItem2";
            this.checkBoxItem2.Text = "最近邻插值";
            // 
            // checkBoxItem3
            // 
            this.checkBoxItem3.AutoCollapseOnClick = false;
            this.checkBoxItem3.GlobalItem = false;
            this.checkBoxItem3.Name = "checkBoxItem3";
            this.checkBoxItem3.Text = "双线性插值";
            // 
            // checkBoxItem4
            // 
            this.checkBoxItem4.GlobalItem = false;
            this.checkBoxItem4.Name = "checkBoxItem4";
            this.checkBoxItem4.Text = "双三次样条插值";
            // 
            // timer1
            // 
            this.timer1.Interval = 50;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // superTabControl1
            // 
            this.superTabControl1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            // 
            // 
            // 
            this.superTabControl1.ControlBox.CloseBox.Name = "";
            // 
            // 
            // 
            this.superTabControl1.ControlBox.MenuBox.Name = "";
            this.superTabControl1.ControlBox.Name = "";
            this.superTabControl1.ControlBox.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.superTabControl1.ControlBox.MenuBox,
            this.superTabControl1.ControlBox.CloseBox});
            this.superTabControl1.Controls.Add(this.superTabControlPanel2);
            this.superTabControl1.Controls.Add(this.superTabControlPanel1);
            this.superTabControl1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.superTabControl1.Location = new System.Drawing.Point(555, 244);
            this.superTabControl1.Name = "superTabControl1";
            this.superTabControl1.ReorderTabsEnabled = true;
            this.superTabControl1.SelectedTabFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.superTabControl1.SelectedTabIndex = 0;
            this.superTabControl1.Size = new System.Drawing.Size(197, 286);
            this.superTabControl1.TabFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.superTabControl1.TabIndex = 11;
            this.superTabControl1.Tabs.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.superTabItem1,
            this.superTabItem2});
            this.superTabControl1.Text = "效果选择";
            // 
            // superTabControlPanel2
            // 
            this.superTabControlPanel2.Controls.Add(this.slider3);
            this.superTabControlPanel2.Controls.Add(this.slider4);
            this.superTabControlPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.superTabControlPanel2.Location = new System.Drawing.Point(0, 30);
            this.superTabControlPanel2.Name = "superTabControlPanel2";
            this.superTabControlPanel2.Size = new System.Drawing.Size(197, 256);
            this.superTabControlPanel2.TabIndex = 0;
            this.superTabControlPanel2.TabItem = this.superTabItem2;
            // 
            // superTabItem2
            // 
            this.superTabItem2.AttachedControl = this.superTabControlPanel2;
            this.superTabItem2.GlobalItem = false;
            this.superTabItem2.Name = "superTabItem2";
            this.superTabItem2.Text = "水波变换";
            // 
            // superTabControlPanel1
            // 
            this.superTabControlPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.superTabControlPanel1.Controls.Add(this.checkBoxX1);
            this.superTabControlPanel1.Controls.Add(this.slider2);
            this.superTabControlPanel1.Controls.Add(this.label1);
            this.superTabControlPanel1.Controls.Add(this.switchButton1);
            this.superTabControlPanel1.Controls.Add(this.slider1);
            this.superTabControlPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.superTabControlPanel1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.superTabControlPanel1.Location = new System.Drawing.Point(0, 30);
            this.superTabControlPanel1.Name = "superTabControlPanel1";
            this.superTabControlPanel1.Size = new System.Drawing.Size(197, 256);
            this.superTabControlPanel1.TabIndex = 1;
            this.superTabControlPanel1.TabItem = this.superTabItem1;
            // 
            // comboBoxEx1
            // 
            this.comboBoxEx1.DisplayMember = "Text";
            this.comboBoxEx1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxEx1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBoxEx1.FormattingEnabled = true;
            this.comboBoxEx1.ItemHeight = 17;
            this.comboBoxEx1.Items.AddRange(new object[] {
            this.comboItem1,
            this.comboItem2,
            this.comboItem3});
            this.comboBoxEx1.Location = new System.Drawing.Point(635, 177);
            this.comboBoxEx1.Name = "comboBoxEx1";
            this.comboBoxEx1.Size = new System.Drawing.Size(104, 23);
            this.comboBoxEx1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.comboBoxEx1.TabIndex = 18;
            this.comboBoxEx1.SelectedIndexChanged += new System.EventHandler(this.comboBoxEx1_SelectedIndexChanged);
            // 
            // comboItem1
            // 
            this.comboItem1.Text = "最近邻插值";
            // 
            // comboItem2
            // 
            this.comboItem2.Text = "双线性插值";
            // 
            // comboItem3
            // 
            this.comboItem3.Text = "双三次样条插值";
            // 
            // checkBoxX1
            // 
            this.checkBoxX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.checkBoxX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.checkBoxX1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBoxX1.Location = new System.Drawing.Point(63, 207);
            this.checkBoxX1.Name = "checkBoxX1";
            this.checkBoxX1.Size = new System.Drawing.Size(94, 26);
            this.checkBoxX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.checkBoxX1.TabIndex = 17;
            this.checkBoxX1.Text = "动画演示";
            // 
            // slider2
            // 
            this.slider2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.slider2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.slider2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.slider2.LabelWidth = 40;
            this.slider2.Location = new System.Drawing.Point(9, 153);
            this.slider2.Maximum = 40;
            this.slider2.Minimum = 1;
            this.slider2.Name = "slider2";
            this.slider2.Size = new System.Drawing.Size(182, 30);
            this.slider2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.slider2.TabIndex = 14;
            this.slider2.Text = "范围";
            this.slider2.Value = 20;
            this.slider2.ValueChanged += new System.EventHandler(this.slider2_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(555, 177);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 21);
            this.label2.TabIndex = 16;
            this.label2.Text = "插值算法";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(5, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 21);
            this.label1.TabIndex = 13;
            this.label1.Text = "旋转方向";
            // 
            // switchButton1
            // 
            // 
            // 
            // 
            this.switchButton1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.switchButton1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.switchButton1.Location = new System.Drawing.Point(87, 51);
            this.switchButton1.Name = "switchButton1";
            this.switchButton1.OffText = "顺时针";
            this.switchButton1.OnText = "逆时针";
            this.switchButton1.Size = new System.Drawing.Size(104, 30);
            this.switchButton1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.switchButton1.SwitchWidth = 35;
            this.switchButton1.TabIndex = 15;
            this.switchButton1.ValueChanged += new System.EventHandler(this.switchButton1_ValueChanged);
            // 
            // slider1
            // 
            this.slider1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.slider1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.slider1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.slider1.LabelWidth = 40;
            this.slider1.Location = new System.Drawing.Point(9, 102);
            this.slider1.Maximum = 40;
            this.slider1.Minimum = 1;
            this.slider1.Name = "slider1";
            this.slider1.Size = new System.Drawing.Size(182, 30);
            this.slider1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.slider1.TabIndex = 12;
            this.slider1.Text = "强度";
            this.slider1.Value = 20;
            this.slider1.ValueChanged += new System.EventHandler(this.slider1_ValueChanged);
            // 
            // superTabItem1
            // 
            this.superTabItem1.AttachedControl = this.superTabControlPanel1;
            this.superTabItem1.GlobalItem = false;
            this.superTabItem1.Name = "superTabItem1";
            this.superTabItem1.Text = "旋转扭曲";
            // 
            // slider3
            // 
            this.slider3.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.slider3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.slider3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.slider3.LabelWidth = 40;
            this.slider3.Location = new System.Drawing.Point(4, 118);
            this.slider3.Maximum = 50;
            this.slider3.Minimum = 10;
            this.slider3.Name = "slider3";
            this.slider3.Size = new System.Drawing.Size(182, 30);
            this.slider3.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.slider3.TabIndex = 16;
            this.slider3.Text = "幅度";
            this.slider3.Value = 30;
            this.slider3.ValueChanged += new System.EventHandler(this.slider3_ValueChanged);
            // 
            // slider4
            // 
            this.slider4.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.slider4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.slider4.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.slider4.LabelWidth = 40;
            this.slider4.Location = new System.Drawing.Point(4, 165);
            this.slider4.Maximum = 110;
            this.slider4.Minimum = 10;
            this.slider4.Name = "slider4";
            this.slider4.Size = new System.Drawing.Size(182, 30);
            this.slider4.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.slider4.TabIndex = 15;
            this.slider4.Text = "波长";
            this.slider4.Value = 60;
            this.slider4.ValueChanged += new System.EventHandler(this.slider4_ValueChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(764, 550);
            this.Controls.Add(this.comboBoxEx1);
            this.Controls.Add(this.superTabControl1);
            this.Controls.Add(this.buttonX4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonX3);
            this.Controls.Add(this.buttonX2);
            this.Controls.Add(this.buttonX1);
            this.Controls.Add(this.pictureBox1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.superTabControl1)).EndInit();
            this.superTabControl1.ResumeLayout(false);
            this.superTabControlPanel2.ResumeLayout(false);
            this.superTabControlPanel1.ResumeLayout(false);
            this.superTabControlPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private DevComponents.DotNetBar.ButtonX buttonX2;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private DevComponents.DotNetBar.ButtonX buttonX3;
        private DevComponents.DotNetBar.ButtonX buttonX4;
        private DevComponents.DotNetBar.CheckBoxItem checkBoxItem1;
        private DevComponents.DotNetBar.CheckBoxItem checkBoxItem2;
        private DevComponents.DotNetBar.CheckBoxItem checkBoxItem3;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private DevComponents.DotNetBar.CheckBoxItem checkBoxItem4;
        private DevComponents.DotNetBar.SuperTabControl superTabControl1;
        private DevComponents.DotNetBar.SuperTabControlPanel superTabControlPanel1;
        private DevComponents.DotNetBar.Controls.CheckBoxX checkBoxX1;
        private DevComponents.DotNetBar.Controls.Slider slider2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private DevComponents.DotNetBar.Controls.SwitchButton switchButton1;
        private DevComponents.DotNetBar.Controls.Slider slider1;
        private DevComponents.DotNetBar.SuperTabItem superTabItem1;
        //private DevComponents.Editors.ComboItem comboItem2;
        //private DevComponents.Editors.ComboItem comboItem1;
        //private DevComponents.Editors.ComboItem comboItem0;
        private DevComponents.DotNetBar.Controls.ComboBoxEx comboBoxEx1;
        private DevComponents.DotNetBar.SuperTabControlPanel superTabControlPanel2;
        private DevComponents.DotNetBar.SuperTabItem superTabItem2;
        private DevComponents.Editors.ComboItem comboItem1;
        private DevComponents.Editors.ComboItem comboItem2;
        private DevComponents.Editors.ComboItem comboItem3;
        private DevComponents.DotNetBar.Controls.Slider slider3;
        private DevComponents.DotNetBar.Controls.Slider slider4;
    }
}

