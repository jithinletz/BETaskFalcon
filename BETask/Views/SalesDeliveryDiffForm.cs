using BETask.Common;
using BETask.BAL;
using BETask.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using EDMX = BETask.DAL.EDMX;
using System.Data;

namespace BETask.Views
{
    public partial class SalesDeliveryDiffForm : Form
    {
        SaleDeliveryDiffButtonCollection button;
        BAL.CustomerBAL _customerBAL = new BAL.CustomerBAL();
        public enum EnumFormEvents
        {
            FormLoad,
            Cancel,
            Close,
            Search,
            Print
        }
        public SalesDeliveryDiffForm()
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
                    break;
                case EnumFormEvents.Close:
                    this.BeginInvoke(new MethodInvoker(Close));
                    break;
                case EnumFormEvents.Search:
                    Search();
                    break;
                case EnumFormEvents.Print:
                    //Print();
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

        private void Search()
        {
            SaleBAL saleBal = new SaleBAL();
            try
            {
                General.ClearGrid(gridCustomers);
                int itemId = 0;
                if (!String.IsNullOrEmpty(cmbProductName.Text))
                {
                    Object selectedProduct = cmbProductName.SelectedItem;
                    itemId = (int)((BETask.Views.ComboboxItem)selectedProduct).Value;
                }
                if (itemId > 0)
                {
                    List<DAL.Model.SaleDeliveryDiffModel> listSale = saleBal.GetSalesDeliveryDifference(General.ConvertDateServerFormatWithStartTime(dtpDateFrom.Value), General.ConvertDateServerFormatWithEndTime(dtpDateFrom.Value), itemId);
                    if (listSale != null && listSale.Count > 0)
                    {
                        foreach (DAL.Model.SaleDeliveryDiffModel sl in listSale)
                        {
                            gridCustomers.Rows.Add(General.ConvertDateAppFormat(dtpDateFrom.Value),sl.Route, sl.customerId,sl.CustomerName ,sl.Delivery, sl.Sales);
                        }
                        gridCustomers.Rows.Add("","","","",listSale.Sum(x=>x.Delivery), listSale.Sum(x => x.Sales));
                        General.GridBackcolorYellow(gridCustomers);
                    }
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }

        private void LoadProducts()
        {
            try
            {
                ItemBAL itemBAL = new ItemBAL();
                List<EDMX.item> listProducts = itemBAL.GetAllItem_Sellable();
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
        private void FormLoad()
        {
            button = new SaleDeliveryDiffButtonCollection
            {
                BtnSearch = btnSearch,
                //BtnPrint = btnPrint,
                BtnClose = btnClose
            };
          
            LoadProducts();
        }

        private void SalesDeliveryDiffForm_Load(object sender, EventArgs e)
        {
            FormLoad();
        }
    }
    class SaleDeliveryDiffButtonCollection
    {
        public Button BtnSearch { get; set; }
        public Button BtnCancel { get; set; }
        public Button BtnClose { get; set; }
        public Button BtnPrint { get; set; }
    }
}
