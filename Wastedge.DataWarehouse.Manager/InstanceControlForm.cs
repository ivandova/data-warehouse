using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Wastedge.DataWarehouse.Manager
{
    public partial class InstanceControlForm : SystemEx.Windows.Forms.Form
    {
        private readonly Instance _instance;
        private InstanceControlAction _control;

        public InstanceControlForm(Instance instance, InstanceControlAction control)
        {
            if (instance == null)
                throw new ArgumentNullException(nameof(instance));
            if (!Enum.IsDefined(typeof(InstanceControlAction), control))
                throw new ArgumentOutOfRangeException(nameof(control));

            _instance = instance;
            _control = control;

            InitializeComponent();

            UpdateLabels();
        }

        private void UpdateLabels()
        {
            _service.Text = _instance.Configuration.InstanceName;

            if (_control == InstanceControlAction.Start)
                _task.Text = "Attempting to start the following service...";
            else
                _task.Text = "Attempting to stop the following service...";
        }

        private void InstanceControlForm_Shown(object sender, EventArgs e)
        {
            Begin();
        }

        private void Begin()
        {
            _timer.Stop();

            _progressBar.Value = 1;

            try
            {
                if (_control == InstanceControlAction.Start)
                    _instance.Start();
                else
                    _instance.Stop();

                _timer.Start();
            }
            catch (Exception ex)
            {
                var sb = new StringBuilder()
                    .AppendLine("Could not complete the operation:")
                    .AppendLine()
                    .AppendLine(ex.Message);

                if (ex.InnerException != null)
                    sb.AppendLine().AppendLine(ex.InnerException.Message);

                MessageBox.Show(
                    this,
                    sb.ToString(),
                    Text,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                Dispose();
            }
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            _progressBar.Value++;

            _instance.RefreshStatus();
            var status = _instance.Status.Value;

            bool done;

            if (_control == InstanceControlAction.Start)
                done = status == ServiceControllerStatus.Running;
            else
                done = status == ServiceControllerStatus.Stopped;

            if (done)
            {
                _timer.Stop();

                if (_control == InstanceControlAction.Restart)
                {
                    _control = InstanceControlAction.Start;

                    UpdateLabels();

                    Begin();
                }
                else
                {
                    Dispose();
                }
                return;
            }

            if (
                _control == InstanceControlAction.Start &&
                status == ServiceControllerStatus.Stopped
            )
            {
                _timer.Stop();

                MessageBox.Show(
                    this,
                    "The service did not start. Please review the event log for a possible cause.",
                    Text,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                Dispose();
                return;
            }

            if (_progressBar.Value >= _progressBar.Maximum)
            {
                _timer.Stop();

                MessageBox.Show(
                    this,
                    "The service did not start in time. Please review the event log for a possible cause.",
                    Text,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                Dispose();
            }
        }
    }
}
