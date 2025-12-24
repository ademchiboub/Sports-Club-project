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
    public class clsReservationData
    {

        public static DataTable GetAllReservations()
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("SP_GetReservationsList", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    dt.Load(reader);
                }
            }

            return dt;
        }
        public static int AddReservation(int memberID, int scheduleID, string status, int userID)
        {
            int newID = 0;

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("SP_AddReservation", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MemberID", memberID);
                cmd.Parameters.AddWithValue("@ScheduleID", scheduleID);
                cmd.Parameters.AddWithValue("@Status", status);
                cmd.Parameters.AddWithValue("@CreateByUserID", userID);

                SqlParameter outputId = new SqlParameter("@NewReservationID", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };

                cmd.Parameters.Add(outputId);

                conn.Open();
                cmd.ExecuteNonQuery();

                newID = Convert.ToInt32(outputId.Value);
            }

            return newID;
        }

        public static bool GetReservationByID(int reservationID, ref int memberID, ref int scheduleID, ref DateTime reservationDate,
                                                 ref string status, ref int createByUserID)
        {
            bool isFound = false;

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("SP_GetReservationByID", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ReservationID", reservationID);

                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        isFound = true;

                        memberID = reader.GetInt32(reader.GetOrdinal("MemberID"));
                        scheduleID = reader.GetInt32(reader.GetOrdinal("ScheduleID"));
                        reservationDate = reader.GetDateTime(reader.GetOrdinal("ReservationDate"));
                        status = reader["Status"] != DBNull.Value ? reader.GetString(reader.GetOrdinal("Status")) : "";
                        createByUserID = reader.GetInt32(reader.GetOrdinal("CreateByUserID"));
                    }
                }
            }

            return isFound;
        }

        public static bool UpdateReservation(int reservationID, int scheduleID, string status)
        {
            bool isUpdated = false;

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("SP_UpdateReservation", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ReservationID", reservationID);
                cmd.Parameters.AddWithValue("@ScheduleID", scheduleID);
                cmd.Parameters.AddWithValue("@Status", status);

                conn.Open();

                int rowsAffected = Convert.ToInt32(cmd.ExecuteScalar());

                isUpdated = rowsAffected > 0;
            }

            return isUpdated;
        }

        public static bool DeleteReservation(int reservationID, out string errorMessage)
        {
            errorMessage = string.Empty;
            int rowsAffected = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand("SP_DeleteReservation", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ReservationID", reservationID);

                    conn.Open();
                    rowsAffected = cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547) // Foreign key constraint violation
                {
                    errorMessage = "This reservation cannot be deleted because it is linked to attendance records.";
                }
                else
                {
                    errorMessage = "An error occurred while deleting: " + ex.Message;
                }

                return false;
            }

            return rowsAffected > 0;
        }


        public enum enReservationCheckResult
        {
            Allowed = 1,
            NoActiveSubscription = -1,
            DuplicateReservation = -2,
            ScheduleFull = -3
        }
        public static int CanAddReservation(int memberID, int scheduleID)
        {
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("CanAddReservation", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MemberID", memberID);
                cmd.Parameters.AddWithValue("@ScheduleID", scheduleID);

                conn.Open();
                object resultObj = cmd.ExecuteScalar();

                if (resultObj == null || resultObj == DBNull.Value)
                    return (int)enReservationCheckResult.Allowed; // Default to Allowed

                return Convert.ToInt32(resultObj);
            }
        }

    }
}
