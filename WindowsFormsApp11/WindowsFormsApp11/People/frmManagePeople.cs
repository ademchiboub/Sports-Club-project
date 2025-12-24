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
    public partial class frmManagePeople : Form
    {

        DataTable _dtPeople;

        public frmManagePeople()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void button1_Click(object sender, EventArgs e)
        {
            frmAddUpdatePerson frm = new frmAddUpdatePerson();
            frm.ShowDialog();

            frmManagePeople_Load(null, null);
        }

        private void frmManagePeople_Load(object sender, EventArgs e)
        {
            _dtPeople = clsPerson.GetAllPeople();
            dgvAllPeople.DataSource = _dtPeople;
            lblCountRecords.Text = dgvAllPeople.RowCount.ToString();
            cbFilter.SelectedIndex = 0;

            if(dgvAllPeople.Rows.Count > 0 )
            {
                dgvAllPeople.Columns[0].HeaderText = "Person ID";
                dgvAllPeople.Columns[0].Width = 80;

                dgvAllPeople.Columns[1].HeaderText = "Full Name";
                dgvAllPeople.Columns[1].Width = 200;

                dgvAllPeople.Columns[2].HeaderText = "Date Of Birth";

                dgvAllPeople.Columns[3].Width = 70;
                dgvAllPeople.Columns[4].Width = 80;
            }
            
        }

        private void dgvAllPeople_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                var hit = dgvAllPeople.HitTest(e.X, e.Y);
                if(hit.RowIndex >= 0)
                {
                    dgvAllPeople.ClearSelection();
                    dgvAllPeople.Rows[hit.RowIndex].Cells[hit.ColumnIndex].Selected = true;
                    dgvAllPeople.CurrentCell = dgvAllPeople.Rows[hit.RowIndex].Cells[0];
                    
                }
            }
        }

        private void showPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowPersonInfo frm = new frmShowPersonInfo((int)dgvAllPeople.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdatePerson frm = new frmAddUpdatePerson();
            frm.ShowDialog();
            frmManagePeople_Load(null, null);
        }

        private void updatePersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdatePerson frm = new frmAddUpdatePerson((int)dgvAllPeople.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            frmManagePeople_Load(null, null);
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
            if (_dtPeople == null)
                return;

            string filterValue = txtFilter.Text.Trim();

            if (string.IsNullOrEmpty(filterValue))
            {
                _dtPeople.DefaultView.RowFilter = "";
                lblCountRecords.Text = dgvAllPeople.RowCount.ToString();
                return;
            }

            string column = "";
            switch (cbFilter.Text)
            {
                case "Person ID":
                    column = "PersonID";
                    break;
                case "Full Name":
                    column = "FullName";
                    break;
                case "Gendor":
                    column = "Gendor";
                    break;
                case "Email":
                    column = "Email";
                    break;
                case "Phone":
                    column = "Phone";
                    break;
                case "Address":
                    column = "Address";
                    break;
                default:
                    return;
            }

            try
            {
                if (column == "PersonID")
                {
                    // تأكد من أن المستخدم أدخل رقم صحيح
                    if (int.TryParse(filterValue, out int id))
                        _dtPeople.DefaultView.RowFilter = $"{column} = {id}";
                    else
                        _dtPeople.DefaultView.RowFilter = "1 = 0"; // لا تظهر نتائج إذا لم يكن رقماً
                }
                else
                {
                    // نهرب المدخل قبل وضعه في LIKE
                    string safe = EscapeForRowFilter(filterValue);
                    _dtPeople.DefaultView.RowFilter = $"{column} LIKE '%{safe}%'";
                }
            }
            catch (Exception ex)
            {
                // في حالة خطأ غير متوقع سنمسح الفلتر بدلاً من رمي استثناء للمستخدم
                _dtPeople.DefaultView.RowFilter = "";
                // اختياري: تعرض رسالة للمطور أو سجلها
                // MessageBox.Show("Filter error: " + ex.Message);
            }

            lblCountRecords.Text = dgvAllPeople.RowCount.ToString();
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            // إذا الفلتر ليس Person ID، اقبل كل شيء
            if (cbFilter.Text != "Person ID")
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

        private void deletePersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are yousure do you wante delete this Person", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if(clsPerson.DeletePerson((int)dgvAllPeople.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Delete Successfully", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Delete Filed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            frmManagePeople_Load(null, null);
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbFilter.Text == "None")
            {
                txtFilter.Visible = false;
            }
            else
            {
                txtFilter.Visible = true;
            }
        }
    }
}
