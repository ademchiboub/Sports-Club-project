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
    public class clsCoachData
    {
        public static DataTable GetCoachesOverview()
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SP_GetCoachesOverview", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    try
                    {
                        conn.Open();
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt); // تعبئة DataTable بالنتيجة
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                        // يمكن التعامل مع الخطأ كما تريد
                    }
                }
            }

            return dt;
        }

        public static bool GetCoachInfoByID(int coachID, ref int personID, ref int specializationID)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_GetCoachInfoByID", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@CoachID", coachID);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read()) // يوجد صف
                    {
                        personID = reader.GetInt32(reader.GetOrdinal("PersonID"));
                        specializationID = reader.GetInt32(reader.GetOrdinal("SpecializationID"));
                        isFound = true;
                    }
                }
            }

            return isFound;
        }

        public static int AddNewCoach(int personID, int specializationID)
        {
            int newCoachID = 0;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_AddCoach", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@PersonID", personID);
                command.Parameters.AddWithValue("@SpecializationID", specializationID);

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        newCoachID = Convert.ToInt32(reader["NewCoachID"]);
                    }
                }
            }

            return newCoachID;
        }

        public static bool UpdateCoach(int coachID, int specializationID)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_UpdateCoach", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@CoachID", coachID);
                command.Parameters.AddWithValue("@SpecializationID", specializationID);

                connection.Open();
                int rowsAffected = Convert.ToInt32(command.ExecuteScalar()); // لأن SP ترجع @@ROWCOUNT
                return rowsAffected > 0;
            }
        }

        public static bool DeleteCoach(int coachID)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_DeleteCoach", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                // إضافة المعامل
                command.Parameters.AddWithValue("@CoachID", coachID);

                connection.Open();

                int rowsAffected = command.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }

        public static bool CoachIDIsExist(int coachID)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_CoachIDIsExist", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@CoachID", coachID);

                connection.Open();

                int exists = Convert.ToInt32(command.ExecuteScalar());

                return exists == 1;
            }
        }

        public static bool IsCoachExist(int personId)
        {
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("IsCoachExist", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PersonID", personId);

                conn.Open();
                int result = Convert.ToInt32(cmd.ExecuteScalar());
                return result == 1;
            }
        }

    }
}
