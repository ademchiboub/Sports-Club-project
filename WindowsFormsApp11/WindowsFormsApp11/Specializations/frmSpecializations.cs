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
    public partial class frmSpecializations : Form
    {
        public frmSpecializations()
        {
            InitializeComponent();
        }



        private void frmSpecializations_Load(object sender, EventArgs e)
        {
            dgvSpecialzations.DataSource = clsSpecializations.GetAll();

            if(dgvSpecialzations.Rows.Count > 0 )
            {
                dgvSpecialzations.Columns[1].Width = 150;
            }
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateSpecialization frm = new frmAddUpdateSpecialization();
            frm.ShowDialog();

            frmSpecializations_Load(null, null);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateSpecialization frm = new frmAddUpdateSpecialization((int)dgvSpecialzations.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

            frmSpecializations_Load(null, null);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you sure do you want to Delete This", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (clsSpecializations.Delete((int)dgvSpecialzations.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Delete Successfully", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Failed Deleted", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            frmSpecializations_Load(null, null);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAddUpdateSpecialization frm = new frmAddUpdateSpecialization();
            frm.ShowDialog();

            frmSpecializations_Load(null, null);
        }
    }
}
