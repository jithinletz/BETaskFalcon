using BETask.Common;
using BETask.BAL;
using BETask.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using EDMX = BETask.DAL.EDMX;
using System.Drawing;
using System.Data;

namespace BETask.Views
{
    public partial class DeliveryStaffReportForm : Form
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
        DeliveryStaffReportButtonCollection button ;


        public DeliveryStaffReportForm()
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
                _lstEmployee = employeeBAL.GetActiveEmployees();
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
                General.lastDsrLoaded = dtpDateTo.Value;
                General.ClearGrid(gridDelivery);
                int empId = 0;
                if (!String.IsNullOrEmpty(cmbEmployee.Text))
                {
                    Object selectedEmployee = cmbEmployee.SelectedItem;
                    empId = (int)((BETask.Views.ComboboxItem)selectedEmployee).Value;
                }
               
                // List<EDMX.delivery> listdelivery = deliveryBAL.EmployeeDeliveryReport(General.ConvertDateServerFormat(dtpDateFrom.Value), General.ConvertDateServerFormat(dtpDateTo.Value), empId);
                DataTable tblDSR= deliveryBAL.EmployeeDeliveryReportSummary(General.ConvertDateServerFormat(dtpDateFrom.Value), General.ConvertDateServerFormat(dtpDateTo.Value), empId);
                foreach (DataRow dr in tblDSR.Rows)
                {
                    string employee = String.Format("{0}",Convert.ToString(dr["employee"]));
                    int deliveredCustomerCount =Convert.ToInt32(dr["customerCount"]);
                    var saleSum = Convert.ToDecimal(dr["saleAmount"]);
                    var deliverySum = Convert.ToDecimal(dr["deliveryAmount"]);

                   gridDelivery.Rows.Add(dr["delivery_id"],General.ConvertDateAppFormat(DateTime.Parse(dr["delivery_date"].ToString())), employee,Convert.ToString(dr["vehicle_no"]), Convert.ToString(dr["delivery_route"]), deliveredCustomerCount, deliveredCustomerCount, deliverySum, saleSum);
                    if (Convert.ToString(dr["remarks"]).Contains("cash.collected"))
                        General.GridBackcolorGreen(gridDelivery);
                }
                lblNetAmount.Text = String.Format("{0} {1}","Net Delivery Amount ",tblDSR.Compute("Sum(deliveryAmount)",""));
                lblNetSaleAmount.Text = String.Format("{0} {1}", "Net Sale Amount ", tblDSR.Compute("Sum(saleAmount)", ""));
                General.GridRownumber(gridDelivery);

            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error);
               // this.BeginInvoke(new MethodInvoker(Close));
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

                deliveryBAL.PrintEmployeeDeliveryReport(General.ConvertDateServerFormat(dtpDateFrom.Value), General.ConvertDateServerFormat(dtpDateTo.Value), empId);
               
               // saleBAL.PrintCustomerSalesReport(General.ConvertDateServerFormat(dtpDateFrom.Value), General.ConvertDateServerFormat(dtpDateTo.Value), vendorId, paymentMode);
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error);
                this.BeginInvoke(new MethodInvoker(Close));
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
                    this.saleId = _saleId;
                    bool isRoute =false;
                    if (e.ColumnIndex == 4)
                        isRoute = true;
                    CustomerDeliveryReportStaffwise customerDeliveryReportStaffwise = new CustomerDeliveryReportStaffwise(_saleId,isRoute);
                  DialogResult result=  customerDeliveryReportStaffwise.ShowDialog();
                    if (result == DialogResult.OK)
                        return;
                }
                catch (Exception ee)
                {
                    General.Error(ee.ToString());
                    General.ShowMessage(General.EnumMessageTypes.Error);
                   // this.BeginInvoke(new MethodInvoker(Close));
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
            button = new DeliveryStaffReportButtonCollection()
            {
                BtnSearch = btnSearch,
                BtnCancel = btnCancel,
                BtnClose = btnClose,
                BtnPrint = btnPrint
            };
            dtpDateFrom.Value = General.lastDsrLoaded;
            dtpDateTo.Value = General.lastDsrLoaded;
            GetAllEmployees();

        }
       
        private void dtpDateFrom_ValueChanged(object sender, EventArgs e)
        {
            dtpDateTo.Value = dtpDateFrom.Value;
        }
    }
    class DeliveryStaffReportButtonCollection
    {
        public Button BtnSearch { get; set; }
        public Button BtnCancel { get; set; }
        public Button BtnClose { get; set; }
        public Button BtnPrint { get; set; }

    }
}
