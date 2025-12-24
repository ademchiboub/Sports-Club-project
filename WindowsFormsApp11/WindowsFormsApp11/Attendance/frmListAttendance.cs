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
    public partial class frmListAttendance : Form
    {
        DataTable _dtAttendance;
        public frmListAttendance()
        {
            InitializeComponent();
        }

        private void frmListAttendance_Load(object sender, EventArgs e)
        {
            _dtAttendance = clsAttendance.GetAttendanceList();
            dgvAllAttendance.DataSource = _dtAttendance;

            lblCountRecords.Text = dgvAllAttendance.Rows.Count.ToString();
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilter.Visible = cbFilter.SelectedItem.ToString() != "None";
            txtFilter.Text = "";
        }

        private string EscapeLikeValue(string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            return value
                .Replace("[", "[[]")
                .Replace("]", "[]]")
                .Replace("*", "[*]")
                .Replace("%", "[%]")
                .Replace("'", "''");
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            if (dgvAllAttendance.DataSource == null)
                return;

            DataTable dt = (DataTable)dgvAllAttendance.DataSource;

            if (cbFilter.SelectedItem == null ||
                cbFilter.SelectedItem.ToString() == "None" ||
                string.IsNullOrWhiteSpace(txtFilter.Text))
            {
                dt.DefaultView.RowFilter = "";
            }
            else
            {
                string filterColumn = cbFilter.SelectedItem.ToString();
                string filterText = EscapeLikeValue(txtFilter.Text);

                dt.DefaultView.RowFilter =
                    $"{filterColumn} LIKE '%{filterText}%'";
            }

            lblCountRecords.Text = dgvAllAttendance.Rows.Count.ToString();
        }
        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateAttendance frm = new frmAddUpdateAttendance();
            frm.ShowDialog();

            frmListAttendance_Load(null, null);
        }
        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateAttendance frm = new frmAddUpdateAttendance((int)dgvAllAttendance.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

            frmListAttendance_Load(null, null);
        }

        private void btnAddMember_Click(object sender, EventArgs e)
        {
            frmAddUpdateAttendance frm = new frmAddUpdateAttendance();
            frm.ShowDialog();

            frmListAttendance_Load(null, null);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            if (MessageBox.Show("Are you sure you want to delete this attendance?","Confirm Delete",MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.No)
                return;

            if (clsAttendance.Delete((int)dgvAllAttendance.CurrentRow.Cells[0].Value))
            {
                MessageBox.Show("Attendance deleted successfully.","Deleted",MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("Attendance not found or already deleted.", "Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }

            frmListAttendance_Load(null, null);
        }
    }
}
