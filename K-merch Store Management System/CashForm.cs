﻿using System;
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
    public partial class CashForm : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DbConnect dbcon = new DbConnect();
        SqlDataReader dr;
        String title = "K-pop Merch Store Management";
        MainFrom main;

        public CashForm(MainFrom from)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.Connection());
            main = from;
            getTransno();
            loadCash();
        }

        //btnAdd......
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            CashProduct product = new CashProduct(this);
            product.uname = main.lblUsername.Text;
            product.ShowDialog();

        }

        private void btnCash_Click(object sender, EventArgs e)
        {
            CashCustomer customer = new CashCustomer(this);
            customer.ShowDialog();


            if(MessageBox.Show("Are you sure want to cash this product?", "Cashing", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                getTransno();
                main.loadDailySale();
                for(int i=0; i < dgvCash.Rows.Count; i++)
                {
                    dbcon.executeQuerey("UPDATE tbProduct SET pqty=pqty -" + int.Parse(dgvCash.Rows[i].Cells[4].Value.ToString()) + " WHERE pcode LIKE " + dgvCash.Rows[i].Cells[2].Value.ToString() + " ");
                        
                }
                dgvCash.Rows.Clear();
            }
        }
        private void dgvCash_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            string colName = dgvCash.Columns[e.ColumnIndex].Name;
            removeitem:
            if (colName == "Delete")
            {
               
                    if (MessageBox.Show("Are you sure want to delete this cash?", "Delete Cash?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        dbcon.executeQuerey("DELETE FROM tbCash WHERE cashid LIKE'" + dgvCash.Rows[e.RowIndex].Cells[1].Value.ToString() + "'");

                        MessageBox.Show("Cash record has been successfully removed", title, MessageBoxButtons.OK, MessageBoxIcon.Question);

                    }
                   

            }
            else if (colName == "Increase")
            {
                int i = checkPqty(dgvCash.Rows[e.RowIndex].Cells[2].Value.ToString());
                if (int.Parse (dgvCash.Rows[e.RowIndex].Cells[4].Value.ToString()) < i)
                {
                    dbcon.executeQuerey("UPDATE tbCash SET qty = qty + " + 1 + " WHERE cashid LIKE '" + dgvCash.Rows[e.RowIndex].Cells[1].Value.ToString() + "'");
                }
                else
                {
                    MessageBox.Show("Remaining quantity in hand is " + i + "!", "Out of stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                 
            }
            else if (colName == "Decrease")
            {
                if (int.Parse(dgvCash.Rows[e.RowIndex].Cells[4].Value.ToString()) == 1)
                {
                    colName = "Delete";
                    goto removeitem;
                }
                dbcon.executeQuerey("UPDATE tbCash SET qty = qty - " + 1 + " WHERE cashid LIKE '" + dgvCash.Rows[e.RowIndex].Cells[1].Value.ToString() + "'");
            }
            loadCash();

        }

        #region method
        public void getTransno()
        {
            try
            {
                string sdate = DateTime.Now.ToString("yyyyMMdd");
                int count;
                string transno;

                cn.Open();
                cm = new SqlCommand("SELECT TOP 1 transno FROM tbCash WHERE transno LIKE '" + sdate + "%'  ORDER BY cashid DESC", cn);
                dr = cm.ExecuteReader();
                dr.Read();


                if (dr.HasRows)
                {
                    transno = dr[0].ToString();
                    count = int.Parse(transno.Substring(8, 4));
                    lblTransno.Text = sdate + (count + 1);
                }
                else
                {
                    transno = sdate + "1001";
                    lblTransno.Text = transno;
                }
                dr.Close();
                cn.Close();

            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, title);

            }
        }

        public void loadCash()
        {
            try
            {

                int i = 0;
                double total = 0;
                dgvCash.Rows.Clear();
                cm = new SqlCommand("SELECT cashid,pcode,pname,qty,price,total,c.name,cashier FROM tbCash as cash LEFT JOIN tbCustomer c ON cash.cid = c.id WHERE transno LIKE "+lblTransno.Text+"", cn);
                cn.Open();
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    i++;
                    dgvCash.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString(), dr[7].ToString());
                    total += double.Parse(dr[5].ToString());
                }
                dr.Close();
                cn.Close();
                lblTotal.Text = total.ToString("#,##0.00");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, title);
            }
        }

        public int checkPqty(string pcode)
        {
            int i = 0;
            try
            {
                cn.Open();
                cm = new SqlCommand("SELECT pqty FROM tbProduct WHERE pcode LIKE '" + pcode + "'", cn);
                i = int.Parse(cm.ExecuteScalar().ToString());
                cn.Close();

            }
            catch(Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, title);
            }
            return i;
        }

        #endregion method

       
    }
}
