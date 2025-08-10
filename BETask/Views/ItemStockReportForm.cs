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
    public partial class ItemStockReportForm : Form
    {
        public enum EnumFormEvents
        {
            FormLoad,
            New,
            Search,
            Cancel,
            Close,
            Print,

        }
        ItemStockReportButtonCollection button;
        List<EDMX.item> listItem;
        public ItemStockReportForm()
        {
            InitializeComponent();
        }
        #region Buttonfunction
        private void ButtonActive(Enum activeEvent)
        {

            switch (activeEvent)
            {
                case EnumFormEvents.FormLoad:

                    break;
                case EnumFormEvents.Cancel:
                    ButtonActive(EnumFormEvents.FormLoad);
                    General.ClearTextBoxes(this);
                    lblQty.Text = "";
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
        #endregion Buttonfunction

        private void Search()
        {
            try
            {
                int  itemId = 0;

                if (cmbProductName.Text != "")
                {
                    Object selectedProduct = cmbProductName.SelectedItem;
                    itemId = (int)((BETask.Views.ComboboxItem)selectedProduct).Value;
                }                
                ItemTransactionBAL ItemTransactionBAL = new ItemTransactionBAL();
                List<DAL.Model.ItemStockReportModel> listItems = ItemTransactionBAL.GetItemStockReportData(General.ConvertDateServerFormat(dtpFrom.Value), General.ConvertDateServerFormat(dtpTo.Value), itemId);
                General.ClearGrid(dgItemStock);
                //lblQty.Text = "";
                if (listItems != null && listItems.Count > 0)
                {
                    foreach (DAL.Model.ItemStockReportModel di in listItems)
                    {
                        dgItemStock.Rows.Add(di.item_id, General.ConvertDateAppFormat(di.Transaction_Date), di.Item_name, di.Opening_Stock, di.Purchase,di.ProductionIn, di.ProductionOut, di.Sale, di.Return,di.Damage,di.Closing_Stock, di.TransferIn,di.TransferOut);
                    }
                    //lblQty.Text = String.Format("{0} {1}", "Total Qty", listItems.Sum(x => x.qty));
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
                int itemId = 0;

                if (cmbProductName.Text != "")
                {
                    Object selectedProduct = cmbProductName.SelectedItem;
                    itemId = (int)((BETask.Views.ComboboxItem)selectedProduct).Value;
                }
                ItemTransactionBAL ItemTransactionBAL = new ItemTransactionBAL();
                ItemTransactionBAL.PrintItemStockReportData(General.ConvertDateServerFormat(dtpFrom.Value), General.ConvertDateServerFormat(dtpTo.Value), itemId);
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }

        private void LoadItem(int itemId)
        {
            try
            {
                ItemBAL itemBAL = new ItemBAL();
                listItem = itemBAL.GetAllItem(itemId);
                foreach (EDMX.item item in listItem)
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
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }

        private void ItemStockReportForm_Load(object sender, EventArgs e)
        {
            button = new ItemStockReportButtonCollection
            {
                BtnCancel = btnCancel,
                BtnClose = btnClose,
                BtnSearch = btnSearch,
                BtnPrint = btnPrint,
            };
            LoadItem(-1);
        }
    }
    class ItemStockReportButtonCollection
    {
        public Button BtnSearch { get; set; }
        public Button BtnCancel { get; set; }
        public Button BtnClose { get; set; }
        public Button BtnPrint { get; set; }

    }
}
