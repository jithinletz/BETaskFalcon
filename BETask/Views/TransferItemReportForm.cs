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
    public partial class TransferItemReportForm : Form
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
        TransferReportButtonCollection button;
    
        List<EDMX.item> listItem;
        public TransferItemReportForm()
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
                int routeId = 0, customerId = 0, employeeId = 0, itemId = 0;
               
                if (cmbProductName.Text != "")
                {
                    Object selectedProduct = cmbProductName.SelectedItem;
                    itemId = (int)((BETask.Views.ComboboxItem)selectedProduct).Value;
                }
                TransferItemBAL TransferItemBAL = new TransferItemBAL();
                List<EDMX.transfer_item> listTransferItems = TransferItemBAL.GetTransferItemByItemId(General.ConvertDateServerFormatWithStartTime(dtpFrom.Value), General.ConvertDateServerFormatWithEndTime(dtpTo.Value),itemId);
                General.ClearGrid(dgItemsTransfer);
                lblQty.Text = "";
                if (listTransferItems != null && listTransferItems.Count > 0)
                {
                    foreach (EDMX.transfer_item di in listTransferItems)
                    {
                        dgItemsTransfer.Rows.Add(di.transfer_id, General.ConvertDateTimeAppFormat(di.transfer_date), di.item.item_name, di.qty, di.transfer_type, di.employee.first_name, di.remarks);
                    }
                    lblQty.Text = String.Format("{0} {1}", "Total Qty", listTransferItems.Sum(x => x.qty));
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
                int routeId = 0, customerId = 0, employeeId = 0, itemId = 0;               
                if (cmbProductName.Text != "")
                {
                    Object selectedProduct = cmbProductName.SelectedItem;
                    itemId = (int)((BETask.Views.ComboboxItem)selectedProduct).Value;
                }
                TransferItemBAL TransferItemBAL = new TransferItemBAL();
                TransferItemBAL.PrintItemTransferReport(General.ConvertDateServerFormat(dtpFrom.Value), General.ConvertDateServerFormat(dtpTo.Value), itemId);
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
        private void TransferItemReportForm_Load(object sender, EventArgs e)
        {
            button = new TransferReportButtonCollection
            {


                BtnCancel = btnCancel,
                BtnClose = btnClose,
                BtnSearch = btnSearch,
                BtnPrint = btnPrint,
            };
            LoadItem(-1);
    
        }

       
    }
    class TransferReportButtonCollection
    {
        public Button BtnSearch { get; set; }
        public Button BtnCancel { get; set; }
        public Button BtnClose { get; set; }
        public Button BtnPrint { get; set; }

    }
}
