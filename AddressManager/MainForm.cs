using System;
using System.Collections.Generic;
using Wisej.Web;
using System.Data.SQLite;
using System.Drawing;

namespace AddressManager
{
    public partial class MainForm : Form
    {
        private DataContext _context;
        private TabControl _tabControl;
        private DataGridView _orgGridView;
        private DataGridView _staffGridView;
        private TextBox _orgNameTextBox;
        private TextBox _orgStreetTextBox;
        private TextBox _orgZipTextBox;
        private TextBox _orgCityTextBox;
        private TextBox _orgCountryTextBox;
        private TextBox _staffTitleTextBox;
        private TextBox _staffFirstNameTextBox;
        private TextBox _staffLastNameTextBox;
        private TextBox _staffPhoneTextBox;
        private TextBox _staffEmailTextBox;
        private ComboBox _staffOrgComboBox;
        private Button _addOrgButton;
        private Button _updateOrgButton;
        private Button _addStaffButton;
        private Button _updateStaffButton;
        private Button _deleteOrgButton;
         private Button _deleteStaffButton;
         private int _selectedOrganizationId = -1;
         private int _selectedStaffId = -1;
        public MainForm()
        {
            InitializeComponent();
            this.Text = "Address Manager";
            this.WindowState = FormWindowState.Maximized;
            this.Icon = new Icon(GetType(), "Resources.app.ico");
            _context = new DataContext();
            InitializeUI();
            LoadData();
             
        }

