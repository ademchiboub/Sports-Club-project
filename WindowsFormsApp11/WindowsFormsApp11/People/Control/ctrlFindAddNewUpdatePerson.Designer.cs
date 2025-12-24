namespace WindowsFormsApp11
{
    partial class ctrlFindAddNewUpdatePerson
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtPersonID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ctrlPersonInfo1 = new WindowsFormsApp11.ctrlPersonInfo();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnResearch = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtPersonID
            // 
            this.txtPersonID.Location = new System.Drawing.Point(162, 28);
            this.txtPersonID.Name = "txtPersonID";
            this.txtPersonID.Size = new System.Drawing.Size(207, 26);
            this.txtPersonID.TabIndex = 1;
            this.txtPersonID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            this.txtPersonID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(50, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Person ID";
            // 
            // ctrlPersonInfo1
            // 
            this.ctrlPersonInfo1.Location = new System.Drawing.Point(3, 76);
            this.ctrlPersonInfo1.Name = "ctrlPersonInfo1";
            this.ctrlPersonInfo1.Size = new System.Drawing.Size(630, 293);
            this.ctrlPersonInfo1.TabIndex = 0;
            // 
            // btnAdd
            // 
            this.btnAdd.BackgroundImage = global::WindowsFormsApp11.Properties.Resources.Add_Person_72;
            this.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAdd.Location = new System.Drawing.Point(490, 6);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(62, 55);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnResearch
            // 
            this.btnResearch.BackgroundImage = global::WindowsFormsApp11.Properties.Resources.SearchPerson;
            this.btnResearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnResearch.Location = new System.Drawing.Point(412, 6);
            this.btnResearch.Name = "btnResearch";
            this.btnResearch.Size = new System.Drawing.Size(62, 55);
            this.btnResearch.TabIndex = 3;
            this.btnResearch.UseVisualStyleBackColor = true;
            this.btnResearch.Click += new System.EventHandler(this.btnResearch_Click);
            // 
            // ctrlFindAddNewUpdatePerson
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnResearch);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPersonID);
            this.Controls.Add(this.ctrlPersonInfo1);
            this.Name = "ctrlFindAddNewUpdatePerson";
            this.Size = new System.Drawing.Size(652, 378);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ctrlPersonInfo ctrlPersonInfo1;
        private System.Windows.Forms.TextBox txtPersonID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnResearch;
        private System.Windows.Forms.Button btnAdd;
    }
}
