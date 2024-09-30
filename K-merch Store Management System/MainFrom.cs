using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace K_merch_Store_Management_System
{
    public partial class MainFrom : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DbConnect dbcon = new DbConnect();
        SqlDataReader dr;
        String title = "K-pop Merch Store Management";
        public MainFrom()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.Connection());
            btnDashboard.PerformClick();
            loadDailySale();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Exit Application?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }

        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            openChildFrom(new Dashboard());
        }

        private void btnCustomers_Click(object sender, EventArgs e)
        {
            openChildFrom(new CustomerForm());
        }

        private void btnUser_Click(object sender, EventArgs e)
        {
            openChildFrom(new UserForm());
        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            openChildFrom(new ProductForm());
        }

        private void btnCash_Click(object sender, EventArgs e)
        {
            openChildFrom(new CashForm(this));
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Logout Application?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                LoginFrom login = new LoginFrom();
                this.Dispose();
                login.ShowDialog();
            }
           
        }

        #region method

        private Form activeForm = null;
        public void openChildFrom(Form childFrom)
        {
            if(activeForm != null)
            
                activeForm.Close();
                activeForm = childFrom;
                childFrom.TopLevel = false;
                childFrom.FormBorderStyle = FormBorderStyle.None;
                childFrom.Dock = DockStyle.Fill;
                lblTitle.Text = childFrom.Text;
                panelChild.Controls.Add(childFrom);
                panelChild.Tag = childFrom;
                childFrom.BringToFront();
                childFrom.Show();

            
        }
        public void loadDailySale()
        {
            string sdate = DateTime.Now.ToString("yyyyMMdd");
           
            try
            {
                cn.Open();
                cm = new SqlCommand("SELECT ISNULL(SUM(total),0) AS total FROM tbCash WHERE transno LIKE '" + sdate + "%'", cn);

                lblDailysale.Text = double.Parse(cm.ExecuteScalar().ToString()).ToString("#,##0.00");
                cn.Close();

            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message);
            }

        }

        #endregion method
    }

}
