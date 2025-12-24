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
    public partial class frmCoachesList : Form
    {
        clsCoach _Coach;
        DataTable _dtCoaches;

        public Action<int, string, string> CoachSelected;
        public frmCoachesList()
        {
            InitializeComponent();
        }

        public frmCoachesList(int number)
        {
            InitializeComponent();

            lblVisableChooseCoach.Visible = true;
        }

        private void frmCoachesList_Load(object sender, EventArgs e)
        {
            _dtCoaches = clsCoach.GetAllCoaches();
            dgvAllCoaches.DataSource = _dtCoaches;
            cbFilter.SelectedIndex = 0;

            if (dgvAllCoaches.Rows.Count > 0)
            {
                dgvAllCoaches.Columns[0].HeaderText = "Coach ID";

                dgvAllCoaches.Columns[1].HeaderText = "Full Name";
                dgvAllCoaches.Columns[1].Width = 205;

                dgvAllCoaches.Columns[2].Width = 200;

                dgvAllCoaches.Columns[3].HeaderText = "Total Work Outs";
                dgvAllCoaches.Columns[3].Width = 110;
            }

            lblCountRecords.Text = dgvAllCoaches.Rows.Count.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAddUpdateCoaches frm = new frmAddUpdateCoaches();
            frm.ShowDialog();

            frmCoachesList_Load(null, null);
        }

        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowPersonInfo frm = new frmShowPersonInfo(clsCoach.Find((int)dgvAllCoaches.CurrentRow.Cells[0].Value).PersonID);
            frm.ShowDialog();
        }

        private void addNewMemberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateCoaches frm = new frmAddUpdateCoaches();
            frm.ShowDialog();

            frmCoachesList_Load(null, null);
        }

        private void updateMemberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateCoaches frm = new frmAddUpdateCoaches((int)dgvAllCoaches.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

            frmCoachesList_Load(null, null);
        }

        private void deleteMemberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this Coach?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (clsCoach.DeleteCoach((int)dgvAllCoaches.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Delete Coach Successfull", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Delete Faild", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            frmCoachesList_Load(null, null);
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilter.Text == "None")
            {
                txtFilter.Visible = false;
            }
            else
            {
                txtFilter.Visible = true;
            }
        }
        // الدالة المساعدة: تهرب الأحرف الخاصة لـ RowFilter/LIKE
        private string EscapeForRowFilter(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            // 1) مضاعفة single-quote لأن القيمة داخل '' في التعبير
            string s = input.Replace("'", "''");

            // 2) هروب الأحرف الخاصة في نمط LIKE بوضعها داخل أقواس [...]
            //    [  -> [[]    ] -> []]    % -> [%]    * -> [*]    _ -> [_]
            // ترتيب الاستبدالات مهم (أولًا أقواس لتجنب تعارض)
            s = s.Replace("[", "[[]");
            s = s.Replace("]", "[]]");
            s = s.Replace("%", "[%]");
            s = s.Replace("*", "[*]");
            s = s.Replace("_", "[_]");

            return s;
        }
        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            if (_dtCoaches == null)
                return;

            string filterValue = txtFilter.Text.Trim();

            if (string.IsNullOrEmpty(filterValue))
            {
                _dtCoaches.DefaultView.RowFilter = "";
                lblCountRecords.Text = dgvAllCoaches.RowCount.ToString();
                return;
            }


            string column = "";
            switch (cbFilter.Text)
            {
                case "Coach ID":
                    column = "CoachID";
                    break;
                case "Full Name":
                    column = "FullName";
                    break;
                case "Specialization":
                    column = "Specialization";
                    break;
                case "Status":
                    column = "Status";
                    break;
                default:
                    return;
            }

            if (column == "CoachID")
            {
                _dtCoaches.DefaultView.RowFilter = $"{column} = {filterValue}";
            }
            else
            {
                // نهرب المدخل قبل وضعه في LIKE
                string safe = EscapeForRowFilter(filterValue);
                _dtCoaches.DefaultView.RowFilter = $"{column} LIKE '%{safe}%'";
            }
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilter.Text != "Coach ID")
                return;

            // الأرقام مسموحة
            if (char.IsDigit(e.KeyChar))
                return;

            // Backspace مسموح
            if (e.KeyChar == (char)Keys.Back)
                return;

            // غير ذلك = منع الإدخال
            e.Handled = true;
        }

        private void dgvAllCoaches_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int coachID = Convert.ToInt32(dgvAllCoaches.Rows[e.RowIndex].Cells["CoachID"].Value);
                string coachName = dgvAllCoaches.Rows[e.RowIndex].Cells["FullName"].Value.ToString();
                string Specialization = dgvAllCoaches.Rows[e.RowIndex].Cells["SpecializationName"].Value.ToString();

                // نرسل القيمة للشاشة السابقة
                CoachSelected?.Invoke(coachID, coachName, Specialization);

                this.Close(); // إغلاق الفورم
            }
        }
    }
}
