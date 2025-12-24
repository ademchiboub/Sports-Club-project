using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SportsClub_DataAccess
{
    public class clsPersonData
    {


        static public int AddNewPerson(string firstName, string secondName, string thirdName, string lastName,
         DateTime DateOfBirth, string gendor, string phone, string email, string address, string ImagePath)
        {
            int newPersonID = 0;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_AddNewPerson", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // إضافة البراميترز
                    command.Parameters.AddWithValue("@FirstName", firstName);
                    command.Parameters.AddWithValue("@SecondName", secondName);
                    command.Parameters.AddWithValue("@ThirdName", thirdName ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@LastName", lastName ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
                    command.Parameters.AddWithValue("@Gendor", gendor);
                    command.Parameters.AddWithValue("@Phone", phone);
                    command.Parameters.AddWithValue("@Email", email ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Address", address);
                    command.Parameters.AddWithValue("@ImagePath", ImagePath ?? (object)DBNull.Value);

                    // Output parameter
                    SqlParameter outputIdParam = new SqlParameter("@NewPersonID", SqlDbType.Int);
                    outputIdParam.Direction = ParameterDirection.Output;
                    command.Parameters.Add(outputIdParam);

                    // تنفيذ SP
                    connection.Open();
                    command.ExecuteNonQuery();

                    // استرجاع القيمة
                    newPersonID = Convert.ToInt32(command.Parameters["@NewPersonID"].Value);
                }
            }

            return newPersonID;
        }

        static public bool UpdatePerson(int? personID, string firstName, string secondName, string thirdName,
        string lastName, DateTime DateOfBirth, string gendor,  string phone, string email, string address, string ImagePath)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_UpdatePerson", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // حقول إلزامية (لا يمكن أن تكون null)
                    command.Parameters.AddWithValue("@PersonID", personID);
                    command.Parameters.AddWithValue("@FirstName", firstName);
                    command.Parameters.AddWithValue("@SecondName", secondName);
                    command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
                    command.Parameters.AddWithValue("@Gendor", gendor);
                    command.Parameters.AddWithValue("@Phone", phone);
                    command.Parameters.AddWithValue("@Address", address);

                    // حقول اختيارية يمكن أن تكون null
                    command.Parameters.AddWithValue("@ThirdName", thirdName ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@LastName", lastName ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Email", email ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@ImagePath", ImagePath ?? (object)DBNull.Value);

                    connection.Open();

                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
        }

        // 3. DeletePerson
        static public bool DeletePerson(int personID)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_DeletePerson", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@PersonID", personID);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }


        // 1. FindByPersonID
        static public bool FindPersonByID(int PersonID, ref string FirstName, ref string SecondName, ref string ThirdName,
    ref string LastName, ref DateTime DateOfBirth, ref string Gendor, ref string Phone, ref string Email, ref string Address,
    ref string ImagePath)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_FindPersonByID", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@PersonID", PersonID);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    // ❗ مهم جداً
                    if (!reader.Read())
                    {
                        return false; // لا يوجد صف => false
                    }

                    // الآن نستطيع قراءة البيانات

                    FirstName = reader.GetString(reader.GetOrdinal("FirstName"));
                    SecondName = reader.GetString(reader.GetOrdinal("SecondName"));
                    ThirdName = reader.IsDBNull(reader.GetOrdinal("ThirdName")) ? "" : reader.GetString(reader.GetOrdinal("ThirdName"));
                    LastName = reader.IsDBNull(reader.GetOrdinal("LastName")) ? "" : reader.GetString(reader.GetOrdinal("LastName"));

                    DateOfBirth = reader.GetDateTime(reader.GetOrdinal("DateOfBirth"));

                    Gendor = reader.GetString(reader.GetOrdinal("Gendor"));
                    Phone = reader.GetString(reader.GetOrdinal("Phone"));
                    Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? "" : reader.GetString(reader.GetOrdinal("Email"));
                    Address = reader.GetString(reader.GetOrdinal("Address"));
                    ImagePath = reader.IsDBNull(reader.GetOrdinal("ImagePath")) ? "" : reader.GetString(reader.GetOrdinal("ImagePath"));

                    return true;
                }
            }
        }

        // 2. GetPeople
        static public DataTable GetPeople()
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_GetPeople", connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                command.CommandType = CommandType.StoredProcedure;
                adapter.Fill(dt);
            }

            return dt;
        }

       

        // 4. IsExist
        static public bool IsExist(int personID)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_IsExist", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@PersonID", personID);

                connection.Open();
                int exists = Convert.ToInt32(command.ExecuteScalar());
                return exists == 1;
            }
        }
        //----------------------------------------------------------------------------------
        public static bool IsPersonExistFlexible(
    string firstName,
    string secondName,
    string thirdName = null,
    string lastName = null)
        {
            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(secondName))
                throw new ArgumentException("FirstName and SecondName cannot be null or empty.");

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("IsPersonExist_Flexible", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@FirstName", firstName);
                cmd.Parameters.AddWithValue("@SecondName", secondName);
                cmd.Parameters.AddWithValue("@ThirdName", string.IsNullOrWhiteSpace(thirdName) ? DBNull.Value : (object)thirdName);
                cmd.Parameters.AddWithValue("@LastName", string.IsNullOrWhiteSpace(lastName) ? DBNull.Value : (object)lastName);

                conn.Open();
                int result = Convert.ToInt32(cmd.ExecuteScalar());
                return result == 1;
            }
        }

    }
}





