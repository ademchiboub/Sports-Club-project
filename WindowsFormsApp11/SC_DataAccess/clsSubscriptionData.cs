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
    public class clsSubscriptionData
    {
        public static int AddSubscription(string type, int duration, decimal price)
        {
            int newID = 0;

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("AddSubscription", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Type", type);
                cmd.Parameters.AddWithValue("@DurationMonths", duration);
                cmd.Parameters.AddWithValue("@Price", price);

                // إضافة باراميتر OUTPUT
                SqlParameter outputIdParam = new SqlParameter("@NewSubscriptionID", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(outputIdParam);

                conn.Open();
               
                cmd.ExecuteNonQuery();

                newID = Convert.ToInt32(outputIdParam.Value);
            }

            return newID;
        }

        public static bool UpdateSubscription(int id, string type, int duration, decimal price)
        {
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("SP_UpdateSubscription", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SubscriptionID", id);
                cmd.Parameters.AddWithValue("@Type", type);
                cmd.Parameters.AddWithValue("@DurationMonths", duration);
                cmd.Parameters.AddWithValue("@Price", price);

                conn.Open();
                int rows = (int)cmd.ExecuteScalar();
                return rows > 0;
            }
        }

        public static bool DeleteSubscription(int id)
        {
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("SP_DeleteSubscription", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SubscriptionID", id);

                conn.Open();
                int rows = (int)cmd.ExecuteScalar();
                return rows > 0;
            }
        }

        public static bool GetSubscriptionByID(int id, ref string type, ref int duration, ref decimal price)
        {
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("SP_GetSubscriptionByID", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SubscriptionID", id);

                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        type = reader["Type"].ToString();
                        duration = (int)reader["DurationMonths"];
                        price = (decimal)reader["Price"];
                        return true;
                    }
                }
            }
            return false;
        }

        public static DataTable GetAllSubscriptions()
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("SP_GetAllSubscriptions", conn))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                da.Fill(dt);
            }

            return dt;
        }
    }
}
