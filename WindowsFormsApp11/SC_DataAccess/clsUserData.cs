using SportsClub_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SC_DataAccess
{
    public class clsUserData
    {
        public static bool GetUserInfoByID(int userID, ref int personID, ref string username, ref int role)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_GetUserInfoByID", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserID", userID);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read()) // يوجد صف
                    {
                        personID = reader.GetInt32(reader.GetOrdinal("PersonID"));
                        username = reader.GetString(reader.GetOrdinal("Username"));
                        role = reader.GetInt32(reader.GetOrdinal("Role"));
                        isFound = true;
                    }
                }
            }

            return isFound;
        }

        public static string GetPasswordHashByUsername(string username)
        {
            string storedHash = "";

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("SP_GetUserPasswordHash", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Username", username);

                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        storedHash = reader.GetString(reader.GetOrdinal("Password"));
                    }
                }
            }

            return storedHash;
        }

        public static string HashPassword(string password)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }

        public static int AddNewUser(int personID, string username, string password, int role)
        {
            int newUserID = 0;

            // أولاً نحول كلمة السر إلى Hash
            string hashedPassword = HashPassword(password);

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_AddUser_New", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@PersonID", personID);
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", hashedPassword);
                command.Parameters.AddWithValue("@Role", role);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        newUserID = Convert.ToInt32(reader["NewUserID"]);
                    }
                }
            }

            return newUserID;
        }

        public static bool UpdateUser(int userID, string username, string password, int role)
        {
            bool isUpdated = false;

            // توليد Hash لكلمة المرور
            string hashedPassword = HashPassword(password);

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("dbo.SP_UpdateUser", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserID", userID);
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", hashedPassword);
                command.Parameters.AddWithValue("@Role", role);

                connection.Open();

                int rowsAffected = (int)command.ExecuteScalar();
                isUpdated = rowsAffected > 0;
            }

            return isUpdated;
        }

        public static DataTable GetAllUsers()
        {
            DataTable dtUsers = new DataTable();

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_GetAllUsers", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(dtUsers);
                }
            }

            return dtUsers;
        }

        public static bool IsUserExistForPersonID(int personID)
        {
            bool isExist = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_IsUserExistForPersonID", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@PersonID", personID);

                connection.Open();

                var result = command.ExecuteScalar();

                if (result != null && Convert.ToInt32(result) == 1)
                {
                    isExist = true;
                }
            }

            return isExist;
        }

        public static bool IsUserExist(string username)
        {
            bool isExist = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_IsUserExist", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Username", username);

                connection.Open();

                var result = command.ExecuteScalar();

                if (result != null && Convert.ToInt32(result) == 1)
                {
                    isExist = true;
                }
            }

            return isExist;
        }

        public static bool DeleteUser(int userID)
        {
            bool isDeleted = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_DeleteUser", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserID", userID);

                connection.Open();

                var result = command.ExecuteScalar();

                if (result != null && Convert.ToInt32(result) == 1)
                {
                    isDeleted = true;
                }
            }

            return isDeleted;
        }

        public static bool GetUserInfoByUsernameAndPassword(string username, string password, ref int userID, ref int personID, ref int role)
        {
            bool isLogged = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_LoginUser", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", password);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        isLogged = true;
                        userID   = reader.GetInt32(reader.GetOrdinal("UserID"));
                        personID = reader.GetInt32(reader.GetOrdinal("PersonID"));
                        role     = reader.GetInt32(reader.GetOrdinal("Role"));
                     
                    }
                }
            }

            return isLogged;
        }
    }
}
