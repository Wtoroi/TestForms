using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.Resources;
using System.IO;

namespace TestsForms.Forms
{
    public partial class Profile : Form
    {
        OpenFileDialog dlg;

        public Profile()
        {
            InitializeComponent();
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            dlg = new OpenFileDialog();
            dlg.Title = "Open Image";
            dlg.Filter = "Image Files (*.bmp;*.jpg;*.jpeg,*.png)|*.BMP;*.JPG;*.JPEG;*.PNG";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                UserAvatar.Image = new Bitmap("UserLocalData/BaseAvatar.png");
                UserAvatar.Image = new Bitmap(dlg.FileName);
                File.Delete("UserLocalData/Avatar.png");
                UserAvatar.Image.Save("UserLocalData/Avatar.png");
            }
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            if (!NamePanel.Visible)
            {
                label1.Visible = false;
                panel2.Visible = true;
                NamePanel.Visible = true;
                NamePanel.Enabled = true;
                NamePanel.ReadOnly = false;
                NamePanel.Focus();
            }
            else
            {
                SetName();
            }
        }

        private void NamePanel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SetName();
            }
        }

        private void Profile_Load(object sender, EventArgs e)
        {
            NamePanel.Text = Properties.Settings.Default.UserName;
            label1.Text    = Properties.Settings.Default.UserName;
            try
            {
                UserAvatar.Image = new Bitmap("UserLocalData/Avatar.png");
            }
            catch
            {
                UserAvatar.Image = new Bitmap("UserLocalData/BaseAvatar.png");
            }
        }

        private void SetName()
        {
            NamePanel.Text = NamePanel.Text.Replace(' ', '_');
            label1.Visible = true;
            panel2.Visible = false;
            NamePanel.Visible = false;
            NamePanel.Enabled = false;
            NamePanel.ReadOnly = true;
            label1.Text = NamePanel.Text;
            Properties.Settings.Default.UserName = NamePanel.Text;
            Properties.Settings.Default.Save();
        }
    }
}