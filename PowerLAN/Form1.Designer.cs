﻿﻿namespace PowerLAN
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// &param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtMacAddress = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtIp4 = new System.Windows.Forms.TextBox();
            this.txtIp3 = new System.Windows.Forms.TextBox();
            this.txtIp2 = new System.Windows.Forms.TextBox();
            this.txtIp1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.listViewComputers = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnShutdownSelected = new System.Windows.Forms.Button();
            this.btnStartupSelected = new System.Windows.Forms.Button();
            this.btnShutdownAll = new System.Windows.Forms.Button();
            this.btnStartupAll = new System.Windows.Forms.Button();
            this.btnRemoveSelected = new System.Windows.Forms.Button();
            this.btnClearList = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Controls.Add(this.txtPassword);
            this.groupBox1.Controls.Add(this.txtUsername);
            this.groupBox1.Controls.Add(this.txtMacAddress);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtIp4);
            this.groupBox1.Controls.Add(this.txtIp3);
            this.groupBox1.Controls.Add(this.txtIp2);
            this.groupBox1.Controls.Add(this.txtIp1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 11);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(543, 120);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "计算机信息";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(443, 91);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(90, 23);
            this.btnAdd.TabIndex = 12;
            this.btnAdd.Text = "添加到列表";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(383, 53);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(150, 21);
            this.txtPassword.TabIndex = 11;
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(383, 25);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(150, 21);
            this.txtUsername.TabIndex = 10;
            // 
            // txtMacAddress
            // 
            this.txtMacAddress.Location = new System.Drawing.Point(100, 54);
            this.txtMacAddress.Name = "txtMacAddress";
            this.txtMacAddress.Size = new System.Drawing.Size(212, 21);
            this.txtMacAddress.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(323, 55);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "密码:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(323, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "用户名:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "MAC地址:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "IP地址:";
            // 
            // txtIp4
            // 
            this.txtIp4.Location = new System.Drawing.Point(271, 27);
            this.txtIp4.Name = "txtIp4";
            this.txtIp4.Size = new System.Drawing.Size(38, 21);
            this.txtIp4.TabIndex = 4;
            // 
            // txtIp3
            // 
            this.txtIp3.Location = new System.Drawing.Point(214, 27);
            this.txtIp3.Name = "txtIp3";
            this.txtIp3.Size = new System.Drawing.Size(38, 21);
            this.txtIp3.TabIndex = 3;
            this.txtIp3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtIp_KeyPress);
            // 
            // txtIp2
            // 
            this.txtIp2.Location = new System.Drawing.Point(157, 27);
            this.txtIp2.Name = "txtIp2";
            this.txtIp2.Size = new System.Drawing.Size(38, 21);
            this.txtIp2.TabIndex = 2;
            this.txtIp2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtIp_KeyPress);
            // 
            // txtIp1
            // 
            this.txtIp1.Location = new System.Drawing.Point(100, 27);
            this.txtIp1.Name = "txtIp1";
            this.txtIp1.Size = new System.Drawing.Size(38, 21);
            this.txtIp1.TabIndex = 1;
            this.txtIp1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtIp_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(142, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(11, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = ".";
            // 
            // listViewComputers
            // 
            this.listViewComputers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.listViewComputers.FullRowSelect = true;
            this.listViewComputers.GridLines = true;
            this.listViewComputers.HideSelection = false;
            this.listViewComputers.Location = new System.Drawing.Point(12, 137);
            this.listViewComputers.Name = "listViewComputers";
            this.listViewComputers.Size = new System.Drawing.Size(533, 231);
            this.listViewComputers.TabIndex = 1;
            this.listViewComputers.UseCompatibleStateImageBehavior = false;
            this.listViewComputers.View = System.Windows.Forms.View.Details;
            this.listViewComputers.SelectedIndexChanged += new System.EventHandler(this.ListViewComputers_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "IP 地址";
            this.columnHeader1.Width = 150;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "MAC 地址";
            this.columnHeader2.Width = 150;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "用户名";
            this.columnHeader3.Width = 120;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "状态";
            this.columnHeader4.Width = 100;
            // 
            // btnShutdownSelected
            // 
            this.btnShutdownSelected.Location = new System.Drawing.Point(12, 383);
            this.btnShutdownSelected.Name = "btnShutdownSelected";
            this.btnShutdownSelected.Size = new System.Drawing.Size(70, 21);
            this.btnShutdownSelected.TabIndex = 2;
            this.btnShutdownSelected.Text = "关机选中";
            this.btnShutdownSelected.UseVisualStyleBackColor = true;
            this.btnShutdownSelected.Click += new System.EventHandler(this.BtnShutdownSelected_Click);
            // 
            // btnStartupSelected
            // 
            this.btnStartupSelected.Location = new System.Drawing.Point(91, 383);
            this.btnStartupSelected.Name = "btnStartupSelected";
            this.btnStartupSelected.Size = new System.Drawing.Size(70, 21);
            this.btnStartupSelected.TabIndex = 3;
            this.btnStartupSelected.Text = "开机选中";
            this.btnStartupSelected.UseVisualStyleBackColor = true;
            this.btnStartupSelected.Click += new System.EventHandler(this.BtnStartupSelected_Click);
            // 
            // btnShutdownAll
            // 
            this.btnShutdownAll.Location = new System.Drawing.Point(170, 383);
            this.btnShutdownAll.Name = "btnShutdownAll";
            this.btnShutdownAll.Size = new System.Drawing.Size(70, 21);
            this.btnShutdownAll.TabIndex = 4;
            this.btnShutdownAll.Text = "关机全部";
            this.btnShutdownAll.UseVisualStyleBackColor = true;
            this.btnShutdownAll.Click += new System.EventHandler(this.BtnShutdownAll_Click);
            // 
            // btnStartupAll
            // 
            this.btnStartupAll.Location = new System.Drawing.Point(249, 383);
            this.btnStartupAll.Name = "btnStartupAll";
            this.btnStartupAll.Size = new System.Drawing.Size(70, 21);
            this.btnStartupAll.TabIndex = 5;
            this.btnStartupAll.Text = "开机全部";
            this.btnStartupAll.UseVisualStyleBackColor = true;
            this.btnStartupAll.Click += new System.EventHandler(this.BtnStartupAll_Click);
            // 
            // btnRemoveSelected
            // 
            this.btnRemoveSelected.Location = new System.Drawing.Point(399, 383);
            this.btnRemoveSelected.Name = "btnRemoveSelected";
            this.btnRemoveSelected.Size = new System.Drawing.Size(70, 21);
            this.btnRemoveSelected.TabIndex = 6;
            this.btnRemoveSelected.Text = "移除选中";
            this.btnRemoveSelected.UseVisualStyleBackColor = true;
            this.btnRemoveSelected.Click += new System.EventHandler(this.BtnRemoveSelected_Click);
            // 
            // btnClearList
            // 
            this.btnClearList.Location = new System.Drawing.Point(475, 383);
            this.btnClearList.Name = "btnClearList";
            this.btnClearList.Size = new System.Drawing.Size(70, 21);
            this.btnClearList.TabIndex = 7;
            this.btnClearList.Text = "清空列表";
            this.btnClearList.UseVisualStyleBackColor = true;
            this.btnClearList.Click += new System.EventHandler(this.BtnClearList_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(383, 91);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(54, 23);
            this.button1.TabIndex = 13;
            this.button1.Text = "刷新";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.BtnRefresh_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 416);
            this.Controls.Add(this.btnClearList);
            this.Controls.Add(this.btnRemoveSelected);
            this.Controls.Add(this.btnStartupAll);
            this.Controls.Add(this.btnShutdownAll);
            this.Controls.Add(this.btnStartupSelected);
            this.Controls.Add(this.btnShutdownSelected);
            this.Controls.Add(this.listViewComputers);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "局域网开关机工具";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtMacAddress;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtIp4;
        private System.Windows.Forms.TextBox txtIp3;
        private System.Windows.Forms.TextBox txtIp2;
        private System.Windows.Forms.TextBox txtIp1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView listViewComputers;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Button btnShutdownSelected;
        private System.Windows.Forms.Button btnStartupSelected;
        private System.Windows.Forms.Button btnShutdownAll;
        private System.Windows.Forms.Button btnStartupAll;
        private System.Windows.Forms.Button btnRemoveSelected;
        private System.Windows.Forms.Button btnClearList;
        private System.Windows.Forms.Button button1;
    }
}