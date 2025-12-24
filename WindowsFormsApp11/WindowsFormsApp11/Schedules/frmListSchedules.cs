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
    public partial class frmListSchedules : Form
    {

        public Action<int> OnSelectSchedule;
        int _DoubleClickInSchedule;

        clsSchedules _Schedules;
        DataTable _dtSchedules;
        public frmListSchedules()
        {
            InitializeComponent();
        }

        public frmListSchedules(int DoubleClickInSchedule)
        {
            InitializeComponent();

            _DoubleClickInSchedule = DoubleClickInSchedule;
        }

        private void frmListSchedules_Load(object sender, EventArgs e)
        {

            if(_DoubleClickInSchedule == -1)
            {
                lblDoubleClick.Visible = true;
            }

            _dtSchedules = clsSchedules.GetAllSchedules();
            dgvSchedules.DataSource = _dtSchedules;
            cbFilter.SelectedIndex = 0;
            lblCountRecords.Text = dgvSchedules.Rows.Count.ToString();

            if(dgvSchedules.Rows.Count > 0 )
            {
                dgvSchedules.Columns[0].HeaderText = "Schedule ID";

                dgvSchedules.Columns[1].HeaderText = "Workout Name";

                dgvSchedules.Columns[3].HeaderText = "Start Time";
                dgvSchedules.Columns[4].HeaderText = "End Time";

                dgvSchedules.Columns[5].HeaderText = "Max Members";

                dgvSchedules.Columns[6].HeaderText = "Coach FullName";
                dgvSchedules.Columns[6].Width = 115;
            }
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
            if (_dtSchedules == null)
                return;

            string filterValue = txtFilter.Text.Trim();

            if (string.IsNullOrEmpty(filterValue))
            {
                _dtSchedules.DefaultView.RowFilter = "";
                lblCountRecords.Text = dgvSchedules.RowCount.ToString();
                return;
            }


            string column = "";
            switch (cbFilter.Text)
            {
                case "Schedule ID":
                    column = "ScheduleID";
                    break;
                case "Workout Name":
                    column = "WorkoutName";
                    break;
                case "Coach FullName":
                    column = "CoachFullName";
                    break;
                default:
                    return;
            }

            if (column == "ScheduleID")
            {
                _dtSchedules.DefaultView.RowFilter = $"{column} = {filterValue}";
            }
            else
            {
                // نهرب المدخل قبل وضعه في LIKE
                string safe = EscapeForRowFilter(filterValue);
                _dtSchedules.DefaultView.RowFilter = $"{column} LIKE '%{safe}%'";
            }
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilter.Text != "Schedule ID")
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAddUpdateSchedules frm = new frmAddUpdateSchedules();
            frm.ShowDialog();

            frmListSchedules_Load(null, null);
        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateSchedules frm = new frmAddUpdateSchedules();
            frm.ShowDialog();

            frmListSchedules_Load(null, null);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateSchedules frm = new frmAddUpdateSchedules((int)dgvSchedules.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

            frmListSchedules_Load(null, null);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            if (clsSchedules.DeleteSchedule((int)dgvSchedules.CurrentRow.Cells[0].Value))
            {
                MessageBox.Show("Schedule has been deleted successfully", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmListSchedules_Load(null, null);
            }

            else
                MessageBox.Show("Schedule is not delted due to data connected to it.", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void dgvSchedules_DoubleClick(object sender, EventArgs e)
        {
            OnSelectSchedule?.Invoke((int)dgvSchedules.CurrentRow.Cells[0].Value);
            this.Close();
        }
    }
}
