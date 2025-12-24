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
    public partial class frmAddUpdateMember : Form
    {

        int _MemberID;
        clsMember _Member;
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public frmAddUpdateMember()
        {
            InitializeComponent();

            Mode = enMode.AddNew;
        }

        public frmAddUpdateMember(int MemberID)
        {
            InitializeComponent();

            _MemberID = MemberID;
            _Member = clsMember.Find(MemberID);
            Mode = enMode.Update;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (Mode == enMode.AddNew)
            {

                int? PersonID = ctrlFindAddNewUpdatePerson1.PersonID;
                if (PersonID != null)
                {
                    if (clsMember.IsMemberExistAndActive(PersonID.Value))
                    {
                        MessageBox.Show("This Member is Exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        return;
                    }

                    _Member = new clsMember();
                    _Member.PersonID = PersonID.Value;

                    if (cbStatus.SelectedIndex == 0)
                        _Member.Status = clsMember.MemberStatus.Active;
                    else if (cbStatus.SelectedIndex == 1)
                        _Member.Status = clsMember.MemberStatus.Inactive;
                    else _Member.Status = clsMember.MemberStatus.Suspended;
                    if (_Member
                        .Save())
                    {
                        lblTitle.Text = "Update Member";
                        this.Text = "Update Member";
                        Mode = enMode.Update;

                        MessageBox.Show("Member Save Successfully", "Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Failed to Add Member", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    return;
                }
            }

            if(Mode == enMode.Update)
            {
                if (cbStatus.SelectedIndex == 0)
                    _Member.Status = clsMember.MemberStatus.Active;
                else if (cbStatus.SelectedIndex == 1)
                    _Member.Status = clsMember.MemberStatus.Inactive;
                else _Member.Status = clsMember.MemberStatus.Suspended;

                if (_Member.Save())
                {

                    lblMemberID.Text = _Member.MemberID.ToString();
                    MessageBox.Show("Member Save Successfully", "Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Failed to Update Member", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void frmAddUpdateMember_Load(object sender, EventArgs e)
        {
            
            if(Mode == enMode.AddNew)
            {
                lblTitle.Text = "Add New Member";
                this.Text = "Add New Member";
            }
            else
            {
                lblTitle.Text = "Update Member";
                this.Text = "Update Member";
                ctrlFindAddNewUpdatePerson1.SelectPersonID(_Member.PersonInfo.PersonID);
                cbStatus.Text = _Member.Status.ToString();
            }
            
        }

        private void frmAddUpdateMember_Shown(object sender, EventArgs e)
        {
            ctrlFindAddNewUpdatePerson1.txtFilterFoucs();
        }
    }
}
