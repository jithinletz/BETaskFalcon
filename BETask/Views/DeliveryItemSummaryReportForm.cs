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
    public partial class DeliveryItemSummaryReportForm : Form
    {
        public enum EnumFormEvents
        {
            FormLoad,
            Cancel,
            Close,
            Search,
            Print
        }
        public int saleId = 0;
        SaleBAL saleBAL = new SaleBAL();
        BAL.DeliveryBAL deliveryBAL = new BAL.DeliveryBAL();
        List<EDMX.employee> _lstEmployee = null;
        DeliveryItemSummaryReportButtonCollection button ;


        public DeliveryItemSummaryReportForm()
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
                case EnumFormEvents.Print:
                    Print();
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
            else if (sender == button.BtnPrint)
            {
                ButtonActive(EnumFormEvents.Print);
            }
        }
        private void submitItem()
        {
            try
            {
                if (gridDelivery.Rows.Count > 0)
                {
                    int ridx = gridDelivery.CurrentRow.Index;
                    int _purchaseId = 0;
                    int.TryParse(gridDelivery[ridx, 0].Value.ToString(), out _purchaseId);
                    saleId = _purchaseId;
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
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error);
                this.BeginInvoke(new MethodInvoker(Close));
            }
        }
        private void GetAllEmployees()
        {
            try
            {
                BAL.EmployeeBAL employeeBAL = new BAL.EmployeeBAL();
                _lstEmployee = employeeBAL.GetAllEmployee();
                foreach (EDMX.employee emp in _lstEmployee)
                {
                    string routeName = emp.route_id!=null? $"({emp.route.route_name})":"";
                    ComboboxItem _cmbItem = new ComboboxItem()
                    {
                       
                        Text = String.Format("{0} {1} {2}", emp.first_name, emp.last_name,routeName),
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
                if (!String.IsNullOrEmpty(cmbEmployee.Text))
                {
                    Object selectedEmployee = cmbEmployee.SelectedItem;
                    empId = (int)((BETask.Views.ComboboxItem)selectedEmployee).Value;
                }

                List<DAL.Model.DeliveryItemSummaryModel> listdelivery = deliveryBAL.GetDeliveryItemSumamry(General.ConvertDateServerFormat(dtpDateFrom.Value), General.ConvertDateServerFormat(dtpDateTo.Value), empId);

                foreach (DAL.Model.DeliveryItemSummaryModel delivery in listdelivery)
                {

                    gridDelivery.Rows.Add(delivery.ItemName, delivery.DeleveredQty, delivery.Foc, delivery.TotalQty,delivery.Amount);
                }
                gridDelivery.Rows.Add("Total", listdelivery.Sum(x => x.DeleveredQty), listdelivery.Sum(x => x.Foc), listdelivery.Sum(x => x.TotalQty), listdelivery.Sum(x => x.Amount));
                General.GridBackcolorYellow(gridDelivery);

            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error);

            }

        }

        private void Print()
        {
            try
            {
                int empId = 0;
                if (!String.IsNullOrEmpty(cmbEmployee.Text))
                {
                    Object selectedEmployee = cmbEmployee.SelectedItem;
                    empId = (int)((BETask.Views.ComboboxItem)selectedEmployee).Value;
                }
                string header = $"Date between {dtpDateFrom.Text} and {dtpDateTo.Text} . {cmbEmployee.Text}";
                deliveryBAL.PrintDeliverySummary(General.ConvertDateServerFormat(dtpDateFrom.Value), General.ConvertDateServerFormat(dtpDateTo.Value), empId, header);

                // saleBAL.PrintCustomerSalesReport(General.ConvertDateServerFormat(dtpDateFrom.Value), General.ConvertDateServerFormat(dtpDateTo.Value), vendorId, paymentMode);
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error);
            }
        }
        private void gridPurchase_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0 && e.ColumnIndex>=0)
            {
                try
                {
                    int _saleId = 0;
                    int.TryParse(gridDelivery[0,e.RowIndex].Value.ToString(), out _saleId);
                    saleId = _saleId;
                   
                }
                catch (Exception ee)
                {
                    General.Error(ee.ToString());
                    General.ShowMessage(General.EnumMessageTypes.Error);
                    this.BeginInvoke(new MethodInvoker(Close));
                }
            }

        }
     

        private void gridPurchase_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
               // submitItem();
            }
        }

        private void PurchaseSearchForm_Load(object sender, EventArgs e)
        {
            button = new DeliveryItemSummaryReportButtonCollection()
            {
                BtnSearch = btnSearch,
                BtnCancel = btnCancel,
                BtnClose = btnClose,
                BtnPrint = btnPrint
            };
            Search();
            GetAllEmployees();

        }
      
    }
    class DeliveryItemSummaryReportButtonCollection
    {
        public Button BtnSearch { get; set; }
        public Button BtnCancel { get; set; }
        public Button BtnClose { get; set; }
        public Button BtnPrint { get; set; }

    }
}
