using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        Image inputImage;
        int resizeInW = 0;
        int resizeInH = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //inputImage = Image.FromFile(Application.ExecutablePath.Replace(Application.ProductName + ".EXE", string.Empty) + "Test.png");
            //pictureBox1.Image = inputImage;
            //resizeInW = this.Width;
            //resizeInH = this.Height;
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            this.Height += (this.Width - resizeInW) * 720 / 1280 - (this.Height - resizeInH);
            resizeInW = this.Width;
            resizeInH = this.Height;
        }

        private Bitmap HighPassFilter(Image input, int threshold)
        {
            Bitmap inputBMP = new Bitmap(input);
            Bitmap resultBMP = inputBMP;
            int prevPixel = 0;

            for (int y = 0; y < input.Height; ++y)
            {
                for (int x = 0; x < input.Width; ++x)
                {
                    int nowPixel = getPixelAverage(ref inputBMP, x, y);
                    if (!(Math.Abs(nowPixel - prevPixel) > threshold))
                    {
                        resultBMP.SetPixel(x, y, Color.Black);
                    }
                }
            }

            return resultBMP;
        }

        private int getPixelAverage(ref Bitmap image, int x, int y)
        {
            Color pixel = image.GetPixel(x, y);
            return (pixel.R + pixel.G + pixel.B) / 3;
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            pictureBox1.Image = HighPassFilter(inputImage, trackBar1.Value);
            label1.Text = "Value: " + trackBar1.Value;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
                pictureBox1.Image = Image.FromFile(ofd.FileName);
            resizeInW = this.Width;
            resizeInH = this.Height;
            inputImage= pictureBox1.Image;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Студенти:" + "\r\n" +
                "Мільке Денис, Поляков Євген, Мелешко Сергій" + "\r\n" +
                "Група СЕ-307а" + "\r\n" +
                "Кафедра електроніки" + "\r\n" +
                "ННІАЕТ" + "\r\n" +
                "НАУ" + "\r\n" +
                "Україна, Київ",
                "Реквізити авторів:",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
                );

        }
    }
}
