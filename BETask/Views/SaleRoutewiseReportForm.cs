using BETask.Common;
using BETask.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BETask.BAL;
using EDMX = BETask.DAL.EDMX;
using System.Data;


namespace BETask.Views
{
    public partial class SaleRoutewiseReportForm : Form
    {
        public enum EnumFormEvents
        {
            FormLoad,
            Close,
            Print,
            Search

        }
        SaleRoutewiseReportButtonCollection button;
        List<EDMX.employee> _lstEmployee = new List<EDMX.employee>();
        RoutewiseSaleBAL routewiseSale = new RoutewiseSaleBAL();
        public SaleRoutewiseReportForm()
        {
            InitializeComponent();
        }
        private void ButtonEvents(object sender, EventArgs e)
        {


            if (sender == button.BtnPrint)
            {
                ButtonActive(EnumFormEvents.Print);
            }
            else if (sender == button.BtnClose)
            {
                ButtonActive(EnumFormEvents.Close);
            }
            else if (sender == button.BtnSearch)
            {
                ButtonActive(EnumFormEvents.Search);
            }
        }
        private void ButtonActive(Enum activeEvent)
        {

            switch (activeEvent)
            {
                case EnumFormEvents.FormLoad:

                    break;

                case EnumFormEvents.Search:
                    Search();
                    break;

                case EnumFormEvents.Close:
                    this.BeginInvoke(new MethodInvoker(Close));
                    break;

                case EnumFormEvents.Print:
                    Print();
                    break;
                default:
                    break;

            }
        }

      
        private void Search()
        {
            try
            {
                General.ClearGrid(gridCustomer);
                int empId = 0;
                if (!String.IsNullOrEmpty(cmbEmployee.Text))
                {
                    Object selectedEmployee = cmbEmployee.SelectedItem;
                    empId = (int)((BETask.Views.ComboboxItem)selectedEmployee).Value;
                }
                int itemId = 0;
                if (!String.IsNullOrEmpty(cmbProductName.Text))
                {
                    Object selectedProduct = cmbProductName.SelectedItem;
                    itemId = (int)((BETask.Views.ComboboxItem)selectedProduct).Value;
                }
                List<DAL.Model.RoutewiseSaleModel> listRoute = routewiseSale.GetRoutewiseSale(General.ConvertDateServerFormat(dtpDateFrom.Value), General.ConvertDateServerFormat(dtpDateTo.Value), empId, itemId);
                if (listRoute != null && listRoute.Count > 0)
                {
                    if (listRoute.Sum(x => x.DoSale) > 0)
                        gridCustomer.Columns["clmDoSale"].Visible = true;
                    if (listRoute.Sum(x => x.SalesmanCredit) > 0)
                        gridCustomer.Columns["clmSalesmanCredit"].Visible = true;

                    foreach (DAL.Model.RoutewiseSaleModel route in listRoute)
                    {
                        gridCustomer.Rows.Add(General.ConvertDateAppFormat(route.DeliveryDate),route.Loading,route.Offload,route.Sale,route.Empty, route.Balance, route.Damage,route.Cash,route.Wallet,route.Outstanding,route.DoSale,route.SalesmanCredit,route.Total,route.Collection, route.Foc);
                    }
                    gridCustomer.Rows.Add("Total",listRoute.Sum(x=>x.Loading), listRoute.Sum(x => x.Offload), listRoute.Sum(x => x.Sale), listRoute.Sum(x => x.Empty), listRoute.Sum(x => x.Balance), listRoute.Sum(x => x.Damage), listRoute.Sum(x => x.Cash), listRoute.Sum(x => x.Wallet), listRoute.Sum(x => x.Outstanding), listRoute.Sum(x => x.DoSale), listRoute.Sum(x => x.SalesmanCredit), listRoute.Sum(x => x.Total), listRoute.Sum(x => x.Collection), listRoute.Sum(x => x.Foc));
                    gridCustomer.Rows[gridCustomer.Rows.Count - 1].DefaultCellStyle.BackColor = System.Drawing.Color.Yellow;
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
        private void Print()
        {
            try
            {
                if (gridCustomer.Rows.Count > 0)
                {
                    int empId = 0;
                    if (!String.IsNullOrEmpty(cmbEmployee.Text))
                    {
                        Object selectedEmployee = cmbEmployee.SelectedItem;
                        empId = (int)((BETask.Views.ComboboxItem)selectedEmployee).Value;
                    }
                    int itemId = 0;
                    if (!String.IsNullOrEmpty(cmbProductName.Text))
                    {
                        Object selectedProduct = cmbProductName.SelectedItem;
                        itemId = (int)((BETask.Views.ComboboxItem)selectedProduct).Value;
                    }

                    string header1 = $"{General.companyName} - Route wise sale report for the date between {General.ConvertDateAppFormat(dtpDateFrom.Value)} and {General.ConvertDateAppFormat(dtpDateTo.Value)}";
                    string header2 = cmbEmployee.Text.PadRight(50) + cmbProductName.Text;
                    routewiseSale.Print(General.ConvertDateServerFormat(dtpDateFrom.Value), General.ConvertDateServerFormat(dtpDateTo.Value), empId, itemId, header1, header2);
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
     
      

        private void FormLoad()
        {
            button = new SaleRoutewiseReportButtonCollection
            {

                BtnClose = btnClose,
                BtnPrint = btnPrint,
                BtnSearch = btnSearch

            };
            ButtonActive(EnumFormEvents.FormLoad);
            dtpDateFrom.Value.AddDays(-10);
            LoadProducts();
            GetAllEmployees();
        }
        private void LoadProducts()
        {
            try
            {
                List<EDMX.item> listProducts = new List<EDMX.item>();
                ItemBAL itemBAL = new ItemBAL();
                listProducts = itemBAL.GetDistinctDeliveryItemListByDate(dtpDateFrom.Value.AddDays(-2), dtpDateTo.Value.AddDays(1));
               // listProducts = itemBAL.GetAllItem_Sellable();
                foreach (EDMX.item item in listProducts)
                {
                    ComboboxItem _cmbItem = new ComboboxItem()
                    {
                        Text = item.item_name,
                        Value = item.item_id
                    };
                    cmbProductName.Items.Add(_cmbItem);


                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error);
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
                    ComboboxItem _cmbItem = new ComboboxItem()
                    {
                        Text = String.Format("{0} {1} {2}", emp.first_name, emp.last_name ?? "", emp.route==null?"":emp.route.route_name),
                        Value = emp.employee_id
                    };
                    cmbEmployee.Items.Add(_cmbItem);
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
               // General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
        private void SaleRoutewiseReportForm_Load(object sender, EventArgs e)
        {
            FormLoad();
        }

        private void dtpDateTo_ValueChanged(object sender, EventArgs e)
        {
            LoadProducts();
        }
    }
    class SaleRoutewiseReportButtonCollection
    {

        public Button BtnPrint { get; set; }
        public Button BtnSearch { get; set; }
        public Button BtnClose { get; set; }

    }
}
