using SC_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace SportsClub_Buisness
{
    public class clsMemberSubscription
    {
        enum enMode { AddNew, Update};
        enMode Mode = enMode.AddNew;
        public int MemberSubscriptionID { get; set; }
        public int MemberID { get; set; }
        public clsMember MemberInfo;
        public int SubscriptionID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public int CreateByUserID { get; set; }

        public clsMemberSubscription()
        {
            MemberSubscriptionID=-1;
            MemberID=0;
            SubscriptionID=0;
            StartDate=DateTime.MinValue;
            EndDate=DateTime.MinValue;
            Status="";
            CreateByUserID=0;

            Mode =enMode.AddNew;
        }
        public clsMemberSubscription(int memberSubscriptionID, int memberID, int subscriptionID, DateTime startDate, 
            DateTime endDate, string status, int createByUserID)
        {
            MemberSubscriptionID=memberSubscriptionID;
            MemberID=memberID;
            this.MemberInfo = clsMember.Find(MemberID);
            SubscriptionID=subscriptionID;
            StartDate=startDate;
            EndDate=endDate;
            Status=status;
            CreateByUserID=createByUserID;

            Mode = enMode.Update;
        }

        private bool _AddMemberSubscription()
        {
            this.MemberSubscriptionID = clsMemberSubscriptionData.AddMemberSubscription(this.MemberID, this.SubscriptionID,
                this.StartDate, this.EndDate, this.Status, this.CreateByUserID);

            return (this.MemberSubscriptionID != -1);
        }

        private bool _UpdateMemberSubscription()
        {
            return clsMemberSubscriptionData.UpdateMemberSubscription(this.MemberSubscriptionID, this.SubscriptionID, this.StartDate,
            this.EndDate, this.Status);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddMemberSubscription())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateMemberSubscription();

            }

            return false;
        }

        public static clsMemberSubscription Find(int MemberSubscriptionID)
        {
            int memberID = 0, subscriptionID = 0; DateTime startDate = DateTime.MinValue,
             endDate = DateTime.MinValue; string status = ""; int createByUserID = 0;

            if (clsMemberSubscriptionData.GetMemberSubscriptionByID(MemberSubscriptionID, ref memberID, ref subscriptionID, ref startDate,
                         ref endDate, ref status, ref createByUserID))
            {
                return new clsMemberSubscription(MemberSubscriptionID, memberID, subscriptionID, startDate,
                         endDate, status, createByUserID);
            }
            else
                return null;
        }

        public static bool DeleteMemberSubscription(int MemberSubscriptionID)
        {
            return clsMemberSubscriptionData.DeleteMemberSubscription(MemberSubscriptionID);
        }

        public static DataTable GetMemberSubscriptionsOverview()
        {
            return clsMemberSubscriptionData.GetMemberSubscriptionsOverview();
        }

        public static bool IsMemberSubscriptionConflict(int memberID, DateTime startDate, DateTime endDate)
        {
            return clsMemberSubscriptionData.IsMemberSubscriptionConflict(memberID, startDate, endDate);
        }
    }
}
