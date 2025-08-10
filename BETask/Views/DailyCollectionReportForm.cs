using BETask.Common;
using BETask.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BETask.BAL;
using EDMX = BETask.DAL.EDMX;
using System.Windows.Forms;

namespace BETask.Views
{
    public partial class DailyCollectionReportForm : Form
    {
        DailyCollectionReportButtonCollection button;
        DeliveryBAL deliveryBAL = new DeliveryBAL();
        public enum EnumFormEvents
        {
            FormLoad,
            New,
            Search,
            Cancel,
            Close,
            Print,
            Update,
            UpdateClose

        }
        public DailyCollectionReportForm()
        {
            InitializeComponent();
        }

        private void FormLoad()
        {
            button = new DailyCollectionReportButtonCollection
            {

                BtnCancel = btnCancel,
                BtnClose = btnClose,
                BtnSearch = btnSearch,
                BtnPrint = btnPrint,

            };
            GetAllEmployees();
            GetAllRoutes();
            General.BindPaymentModes(cmbPaymentMode);
            cmbPaymentMode.Text = "";

        }
        private void GetAllEmployees()
        {
            try
            {
                BAL.EmployeeBAL employeeBAL = new BAL.EmployeeBAL();
                List<EDMX.employee> _lstEmployee = employeeBAL.GetAllEmployee();
                foreach (EDMX.employee emp in _lstEmployee)
                {
                    ComboboxItem _cmbItem = new ComboboxItem()
                    {
                        Text = String.Format("{0} {1}", emp.first_name, emp.last_name),
                        Value = emp.employee_id
                    };
                    cmbEmployee.Items.Add(_cmbItem);
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }

        private void ButtonActive(Enum activeEvent)
        {

            switch (activeEvent)
            {
                case EnumFormEvents.FormLoad:

                    break;
                case EnumFormEvents.Cancel:
                    ButtonActive(EnumFormEvents.FormLoad);
                    General.ClearTextBoxes(this);
                    General.ClearGrid(gridCollection);

                    break;
                case EnumFormEvents.Close:
                    this.BeginInvoke(new MethodInvoker(Close));
                    break;
                case EnumFormEvents.Search:
                    Search();
                    break;
                case EnumFormEvents.Print:
                    Print();
                    break;
                default:
                    break;

            }
        }
        private void ButtonEvents(object sender, EventArgs e)
        {

            if (sender == button.BtnCancel)
            {
                ButtonActive(EnumFormEvents.Cancel);
            }

            else if (sender == button.BtnClose)
            {
                ButtonActive(EnumFormEvents.Close);

            }
            else if (sender == button.BtnSearch)
            {
                ButtonActive(EnumFormEvents.Search);
            }
            else if (sender == button.BtnPrint)
            {
                ButtonActive(EnumFormEvents.Print);
            }
        }

        private void Search()
        {
            try
            {
                General.ClearGrid(gridCollection);
                string paymentMode = "All";
                if (cmbPaymentMode.Text != "")
                {
                    if (General.ValidatePaymentModes(cmbPaymentMode.Text))
                        paymentMode = cmbPaymentMode.Text;
                }

                int employeeId = General.GetComboBoxSelectedValue(cmbEmployee);
                int customerId= General.GetComboBoxSelectedValue(cmbCustomer);
                int routeId = General.GetComboBoxSelectedValue(cmbRoute);

                var collection = deliveryBAL.GetCollectionReport(General.ConvertDateServerFormatWithStartTime(dtpFrom.Value), General.ConvertDateServerFormatWithStartTime(dtpDateTo.Value), routeId, employeeId,customerId,paymentMode);
                     
                gridCollection.SuspendLayout();
                List<DataGridViewRow> rows = new List<DataGridViewRow>();

                foreach (EDMX.daily_collection coll in collection)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(gridCollection, coll.delivery_id, coll.customer_id,General.ConvertDateTimeAppFormat(coll.delivery_time) ,coll.employee.first_name, coll.customer.route.route_name, coll.customer.customer_name, coll.payment_mode, coll.collected_amount,coll.daily_collection_id);
                    rows.Add(row);
                }
                General.GridRownumber(gridCollection);
                lblTotalCollection.Text ="Total Collection " +collection.Sum(x => x.collected_amount).ToString();

                gridCollection.Rows.AddRange(rows.ToArray()); // Add all rows at once

                gridCollection.ResumeLayout(); // Resume layout updates
                gridCollection.RowTemplate.Height = 60;
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);

            }
        }
        private void Print()
        {
            try {
                string paymentMode = "All";
                if (cmbPaymentMode.Text != "")
                {
                    if (General.ValidatePaymentModes(cmbPaymentMode.Text))
                        paymentMode = cmbPaymentMode.Text;
                }

                int employeeId = General.GetComboBoxSelectedValue(cmbEmployee);
                int customerId = General.GetComboBoxSelectedValue(cmbCustomer);

                deliveryBAL.PrintCollectionReport(General.ConvertDateServerFormatWithStartTime(dtpFrom.Value), General.ConvertDateServerFormatWithStartTime(dtpDateTo.Value), 0, employeeId, customerId, paymentMode);
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);

            }
        }
     
        private void cmbCustomer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData != Keys.Back && e.KeyData != Keys.Delete)
                CustomerSearch();
        }
        private void CustomerSearch()
        {
            CustomerSearchForm searchForm = new CustomerSearchForm();
            DialogResult result = searchForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                cmbCustomer.Items.Clear();
                ComboboxItem comboboxItem = new ComboboxItem {
                    Text = searchForm.CustomerName,
                    Value = searchForm.CustomerId,
                };
                cmbCustomer.Items.Add(comboboxItem);
                cmbCustomer.SelectedIndex = 0;
            }
        }

        private void DailyCollectionReportForm_Load(object sender, EventArgs e)
        {
            FormLoad();
        }

        private void GetAllRoutes()
        {
            try
            {
                RouteBAL routeBAL = new RouteBAL();
                List<EDMX.route> listRoute = routeBAL.GetAllRoutes();
                cmbRoute.Items.Clear();
                foreach (EDMX.route route in listRoute)
                {
                    ComboboxItem _cmbItem = new ComboboxItem()
                    {
                        Text = route.route_name,
                        Value = route.route_id
                    };
                    cmbRoute.Items.Add(_cmbItem);

                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }

    }
    class DailyCollectionReportButtonCollection
    {
        public Button BtnCancel { get; set; }
        public Button BtnClose { get; set; }
        public Button BtnSearch { get; set; }
        public Button BtnPrint { get; set; }

    }
}
