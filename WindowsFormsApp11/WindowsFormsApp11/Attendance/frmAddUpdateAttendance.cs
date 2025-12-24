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
using static SportsClub_Buisness.clsAttendance;

namespace WindowsFormsApp11
{
    public partial class frmAddUpdateAttendance : Form
    {

        enum enMode { AddNew=0, Update = 1}
        enMode Mode = enMode.AddNew;

        clsAttendance _Attendance;
        int _AttendaceID;
        public frmAddUpdateAttendance()
        {
            InitializeComponent();

            _Attendance = new clsAttendance();

            lblTitle.Text = "Add New Attendance";
            this.Text = "Add New Attendance";
            dtpCheckOut.Enabled = false;

            Mode = enMode.AddNew;
        }

        public frmAddUpdateAttendance(int AttendaceID)
        {
            InitializeComponent();

            _AttendaceID = AttendaceID;
            _Attendance = clsAttendance.Find(_AttendaceID);
            lblTitle.Text = "Update Attendance";
            this.Text = "Update Attendance";
            dtpCheckIn.Enabled = false;

            Mode = enMode.Update;
        }

        private void frmAddUpdateAttendance_Load(object sender, EventArgs e)
        {
            if(Mode == enMode.Update)
            {
                lblAttendanceID.Text = _Attendance.AttendanceID.ToString();
                lblReservationID.Text = _Attendance.ReservationID.ToString();
                dtpCheckIn.Value = _Attendance.CheckInTime.Value;
            //    dtpCheckOut.Value = _Attendance.CheckOutTime.Value;
            }
        }

        private bool ValidateInput()
        {
            if (Mode == enMode.Update)
            {
                if (dtpCheckOut.Value <= dtpCheckIn.Value)
                {
                    MessageBox.Show(
                        "Check-out time must be after check-in time.",
                        "Invalid Time",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    return false;
                }
            }

            return true;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            if (Mode == enMode.AddNew)
            {
                    int reservationID =
                        Convert.ToInt32(lblReservationID.Text);

                    var checkResult =
                        clsAttendance.CanAddAttendance(reservationID);

                    switch (checkResult)
                    {
                        case enAttendanceCheckResult.ReservationNotFound:
                            MessageBox.Show(
                                "Reservation does not exist.",
                                "Attendance",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                            return;

                        case enAttendanceCheckResult.AttendanceAlreadyExists:
                            MessageBox.Show(
                                "Attendance already recorded for this reservation.",
                                "Attendance",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                            return;
                    }

                    _Attendance.ReservationID = reservationID;
                    _Attendance.CheckInTime = dtpCheckIn.Value;
                    _Attendance.CheckOutTime = null;
                

                if (_Attendance.Save())
                {
                    lblAttendanceID.Text =
                        _Attendance.AttendanceID.ToString();

                    Mode = enMode.Update;
                    lblTitle.Text = "Update Attendance";
                    this.Text = "Update Attendance";

                    MessageBox.Show(
                        "Attendance saved successfully.",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
                else
                {
                    MessageBox.Show(
                        "Failed to save attendance.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }

            if(Mode == enMode.Update)
            {
                dtpCheckIn.Enabled = false;
                dtpCheckOut.Enabled = true;
                
                 _Attendance.CheckOutTime = dtpCheckOut.Value;  

               
                    if (_Attendance.Save())
                    {
                        lblAttendanceID.Text =
                            _Attendance.AttendanceID.ToString();

                        Mode = enMode.Update;
                        lblTitle.Text = "Update Attendance";
                        this.Text = "Update Attendance";

                        MessageBox.Show(
                            "Attendance saved successfully.",
                            "Success",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );
                    }
                    else
                    {
                        MessageBox.Show(
                            "Failed to save attendance.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                    }
                
                 
            }
        }

        private void btnChoose_Click(object sender, EventArgs e)
        {
            frmListReservations frm = new frmListReservations(-1);
            frm.OnSelectReservation += OnReservationSelected;
            frm.ShowDialog();
        }

        private void OnReservationSelected(int ReservationID)
        {
            lblReservationID.Text = ReservationID.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
