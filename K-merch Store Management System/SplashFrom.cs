using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace K_merch_Store_Management_System
{
    public partial class SplashFrom : Form
    {
        public SplashFrom()
        {
            InitializeComponent();
        }

        int startPoint = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {
            startPoint += 2;
            guna2ProgressBar1.Value = startPoint;
            if (guna2ProgressBar1.Value == 100)
            {
                guna2ProgressBar1.Value = 0;
                timer1.Stop();
                LoginFrom login = new LoginFrom();
                login.ShowDialog();
                this.Hide();
            }
        }

        private void SplashFrom_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }
    }
}
