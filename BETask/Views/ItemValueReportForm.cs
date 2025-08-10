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
    public partial class ItemValueReportForm : Form
    {
        public enum EnumFormEvents
        {
            FormLoad,
            Cancel,
            Close,
            Search,
            Print,
            
        }
        ItemValueReportButtonCollection button ;
        List<EDMX.item> listProducts = new List<EDMX.item>();
        public ItemValueReportForm()
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
                listProducts = itemBAL.GetAllItem_Sellable();
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
                    Object selectedItem = cmbProductName.SelectedItem;
                    itemId = (int)((BETask.Views.ComboboxItem)selectedItem).Value;
                }
                List<EDMX.item> listItem = itemBAL.GetAllItem(itemId);
                if (itemId == 0)
                {
                    if (rdbRawmaterial.Checked)
                    {
                        listItem = itemBAL.GetAllItem_Rawmaterial();
                    }
                    else if (rdbSellable.Checked)
                    {
                        listItem = itemBAL.GetAllItem_Sellable();
                    }
                   
                }
                if (listItem != null && listItem.Count > 0)
                {
                    foreach (EDMX.item prod in listItem)
                    {
                        gridItem.Rows.Add(prod.item_id, prod.item_name, prod.uom_setting.uom_name, prod.Stock, prod.cost, General.TruncateDecimalPlaces(prod.cost * prod.Stock));
                    }
                    lblTotalCost.Text = String.Format("{0} {1}", "Total item Value", General.TruncateDecimalPlaces(listItem.Sum(x => x.cost * x.Stock)));
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
                    ItemBAL itemBAL = new ItemBAL();
                    itemBAL.PrintItemValue(rdbRawmaterial.Checked, rdbSellable.Checked);

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

        private void ShowItemTransaction(string itemName)
        {
            ItemTransactionReportForm itemTransactionReportForm = new ItemTransactionReportForm(itemName);
            itemTransactionReportForm.ShowDialog();
        }
      

        private void ProductionReport_Load(object sender, EventArgs e)
        {
            button = new ItemValueReportButtonCollection()
            {
                BtnSearch = btnSearch,
                BtnCancel = btnCancel,
                BtnClose = btnClose,
                BtnPrint=btnPrint,
               
            };
            Search();
            LoadProducts();
        }

        private void gridItem_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    string itemName = gridItem["clmItemName", e.RowIndex].Value.ToString();
                    ShowItemTransaction(itemName);
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error);
            }
        }
    }
    class ItemValueReportButtonCollection
    {
        public Button BtnSearch { get; set; }
        public Button BtnCancel { get; set; }
        public Button BtnClose { get; set; }
        public Button BtnPrint { get; set; }
        public Button BtnRawmaterial { get; set; }

    }
}
