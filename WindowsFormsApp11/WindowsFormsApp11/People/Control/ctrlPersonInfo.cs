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
using WindowsFormsApp11.Properties;

namespace WindowsFormsApp11
{
    public partial class ctrlPersonInfo : UserControl
    {
        public clsPerson Person;
        public ctrlPersonInfo()
        {
            InitializeComponent();
        }

        public void ResteScreen()
        {
            lblPersonID.Text = "[?????]";
            lblFullName.Text = "[?????]";
            lblDateOfBirth.Text = "[?????]";
            lblGendor.Text = "[?????]";
            lblPhone.Text = "[?????]";
            lblEmail.Text = "[?????]";
            lblAddress.Text = "[?????]";
            pbPerson.Image = Resources.Male_512;
        }
        public void LoadDataByPersonID(int PersonID)
        {
            Person = clsPerson.Find(PersonID);

            if( Person != null )
            {
                lblPersonID.Text = Person.PersonID.ToString();
                lblFullName.Text = Person.FullName;
                lblDateOfBirth.Text = Person.DateOfBirth.ToShortDateString();
                lblGendor.Text = Person.Gendor;
                lblPhone.Text = Person.Phone;
                lblEmail.Text = Person.Email;
                lblAddress.Text = Person.Address;
                pbPerson.ImageLocation = Person.ImagePath;
            }
            else
            {
                ResteScreen();
                MessageBox.Show("Person Not Found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (Person!= null)
            {
                frmAddUpdatePerson frm = new frmAddUpdatePerson(Person.PersonID);
                frm.DataBackPersonID += DataBackPersonID;
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("There is no one", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DataBackPersonID(int PersonID)
        {
            LoadDataByPersonID(PersonID);   
        }
    }
}
