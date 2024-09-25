using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Movie_Recommendation_System
{
    public partial class Register : System.Web.UI.Page
    {
        private string connectionString = WebConfigurationManager.ConnectionStrings["MRS"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;

        }

        // Check username
        private bool UserExists(string username)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Users WHERE Username = @Username";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Username", username);

                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    // Check if there is at least one record
                    if (reader.Read())
                    {
                        int count = reader.GetInt32(0); 
                        return count > 0;
                    }
                }
            }

            return false;
        }

        // Check email
        private bool EmailExists(string email)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Users WHERE Email = @Email";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Email", email);

                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    // Check if there is at least one record
                    if (reader.Read())
                    {
                        int count = reader.GetInt32(0); 
                        return count > 0;
                    }
                }
            }

            return false;
        }

        // Save to db
        private bool SaveUserToDatabase(string username, string email, string hashedPassword, byte[] salt)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Users (Username, Email, PasswordHash, PasswordSalt) VALUES (@Username, @Email, @PasswordHash, @PasswordSalt)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@PasswordHash", hashedPassword);
                cmd.Parameters.AddWithValue("@PasswordSalt", salt);
                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        private string HashPassword(string password, byte[] salt)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256))
            {
                byte[] hash = pbkdf2.GetBytes(20);
                byte[] hashBytes = new byte[36];
                Array.Copy(salt, 0, hashBytes, 0, 16);
                Array.Copy(hash, 0, hashBytes, 16, 20);
                return Convert.ToBase64String(hashBytes);
            }
        }

        private byte[] GenerateSalt()
        {
            var salt = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {

                rng.GetBytes(salt);
            }
            return salt;
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string enteredUsername = txtUsername.Text;
            string enteredPassword = txtPassword.Text;
            string enteredEmail = txtEmail.Text;
            string enteredConfirmPassword = txtConfirmPassword.Text;

            if (UserExists(enteredUsername))
            {
                lblMessage.Text = "Username already exists.";
                return;
            }

            if (EmailExists(enteredEmail))
            {
                lblMessage.Text = "User With this email already exists.";
                return;
            }

            byte[] salt = GenerateSalt();
            string hashedPassword = HashPassword(enteredPassword, salt);

            bool success = SaveUserToDatabase(enteredUsername, enteredEmail, hashedPassword, salt);

            if (success)
            {
                lblMessage.Text = "Registration successful!";
            }
            else
            {
                lblMessage.Text = "An error occurred. Please try again.";
            }
        }
    }
}