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
    public partial class Information : Form
    {
        public Information()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-0ICTB8Q\TRINHANHQUAN;Initial Catalog=EmployeeManagementDb;Integrated Security=True");
        private void fetchData()
        {
            if(tbId.Text == "")
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
                        lblEml_ID.Text = dr["Id"].ToString();
                        lblEml_Name.Text = dr["Name"].ToString();
                        lblEml_Phone.Text = dr["Phone"].ToString();
                        lblEml_Address.Text = dr["Address"].ToString();
                        lblEml_Gender.Text = dr["Gender"].ToString();
                        lblEml_Position.Text = dr["Position"].ToString();
                        lblEml_Education.Text = dr["Education"].ToString();
                        lblEml_Birthday.Text = dr["Birthday"].ToString();
                    }
                    con.Close();

                }catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
           
        }
        private void clearData()
        {
            tbId.Text = "";
            lblEml_ID.Text = "";
            lblEml_Name.Text = "";
            lblEml_Phone.Text = "";
            lblEml_Address.Text = "";
            lblEml_Gender.Text = "";
            lblEml_Position.Text = "";
            lblEml_Education.Text = "";
            lblEml_Birthday.Text = "";
        }

        private void ptbClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            fetchData();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
           clearData();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Hide();
        }
    }
}
