using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BETask.Common;
using BETask.BAL;
using BETask.Model;
using EDMX = BETask.DAL.EDMX;

namespace BETask.Views
{
    public partial class ItemOpeningStockForm : Form
    {
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
        ItemOpeningButtonCollection button;
        List<EDMX.item> listItem = new List<EDMX.item>();
        BAL.ItemBAL itemBAL = new ItemBAL();
        BAL.ItemTransactionBAL itemTranBAL = new ItemTransactionBAL();
        public ItemOpeningStockForm()
        {
            InitializeComponent();
        }
        private void ButtonActive(Enum activeEvent)
        {

            switch (activeEvent)
            {
                case EnumFormEvents.FormLoad:

                    break;
                case EnumFormEvents.Cancel:
                    ButtonActive(EnumFormEvents.FormLoad);

                    break;
                case EnumFormEvents.Close:
                    this.BeginInvoke(new MethodInvoker(Close));
                    break;
                case EnumFormEvents.Search:
                    Search();
                    break;
                case EnumFormEvents.Save:
                    SaveOpening();
                    break;
                default:
                    break;

            }
        }
        private void ButtonEvents(object sender, EventArgs e)
        {
            if (sender == button.BtnNew)
            {
                ButtonActive(EnumFormEvents.New);
            }
            else if (sender == button.BtnCancel)
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
        private void LoadItem(int itemId)
        {
            try
            {
                General.ClearGrid(gridItem);
                listItem = itemBAL.GetAllItem(itemId);
                int i = 0;
                if (!chkStock.Checked)
                {
                    foreach (EDMX.item item in listItem)
                    {
                        gridItem.Rows.Add("true", item.item_id, $" {item.item_name} - {item.uom_setting.uom_name}", "", "", "");
                        // gridItem.Rows[i].HeaderCell.Value = item.barcode;

                    }
                }
                else
                {
                    foreach (EDMX.item item in listItem)
                    {
                        gridItem.Rows.Add("true", item.item_id, $" {item.item_name} - {item.uom_setting.uom_name}", item.cost, item.Stock, General.TruncateDecimalPlaces(item.Stock*item.cost,2),"",item.godown_stock);
                    }
                }

                lblResultCount.Text = gridItem.Rows.Count.ToString() + "search results";
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
        private void Search()
        {
            try
            {
                General.ClearGrid(gridSearch);
                pnlSearch.Show();
                List<EDMX.item_transaction> listTransaction = itemTranBAL.GetItemOpeningReport(General.ConvertDateServerFormat(dtpDateFrom.Value), General.ConvertDateServerFormat(dtpDateTo.Value));
                if (listTransaction != null && listTransaction.Count > 0)
                {
                    foreach (EDMX.item_transaction item in listTransaction)
                    {
                        gridSearch.Rows.Add(General.ConvertDateAppFormat(item.transaction_date), item.item_id, item.item.item_name, item.item_cost, item.closing_stock, item.closing_value, item.narration, "Remove", item.item_transaction_id);
                    }
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error);
            }
        }
        private void SaveOpening()
        {
            try
            {
                List<EDMX.item_transaction> listOpening = GetOpening();
                if (listOpening.Count > 0)
                {
                    if (General.ShowMessageConfirm(listOpening.Count.ToString() + "items to update , Are you sure want to save this") == DialogResult.Yes)
                    {
                        itemTranBAL.SaveItemTransaction_BulkOpening(listOpening,chkStock.Checked);
                        General.ShowMessage(General.EnumMessageTypes.Success, "Opening entry saved");
                        ClearAll();
                    }
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error);
            }
        }
        private void ClearAll()
        {
            foreach (DataGridViewRow dr in gridItem.Rows)
            {

                dr.Cells["clmStock"].Value = "";
                dr.Cells["clmCost"].Value = "";
                dr.Cells["clmValue"].Value = "";
                dr.Cells["clmRemarks"].Value = "";
            }
        }
        private List<EDMX.item_transaction> GetOpening()
        {
            List<EDMX.item_transaction> listOpening = new List<EDMX.item_transaction>();
            try
            {
                foreach (DataGridViewRow dr in gridItem.Rows)
                {
                    if (dr.Cells["clmSelect"].Value.Equals(true) || dr.Cells["clmSelect"].Value.ToString().Equals("true"))
                    {
                        decimal stock = 0, cost = 0, value = 0, godownStock = 0;
                        stock = !string.IsNullOrEmpty(dr.Cells["clmStock"].Value.ToString()) ? Convert.ToDecimal(dr.Cells["clmStock"].Value) : 0;
                        cost = !string.IsNullOrEmpty(dr.Cells["clmCost"].Value.ToString()) ? Convert.ToDecimal(dr.Cells["clmCost"].Value) : 0;
                        value = !string.IsNullOrEmpty(dr.Cells["clmValue"].Value.ToString()) ? Convert.ToDecimal(dr.Cells["clmValue"].Value) : 0;
                        godownStock = !(dr.Cells["clmGodownstock"].Value!=null && string.IsNullOrEmpty(dr.Cells["clmGodownstock"].Value.ToString())) ? Convert.ToDecimal(dr.Cells["clmGodownstock"].Value) : 0;
                        if (stock >= 0)
                        {
                            EDMX.item it = null;

                            if (chkStock.Checked)
                            {
                                it = new EDMX.item()
                                {
                                    item_id = Convert.ToInt32(dr.Cells["clmItemId"].Value),
                                    Stock = stock,
                                    godown_stock = godownStock,
                                    cost = cost
                                };
                            }
                            listOpening.Add(new EDMX.item_transaction
                            {
                                closing_stock = stock,
                                closing_value = value,
                                item_cost = cost,
                                item_id = Convert.ToInt32(dr.Cells["clmItemId"].Value),
                                item_transaction_id = 0,
                                narration = dr.Cells["clmRemarks"].Value.ToString(),
                                qty_added = 0,
                                qty_reduced = 0,
                                status = 1,
                                transaction_date = dtpAsOn.Value,
                                transaction_type = "OPENING",
                                transaction_type_id = -1,
                                item=it

                            });
                        }
                    }
                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return listOpening;
        }
        private void FormLoad()
        {
            button = new ItemOpeningButtonCollection
            {
                //BtnNew = btnNew,
                BtnCancel = btnCancel,
                BtnClose = btnClose,
                BtnSave = btnSave,
                BtnSearch = btnSearch,
                // BtnPrint = btnPrint
            };
            LoadItem(-1);
        }

        private void ItemOpeningStockForm_Load(object sender, EventArgs e)
        {
            FormLoad();
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dr in gridItem.Rows)
            {
                if (chkSelectAll.Checked)
                    dr.Cells["clmSelect"].Value = true;
                else
                    dr.Cells["clmSelect"].Value = false;
            }
        }

        private void gridItem_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            gridItem.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void gridItem_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            //if (!gridItem.Rows[e.RowIndex].IsNewRow)
            //{
            //    if (gridItem.Rows[e.RowIndex].Cells["clmSelect"].Value.Equals(false))
            //    {
            //        e.Cancel = true;
            //    }
            //}
        }

        private void gridItem_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 3)
            {
                try
                {
                    decimal stock = 0, cost = 0, value = 0;
                    stock = !string.IsNullOrEmpty(gridItem.Rows[e.RowIndex].Cells["clmStock"].Value.ToString()) ? Convert.ToDecimal(gridItem.Rows[e.RowIndex].Cells["clmStock"].Value) : 0;
                    cost = !string.IsNullOrEmpty(gridItem.Rows[e.RowIndex].Cells["clmCost"].Value.ToString()) ? Convert.ToDecimal(gridItem.Rows[e.RowIndex].Cells["clmCost"].Value) : 0;
                    value = General.TruncateDecimalPlaces(stock * cost);
                    gridItem.Rows[e.RowIndex].Cells["clmValue"].Value = value;
                    gridItem.Rows[e.RowIndex].Cells["clmRemarks"].Value = "Bulk Opening";
                }
                catch (Exception ee)
                {
                    General.Error(ee.ToString());
                    General.ShowMessage(General.EnumMessageTypes.Error);
                }
            }
        }

        private void gridItem_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (gridItem.CurrentRow.Index >= 0 && (gridItem.CurrentCell.ColumnIndex > 2 && gridItem.CurrentCell.ColumnIndex < 6))
            {
                if (gridItem.CurrentCell.ColumnIndex != 6)
                {
                    TextBox tb = e.Control as TextBox;
                    if (tb != null)
                    {
                        tb.KeyPress += new KeyPressEventHandler(General.TxtOnlyDecimal);
                    }
                }
               
            }
        }

        private void linkCloseSearch_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pnlSearch.Hide();
        }

        private void gridSearch_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex ==7)
                {
                    int tranId = Convert.ToInt32(gridSearch["clmTranId", e.RowIndex].Value);
                    if (tranId > 0)
                    {
                        if (General.ShowMessageConfirm("Are you sure want to remove this", "Please confirm") == DialogResult.Yes)
                        {
                            itemTranBAL.RemoveItemOpening(tranId);
                            Search();
                        }
                    }

                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error);
            }
        }

        private void chkStock_CheckedChanged(object sender, EventArgs e)
        {
            LoadItem(-1);
        }
    }
    class ItemOpeningButtonCollection
    {
        public Button BtnNew { get; set; }
        public Button BtnCancel { get; set; }
        public Button BtnClose { get; set; }
        public Button BtnSave { get; set; }
        public Button BtnSearch { get; set; }
        public Button BtnPrint { get; set; }
    }
}
