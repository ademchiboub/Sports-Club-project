using SportsClub_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace SportsClub_Buisness
{
    public class clsPerson
    {

        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
      
        public int PersonID { set; get; }
        public string FirstName { set; get; }
        public string SecondName { set; get; }
        public string ThirdName { set; get; }
        public string LastName { set; get; }
        public string FullName
        {
            get
            {
                // نجمع فقط الأسماء غير الفارغة أو null
                string[] names = new string[] { FirstName, SecondName, ThirdName, LastName };
                var validNames = names.Where(n => !string.IsNullOrWhiteSpace(n));
                return string.Join(" ", validNames);
            }
        }
        public DateTime DateOfBirth { set; get; }
        public string Gendor { set; get; }
        public string Address { set; get; }
        public string Phone { set; get; }
        public string Email { set; get; }
        public string ImagePath { set; get; }

        public clsPerson()

        {
            this.PersonID = -1;
            this.FirstName = "";
            this.SecondName = "";
            this.ThirdName = "";
            this.LastName = "";
            this.DateOfBirth = DateTime.Now;
            this.Gendor = "";
            this.Address = "";
            this.Phone = "";
            this.Email = "";
            this.ImagePath = "";

            Mode = enMode.AddNew;
        }

        private clsPerson(int PersonID, string FirstName, string SecondName, string ThirdName,
            string LastName, DateTime DateOfBirth, string Gendor,
             string Address, string Phone, string Email, string ImagePath)

        {
            this.PersonID = PersonID;
            this.FirstName = FirstName;
            this.SecondName= SecondName;
            this.ThirdName = ThirdName;
            this.LastName = LastName;
            this.DateOfBirth = DateOfBirth;
            this.Gendor= Gendor;
            this.Address = Address;
            this.Phone = Phone;
            this.Email = Email;
            this.ImagePath = ImagePath;
            Mode = enMode.Update;
        }

        private bool _AddNewPerson()
        {
            //call DataAccess Layer 
          
            this.PersonID = clsPersonData.AddNewPerson(this.FirstName, this.SecondName, this.ThirdName, this.LastName,
         this.DateOfBirth, this.Gendor, this.Phone, this.Email, this.Address, this.ImagePath);

            return (this.PersonID != -1);
        }

        private bool _UpdatePerson()
        {
            //call DataAccess Layer 

            return clsPersonData.UpdatePerson(this.PersonID, this.FirstName, this.SecondName, this.ThirdName,
        this.LastName, this.DateOfBirth, this.Gendor, this.Phone, this.Email, this.Address, this.ImagePath);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewPerson())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdatePerson();

            }

            return false;
        }

        public static clsPerson Find(int PersonID)
        {

            string FirstName = "", SecondName = "", ThirdName = "", LastName = "", Email = "", Phone = "", Address = "", ImagePath = "";
            DateTime DateOfBirth = DateTime.Now;
            string Gendor ="";

            bool IsFound = clsPersonData.FindPersonByID(PersonID, ref FirstName, ref SecondName, ref ThirdName,
            ref LastName, ref DateOfBirth, ref Gendor, ref Phone, ref Email, ref Address,
            ref ImagePath);

            if (IsFound)
                //we return new object of that person with the right data
                return new clsPerson(PersonID, FirstName, SecondName, ThirdName, LastName, DateOfBirth, Gendor,
                                     Address, Phone, Email, ImagePath);
            else
                return null;
        }

        public static DataTable GetAllPeople()
        {
            return clsPersonData.GetPeople();
        }

        public static bool DeletePerson(int PersonID)
        {
            return clsPersonData.DeletePerson(PersonID);
        }

        public static bool IsPersonExistFlexible(string firstName = null, string secondName = null, string thirdName = null,
                                                      string lastName = null)
        {
            return clsPersonData.IsPersonExistFlexible(firstName, secondName, thirdName, lastName);
        }
    }
}
