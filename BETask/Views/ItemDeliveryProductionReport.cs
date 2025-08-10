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
    public partial class ItemDeliveryProductionReport : Form
    {
        ItemDeliveryProductionReportButtons button;
        DeliveryBAL deliveryBAL = new DeliveryBAL();
        public enum EnumFormEvents
        {
            FormLoad,
            Cancel,
            Close,
            Search,
            Print
        }
        public ItemDeliveryProductionReport()
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
                    cmbProductName.Text = string.Empty;
                    General.ClearGrid(gridDelivery);
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
     
        private void Search()
        {
            try
            {
                General.ClearGrid(gridDelivery);
                int itemId = 0;
                if (!String.IsNullOrEmpty(cmbProductName.Text))
                {
                    Object selectedProduct = cmbProductName.SelectedItem;
                    itemId = (int)((BETask.Views.ComboboxItem)selectedProduct).Value;
                }
                if (itemId > 0)
                {
                    BETask.DAL.Model.ProductionDeliveryReportModel prodReport = deliveryBAL.GetProductionDeliveryReport(General.ConvertDateServerFormat(dtpDateFrom.Value), General.ConvertDateServerFormat(dtpDateTo.Value), itemId);
                    if (prodReport != null)
                    {
                        List<BETask.DAL.Model.ProductionDeliveryDetail> prodDetail = prodReport.listDeliveryDetail!=null? prodReport.listDeliveryDetail.ToList():null;
                        if (prodDetail != null)
                        {
                            foreach (BETask.DAL.Model.ProductionDeliveryDetail pl in prodDetail)
                            {
                                gridDelivery.Rows.Add(pl.EmployeeId, pl.EmployeeName, pl.Scheduled, pl.Delivered, pl.Returned,pl.Balance);
                            }
                            gridDelivery.Rows.Add("", "Total", prodDetail.Sum(x=>x.Scheduled), prodDetail.Sum(x => x.Delivered), prodDetail.Sum(x => x.Returned), prodDetail.Sum(x => x.Balance));
                            General.GridBackcolorYellow(gridDelivery);
                            gridDelivery.Rows.Add("", "Production", prodReport.Production, "", "", "");
                            General.GridBackcolorOrange(gridDelivery);
                            gridDelivery.Rows.Add("", "ItemStock", prodReport.Stock, "", "","");
                            General.GridBackcolorRed(gridDelivery);
                          
                        }

                    }
                }
            }
            catch(Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error,ee.Message);
            }
        }
        private void Print()
        {
            try
            {
                int itemId = 0;
                string header = $"{cmbProductName.Text} , Date between {dtpDateFrom.Text} and {dtpDateTo.Text} ";
                if (!String.IsNullOrEmpty(cmbProductName.Text))
                {
                    Object selectedProduct = cmbProductName.SelectedItem;
                    itemId = (int)((BETask.Views.ComboboxItem)selectedProduct).Value;
                }
                deliveryBAL.PrintProductionDeliveryReport(General.ConvertDateServerFormat(dtpDateFrom.Value), General.ConvertDateServerFormat(dtpDateTo.Value), itemId, header);
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
                List<EDMX.item> listProducts  = itemBAL.GetAllItem_Sellable();
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
                //General.ShowMessage(General.EnumMessageTypes.Error);
            }
        }

        private void FormLoad()
        {
            button = new ItemDeliveryProductionReportButtons()
            {
                BtnSearch = btnSearch,
                BtnCancel = btnCancel,
                BtnClose = btnClose,
                BtnPrint = btnPrint
            };
            LoadProducts();
        }

        private void ItemDeliveryProductionReport_Load(object sender, EventArgs e)
        {
            FormLoad();
        }
    }
    class ItemDeliveryProductionReportButtons
    {
        public Button BtnSearch { get; set; }
        public Button BtnCancel { get; set; }
        public Button BtnClose { get; set; }
        public Button BtnPrint { get; set; }
    }



}
