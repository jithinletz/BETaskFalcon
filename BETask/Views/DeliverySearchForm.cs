using BETask.Common;
using BETask.BAL;
using BETask.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using EDMX = BETask.DAL.EDMX;

namespace BETask.Views
{
    public partial class DeliverySearchForm : Form
    {
        public enum EnumFormEvents
        {
            FormLoad,
            Cancel,
            Close,
            Search,
        }
        public int deleId = 0;
        DeliveryBAL deleBAL = new DeliveryBAL();
        BAL.CustomerBAL _customerBAL = new BAL.CustomerBAL();
        List<EDMX.employee> _lstEmployee = null;
        DeliverySearchButtonCollection button ;


        public DeliverySearchForm()
        {
            InitializeComponent();
        }
        private void ButtonActive(Enum activeEvent)
        {

            switch (activeEvent)
            {
                case EnumFormEvents.FormLoad:
                    Search();
                    break;
                case EnumFormEvents.Cancel:
                    ButtonActive(EnumFormEvents.FormLoad);
                    cmbEmployee.Text = string.Empty;
                    break;
                case EnumFormEvents.Close:
                    this.BeginInvoke(new MethodInvoker(Close));
                    break;
                case EnumFormEvents.Search:
                    Search();
                    break;

                default:
                    break;

            }
        }
        private void ButtonEvents(object sender, EventArgs e)
        {
            if (sender == button.BtnSearch)
            {
                ButtonActive(EnumFormEvents.Search);
            }
            else if (sender == button.BtnCancel)
            {
                ButtonActive(EnumFormEvents.Cancel);
            }

            else if (sender == button.BtnClose)
            {
                ButtonActive(EnumFormEvents.Close);
            }
        }
        private void submitItem()
        {
            try
            {
                if (gridDelivery.Rows.Count > 0)
                {
                    int ridx = gridDelivery.CurrentRow.Index;
                    int _deleId = 0;
                    int.TryParse(gridDelivery[ridx, 0].Value.ToString(), out _deleId);
                    deleId = _deleId;
                    this.DialogResult = DialogResult.OK;
                    try
                    {
                        this.Close();
                    }
                    catch
                    {
                        this.BeginInvoke(new MethodInvoker(Close));

                    }
                }
            }
            catch (Exception ee)
            {
                General.ShowMessage(General.EnumMessageTypes.Error);
                this.BeginInvoke(new MethodInvoker(Close));
            }
        }
        private void GetAllEmployees()
        {
            try
            {
                EmployeeBAL employeeBAL = new EmployeeBAL();
                _lstEmployee = employeeBAL.GetAllEmployee();
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
        private void Search()
        {
            try
            {

                General.ClearGrid(gridDelivery);

                int empId = 0;
                if (cmbEmployee.Text != "")
                {
                    Object selectedEmployee = cmbEmployee.SelectedItem;
                     empId = (int)((BETask.Views.ComboboxItem)selectedEmployee).Value;
                }
                
                List<EDMX.delivery> listDelivery = deleBAL.SearchDelivery(General.ConvertDateServerFormat(dtpDateFrom.Value), General.ConvertDateServerFormat(dtpDateTo.Value), empId);
                foreach (EDMX.delivery delivery in listDelivery)
                {
                    gridDelivery.Rows.Add(delivery.delivery_id, delivery.employee.first_name, General.ConvertDateAppFormat(delivery.delivery_date), delivery.delivery_route, delivery.customer_count, delivery.vehicle_no);
                }
                
            }
            catch (Exception ee)
            {
                General.ShowMessage(General.EnumMessageTypes.Error);
                this.BeginInvoke(new MethodInvoker(Close));
            }
            
        }
        private void gridDelivery_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0 && e.ColumnIndex>=0)
            {
                try
                {
                    int _deleId = 0;
                    int.TryParse(gridDelivery[0,e.RowIndex].Value.ToString(), out _deleId);
                    deleId = _deleId;
                    this.DialogResult = DialogResult.OK;
                    try
                    {
                        this.Close();
                    }
                    catch
                    {
                        this.BeginInvoke(new MethodInvoker(Close));

                    }
                }
                catch (Exception ee)
                {
                    General.ShowMessage(General.EnumMessageTypes.Error);
                    this.BeginInvoke(new MethodInvoker(Close));
                }
            }

        }
     

        private void gridPurchase_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                submitItem();
            }
        }

        private void PurchaseSearchForm_Load(object sender, EventArgs e)
        {
            button = new DeliverySearchButtonCollection()
            {
                BtnSearch = btnSearch,
                BtnCancel = btnCancel,
                BtnClose = btnClose
            };
            Search();
            GetAllEmployees();

        }
        class DeliverySearchButtonCollection
        {
            public Button BtnSearch { get; set; }
            public Button BtnCancel { get; set; }
            public Button BtnClose { get; set; }
          
        }
    }
}
