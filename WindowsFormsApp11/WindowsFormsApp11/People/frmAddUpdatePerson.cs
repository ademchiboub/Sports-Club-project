using SportsClub_Buisness;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using WindowsFormsApp11.Properties;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace WindowsFormsApp11
{
    public partial class frmAddUpdatePerson : Form
    {

        public event Action<int> DataBackPersonID;



        clsPerson _Person;
        enum enMode { AddNew, Update };
        enMode Mode = enMode.AddNew;

        public frmAddUpdatePerson()
        {
            InitializeComponent();

            Mode = enMode.AddNew;
            this.Text = "Add New Person";
            lblTitle.Text = "Add New Person";
        }

        public frmAddUpdatePerson(int PersonID)
        {
            InitializeComponent();

            Mode = enMode.Update;
            this.Text = "Update Person";
            lblTitle.Text = "Update Person";
            _Person = clsPerson.Find(PersonID);
        }

        // مجلد الصور داخل البرنامج
        public static string ImagesFolder
        {
            get
            {
                string folder = Path.Combine(Application.StartupPath, "Images");
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);
                return folder;
            }
        }

        private string SaveImageWithUniqueName(string sourceFilePath)
        {
            string ext = Path.GetExtension(sourceFilePath);
            string uniqueFileName = Guid.NewGuid().ToString() + ext;
            string destPath = Path.Combine(ImagesFolder, uniqueFileName);
            File.Copy(sourceFilePath, destPath, true);
            return destPath;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_Person != null)
            {
                DataBackPersonID?.Invoke(_Person.PersonID);  
            }

            this.Close();
        }

        private void _AddNewPerson()
        {
            if (!ValidateInputs()) return;

            //--------------- Is Person Ixist 2 - 3 - 4 Name
            if(clsPerson.IsPersonExistFlexible(txtFirstName.Text, txtSecondName.Text, txtThirdName.Text, txtLastName.Text))
            {
                MessageBox.Show("Person Is Exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //--------------

            _Person = new clsPerson();

            

            FillPersonFromForm(_Person);

            if (_Person.Save())
            {
                lblPersonID.Text = _Person.PersonID.ToString();
                Mode = enMode.Update;
                this.Text = "Update Person";
                lblTitle.Text = "Update Person";

                MessageBox.Show("Data saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Save failed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void _LoadData()
        {
            lblPersonID.Text = _Person.PersonID.ToString();
            txtFirstName.Text = _Person.FirstName;
            txtSecondName.Text = _Person.SecondName;
            txtThirdName.Text = _Person.ThirdName;
            txtLastName.Text = _Person.LastName;
            dtDateOfBirth.Value = _Person.DateOfBirth.Date;
            rbMale.Checked = _Person.Gendor == "Male";
            rbFemale.Checked = _Person.Gendor == "Female";
            txtPhone.Text = _Person.Phone;
            txtEmail.Text = _Person.Email;
            txtAddress.Text = _Person.Address;

            if (!string.IsNullOrEmpty(_Person.ImagePath) && File.Exists(_Person.ImagePath))
            {
                pbImagePath.ImageLocation = _Person.ImagePath;
                pbImagePath.Tag = _Person.ImagePath;
            }
            llRemoveImage.Visible = !string.IsNullOrEmpty(_Person.ImagePath);
        }

        private void frmAddUpdatePerson_Load(object sender, EventArgs e)
        {
            if (Mode == enMode.Update)
                _LoadData();
        }

        private void _UpdateData()
        {
            if (!ValidateInputs()) return;

            FillPersonFromForm(_Person);

            if (_Person.Save())
                MessageBox.Show("Data updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Update failed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void FillPersonFromForm(clsPerson person)
        {
            person.FirstName = txtFirstName.Text;
            person.SecondName = txtSecondName.Text;
            person.ThirdName = txtThirdName.Text;
            person.LastName = txtLastName.Text;
            person.DateOfBirth = dtDateOfBirth.Value.Date;
            person.Gendor = rbMale.Checked ? "Male" : "Female";
            person.Phone = txtPhone.Text;
            person.Email = txtEmail.Text;
            person.Address = txtAddress.Text;

            if (!string.IsNullOrEmpty(pbImagePath.Tag?.ToString()))
                person.ImagePath = pbImagePath.Tag.ToString();
            else
                person.ImagePath = null;

            llRemoveImage.Visible = !string.IsNullOrEmpty(person.ImagePath);
        }

        private bool ValidateInputs()
        {
            bool isValid = true;
            errorProvider1.Clear();

            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                errorProvider1.SetError(txtFirstName, "First Name is required");
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(txtSecondName.Text))
            {
                errorProvider1.SetError(txtSecondName, "Second Name is required");
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                errorProvider1.SetError(txtPhone, "Phone is required");
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(txtAddress.Text))
            {
                errorProvider1.SetError(txtAddress, "Address is required");
                isValid = false;
            }

            string email = txtEmail.Text.Trim();
            if (!string.IsNullOrEmpty(email))
            {
                string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                if (!Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase))
                {
                    errorProvider1.SetError(txtEmail, "Email غير صالح");
                    isValid = false;
                }
            }


            return isValid;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Mode == enMode.AddNew)
                _AddNewPerson();
            else
                _UpdateData();
        }

        // اختيار صورة
        private void llChooseImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Images|*.jpg;*.jpeg;*.png;*.bmp";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string newPath = SaveImageWithUniqueName(ofd.FileName);
                    pbImagePath.ImageLocation = newPath;
                    pbImagePath.Tag = newPath;
                    llRemoveImage.Visible = true;
                }
            }
        }

        // حذف صورة
        private void llRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string path = pbImagePath.ImageLocation;
            if (!string.IsNullOrEmpty(path) && File.Exists(path))
                File.Delete(path);

            pbImagePath.Image = null;
            pbImagePath.ImageLocation = null;
            pbImagePath.Tag = null;
            llRemoveImage.Visible = false;
        }

        // التحكم بـ txtPhone لقبول أرقام فقط
        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {
            pbImagePath.Image = Resources.Male_512;
        }

        private void rbFemale_CheckedChanged(object sender, EventArgs e)
        {
            pbImagePath.Image = Resources.Female_512;
        }
    }
}