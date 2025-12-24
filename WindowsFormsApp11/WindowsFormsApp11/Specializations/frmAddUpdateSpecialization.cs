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
    public partial class frmAddUpdateSpecialization : Form
    {
        enum enMode { AddNew, Update};
        enMode Mode = enMode.AddNew;
        int _SpecializationID;

        clsSpecializations _Specialization;

        public frmAddUpdateSpecialization()
        {
            InitializeComponent();

            _Specialization = new clsSpecializations();
            lblTitle.Text = "Add New Specialization";
            this.Text = "Add New Specialization";
            Mode = enMode.AddNew;

        }

        public frmAddUpdateSpecialization(int SpecializationID)
        {
            InitializeComponent();

            _SpecializationID = SpecializationID;
            lblTitle.Text = "Update Specialization";
            this.Text = "Update Specialization";
            _Specialization = clsSpecializations.Find(_SpecializationID);
            Mode = enMode.Update;
        }

        private void frmAddUpdateSpecialization_Load(object sender, EventArgs e)
        {
            if(Mode == enMode.Update)
            {
                lblSpecializationID.Text = _Specialization.SpecializationID.ToString();
                txtSpecialization.Text = _Specialization.Name;

            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //if(Mode == enMode.AddNew)
            //{
                _Specialization.Name = txtSpecialization.Text;

                if(_Specialization.Save())
                {
                    lblSpecializationID.Text = _Specialization.SpecializationID.ToString();
                    lblTitle.Text = "Update Specialization";
                    this.Text = "Update Specialization";
                    Mode = enMode.Update;
                    MessageBox.Show("Add Specialization Successfully", "Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    MessageBox.Show("Add Specialization Failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
          //  }
            //if(Mode == enMode.Update)
            //{
            //    _Specialization.Name = txtSpecialization.Text;
            //    if (_Specialization.Save())
            //    {
            //        lblSpecializationID.Text = _Specialization.SpecializationID.ToString();
            //        lblTitle.Text = "Add New Specialization";
            //        this.Text = "Add New Specialization";
                  
            //        MessageBox.Show("Update Specialization Successfully", "Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        return;
            //    }
            //    else
            //    {
            //        MessageBox.Show("Update Specialization Failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //}

        }
    }
}
