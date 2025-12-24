namespace WindowsFormsApp11
{
    partial class frmAddUpdateWorkout
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
            this.components = new System.ComponentModel.Container();
            this.lblTitle = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblWorkoutID = new System.Windows.Forms.Label();
            this.btnChooseCoach = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.aa = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblFullName = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblSpecialization = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.lblCoachID = new System.Windows.Forms.Label();
            this.lblError = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTitle.Location = new System.Drawing.Point(12, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(448, 50);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "AddUpdateWorkout";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "WorkoutID";
            // 
            // lblWorkoutID
            // 
            this.lblWorkoutID.AutoSize = true;
            this.lblWorkoutID.Location = new System.Drawing.Point(137, 99);
            this.lblWorkoutID.Name = "lblWorkoutID";
            this.lblWorkoutID.Size = new System.Drawing.Size(27, 20);
            this.lblWorkoutID.TabIndex = 2;
            this.lblWorkoutID.Text = "??";
            // 
            // btnChooseCoach
            // 
            this.btnChooseCoach.Location = new System.Drawing.Point(261, 114);
            this.btnChooseCoach.Name = "btnChooseCoach";
            this.btnChooseCoach.Size = new System.Drawing.Size(145, 49);
            this.btnChooseCoach.TabIndex = 3;
            this.btnChooseCoach.Text = "Choose Coach";
            this.btnChooseCoach.UseVisualStyleBackColor = true;
            this.btnChooseCoach.Click += new System.EventHandler(this.btnChooseCoach_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 143);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 20);
            this.label4.TabIndex = 4;
            this.label4.Text = "Coach ID";
            // 
            // aa
            // 
            this.aa.AutoSize = true;
            this.aa.Location = new System.Drawing.Point(18, 271);
            this.aa.Name = "aa";
            this.aa.Size = new System.Drawing.Size(51, 20);
            this.aa.TabIndex = 6;
            this.aa.Text = "Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 315);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "Description";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(129, 265);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(226, 26);
            this.txtName.TabIndex = 8;
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(128, 312);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(278, 26);
            this.txtDescription.TabIndex = 9;
            // 
            // lblFullName
            // 
            this.lblFullName.AutoSize = true;
            this.lblFullName.Location = new System.Drawing.Point(137, 187);
            this.lblFullName.Name = "lblFullName";
            this.lblFullName.Size = new System.Drawing.Size(27, 20);
            this.lblFullName.TabIndex = 11;
            this.lblFullName.Text = "??";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 187);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101, 20);
            this.label6.TabIndex = 10;
            this.label6.Text = "Coach Name";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(340, 357);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(120, 52);
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(170, 357);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(120, 52);
            this.btnClose.TabIndex = 13;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblSpecialization
            // 
            this.lblSpecialization.AutoSize = true;
            this.lblSpecialization.Location = new System.Drawing.Point(136, 228);
            this.lblSpecialization.Name = "lblSpecialization";
            this.lblSpecialization.Size = new System.Drawing.Size(27, 20);
            this.lblSpecialization.TabIndex = 15;
            this.lblSpecialization.Text = "??";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 228);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(107, 20);
            this.label7.TabIndex = 14;
            this.label7.Text = "Specialization";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // lblCoachID
            // 
            this.lblCoachID.AutoSize = true;
            this.lblCoachID.Location = new System.Drawing.Point(137, 143);
            this.lblCoachID.Name = "lblCoachID";
            this.lblCoachID.Size = new System.Drawing.Size(27, 20);
            this.lblCoachID.TabIndex = 5;
            this.lblCoachID.Text = "??";
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblError.ForeColor = System.Drawing.Color.Red;
            this.lblError.Location = new System.Drawing.Point(246, 89);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(179, 22);
            this.lblError.TabIndex = 16;
            this.lblError.Text = "Pleas Choose Coach";
            this.lblError.Visible = false;
            // 
            // frmAddUpdateWorkout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(472, 421);
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.lblSpecialization);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblFullName);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.aa);
            this.Controls.Add(this.lblCoachID);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnChooseCoach);
            this.Controls.Add(this.lblWorkoutID);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmAddUpdateWorkout";
            this.Text = "frmAddUpdateWorkout";
            this.Load += new System.EventHandler(this.frmAddUpdateWorkout_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblWorkoutID;
        private System.Windows.Forms.Button btnChooseCoach;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label aa;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblFullName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblSpecialization;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Label lblCoachID;
        private System.Windows.Forms.Label lblError;
    }
}