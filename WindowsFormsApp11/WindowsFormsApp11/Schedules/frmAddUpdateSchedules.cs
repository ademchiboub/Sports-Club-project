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
    public partial class frmAddUpdateSchedules : Form
    {
        int _ScheduleID;
        clsSchedules _Schedules;
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public frmAddUpdateSchedules()
        {
            InitializeComponent();

            _Schedules = new clsSchedules();
            Mode = enMode.AddNew;
        }

        public frmAddUpdateSchedules(int ScheduleID)
        {
            InitializeComponent();

            _ScheduleID = ScheduleID;
            _Schedules = clsSchedules.Find(_ScheduleID);
            Mode = enMode.Update;
        }



        private void _LoadcbWorkoutName()
        {
            DataTable dtWorkoutsName = clsWorkouts.GetAllWorkoutsName();

            cbWorkout.DataSource = dtWorkoutsName;
            cbWorkout.DisplayMember = "Name";
            cbWorkout.ValueMember = "WorkoutID";

            cbDay.SelectedIndex = 0;
        }


        private void frmAddUpdateSchedules_Load(object sender, EventArgs e)
        {
            _LoadcbWorkoutName();

            if(Mode == enMode.AddNew)
            {
                this.Text = "Add New Schedules";
                lblTitle.Text = "Add New Schedules";
            }
            if(Mode == enMode.Update)
            {
                if(_Schedules != null)
                {
                    this.Text = "Update Schedules";
                    lblTitle.Text = "Update Schedules";

                    lblScheduleID.Text = _Schedules.ScheduleID.ToString();
                    cbWorkout.Name = _Schedules.WorkoutInfo.Name;
                    cbDay.Name = _Schedules.Day;
                    dtStartTime.Value = DateTime.Today + _Schedules.StartTime;
                    dtEndTime.Value = DateTime.Today + _Schedules.EndTime;
                    numMaxMembers.Value = _Schedules.MaxMembers;
                }
            }

           
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
           
            _Schedules.WorkoutID = (int)cbWorkout.SelectedValue;
            _Schedules.Day = cbDay.SelectedItem.ToString();
            _Schedules.StartTime = dtStartTime.Value.TimeOfDay;
            _Schedules.EndTime = dtEndTime.Value.TimeOfDay;
            _Schedules.MaxMembers = (int)numMaxMembers.Value;
            _Schedules.CreateByUserID = clsGlobal.CurrentUser.UserID; 

            if(_Schedules.Mode == 0)
            if(clsSchedules.IsScheduleConflict(_Schedules.WorkoutID, _Schedules.Day, _Schedules.StartTime, _Schedules.EndTime))
            {
                MessageBox.Show("There is a schedule conflict for this workout on the same day.", "Warning", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_Schedules.Save())
            {
                this.Text = "Update Schedules";
                lblTitle.Text = "Update Schedules";
                Mode = enMode.Update;
                lblScheduleID.Text = _Schedules.ScheduleID.ToString();
                MessageBox.Show($"Schedule added successfully. ID = {_Schedules.ScheduleID}");
                
            }
            else
            {
                MessageBox.Show("Failed to add schedule.");
            }
        }
    }
}
