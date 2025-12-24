using SC_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsClub_Buisness
{
    public class clsPayment
    {
        enum enMode { AddNew, Update};
        enMode Mode = enMode.AddNew;
        public int PaymentID {  get; set; }
        public int MemberSubscriptionID {  get; set; }
        public clsMemberSubscription MemberSubscriptionInfo;
        public Decimal Amount {  get; set; }
        public DateTime PaymentDate {  get; set; }
        public string Method {  get; set; }
        public int CreateByUserID {  get; set; }

        public clsPayment()
        {
            PaymentID=-1;
            MemberSubscriptionID=0;
            Amount=0;
            PaymentDate=DateTime.MinValue;
            Method="";
            CreateByUserID=0;

            Mode=enMode.AddNew;
        }
        public clsPayment(int paymentID, int memberSubscriptionID, decimal amount, DateTime paymentDate, string method, int createByUserID)
        {
            PaymentID=paymentID;
            MemberSubscriptionID=memberSubscriptionID;
            this.MemberSubscriptionInfo = clsMemberSubscription.Find(MemberSubscriptionID);
            Amount=amount;
            PaymentDate=paymentDate;
            Method=method;
            CreateByUserID=createByUserID;

            Mode = enMode.Update;
        }

        private bool _AddPayment()
        {
            this.PaymentID = clsPaymentData.AddPayment(this.MemberSubscriptionID, this.Amount, this.PaymentDate, this.Method, this.CreateByUserID);

            return (this.PaymentID != -1);
        }

        private bool _UpdatePayment()
        {
            return clsPaymentData.UpdatePayment(this.PaymentID, this.MemberSubscriptionID, this.Amount, this.PaymentDate, this.Method);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddPayment())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdatePayment();

            }

            return false;
        }

        public static bool DeletePayment(int paymentID)
        {
            return clsPaymentData.DeletePayment(paymentID);
        }

        public static clsPayment Find(int paymentID)
        {
            int memberSubscriptionID = 0;  decimal amount = 0;
            DateTime paymentDate = DateTime.MinValue;  string method = "";  int createByUserID = 0;

            if (clsPaymentData.GetPaymentByID(paymentID, ref memberSubscriptionID, ref amount,
                          ref paymentDate, ref method, ref createByUserID))
            {
                return new clsPayment(paymentID, memberSubscriptionID, amount, paymentDate, method, createByUserID);
            }
            else
                return null;
        }

        public static DataTable GetAllPayments()
        {
            return clsPaymentData.GetAllPayments();
        }
    }
}
