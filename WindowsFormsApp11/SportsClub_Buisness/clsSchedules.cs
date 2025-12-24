using SC_DataAccess;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsClub_Buisness
{
    public class clsSchedules
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int ScheduleID { get; set; }
        public int WorkoutID { get; set; }
        public clsWorkouts WorkoutInfo;
        public string Day {  get; set; }
        public TimeSpan StartTime {  get; set; }
        public TimeSpan EndTime { get; set; }
        public int MaxMembers { get; set; }
        public int CreateByUserID { get; set; }
        public clsUser UserInfo;
        public DateTime CreateDate { get; set; }

        public clsSchedules()
        {
            this.ScheduleID=0;
            this.WorkoutID=0;
            this.Day="";
            this.StartTime= TimeSpan.MinValue;
            this.EndTime=TimeSpan.MinValue;
            this.MaxMembers=0;
            this.CreateByUserID=0;
            this.CreateDate=DateTime.MinValue;

            Mode = enMode.AddNew;
        }
        public clsSchedules(int scheduleID, int workoutID, string day, TimeSpan startTime, TimeSpan endTime, 
            int maxMembers, int createByUserID, DateTime createDate)
        {
            this.ScheduleID=scheduleID;
            this.WorkoutID=workoutID;
            this.WorkoutInfo = clsWorkouts.Find(WorkoutID);
            this.Day=day;
            this.StartTime=startTime;
            this.EndTime=endTime;
            this.MaxMembers=maxMembers;
            this.CreateByUserID=createByUserID;
            this.UserInfo= clsUser.Find(CreateByUserID);
            this.CreateDate=createDate;

            Mode = enMode.Update;
        }

        public static clsSchedules Find(int ScheduleID)
        {
            int workoutID = 0; string day = ""; TimeSpan startTime = TimeSpan.MinValue, endTime = TimeSpan.MinValue;
            int maxMembers = 0, createByUserID = 0; DateTime createDate = DateTime.Now;

            if (clsSchedulesData.GetScheduleInfoByID(ScheduleID, ref workoutID, ref day, ref startTime,
                ref endTime, ref maxMembers, ref createByUserID, ref createDate))
            {
                return new clsSchedules(ScheduleID, workoutID, day, startTime, endTime,
                                        maxMembers, createByUserID, createDate);
            }
            else
                return null;
        }

        public static DataTable GetAllSchedules()
        {
            return clsSchedulesData.GetAllSchedules();
        }

        private bool _AddNewSchedule()
        {

            int newID = clsSchedulesData.AddSchedule(this.WorkoutID, this.Day,  this.StartTime, this.EndTime, this.MaxMembers, this.CreateByUserID);

            if (newID > 0)
            {
                this.ScheduleID = newID;
                return true;
            }
            return false;
        }

        private bool _UpdateSchedule()
        {
            //call DataAccess Layer 

            return clsSchedulesData.UpdateSchedule(this.ScheduleID, this.WorkoutID, this.Day, this.StartTime, this.EndTime, 
                this.MaxMembers);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewSchedule())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateSchedule();

            }

            return false;
        }

        public static bool DeleteSchedule(int scheduleID)
        {
            return clsSchedulesData.DeleteSchedule(scheduleID);
        }

        public static bool IsScheduleConflict(int workoutId, string day, TimeSpan startTime, TimeSpan endTime)
        {
            return clsSchedulesData.IsScheduleConflict(workoutId, day, startTime, endTime);
        }
    }
}
