namespace Wastedge.DataWarehouse.Manager
{
    partial class LogForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._refreshButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._clearLogButton = new System.Windows.Forms.ToolStripButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this._grid = new SourceGrid.Grid();
            this._timer = new System.Windows.Forms.Timer(this.components);
            this.toolStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._refreshButton,
            this.toolStripSeparator1,
            this._clearLogButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(284, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _refreshButton
            // 
            this._refreshButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._refreshButton.Image = global::Wastedge.DataWarehouse.Manager.NeutralResources.refresh;
            this._refreshButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._refreshButton.Name = "_refreshButton";
            this._refreshButton.Size = new System.Drawing.Size(23, 22);
            this._refreshButton.Text = "Refresh";
            this._refreshButton.Click += new System.EventHandler(this._refreshButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // _clearLogButton
            // 
            this._clearLogButton.Image = global::Wastedge.DataWarehouse.Manager.NeutralResources.delete;
            this._clearLogButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._clearLogButton.Name = "_clearLogButton";
            this._clearLogButton.Size = new System.Drawing.Size(77, 22);
            this._clearLogButton.Text = "Clear Log";
            this._clearLogButton.Click += new System.EventHandler(this._clearLogButton_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel2.Controls.Add(this._grid);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 25);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(1);
            this.panel2.Size = new System.Drawing.Size(284, 236);
            this.panel2.TabIndex = 5;
            // 
            // _grid
            // 
            this._grid.BackColor = System.Drawing.SystemColors.Window;
            this._grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._grid.EnableSort = true;
            this._grid.Location = new System.Drawing.Point(1, 1);
            this._grid.Name = "_grid";
            this._grid.OptimizeMode = SourceGrid.CellOptimizeMode.ForRows;
            this._grid.SelectionMode = SourceGrid.GridSelectionMode.Cell;
            this._grid.Size = new System.Drawing.Size(282, 234);
            this._grid.TabIndex = 0;
            this._grid.TabStop = true;
            this._grid.ToolTipText = "";
            // 
            // _timer
            // 
            this._timer.Interval = 5000;
            this._timer.Tick += new System.EventHandler(this._timer_Tick);
            // 
            // LogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LogForm";
            this.Text = "{0} - Log";
            this.Shown += new System.EventHandler(this.LogForm_Shown);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton _refreshButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton _clearLogButton;
        private System.Windows.Forms.Panel panel2;
        private SourceGrid.Grid _grid;
        private System.Windows.Forms.Timer _timer;
    }
}