using SportsClub_Buisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp11
{
    public partial class frmAddUpdateSubscription : Form
    {
        enum enMode { AddNew, Update};
        enMode Mode = enMode.AddNew;

        clsSubscription _Subscription;
        int _SubscriptionID;
        public frmAddUpdateSubscription()
        {
            InitializeComponent();

            lblTitle.Text = "Add New Subscription";
            this.Text = "Add New Subscription";
            _Subscription = new clsSubscription();

            Mode = enMode.AddNew;
        }

        public frmAddUpdateSubscription(int SubscriptionTypeID)
        {
            InitializeComponent();

            _SubscriptionID = SubscriptionTypeID;
            _Subscription = clsSubscription.Find(_SubscriptionID);
            lblTitle.Text = "Update Subscription";
            this.Text = "Update Subscription";

            Mode = enMode.Update;
        }

        private void frmAddUpdateSubscription_Load(object sender, EventArgs e)
        {
            if (Mode == enMode.Update)
            {
                lblSubscriptionID.Text = _Subscription.SubscriptionID.ToString();
                txtType.Text = _Subscription.Type;
                nudDurationMonthly.Value = _Subscription.DurationMonths;
                txtPrice.Text = _Subscription.Price.ToString();
            }
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _Subscription.Type = txtType.Text;
            _Subscription.DurationMonths = (int)nudDurationMonthly.Value;
            _Subscription.Price = Convert.ToDecimal(txtPrice.Text);

            if(_Subscription.Save())
            {
                MessageBox.Show("Save Successfully", "Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Save Failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