        private void InitializeUI()
        {
           
            _tabControl = new TabControl();
            _tabControl.Dock = DockStyle.Fill;
            _tabControl.TabPages.Add(new TabPage("Organizations"));
            _tabControl.TabPages.Add(new TabPage("Staff"));

            //------------------------------------------ Organizations UI ---------------------------------------
             Panel orgPanel = new Panel();
             orgPanel.Dock = DockStyle.Fill;
              _orgGridView = new DataGridView();
            _orgGridView.Dock = DockStyle.Fill;
            _orgGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _orgGridView.MultiSelect = false;
            _orgGridView.AllowUserToAddRows = false;
             _orgGridView.RowHeadersVisible = false;
            _orgGridView.CellClick += (sender, e) =>
            {
                  if (e.RowIndex >= 0)
                    {
                        var row = _orgGridView.Rows[e.RowIndex];
                         _selectedOrganizationId = int.Parse(row.Cells["Id"].Value.ToString());
                        _orgNameTextBox.Text = row.Cells["Name"].Value.ToString();
                        _orgStreetTextBox.Text = row.Cells["Street"].Value.ToString();
                        _orgZipTextBox.Text = row.Cells["Zip"].Value.ToString();
                        _orgCityTextBox.Text = row.Cells["City"].Value.ToString();
                        _orgCountryTextBox.Text = row.Cells["Country"].Value.ToString();
                         
                        // Enable the update and delete buttons
                        _updateOrgButton.Enabled = true;
                        _deleteOrgButton.Enabled = true;
                    }
            };
             _orgGridView.Columns.Add("Id", "Id");
            _orgGridView.Columns.Add("Name", "Name");
            _orgGridView.Columns.Add("Street", "Street");
            _orgGridView.Columns.Add("Zip", "Zip");
            _orgGridView.Columns.Add("City", "City");
            _orgGridView.Columns.Add("Country", "Country");
             _tabControl.TabPages[0].Controls.Add(orgPanel);
            orgPanel.Controls.Add(_orgGridView);
              Panel orgAddEditPanel = new Panel();
              orgAddEditPanel.Dock = DockStyle.Top;
              orgPanel.Controls.Add(orgAddEditPanel);
            
             Label orgNameLabel = new Label();
             orgNameLabel.Text = "Name";
              _orgNameTextBox = new TextBox();
             orgAddEditPanel.Controls.Add(orgNameLabel);
             orgAddEditPanel.Controls.Add(_orgNameTextBox);

            Label orgStreetLabel = new Label();
             orgStreetLabel.Text = "Street";
              _orgStreetTextBox = new TextBox();
             orgAddEditPanel.Controls.Add(orgStreetLabel);
              orgAddEditPanel.Controls.Add(_orgStreetTextBox);

            Label orgZipLabel = new Label();
            orgZipLabel.Text = "Zip";
            _orgZipTextBox = new TextBox();
            orgAddEditPanel.Controls.Add(orgZipLabel);
            orgAddEditPanel.Controls.Add(_orgZipTextBox);

             Label orgCityLabel = new Label();
             orgCityLabel.Text = "City";
            _orgCityTextBox = new TextBox();
            orgAddEditPanel.Controls.Add(orgCityLabel);
             orgAddEditPanel.Controls.Add(_orgCityTextBox);

            Label orgCountryLabel = new Label();
            orgCountryLabel.Text = "Country";
             _orgCountryTextBox = new TextBox();
             orgAddEditPanel.Controls.Add(orgCountryLabel);
             orgAddEditPanel.Controls.Add(_orgCountryTextBox);


             _addOrgButton = new Button();
             _addOrgButton.Text = "Add Organization";
            _addOrgButton.Click += AddOrganizationButton_Click;
             _updateOrgButton = new Button();
             _updateOrgButton.Text = "Update Organization";
              _updateOrgButton.Enabled = false;
             _updateOrgButton.Click += UpdateOrganizationButton_Click;
             _deleteOrgButton = new Button();
             _deleteOrgButton.Text = "Delete Organization";
             _deleteOrgButton.Enabled = false;
             _deleteOrgButton.Click += DeleteOrganizationButton_Click;

              orgAddEditPanel.Controls.Add(_addOrgButton);
              orgAddEditPanel.Controls.Add(_updateOrgButton);
               orgAddEditPanel.Controls.Add(_deleteOrgButton);

             orgNameLabel.Location = new Point(10, 10);
            _orgNameTextBox.Location = new Point(100, 10);
             orgStreetLabel.Location = new Point(10, 40);
            _orgStreetTextBox.Location = new Point(100, 40);
            orgZipLabel.Location = new Point(10, 70);
            _orgZipTextBox.Location = new Point(100, 70);
             orgCityLabel.Location = new Point(10, 100);
            _orgCityTextBox.Location = new Point(100, 100);
            orgCountryLabel.Location = new Point(10, 130);
            _orgCountryTextBox.Location = new Point(100, 130);

              _addOrgButton.Location = new Point(10, 170);
              _updateOrgButton.Location = new Point(150, 170);
              _deleteOrgButton.Location = new Point(300, 170);


            //------------------------------------------ Staff UI ---------------------------------------------
             Panel staffPanel = new Panel();
            staffPanel.Dock = DockStyle.Fill;
            _staffGridView = new DataGridView();
            _staffGridView.Dock = DockStyle.Fill;
             _staffGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _staffGridView.MultiSelect = false;
            _staffGridView.AllowUserToAddRows = false;
            _staffGridView.RowHeadersVisible = false;
             _staffGridView.CellClick += (sender, e) =>
            {
                if (e.RowIndex >= 0)
                {
                     var row = _staffGridView.Rows[e.RowIndex];
                     _selectedStaffId = int.Parse(row.Cells["Id"].Value.ToString());
                     _staffTitleTextBox.Text = row.Cells["Title"].Value.ToString();
                    _staffFirstNameTextBox.Text = row.Cells["FirstName"].Value.ToString();
                    _staffLastNameTextBox.Text = row.Cells["LastName"].Value.ToString();
                    _staffPhoneTextBox.Text = row.Cells["PhoneNumber"].Value.ToString();
                     _staffEmailTextBox.Text = row.Cells["Email"].Value.ToString();
                     _staffOrgComboBox.SelectedValue = int.Parse(row.Cells["OrganizationId"].Value.ToString());

                     _updateStaffButton.Enabled = true;
                     _deleteStaffButton.Enabled = true;
                 }

             };
             _staffGridView.Columns.Add("Id", "Id");
            _staffGridView.Columns.Add("Title", "Title");
            _staffGridView.Columns.Add("FirstName", "First Name");
            _staffGridView.Columns.Add("LastName", "Last Name");
            _staffGridView.Columns.Add("PhoneNumber", "Phone Number");
            _staffGridView.Columns.Add("Email", "Email");
             _staffGridView.Columns.Add("OrganizationId", "OrganizationId");
             _tabControl.TabPages[1].Controls.Add(staffPanel);
             staffPanel.Controls.Add(_staffGridView);
               Panel staffAddEditPanel = new Panel();
              staffAddEditPanel.Dock = DockStyle.Top;
             staffPanel.Controls.Add(staffAddEditPanel);

             Label staffTitleLabel = new Label();
            staffTitleLabel.Text = "Title";
            _staffTitleTextBox = new TextBox();
            staffAddEditPanel.Controls.Add(staffTitleLabel);
            staffAddEditPanel.Controls.Add(_staffTitleTextBox);

             Label staffFirstNameLabel = new Label();
             staffFirstNameLabel.Text = "First Name";
             _staffFirstNameTextBox = new TextBox();
             staffAddEditPanel.Controls.Add(staffFirstNameLabel);
             staffAddEditPanel.Controls.Add(_staffFirstNameTextBox);


             Label staffLastNameLabel = new Label();
            staffLastNameLabel.Text = "Last Name";
            _staffLastNameTextBox = new TextBox();
             staffAddEditPanel.Controls.Add(staffLastNameLabel);
             staffAddEditPanel.Controls.Add(_staffLastNameTextBox);

            Label staffPhoneLabel = new Label();
            staffPhoneLabel.Text = "Phone Number";
             _staffPhoneTextBox = new TextBox();
             staffAddEditPanel.Controls.Add(staffPhoneLabel);
             staffAddEditPanel.Controls.Add(_staffPhoneTextBox);

            Label staffEmailLabel = new Label();
            staffEmailLabel.Text = "Email";
            _staffEmailTextBox = new TextBox();
             staffAddEditPanel.Controls.Add(staffEmailLabel);
             staffAddEditPanel.Controls.Add(_staffEmailTextBox);

              Label staffOrgLabel = new Label();
              staffOrgLabel.Text = "Organization";
             _staffOrgComboBox = new ComboBox();
              staffAddEditPanel.Controls.Add(staffOrgLabel);
             staffAddEditPanel.Controls.Add(_staffOrgComboBox);


            _addStaffButton = new Button();
            _addStaffButton.Text = "Add Staff";
             _addStaffButton.Click += AddStaffButton_Click;
             _updateStaffButton = new Button();
             _updateStaffButton.Text = "Update Staff";
             _updateStaffButton.Enabled = false;
              _updateStaffButton.Click += UpdateStaffButton_Click;
             _deleteStaffButton = new Button();
             _deleteStaffButton.Text = "Delete Staff";
             _deleteStaffButton.Enabled = false;
            _deleteStaffButton.Click += DeleteStaffButton_Click;

             staffAddEditPanel.Controls.Add(_addStaffButton);
             staffAddEditPanel.Controls.Add(_updateStaffButton);
              staffAddEditPanel.Controls.Add(_deleteStaffButton);

             staffTitleLabel.Location = new Point(10, 10);
            _staffTitleTextBox.Location = new Point(100, 10);
             staffFirstNameLabel.Location = new Point(10, 40);
             _staffFirstNameTextBox.Location = new Point(100, 40);
             staffLastNameLabel.Location = new Point(10, 70);
            _staffLastNameTextBox.Location = new Point(100, 70);
            staffPhoneLabel.Location = new Point(10, 100);
            _staffPhoneTextBox.Location = new Point(100, 100);
            staffEmailLabel.Location = new Point(10, 130);
             _staffEmailTextBox.Location = new Point(100, 130);
              staffOrgLabel.Location = new Point(10, 160);
             _staffOrgComboBox.Location = new Point(100, 160);

             _addStaffButton.Location = new Point(10, 200);
            _updateStaffButton.Location = new Point(150, 200);
            _deleteStaffButton.Location = new Point(300, 200);
              this.Controls.Add(_tabControl);
        }

