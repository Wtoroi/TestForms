using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestsForms.Forms
{
    public partial class Painter : Form
    {
        Bitmap SelectedImg;
        Bitmap picture;
        Thread myThread1;
        int Size = 40;

        public Painter()
        {
            InitializeComponent();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                myThread1.Abort();
            }
            catch { }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            myThread1 = new Thread(new ThreadStart(Painting));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                myThread1.Abort();
            }
            catch { }
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Open Image";
            dlg.Filter = "Image Files (*.bmp;*.jpg;*.jpeg,*.png)|*.BMP;*.JPG;*.JPEG;*.PNG";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                SelectedImg = new Bitmap(dlg.FileName);
                picture = new Bitmap(SelectedImg.Width, SelectedImg.Height);
                myThread1 = new Thread(new ThreadStart(Painting));
                myThread1.Start();
            }
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Png Files (*.png)|*.PNG";
            saveFileDialog1.Title = "Save an Image File";
            saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName != "")
            {
                pictureBox.Image.Save(saveFileDialog1.FileName);
            }
        }

        void Painting()
        {
            int max;

            if (SelectedImg.Width > SelectedImg.Height)
                max = SelectedImg.Width;
            else
                max = SelectedImg.Height;

            progressBar.Invoke((MethodInvoker)(() => progressBar.Value = 0));
            progressBar.Invoke((MethodInvoker)(() => progressBar.Maximum = (max / 2) + max + (max * 2) + (max * 4)));

            int x, y;
            Random rnd = new Random();
            Color color;
            SolidBrush Brush;
            Graphics g = Graphics.FromImage(picture);
            g.Clear(pictureBox.BackColor);
            for (int i = 0; i < max / 2; i++)
            {
                x = rnd.Next(0, SelectedImg.Width);
                y = rnd.Next(0, SelectedImg.Height);
                color = Color.FromArgb(Convert.ToInt32(255 * 0.2), SelectedImg.GetPixel(x, y));
                Brush = new SolidBrush(color);
                g.Draw(Brush, x - Size / 2, y - Size / 2, Size * 2, Size * 2, Rectangls.Checked);
                pictureBox.Invoke((MethodInvoker)(() => pictureBox.SetInage(picture)));
                progressBar.Invoke((MethodInvoker)(() => progressBar.Value++));
            }
            for (int i = 0; i < max; i++)
            {
                x = rnd.Next(Convert.ToInt32(SelectedImg.Width * 0.05), SelectedImg.Width - Convert.ToInt32(SelectedImg.Width * 0.05));
                y = rnd.Next(Convert.ToInt32(SelectedImg.Height * 0.05), SelectedImg.Height - Convert.ToInt32(SelectedImg.Height * 0.05));
                color = Color.FromArgb(Convert.ToInt32(255 * 0.3), SelectedImg.GetPixel(x, y));
                Brush = new SolidBrush(color);
                g.Draw(Brush, x - Size / 2, y - Size / 2, Size, Size, Rectangls.Checked);
                pictureBox.Invoke((MethodInvoker)(() => pictureBox.SetInage(picture)));
                progressBar.Invoke((MethodInvoker)(() => progressBar.Value++));
            }
            for (int i = 0; i < max * 2; i++)
            {
                x = rnd.Next(Convert.ToInt32(SelectedImg.Width * 0.1), SelectedImg.Width - Convert.ToInt32(SelectedImg.Width * 0.1));
                y = rnd.Next(Convert.ToInt32(SelectedImg.Height * 0.1), SelectedImg.Height - Convert.ToInt32(SelectedImg.Height * 0.1));
                color = Color.FromArgb(Convert.ToInt32(255 * 0.4), SelectedImg.GetPixel(x, y));
                Brush = new SolidBrush(color);
                g.Draw(Brush, x - Size / 2, y - Size / 2, Size / 2, Size / 2, Rectangls.Checked);
                pictureBox.Invoke((MethodInvoker)(() => pictureBox.SetInage(picture)));
                progressBar.Invoke((MethodInvoker)(() => progressBar.Value++));
            }
            for (int i = 0; i < max * 4; i++)
            {
                x = rnd.Next(Convert.ToInt32(SelectedImg.Width * 0.15), SelectedImg.Width - Convert.ToInt32(SelectedImg.Width * 0.15));
                y = rnd.Next(Convert.ToInt32(SelectedImg.Height * 0.15), SelectedImg.Height - Convert.ToInt32(SelectedImg.Height * 0.15));
                color = Color.FromArgb(Convert.ToInt32(255 * 0.4), SelectedImg.GetPixel(x, y));
                Brush = new SolidBrush(color);
                g.Draw(Brush, x - Size / 2, y - Size / 2, Size / 4, Size / 4, Rectangls.Checked);
                pictureBox.Invoke((MethodInvoker)(() => pictureBox.SetInage(picture)));
                progressBar.Invoke((MethodInvoker)(() => progressBar.Value++));
            }
        }
    }

    static class Extend
    {
        public static void SetInage(this PictureBox pictureBox, string FileName)
        {
            pictureBox.Image = new Bitmap(FileName);
        }

        public static void SetInage(this PictureBox pictureBox, Bitmap File)
        {
            pictureBox.Image = File;
        }

        public static void Draw(this Graphics g, SolidBrush Brush, int x, int y, int w, int h, bool check)
        {
            if (check)
                g.FillRectangle(Brush, x, y, w, h);
            else
                g.FillEllipse(Brush, x, y, w, h);
        }
    }
}