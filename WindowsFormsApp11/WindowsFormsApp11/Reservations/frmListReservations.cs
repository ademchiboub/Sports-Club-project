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
    public partial class frmListReservations : Form
    {

        public Action<int> OnSelectReservation;

        DataTable _dtAllReservation;
        public frmListReservations()
        {
            InitializeComponent();
        }

        public frmListReservations(int DoubleClick)
        {
            InitializeComponent();

            lblChoose.Visible = true;
        }

        private void frmListReservations_Load(object sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex = 0;
            _dtAllReservation =  clsReservation.GetAllReservations();
            dgvAllReservations.DataSource = _dtAllReservation;
            lblRecordsCount.Text = dgvAllReservations.Rows.Count.ToString();

            if(dgvAllReservations.Rows.Count > 0 )
            {
                dgvAllReservations.Columns[0].Width = 110;
                dgvAllReservations.Columns[5].Width = 110;
                dgvAllReservations.Columns[7].Width = 120;
            }


        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = (cbFilterBy.Text != "None");
           
            txtFilterValue.Text = "";
            txtFilterValue.Focus();
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string filterColumn = cbFilterBy.SelectedItem?.ToString() ?? "None";
            string filterValue = txtFilterValue.Text.Trim();

            if (string.IsNullOrEmpty(filterValue) || filterColumn == "None")
            {
                // إزالة أي فلتر إذا القيمة فارغة أو "None"
                _dtAllReservation.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvAllReservations.Rows.Count.ToString();
                return;
            }

            try
            {
                if (filterColumn == "Reservation ID") // عمود رقمي
                {
                    if (int.TryParse(filterValue, out int idValue))
                    {
                        _dtAllReservation.DefaultView.RowFilter = $"[{filterColumn}] = {idValue}";
                    }
                    else
                    {
                        // إذا المدخل غير صالح، أفرغ الفلتر
                        _dtAllReservation.DefaultView.RowFilter = "";
                    }
                }
                else // عمود نصي
                {
                    // تجنب مشاكل مع علامات الاقتباس
                    string safeValue = filterValue.Replace("'", "''");
                    _dtAllReservation.DefaultView.RowFilter = $"[{filterColumn}] LIKE '{safeValue}%'";
                }
            }
            catch
            {
                // في حال حصل خطأ غير متوقع، فقط أفرغ الفلتر
                _dtAllReservation.DefaultView.RowFilter = "";
            }

            // تحديث عدد السجلات
            lblRecordsCount.Text = dgvAllReservations.Rows.Count.ToString();
        }
        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "Reservation ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void btnAddNewUser_Click(object sender, EventArgs e)
        {
            frmAddUpdateReservation frm = new frmAddUpdateReservation();
            frm.ShowDialog();

            frmListReservations_Load(null, null);
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateReservation frm = new frmAddUpdateReservation();
            frm.ShowDialog();

            frmListReservations_Load(null, null);
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateReservation frm = new frmAddUpdateReservation((int)dgvAllReservations.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

            frmListReservations_Load(null, null);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this reservation?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string msg;

                if (clsReservation.DeleteReservation(
                    (int)dgvAllReservations.CurrentRow.Cells[0].Value, out msg))
                {
                    MessageBox.Show("Reservation deleted successfully.",
                        "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(msg,
                        "Delete Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            // Reload grid
            frmListReservations_Load(null, null);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
         
        }

        private void dgvAllReservations_DoubleClick(object sender, EventArgs e)
        {
            if (OnSelectReservation != null)
            {
                OnSelectReservation?.Invoke((int)dgvAllReservations.CurrentRow.Cells[0].Value);
                this.Close();
            }
        }
    }


}
