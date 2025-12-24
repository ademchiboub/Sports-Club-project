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
    public class clsPaymentData
    {
        public static int AddPayment(int memberSubscriptionID, decimal amount, DateTime? paymentDate, string method, int createByUserID)
        {
            using (SqlConnection con = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("SP_AddPayment", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MemberSubscriptionID", memberSubscriptionID);
                cmd.Parameters.AddWithValue("@Amount", amount);
                cmd.Parameters.AddWithValue("@PaymentDate", (object)paymentDate ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Method", (object)method ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@CreateByUserID", createByUserID);

                SqlParameter outputId = new SqlParameter("@NewPaymentID", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(outputId);

                con.Open();
                cmd.ExecuteNonQuery();

                return (int)outputId.Value;
            }
        }

        public static bool UpdatePayment(int paymentID, int memberSubscriptionID, decimal amount, DateTime paymentDate, string method)
        {
            using (SqlConnection con = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("SP_UpdatePayment", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PaymentID", paymentID);
                cmd.Parameters.AddWithValue("@MemberSubscriptionID", memberSubscriptionID);
                cmd.Parameters.AddWithValue("@Amount", amount);
                cmd.Parameters.AddWithValue("@PaymentDate", paymentDate);
                cmd.Parameters.AddWithValue("@Method", (object)method ?? DBNull.Value);

                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        public static bool DeletePayment(int paymentID)
        {
            using (SqlConnection con = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("SP_DeletePayment", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PaymentID", paymentID);

                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        public static bool GetPaymentByID( int paymentID, ref int memberSubscriptionID, ref decimal amount,
                          ref DateTime paymentDate, ref string method, ref int createByUserID)
        {
            using (SqlConnection con = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("SP_GetPaymentByID", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PaymentID", paymentID);

                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        memberSubscriptionID = reader.GetInt32(reader.GetOrdinal("MemberSubscriptionID"));
                        amount = reader.GetDecimal(reader.GetOrdinal("Amount"));
                        paymentDate = reader.GetDateTime(reader.GetOrdinal("PaymentDate"));
                        method = reader.IsDBNull(reader.GetOrdinal("Method")) ? null : reader.GetString(reader.GetOrdinal("Method"));
                        createByUserID = reader.GetInt32(reader.GetOrdinal("CreateByUserID"));
                        return true;
                    }
                }
            }
            return false; // لم يتم العثور على سجل
        }

        public static DataTable GetAllPayments()
        {
            using (SqlConnection con = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("SP_GetAllPayments", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }
    }
}