       private void LoadData()
        {
              LoadOrganizations();
              LoadStaff();
        }
        private void LoadOrganizations()
        {
            _orgGridView.Rows.Clear();
            List<Organization> organizations = _context.GetOrganizations();

            foreach (var org in organizations)
            {
                _orgGridView.Rows.Add(org.Id, org.Name, org.Street, org.Zip, org.City, org.Country);
            }
              _staffOrgComboBox.DataSource = organizations;
             _staffOrgComboBox.DisplayMember = "Name";
            _staffOrgComboBox.ValueMember = "Id";

        }
        private void LoadStaff()
        {
            _staffGridView.Rows.Clear();
           List<Staff> staffList = _context.GetStaff();
            foreach (var staff in staffList)
            {
               _staffGridView.Rows.Add(staff.Id, staff.Title, staff.FirstName, staff.LastName, staff.PhoneNumber, staff.Email, staff.OrganizationId);
             }
        }
        private void AddOrganizationButton_Click(object sender, EventArgs e)
        {
             string name = _orgNameTextBox.Text;
             string street = _orgStreetTextBox.Text;
             string zip = _orgZipTextBox.Text;
             string city = _orgCityTextBox.Text;
            string country = _orgCountryTextBox.Text;

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(street) || string.IsNullOrEmpty(zip) || string.IsNullOrEmpty(city) || string.IsNullOrEmpty(country))
             {
                AlertBox.Show("Please fill in all the organization details.");
                return;
            }

            Organization newOrg = new Organization
            {
                Name = name,
                Street = street,
               Zip = zip,
                City = city,
                Country = country,
           };
            _context.AddOrganization(newOrg);
            LoadOrganizations();
             // Clear textboxes
            _orgNameTextBox.Text = "";
            _orgStreetTextBox.Text = "";
             _orgZipTextBox.Text = "";
             _orgCityTextBox.Text = "";
            _orgCountryTextBox.Text = "";
        }

        private void UpdateOrganizationButton_Click(object sender, EventArgs e)
        {
            string name = _orgNameTextBox.Text;
            string street = _orgStreetTextBox.Text;
            string zip = _orgZipTextBox.Text;
            string city = _orgCityTextBox.Text;
            string country = _orgCountryTextBox.Text;

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(street) || string.IsNullOrEmpty(zip) || string.IsNullOrEmpty(city) || string.IsNullOrEmpty(country) || _selectedOrganizationId <=0)
            {
                AlertBox.Show("Please select an organization or fill in all the organization details.");
                return;
             }

