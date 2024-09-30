using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace K_merch_Store_Management_System
{
    internal class DbConnect
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        private string con;


        public string Connection()
        {
            con = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Saadman Sakib Saad\source\repos\K-merch Store Management System\K-merch Store Management System\DbK-Merch.mdf"";Integrated Security=True;AttachDbFilename=C:\Users\Saadman Sakib Saad\source\repos\K-merch Store Management System\K-merch Store Management System\DbK-Merch.mdf;Integrated Security=True; Connect Timeout =30";
            return con;
        }
        public void executeQuerey(string sql)
        {
            try
            {
                cn.ConnectionString = Connection();
                cn.Open();
                cm = new SqlCommand(sql,cn);
                cm.ExecuteNonQuery();

                cn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }
    }
}
