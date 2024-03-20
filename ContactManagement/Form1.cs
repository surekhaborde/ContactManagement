using ContactManagement.ContactMangement_Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ContactManagement
{
    public partial class FormContact : Form
    {
        public FormContact()
        {
            InitializeComponent();
        }
        ContClass c = new ContClass();
        private void button2_Click(object sender, EventArgs e)
        {
            //get the data from database
            c.ContactID = int.Parse(txtCID.Text);
            c.FirstName = txtFirstName.Text;
            c.LastName = txtLastName.Text;
            c.ContactNo = txtContactNo.Text;
            c.Address = txtAddress.Text;
            c.Email = txtEmail.Text;
            c.Gender = comboBoxGender.Text;
            //update data in database

            bool b = c.Update(c);
            if (b == true)
            {
                MessageBox.Show("Contact updated successfully");
                DataTable dt = c.Select();
                dataGridViewDetails.DataSource = dt;
                Clear();
            }
            else
            {
                MessageBox.Show("Contact failed to update");
            }

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {


            c.FirstName = txtFirstName.Text;
            c.LastName = txtLastName.Text;
            c.ContactNo = txtContactNo.Text;
            c.Address = txtAddress.Text;
            c.Email = txtEmail.Text;
            c.Gender = comboBoxGender.Text;

            if (ValidateChildren(ValidationConstraints.Enabled))
            {
                
            }

            bool b = c.Insert(c);

            if (b == true)
            {
                MessageBox.Show("Contact details inserted Successfully");
                Clear();
            }
            else
            {
                MessageBox.Show("Failed to update Contact details");
                Clear();
            }
            DataTable dt = c.Select();
            dataGridViewDetails.DataSource = dt;
        }

        private void FormContact_Load(object sender, EventArgs e)
        {
            DataTable dt = c.Select();
            dataGridViewDetails.DataSource = dt;
        }
        public void Clear()
        {
            txtCID.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtContactNo.Text = "";
            txtAddress.Text = "";
            txtSearch.Text = "";
            txtEmail.Text = "";
            comboBoxGender.Text = "";
        }

        private void dataGridViewDetails_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowIndex = e.RowIndex;
            txtCID.Text = dataGridViewDetails.Rows[rowIndex].Cells[0].Value.ToString();
            txtFirstName.Text = dataGridViewDetails.Rows[rowIndex].Cells[1].Value.ToString();
            txtLastName.Text = dataGridViewDetails.Rows[rowIndex].Cells[2].Value.ToString();
            txtContactNo.Text = dataGridViewDetails.Rows[rowIndex].Cells[3].Value.ToString();
            txtAddress.Text = dataGridViewDetails.Rows[rowIndex].Cells[4].Value.ToString();

            txtEmail.Text = dataGridViewDetails.Rows[rowIndex].Cells[5].Value.ToString();
            comboBoxGender.Text = dataGridViewDetails.Rows[rowIndex].Cells[6].Value.ToString();


        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnDELETE_Click(object sender, EventArgs e)
        {
            c.ContactID = Convert.ToInt32(txtCID.Text);
            bool b = c.Delete(c);
            if (b == true)
            {
                MessageBox.Show("Contact deleted Successfully");
                DataTable dt = c.Select();
                dataGridViewDetails.DataSource = dt;
                Clear();

            }
            else
            {
                MessageBox.Show("Failed to delete Contact");

            }
        }
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text;
            SqlConnection con = new SqlConnection(myconnstrng);
            SqlDataAdapter adapter = new SqlDataAdapter("Select * from Tbl_ContactMangementSystem where FirstName Like '%" + keyword + "%' OR LastName LIKE '%" + keyword + "%' ", con);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridViewDetails.DataSource = dt;

        }

        private void txtFirstName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                e.Cancel = true;
                txtFirstName.Focus();
                errorProvider1.SetError(txtFirstName, "Name should not be left blank!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtFirstName, "");
            }
        }
    }
}
