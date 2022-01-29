using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestsForms
{
    public partial class Form1 : Form
    {
        Form currentChildForm;
        Color startColor1, startColor2;

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            startColor1 = panel3.BackColor;
            startColor2 = panel2.BackColor;
            this.DoubleBuffered = true;
        }

        private void OpenChildForm(Form childForm)
        {
            if (currentChildForm != null)
            {
                currentChildForm.Close();
            }
            currentChildForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            MainPanel.Controls.Add(childForm);
            childForm.BringToFront();
            childForm.Show();
            iconButton1.Visible = true;
        }

        private void CloseChildForm()
        {
            if (currentChildForm != null)
            {
                currentChildForm.Close();
            }
            iconButton1.Visible = false;
        }

        //Обработка событий
        private void label1_MouseDown(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            //Закрыть
            this.Close();
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            // Развернуть
            if(this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
                iconButton3.IconFont = FontAwesome.Sharp.IconFont.Auto;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
                iconButton3.IconFont = FontAwesome.Sharp.IconFont.Solid;
            }
        }

        private void iconButton4_Click(object sender, EventArgs e)
        {
            // свернуть
            this.WindowState = FormWindowState.Minimized;

        }

        private void GallaruButton_Click(object sender, EventArgs e)
        {
            zeroRecolor();
            OpenChildForm(new Forms.Gallery());
            Color color = FormColors.GetRandomColor();
            Recolor(color);
            GallaryButton.BackColor = color;
            GallaryButton.Enabled = false;
            GallaryButton.IconFont = IconFont.Solid;
        }

        private void ProfileButton_Click(object sender, EventArgs e)
        {
            zeroRecolor();
            OpenChildForm(new Forms.Profile());
            Color color = FormColors.GetRandomColor();
            Recolor(color);
            ProfileButton.BackColor = color;
            ProfileButton.Enabled = false;
            ProfileButton.IconFont = IconFont.Solid;
        }

        private void iconButton5_Click(object sender, EventArgs e)
        {
            zeroRecolor();
            OpenChildForm(new Forms.Painter());
            Color color = FormColors.GetRandomColor();
            Recolor(color);
            iconButton5.BackColor = color;
            iconButton5.Enabled = false;
            iconButton5.IconFont = IconFont.Solid;
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            zeroRecolor();
            CloseChildForm();

            //кнопки
            GallaryButton.Enabled = true;
            GallaryButton.IconFont = IconFont.Auto;

            ProfileButton.Enabled = true;
            ProfileButton.IconFont = IconFont.Auto;

            iconButton5.Enabled = true;
            iconButton5.IconFont = IconFont.Auto;
        }

        private void Recolor(Color color)
        {
            panel3.BackColor = color;
            panel2.BackColor = Color.FromArgb(color.A, (int)(color.R * 0.8), (int)(color.G * 0.8), (int)(color.B * 0.8));
        }

        private void zeroRecolor()
        {
            //панель
            panel3.BackColor = startColor1;
            panel2.BackColor = startColor2;

            //кнопки
            GallaryButton.BackColor = startColor1;
            GallaryButton.Enabled = true;
            ProfileButton.BackColor = startColor1;
            ProfileButton.Enabled = true;
            iconButton5.BackColor = startColor1;
            iconButton5.Enabled = true;
        }
    }
}