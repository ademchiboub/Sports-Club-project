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
    public partial class ctrlFindAddNewUpdatePerson : UserControl
    {


        public int? PersonID = null;
        public void txtFilterFoucs()
        {
            txtPersonID.Focus();
        }
        public ctrlFindAddNewUpdatePerson()
        {
            InitializeComponent();
        }


        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; 
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                btnResearch.PerformClick();
                e.SuppressKeyPress = true;
            }
        }

        private void btnResearch_Click(object sender, EventArgs e)
        {
            int.TryParse(txtPersonID.Text, out int personID);
            ctrlPersonInfo1.LoadDataByPersonID(personID);

            if(ctrlPersonInfo1.Person != null)
            {
                PersonID = personID;
            }
            else
            {
                PersonID = null;
            }

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAddUpdatePerson frm = new frmAddUpdatePerson();
            frm.DataBackPersonID += DataBackPersonID;
            frm.ShowDialog();
        }

        private void DataBackPersonID(int PersonID)
        {
            ctrlPersonInfo1.LoadDataByPersonID(PersonID);

        }

        public void SelectPersonID(int PersonID)
        {
            txtPersonID.Text = PersonID.ToString();
            this.PersonID = PersonID;
            ctrlPersonInfo1.LoadDataByPersonID(PersonID);
            txtPersonID.Enabled = false;
            btnAdd.Enabled = false;
            btnResearch.Enabled = false;
        }
    }
}
