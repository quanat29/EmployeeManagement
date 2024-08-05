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
    public partial class EmployeeSalary : Form
    {
        public EmployeeSalary()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-0ICTB8Q\TRINHANHQUAN;Initial Catalog=EmployeeManagementDb;Integrated Security=True");
        private void fetchData()
        {
            if (tbId.Text == "")
            {
                MessageBox.Show("Please enter the ID");
            }
            else
            {
                try
                {
                    con.Open();
                    string query = "select * from tblEmployee where Id = '" + tbId.Text + "'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    DataTable dt = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    foreach (DataRow dr in dt.Rows)
                    {
                        tbName.Text = dr["Name"].ToString();
                        tbPosition.Text = dr["Position"].ToString();
                    }
                    con.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void viewData()
        {
            int baseSalary, totalSalary;

            if (tbPosition.Text == "")
            {
                MessageBox.Show("Position information not available");

            } else if (tbWorkday.Text == "" || Convert.ToInt32(tbWorkday.Text) > 30 || Convert.ToInt32(tbWorkday.Text) < 0)
            {
                MessageBox.Show("Please enter valid Work Day");

            }
            else
            {
                if (tbPosition.Text == "Database Developer")
                {
                    baseSalary = 400000;
                }
                else if (tbPosition.Text == "Front-End Developer")
                {
                    baseSalary = 450000;
                }
                else if (tbPosition.Text == "Back-End Developer")
                {
                    baseSalary = 500000;
                }
                else
                {
                    baseSalary = 350000;
                }
                totalSalary = baseSalary * Convert.ToInt32(tbWorkday.Text);
                rtbSalary.Text = "ID: " + tbId.Text + "\n" + "Name: " + tbName.Text + "\n" + 
                    "Position: " + tbPosition.Text + "\n" + "Work Day: "+ tbWorkday.Text + "\n" + "Total Salary: " + totalSalary + " vnđ";
            }
        }
        private void ptbClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Hide();
        }

        private void btnFetch_Click(object sender, EventArgs e)
        {
            fetchData();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            viewData();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if(printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("===========EMPLOYEE SUMMARY============",
                new Font("Time New Roman", 20, FontStyle.Regular), Brushes.Red, new Point(100));

            e.Graphics.DrawString(rtbSalary.Text,
                new Font("Time New Roman", 20, FontStyle.Regular), Brushes.DarkGreen, new Point(20,100));
        }
    }
}