             Organization updatedOrg = new Organization
            {
                Id = _selectedOrganizationId,
               Name = name,
                Street = street,
                Zip = zip,
                City = city,
                Country = country,
            };

            _context.UpdateOrganization(updatedOrg);
           LoadOrganizations();
            // Clear textboxes
            _orgNameTextBox.Text = "";
           _orgStreetTextBox.Text = "";
            _orgZipTextBox.Text = "";
           _orgCityTextBox.Text = "";
           _orgCountryTextBox.Text = "";
          _selectedOrganizationId = -1;
            _updateOrgButton.Enabled = false;
            _deleteOrgButton.Enabled = false;
        }

        private void DeleteOrganizationButton_Click(object sender, EventArgs e)
        {
            if (_selectedOrganizationId <= 0)
             {
                 AlertBox.Show("Please select an organization to delete.");
                 return;
            }
            _context.DeleteOrganization(_selectedOrganizationId);
            LoadOrganizations();
              // Clear textboxes
            _orgNameTextBox.Text = "";
            _orgStreetTextBox.Text = "";
            _orgZipTextBox.Text = "";
             _orgCityTextBox.Text = "";
             _orgCountryTextBox.Text = "";
             _selectedOrganizationId = -1;
            _updateOrgButton.Enabled = false;
             _deleteOrgButton.Enabled = false;
        }


        private void AddStaffButton_Click(object sender, EventArgs e)
        {
            string title = _staffTitleTextBox.Text;
           string firstName = _staffFirstNameTextBox.Text;
            string lastName = _staffLastNameTextBox.Text;
           string phone = _staffPhoneTextBox.Text;
            string email = _staffEmailTextBox.Text;

            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(email) || _staffOrgComboBox.SelectedValue == null)
            {
               AlertBox.Show("Please fill in all the staff details.");
               return;
            }

           Staff newStaff = new Staff
            {
               Title = title,
               FirstName = firstName,
                LastName = lastName,
                PhoneNumber = phone,
               Email = email,
               OrganizationId = (int)_staffOrgComboBox.SelectedValue,
            };

           _context.AddStaff(newStaff);
            LoadStaff();
             // Clear textboxes
            _staffTitleTextBox.Text = "";
            _staffFirstNameTextBox.Text = "";
           _staffLastNameTextBox.Text = "";
            _staffPhoneTextBox.Text = "";
            _staffEmailTextBox.Text = "";
            _staffOrgComboBox.SelectedIndex = -1;
        }


        private void UpdateStaffButton_Click(object sender, EventArgs e)
        {
            string title = _staffTitleTextBox.Text;
            string firstName = _staffFirstNameTextBox.Text;
            string lastName = _staffLastNameTextBox.Text;
            string phone = _staffPhoneTextBox.Text;
            string email = _staffEmailTextBox.Text;

            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(email) || _staffOrgComboBox.SelectedValue == null || _selectedStaffId <= 0)
            {
                 AlertBox.Show("Please select staff or fill in all the staff details.");
                 return;
            }

           Staff updatedStaff = new Staff
           {
                Id = _selectedStaffId,
                Title = title,
               FirstName = firstName,
                LastName = lastName,
                PhoneNumber = phone,
                Email = email,
                OrganizationId = (int)_staffOrgComboBox.SelectedValue
            };
            _context.UpdateStaff(updatedStaff);
             LoadStaff();
           // Clear textboxes
            _staffTitleTextBox.Text = "";
           _staffFirstNameTextBox.Text = "";
           _staffLastNameTextBox.Text = "";
           _staffPhoneTextBox.Text = "";
             _staffEmailTextBox.Text = "";
            _staffOrgComboBox.SelectedIndex = -1;
            _selectedStaffId = -1;
           _updateStaffButton.Enabled = false;
            _deleteStaffButton.Enabled = false;
        }

        private void DeleteStaffButton_Click(object sender, EventArgs e)
        {
             if (_selectedStaffId <= 0)
             {
                AlertBox.Show("Please select a staff member to delete.");
                 return;
            }
           _context.DeleteStaff(_selectedStaffId);
            LoadStaff();
              // Clear textboxes
            _staffTitleTextBox.Text = "";
            _staffFirstNameTextBox.Text = "";
            _staffLastNameTextBox.Text = "";
            _staffPhoneTextBox.Text = "";
            _staffEmailTextBox.Text = "";
            _staffOrgComboBox.SelectedIndex = -1;
             _selectedStaffId = -1;
            _updateStaffButton.Enabled = false;
            _deleteStaffButton.Enabled = false;
        }
    }
}