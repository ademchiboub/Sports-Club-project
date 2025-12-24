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
    public partial class frmAddUpdatePayment : Form
    {

        clsPayment _Payment;
        public frmAddUpdatePayment(int PaymentID)
        {
            InitializeComponent();

            _Payment = clsPayment.Find(PaymentID);
        }

        private void frmAddUpdatePayment_Load(object sender, EventArgs e)
        {
            lblPaymentID.Text = _Payment.PaymentID.ToString();
            lblMemberSubscriptionID.Text = _Payment.MemberSubscriptionID.ToString();
            txtAmount.Text = _Payment.Amount.ToString();
            dtpDate.Value = _Payment.PaymentDate;
            cbMethod.Text = _Payment.Method;
            lblCreateByUserID.Text = _Payment.CreateByUserID.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmListMemberSubscriptions frm = new frmListMemberSubscriptions(-1);
            frm.OnSelectMemberSubscription += OnMemberSubscriptionSelected;
            frm.ShowDialog();
        }

        private void OnMemberSubscriptionSelected(int MemberID)
        {
            lblMemberSubscriptionID.Text = MemberID.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            _Payment.MemberSubscriptionID = Convert.ToInt32(lblMemberSubscriptionID.Text);
            _Payment.Amount = Convert.ToDecimal(txtAmount.Text);
            _Payment.PaymentDate = dtpDate.Value;
            _Payment.Method = cbMethod.SelectedItem.ToString();
            
            if(_Payment.Save())
            {
                MessageBox.Show("Update Payment Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Update Payment Faile", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
