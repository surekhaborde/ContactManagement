using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ContactManagement.ContactMangement_Classes
{
    internal class ContClass
    {
        public int ContactID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactNo { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }

        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

  
        #region Select Method to select data from database
        public DataTable Select()
        {
            SqlConnection conn=new SqlConnection(myconnstrng);
            conn.Open();
            DataTable dt = new DataTable();
            try
            {
                string sql = "SELECT * FROM Tbl_ContactMangementSystem";
                SqlCommand cmd = new SqlCommand(sql, conn);
             
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();
            return dt;
        }
        #endregion
        #region Insert the values in database
        public bool Insert(ContClass c)
        {
            bool b=false;
            SqlConnection conn = new SqlConnection(myconnstrng);
            conn.Open();
            try
            {
                string sql = "INSERT INTO Tbl_ContactMangementSystem ( FirstName ,LastName ,ContactNo ,Address,Email,Gender ) VALUES ( @FirstName ,@LastName ,@ContactNo ,@Address,@Email,@Gender ) ";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@FirstName", c.FirstName);
                cmd.Parameters.AddWithValue("@LastName", c.LastName);
                cmd.Parameters.AddWithValue("@ContactNo", c.ContactNo);
                cmd.Parameters.AddWithValue("@Address", c.Address);
                cmd.Parameters.AddWithValue("@Email", c.Email);
                cmd.Parameters.AddWithValue("@Gender", c.Gender);
                int rows = cmd.ExecuteNonQuery();
                if(rows > 0)
                {
                    b= true;
                   
                }
                else
                {
                    b = false;
                  
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            conn.Close();
            return b;
        }
        #endregion
        #region Update data in database
        public bool Update(ContClass c)
        {
            bool b=false;
            SqlConnection conn = new SqlConnection(myconnstrng);
            conn.Open();
            try
            {
                string sql = "UPDATE Tbl_ContactMangementSystem SET FirstName=@FirstName , LastName=@LastName, ContactNo=@ContactNo ,Address=@Address , Email=@Email,Gender=@Gender WHERE  ContactID=@ContactID";
                SqlCommand cmd= new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@FirstName", c.FirstName);
                cmd.Parameters.AddWithValue("@LastName", c.LastName);
                cmd.Parameters.AddWithValue("@ContactNo", c.ContactNo);
                cmd.Parameters.AddWithValue("@Address", c.Address);
                cmd.Parameters.AddWithValue("@Email", c.Email);
                cmd.Parameters.AddWithValue("@Gender", c.Gender);
                cmd.Parameters.AddWithValue("ContactID", c.ContactID);
                int rows = cmd.ExecuteNonQuery();
                if(rows > 0)
                {
                    b= true;
                    
                }
                else
                {
                    b = false;
                    
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            conn.Close();
            return b;
        }
        #endregion
        #region Delete data from database
        public bool Delete(ContClass c)
        {
            bool b=false;
            SqlConnection conn = new SqlConnection(myconnstrng);
            conn.Open() ;
            try
            {
                string sql = "DELETE FROM Tbl_ContactMangementSystem WHERE ContactID=@ContactID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ContactID", ContactID);
                int rows= cmd.ExecuteNonQuery();
                if(rows > 0)
                {
                    b = true;
                }
                else
                {
                    b = false;
                }
                
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            conn.Close();
            return b;

        }
        #endregion
       
    }
}
