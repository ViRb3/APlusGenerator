namespace APlusGenerator
{
    partial class ManageAccountsForm
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
            this.txtListViewEdit = new System.Windows.Forms.TextBox();
            this.chkViewStudents = new System.Windows.Forms.CheckBox();
            this.groupViewOptions = new System.Windows.Forms.GroupBox();
            this.btnNote = new System.Windows.Forms.Button();
            this.chkViewTeachers = new System.Windows.Forms.CheckBox();
            this.chkViewUnactivated = new System.Windows.Forms.CheckBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.txtClass = new APlusGenerator.WatermarkTextBox();
            this.editableListViewAccounts = new APlusGenerator.EditableListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupViewOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtListViewEdit
            // 
            this.txtListViewEdit.Location = new System.Drawing.Point(395, 83);
            this.txtListViewEdit.Name = "txtListViewEdit";
            this.txtListViewEdit.Size = new System.Drawing.Size(100, 20);
            this.txtListViewEdit.TabIndex = 0;
            this.txtListViewEdit.Visible = false;
            // 
            // chkViewStudents
            // 
            this.chkViewStudents.AutoSize = true;
            this.chkViewStudents.Checked = true;
            this.chkViewStudents.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkViewStudents.Location = new System.Drawing.Point(140, 18);
            this.chkViewStudents.Name = "chkViewStudents";
            this.chkViewStudents.Size = new System.Drawing.Size(68, 17);
            this.chkViewStudents.TabIndex = 2;
            this.chkViewStudents.Text = "Students";
            this.chkViewStudents.UseVisualStyleBackColor = true;
            this.chkViewStudents.CheckedChanged += new System.EventHandler(this.chkViewStudents_CheckedChanged);
            // 
            // groupViewOptions
            // 
            this.groupViewOptions.Controls.Add(this.btnNote);
            this.groupViewOptions.Controls.Add(this.txtClass);
            this.groupViewOptions.Controls.Add(this.chkViewTeachers);
            this.groupViewOptions.Controls.Add(this.chkViewUnactivated);
            this.groupViewOptions.Controls.Add(this.chkViewStudents);
            this.groupViewOptions.Location = new System.Drawing.Point(12, 12);
            this.groupViewOptions.Name = "groupViewOptions";
            this.groupViewOptions.Size = new System.Drawing.Size(321, 65);
            this.groupViewOptions.TabIndex = 3;
            this.groupViewOptions.TabStop = false;
            this.groupViewOptions.Text = "View Options";
            // 
            // btnNote
            // 
            this.btnNote.Location = new System.Drawing.Point(106, 18);
            this.btnNote.Name = "btnNote";
            this.btnNote.Size = new System.Drawing.Size(16, 22);
            this.btnNote.TabIndex = 4;
            this.btnNote.Text = "!";
            this.btnNote.UseVisualStyleBackColor = true;
            this.btnNote.Click += new System.EventHandler(this.btnNote_Click);
            // 
            // chkViewTeachers
            // 
            this.chkViewTeachers.AutoSize = true;
            this.chkViewTeachers.Checked = true;
            this.chkViewTeachers.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkViewTeachers.Location = new System.Drawing.Point(140, 41);
            this.chkViewTeachers.Name = "chkViewTeachers";
            this.chkViewTeachers.Size = new System.Drawing.Size(71, 17);
            this.chkViewTeachers.TabIndex = 2;
            this.chkViewTeachers.Text = "Teachers";
            this.chkViewTeachers.UseVisualStyleBackColor = true;
            this.chkViewTeachers.CheckedChanged += new System.EventHandler(this.chkViewTeachers_CheckedChanged);
            // 
            // chkViewUnactivated
            // 
            this.chkViewUnactivated.AutoSize = true;
            this.chkViewUnactivated.Location = new System.Drawing.Point(230, 20);
            this.chkViewUnactivated.Name = "chkViewUnactivated";
            this.chkViewUnactivated.Size = new System.Drawing.Size(84, 30);
            this.chkViewUnactivated.TabIndex = 2;
            this.chkViewUnactivated.Text = "Unactivated\r\nonly";
            this.chkViewUnactivated.UseVisualStyleBackColor = true;
            this.chkViewUnactivated.CheckedChanged += new System.EventHandler(this.chkViewUnactivated_CheckedChanged);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(420, 25);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "Discard";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnApply
            // 
            this.btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApply.Location = new System.Drawing.Point(420, 54);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 4;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Location = new System.Drawing.Point(339, 54);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 4;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // txtClass
            // 
            this.txtClass.Location = new System.Drawing.Point(6, 19);
            this.txtClass.Name = "txtClass";
            this.txtClass.Size = new System.Drawing.Size(100, 20);
            this.txtClass.TabIndex = 3;
            this.txtClass.WatermarkColor = System.Drawing.Color.Gray;
            this.txtClass.WatermarkText = "Class";
            // 
            // editableListViewAccounts
            // 
            this.editableListViewAccounts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editableListViewAccounts.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.editableListViewAccounts.FullRowSelect = true;
            this.editableListViewAccounts.Location = new System.Drawing.Point(12, 83);
            this.editableListViewAccounts.Name = "editableListViewAccounts";
            this.editableListViewAccounts.Size = new System.Drawing.Size(483, 306);
            this.editableListViewAccounts.TabIndex = 1;
            this.editableListViewAccounts.UseCompatibleStateImageBehavior = false;
            this.editableListViewAccounts.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "E-mail";
            this.columnHeader1.Width = 47;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "First name";
            this.columnHeader2.Width = 68;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Last name";
            this.columnHeader3.Width = 68;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Class";
            this.columnHeader4.Width = 47;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Type";
            this.columnHeader5.Width = 47;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Activated";
            // 
            // ManageAccountsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(502, 397);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.groupViewOptions);
            this.Controls.Add(this.txtListViewEdit);
            this.Controls.Add(this.editableListViewAccounts);
            this.MinimumSize = new System.Drawing.Size(518, 436);
            this.Name = "ManageAccountsForm";
            this.ShowIcon = false;
            this.Text = "Manage Accounts";
            this.groupViewOptions.ResumeLayout(false);
            this.groupViewOptions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtListViewEdit;
        private EditableListView editableListViewAccounts;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.CheckBox chkViewStudents;
        private System.Windows.Forms.GroupBox groupViewOptions;
        private System.Windows.Forms.CheckBox chkViewTeachers;
        private System.Windows.Forms.CheckBox chkViewUnactivated;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnRefresh;
        private WatermarkTextBox txtClass;
        private System.Windows.Forms.Button btnNote;
    }
}