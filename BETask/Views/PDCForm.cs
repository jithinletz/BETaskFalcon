using BETask.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EDMX = BETask.DAL.EDMX;
using REP = BETask.DAL.DAL;
using BETask.BAL;

namespace BETask.Views
{
    public partial class PDCForm : Form
    {
        PDCButtonCollection button;
        PDCBAL pdcBal = new PDCBAL();
        public enum EnumFormEvents
        {
            FormLoad,
            New,
            Save,
            Cancel,
            Close,
            Update,
            Other,
            Search,
            Print
        }
        public PDCForm()
        {
            InitializeComponent();
        }
        private void ButtonActive(Enum activeEvent)
        {

            switch (activeEvent)
            {

                case EnumFormEvents.Cancel:
                    ButtonActive(EnumFormEvents.FormLoad);
                    //button.BtnNew.Text = "&New";
                    button.BtnSave.Text = "&Save";
                    ResetForms();
                    break;
                case EnumFormEvents.Close:
                    this.BeginInvoke(new MethodInvoker(Close));
                    break;
                case EnumFormEvents.Save:
                    SavePDC();
                    break;
                case EnumFormEvents.New:
                    //button.BtnNew.Enabled = false;
                    button.BtnSave.Enabled = true;
                    button.BtnClose.Enabled = true;
                    button.BtnCancel.Enabled = true;
                    break;
                case EnumFormEvents.Update:

                    button.BtnSave.Text = "&Update";
                    button.BtnSave.Enabled = false;
                    break;
                case EnumFormEvents.Other:

                    button.BtnSave.Text = "&Save";
                    button.BtnSave.Enabled = true;
                    //button.BtnNew.Enabled = false;
                    break;
                case EnumFormEvents.Search:
                    Search();
                    break;
                case EnumFormEvents.Print:
                      PrintAll();
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
            else if (sender == button.BtnSave)
            {
                ButtonActive(EnumFormEvents.Save);
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
        private void ResetForms()
        {
            cmbParty.Text = string.Empty;
            cmbBank.Text = string.Empty;
            dtpDate.Value = DateTime.Today;
            txtAmount.Clear();
            txtChequeNumber.Clear();
            txtRemarks.Clear();
            cmbBank.Text = "";
            cmbParty.Text = "";
            General.ClearGrid(dgItems);
        }
        private void LoadAllLedger()
        {
            BAL.AccountLedgerBAL accountLedgerBAL = new BAL.AccountLedgerBAL();

            try
            {
                List<Model.AccountLedgerModel> listLedger = accountLedgerBAL.GetAllAccountLedger(-1);

                foreach (Model.AccountLedgerModel ledger in listLedger)
                {
                    ComboboxItem _cmbItem = new ComboboxItem()
                    {
                        Text = ledger.Ledger_name,
                        Value = ledger.Ledger_id
                    };
                    cmbParty.Items.Add(_cmbItem);
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error);
            }
        }
        private bool Validation()
        {
            bool resp = true;


            if (cmbParty.Text == string.Empty)
            {
                General.ShowMessage(General.EnumMessageTypes.Warning, "Please Select Account");
                cmbParty.Focus();
                resp = false;
            }
            if (cmbBank.Text == string.Empty)
            {
                General.ShowMessage(General.EnumMessageTypes.Warning, "Please Select Bank");
                cmbBank.Focus();
                resp = false;
            }
            else if (txtChequeNumber.Text == string.Empty)
            {
                General.ShowMessage(General.EnumMessageTypes.Warning, "Please enter cheque number");
                txtChequeNumber.Focus();
                resp = false;
            }
            else if (General.ParseDecimal(txtAmount.Text) <= 0)
            {
                General.ShowMessage(General.EnumMessageTypes.Warning, "Invalid amount");
                txtAmount.Focus();
                resp = false;
            }
            else if (txtRemarks.Text == string.Empty)
            {
                General.ShowMessage(General.EnumMessageTypes.Warning, "Please enter narration");
                txtRemarks.Focus();
                resp = false;
            }
            return resp;
        }
        private EDMX.pdc GetPdcData()
        {
            int bankId = 0;
            if (!String.IsNullOrEmpty(cmbBank.Text))
            {
                Object selectedBank = cmbBank.SelectedItem;
                bankId = (int)((BETask.Views.ComboboxItem)selectedBank).Value;
            }
            int partyId = 0;
            if (!String.IsNullOrEmpty(cmbParty.Text))
            {
                Object selectedParty = cmbParty.SelectedItem;
                partyId = (int)((BETask.Views.ComboboxItem)selectedParty).Value;
            }
            EDMX.pdc pdc = new EDMX.pdc
            {
                ledger_id = bankId,
                party_id = partyId,
                doc_date = General.ConvertDateServerFormat(dtpDate.Value),
                cheque_date = General.ConvertDateServerFormat(dtpChequeDate.Value),
                cheque_number = txtChequeNumber.Text,
                amount = General.ParseDecimal(txtAmount.Text),
                pdc_mode = rdbPayment.Checked ? "payment" : "reciept",
                remarks = txtRemarks.Text,
                status = 1,
                cheque_status = rdbPayment.Checked ? "Deposited" : "Collected",
                updated_on = DateTime.Today


            };
            return pdc;
        }
        private void SavePDC()
        {
            try
            {
                if (General.ShowMessageConfirm() == DialogResult.Yes)
                {
                    if (Validate())
                    {
                        EDMX.pdc pdc = GetPdcData();
                        pdcBal.SavePDC(pdc);
                        SaveChequeDetails(pdc);
                        General.Action($"PDC Saved cheque {txtChequeNumber.Text} {txtAmount.Text}");
                        string saveMessage = "PDC Saved succefully";
                        General.ShowMessage(General.EnumMessageTypes.Success, saveMessage);
                        ResetForms();
                    }
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error);
            }
        }
        private void SaveChequeDetails(EDMX.pdc pdc)
        {
            if (!String.IsNullOrEmpty(pdc.cheque_number))
            {
                int transactionNumber = 0;
                try
                {
                    transactionNumber = pdcBal.GetPDCTransactionNumber(pdc);
                    EDMX.account_transaction_cheque cheque = new EDMX.account_transaction_cheque
                    {
                        cheque_date =pdc.cheque_date,
                        cheque_number = txtChequeNumber.Text,
                        // bank = pdc.ba,
                        other_details = pdc.remarks,
                        account_transaction_number = transactionNumber,
                        status = 1

                    };
                    BAL.AccountTransactionBAL accountTransactionBAL = new BAL.AccountTransactionBAL();
                    accountTransactionBAL.SaveAccountTransactionCheque(cheque);
                }
                catch (Exception ex)
                {
                    General.Error(ex.ToString());
                    General.ShowMessage(General.EnumMessageTypes.Success, "Cheque details not saved");
                }
            }
        }
        private void NextFocus(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                General.NextFocus(sender, e);

            }
        }
        private void GetAllBanks()
        {
            try
            {
                cmbBank.Items.Clear();
                DAL.DAL.LedgerMappingDAL ledgerMappingDAL = new DAL.DAL.LedgerMappingDAL();
                DAL.DAL.AccountLedgerDAL accountLedgerDAL = new DAL.DAL.AccountLedgerDAL();
                int groupId = Convert.ToInt32(ledgerMappingDAL.GetLegerMapping(DAL.DAL.LedgerMappingDAL.EnumLedgerMapGroupTypes.BANKACCOUNTS).group_id);
                List<EDMX.account_ledger> listBank = accountLedgerDAL.GetAllAccountLedger(groupId);
                foreach (EDMX.account_ledger bank in listBank)
                {
                    ComboboxItem _cmbItem = new ComboboxItem()
                    {
                        Text = bank.ledger_name,
                        Value = bank.ledger_id
                    };
                    cmbBank.Items.Add(_cmbItem);
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
            button = new PDCButtonCollection
            {
                // BtnNew = btnNew,
                BtnCancel = btnCancel,
                BtnClose = btnClose,
                BtnSave = btnSave,
                BtnSearch = btnSearch,
                BtnPrint = btnPrint
            };
            ButtonActive(EnumFormEvents.FormLoad);
            GetAllBanks();
            LoadAllLedger();
            string message = pdcBal.CheckPDCMissingConfiguration();
            if (message!="")
            {
                General.ShowMessage(General.EnumMessageTypes.Warning, message);
                button.BtnSave.Hide();
            }
        }

        private void Search()
        {
            try
            {
                General.ClearGrid(dgItems);
                List<EDMX.pdc> listPdc = pdcBal.SearchPDC(cmbSearchStatus.Text, General.ConvertDateServerFormat(dtpSearchDate.Value), General.ConvertDateServerFormat(dtpSearchDateTo.Value));
                if (listPdc != null && listPdc.Count > 0)
                {
                    foreach (EDMX.pdc pdc in listPdc)
                    {
                        dgItems.Rows.Add(pdc.pdc_id, pdc.pdc_mode, pdc.account_ledger1.ledger_name, pdc.cheque_number, pdc.amount, General.ConvertDateAppFormat(pdc.cheque_date), pdc.cheque_status, General.ConvertDateAppFormat(pdc.updated_on),"Print");
                        if (pdc.cheque_status.ToLower() == "done")
                            dgItems.Rows[dgItems.Rows.Count - 1].DefaultCellStyle.BackColor = System.Drawing.Color.Green;
                    }
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
        private void PrintAll()
        {
            try
            {
                pdcBal.PrintAll(cmbSearchStatus.Text, General.ConvertDateServerFormat(dtpSearchDate.Value), General.ConvertDateServerFormat(dtpSearchDate.Value));
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
        private void DeletePDC(int pdcId)
        {
            try
            {
                pdcBal.DeletePDC(pdcId);
                General.Action($"PDC Delete pdc {pdcId}");
                string saveMessage = "PDC Deleted succefully";
                General.ShowMessage(General.EnumMessageTypes.Success, saveMessage);
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
        private void UpdatePDC(int pdcId, string status)
        {
            try
            {
                pdcBal.UpdatePDC(pdcId, status,dtpDate.Value);
                General.Action($"PDC Updated pdc {pdcId}");
                string saveMessage = "PDC Updated succefully";
                General.ShowMessage(General.EnumMessageTypes.Success, saveMessage);
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error,"Unable to process please contact for support \n" +ee.Message);
            }
        }


        private void PDCForm_Load(object sender, EventArgs e)
        {
            FormLoad();
        }

        private void dgItems_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex != 6)
                e.Cancel = true;
            else
            {
                if (dgItems["clmStatus", e.RowIndex].Value.ToString().ToLower() == "done")
                {
                    e.Cancel = true;
                }
            }
        }

        private void dgItems_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
            {
                try
                {
                    if (dgItems.Rows.Count >= 0)
                    {
                        int row = dgItems.CurrentRow.Index;
                        if (dgItems[0, row].Value != null)
                        {
                            if (General.ShowMessageConfirm("Are you sure want to delete this") == DialogResult.Yes)
                            {
                                int pdcId = Convert.ToInt32(dgItems["clmPdcId", row].Value);
                                DeletePDC(pdcId);
                                Search();
                            }
                        }
                    }
                }
                catch (Exception ee)
                {
                    General.Error(ee.ToString());
                    General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
                }
            }
        }

        private void dgItems_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    if (e.ColumnIndex == 6)
                    {
                        DataGridViewComboBoxCell cb = (DataGridViewComboBoxCell)dgItems.Rows[e.RowIndex].Cells["clmStatus"];
                        if (cb.Value != null)
                        {
                            if (General.ShowMessageConfirm("Are you sure want to update this") == DialogResult.Yes)
                            {
                                string status = dgItems["clmStatus", e.RowIndex].Value.ToString();
                                int pdcId = Convert.ToInt32(dgItems["clmPdcId", e.RowIndex].Value);
                                UpdatePDC(pdcId, status);
                                Search();
                            }
                        }
                    }
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }

        private void dgItems_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            dgItems.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void dgItems_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex == 8)
                {
                    pdcBal.Print(General.ParseInt(dgItems["clmPDCId", e.RowIndex].Value.ToString()), "PDC Voucer");
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
    }
    class PDCButtonCollection
    {
        public Button BtnNew { get; set; }
        public Button BtnCancel { get; set; }
        public Button BtnClose { get; set; }
        public Button BtnSave { get; set; }
        public Button BtnSearch { get; set; }
        public Button BtnPrint { get; set; }
    }
}
