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

namespace EmployeeManagement
{
    public partial class Employee : Form
    {
        public Employee()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-0ICTB8Q\TRINHANHQUAN;Initial Catalog=EmployeeManagementDb;Integrated Security=True");

        private void populate()
        {
            con.Open();
            string query = "select * from tblEmployee";
            SqlDataAdapter sqlDataAdapter= new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sqlDataAdapter);
            var listData = new DataSet();
            sqlDataAdapter.Fill(listData);
            dgvData.DataSource = listData.Tables[0];
            con.Close();
        }
        private void Employee_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void ptbClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if(tbId.Text == "" || tbName.Text == "" || tbPhone.Text == "" || tbAddress.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    con.Open();
                    string query = "insert into tblEmployee values('" + tbId.Text + "','" + tbName.Text + "','" + tbAddress.Text + "','" + tbPhone.Text + "','"+ cbbGender.SelectedItem.ToString() + "','" + cbbPosition.SelectedItem.ToString() + "','" + cbbEducation.SelectedItem.ToString() +"','" + dtpBirthday.Value.Date + "')";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Employee successfully added");
                    con.Close();
                    populate();
                    clearData();
                }
                catch(Exception ex) 
                {
                    MessageBox.Show(ex.Message);
                }
             
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(tbId.Text == "")
            {
                MessageBox.Show("Enter The Employee ID");
            }else
            {
                try
                {
                    con.Open();
                    string query = "delete from tblEmployee where Id = '" + tbId.Text + "'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Delete Successfully");
                    con.Close();
                    populate();
                    clearData();
                }catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }    
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            tbId.Text = dgvData.SelectedRows[0].Cells[0].Value.ToString();
            tbName.Text = dgvData.SelectedRows[0].Cells[1].Value.ToString();
            tbAddress.Text = dgvData.SelectedRows[0].Cells[2].Value.ToString();
            tbPhone.Text = dgvData.SelectedRows[0].Cells[3].Value.ToString();
            cbbGender.Text = dgvData.SelectedRows[0].Cells[4].Value.ToString();
            cbbPosition.Text = dgvData.SelectedRows[0].Cells[5].Value.ToString();
            cbbEducation.Text = dgvData.SelectedRows[0].Cells[6].Value.ToString();
            dtpBirthday.Text = dgvData.SelectedRows[0].Cells[7].Value.ToString();
        }
        private void clearData()
        {
            tbId.Text = "";
            tbName.Text = "";
            tbAddress.Text = "";
            tbPhone.Text = "";
            cbbGender.Text = "";
            cbbPosition.Text = "";
            cbbEducation.Text = "";
            dtpBirthday.Text = "";
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (tbId.Text == "" || tbName.Text == "" || tbPhone.Text == "" || tbAddress.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    con.Open();
                    string query = "update tblEmployee set Name = '" + tbName.Text + "',Address = '" + 
                        tbAddress.Text + "',Phone = '" + tbPhone.Text + "',Gender = '"+cbbGender.SelectedItem.ToString()+"',Position = '"+
                        cbbPosition.SelectedItem.ToString()+"',Education = '"+cbbEducation.SelectedItem.ToString()+"',Birthday = '"+
                        dtpBirthday.Value.Date+"' where Id = '"+tbId.Text+"'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Employee successfully edited");
                    con.Close();
                    populate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Hide();
        }
    }
}
