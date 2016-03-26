namespace Wastedge.DataWarehouse.Manager
{
    partial class EditInstanceForm
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
            this._formHeader = new SystemEx.Windows.Forms.FormHeader();
            this.formFlowFooter1 = new SystemEx.Windows.Forms.FormFlowFooter();
            this._cancelButton = new System.Windows.Forms.Button();
            this._acceptButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this._untrackedUpdateInterval = new SystemEx.Windows.Forms.SimpleNumericTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this._password = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this._username = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this._tenant = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this._url = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this._provider = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this._connectionString = new System.Windows.Forms.TextBox();
            this._trackedUpdateInterval = new SystemEx.Windows.Forms.SimpleNumericTextBox();
            this._toolTip = new System.Windows.Forms.ToolTip(this.components);
            this._errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.formFlowFooter1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // _formHeader
            // 
            this._formHeader.Location = new System.Drawing.Point(0, 0);
            this._formHeader.Name = "_formHeader";
            this._formHeader.Size = new System.Drawing.Size(522, 47);
            this._formHeader.SubText = "An instance defines a connection between a Wastdge server and credentials, and a " +
    "database.";
            this._formHeader.TabIndex = 0;
            this._formHeader.Text = "Edit an instance";
            // 
            // formFlowFooter1
            // 
            this.formFlowFooter1.Controls.Add(this._cancelButton);
            this.formFlowFooter1.Controls.Add(this._acceptButton);
            this.formFlowFooter1.Location = new System.Drawing.Point(0, 364);
            this.formFlowFooter1.Name = "formFlowFooter1";
            this.formFlowFooter1.Size = new System.Drawing.Size(522, 45);
            this.formFlowFooter1.TabIndex = 1;
            // 
            // _cancelButton
            // 
            this._cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._cancelButton.Location = new System.Drawing.Point(426, 11);
            this._cancelButton.Name = "_cancelButton";
            this._cancelButton.Size = new System.Drawing.Size(75, 23);
            this._cancelButton.TabIndex = 0;
            this._cancelButton.Text = "Cancel";
            this._cancelButton.UseVisualStyleBackColor = true;
            // 
            // _acceptButton
            // 
            this._acceptButton.Location = new System.Drawing.Point(345, 11);
            this._acceptButton.Name = "_acceptButton";
            this._acceptButton.Size = new System.Drawing.Size(75, 23);
            this._acceptButton.TabIndex = 1;
            this._acceptButton.Text = "OK";
            this._acceptButton.UseVisualStyleBackColor = true;
            this._acceptButton.Click += new System.EventHandler(this._acceptButton_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 47);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(9);
            this.panel1.Size = new System.Drawing.Size(522, 317);
            this.panel1.TabIndex = 2;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this._untrackedUpdateInterval, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.label8, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this._password, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this._username, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this._tenant, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this._url, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this._provider, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this._connectionString, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this._trackedUpdateInterval, 1, 6);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(9, 9);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 8;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(504, 299);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // _untrackedUpdateInterval
            // 
            this._untrackedUpdateInterval.Dock = System.Windows.Forms.DockStyle.Fill;
            this._untrackedUpdateInterval.Location = new System.Drawing.Point(142, 276);
            this._untrackedUpdateInterval.Margin = new System.Windows.Forms.Padding(3, 3, 24, 3);
            this._untrackedUpdateInterval.Name = "_untrackedUpdateInterval";
            this._untrackedUpdateInterval.NumberScale = 0;
            this._untrackedUpdateInterval.Size = new System.Drawing.Size(338, 20);
            this._untrackedUpdateInterval.TabIndex = 17;
            this._toolTip.SetToolTip(this._untrackedUpdateInterval, "The interval in seconds to update tables that cannot be updated incrementally");
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Location = new System.Drawing.Point(3, 273);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(133, 26);
            this.label8.TabIndex = 16;
            this.label8.Text = "Untracked update interval:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Location = new System.Drawing.Point(3, 247);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(133, 26);
            this.label7.TabIndex = 14;
            this.label7.Text = "Tracked update interval:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _password
            // 
            this._password.Dock = System.Windows.Forms.DockStyle.Fill;
            this._password.Location = new System.Drawing.Point(142, 224);
            this._password.Margin = new System.Windows.Forms.Padding(3, 3, 24, 3);
            this._password.Name = "_password";
            this._password.Size = new System.Drawing.Size(338, 20);
            this._password.TabIndex = 13;
            this._toolTip.SetToolTip(this._password, "The password used to connect with Wastedge");
            this._password.UseSystemPasswordChar = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Location = new System.Drawing.Point(3, 221);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(133, 26);
            this.label6.TabIndex = 12;
            this.label6.Text = "Password:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _username
            // 
            this._username.Dock = System.Windows.Forms.DockStyle.Fill;
            this._username.Location = new System.Drawing.Point(142, 198);
            this._username.Margin = new System.Windows.Forms.Padding(3, 3, 24, 3);
            this._username.Name = "_username";
            this._username.Size = new System.Drawing.Size(338, 20);
            this._username.TabIndex = 11;
            this._toolTip.SetToolTip(this._username, "The username used to connect with Wastedge");
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(3, 195);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(133, 26);
            this.label5.TabIndex = 10;
            this.label5.Text = "User name:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _tenant
            // 
            this._tenant.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tenant.Location = new System.Drawing.Point(142, 172);
            this._tenant.Margin = new System.Windows.Forms.Padding(3, 3, 24, 3);
            this._tenant.Name = "_tenant";
            this._tenant.Size = new System.Drawing.Size(338, 20);
            this._tenant.TabIndex = 9;
            this._toolTip.SetToolTip(this._tenant, "The tenant ID or company ID");
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(3, 169);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(133, 26);
            this.label4.TabIndex = 8;
            this.label4.Text = "Tenant:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _url
            // 
            this._url.Dock = System.Windows.Forms.DockStyle.Fill;
            this._url.Location = new System.Drawing.Point(142, 146);
            this._url.Margin = new System.Windows.Forms.Padding(3, 3, 24, 3);
            this._url.Name = "_url";
            this._url.Size = new System.Drawing.Size(338, 20);
            this._url.TabIndex = 7;
            this._toolTip.SetToolTip(this._url, "The URL to Wastedge, e.g. http://www.wastedge.com");
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(3, 143);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(133, 26);
            this.label3.TabIndex = 6;
            this.label3.Text = "Wastedge URL:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 27);
            this.label1.TabIndex = 2;
            this.label1.Text = "Provider:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _provider
            // 
            this._provider.Dock = System.Windows.Forms.DockStyle.Fill;
            this._provider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._provider.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this._provider.FormattingEnabled = true;
            this._provider.Location = new System.Drawing.Point(142, 3);
            this._provider.Margin = new System.Windows.Forms.Padding(3, 3, 24, 3);
            this._provider.Name = "_provider";
            this._provider.Size = new System.Drawing.Size(338, 21);
            this._provider.TabIndex = 3;
            this._toolTip.SetToolTip(this._provider, "The type of database to connect to");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 32);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Connection string:";
            this._toolTip.SetToolTip(this.label2, "The connection string used to connect to the database");
            // 
            // _connectionString
            // 
            this._connectionString.Dock = System.Windows.Forms.DockStyle.Fill;
            this._connectionString.Location = new System.Drawing.Point(142, 30);
            this._connectionString.Margin = new System.Windows.Forms.Padding(3, 3, 24, 3);
            this._connectionString.Multiline = true;
            this._connectionString.Name = "_connectionString";
            this._connectionString.Size = new System.Drawing.Size(338, 110);
            this._connectionString.TabIndex = 5;
            this._toolTip.SetToolTip(this._connectionString, "The connection string used to connect to the database");
            // 
            // _trackedUpdateInterval
            // 
            this._trackedUpdateInterval.Dock = System.Windows.Forms.DockStyle.Fill;
            this._trackedUpdateInterval.Location = new System.Drawing.Point(142, 250);
            this._trackedUpdateInterval.Margin = new System.Windows.Forms.Padding(3, 3, 24, 3);
            this._trackedUpdateInterval.Name = "_trackedUpdateInterval";
            this._trackedUpdateInterval.NumberScale = 0;
            this._trackedUpdateInterval.Size = new System.Drawing.Size(338, 20);
            this._trackedUpdateInterval.TabIndex = 15;
            this._toolTip.SetToolTip(this._trackedUpdateInterval, "The interval in seconds to update tables that can be updated incrementally");
            // 
            // _errorProvider
            // 
            this._errorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this._errorProvider.ContainerControl = this;
            // 
            // EditInstanceForm
            // 
            this.AcceptButton = this._acceptButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._cancelButton;
            this.ClientSize = new System.Drawing.Size(522, 409);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.formFlowFooter1);
            this.Controls.Add(this._formHeader);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditInstanceForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit Instance";
            this.formFlowFooter1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SystemEx.Windows.Forms.FormHeader _formHeader;
        private SystemEx.Windows.Forms.FormFlowFooter formFlowFooter1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button _cancelButton;
        private System.Windows.Forms.Button _acceptButton;
        private SystemEx.Windows.Forms.SimpleNumericTextBox _untrackedUpdateInterval;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ToolTip _toolTip;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox _password;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox _username;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox _tenant;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox _url;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox _provider;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox _connectionString;
        private SystemEx.Windows.Forms.SimpleNumericTextBox _trackedUpdateInterval;
        private System.Windows.Forms.ErrorProvider _errorProvider;
    }
}