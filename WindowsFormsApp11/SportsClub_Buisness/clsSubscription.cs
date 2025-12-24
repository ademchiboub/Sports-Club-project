using SC_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsClub_Buisness
{
    public class clsSubscription
    {
        enum enMode { AddNew, Update};
        enMode Mode = enMode.AddNew;
        public int SubscriptionID { get; set; }
        public string Type { get; set; }
        public int DurationMonths { get; set; }
        public Decimal Price { get; set; }

        public clsSubscription()
        {
            SubscriptionID=-1;
            Type="";
            DurationMonths=0;
            Price=0;

            Mode = enMode.AddNew;
        }
        public clsSubscription(int subscriptionID, string type, int durationMonths, Decimal price)
        {
            SubscriptionID=subscriptionID;
            Type=type;
            DurationMonths=durationMonths;
            Price=price;

            Mode = enMode.Update;
        }

        public static clsSubscription Find(int  SubscriptionID)
        {
            string type = ""; int durationMonths = 0; Decimal price = 0;

            if (clsSubscriptionData.GetSubscriptionByID(SubscriptionID, ref type, ref durationMonths, ref price))
            {
                return new clsSubscription(SubscriptionID, type, durationMonths, price);
            }
            else
                return null;
        }

        private bool _AddSubscription()
        {
            this.SubscriptionID = clsSubscriptionData.AddSubscription(this.Type, this.DurationMonths, this.Price);

            return (this.SubscriptionID != -1);
        }

        private bool _UpdateSubscription()
        {
            return clsSubscriptionData.UpdateSubscription(this.SubscriptionID, this.Type, this.DurationMonths,this.Price);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddSubscription())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateSubscription();

            }

            return false;
        }

        public static bool DeleteSubscription(int  SubscriptionID)
        {
            return clsSubscriptionData.DeleteSubscription(SubscriptionID);
        }

        public static DataTable GetAllSubscriptions()
        {
            return clsSubscriptionData.GetAllSubscriptions(); 
        }



    }
}
