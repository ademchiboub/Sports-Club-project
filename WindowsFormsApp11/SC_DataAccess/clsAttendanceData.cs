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
    public class clsAttendanceData
    {


        public static int AddAttendance(int reservationID, DateTime? checkInTime)
        {
             int attendanceID = -1;
            using (SqlConnection conn =
                new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand cmd =
                new SqlCommand("AddAttendance", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ReservationID", reservationID);
                cmd.Parameters.AddWithValue("@CheckInTime", (object)checkInTime ?? DBNull.Value);

                SqlParameter output =
                    new SqlParameter("@AttendanceID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };

                cmd.Parameters.Add(output);

                conn.Open();
                cmd.ExecuteNonQuery();

                attendanceID = (int)output.Value;
                return attendanceID;
            }
        }
        public static bool UpdateAttendance(int attendanceID, DateTime? checkIn, DateTime? checkOut)
        {
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("UpdateAttendance", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AttendanceID", attendanceID);
                cmd.Parameters.AddWithValue("@CheckInTime", (object)checkIn ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@CheckOutTime", (object)checkOut ?? DBNull.Value);
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                return rows > 0;
            }

        }

        public static bool GetAttendanceByID(int attendanceID, ref int reservationID, ref DateTime? checkInTime, ref DateTime? checkOutTime)
        {
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("GetAttendanceByID", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AttendanceID", attendanceID);

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        reservationID = reader.GetInt32(reader.GetOrdinal("ReservationID"));
                        checkInTime = reader.IsDBNull(reader.GetOrdinal("CheckInTime")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("CheckInTime"));
                        checkOutTime = reader.IsDBNull(reader.GetOrdinal("CheckOutTime")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("CheckOutTime"));
                        return true;
                    }
                    else
                    {
                        return false; // لم يوجد السجل
                    }
                }
            }
        }

        public static DataTable GetAttendanceList()
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("SP_GetAttendanceList", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }

            return dt;
        }

        public static int CheckCanAddAttendance(int reservationID)
        {
            using (SqlConnection conn =
                new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand cmd =
                new SqlCommand("CheckCanAddAttendance", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ReservationID", reservationID);

                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public static bool DeleteAttendance(int attendanceID)
        {
            using (SqlConnection conn =
                new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand cmd =
                new SqlCommand("DeleteAttendance", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AttendanceID", attendanceID);

                conn.Open();
                int result = Convert.ToInt32(cmd.ExecuteScalar());

                return result == 1;
            }
        }
    }
}
