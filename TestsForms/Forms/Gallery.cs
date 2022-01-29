using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using FontAwesome.Sharp;
using System.Threading;

namespace TestsForms.Forms
{
    public partial class Gallery : Form
    {
        PictureBox test;
        Panel pan;
        String[] pass;
        List<PictureBox> Pictures = new List<PictureBox>();
        int CurrentImg = 0;
        Thread myThread;

        public Gallery()
        {
            InitializeComponent();
        }

        private void Gallery_Load(object sender, EventArgs e)
        {
            panel1.Parent = this;
            myThread = new Thread(new ThreadStart(myRefresh));
            myThread.Start();
            Reload();
        }

        void ShowImg(object sender, EventArgs e)
        {
            PictureBox alan = sender as PictureBox;
            PictureBox test = new PictureBox();

            test.Dock = DockStyle.Fill;
            test.Image = alan.Image;
            test.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.Image = alan.Image;
            panel1.BringToFront();
            CloseUpPicture.Visible = true;
            CloseUpPicture.BringToFront();
            setPos(test.Image);
        }

        private void CloseUpPicture_Click(object sender, EventArgs e)
        {
            OpenPictureUpPanel.BringToFront();
            CloseUpPicture.Visible = false;
        }

        private void Reload()
        {
            OpenPictureUpPanel.Controls.Clear();
            pass = Directory.GetFiles("Img");
            foreach (string fileName in pass.Reverse())
            {
                pan = new Panel();
                test = new PictureBox();
                pan.Size = new Size(300, 220);
                pan.Dock = DockStyle.Top;
                pan.BackColor = Color.Transparent;

                try
                {
                    test.Image = Image.FromFile(fileName);
                    OpenPictureUpPanel.Controls.Add(pan);
                    pan.Show();
                    pan.Controls.Add(test);
                    test.Dock = DockStyle.Fill;
                    test.SizeMode = PictureBoxSizeMode.Zoom;
                    test.Show();

                    test.Click += new System.EventHandler(ShowImg);

                    pan = new Panel();
                    pan.Size = new Size(300, 15);
                    pan.Dock = DockStyle.Top;
                    OpenPictureUpPanel.Controls.Add(pan);
                    pan.Show();
                    Pictures.Add(test);
                }
                catch { }
            }
            OpenPictureUpPanel.AutoScroll = true;
        }

        void myRefresh()
        {
            while (true)
            {
                Thread.Sleep(1);
                try
                {
                    OpenPictureUpPanel.Invoke((MethodInvoker)(() => OpenPictureUpPanel.Refresh()));
                }
                catch
                {
                    break;
                }
            }
        }

        private void Gallery_FormClosing(object sender, FormClosingEventArgs e)
        {
            myThread.Abort();
        }

        private void setPos(Image img)
        {
            for (int i = 0; i < Pictures.Count; i++)
            {
                if(img == Pictures[i].Image)
                    CurrentImg = i;
            }
        }

        //left
        private void iconButton1_Click(object sender, EventArgs e)
        {
            if(CurrentImg == Pictures.Count - 1)
            {
                pictureBox1.Image = Pictures[0].Image;
                CurrentImg = 0;
            }
            else
            {
                pictureBox1.Image = Pictures[CurrentImg + 1].Image;
                CurrentImg++;
            }
        }

        //right
        private void iconButton2_Click(object sender, EventArgs e)
        {
            if (CurrentImg == 0)
            {
                pictureBox1.Image = Pictures[Pictures.Count - 1].Image;
                CurrentImg = Pictures.Count - 1;
            }
            else
            {
                pictureBox1.Image = Pictures[CurrentImg - 1].Image;
                CurrentImg--;
            }
        }
    }
}