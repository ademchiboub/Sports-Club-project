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
    public class clsWorkoutsData
    {
        public static DataTable GetWorkoutsOverview()
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SP_GetWorkoutsOverview", conn))
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

        public static int AddNewWorkout(int coachID, string name, string description)
        {
            int newWorkoutID = 0;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_AddNewWorkout", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@CoachID", coachID);
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@Description", description);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        newWorkoutID = Convert.ToInt32(reader["NewWorkoutID"]);
                    }
                }
            }

            return newWorkoutID;
        }

        public static bool UpdateWorkout(int workoutID, int coachID, string name, string description)
        {
            bool isUpdated = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_UpdateWorkout", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@WorkoutID", workoutID);
                command.Parameters.AddWithValue("@CoachID", coachID);
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@Description", description);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                isUpdated = rowsAffected > 0;
            }

            return isUpdated;
        }

        public static bool GetWorkoutInfoByID(int workoutID, ref int coachID, ref string name, ref string description)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_GetWorkoutInfoByID", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@WorkoutID", workoutID);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read()) // إذا وجد صف
                    {
                        coachID = reader.GetInt32(reader.GetOrdinal("CoachID"));
                        name = reader.GetString(reader.GetOrdinal("Name"));
                        description = reader.IsDBNull(reader.GetOrdinal("Description"))
                            ? ""
                            : reader.GetString(reader.GetOrdinal("Description"));

                        isFound = true;
                    }
                }
            }

            return isFound;
        }

        public static DataTable GetAllWorkoutsName()
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_GetAllWorkoutsName", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(dt);
                }
            }

            return dt;
        }

        public static bool DeleteWorkout(int workoutID)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_DeleteWorkout", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@WorkoutID", workoutID);

                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        conn.Close();

                        // إذا تم حذف صف واحد أو أكثر => true
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }

        public static bool IsWorkoutExist(int coachId, string name, string description)
        {
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("IsWorkoutExist", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CoachID", coachId);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Description", description);

                conn.Open();
                int result = Convert.ToInt32(cmd.ExecuteScalar());
                return result == 1;
            }
        }
    }
}
