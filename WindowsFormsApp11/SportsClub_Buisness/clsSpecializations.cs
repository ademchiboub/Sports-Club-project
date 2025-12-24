using SC_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsClub_Buisness
{
    public class clsSpecializations
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        
        public int SpecializationID {  get; set; }
        public string Name { get; set; }

        public clsSpecializations()
        {
            this.SpecializationID = -1;
            this.Name = string.Empty;

            Mode = enMode.AddNew;
        }
        public clsSpecializations(int specializationID, string name)
        {
            SpecializationID=specializationID;
            Name=name;

            Mode = enMode.Update;
        }

        private bool _AddNew()
        {
            this.SpecializationID = clsSpecializationsData.AddNew(this.Name);

            return (this.SpecializationID != -1);
        }

        private bool _Update()
        {
            return clsSpecializationsData.Update(this.SpecializationID, this.Name);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNew())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _Update();

            }

            return false;
        }

        public static DataTable GetAll()
        {
            return clsSpecializationsData.GetAll();
        }

        public static clsSpecializations Find(int ID)
        {
            string Name = "";

            if (clsSpecializationsData.GetByID(ID, ref Name))
            {
                return new clsSpecializations(ID, Name);
            }
            else
                return null;
        }

        public static bool Delete(int SpecializationID)
        {
            return clsSpecializationsData.Delete(SpecializationID);
        }

    }
}
