using SC_DataAccess;
using System;
using System.Data;

namespace SportsClub_Buisness
{
    public class clsMember
    {
        // ------------------ Enum Status ------------------
        public enum MemberStatus
        {
            Active = 0,
            Inactive = 1,
            Suspended = 2
        }

        // ------------------ Mode ------------------
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;

        // ------------------ Properties ------------------
        public int MemberID { get; private set; }
        public int PersonID { get; set; }
        public clsPerson PersonInfo { get; private set; }
        public MemberStatus Status { get; set; } = MemberStatus.Active;


        // ------------------ Constructor ------------------
        public clsMember(int memberID, int personID, MemberStatus status)
        {
            MemberID = memberID;
            PersonID = personID;
            PersonInfo = clsPerson.Find(PersonID);
            Status = status;
            Mode = enMode.Update;
        }

        public clsMember() // Add new
        {
            this.MemberID =0;
            this.PersonID =0;
            Status = MemberStatus.Active;
            Mode = enMode.AddNew;
        }

        // ------------------ Find by MemberID ------------------
        public static clsMember Find(int MemberID)
        {
            int personID = -1;
            string statusString = "";

            bool isFound = clsMemberData.GetMemberInfoByID(MemberID,
                ref personID,
                ref statusString);

            if (isFound)
            {
                clsPerson person = clsPerson.Find(personID);

                MemberStatus Status = MemberStatus.Active;
                Enum.TryParse(statusString, out Status);

                return new clsMember(MemberID, personID, Status);
            }

            return null;
        }

        // ------------------ Save ------------------
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewMember())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    return false;

                case enMode.Update:
                    return _UpdateMember();
            }
            return false;
        }

        // ------------------ Add ------------------
        private bool _AddNewMember()
        {

            int newID = clsMemberData.AddNewMember(this.PersonID, Status.ToString());

            if (newID > 0)
            {
                this.MemberID = newID;
                return true;
            }
            return false;
        }

        // ------------------ Update ------------------
        private bool _UpdateMember()
        {
            if (MemberID <= 0)
                return false;

            return clsMemberData.UpdateMember(MemberID, Status.ToString());
        }

        // ------------------ Delete ------------------
        static public bool DeleteMember(int memberID)
        {
            return clsMemberData.DeleteMember(memberID);
        }

        // ------------------ Get All ------------------
        public static DataTable GetAllMembers()
        {
            return clsMemberData.GetAllMembers();
        }

        // ------------------ Check Exist ------------------
        public static bool IsMemberExistAndActive(int personId)
        {
            return clsMemberData.IsMemberExistAndActive(personId);
        }
    }
}