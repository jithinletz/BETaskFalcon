using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BETask.BAL;
using BETask.Common;
using BETask.Model;
using EDMX = BETask.DAL.EDMX;

namespace BETask.Views
{
    public partial class ItemTransactionReportForm : Form
    {
        public enum EnumFormEvents
        {
            FormLoad,
            Cancel,
            Close,
            Search,
            Print,
            
        }
        ItemTransactionReportButtonCollection button ;
        List<EDMX.item> listProducts = new List<EDMX.item>();
        
        string itemName="";
        public ItemTransactionReportForm()
        {
            InitializeComponent();
        }
        public ItemTransactionReportForm(string itemName)
        {
           
         
            InitializeComponent();
            dtpDateFrom.Value = dtpDateFrom.Value.AddDays(-30);
            this.itemName = itemName;
            // Search();
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
        private void LoadProducts()
        {
            try
            {
                ItemBAL itemBAL = new ItemBAL();
                listProducts = itemBAL.GetAllItem(0);
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

        private void Search()
        {
            try
            {
                General.ClearGrid(gridItem);
                ItemBAL itemBAL = new ItemBAL();
                int itemId = 0;
                if (cmbProductName.Text != "")
                {
                    if (cmbProductName.SelectedItem != null)
                    {
                        Object selectedItem = cmbProductName.SelectedItem;
                        itemId = (int)((BETask.Views.ComboboxItem)selectedItem).Value;
                    }
                    else
                    {
                        itemId = listProducts.Where(x => x.item_name == this.itemName).FirstOrDefault().item_id;
                    }
                }
                if (itemId > 0)
                {
                  
                    ItemTransactionBAL itemTransactionBAL = new ItemTransactionBAL();
                    decimal openingStock = 0;
                    List<EDMX.item_transaction> listItem = itemTransactionBAL.GetItemTransaction(General.ConvertDateServerFormat(dtpDateFrom.Value), General.ConvertDateServerFormat(dtpDateTo.Value), itemId,out openingStock);

                    if (listItem != null && listItem.Count > 0)
                    {
                        foreach (EDMX.item_transaction prod in listItem)
                        {
                           
                            gridItem.Rows.Add(prod.item_id,General.ConvertDateAppFormat(prod.transaction_date) ,prod.item.item_name, prod.item.uom_setting.uom_name, prod.item_cost,prod.transaction_type, openingStock, prod.qty_added,prod.qty_reduced,prod.closing_stock,prod.closing_value);
                            openingStock = prod.closing_stock;
                        }
                        lblQtyAdded.Text = String.Format("{0} {1}", "Total Qty Added", General.TruncateDecimalPlaces(listItem.Sum(x => x.qty_added)));
                        lblQtyReduced.Text = String.Format("{0} {1}", "Total Qty Reduced", General.TruncateDecimalPlaces(listItem.Sum(x => x.qty_reduced)));
                    }
                }
                
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
                if (gridItem.Rows.Count > 0)
                {
                    int itemId = 0;
                    if (cmbProductName.Text != "")
                    {
                        Object selectedItem = cmbProductName.SelectedItem;
                        itemId = (int)((BETask.Views.ComboboxItem)selectedItem).Value;
                        
                    }
                    ItemTransactionBAL itemBAL = new ItemTransactionBAL();
                   itemBAL.PrintItemTransaction(General.ConvertDateServerFormat(dtpDateFrom.Value), General.ConvertDateServerFormat(dtpDateTo.Value),itemId);

                }
                else
                {
                    General.ShowMessage(General.EnumMessageTypes.Warning, "No Data to print");
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error);
            }
        }

      

        private void ProductionReport_Load(object sender, EventArgs e)
        {
            button = new ItemTransactionReportButtonCollection()
            {
                BtnSearch = btnSearch,
                BtnCancel = btnCancel,
                BtnClose = btnClose,
                BtnPrint=btnPrint,
               
            };
            LoadProducts();
            if (this.itemName != "")
            {
                cmbProductName.Text = this.itemName;
            }
            Search();

        }

    }
    class ItemTransactionReportButtonCollection
    {
        public Button BtnSearch { get; set; }
        public Button BtnCancel { get; set; }
        public Button BtnClose { get; set; }
        public Button BtnPrint { get; set; }
        public Button BtnRawmaterial { get; set; }

    }
}
