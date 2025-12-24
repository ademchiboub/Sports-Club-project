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
    public partial class frmMain : Form
    {
        frmLogin _frmLogin;
        public frmMain(frmLogin frm)
        {
            InitializeComponent();

            _frmLogin = frm;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
          

        }

        private void memberSubscriptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((clsGlobal.CurrentUser.Role & (int)clsUser.UserPermissions.frmReservation) == 0)
            {
                MessageBox.Show("You do not have permission to open this screen.");
                return;
            }

            frmListReservations frm = new frmListReservations();
            frm.ShowDialog();
        }

        private void attendanceToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if((clsGlobal.CurrentUser.Role & (int)clsUser.UserPermissions.frmMember) == 0)
            {
                MessageBox.Show("You do not have permission to open this screen.");
                return;
            }

            frmMembersList frm = new frmMembersList();
            frm.ShowDialog();
        }

        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if ((clsGlobal.CurrentUser.Role & (int)clsUser.UserPermissions.frmPeople) == 0)
            {
                MessageBox.Show("You do not have permission to open this screen.");
                return;
            }


            frmManagePeople frm = new frmManagePeople();
            frm.ShowDialog();
        }

        private void coToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((clsGlobal.CurrentUser.Role & (int)clsUser.UserPermissions.frmCoach) == 0)
            {
                MessageBox.Show("You do not have permission to open this screen.");
                return;
            }

            frmCoachesList frm = new frmCoachesList();
            frm.ShowDialog();
        }

        private void workoutsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((clsGlobal.CurrentUser.Role & (int)clsUser.UserPermissions.frmWorkouts) == 0)
            {
                MessageBox.Show("You do not have permission to open this screen.");
                return;
            }

            frmListWorkouts frm = new frmListWorkouts();
            frm.ShowDialog();
        }

        private void schedulesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((clsGlobal.CurrentUser.Role & (int)clsUser.UserPermissions.frmSchedule) == 0)
            {
                MessageBox.Show("You do not have permission to open this screen.");
                return;
            }

            frmListSchedules frm = new frmListSchedules();
            frm.ShowDialog();
        }

        private void memberSubscriptionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((clsGlobal.CurrentUser.Role & (int)clsUser.UserPermissions.frmMemberSubscription) == 0)
            {
                MessageBox.Show("You do not have permission to open this screen.");
                return;
            }

            frmListMemberSubscriptions frm = new frmListMemberSubscriptions();
            frm.ShowDialog();
        }

        private void paymentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((clsGlobal.CurrentUser.Role & (int)clsUser.UserPermissions.frmPayment) == 0)
            {
                MessageBox.Show("You do not have permission to open this screen.");
                return;
            }

            frmListPayments frm = new frmListPayments();
            frm.ShowDialog();
        }

        private void attendanceToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if ((clsGlobal.CurrentUser.Role & (int)clsUser.UserPermissions.frmAttendance) == 0)
            {
                MessageBox.Show("You do not have permission to open this screen.");
                return;
            }

            frmListAttendance frm = new frmListAttendance();
            frm.ShowDialog();
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if ((clsGlobal.CurrentUser.Role & (int)clsUser.UserPermissions.frmUser) == 0)
            //{
            //    MessageBox.Show("You do not have permission to open this screen.");
            //    return;
            //}

            frmListUsers frm = new frmListUsers();
            frm.ShowDialog();
        }
    }
}
