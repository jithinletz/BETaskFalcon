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
    public partial class SalePendingDeliveryForm : Form
    {
        SalePendingButtonCollection button;
        SaleBAL saleBAL = new SaleBAL();
        public enum EnumFormEvents
        {
            FormLoad,
            Cancel,
            Close,
            Search,
            Save,
            Print
        }
        public SalePendingDeliveryForm()
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
                case EnumFormEvents.Search:
                    Search();
                    break;
                case EnumFormEvents.Cancel:

                    break;
                case EnumFormEvents.Close:
                    this.BeginInvoke(new MethodInvoker(Close));
                    break;
                case EnumFormEvents.Save:
                    break;
                case EnumFormEvents.Print:
                    break;
                default:
                    break;

            }
        }
        private void ButtonEvents(object sender, EventArgs e)
        {
            if (sender == button.BtnSave)
            {
                ButtonActive(EnumFormEvents.Save);
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
            else if (sender == button.BtnSearch)
            {
                ButtonActive(EnumFormEvents.Search);
            }
        }
        private void FormLoad()
        {
            button = new SalePendingButtonCollection
            {
                BtnSearch = btnSearch,
                BtnClose = btnClose
            };
            LoadProducts();
            Search();
        }
        private void Search()
        {
            try
            {

                int itemId = General.GetComboBoxSelectedValue(cmbProductName);
                General.ClearGrid(dgItems);
                var deliveryList = saleBAL.SaleNotGeneratedDeliveries(General.ConvertDateServerFormat(dtpDateFrom.Value), itemId);
                if (deliveryList != null)
                {
                    foreach (var dl in deliveryList)
                    {
                        dgItems.Rows.Add(dl.delivery_item_id,dl.delivery_id,dl.customer.customer_name,dl.delivery.employee.first_name,dl.item_id,dl.item.item_name,dl.qty,dl.rate,dl.net_amount,dl.delivery_time,dl.payment_mode,dl.is_deposit==2?"No":"Yes",0,"Generate");
                    }
                    lblRecords.Text = dgItems.Rows.Count.ToString();
                }
            }
            catch (Exception ex)
            {
                General.LogExceptionWithShowError(ex,"Unable to search");
            }
        }

        private void GenerateSale(int deliveryId,int deliveryItemId,int oldLeaf)
        {
            try
            {
               long res= saleBAL.GenerateSaleFromDelivery(deliveryId, deliveryItemId, oldLeaf);
                if(res>0)
                General.ShowMessage(General.EnumMessageTypes.Success, "Successfully generated");
                else
                    General.ShowMessage(General.EnumMessageTypes.Error, "Unable to save");

                Search();
            }
            catch (Exception ex)
            {
                General.LogExceptionWithShowError(ex, "Unable to search");

            }
        }

        private void SalePendingDeliveryForm_Load(object sender, EventArgs e)
        {
            FormLoad();
        }
        private void LoadProducts()
        {
            try
            {
                ItemBAL itemBAL = new ItemBAL();
                List<EDMX.item> listProducts = itemBAL.GetAllItem(-1);
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

        private void dgItems_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 13)
            {
                try
                {
                    int deliveryId = Convert.ToInt32(dgItems["clmDeliveryNo", e.RowIndex].Value);
                    int deliveryItemId = Convert.ToInt32(dgItems["clmDeliveryItemId", e.RowIndex].Value);
                   int oldLeaf = Convert.ToInt32(dgItems["clmOldLeafCount", e.RowIndex].Value);
                    GenerateSale(deliveryId, deliveryItemId, oldLeaf);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
    }

    class SalePendingButtonCollection
    {
        public Button BtnSearch { get; set; }
        public Button BtnSave { get; set; }
        public Button BtnCancel { get; set; }
        public Button BtnClose { get; set; }
        public Button BtnPrint { get; set; }

    }
}
