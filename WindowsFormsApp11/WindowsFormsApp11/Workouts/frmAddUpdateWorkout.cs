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
    public partial class frmAddUpdateWorkout : Form
    {

        enum enMode { AddNew = 0, Update = 1 }
        enMode _Mode = enMode.AddNew;

        clsWorkouts _Workout;
        int _WorkoutID; 
        public frmAddUpdateWorkout()
        {
            InitializeComponent();

            _Workout = new clsWorkouts();
            this.Text = "Add New Workout";
            lblTitle.Text = "Add New Workout";

            _Mode = enMode.AddNew;

        }

        public frmAddUpdateWorkout(int WorkoutID)
        {
            InitializeComponent();

            _Workout = clsWorkouts.Find(WorkoutID);
            _WorkoutID = WorkoutID;
            this.Text = "Update Workout";
            lblTitle.Text = "Update Workout";

            _Mode = enMode.Update;

        }




        private void btnChooseCoach_Click(object sender, EventArgs e)
        {
            frmCoachesList frm = new frmCoachesList(-1);
            frm.CoachSelected += DataBackCoachIDAndFullName;
            frm.ShowDialog();
        }

        private void DataBackCoachIDAndFullName(int CoachID,  string FullName, string Specialization)
        {
            lblError.Visible = false;
            lblCoachID.Text = CoachID.ToString();
            lblFullName.Text = FullName;
            lblSpecialization.Text = Specialization;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool ValidateInputs()
        {
            bool isValid = true;

            errorProvider1.Clear();

            // تحقق من CoachID
            if(lblCoachID.Text == "??")
            {
                lblError.Visible = true;
                isValid = false;
            }


            // تحقق من Name
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                errorProvider1.SetError(txtName, "Name is required.");
                isValid = false;
            }

            return isValid;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs())
                return;

            if (_Mode == enMode.AddNew)
            {
                _Workout.CoachID = int.Parse(lblCoachID.Text);



                _Workout.Name = txtName.Text;
                _Workout.Description = txtDescription.Text;

                if(clsWorkouts.IsWorkoutExist(_Workout.CoachID, _Workout.Name, _Workout.Description))
                {
                    MessageBox.Show("Workouts Coach and Specialization and Description Is Exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (_Workout.Save())
                {
                    this.Text = "Update Workout";
                    lblTitle.Text = "Update Workout";
                    _Mode = enMode.Update;
                    lblWorkoutID.Text = _Workout.WorkoutID.ToString();
                    MessageBox.Show("Add Workout Successfully", "Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    MessageBox.Show("Add Workout Failed", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (_Mode == enMode.Update)
            {
                _Workout.Name = txtName.Text;
                _Workout.Description = txtDescription.Text;

                if(_Workout.Save())
                {
                    MessageBox.Show("Update Workout Successfully", "Successfull", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Update Workout Failed", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void frmAddUpdateWorkout_Load(object sender, EventArgs e)
        {
            if (_Mode == enMode.Update)
            {
                if (_Workout != null)
                {
                    lblWorkoutID.Text = _Workout.WorkoutID.ToString();
                    lblCoachID.Text = _Workout.CoachID.ToString();
                    lblFullName.Text = _Workout.CoachInfo.PersonInfo.FullName;
                    lblSpecialization.Text = clsSpecializations.Find(_Workout.CoachInfo.SpecializationID).Name;
                    txtName.Text = _Workout.Name;
                    txtDescription.Text = _Workout.Description;
                }
                else
                {
                    MessageBox.Show($"Workout ID: {_WorkoutID} Not Found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
