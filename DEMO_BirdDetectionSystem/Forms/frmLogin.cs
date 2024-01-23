using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BirdDetectionSystem
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }
        DataHandler dh = new DataHandler();
        List<Users> users = new List<Users>();
        static bool username = false;
        static bool password = false;
        public static string User;
        bool visible = false;
        static int attempts = 3;
        private void FrmLogin_Load(object sender, EventArgs e)
        {
            users = dh.GetUsers();
            txtUsername.Focus();
        }
        private void BtnSignIn_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text != "" && txtUsername.Text != "")
            {
                try
                {
                    foreach (var item in users)
                    {
                        if (item.Password == txtPassword.Text && item.Username == txtUsername.Text)
                        {
                            username = true;
                            password = true;
                        }
                    }
                    if (password && username)
                    {
                        User = txtUsername.Text;
                        frmMessageBox mb = new frmMessageBox();
                        this.Hide();
                        mb.ShowDialog();
                        this.Close();
                    }
                    else if (!username && !password && attempts != 0)
                    {
                        MessageBox.Show("Incorrect Details " + (attempts - 1) + " Attempts Left", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        attempts--;
                        txtPassword.Text = null;
                        txtUsername.Text = null;
                    }
                    else if (attempts == 0)
                    {
                        MessageBox.Show("You have used the maximum amount of attempts", "Exiting", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Environment.Exit(0);
                    }
                }
                catch (Exception k)
                {
                    MessageBox.Show(k.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please fill in all fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void PicboxHide_Click(object sender, EventArgs e)
        {
            if (visible)
            {
                txtPassword.UseSystemPasswordChar = true;
                picboxEye.Visible = true;
                picboxHide.Visible = false;
                visible = false;
            }
            else
            {
                txtPassword.UseSystemPasswordChar = false;
            }
        }
        private void PicboxEye_Click(object sender, EventArgs e)
        {
            if (!visible)
            {
                txtPassword.UseSystemPasswordChar = false;
                picboxEye.Visible = false;
                picboxHide.Visible = true;
                visible = true;
            }
            else
            {
                txtPassword.UseSystemPasswordChar = true;
            }
        }

       
    }
}
