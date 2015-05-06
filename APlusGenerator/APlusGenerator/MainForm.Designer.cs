using System.Windows.Forms;

namespace APlusGenerator
{
    partial class MainForm
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
            this.btnSelectStudents = new System.Windows.Forms.Button();
            this.groupWelcome = new System.Windows.Forms.GroupBox();
            this.btnManageAccounts = new System.Windows.Forms.Button();
            this.groupStudents = new System.Windows.Forms.GroupBox();
            this.listViewStudents = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.numCodes = new System.Windows.Forms.NumericUpDown();
            this.lblCodes = new System.Windows.Forms.Label();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.groupWelcome.SuspendLayout();
            this.groupStudents.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCodes)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSelectStudents
            // 
            this.btnSelectStudents.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectStudents.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSelectStudents.Location = new System.Drawing.Point(182, 17);
            this.btnSelectStudents.Name = "btnSelectStudents";
            this.btnSelectStudents.Size = new System.Drawing.Size(132, 46);
            this.btnSelectStudents.TabIndex = 0;
            this.btnSelectStudents.Text = "Select students";
            this.btnSelectStudents.UseVisualStyleBackColor = true;
            this.btnSelectStudents.Click += new System.EventHandler(this.btnSelectStudents_Click);
            // 
            // groupWelcome
            // 
            this.groupWelcome.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupWelcome.Controls.Add(this.btnManageAccounts);
            this.groupWelcome.Controls.Add(this.btnSelectStudents);
            this.groupWelcome.Location = new System.Drawing.Point(12, 12);
            this.groupWelcome.Name = "groupWelcome";
            this.groupWelcome.Size = new System.Drawing.Size(328, 75);
            this.groupWelcome.TabIndex = 3;
            this.groupWelcome.TabStop = false;
            // 
            // btnManageAccounts
            // 
            this.btnManageAccounts.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnManageAccounts.Location = new System.Drawing.Point(15, 17);
            this.btnManageAccounts.Name = "btnManageAccounts";
            this.btnManageAccounts.Size = new System.Drawing.Size(132, 46);
            this.btnManageAccounts.TabIndex = 1;
            this.btnManageAccounts.Text = "Manage accounts";
            this.btnManageAccounts.UseVisualStyleBackColor = true;
            this.btnManageAccounts.Click += new System.EventHandler(this.btnManageAccounts_Click);
            // 
            // groupStudents
            // 
            this.groupStudents.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupStudents.Controls.Add(this.listViewStudents);
            this.groupStudents.Location = new System.Drawing.Point(12, 105);
            this.groupStudents.Name = "groupStudents";
            this.groupStudents.Size = new System.Drawing.Size(328, 288);
            this.groupStudents.TabIndex = 4;
            this.groupStudents.TabStop = false;
            this.groupStudents.Text = "Students";
            // 
            // listViewStudents
            // 
            this.listViewStudents.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewStudents.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.listViewStudents.ContextMenuStrip = this.contextMenuStrip1;
            this.listViewStudents.FullRowSelect = true;
            this.listViewStudents.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewStudents.Location = new System.Drawing.Point(6, 19);
            this.listViewStudents.Name = "listViewStudents";
            this.listViewStudents.Size = new System.Drawing.Size(316, 263);
            this.listViewStudents.TabIndex = 0;
            this.listViewStudents.TabStop = false;
            this.listViewStudents.UseCompatibleStateImageBehavior = false;
            this.listViewStudents.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "E-mail";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "First name";
            this.columnHeader2.Width = 63;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Last name";
            this.columnHeader3.Width = 70;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Class";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removeToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(118, 26);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.removeToolStripMenuItem.Text = "Remove";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
            // 
            // numCodes
            // 
            this.numCodes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numCodes.Location = new System.Drawing.Point(114, 406);
            this.numCodes.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.numCodes.Name = "numCodes";
            this.numCodes.Size = new System.Drawing.Size(56, 20);
            this.numCodes.TabIndex = 2;
            this.numCodes.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblCodes
            // 
            this.lblCodes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCodes.AutoSize = true;
            this.lblCodes.Location = new System.Drawing.Point(12, 408);
            this.lblCodes.Name = "lblCodes";
            this.lblCodes.Size = new System.Drawing.Size(96, 13);
            this.lblCodes.TabIndex = 6;
            this.lblCodes.Text = "Codes per student:";
            // 
            // btnGenerate
            // 
            this.btnGenerate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGenerate.Location = new System.Drawing.Point(253, 403);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(87, 23);
            this.btnGenerate.TabIndex = 3;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(351, 435);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.lblCodes);
            this.Controls.Add(this.numCodes);
            this.Controls.Add(this.groupStudents);
            this.Controls.Add(this.groupWelcome);
            this.MinimumSize = new System.Drawing.Size(367, 474);
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.Text = "APlus QR Generator ~ViRb3";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.groupWelcome.ResumeLayout(false);
            this.groupStudents.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numCodes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSelectStudents;
        private System.Windows.Forms.GroupBox groupWelcome;
        private System.Windows.Forms.GroupBox groupStudents;
        private System.Windows.Forms.NumericUpDown numCodes;
        private System.Windows.Forms.Label lblCodes;
        private System.Windows.Forms.Button btnGenerate;
        private ListView listViewStudents;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private Button btnManageAccounts;
    }
}

