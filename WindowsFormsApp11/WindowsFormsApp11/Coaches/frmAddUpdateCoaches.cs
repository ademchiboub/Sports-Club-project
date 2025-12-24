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
    public partial class frmAddUpdateCoaches : Form
    {
        int _CoacheID;
        clsCoach _Coache;
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public frmAddUpdateCoaches()
        {
            InitializeComponent();

            Mode = enMode.AddNew;
        }

        public frmAddUpdateCoaches(int CoachID)
        {
            InitializeComponent();

            _CoacheID = CoachID;
            _Coache = clsCoach.Find(_CoacheID);
            Mode = enMode.Update;
        }

        private void _LoadSpecialization()
        {
            DataTable dt = clsSpecializations.GetAll();

            cbSpecialization.DataSource = dt;
            cbSpecialization.DisplayMember = "Name";
            cbSpecialization.ValueMember = "SpecializationID";
            cbSpecialization.SelectedIndex = 0;
            //foreach (DataRow dr in dt.Rows)
            //{
            //    cbSpecialization.Items.Add(dr[1]);
            //}
        }

        private void frmAddUpdateCoaches_Load(object sender, EventArgs e)
        {
            _LoadSpecialization();
            if (Mode == enMode.AddNew)
            {
                lblTitle.Text = "Add New Coache";
                this.Text = "Add New Coache";
            }
            else
            {
                lblCoachID.Text = _CoacheID.ToString(); 
                lblTitle.Text = "Update Coache";
                this.Text = "Update Coache";
                ctrlFindAddNewUpdatePerson1.SelectPersonID(_Coache.PersonInfo.PersonID);
                cbSpecialization.Text = clsSpecializations.Find(_Coache.SpecializationID).Name;
            }
        }

        private void frmAddUpdateCoaches_Shown(object sender, EventArgs e)
        {
            ctrlFindAddNewUpdatePerson1.txtFilterFoucs();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int? PersonID = ctrlFindAddNewUpdatePerson1.PersonID;

            if (PersonID != null)
            {
                if (Mode == enMode.AddNew)
                {

                    if(clsCoach.IsCoachExist(PersonID.Value))
                    {
                        MessageBox.Show("Coach Is Exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    _Coache = new clsCoach();
                    _Coache.PersonID = PersonID.Value;
                    _Coache.SpecializationID = (int)cbSpecialization.SelectedValue;

                    if (_Coache.Save())
                    {
                        lblTitle.Text = "Update Coache";
                        this.Text = "Update Coache";
                        Mode = enMode.Update;

                        lblCoachID.Text = _Coache.CoachID.ToString();
                        MessageBox.Show("Coach Save Successfully", "Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Failed to Add Coach", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    return;
                }
            }

            if (Mode == enMode.Update)
            {
                _Coache.SpecializationID = (int)cbSpecialization.SelectedValue;

                if (_Coache.Save())
                {
                    MessageBox.Show("Coach Save Successfully", "Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Failed to Update Coach", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
