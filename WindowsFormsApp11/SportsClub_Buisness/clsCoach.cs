using SC_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using static SportsClub_Buisness.clsMember;

namespace SportsClub_Buisness
{
    public class clsCoach
    {

        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;

        public int CoachID { get; private set; }
        public int PersonID { get; set; }
        public int SpecializationID { get; set; }
        public clsPerson PersonInfo { get; private set; }

        public clsCoach(int CoachID, int PersonID, int SpecializationID)
        {
            this.CoachID = CoachID;
            this.PersonID = PersonID;
            this.SpecializationID = SpecializationID;
            PersonInfo = clsPerson.Find(PersonID);
    
            Mode = enMode.Update;
        }

        public clsCoach() 
        {
            this.CoachID =0;
            this.PersonID =0;
            this.SpecializationID = 0;

            Mode = enMode.AddNew;
        }

        public static clsCoach Find(int CoachID)
        {
            int personID = -1;
            int SpecializationID = 0;

            bool isFound = clsCoachData.GetCoachInfoByID(CoachID, ref personID, ref SpecializationID);



            if (isFound)
            {
                clsPerson person = clsPerson.Find(personID);

                return new clsCoach(CoachID, personID, SpecializationID);
            }
            
                return null;
        }

        public static DataTable GetAllCoaches()
        {
            return clsCoachData.GetCoachesOverview();
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewCoach())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    return false;

                case enMode.Update:
                    return _UpdateCoach();
            }

            return false;
        }

        // ------------------ Add ------------------
        private bool _AddNewCoach()
        {
            int newID = clsCoachData.AddNewCoach(this.PersonID, this.SpecializationID);

            if (newID > 0)
            {
                this.CoachID = newID;
                return true;
            }

            return false;
        }

        // ------------------ Update ------------------
        private bool _UpdateCoach()
        {
            if (CoachID <= 0)
                return false;

            return clsCoachData.UpdateCoach(CoachID, SpecializationID);
        }

        static public bool DeleteCoach(int CoachID)
        {
            return clsCoachData.DeleteCoach(CoachID);
        }

        public static bool IsCoachExist(int personId)
        {
            return clsCoachData.IsCoachExist(personId);
        }
    }
}
