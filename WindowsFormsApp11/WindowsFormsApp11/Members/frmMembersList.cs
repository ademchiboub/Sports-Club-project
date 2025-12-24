using SportsClub_Buisness;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp11
{
    public partial class frmMembersList : Form
    {

        public Action<int> OnSelectMember;
        public frmMembersList()
        {
            InitializeComponent();
        }

        int _DoubleClick;
        public frmMembersList(int DoubleClick)
        {
            InitializeComponent();

            _DoubleClick = DoubleClick;
        }

        private void frmMembersList_Load(object sender, EventArgs e)
        {
            if (_DoubleClick == -1)
            {
                lblDoubleClickInMember.Visible = true;
            }
            LoadMembers();
        }

        private void LoadMembers()
        {
            
                DataTable dt = clsMember.GetAllMembers();

                dgvAllMembers.DataSource = dt;

                // تحديث عدد السجلات
                lblCountRecords.Text = dgvAllMembers.Rows.Count.ToString();

            if(dgvAllMembers.Rows.Count > 0 )
            {
                dgvAllMembers.Columns[0].HeaderText = "Member ID";
                dgvAllMembers.Columns[0].Width = 85;

                dgvAllMembers.Columns[1].Width = 150;
                dgvAllMembers.Columns[1].HeaderText = "Full Name";

                dgvAllMembers.Columns[2].Width = 60;
                dgvAllMembers.Columns[3].Width = 70;
                dgvAllMembers.Columns[4].Width = 90;
                dgvAllMembers.Columns[5].Width = 90;
            }
            
        }

        // ------------------ Add New ------------------
        private void addNewMemberToolStripMenuItem_Click(object sender, EventArgs e)
        {
          


            frmAddUpdateMember frmAdd = new frmAddUpdateMember();
            frmAdd.ShowDialog();

            frmMembersList_Load(null, null);
        }

        // ------------------ Update ------------------
        private void updateMemberToolStripMenuItem_Click(object sender, EventArgs e)
        {

          


            frmAddUpdateMember frm = new frmAddUpdateMember((int)dgvAllMembers.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            frmMembersList_Load(null, null);

        }

        // ------------------ Delete ------------------
        private void deleteMemberToolStripMenuItem_Click(object sender, EventArgs e)
        {


            if (MessageBox.Show("Are you sure you want to delete this member?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if(clsMember.DeleteMember((int)dgvAllMembers.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Delete Member Successfull", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Delete Faild", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            frmMembersList_Load(null, null);
        }

        private void btnAddMember_Click(object sender, EventArgs e)
        {
          

            frmAddUpdateMember frm = new frmAddUpdateMember();
            frm.ShowDialog();
            frmMembersList_Load(null, null);
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilter.Visible = cbFilter.SelectedItem.ToString() != "None";
            txtFilter.Text = ""; // تنظيف البحث عند تغيير الفلتر

            ApplyFilter();
        }
        private void ApplyFilter()
        {
            if (dgvAllMembers.DataSource == null)
                return;

            DataTable dt = (DataTable)dgvAllMembers.DataSource;

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
                    case "Person ID":
                        filterColumn = "MemberID";
                        break;

                    case "Full Name":
                        filterColumn = "FullName";
                        break;

                    case "Status":
                        filterColumn = "Status";
                        break;
                }
                if(cbFilter.SelectedIndex == 1) { dt.DefaultView.RowFilter = $"MemberID = {txtFilter.Text}"; return; }
                // فلترة مثل LIKE %
                dt.DefaultView.RowFilter = string.Format("{0} LIKE '%{1}%'", filterColumn, txtFilter.Text.Replace("'", "''"));
            }

            lblCountRecords.Text = dgvAllMembers.Rows.Count.ToString();
        }
        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilter.SelectedItem.ToString() == "Person ID")
            {
                // فقط أرقام + Backspace
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
                {
                    e.Handled = true;
                }
            }
        }

        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsMember member = clsMember.Find((int)dgvAllMembers.CurrentRow.Cells[0].Value);
            if (member != null)
            {
                frmShowPersonInfo frm = new frmShowPersonInfo(member.PersonInfo.PersonID);
                frm.ShowDialog();
            }
        }

        private void dgvAllMembers_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // معرفة الصف الذي ضغطت عليه
                var hit = dgvAllMembers.HitTest(e.X, e.Y);

                if (hit.RowIndex >= 0)
                {
                    dgvAllMembers.ClearSelection();
                    dgvAllMembers.Rows[hit.RowIndex].Cells[hit.ColumnIndex].Selected = true;

                    // إظهار القائمة اليمنى
                    contextMenuStrip1.Show(dgvAllMembers, e.Location);
                }
            }
        }

       
        private void dgvAllMembers_DoubleClick(object sender, EventArgs e)
        {
            if (OnSelectMember != null)
            {
                lblDoubleClickInMember.Visible = true;
                OnSelectMember?.Invoke((int)dgvAllMembers.CurrentRow.Cells[0].Value);
                this.Close();
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}