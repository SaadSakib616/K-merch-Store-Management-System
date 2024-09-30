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
    public partial class Dashboard : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DbConnect dbcon = new DbConnect();
        SqlDataReader dr;
        String title = "K-pop Merch Store Management";
        public Dashboard()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.Connection());
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            lblBTS.Text = extractData("BTS").ToString();
            lblBlackpink.Text = extractData("BLACKPINK").ToString();
            lblTwice.Text = extractData("TWICE").ToString();
            lblSeventeen.Text = extractData("SEVENTEEN").ToString();
        }

        #region method

        public int extractData(string str)
        {
            int data = 0;
            try
            {
                cn.Open();
                cm = new SqlCommand("SELECT ISNULL(SUM(pqty),0) AS pqty FROM tbProduct WHERE pcategory = '"+ str +"'", cn);
                
                 data = int.Parse(cm.ExecuteScalar().ToString());
                cn.Close();

            }
            catch(Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, title);
            }
            return data;
        }
        #endregion method

       
    }
}
