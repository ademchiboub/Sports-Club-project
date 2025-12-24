namespace WindowsFormsApp11
{
    partial class frmAddUpdateReservation
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblMemberID = new System.Windows.Forms.Label();
            this.lblReservationID = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SearchMember = new System.Windows.Forms.Button();
            this.btnSearchSchedule = new System.Windows.Forms.Button();
            this.lblScheduleID = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbStatus = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblCreateByUserID = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTitle.Location = new System.Drawing.Point(12, 24);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(563, 56);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Add Update Reservation";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(30, 164);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 22);
            this.label2.TabIndex = 1;
            this.label2.Text = "Member ID:";
            // 
            // lblMemberID
            // 
            this.lblMemberID.AutoSize = true;
            this.lblMemberID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMemberID.Location = new System.Drawing.Point(206, 164);
            this.lblMemberID.Name = "lblMemberID";
            this.lblMemberID.Size = new System.Drawing.Size(40, 22);
            this.lblMemberID.TabIndex = 2;
            this.lblMemberID.Text = "???";
            // 
            // lblReservationID
            // 
            this.lblReservationID.AutoSize = true;
            this.lblReservationID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReservationID.Location = new System.Drawing.Point(206, 126);
            this.lblReservationID.Name = "lblReservationID";
            this.lblReservationID.Size = new System.Drawing.Size(40, 22);
            this.lblReservationID.TabIndex = 4;
            this.lblReservationID.Text = "???";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(30, 126);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(133, 22);
            this.label5.TabIndex = 3;
            this.label5.Text = "Reservation ID:";
            // 
            // SearchMember
            // 
            this.SearchMember.Location = new System.Drawing.Point(283, 149);
            this.SearchMember.Name = "SearchMember";
            this.SearchMember.Size = new System.Drawing.Size(203, 39);
            this.SearchMember.TabIndex = 5;
            this.SearchMember.Text = "Search Member";
            this.SearchMember.UseVisualStyleBackColor = true;
            this.SearchMember.Click += new System.EventHandler(this.SearchMember_Click);
            // 
            // btnSearchSchedule
            // 
            this.btnSearchSchedule.Location = new System.Drawing.Point(283, 194);
            this.btnSearchSchedule.Name = "btnSearchSchedule";
            this.btnSearchSchedule.Size = new System.Drawing.Size(203, 39);
            this.btnSearchSchedule.TabIndex = 8;
            this.btnSearchSchedule.Text = "Search Schedule";
            this.btnSearchSchedule.UseVisualStyleBackColor = true;
            this.btnSearchSchedule.Click += new System.EventHandler(this.btnSearchSchedule_Click);
            // 
            // lblScheduleID
            // 
            this.lblScheduleID.AutoSize = true;
            this.lblScheduleID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScheduleID.Location = new System.Drawing.Point(206, 209);
            this.lblScheduleID.Name = "lblScheduleID";
            this.lblScheduleID.Size = new System.Drawing.Size(40, 22);
            this.lblScheduleID.TabIndex = 7;
            this.lblScheduleID.Text = "???";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(30, 209);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 22);
            this.label4.TabIndex = 6;
            this.label4.Text = "ScheduleID";
            // 
            // cbStatus
            // 
            this.cbStatus.FormattingEnabled = true;
            this.cbStatus.Items.AddRange(new object[] {
            "Reserved",
            "Canceled",
            "Rejected",
            "Pending"});
            this.cbStatus.Location = new System.Drawing.Point(210, 254);
            this.cbStatus.Name = "cbStatus";
            this.cbStatus.Size = new System.Drawing.Size(198, 28);
            this.cbStatus.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(30, 260);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 22);
            this.label6.TabIndex = 10;
            this.label6.Text = "Status";
            // 
            // dtpDate
            // 
            this.dtpDate.Location = new System.Drawing.Point(210, 304);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(200, 26);
            this.dtpDate.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(30, 308);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 22);
            this.label3.TabIndex = 12;
            this.label3.Text = "Date Time:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(30, 357);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(160, 22);
            this.label7.TabIndex = 13;
            this.label7.Text = "Create By User ID:";
            // 
            // lblCreateByUserID
            // 
            this.lblCreateByUserID.AutoSize = true;
            this.lblCreateByUserID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreateByUserID.Location = new System.Drawing.Point(206, 357);
            this.lblCreateByUserID.Name = "lblCreateByUserID";
            this.lblCreateByUserID.Size = new System.Drawing.Size(40, 22);
            this.lblCreateByUserID.TabIndex = 14;
            this.lblCreateByUserID.Text = "???";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(380, 392);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(157, 46);
            this.btnSave.TabIndex = 15;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // frmAddUpdateReservation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(587, 450);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblCreateByUserID);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cbStatus);
            this.Controls.Add(this.btnSearchSchedule);
            this.Controls.Add(this.lblScheduleID);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.SearchMember);
            this.Controls.Add(this.lblReservationID);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblMemberID);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmAddUpdateReservation";
            this.Text = "frmAddUpdateReservation";
            this.Load += new System.EventHandler(this.frmAddUpdateReservation_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblMemberID;
        private System.Windows.Forms.Label lblReservationID;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button SearchMember;
        private System.Windows.Forms.Button btnSearchSchedule;
        private System.Windows.Forms.Label lblScheduleID;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbStatus;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblCreateByUserID;
        private System.Windows.Forms.Button btnSave;
    }
}