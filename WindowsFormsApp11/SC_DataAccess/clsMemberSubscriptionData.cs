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
    public class clsMemberSubscriptionData
    {
        public static int AddMemberSubscription(int memberID, int subscriptionID, DateTime startDate, DateTime endDate,
            string status, int createByUserID)
        {
            int newID = 0;
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("SP_AddMemberSubscription", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MemberID", memberID);
                cmd.Parameters.AddWithValue("@SubscriptionID", subscriptionID);
                cmd.Parameters.AddWithValue("@StartDate", startDate);
                cmd.Parameters.AddWithValue("@EndDate", endDate);
                cmd.Parameters.AddWithValue("@Status", status);
                cmd.Parameters.AddWithValue("@CreateByUserID", createByUserID);

                SqlParameter pID = new SqlParameter("@NewMemberSubscriptionID", SqlDbType.Int) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(pID);

                conn.Open();
                cmd.ExecuteNonQuery();

                newID = Convert.ToInt32(pID.Value);
            }
            return newID;
        }

        public static bool UpdateMemberSubscription(int memberSubscriptionID, int subscriptionID, DateTime startDate, 
            DateTime endDate, string status)
        {
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("SP_UpdateMemberSubscription", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MemberSubscriptionID", memberSubscriptionID);
                cmd.Parameters.AddWithValue("@SubscriptionID", subscriptionID);
                cmd.Parameters.AddWithValue("@StartDate", startDate);
                cmd.Parameters.AddWithValue("@EndDate", endDate);
                cmd.Parameters.AddWithValue("@Status", status);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        public static bool DeleteMemberSubscription(int memberSubscriptionID)
        {
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("SP_DeleteMemberSubscription", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MemberSubscriptionID", memberSubscriptionID);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        public static bool GetMemberSubscriptionByID(int memberSubscriptionID,
    ref int memberID, ref int subscriptionID, ref DateTime startDate,
    ref DateTime endDate, ref string status, ref int createByUserID)
        {
            bool isFound = false;

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("SP_GetMemberSubscriptionByID", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MemberSubscriptionID", memberSubscriptionID);

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        memberID = reader.GetInt32(reader.GetOrdinal("MemberID"));
                        subscriptionID = reader.GetInt32(reader.GetOrdinal("SubscriptionID"));
                        startDate = reader.GetDateTime(reader.GetOrdinal("StartDate"));
                        endDate = reader.GetDateTime(reader.GetOrdinal("EndDate"));
                        status = reader.GetString(reader.GetOrdinal("SubscriptionStatus"));
                        createByUserID = reader.GetInt32(reader.GetOrdinal("CreateByUserID"));

                        isFound = true;
                    }
                }
            }

            return isFound;
        }

        public static DataTable GetMemberSubscriptionsOverview()
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("SP_GetMemberSubscriptionsOverview", conn))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                da.Fill(dt);
            }

            return dt;
        }

        public static bool IsMemberSubscriptionConflict( int memberID, DateTime startDate, DateTime endDate)
        {
            bool isConflict = false;

            using (SqlConnection connection =
                new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command =
                    new SqlCommand("IsMemberSubscriptionConflict", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@MemberID", memberID);
                    command.Parameters.AddWithValue("@StartDate", startDate);
                    command.Parameters.AddWithValue("@EndDate", endDate);

                    connection.Open();

                    // procedure ترجع 1 أو 0
                    object result = command.ExecuteScalar();

                    if (result != null && Convert.ToInt32(result) == 1)
                        isConflict = true;
                }
            }

            return isConflict;
        }

    }
}
