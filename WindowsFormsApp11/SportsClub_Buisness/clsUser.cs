using SC_DataAccess;
using SportsClub_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace SportsClub_Buisness
{
    public class clsUser
    {

        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int UserID { get; set; }
        public int PersonID { get; set; }
        public clsPerson PersonInfo;
        public string Username { get; set; }
        public string Password { get; set; }
        public int Role {  get; set; }

        public clsUser(int userID, int personID, string username, int role)
        {
            this.UserID=userID;
            this.PersonID=personID;
            PersonInfo = clsPerson.Find(this.PersonID);
            this.Username=username;
            this.Role=role;

            Mode = enMode.Update;
        }

        public clsUser(int userID, int personID, string username,string password, int role)
        {
            this.UserID=userID;
            this.PersonID=personID;
            PersonInfo = clsPerson.Find(this.PersonID);
            this.Username=username;
            this.Password=password;
            this.Role=role;

            Mode = enMode.Update;
        }

        public clsUser() 
        {
            this.UserID=-1;
            this.PersonID=0;
            this.Username="";
            this.Password="";
            this.Role=0;

            Mode = enMode.AddNew;
        }

        public static clsUser Find(int UserID)
        {

            int personID = 0;
            string username = ""; int role = 0;

            bool IsFound = clsUserData.GetUserInfoByID(UserID, ref personID, ref username, ref role);

            if (IsFound)
                //we return new object of that person with the right data
                return new clsUser(UserID, personID, username, role);
            else
                return null;
        }

        static public string GetPasswordHashByUsername(string username)
        {
            return clsUserData.GetPasswordHashByUsername(username);
        }

        public static string HashPassword(string password)
        {
            return clsUserData.HashPassword(password);
        }

        private bool _AddNewUser()
        {

            int newID = clsUserData.AddNewUser(this.PersonID, this.Username, this.Password, this.Role);

            if (newID > 0)
            {
                this.UserID = newID;
                return true;
            }
            return false;
        }

        private bool _UpdateUser()
        {
            //call DataAccess Layer 

            return clsUserData.UpdateUser(this.UserID, this.Username, this.Password, this.Role);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewUser())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateUser();

            }

            return false;
        }

        public static DataTable GetAllUsers()
        {
            return clsUserData.GetAllUsers(); 
        }

        public static bool IsUserExistForPersonID(int personID)
        {
            return clsUserData.IsUserExistForPersonID(personID);
        }

        public static bool IsUserExist(string UserName)
        {
            return clsUserData.IsUserExist(UserName);
        }

        public static bool DeleteUser(int userID)
        {
            return clsUserData.DeleteUser(userID);
        }

        public static clsUser FindByUsernameAndPassword(string UserName, string Password)
        {
            int UserID = -1;
            int PersonID = -1;

            int Role = 0;

            bool IsFound = clsUserData.GetUserInfoByUsernameAndPassword
                                ( UserName, Password, ref UserID, ref PersonID, ref Role);

            if (IsFound)
                //we return new object of that User with the right data
                return new clsUser(UserID, PersonID, UserName, Password, Role);
            else
                return null;
        }

        //[Flags]
        //public enum UserPermissions : long
        //{
        //    None = 0,

        //    // frmPeople
        //    frmPeople = 1L << 0,   // 1
        //    ShowPerson = 1L << 1,   // 2
        //    AddNewPerson = 1L << 2,   // 4
        //    UpdatePerson = 1L << 3,   // 8
        //    DeletePerson = 1L << 4,   // 16

        //    // frmMember
        //    frmMember = 1L << 5,   // 32
        //    ShowMember = 1L << 6,   // 64
        //    AddNewMember = 1L << 7,   // 128
        //    UpdateMember = 1L << 8,   // 256
        //    DeleteMember = 1L << 9,   // 512

        //    // frmCoach
        //    frmCoach = 1L << 10,  // 1024
        //    ShowCoach = 1L << 11,  // 2048
        //    AddNewCoach = 1L << 12,  // 4096
        //    UpdateCoach = 1L << 13,  // 8192
        //    DeleteCoach = 1L << 14,  // 16384

        //    // frmMemberSubscription
        //    frmMemberSubscription = 1L << 15,  // 32768
        //    AddMemberSubscription = 1L << 16,  // 65536
        //    UpdateMemberSubscription = 1L << 17, // 131072
        //    DeleteMemberSubscription = 1L << 18, // 262144

        //    // frmSubscription
        //    frmSubscription = 1L << 19,      // 524288
        //    AddSubscription = 1L << 20,      // 1048576
        //    UpdateSubscription = 1L << 21,   // 2097152
        //    DeleteSubscription = 1L << 22,   // 4194304

        //    // frmAttendance
        //    frmAttendance = 1L << 23,      // 8388608
        //    AddAttendance = 1L << 24,      // 16777216
        //    UpdateAttendance = 1L << 25,     // 33554432
        //    DeleteAttendance = 1L << 26,     // 67108864

        //    // frmPayment
        //    frmPayment = 1L << 27,      // 134217728
        //    UpdatePayment = 1L << 28,      // 268435456
        //    DeletePayment = 1L << 29,      // 536870912

        //    // frmReservation
        //    frmReservation = 1L << 30,      // 1073741824
        //    AddReservation = 1L << 31,      // 2147483648
        //    UpdateReservation = 1L << 32,    // 4294967296
        //    DeleteReservation = 1L << 33,    // 8589934592

        //    // frmSchedule
        //    frmSchedule = 1L << 34,      // 17179869184
        //    ShowSchedule = 1L << 35,      // 34359738368
        //    AddSchedule = 1L << 36,      // 68719476736
        //    UpdateSchedule = 1L << 37,      // 137438953472
        //    DeleteSchedule = 1L << 38,      // 274877906944

        //    // frmSpecialization
        //    frmSpecialization = 1L << 39,    // 549755813888
        //    AddSpecialization = 1L << 40,    // 1099511627776
        //    UpdateSpecialization = 1L << 41, // 2199023255552
        //    DeleteSpecialization = 1L << 42, // 4398046511104

        //    // frmWorkouts
        //    frmWorkouts = 1L << 43,      // 8796093022208
        //    ShowWorkouts = 1L << 44,      // 17592186044416
        //    AddWorkouts = 1L << 45,      // 35184272088832
        //    UpdateWorkouts = 1L << 46,      // 70368744177664
        //    DeleteWorkouts = 1L << 47,      // 140737488355328

        //    // frmUser
        //    frmUser = 1L << 48,      // 281474976710656
        //    ShowUser = 1L << 49,      // 562949953421312
        //    AddUser = 1L << 50,      // 1125899906842624
        //    UpdateUser = 1L << 51,      // 2251799813685248
        //    DeleteUser = 1L << 52,      // 4503599627370496
        //    ChangePasswordUser = 1L << 53    // 9007199254740992
        //}

        [Flags]
        public enum UserPermissions 
        {
            None = 0,              // لا صلاحيات

            // الصلاحيات على مستوى Forms
            frmPeople = 1 << 0,  // يمكن فتح frmPeople
            frmMember = 1 << 1,  // يمكن فتح frmMember
            frmCoach = 1 << 2,  // يمكن فتح frmCoach
            frmMemberSubscription = 1 << 3,  // يمكن فتح frmMemberSubscription
            frmSubscription = 1 << 4,  // يمكن فتح frmSubscription
            frmAttendance = 1 << 5,  // يمكن فتح frmAttendance
            frmPayment = 1 << 6,  // يمكن فتح frmPayment
            frmReservation = 1 << 7,  // يمكن فتح frmReservation
            frmSchedule = 1 << 8,  // يمكن فتح frmSchedule
            frmSpecialization = 1 << 9, // يمكن فتح frmSpecialization
            frmWorkouts = 1 << 10, // يمكن فتح frmWorkouts
            frmUser = 1 << 11  // يمكن فتح frmUser
        }

    }
}
