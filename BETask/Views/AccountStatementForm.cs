using BETask.Common;
using BETask.BAL;
using BETask.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using EDMX = BETask.DAL.EDMX;
using System.Data;
using System.ComponentModel;

namespace BETask.Views
{
    public partial class AccountStatementForm : Form
    {
        public enum EnumFormEvents
        {
            FormLoad,
            Cancel,
            Close,
            Search,
            Print
        }
        public int saleId = 0;
        SaleBAL saleBAL = new SaleBAL();
        BAL.AccountTransactionBAL accountLedgerBAL = new AccountTransactionBAL();
        bool isFormLoaded = false;
        DataTable tblStatement;
        List<Model.AccountLedgerModel> listLedger;


        AccountStatementButtonCollection button;
        private int primaryCostCenter;
        private int CostCenter;

        public AccountStatementForm()
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
                    cmbLedgerAccount.Text = string.Empty;
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

        private void LoadAllLedger()
        {
            BAL.AccountLedgerBAL accountLedgerBAL = new BAL.AccountLedgerBAL();

            try
            {
                
                //cmbLedgerAccount.Items.Clear();
                if (!chkCustomerOnly.Checked)
                {
                    int groupId = -1;
                    if (cmbGroup.SelectedValue != null)
                        groupId = int.Parse(cmbGroup.SelectedValue.ToString());

                    groupId = groupId == 0 ? -1 : groupId;

                    DAL.DAL.AccountLedgerDAL accountLedgerDAL = new DAL.DAL.AccountLedgerDAL();
                    List<EDMX.account_ledger> listCustomer = accountLedgerDAL.GetAllAccountLedgerNonCustomer(groupId);
                  

                    cmbLedgerAccount.DataSource = listCustomer;
                    cmbLedgerAccount.DisplayMember = "ledger_name";
                    cmbLedgerAccount.ValueMember = "ledger_id";
                }
                else
                {
                    DAL.DAL.AccountLedgerDAL accountLedgerDAL = new DAL.DAL.AccountLedgerDAL();
                    List<EDMX.customer> listCustomer = accountLedgerDAL.GetAllAccountLedgerCustomerOnly();
                   
                    cmbLedgerAccount.DataSource = listCustomer;
                    cmbLedgerAccount.DisplayMember = "customer_name";
                    cmbLedgerAccount.ValueMember = "ledger_id";
                   
                }
                cmbLedgerAccount.SelectedIndex = -1;

            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error);
            }
        }
        private void LoadAccountGroup()
        {
            try {
                DAL.DAL.AccountGroupDAL accountGroupDAL = new DAL.DAL.AccountGroupDAL();
                List<DAL.EDMX.account_group> listGroup = accountGroupDAL.GetAllAccountGroupHasLedger(General.companyId, General.locationId);
                cmbGroup.DataSource = listGroup;
                cmbGroup.DisplayMember = "group_name";
                cmbGroup.ValueMember = "group_id";
                cmbGroup.SelectedIndex = -1;
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
                tblStatement = new DataTable();
                lblBalance.Text = "0.00";
                lblPayment.Text = "0.00";
                lblReciept.Text = "0.00";
                General.ClearGrid(gridAccounts);
                Application.DoEvents();
                AccountTransactionBAL accountTransactionBAL = new AccountTransactionBAL();
                int ledgerId = 0,groupId=0;
                button.BtnSearch.Hide();
                if (cmbLedgerAccount.ValueMember == null)
                {
                    return;
                }
                if (cmbLedgerAccount.Text != "" && cmbLedgerAccount.SelectedValue != null)
                {
                    //Object selectedLedger = cmbLedgerAccount.SelectedItem;
                    //ledgerId = (int)((BETask.Views.ComboboxItem)selectedLedger).Value;

                    ledgerId = int.Parse(cmbLedgerAccount.SelectedValue.ToString());
                }
                if (!string.IsNullOrEmpty(cmbGroup.Text) && cmbGroup.SelectedValue != null)
                {
                    groupId = int.Parse(cmbGroup.SelectedValue.ToString());
                }
                decimal openingDebit = 0, openingCredit = 0;

                bool isCostCenter = false;
                if (primaryCostCenter > 0 || CostCenter > 0)
                    isCostCenter = true;
               

                 tblStatement = accountTransactionBAL.GetAccountTransactionStatement(General.ConvertDateServerFormat(dtpDateFrom.Value), General.ConvertDateServerFormat(dtpDateTo.Value), out openingDebit, out openingCredit, ledgerId, chkHideNotReconsiled.Checked, isCostCenter, primaryCostCenter, CostCenter, txtContains.Text,groupId);
                if (openingDebit != 0 || openingCredit != 0)
                {
                    gridAccounts.Rows.Add(dtpDateFrom.Value.AddDays(-1), "Opening", openingDebit, openingCredit, "Opening", "");
                }



                gridAccounts.SuspendLayout();
                List<DataGridViewRow> rows = new List<DataGridViewRow>();

               // foreach (EDMX.account_transaction tran in listAccountTransaction)
                foreach(DataRow dr in tblStatement.Rows)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(gridAccounts, General.ConvertDateAppFormat(Convert.ToDateTime( dr["transaction_date"])), dr["ledger_name"], dr["debit"], dr["credit"],dr["transaction_type"],$"{Convert.ToString(dr["narration"] )} {dr["voucher_number"]}" , dr["transaction_type_id"], dr["transaction_number"]);
                    // row.CreateCells(gridAccounts, General.ConvertDateAppFormat(tran.transaction_date), tran.account_ledger.ledger_name, tran.debit, tran.credit, tran.transaction_type, $" {tran.narration}", tran.transaction_type_id, $"{tran.transaction_number}");
                    rows.Add(row);
                }

                gridAccounts.Rows.AddRange(rows.ToArray()); // Add all rows at once

                gridAccounts.ResumeLayout(); // Resume layout updates
                gridAccounts.RowTemplate.Height = 60;
                #region slowload
                //foreach (EDMX.account_transaction tran in listAccountTransaction)
                //{
                //    gridAccounts.Rows.Add(General.ConvertDateAppFormat(tran.transaction_date), tran.account_ledger.ledger_name, tran.debit, tran.credit, tran.transaction_type, $" {tran.narration}",tran.transaction_type_id, $"{tran.transaction_number}");
                //    // totalPayment += tran.debit;
                //    if (!string.IsNullOrEmpty(tran.voucher_number) && tran.voucher_number.Contains("*"))
                //        General.GridBackcolorOrange(gridAccounts);


                //    gridAccounts.FirstDisplayedScrollingRowIndex = gridAccounts.Rows.Count - 1;
                //    lblCount.Text = gridAccounts.Rows.Count.ToString() + "/" + listAccountTransaction.Count.ToString();
                //    Application.DoEvents();
                //}
                #endregion
                if (tblStatement.Rows.Count > 0)
                {
                    decimal debit = Convert.ToDecimal(tblStatement.Compute("sum(debit)", ""));
                    decimal credit = Convert.ToDecimal(tblStatement.Compute("sum(credit)", ""));

                    lblPayment.Text = String.Format("{0} {1}", "Debit", General.TruncateDecimalPlaces(debit + openingDebit));
                    lblReciept.Text = String.Format("{0} {1}", "Credit", General.TruncateDecimalPlaces(credit + openingCredit));
                    lblBalance.Text = String.Format("{0} {1}", "Balance", General.TruncateDecimalPlaces((debit + openingDebit) - (credit + openingCredit)));
                }

            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error);
            }
            finally
            {
                button.BtnSearch.Show();

            }

        }

        private void Print()
        {
            try
            {
                lblBalance.Text = "0.00";
                lblPayment.Text = "0.00";
                lblReciept.Text = "0.00";
                string headerText = string.Empty;
                //General.ClearGrid(gridAccounts);
                AccountTransactionBAL accountTransactionBAL = new AccountTransactionBAL();
                int ledgerId = 0,groupId=0;
                if (cmbLedgerAccount.Text != "" && cmbLedgerAccount.SelectedValue != null)
                {
                    
                    ledgerId = int.Parse(cmbLedgerAccount.SelectedValue.ToString());
                }
                if (!string.IsNullOrEmpty(cmbGroup.Text) && cmbGroup.SelectedValue != null)
                {
                    groupId = General.GetComboBoxSelectedValue(cmbGroup);
                }
                decimal openingDebit = 0, openingCredit = 0;
             //  DataTable tblStatement = accountTransactionBAL.GetAccountTransactionStatement(General.ConvertDateServerFormat(dtpDateFrom.Value), General.ConvertDateServerFormat(dtpDateTo.Value), out openingDebit, out openingCredit, ledgerId, chkHideNotReconsiled.Checked,groupId);

                accountTransactionBAL.PrintAccountStatement(tblStatement, General.ConvertDateServerFormat(dtpDateFrom.Value), General.ConvertDateServerFormat(dtpDateTo.Value), openingDebit,openingCredit,headerText);



            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error);

            }
        }
        private void gridPurchase_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                try
                {
                    int _saleId = 0;
                    int.TryParse(gridAccounts[0, e.RowIndex].Value.ToString(), out _saleId);
                    saleId = _saleId;

                    int transaction = Convert.ToInt32(gridAccounts["clmId", e.RowIndex].Value.ToString());
                    ViewJournalForm viewJournalForm = new ViewJournalForm(transaction,true);
                    viewJournalForm.ShowDialog();

                }
                catch (Exception ee)
                {
                    General.Error(ee.ToString());
                    General.ShowMessage(General.EnumMessageTypes.Error);
                }
            }

        }


        private void gridPurchase_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                // submitItem();
            }
        }

        private void PurchaseSearchForm_Load(object sender, EventArgs e)
        {
            button = new AccountStatementButtonCollection()
            {
                BtnSearch = btnSearch,
                BtnCancel = btnCancel,
                BtnClose = btnClose,
                BtnPrint = btnPrint
            };
            //Search();
            LoadAccountGroup();
            isFormLoaded = true;
            LoadAllLedger();

        }

        private void chkCustomerOnly_CheckedChanged(object sender, EventArgs e)
        {
            cmbLedgerAccount.Text = "";
            cmbLedgerAccount.SelectedIndex = -1;
            LoadAllLedger();
        }

        private void cmbGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isFormLoaded)
            {

                LoadAllLedger();
            }

        }

        private void cmbGroup_Validated(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbGroup.Text))
            {
                LoadAllLedger();
            }
        }

        private void linkCostCenter_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (linkCostCenter.Text == "Cost center search")
            {
                CostCenterSearchForm costCenterSearchForm = new CostCenterSearchForm();
                if (costCenterSearchForm.ShowDialog() == DialogResult.OK)
                {
                    this.primaryCostCenter = costCenterSearchForm.PrimaryCostCenter;
                    this.CostCenter = costCenterSearchForm.SubCostCenter;
                    if (this.primaryCostCenter > 0 || this.CostCenter > 0)
                    {
                        linkCostCenter.Text = costCenterSearchForm.SearchValue;
                        linkCostCenter.ActiveLinkColor = System.Drawing.Color.Red;
                    }
                }
            }
            else
            {
                linkCostCenter.Text = "Cost center search";
                this.primaryCostCenter = 0;
                this.CostCenter = 0;

            }
        }

    }
    class AccountStatementButtonCollection
    {
        public Button BtnSearch { get; set; }
        public Button BtnCancel { get; set; }
        public Button BtnClose { get; set; }
        public Button BtnPrint { get; set; }

    }
}
