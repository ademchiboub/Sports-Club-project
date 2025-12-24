using SC_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static SportsClub_Buisness.clsMember;

namespace SportsClub_Buisness
{
    public class clsWorkouts
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;


        
        public int WorkoutID { get; private set; }
        public int CoachID {  get; set; }
        public clsCoach CoachInfo { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }


        public clsWorkouts()
        {
            WorkoutID=-1;
            CoachID=0;
            Name="";
            Description="";

            Mode = enMode.AddNew;
        }
        public clsWorkouts(int workoutID, int coachID, string name, string description)
        {
            WorkoutID=workoutID;
            CoachID=coachID;
            CoachInfo= clsCoach.Find(CoachID);
            Name=name;
            Description=description;

            Mode = enMode.Update;
        }

        // ------------------ Find by MemberID ------------------
        public static clsWorkouts Find(int workoutID)
        {
            int coachID = -1;
            string name = "", description = "";

            bool isFound = clsWorkoutsData.GetWorkoutInfoByID(workoutID, ref coachID, ref name, ref description);
               
            if (isFound)
            {
                return new clsWorkouts(workoutID, coachID, name, description);
            }

            return null;
        }

        private bool _AddNewWorkout()
        {
            this.WorkoutID = clsWorkoutsData.AddNewWorkout(this.CoachID, this.Name, this.Description);

            return (this.WorkoutID != -1);
        }

        private bool _UpdateWorkout()
        {
            return clsWorkoutsData.UpdateWorkout(this.WorkoutID, this.CoachID, this.Name, this.Description);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewWorkout())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateWorkout();
            }
            return false;
        }

        public static DataTable GetAllWorkouts()
        {
            return clsWorkoutsData.GetWorkoutsOverview();
        }

        public static DataTable GetAllWorkoutsName()
        {
            return clsWorkoutsData.GetAllWorkoutsName();
        }

        public static bool DeleteWorkout(int workoutID)
        {
            return clsWorkoutsData.DeleteWorkout(workoutID);
        }

        public static bool IsWorkoutExist(int coachId, string name, string description)
        {
            return clsWorkoutsData.IsWorkoutExist(coachId, name, description);
        }
    }
}
