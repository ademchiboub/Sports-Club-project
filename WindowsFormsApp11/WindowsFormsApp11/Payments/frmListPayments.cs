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
    public partial class frmListPayments : Form
    {
        DataTable _dtPayments;
        public frmListPayments()
        {
            InitializeComponent();
        }

        private void frmListPayments_Load(object sender, EventArgs e)
        {
            cbFilter.SelectedIndex = 0;

            _dtPayments = clsPayment.GetAllPayments();
            dgvAllPayments.DataSource = _dtPayments;
            lblRecordsCount.Text = dgvAllPayments.Rows.Count.ToString();

            if(dgvAllPayments.Rows.Count > 0 )
            {
                dgvAllPayments.Columns[0].HeaderText = "Payment ID";
                dgvAllPayments.Columns[1].HeaderText = "Full Name";
                dgvAllPayments.Columns[2].HeaderText = "Member Subscription ID";
                dgvAllPayments.Columns[4].HeaderText = "Payment Date";
                dgvAllPayments.Columns[6].HeaderText = "CreateByUser ID";

            }
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            if (dgvAllPayments.DataSource == null)
                return;

            DataTable dt = (DataTable)dgvAllPayments.DataSource;

            // لو None → بلا فلترة
            if (cbFilter.SelectedItem.ToString() == "None" || string.IsNullOrWhiteSpace(txtFilter.Text))
            {
                dt.DefaultView.RowFilter = "";
            }
            else
            {
                string filterColumn = "";

                switch (cbFilter.SelectedItem.ToString())
                {
                    case "Payment ID":
                        filterColumn = "PaymentID";
                        break;

                    case "Full Name":
                        filterColumn = "FullName";
                        break;

                    case "Member Subscription ID":
                        filterColumn = "MemberSubscriptionID";
                        break;
                }
                if (cbFilter.SelectedIndex == 1 || cbFilter.SelectedIndex == 3) 
                { dt.DefaultView.RowFilter = $"{filterColumn} = {txtFilter.Text}"; return; }
                // فلترة مثل LIKE %
                dt.DefaultView.RowFilter = string.Format("{0} LIKE '%{1}%'", filterColumn, txtFilter.Text.Replace("'", "''"));
            }

            lblRecordsCount.Text = dgvAllPayments.Rows.Count.ToString();
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilter.SelectedItem.ToString() == "Payment ID" || cbFilter.SelectedItem.ToString() == "Member Subscription ID")
            {
                // فقط أرقام + Backspace
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
                {
                    e.Handled = true;
                }
            }
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilter.Visible = cbFilter.SelectedItem.ToString() != "None";
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdatePayment frm = new frmAddUpdatePayment((int)dgvAllPayments.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

            frmListPayments_Load(null, null);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you sure do you wante to Delete this", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (clsPayment.DeletePayment((int)dgvAllPayments.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Delete Successfully", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Delete Faile", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            frmListPayments_Load(null, null); 
        }
    }
}
