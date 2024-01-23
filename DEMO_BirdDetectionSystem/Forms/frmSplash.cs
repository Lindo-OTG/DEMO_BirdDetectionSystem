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
    public partial class frmSplash : Form
    {
        Timer t = new Timer();
        frmLogin l = new frmLogin();
        public frmSplash()
        {
            InitializeComponent();
        }
        public void fn_prbar_()
        {          
            if (progressBar1.Value == 100)
            {
                t.Enabled = false;
                t.Stop();
                this.Hide();
                l.ShowDialog();
                this.Close();
            }
            progressBar1.Value += 1;
        }
        private void t_Tick(object sender, EventArgs e)
        {
            fn_prbar_();
        }

        private void FrmSplash_Load(object sender, EventArgs e)
        {
            t.Interval = 50;
            t.Tick += new EventHandler(this.t_Tick);
            t.Start();
        }
    }
}
