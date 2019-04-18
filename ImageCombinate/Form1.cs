using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;

namespace ImageCombinate
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Image img1 = pictureBox1.Image;
            Image img2 = pictureBox2.Image;
            Image img3 = pictureBox3.Image;

            int width = img1.Width + img2.Width + img3.Width;
            int h = Math.Max(img1.Height, img2.Height);
            int height = Math.Max(h, img3.Height);
            Bitmap bitmap = new Bitmap(width, height);
            //Graphics g = this.CreateGraphics();  //在界面创建画布
            Graphics g = Graphics.FromImage(bitmap);    //以bitmap为基础创建画布
            //g.DrawImage(img1, 0, 0, img1.Width, img1.Height);
            //g.DrawImage(img2, img1.Width, 0, img2.Width, img2.Height);
            //g.DrawImage(img3, img1.Width + img2.Width, 0, img3.Width, img3.Height);

            int w1 = ((img1.Width / img1.Height) * height);
            int w2 = ((img2.Width / img2.Height) * height);
            int w3 = ((img3.Width / img3.Height) * height);

            g.DrawImage(img1, 0, 0, w1, height);
            g.DrawImage(img2, w1, 0, w2, height);
            g.DrawImage(img3, w1+w2, 0, w3, height);
            Image img = bitmap;
            pictureBox4.Image = img;

        }

        private void OpenFileDialog_btn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "图片文件（*.bmp,*.jpg,*.png)|*.bmp;*.jpg;*.png|所有文件（*.*）|*.*";
            openFileDialog1.Title = "选择图片";
            openFileDialog1.Multiselect = true;
            openFileDialog1.ShowDialog();
            Image<Bgr, byte>[] sources = new Image<Bgr, byte>[openFileDialog1.FileNames.Length];
            for (int i = 0; i < openFileDialog1.FileNames.Length; i++)
            {
                sources[i] = new Image<Bgr, byte>(openFileDialog1.FileNames[i]);
            }
            if (openFileDialog1.FileNames.Length ==3 )
            {
                pictureBox1.Image = sources[0].Bitmap;
                pictureBox2.Image = sources[1].Bitmap;
                pictureBox3.Image = sources[2].Bitmap;
            }
            else MessageBox.Show("请选取至少但不超过3张图片！");

        }

        private void Save_Click(object sender, EventArgs e)
        {
            SaveFileDialog SavePic = new SaveFileDialog();
            SavePic.Title = "图片另存为";
            SavePic.Filter = "图片文件（*.bmp,*.jpg,*.png)|*.bmp;*.jpg;*.png|所有文件（*.*）|*.*";
            SavePic.ShowDialog();
            pictureBox4.Image.Save(SavePic.FileName);  //FileName实际是ShowDialog界面生成的文件的完整路径
        }
    }
}
