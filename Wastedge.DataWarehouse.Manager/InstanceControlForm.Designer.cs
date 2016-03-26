namespace Wastedge.DataWarehouse.Manager
{
    partial class InstanceControlForm
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this._task = new System.Windows.Forms.Label();
            this._service = new System.Windows.Forms.Label();
            this._progressBar = new System.Windows.Forms.ProgressBar();
            this._acceptButton = new System.Windows.Forms.Button();
            this._timer = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this._task, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this._service, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this._progressBar, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this._acceptButton, 1, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(9, 9);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(403, 138);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // _task
            // 
            this._task.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this._task, 3);
            this._task.Dock = System.Windows.Forms.DockStyle.Fill;
            this._task.Location = new System.Drawing.Point(6, 6);
            this._task.Margin = new System.Windows.Forms.Padding(6);
            this._task.Name = "_task";
            this._task.Size = new System.Drawing.Size(391, 13);
            this._task.TabIndex = 0;
            this._task.Text = "<busy>";
            // 
            // _service
            // 
            this._service.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this._service, 3);
            this._service.Dock = System.Windows.Forms.DockStyle.Fill;
            this._service.Location = new System.Drawing.Point(6, 31);
            this._service.Margin = new System.Windows.Forms.Padding(6);
            this._service.Name = "_service";
            this._service.Size = new System.Drawing.Size(391, 13);
            this._service.TabIndex = 1;
            this._service.Text = "<service>";
            // 
            // _progressBar
            // 
            this.tableLayoutPanel1.SetColumnSpan(this._progressBar, 3);
            this._progressBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this._progressBar.Location = new System.Drawing.Point(6, 80);
            this._progressBar.Margin = new System.Windows.Forms.Padding(6);
            this._progressBar.Maximum = 30;
            this._progressBar.Name = "_progressBar";
            this._progressBar.Size = new System.Drawing.Size(391, 23);
            this._progressBar.Step = 1;
            this._progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this._progressBar.TabIndex = 2;
            // 
            // _acceptButton
            // 
            this._acceptButton.Location = new System.Drawing.Point(164, 112);
            this._acceptButton.Name = "_acceptButton";
            this._acceptButton.Size = new System.Drawing.Size(75, 23);
            this._acceptButton.TabIndex = 3;
            this._acceptButton.Text = "C&lose";
            this._acceptButton.UseVisualStyleBackColor = true;
            // 
            // _timer
            // 
            this._timer.Interval = 1000;
            this._timer.Tick += new System.EventHandler(this._timer_Tick);
            // 
            // InstanceControlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(421, 156);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InstanceControlForm";
            this.Padding = new System.Windows.Forms.Padding(9);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Instance Control";
            this.Shown += new System.EventHandler(this.InstanceControlForm_Shown);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label _task;
        private System.Windows.Forms.Label _service;
        private System.Windows.Forms.ProgressBar _progressBar;
        private System.Windows.Forms.Button _acceptButton;
        private System.Windows.Forms.Timer _timer;
    }
}