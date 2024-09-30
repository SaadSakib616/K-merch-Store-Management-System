using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;



namespace K_merch_Store_Management_System
{
    public partial class UserModule : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DbConnect dbcon = new DbConnect();
        String title = "K-pop Merch Store Management";


        bool check = false;
        UserForm userForm;

        public UserModule(UserForm user)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.Connection());
            userForm = user;
            cbRole.SelectedIndex = 1;


        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try{
                CheckField();
                if (check)
                {



                    if (MessageBox.Show("Are you sure want to register this user?", "User Registration", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cm = new SqlCommand("INSERT INTO tbUser(name,address,phone,role,dob,password)VALUES(@name,@address,@phone,@role,@dob,@password)", cn);
                        cm.Parameters.AddWithValue("@name", txtName.Text);
                        cm.Parameters.AddWithValue("@address", txtAddress.Text);
                        cm.Parameters.AddWithValue("phone", txtPhone.Text);
                        cm.Parameters.AddWithValue("@role", cbRole.Text);
                        cm.Parameters.AddWithValue("@dob", dtDob.Value);
                        cm.Parameters.AddWithValue("@password", txtpass.Text);


                        cn.Open();
                        cm.ExecuteNonQuery();
                        cn.Close();
                        MessageBox.Show("User has been successfully registered!", title);
                        Clear();
                        userForm.LoadUser();

                    }



                }

            }
            catch(Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, title);
            }

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                CheckField();
                if (check)
                {



                    if (MessageBox.Show("Are you sure want to update this record?", "Edit Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cm = new SqlCommand("UPDATE  tbUser SET name=@name,address=@address,phone=@phone,role=@role,dob=@dob,password=@password WHERE id=@id", cn);
                        cm.Parameters.AddWithValue("@id", lbluid.Text);
                        cm.Parameters.AddWithValue("@name", txtName.Text);
                        cm.Parameters.AddWithValue("@address", txtAddress.Text);
                        cm.Parameters.AddWithValue("phone", txtPhone.Text);
                        cm.Parameters.AddWithValue("@role", cbRole.Text);
                        cm.Parameters.AddWithValue("@dob", dtDob.Value);
                        cm.Parameters.AddWithValue("@password", txtpass.Text);


                        cn.Open();
                        cm.ExecuteNonQuery();
                        cn.Close();
                        MessageBox.Show("User's data has been successfully updated!", title);
                        Clear();
                        userForm.LoadUser();
                        this.Dispose();

                    }



                }

            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, title);
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void cbRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbRole.Text == "Employee")
            {
                this.Height = 674 - 28;
                lblPass.Visible = false;
                txtpass.Visible = false;

            }
            else
            {
               

                lblPass.Visible = true;
                txtpass.Visible = true;
                this.Height = 674;
            }
        }

        #region method

        public void Clear()
        {
            txtName.Clear();
            txtAddress.Clear();
            txtPhone.Clear();
            txtpass.Clear();
            cbRole.SelectedIndex = 0;
            dtDob.Value = DateTime.Now;

            btnUpdate.Enabled = false;

        }

        //to check field and date of birth
        public void CheckField()
        {
            if(txtName.Text== "" | txtAddress.Text == ""){

                MessageBox.Show("Requried data field!", "warning");
                return;
            }

            if (CheckAGe(dtDob.Value) < 18)
            {
                MessageBox.Show("User is child Worker! . under 18 year ", "warning");
                return;
            }
            check= true;
               
                
        }

        //to calculate age for under 18
       
        private static int CheckAGe(DateTime dateofbirth)
        {
            int age = DateTime.Now.Year - dateofbirth.Year;
            if (DateTime.Now.DayOfYear < dateofbirth.DayOfYear)
            {
                age = age - 1;
            }
            return age;

        }






        #endregion method

       
    }
}
