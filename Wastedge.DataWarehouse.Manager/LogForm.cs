using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SourceGrid;
using SourceGrid.Cells;
using Wastedge.DataWarehouse.Manager.Util;

namespace Wastedge.DataWarehouse.Manager
{
    public partial class LogForm : DockContent
    {
        private const int MaxCount = 100;

        private readonly SourceGrid.Cells.Views.Cell _defaultView;
        private readonly SourceGrid.Cells.Views.Cell _numberView;
        private DataWarehouseConnection _connection;
        private DateTime? _lastStart;

        public LogForm(Instance instance)
        {
            InitializeComponent();

            Text = String.Format(Text, instance.Configuration.InstanceName);

            var headerView = new SourceGrid.Cells.Views.ColumnHeader
            {
                ElementText = new DevAge.Drawing.VisualElements.TextRenderer()
            };

            _defaultView = new SourceGrid.Cells.Views.Cell
            {
                ElementText = new DevAge.Drawing.VisualElements.TextRenderer()
            };

            _numberView = new SourceGrid.Cells.Views.Cell
            {
                ElementText = new DevAge.Drawing.VisualElements.TextRenderer(),
                TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight
            };

            _grid.SelectionMode = GridSelectionMode.Row;
            _grid.ColumnsCount = 4;
            _grid.FixedRows = 1;

            _grid.Rows.Insert(0);

            _grid[0, 0] = new SourceGrid.Cells.ColumnHeader("Started")
            {
                View = headerView,
                AutomaticSortEnabled = false
            };

            _grid[0, 1] = new SourceGrid.Cells.ColumnHeader("Path")
            {
                View = headerView,
                AutomaticSortEnabled = false
            };

            _grid[0, 2] = new SourceGrid.Cells.ColumnHeader("Duration")
            {
                View = headerView,
                AutomaticSortEnabled = false
            };

            _grid[0, 3] = new SourceGrid.Cells.ColumnHeader("Records")
            {
                View = headerView,
                AutomaticSortEnabled = false
            };

            _connection = instance.Configuration.OpenConnection();
        }

        private void AddLogs(List<LogLine> logs)
        {
            if (logs.Count == 0)
                return;

            int row = _grid.RowsCount;
            _grid.RowsCount += logs.Count;

            foreach (var log in logs)
            {
                _grid[row, 0] = BuildCell(log.Start);
                _grid[row, 1] = BuildCell(log.Path);
                _grid[row, 2] = BuildCell(log.Duration);
                _grid[row, 3] = BuildCell(log.Records);

                row++;

                if (_lastStart == null || _lastStart.Value < log.Start)
                    _lastStart = log.Start;
            }

            _grid.AutoSizeCells(new Range(
                new Position(Math.Max(0, _grid.Rows.Count - 20), 0),
                new Position(_grid.Rows.Count - 1, _grid.ColumnsCount - 1)
            ));

            _grid.Selection.Focus(
                new Position(_grid.RowsCount - 1, 0),
                true
            );
        }

        private ICell BuildCell(object value)
        {
            if (value is DateTimeOffset)
                value = ((DateTimeOffset)value).LocalDateTime;
            if (value is DateTime)
                value = ((DateTime)value).ToShortDateString() + " " + ((DateTime)value).ToLongTimeString();

            if (value == null || value is string)
            {
                return new Cell(value)
                {
                    View = _defaultView
                };
            }
            if (value is int)
            {
                return new Cell(((int)value).ToString(CultureInfo.CurrentUICulture))
                {
                    View = _numberView
                };
            }

            throw new InvalidOperationException();
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            GetNextLog();
        }

        private void GetNextLog()
        {
            AddLogs(_connection.GetLog(_lastStart, MaxCount));
        }

        private void LogForm_Shown(object sender, EventArgs e)
        {
            AddLogs(_connection.GetLog(null, MaxCount));

            _timer.Start();
        }

        private void _refreshButton_Click(object sender, EventArgs e)
        {
            GetNextLog();
        }

        private void _clearLogButton_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                this,
                "Are you sure you want to delete all log entries?",
                Parent.FindForm().Text,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result != DialogResult.Yes)
                return;

            _grid.RowsCount = _grid.FixedRows;

            _connection.ClearLog();
        }
    }
}
