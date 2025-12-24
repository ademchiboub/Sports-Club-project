using SportsClub_Buisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp11
{
    public partial class frmAddUpdateMemberSubscription : Form
    {
        enum enMode { AddNew, Update};
        enMode Mode = enMode.AddNew;

        clsMemberSubscription _MemberSubscription;
        int _MemberSubscriptionID;

        clsPayment _Payment;
        int _PaymentID;
        public frmAddUpdateMemberSubscription()
        {
            InitializeComponent();

            
            _MemberSubscription = new clsMemberSubscription();
            _Payment = new clsPayment();
            lblTitle.Text = "Add New Member Subscription";
            this.Text = "Add New Member Subscription";

            Mode = enMode.AddNew;
        }

        public frmAddUpdateMemberSubscription(int MemberSubscriptionID)
        {
            InitializeComponent();

            _MemberSubscriptionID = MemberSubscriptionID;
            lblTitle.Text = "Update Member Subscription";
            this.Text = "Update Member Subscription";
            _MemberSubscription =  clsMemberSubscription.Find(_MemberSubscriptionID);

            Mode = enMode.Update;
        }

        private void _LoadAllComboBox()
        {

            cbMethod.SelectedIndex=0;
            DataTable dtt = clsSubscription.GetAllSubscriptions(); // SP لجلب كل الاشتراكات
            cbSubscriptions.DataSource = dtt;
            cbSubscriptions.DisplayMember = "Type";
            cbSubscriptions.ValueMember = "SubscriptionID";
            cbSubscriptions.DropDownStyle = ComboBoxStyle.DropDownList;

            cbStatus.Items.Clear();
            cbStatus.Items.Add("Active");
            cbStatus.Items.Add("Expired");
            cbStatus.Items.Add("Canceled");
            cbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cbStatus.SelectedIndex = 0;
        }

        private void frmAddUpdateMemberSubscription_Load(object sender, EventArgs e)
        {
            _LoadAllComboBox();
            lblCreateByUser.Text = clsGlobal.CurrentUser.Username + " ID: " + clsGlobal.CurrentUser.UserID;

            if(Mode == enMode.Update)
            {

                lblMemberSubscriptionID.Text = _MemberSubscription.MemberSubscriptionID.ToString();

            }
        }

        private void btnSearchMember_Click(object sender, EventArgs e)
        {
            frmMembersList frm = new frmMembersList(-1);
            frm.OnSelectMember += OnMemberSelected;
            frm.ShowDialog();
        }

        private void OnMemberSelected(int MemberID)
        {
            lblMemberID.Text = MemberID.ToString();
        }



        private bool InputeValidate()
{
    // التحقق من اختيار العضو والجدول
    if (lblMemberID.Text == "???" )
    {
        MessageBox.Show(
            "Please populate the required data.", 
            "Error", 
            MessageBoxButtons.OK, 
            MessageBoxIcon.Error
        );
        return true; // خطأ، يمنع الحفظ
    }

    // التحقق من السعر
    decimal lblPriceValue = 0;
    decimal txtPriceValue = 0;

    // محاولة تحويل القيم للتأكد من أنها أرقام
    decimal.TryParse(lblPrice.Text, System.Globalization.NumberStyles.Currency, null, out lblPriceValue);
    decimal.TryParse(txtPrice.Text, out txtPriceValue);

    if (lblPriceValue != txtPriceValue)
    {
        MessageBox.Show(
            "The entered price does not match the expected subscription price.", 
            "Invalid Price", 
            MessageBoxButtons.OK, 
            MessageBoxIcon.Warning
        );
        return true; // يمنع الحفظ
    }

    // كل شيء صحيح
    return false;
}

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (InputeValidate())
                return;
            
            _MemberSubscription.MemberID = Convert.ToInt32(lblMemberID.Text);
            _MemberSubscription.SubscriptionID = Convert.ToInt32(cbSubscriptions.SelectedValue);
            _MemberSubscription.StartDate = dtpStart.Value;
            _MemberSubscription.EndDate = dtpEnd.Value;
            _MemberSubscription.Status = cbStatus.SelectedItem.ToString();
            _MemberSubscription.CreateByUserID = clsGlobal.CurrentUser.UserID;

            if (_MemberSubscription.EndDate <= _MemberSubscription.StartDate)
            {
                MessageBox.Show( "End date must be greater than start date.","Invalid Date", MessageBoxButtons.OK,
                                       MessageBoxIcon.Error);
                return;
            }

            if (clsMemberSubscription.IsMemberSubscriptionConflict(_MemberSubscription.MemberID, _MemberSubscription.StartDate,
               _MemberSubscription.EndDate))
            {
                MessageBox.Show("This member already has an active subscription during this period.", "Subscription Conflict",
                      MessageBoxButtons.OK, MessageBoxIcon.Warning);
               return;
            }


            if (_MemberSubscription.Save())
            {


                if(Mode == enMode.AddNew)
                {
                    _Payment.MemberSubscriptionID = _MemberSubscription.MemberSubscriptionID;
                    _Payment.Amount = Convert.ToDecimal(txtPrice.Text);
                    _Payment.PaymentDate = _MemberSubscription.StartDate;
                    _Payment.Method = cbMethod.SelectedItem.ToString();
                    _Payment.CreateByUserID = _MemberSubscription.CreateByUserID;

                    _Payment.Save();
                }

                lblPrice.Enabled = false;
                txtPrice.Enabled = false;
                cbMethod.Enabled = false;

                lblMemberSubscriptionID.Text = _MemberSubscription.MemberSubscriptionID.ToString();
                Mode = enMode.Update;
                lblTitle.Text = "Update Member Subscription";
                this.Text = "Update Member Subscription";

                MessageBox.Show("Successfully", "Effected", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

     


        private void cbSubscriptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbSubscriptions.SelectedIndex == -1)
                return;

            DataRowView selectedRow = cbSubscriptions.SelectedItem as DataRowView;
            if (selectedRow == null)
                return;

            // السعر
            decimal price = Convert.ToDecimal(selectedRow["Price"]);
            lblPrice.Text = price.ToString("C");

            // التاريخ
            DateTime startDate = DateTime.Now;
            DateTime endDate = startDate;

            string subscriptionName = selectedRow["Type"].ToString();

            switch (subscriptionName)
            {
                case "Monthly":
                case "Basic Monthly":
                case "Gym Only":
                    endDate = startDate.AddMonths(1);
                    break;

                case "Standard Quarterly":
                    endDate = startDate.AddMonths(3);
                    break;

                case "Premium Annual":
                case "VIP All Access":
                    endDate = startDate.AddYears(1);
                    break;
            }

            dtpStart.Value = startDate;
            dtpEnd.Value   = endDate;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

       
    }
}
