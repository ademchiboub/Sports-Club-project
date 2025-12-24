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
using static SportsClub_Buisness.clsReservation;

namespace WindowsFormsApp11
{
    public partial class frmAddUpdateReservation : Form
    {

        enum enMode { AddNew, Update};
        enMode Mode = enMode.AddNew;
        int _ReservationID;
        clsReservation _Reservation;
        string Action;
        public frmAddUpdateReservation()
        {
            InitializeComponent();

            lblTitle.Text ="Add New Reservation";
            this.Text = "Add New Reservation";
            Action = "Add";
            _Reservation = new clsReservation();
            Mode = enMode.AddNew;
        }

        public frmAddUpdateReservation(int ReservationID)
        {
            InitializeComponent();

            lblTitle.Text ="Update Reservation";
            this.Text = "Update Reservation";
            Action = "Update";
            _ReservationID = ReservationID;
            _Reservation = clsReservation.Find(_ReservationID);
            Mode = enMode.Update;
        }

        private void SearchMember_Click(object sender, EventArgs e)
        {
            frmListMemberSubscriptions frm = new frmListMemberSubscriptions(-1);
            frm.OnSelectMemberSubscription += OnSelectMemberID;
            frm.ShowDialog();
        }

        private void OnSelectMemberID(int MemberID)
        {
            lblMemberID.Text = MemberID.ToString();
        }

        private void btnSearchSchedule_Click(object sender, EventArgs e)
        {
            frmListSchedules frm = new frmListSchedules(-1);
            frm.OnSelectSchedule += OnSelectedScheduleID;
            frm.ShowDialog();
        }

        private void OnSelectedScheduleID(int ScheduleID)
        {
            lblScheduleID.Text = ScheduleID.ToString(); 
        }

        private void frmAddUpdateReservation_Load(object sender, EventArgs e)
        {
            cbStatus.SelectedIndex = 0;
            lblCreateByUserID.Text = clsGlobal.CurrentUser.UserID.ToString();

            if(Mode == enMode.Update)
            {
                lblReservationID.Text = _Reservation.ReservationID.ToString();
                lblMemberID.Text = _Reservation.MemberID.ToString();
                lblScheduleID.Text = _Reservation.ScheduleID.ToString();
                cbStatus.Text = _Reservation.Status; 
                lblCreateByUserID.Text = _Reservation.CreateByUserID.ToString();
            }
        }

        private bool InputeValidate()
        {
            if(lblMemberID.Text == "???" ||  lblScheduleID.Text == "???")
            {
                return true;
            }

            return false;
        }

        


        private void btnSave_Click(object sender, EventArgs e)
        {
            // 1️⃣ التحقق من صحة المدخلات
            if (InputeValidate())
            {
                MessageBox.Show("Please populate the required data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // =========================
            // ADD NEW LOGIC
            // =========================
            if (Mode == enMode.AddNew)
            {
                int memberID = Convert.ToInt32(lblMemberID.Text);
                int scheduleID = Convert.ToInt32(lblScheduleID.Text);

                // استدعاء Business Layer للتحقق من إمكانية الحجز
                enReservationCheckResult checkResult = clsReservation.CanAddReservation(memberID, scheduleID);

                switch (checkResult)
                {
                    case enReservationCheckResult.NoActiveSubscription:
                        MessageBox.Show(
                            "This member does not have an active subscription.",
                            "Reservation Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                        return;

                    case enReservationCheckResult.DuplicateReservation:
                        MessageBox.Show(
                            "This member already has a reservation for this schedule.",
                            "Reservation Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                        return;

                    case enReservationCheckResult.ScheduleFull:
                        MessageBox.Show(
                            "This class is fully booked.",
                            "Reservation Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                        return;

                    case enReservationCheckResult.Allowed:
                        // كل شيء تمام، نتابع
                        break;

                    default:
                        MessageBox.Show(
                            "Unknown reservation check result.",
                            "Reservation Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                        return;
                }

                // تعيين القيم عند الإضافة
                _Reservation.MemberID = memberID;
                _Reservation.ScheduleID = scheduleID;
                _Reservation.ReservationDate = dtpDate.Value;
                _Reservation.CreateByUserID = clsGlobal.CurrentUser.UserID;
            }

            // =========================
            // UPDATE LOGIC (Status Only)
            // =========================
            _Reservation.Status = cbStatus.SelectedItem.ToString();

            // =========================
            // SAVE
            // =========================
            if (_Reservation.Save())
            {
                lblReservationID.Text = _Reservation.ReservationID.ToString();
                lblTitle.Text = "Update Reservation";
                this.Text = "Update Reservation";
                Mode = enMode.Update;
                Action = "Update";

                MessageBox.Show(
                    $"{Action} Reservation Successfully",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            else
            {
                MessageBox.Show(
                    $"{Action} Reservation Failed",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}
