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
    public partial class frmSubscriptionType : Form
    {
        public frmSubscriptionType()
        {
            InitializeComponent();
        }

        private void frmSubscriptionType_Load(object sender, EventArgs e)
        {
            dgvSubscriptionType.DataSource = clsSubscription.GetAllSubscriptions();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateSubscription frm = new frmAddUpdateSubscription();
            frm.ShowDialog();

            frmSubscriptionType_Load(null, null);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateSubscription frm = new frmAddUpdateSubscription((int)dgvSubscriptionType.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

            frmSubscriptionType_Load(null, null);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you sure do you want to Delete This", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (clsSubscription.DeleteSubscription((int)dgvSubscriptionType.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Delete Successfully", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Faile Delete", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            frmSubscriptionType_Load(null, null);

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAddUpdateSubscription frm = new frmAddUpdateSubscription();
            frm.ShowDialog();

            frmSubscriptionType_Load(null, null);
        }
    }
}
