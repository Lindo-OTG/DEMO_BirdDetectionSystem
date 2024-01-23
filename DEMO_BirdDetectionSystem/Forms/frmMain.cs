using Blaney;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.Windows.Forms;

namespace BirdDetectionSystem
{
    public partial class frmRador : Form
    {
        #region Variable Declaration
        #region Radar Variables
        Radar _radar;
        static int velocity;
        static int radius;
        Random rnd = new Random();
        RadarItem item1 = new TriangleRadarItem(1, 8, 190, 60);
        RadarItem item2 = new TriangleRadarItem(2, 8, 45, 45);
        RadarItem item3 = new TriangleRadarItem(3, 8, 30, 30);
        #endregion
        #region timers
        Timer t = new Timer(); //Radar Timer (Handles Radar fucntions and movements)
        Timer k = new Timer(); //Altitude increase timer (Handles the changes made when plane takes off)
        Timer l = new Timer(); //Altitude decrease timer (Handles the changes made when plane is landing)
        Timer alert = new Timer(); //Altert Timer (Handles Alerts, and events occuring during an alert or emergency)
        Timer sys = new Timer(); //All round system diagnostics timer (Makes sure every aspect of the program runs when required to)
        int interval = 100;
        #endregion
        #endregion
        public frmRador()
        {
            InitializeComponent();
            // internal item update timer. (Initialising Rador Timer)
            t.Interval = 60;
            t.Tick += new EventHandler(t_Tick);
            t.Enabled = true;
        }
        private void FrmRador_Load(object sender, EventArgs e)
        {
            //Radar Timer Properties and parameters
            _radar = new Radar(pictureBox4.Width);
            _radar.ImageUpdate += new ImageUpdateHandler(_radar_ImageUpdate);
            _radar.DrawScanInterval = 60;
            _radar.DrawScanLine = true;
            //Cast trackbar value to correponding ;abels
            lblSound.Text = tkbSound.Value.ToString() + "%";
            lblLight.Text = tkbLight.Value.ToString() + "%";
            //System Timer Initialisation
            sys.Interval = 60;
            sys.Tick += new EventHandler(this.sys_Tick);
            sys.Start();
            //mock start
            alert.Interval = 60;
            alert.Tick += new EventHandler(this.alert_Tick);
            alert.Start();
        }
        int GetDelta(int min, int max)
        {
            int i = rnd.Next(min, max); //sets the range by which the motion variabes can flactuate
            if (i == 0)
                i--;
            return i;
        }//Calculate the random flactuation in the motion virables of the the radar objects
        private void BtnTakeoff_Click_2(object sender, EventArgs e)
        {
            l.Stop(); //stops the landing timer
            k.Interval = 60; //inititates the takeoff timer
            k.Tick += new EventHandler(this.k_Tick);
            k.Start();
        }//button land the plane and activate system
        private void BtnLanding_Click_2(object sender, EventArgs e)
        {
            k.Stop();
            l.Interval = 60; //speed of the of the plane landing
            l.Tick += new EventHandler(this.l_Tick); //linking the timer to its corresponding tick event
            l.Start();
        }//button to activate the system and increase altitude       
        private void TkbLight_Scroll_1(object sender, Zeroit.Framework.Metro.ZeroitMetroTrackbar.TrackbarEventArgs e)
        {
            lblLight.Text = tkbLight.Value.ToString() + "%"; //Link scroll bar value to label
        } //Light scrollbar event
        private void TkbSound_Scroll_1(object sender, Zeroit.Framework.Metro.ZeroitMetroTrackbar.TrackbarEventArgs e)
        {
            lblSound.Text = tkbSound.Value.ToString() + "%"; //Link scroll bar value to label
        }// sound scrollbar event
        void _radar_ImageUpdate(object sender, ImageUpdateEventArgs e)
        {
            // this event is important to catch!
            pictureBox4.Image = e.Image; //It use a preinstalled library to update radar picture box
        }      
        #region Timer EventHandlers
        private void alert_Tick(object sender, EventArgs e)
        {
            if (bckBack.BackColor != Color.Red)
            {
                bckBack.BackColor = Color.Red;
            }
            else
            {
                bckBack.BackColor = Color.White;
            }
        } //Alert Timer Event
        void t_Tick(object sender, EventArgs e)
        {
            // select which of the three items to update
            int i = rnd.Next(1, 4);
            switch (i)
            {
                case 1:
                    item1.Azimuth += GetDelta(0,2);
                    item1.Elevation += GetDelta(0,2);
                    _radar.AddItem(item1);
                    break;
                case 2:
                    item2.Azimuth += GetDelta(0,2);
                    item2.Elevation += GetDelta(0,2);
                    _radar.AddItem(item2);
                    break;
                case 3:
                    item3.Azimuth += GetDelta(0,2);
                    item3.Elevation += GetDelta(0,2);
                    _radar.AddItem(item3);
                    break;
            }
        }//radar timer tick event
        private void k_Tick(object sender, EventArgs e)
        {
            if (trkHeight.Value > 0) //as long as the altitude is below 2000m the plane will keep rising
            {
                trkHeight.Value -= 1; // increament the height of the plane at the rate of 1
            }
            velocity = (200 - trkHeight.Value + 15);
            radius = (200 - trkHeight.Value + 150);
            if (trkHeight.Value == 0)
            {
                velocity = 279;
                radius = 350;
            }
            prgRadius.Value = radius * 100 / 350; ;
            txtalt.Text = "Altitude : " + (200 - trkHeight.Value).ToString() + "0m" + "\tVelocity : " + velocity + "km/h" + "\t   Rador Radius : " + radius + "km";
        } //altitude increase timer event
        private void l_Tick(object sender, EventArgs e)
        {
            bool standby = false;
            if (trkHeight.Value < 200)
            {
                trkHeight.Value += 1;
            }
            velocity = (200 - trkHeight.Value + 15);
            radius = (200 - trkHeight.Value + 150);
            if (trkHeight.Value == 200)
            {
                velocity = 0;
                radius = 0;
                standby = true;
            }
            txtalt.Text = "Altitude : " + (200 - trkHeight.Value).ToString() + "0m" + "\tVelocity : " + velocity + "km/h" + "\t   Rador Radius : " + radius + "km";
            prgRadius.Value = radius * 100 / 350;
            if (standby)
            {
                txtalt.Text = "\t\t      SYSTEM ON STANDBY";
            }
        } //altitude decrease timer event
        void sys_Tick(object sender, EventArgs e) //Event Handler for system timer, to handle tick events
        {
            int val = GetDelta(2,6);
            int opacity = 105;
            interval = 999 - tkbLight.Value * 10;
            if (swtSystem.Checked) //if the system is on (ACTIVE)
            {
                tkbLight.Enabled = true;
                tkbSound.Enabled = true;
                alert.Interval = interval;
                alert.Tick += new EventHandler(this.alert_Tick);
                alert.Start();
                prgOpacity.Value = 15;
            }
            else if (!swtSystem.Checked) //if the system is off (STANDBY)
            {
                tkbLight.Enabled = true;
                tkbSound.Enabled = true;
                alert.Stop();
                bckBack.BackColor = Color.White;
                prgOpacity.Value = ((-tkbLight.Value) - trkHeight.Value / 4) + opacity - val;
            }
            if (trkHeight.Value == 200 || trkHeight.Value == 0)
            {
                val = 0;
            }
            if (trkHeight.Value == 0)
            {
                opacity = 87;
            }
            else if(prgOpacity.Value <0)
            {
                prgOpacity.Value = 3;
            }           
            prgSonic.Value = ((trkHeight.Value / 2) + tkbSound.Value) / 2 + 18 - val;
        }
        #endregion
        #region Dudd EventHandlers
        private void SwtSystem_CheckedChanged(object sender, EventArgs e)
        {

        }
        #endregion
    }
}
