using SC_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using static SC_DataAccess.clsReservationData;

namespace SportsClub_Buisness
{
    public class clsReservation
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int ReservationID {  get; set; }
        public int MemberID {  get; set; }
        public clsMember MemberInfo;
        public int ScheduleID;

        
        public clsSchedules ScheduleInfo;
        public string Status {  get; set; }
        public DateTime ReservationDate {  get; set; }
        public int CreateByUserID {  get; set; }

        public clsReservation()
        {
            ReservationID=-1;
            MemberID=0;
            ScheduleID=0;
            Status="";
            ReservationDate=DateTime.MinValue;
            CreateByUserID=0;

            Mode = enMode.AddNew;
        }

        public clsReservation(int reservationID, int memberID, int scheduleID, string status, DateTime reservationDate, int createByUserID)
        {
            ReservationID=reservationID;
            MemberID=memberID;
            this.MemberInfo = clsMember.Find(MemberID);
            ScheduleID=scheduleID;
            this.ScheduleInfo = clsSchedules.Find(ScheduleID);
            Status=status;
            ReservationDate=reservationDate;
            CreateByUserID=createByUserID;

            Mode = enMode.Update;
        }

        public static clsReservation Find(int ReservationID)
        {
            int memberID = 0, scheduleID = 0; DateTime reservationDate = DateTime.MinValue;
            string status = ""; int createByUserID = 0;

            if (clsReservationData.GetReservationByID(ReservationID, ref memberID, ref scheduleID, ref reservationDate,
                                                 ref status, ref createByUserID))
            {
                return new clsReservation(ReservationID, memberID, scheduleID, status, reservationDate, createByUserID);
            }
            else
                return null;

        }

        private bool _AddReservation()
        {
            this.ReservationID = clsReservationData.AddReservation(this.MemberID, this.ScheduleID, this.Status, this.CreateByUserID);

            return (this.ReservationID != -1);
        }

        private bool _UpdateReservation()
        {
            return clsReservationData.UpdateReservation(this.ReservationID, this.ScheduleID, this.Status);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddReservation())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateReservation();

            }

            return false;
        }

        public static bool DeleteReservation(int reservationID, out string msg)
        {
            return clsReservationData.DeleteReservation(reservationID, out msg);
        }

        public static DataTable GetAllReservations()
        {
            return clsReservationData.GetAllReservations();
        }

        public enum enReservationCheckResult
        {
            Allowed = 1,
            NoActiveSubscription = -1,
            DuplicateReservation = -2,
            ScheduleFull = -3
        }

        public static enReservationCheckResult CanAddReservation(int memberID, int scheduleID)
        {
            int number = clsReservationData.CanAddReservation(memberID, scheduleID);

            // إذا number لا يطابق Enum، نرجع Allowed
            if (!Enum.IsDefined(typeof(enReservationCheckResult), number))
                return enReservationCheckResult.Allowed;

            return (enReservationCheckResult)number;
        }

    }
}
