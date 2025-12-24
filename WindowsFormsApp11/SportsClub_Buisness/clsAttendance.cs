using SC_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SportsClub_Buisness
{
    public class clsAttendance
    {
        
        enum enMode { AddNew = 0, Update = 1 }
        enMode Mode = enMode.AddNew;
        public int AttendanceID { get; set; }
        public int ReservationID { get; set; }
        public DateTime? CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }

        public clsAttendance()
        {
            AttendanceID=-1;
            ReservationID=0;
            CheckInTime=DateTime.MinValue;
            CheckOutTime=DateTime.MinValue;

            Mode = enMode.AddNew;
        }
        public clsAttendance(int attendanceID, int reservationID, DateTime? checkInTime, DateTime? checkOutTime)
        {
            AttendanceID=attendanceID;
            ReservationID=reservationID;
            CheckInTime=checkInTime;
            CheckOutTime=checkOutTime;

            Mode = enMode.Update;
        }

        public static clsAttendance Find(int attendanceID)
        {
            int reservationID = 0; DateTime? checkInTime = DateTime.MinValue; DateTime? checkOutTime = DateTime.MinValue;

            if (clsAttendanceData.GetAttendanceByID(attendanceID, ref reservationID, ref checkInTime, ref checkOutTime))
            {
                return new clsAttendance(attendanceID, reservationID, checkInTime, checkOutTime);
            }
            else
                return null;
          
        }

        private bool _AddNewAttendance()
        {
            this.AttendanceID =  clsAttendanceData.AddAttendance(this.ReservationID, this.CheckInTime);
           
            return (this.AttendanceID != -1);
        }

        private bool UpdateAttendance()
        {
            return clsAttendanceData.UpdateAttendance(this.AttendanceID, this.CheckInTime, this.CheckOutTime);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewAttendance())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    return false;

                case enMode.Update:
                    return UpdateAttendance();
            }
            return false;
        }

        public static DataTable GetAttendanceList()
        {
            return clsAttendanceData.GetAttendanceList();
        }

        public enum enAttendanceCheckResult
        {
            Allowed = 1,
            ReservationNotFound = -1,
            AttendanceAlreadyExists = -2
        }

        public static enAttendanceCheckResult CanAddAttendance(int reservationID)
        {
            int result =
                clsAttendanceData.CheckCanAddAttendance(reservationID);

            return (enAttendanceCheckResult)result;
        }

        public static bool Delete(int attendanceID)
        {
            return clsAttendanceData.DeleteAttendance(attendanceID);
        }

    }
}
