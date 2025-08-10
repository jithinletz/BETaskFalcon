using BETask.Common;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BETask.BAL;
using EDMX = BETask.DAL.EDMX;

namespace BETask.Views
{
    public partial class OpeningBalanceReportForm : Form
    {
        AccountTransactionBAL transactionBAL = new AccountTransactionBAL();
        public OpeningBalanceReportForm()
        {
            InitializeComponent();
        }
        private void GetOpening(int routeId=0)
        {
            try
            {
                General.ClearGrid(gridCustomer);
                List<EDMX.account_transaction> listTransaction = new List<EDMX.account_transaction>();
                listTransaction = transactionBAL.GetOpening(routeId);
                
                if (listTransaction != null)
                {
                    decimal debit = 0, credit = 0;
                    int _lastLedger = 0;
                    foreach (EDMX.account_transaction lg in listTransaction)
                    {
                        gridCustomer.Rows.Add(lg.account_transaction_id, lg.ledger_id, lg.account_ledger.account_group.group_name, lg.account_ledger.ledger_name, General.ConvertDateAppFormat(lg.transaction_date), lg.debit, lg.credit, lg.added_time, "Update");
                        debit += Convert.ToDecimal(lg.debit);
                        credit += Convert.ToDecimal(lg.credit);
                        if (_lastLedger == lg.ledger_id)
                            General.GridBackcolorRed(gridCustomer);
                        _lastLedger = lg.ledger_id;
                    }
                    gridCustomer.Rows.Add("", "", "", "","", debit, credit, "");
                    General.GridBackcolorYellow(gridCustomer);
                    General.GridRownumber(gridCustomer);
                }
            }
            catch { }
        }
        private void GetAllRoutes()
        {
            try
            {
                RouteBAL routeBAL = new RouteBAL();
                List<EDMX.route> listRoute = routeBAL.GetAllRoutes();
                cmbRoute.Items.Clear();
                foreach (EDMX.route route in listRoute)
                {
                    ComboboxItem _cmbItem = new ComboboxItem()
                    {
                        Text = route.route_name,
                        Value = route.route_id
                    };
                    cmbRoute.Items.Add(_cmbItem);

                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
        private void OpeningBalanceReportForm_Load(object sender, EventArgs e)
        {
            GetOpening();
            GetAllRoutes();
        }
        private void UpdateOpening(int idx)
        {
            try
            {
                if (General.ShowMessageConfirm() == DialogResult.Yes)
                {
                    int transactionId = 0, ledgerId = 0;
                    decimal debit = 0, credit = 0;
                    transactionId = Convert.ToInt32(gridCustomer["clmTransactionId", idx].Value);
                    ledgerId = Convert.ToInt32(gridCustomer["clmLedgerId", idx].Value);
                    debit = Convert.ToDecimal(gridCustomer["clmDebit", idx].Value);
                    credit = Convert.ToDecimal(gridCustomer["clmCredit", idx].Value);
                    transactionBAL.UpdateOpening(transactionId, ledgerId, debit, credit);
                    General.ShowMessage(General.EnumMessageTypes.Success, "Opening balance updatetd");
                    GetOpening();
                    gridCustomer.Rows[idx].Selected = true;
                    gridCustomer.Rows[idx].Cells[3].Selected = true;
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }

        }

        private void gridCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 8 && e.RowIndex >= 0)
            {
                UpdateOpening(e.RowIndex);
            }
        }

        private void gridCustomer_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (gridCustomer.CurrentRow.Index >= 0 && (gridCustomer.CurrentCell.ColumnIndex > 4 && gridCustomer.CurrentCell.ColumnIndex < 7))
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(General.TxtOnlyDecimal);
                }
            }
        }

        private void gridCustomer_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.RowIndex >= 0 && (e.ColumnIndex <= 4))
                e.Cancel = true;
        }
        private void Search()
        {
            try
            {
                int routeId = 0;
                if (!String.IsNullOrEmpty(cmbRoute.Text))
                {
                    Object selectedRoute = cmbRoute.SelectedItem;
                    routeId = (int)((BETask.Views.ComboboxItem)selectedRoute).Value;

                }
                if (routeId > 0)
                    GetOpening(routeId);
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error);
                this.BeginInvoke(new MethodInvoker(Close));
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Search();
        }
    }
}
