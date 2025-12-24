namespace WindowsFormsApp11
{
    partial class frmAddUpdateCoaches
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
            this.lblCoachID = new System.Windows.Forms.Label();
            this.cbSpecialization = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.ctrlFindAddNewUpdatePerson1 = new WindowsFormsApp11.ctrlFindAddNewUpdatePerson();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblCoachID
            // 
            this.lblCoachID.AutoSize = true;
            this.lblCoachID.Location = new System.Drawing.Point(115, 223);
            this.lblCoachID.Name = "lblCoachID";
            this.lblCoachID.Size = new System.Drawing.Size(27, 20);
            this.lblCoachID.TabIndex = 17;
            this.lblCoachID.Text = "??";
            // 
            // cbSpecialization
            // 
            this.cbSpecialization.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.cbSpecialization.FormattingEnabled = true;
            this.cbSpecialization.Location = new System.Drawing.Point(331, 220);
            this.cbSpecialization.Name = "cbSpecialization";
            this.cbSpecialization.Size = new System.Drawing.Size(169, 28);
            this.cbSpecialization.TabIndex = 14;
            this.cbSpecialization.Text = "Kickboxing Basics";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(218, 223);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 20);
            this.label3.TabIndex = 13;
            this.label3.Text = "Specialization:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 223);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 20);
            this.label2.TabIndex = 12;
            this.label2.Text = "CoachID";
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTitle.Location = new System.Drawing.Point(11, 123);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(670, 51);
            this.lblTitle.TabIndex = 18;
            this.lblTitle.Text = "AddUpdateCoache";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnClose
            // 
            this.btnClose.Image = global::WindowsFormsApp11.Properties.Resources.Close_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(352, 617);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(128, 56);
            this.btnClose.TabIndex = 20;
            this.btnClose.Text = "       Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(275, 1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(148, 119);
            this.pictureBox1.TabIndex = 19;
            this.pictureBox1.TabStop = false;
            // 
            // btnSave
            // 
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnSave.Image = global::WindowsFormsApp11.Properties.Resources.Save_32;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(535, 617);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(129, 56);
            this.btnSave.TabIndex = 16;
            this.btnSave.Text = "      Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // ctrlFindAddNewUpdatePerson1
            // 
            this.ctrlFindAddNewUpdatePerson1.Location = new System.Drawing.Point(29, 266);
            this.ctrlFindAddNewUpdatePerson1.Name = "ctrlFindAddNewUpdatePerson1";
            this.ctrlFindAddNewUpdatePerson1.Size = new System.Drawing.Size(652, 378);
            this.ctrlFindAddNewUpdatePerson1.TabIndex = 15;
            // 
            // frmAddUpdateCoaches
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(694, 680);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblCoachID);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.ctrlFindAddNewUpdatePerson1);
            this.Controls.Add(this.cbSpecialization);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Name = "frmAddUpdateCoaches";
            this.Text = "frmAddUpdateCoaches";
            this.Load += new System.EventHandler(this.frmAddUpdateCoaches_Load);
            this.Shown += new System.EventHandler(this.frmAddUpdateCoaches_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCoachID;
        private System.Windows.Forms.Button btnSave;
        private ctrlFindAddNewUpdatePerson ctrlFindAddNewUpdatePerson1;
        private System.Windows.Forms.ComboBox cbSpecialization;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnClose;
    }
}