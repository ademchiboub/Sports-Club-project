using SportsClub_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC_DataAccess
{
    public class clsSpecializationsData
    {
        public static int AddNew(string Name)
        {
            int newID = -1;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_AddSpecialization", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Name", Name);

                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null)
                    newID = Convert.ToInt32(result);
            }

            return newID;
        }

        public static bool Update(int ID, string Name)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_UpdateSpecialization", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@SpecializationID", ID);
                command.Parameters.AddWithValue("@Name", Name);

                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }

        public static bool Delete(int ID)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_DeleteSpecialization", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@SpecializationID", ID);

                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }

        public static DataTable GetAll()
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_GetAllSpecializations", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    dt.Load(reader);
                }
            }

            return dt;
        }

        public static bool GetByID(int ID, ref string Name)
        {
            bool Found = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_GetSpecializationByID", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@SpecializationID", ID);

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Found = true;
                        Name = reader["Name"].ToString();
                    }
                }
            }

            return Found;
        }
    }
}
