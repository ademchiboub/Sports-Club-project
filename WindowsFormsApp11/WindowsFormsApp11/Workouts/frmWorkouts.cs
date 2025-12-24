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
    public partial class frmListWorkouts : Form
    {
        DataTable _dtGetAllWorkouts;
        public frmListWorkouts()
        {
            InitializeComponent();
        }

        private void frmWorkouts_Load(object sender, EventArgs e)
        {
            _dtGetAllWorkouts = clsWorkouts.GetAllWorkouts();

            dgvAllWorkouts.DataSource = _dtGetAllWorkouts;
            lblCountRecords.Text = dgvAllWorkouts.Rows.Count.ToString();

            if(dgvAllWorkouts.Rows.Count > 0 )
            {
                dgvAllWorkouts.Columns[0].HeaderText = "Workout ID";

                dgvAllWorkouts.Columns[1].HeaderText = "Workout Name";
                dgvAllWorkouts.Columns[1].Width = 200;

                dgvAllWorkouts.Columns[2].HeaderText = "Full Name";
                dgvAllWorkouts.Columns[2].Width = 250;

               
                dgvAllWorkouts.Columns[3].Width = 170;
            }

            

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddWorkout_Click(object sender, EventArgs e)
        {
            frmAddUpdateWorkout frm = new frmAddUpdateWorkout();
            frm.ShowDialog();

            frmWorkouts_Load(null, null);
        }

        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowPersonInfo frm = new frmShowPersonInfo((clsWorkouts.Find((int)dgvAllWorkouts.CurrentRow.Cells[0].Value).CoachInfo.PersonInfo.PersonID));
            frm.ShowDialog();

            
        }

        private void addNewWorkoutsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateWorkout frm = new frmAddUpdateWorkout();
            frm.ShowDialog();

            frmWorkouts_Load(null, null);
        }

        private void updateWorkoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateWorkout frm = new frmAddUpdateWorkout((int)dgvAllWorkouts.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

            frmWorkouts_Load(null, null);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (clsWorkouts.DeleteWorkout((int)dgvAllWorkouts.CurrentRow.Cells[0].Value))
            {
                MessageBox.Show("Workouts has been deleted successfully", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmWorkouts_Load(null, null);
            }

            else
                MessageBox.Show("Workouts is not delted due to data connected to it.", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
    }
}
