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
    public class clsSchedulesData
    {
        public static DataTable GetAllSchedules()
        {
            DataTable dtSchedules = new DataTable();

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_GetSchedulesDetailsSimple", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(dtSchedules);
                }
            }

            return dtSchedules;
        }

        public static int AddSchedule(int workoutID, string day, TimeSpan start, TimeSpan end, int maxMembers, int createByUserID)
        {
            int newScheduleID = 0;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_AddSchedule", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@WorkoutID", workoutID);
                command.Parameters.AddWithValue("@Day", day);
                command.Parameters.AddWithValue("@StartTime", start);
                command.Parameters.AddWithValue("@EndTime", end);
                command.Parameters.AddWithValue("@MaxMembers", maxMembers);
                command.Parameters.AddWithValue("@CreateByUserID", createByUserID);

                // تعريف OUT parameter
                SqlParameter outParam = new SqlParameter("@NewScheduleID", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(outParam);

                connection.Open();
                command.ExecuteNonQuery();

                // الحصول على القيمة بعد التنفيذ
                newScheduleID = (int)outParam.Value;
            }

            return newScheduleID;
        }

        public static bool UpdateSchedule(int scheduleID, int workoutID, string day, TimeSpan start, TimeSpan end, int maxMembers)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_UpdateSchedule", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ScheduleID", scheduleID);
                command.Parameters.AddWithValue("@WorkoutID", workoutID);
                command.Parameters.AddWithValue("@Day", day);
                command.Parameters.AddWithValue("@StartTime", start);
                command.Parameters.AddWithValue("@EndTime", end);
                command.Parameters.AddWithValue("@MaxMembers", maxMembers);

                connection.Open();

                // ExecuteNonQuery ترجع عدد الصفوف المتأثرة
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        public static bool DeleteSchedule(int scheduleID)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_DeleteSchedule", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ScheduleID", scheduleID);

                connection.Open();

                // هذا يرجع عدد الصفوف المتأثرة
                int rowsAffected = command.ExecuteNonQuery();

                // أسرع طريقة لمعرفة إن تم الحذف
                return rowsAffected > 0;
            }


        }

        public static bool GetScheduleInfoByID(int scheduleID, ref int workoutID, ref string day, ref TimeSpan startTime,
                ref TimeSpan endTime, ref int maxMembers, ref int createByUserID, ref DateTime createDate)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_GetScheduleInfoByID", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ScheduleID", scheduleID);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        workoutID = reader.GetInt32(reader.GetOrdinal("WorkoutID"));
                        day = reader.GetString(reader.GetOrdinal("Day"));
                        startTime = reader.GetTimeSpan(reader.GetOrdinal("StartTime"));
                        endTime = reader.GetTimeSpan(reader.GetOrdinal("EndTime"));
                        maxMembers = reader.GetInt32(reader.GetOrdinal("MaxMembers"));
                        createByUserID = reader.GetInt32(reader.GetOrdinal("CreateByUserID"));
                        createDate = reader.GetDateTime(reader.GetOrdinal("CreateDate"));

                        isFound = true;
                    }
                }
            }

            return isFound;
        }

        public static bool IsScheduleConflict(int workoutId, string day, TimeSpan startTime, TimeSpan endTime)
        {
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("IsScheduleConflict", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@WorkoutID", workoutId);
                cmd.Parameters.AddWithValue("@Day", day);
                cmd.Parameters.AddWithValue("@StartTime", startTime);
                cmd.Parameters.AddWithValue("@EndTime", endTime);

                conn.Open();
                int result = Convert.ToInt32(cmd.ExecuteScalar());
                return result == 1; // true إذا يوجد تداخل
            }
        }
    }
}
