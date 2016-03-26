using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Wastedge.DataWarehouse.Manager.Util;

namespace Wastedge.DataWarehouse.Manager
{
    public partial class EditInstanceForm : SystemEx.Windows.Forms.Form
    {
        public DataWarehouseConfiguration Configuration { get; private set; }

        public EditInstanceForm()
            : this(new DataWarehouseConfiguration())
        {
        }

        public EditInstanceForm(DataWarehouseConfiguration configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            Configuration = configuration;

            InitializeComponent();

            if (Configuration.InstanceId == 0)
            {
                Text = "New Instance";
                _formHeader.Text = "Create an instance";
            }

            _provider.Items.Add("");

            foreach (DataWarehouseProvider item in Enum.GetValues(typeof(DataWarehouseProvider)))
            {
                _provider.Items.Add(new ComboBoxValue<DataWarehouseProvider>(item, item.GetDescription()));
            }

            _provider.SelectedItem = Configuration.Provider;
            _connectionString.Text = Configuration.ConnectionString;
            _url.Text = Configuration.Url;
            _tenant.Text = Configuration.Tenant;
            _username.Text = Configuration.Username;
            _password.Text = Configuration.Password;
            _trackedUpdateInterval.Value = Configuration.TrackedTableInterval;
            _untrackedUpdateInterval.Value = Configuration.UntrackedTableInterval;
        }

        private void _acceptButton_Click(object sender, EventArgs e)
        {
            _errorProvider.Clear();
            bool valid = true;

            if (_provider.SelectedIndex <= 0)
            {
                _errorProvider.SetError(_provider, "Provider is required");
                valid = false;
            }
            if (_connectionString.Text.Length == 0)
            {
                _errorProvider.SetError(_connectionString, "Connection string is required");
                valid = false;
            }
            if (_url.Text.Length == 0)
            {
                _errorProvider.SetError(_url, "Wastedge URL is required");
                valid = false;
            }
            if (_tenant.Text.Length == 0)
            {
                _errorProvider.SetError(_tenant, "Tenant is required");
                valid = false;
            }
            if (_username.Text.Length == 0)
            {
                _errorProvider.SetError(_username, "Username is required");
                valid = false;
            }
            if (_password.Text.Length == 0)
            {
                _errorProvider.SetError(_password, "Password is required");
                valid = false;
            }
            if (_trackedUpdateInterval.Value == null)
            {
                _errorProvider.SetError(_trackedUpdateInterval, "Tracked update interval is required");
                valid = false;
            }
            if (_untrackedUpdateInterval.Value == null)
            {
                _errorProvider.SetError(_untrackedUpdateInterval, "Untracked update interval is required");
                valid = false;
            }

            if (!valid)
                return;

            Configuration.Provider = (_provider.SelectedItem as ComboBoxValue<DataWarehouseProvider>)?.Value;
            Configuration.ConnectionString = _connectionString.Text;
            Configuration.Url = _url.Text;
            Configuration.Tenant = _tenant.Text;
            Configuration.Username = _username.Text;
            Configuration.Password = _password.Text;
            Configuration.TrackedTableInterval = (int)_trackedUpdateInterval.Value;
            Configuration.UntrackedTableInterval = (int)_untrackedUpdateInterval.Value;

            Configuration.Save(Program.BaseKey);

            DialogResult = DialogResult.OK;
        }
    }
}
