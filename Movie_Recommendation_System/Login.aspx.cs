using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;

// For Password verification
using System.Security.Cryptography;
using System.Text;

namespace Movie_Recommendation_System
{
    public partial class Login : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;

        }

        public static bool VerifyPassword(string enteredPassword, string storedHash, byte[] storedSalt)
        {
            string newHash;

            using (var pbkdf2 = new Rfc2898DeriveBytes(enteredPassword, storedSalt, 10000, HashAlgorithmName.SHA256))
            {
                // Generate 20 byte hash from enteredPassword
                byte[] hash = pbkdf2.GetBytes(20);

                // 36 byte hash value
                byte[] hashBytes = new byte[36];

                //Combine salt and hash
                Array.Copy(storedSalt, 0, hashBytes, 0, 16);
                Array.Copy(hash, 0, hashBytes, 16, 20);

                newHash = Convert.ToBase64String(hashBytes);
            }
            return newHash == storedHash;
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string enteredUsername = txtUsername.Text;
            string enteredPassword = txtPassword.Text;

            string connectionString = WebConfigurationManager.ConnectionStrings["MRS"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Users WHERE Username=@Username";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Username", enteredUsername);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    string storedHash = reader["PasswordHash"].ToString();
                    byte[] storedSalt = (byte[])reader["PasswordSalt"];

                    bool isPasswordCorrect = VerifyPassword(enteredPassword, storedHash, storedSalt);

                    if (isPasswordCorrect)
                    {
                        Response.Redirect("Home.aspx");
                    }
                    else
                    {
                        lblMessage.Text = "Invalid username or password. Please try again.";
                    }
                }
                else
                {
                    lblMessage.Text = "Invalid username or password. Please try again.";
                }
            }
        }

     
    }
}