using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SportsClub_Buisness.clsUser;

namespace WindowsFormsApp11
{
    public partial class frmPermissions : Form
    {
        public Action<int> OnSelectRole;
        public frmPermissions()
        {
            InitializeComponent();
        }

        private void frmPermissions_Load(object sender, EventArgs e)
        {
            flowPermissions.Controls.Clear();

            foreach (UserPermissions perm in Enum.GetValues(typeof(UserPermissions)))
            {
                if (perm == UserPermissions.None)
                    continue;

                CheckBox chk = new CheckBox();
                chk.Text = perm.ToString();
                chk.Tag = perm;
                chk.AutoSize = true;

                flowPermissions.Controls.Add(chk);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int roleValue = 0;

            foreach (CheckBox chk in flowPermissions.Controls.OfType<CheckBox>())
            {
                if (chk.Checked)
                {
                    roleValue |= (int)(UserPermissions)chk.Tag;
                }
            }
          
            OnSelectRole?.Invoke(roleValue);
        }



        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
