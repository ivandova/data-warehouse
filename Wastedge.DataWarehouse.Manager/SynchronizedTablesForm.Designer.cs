namespace Wastedge.DataWarehouse.Manager
{
    partial class SynchronizedTablesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SynchronizedTablesForm));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this._synchronized = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this._available = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._add = new System.Windows.Forms.Button();
            this._remove = new System.Windows.Forms.Button();
            this._toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this._add, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this._remove, 1, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(9, 9);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 61F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(534, 408);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this._synchronized);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(8, 4, 8, 8);
            this.tableLayoutPanel1.SetRowSpan(this.groupBox1, 5);
            this.groupBox1.Size = new System.Drawing.Size(244, 402);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Synchronized tables";
            // 
            // _synchronized
            // 
            this._synchronized.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this._synchronized.Dock = System.Windows.Forms.DockStyle.Fill;
            this._synchronized.Location = new System.Drawing.Point(8, 17);
            this._synchronized.Name = "_synchronized";
            this._synchronized.Size = new System.Drawing.Size(228, 377);
            this._synchronized.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this._synchronized.TabIndex = 0;
            this._synchronized.UseCompatibleStateImageBehavior = false;
            this._synchronized.View = System.Windows.Forms.View.Details;
            this._synchronized.SelectedIndexChanged += new System.EventHandler(this._synchronized_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Table";
            this.columnHeader1.Width = 300;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this._available);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(287, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(8, 4, 8, 8);
            this.tableLayoutPanel1.SetRowSpan(this.groupBox2, 5);
            this.groupBox2.Size = new System.Drawing.Size(244, 402);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Available tables";
            // 
            // _available
            // 
            this._available.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
            this._available.Dock = System.Windows.Forms.DockStyle.Fill;
            this._available.Location = new System.Drawing.Point(8, 17);
            this._available.Name = "_available";
            this._available.Size = new System.Drawing.Size(228, 377);
            this._available.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this._available.TabIndex = 0;
            this._available.UseCompatibleStateImageBehavior = false;
            this._available.View = System.Windows.Forms.View.Details;
            this._available.SelectedIndexChanged += new System.EventHandler(this._available_SelectedIndexChanged);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Table";
            this.columnHeader2.Width = 300;
            // 
            // _add
            // 
            this._add.Image = global::Wastedge.DataWarehouse.Manager.NeutralResources.navigate_left;
            this._add.Location = new System.Drawing.Point(253, 64);
            this._add.Name = "_add";
            this._add.Size = new System.Drawing.Size(28, 28);
            this._add.TabIndex = 1;
            this._toolTip.SetToolTip(this._add, "Add a table to be synchronized");
            this._add.UseVisualStyleBackColor = true;
            this._add.Click += new System.EventHandler(this._add_Click);
            // 
            // _remove
            // 
            this._remove.Image = global::Wastedge.DataWarehouse.Manager.NeutralResources.navigate_right;
            this._remove.Location = new System.Drawing.Point(253, 106);
            this._remove.Name = "_remove";
            this._remove.Size = new System.Drawing.Size(28, 28);
            this._remove.TabIndex = 2;
            this._toolTip.SetToolTip(this._remove, "Stop synchronizing a table");
            this._remove.UseVisualStyleBackColor = true;
            this._remove.Click += new System.EventHandler(this._remove_Click);
            // 
            // SynchronizedTablesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(552, 426);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SynchronizedTablesForm";
            this.Padding = new System.Windows.Forms.Padding(9);
            this.Text = "{0} - Synchronized tables";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button _remove;
        private System.Windows.Forms.Button _add;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView _synchronized;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListView _available;
        private System.Windows.Forms.ToolTip _toolTip;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
    }
}