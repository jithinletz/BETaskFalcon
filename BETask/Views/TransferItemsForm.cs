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
    public partial class TransferItemsForm : Form
    {
        TransferButtonCollection button;
        TransferItemBAL transferItemBAL = new TransferItemBAL();
        public enum EnumFormEvents
        {
            FormLoad,
            Cancel,
            Close,
            Search,
            Save,
            Print
        }
        public TransferItemsForm()
        {
            InitializeComponent();
        }
        private void ButtonActive(Enum activeEvent)
        {

            switch (activeEvent)
            {
                case EnumFormEvents.FormLoad:
                    //Search();
                    break;
                case EnumFormEvents.Cancel:
                    ResetForm();
                    break;
                case EnumFormEvents.Close:
                    this.BeginInvoke(new MethodInvoker(Close));
                    break;
                case EnumFormEvents.Save:
                    Save();
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
        }
        private void ResetForm()
        {
            cmbEmployee.Text = "";
            txtQty.Text = "0";
            txtRemarks.Text = "";
            lblGodownStock.Text = "0.00";
            lblTtotalStock.Text = "0.00";
            button.BtnSave.Show();
            lblTransferId.Text = "0";

        }
        private bool Validation()
        {
            bool resp = true;
            if (String.IsNullOrEmpty(cmbEmployee.Text)) { resp = false; General.ShowMessage(General.EnumMessageTypes.Warning, "Please select employee", "Invalid employee"); cmbEmployee.Focus(); }
            if (String.IsNullOrEmpty(cmbProductName.Text)) { resp = false; General.ShowMessage(General.EnumMessageTypes.Warning, "Please select item", "Invalid item"); cmbEmployee.Focus(); }
            decimal qty = 0;
            decimal.TryParse(txtQty.Text, out qty);
            if (qty == 0) { resp = false; General.ShowMessage(General.EnumMessageTypes.Warning, "Please enter valid quantity", "Invalid qty"); txtQty.Focus(); }
            if (String.IsNullOrEmpty(cmbTransferType.Text)) { resp = false; General.ShowMessage(General.EnumMessageTypes.Warning, "Please select transfer type(IN/OUT)", "Invalid type"); cmbTransferType.Focus(); }
            if(resp)
            {
                resp = General.CheckFinancialDate(dtpTransferDate.Value);
            }
            return resp ;

        }
        private EDMX.transfer_item GetTransfer()
        {
            int itemId = 0,employeeId=0;
           
            Object selectedEmployee = cmbEmployee.SelectedItem;
            employeeId = (int)((BETask.Views.ComboboxItem)selectedEmployee).Value;

           
            Object selectedItem = cmbProductName.SelectedItem;
            itemId = (int)((BETask.Views.ComboboxItem)selectedItem).Value;
            

            EDMX.transfer_item transfer = new EDMX.transfer_item
            {
                transfer_id = 0,
                employee_id = employeeId,
                item_id = itemId,
                qty=Convert.ToDecimal(txtQty.Text),
                transfer_date=General.ConvertDateServerFormatWithCurrentTime(dtpTransferDate.Value),
                transfer_type=cmbTransferType.Text,
                remarks=txtRemarks.Text,
                status=1
            };
            return transfer;
        }
        private void Save()
        {
            try
            {
                if (Validation())
                {
                    if (General.ShowMessageConfirm() == DialogResult.Yes)
                    {
                        EDMX.transfer_item transfer = GetTransfer();
                        transferItemBAL.SaveTransfer(transfer);
                        General.ShowMessage(General.EnumMessageTypes.Success, "Transfer successfully saved", "Saved");
                        ButtonActive(EnumFormEvents.Cancel);
                        GetTransferReport();
                        GetStock();
                    }
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error);
            }
        }
        private void GetTransferReport()
        {
            try
            {
                General.ClearGrid(dgItems);
                List<EDMX.transfer_item> listTransfer = transferItemBAL.GetTransferItem(General.ConvertDateServerFormatWithStartTime(dtpDateFrom.Value), General.ConvertDateServerFormatWithEndTime(dtpDateTo.Value)).ToList();
                if (listTransfer != null)
                {
                    foreach (EDMX.transfer_item it in listTransfer)
                    {
                        dgItems.Rows.Add(it.transfer_id, General.ConvertDateTimeAppFormat(it.transfer_date), it.item.item_name, it.qty, it.transfer_type, it.remarks);
                    }
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error);
            }

        }
        private void LoadTransfer(int transferId)
        {
            try
            {
                EDMX.transfer_item _Item = transferItemBAL.GetTransferById(transferId);
                lblTransferId.Text = _Item.transfer_id.ToString();
                dtpTransferDate.Value = _Item.transfer_date;
                string employee = $"{ _Item.employee.first_name} {_Item.employee.last_name}";
                cmbEmployee.Text = employee;
                cmbProductName.Text = _Item.item.item_name;
                txtQty.Text = _Item.qty.ToString();
                cmbTransferType.Text = _Item.transfer_type;
                txtRemarks.Text = _Item.remarks;
                button.BtnSave.Hide();
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
                int transferId = Convert.ToInt32(lblTransferId.Text);
                if (transferId > 0)
                {
                    transferItemBAL.PrintTransfer(transferId);
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error);
            }

        }
        private void GetStock()
        {
            try
            {
                BAL.ItemBAL itemBAL = new ItemBAL();
                int itemId = 0;
                if (!String.IsNullOrEmpty(cmbProductName.Text))
                {
                    Object selectedItem = cmbProductName.SelectedItem;
                    itemId = (int)((BETask.Views.ComboboxItem)selectedItem).Value;
                }
                if (itemId > 0)
                {
                    EDMX.item it = itemBAL.GetItemDetails(itemId);
                    lblGodownStock.Text = it.godown_stock.ToString();
                    lblTtotalStock.Text = it.Stock.ToString();
                }

            }
            catch (Exception ee)
            { }
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
        private void GetAllEmployees()
        {
            try
            {
                BAL.EmployeeBAL employeeBAL = new BAL.EmployeeBAL();
                List<EDMX.employee> _lstEmployee = employeeBAL.GetAllEmployee();
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
        private void FormLoad()
        {
            button = new TransferButtonCollection()
            {
                BtnSave = btnSave,
                BtnCancel = btnCancel,
                BtnClose = btnClose,
                BtnPrint = btnPrint
            };
            GetAllEmployees();
            LoadProducts();
            GetTransferReport();
        }
        private void NextFocus(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {

                General.NextFocus(sender, e);

            }
        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            General.TxtOnlyDecimal(sender, e);
        }

        private void TransferItemsForm_Load(object sender, EventArgs e)
        {
            FormLoad();
        }

        private void dtpDateFrom_ValueChanged(object sender, EventArgs e)
        {
            GetTransferReport();
        }

        private void cmbProductName_Validated(object sender, EventArgs e)
        {
            GetStock();
        }

        private void dgItems_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                int transferId = 0;
                transferId = Convert.ToInt32(dgItems["clmTransferId", e.RowIndex].Value.ToString());
                LoadTransfer(transferId);
                GetStock();
            }
        }
    }
    class TransferButtonCollection
    {
        public Button BtnSearch { get; set; }
        public Button BtnSave { get; set; }
        public Button BtnCancel { get; set; }
        public Button BtnClose { get; set; }
        public Button BtnPrint { get; set; }

    }
}
