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
    public partial class frmMessageBox : Form
    {
        public frmMessageBox()
        {
            InitializeComponent();
        }
        private void FrmMessageBox_Load(object sender, EventArgs e)
        {
            bunifuFormFadeTransition1.ShowAsyc(this);
        }
        private void BunifuFormFadeTransition1_TransitionEnd(object sender, EventArgs e)
        {
            Icon_delay.Start();
            Icon.Enabled = true;
        }
        private void Icon_delay_Tick(object sender, EventArgs e)
        {
            Icon.Enabled = false;
            Icon_delay.Stop();
            btnOk.Visible = true;
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            frmRador rd = new frmRador();
            this.Hide();
            rd.ShowDialog();
            this.Close();
        }
    }
}
