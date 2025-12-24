using SportsClub_Buisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp11
{
    public partial class frmListMemberSubscriptions : Form
    {

        public Action<int> OnSelectMemberSubscription;
        int _DoubleClickInMemberSubscription;

        private static DataTable _dtAllMembersSubscription;

        
        public frmListMemberSubscriptions()
        {
            InitializeComponent();
        }

        public frmListMemberSubscriptions(int DoubleClickInMemberSubscription)
        {
            InitializeComponent();

            _DoubleClickInMemberSubscription = DoubleClickInMemberSubscription;

        }

        private void frmListMemberSubscriptions_Load(object sender, EventArgs e)
        {

            if(_DoubleClickInMemberSubscription == -1)
            {
                lblDoubleClick.Visible = true;
            }

            _dtAllMembersSubscription = clsMemberSubscription.GetMemberSubscriptionsOverview();
            dgvMembersSubscriptions.DataSource = _dtAllMembersSubscription;
            cbFilterBy.SelectedIndex = 0;

            lblRecordsCount.Text = dgvMembersSubscriptions.Rows.Count.ToString();

            if (dgvMembersSubscriptions.Rows.Count > 0)
            {

                dgvMembersSubscriptions.Columns[0].Width = 145;
                dgvMembersSubscriptions.Columns[0].HeaderText = "Member Subscription ID";

                dgvMembersSubscriptions.Columns[1].Width = 135;
                dgvMembersSubscriptions.Columns[1].HeaderText = "Full Name";

                dgvMembersSubscriptions.Columns[2].Width = 110;
                dgvMembersSubscriptions.Columns[2].HeaderText = "Member Status";

                dgvMembersSubscriptions.Columns[3].Width = 120;
                dgvMembersSubscriptions.Columns[3].HeaderText = "Subscription Type";

                dgvMembersSubscriptions.Columns[4].HeaderText = "Start Date";
                dgvMembersSubscriptions.Columns[5].HeaderText = "End Date";

                dgvMembersSubscriptions.Columns[6].Width = 125;
                dgvMembersSubscriptions.Columns[6].HeaderText = "Subscription Status";
            }


        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateMemberSubscription frm = new frmAddUpdateMemberSubscription();
            frm.ShowDialog();

            frmListMemberSubscriptions_Load(null, null);
        }

        private void btnAddNewUser_Click(object sender, EventArgs e)
        {
            frmAddUpdateMemberSubscription frm = new frmAddUpdateMemberSubscription();
            frm.ShowDialog();

            frmListMemberSubscriptions_Load(null, null);
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateMemberSubscription frm = new frmAddUpdateMemberSubscription((int)dgvMembersSubscriptions.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

            frmListMemberSubscriptions_Load(null, null);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure do you want to Delete This", "Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (clsMemberSubscription.DeleteMemberSubscription((int)dgvMembersSubscriptions.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Delete Successfully", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Failed Deleted", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            frmListMemberSubscriptions_Load(null, null);
        }


        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            //Map Selected Filter to real Column name 
        
            switch (cbFilterBy.Text)
            {
                case "Member Subscription ID":
                    FilterColumn = "MemberSubscriptionID";
                    break;
              
                case "Subscription Status":
                    FilterColumn = "SubscriptionStatus";
                    break;


                case "Full Name":
                    FilterColumn = "FullName";
                    break;

                default:
                    FilterColumn = "None";
                    break;

            }

            //Reset the filters in case nothing selected or filter value conains nothing.
            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtAllMembersSubscription.DefaultView.RowFilter = "";
                lblRecordsCount.Text = _dtAllMembersSubscription.Rows.Count.ToString();
                return;
            }

            if (FilterColumn != "FullName" && FilterColumn != "SubscriptionStatus")
                //in this case we deal with numbers not string.
                _dtAllMembersSubscription.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
            else
                _dtAllMembersSubscription.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterValue.Text.Trim());

            lblRecordsCount.Text = _dtAllMembersSubscription.Rows.Count.ToString();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "Member Subscription ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
           
                txtFilterValue.Visible = (cbFilterBy.Text != "None");
            
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            
        }

        private void dgvMembersSubscriptions_DoubleClick(object sender, EventArgs e)
        {
            OnSelectMemberSubscription?.Invoke((int)dgvMembersSubscriptions.CurrentRow.Cells[0].Value);
            this.Close();
        }
    }
}
