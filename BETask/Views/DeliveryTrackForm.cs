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
    public partial class DeliveryTrackForm : Form
    {
        public bool isExportToSales = false;
        /// <summary>
        /// This Below vars will send selected delivery data to another form
        /// </summary>

        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int InvoiceNo { get; set; }
        public int deliveryId = 0;
        string customerName = string.Empty;
        long xSaleId = 0;

        public enum EnumFormEvents
        {
            FormLoad,
            Cancel,
            Close,
            Search,
            Hide,
            Save
        }
        DeliveryTrackButtonCollection button;
        DeliveryBAL deliveryBAL = new DeliveryBAL();

        public DeliveryTrackForm()
        {
            InitializeComponent();
        }
        public DeliveryTrackForm(bool fromSales)
        {
            InitializeComponent();
            this.isExportToSales = fromSales;
        }

        private void ButtonActive(Enum activeEvent)
        {

            switch (activeEvent)
            {
                case EnumFormEvents.FormLoad:
                    // Search();
                    break;
                case EnumFormEvents.Cancel:
                    ButtonActive(EnumFormEvents.FormLoad);
                    deliveryId = 0;
                    break;
                case EnumFormEvents.Close:
                    this.BeginInvoke(new MethodInvoker(Close));
                    break;
                case EnumFormEvents.Save:
                    UpdateDeliveredQty();
                    ButtonActive(EnumFormEvents.Hide);
                    break;
                case EnumFormEvents.Hide:
                    pnlDeliveryUpdate.Hide();
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
            else if (sender == button.BtnHide)
            {
                ButtonActive(EnumFormEvents.Hide);
            }
            else if (sender == button.BtnSave)
            {
                ButtonActive(EnumFormEvents.Save);
            }
        }

        private void SearchDelivery()
        {
            try
            {

                General.ClearGrid(gridDeliveries);
                List<EDMX.delivery> listDelivery = deliveryBAL.GetDeliveryDataByDate(General.ConvertDateServerFormat(dtpDeliveryDate.Value));
                foreach (EDMX.delivery delivery in listDelivery)
                {
                    gridDeliveries.Rows.Add(delivery.delivery_id, delivery.employee.first_name, delivery.delivery_route, delivery.vehicle_no);
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error);
                this.BeginInvoke(new MethodInvoker(Close));
            }
        }

        private void GetDeliveryDetails(int deliveryId)
        {
            try
            {
                DataSet ds = deliveryBAL.GetDelivery(deliveryId);
                DataTable tblDelivery = ds.Tables[0];
                DataTable tblDetail = ds.Tables[1];
                DataTable tblSumamry = ds.Tables[2];

                if (tblDelivery != null && tblDelivery.Rows.Count > 0)
                {
                    PopulateDeliveryItemsSummary(tblSumamry);
                    PopulateDeliveryItems(tblDetail);
                }

            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error);
                //  this.BeginInvoke(new MethodInvoker(Close));
            }

        }

        private void PopulateDeliveryItems(DataTable tblDetail)
        {
            try
            {
                General.ClearGrid(gridCustomers);
                List<string> distinctCustomerNames = tblDetail.AsEnumerable()
                                                       .Select(row => row.Field<string>("Customer_Name"))
                                                       .Distinct()
                                                       .ToList();

                foreach (string pi in distinctCustomerNames)
                {
                    customerName = pi;
                    string piEscaped = pi.Replace("'", "''");
                    DataRow[] custList = tblDetail.Select("customer_name='" + piEscaped + "'");
                    int customerId = 0;
                    long salesId = 0;
                    if (custList.Length > 0)
                    {
                        object customerIdValue = custList[0]["customer_id"];
                        customerId = customerIdValue != DBNull.Value ? Convert.ToInt32(customerIdValue) : -1; // Handle as needed

                        object salesIdValue = custList[0]["sales_id"];
                        salesId = salesIdValue != DBNull.Value ? Convert.ToInt64(salesIdValue) : -1; // Handle as needed
                        xSaleId = salesId;

                        // You can continue to use foundRows for other operations
                    }
                    else
                    {
                        // Handle the case where no rows are found
                        customerId = -1; // Default value or appropriate handling
                        salesId = -1; // Default value or appropriate handling
                    }


                    object deliveryTime = custList[0]["delivery_time"];
                    decimal sum = custList.Sum(row => row.Field<decimal>("delivered_qty"));
                    string dTime = deliveryTime != null ? General.ConvertDateTimeAppFormat(DateTime.Parse(deliveryTime.ToString())) : "";
                    if (deliveryTime != null)
                        dtpDeliveredTime.Value = DateTime.Parse(deliveryTime.ToString());
                    gridCustomers.Rows.Add(customerId, pi, sum, dTime, salesId, "Add to Sales", "Change Delivery");
                    if (this.isExportToSales)
                        gridCustomers.Columns["clmAddToSales"].Visible = true;
                }

            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }

        }

        private void PopulateDeliveryItemsSummary(DataTable tblSummary)
        {
            try
            {
                General.ClearGrid(gridItemSummary);
                foreach (DataRow row in tblSummary.Rows)
                {
                    gridItemSummary.Rows.Add(row["item_name"], row["item_id"], "Nos", row["qty"], row["used_qty"], row["balance_qty"], 0);

                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }

        }

        private void DeliveryTrackForm_Load(object sender, EventArgs e)
        {
            button = new DeliveryTrackButtonCollection()
            {
                //  BtnSearch = btnSearch,
                BtnCancel = btnCancel,
                BtnClose = btnClose,
                BtnHide = btnHideDeliveryItems,
                BtnSave = btnSaveDeliveryItems

            };
            SearchDelivery();
            General.BindPaymentModes(cmbPaymentMode);
        }

        private void dtpDeliveryDate_ValueChanged(object sender, EventArgs e)
        {
            SearchDelivery();
        }

        private void gridDeliveries_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SynchronizationBAL sync = new SynchronizationBAL();
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                try
                {
                    deliveryId = General.ParseInt(gridDeliveries["clmDeliveryId", e.RowIndex].Value.ToString());

                    if (!chkShowSynced.Checked)
                    {
                        try
                        {

                            if (chkSyncRecheck.Checked)
                            {

                                sync.DeliveryFromApp(deliveryId);
                                General.Error($"Recheck {deliveryId}");
                                chkSyncRecheck.Checked = false;
                            }
                        }
                        catch { General.Error($"Recheck Error {deliveryId}"); }
                    }

                    GetDeliveryDetails(deliveryId);
                    lblCount.Text = "";
                    Application.DoEvents();

                    int deliveryCount = deliveryBAL.GetDeliveryCount(deliveryId);
                    lblCount.Text = $"In Local : {deliveryCount} ";

                    if (!chkShowSynced.Checked)
                        sync.DeliveryReturnItems();

                    try
                    {
                        if (chkSaleNotUpdated.Checked && !chkShowSynced.Checked)
                        {
                            General.Error($"Recheck Sale {deliveryId}");
                            chkSaleNotUpdated.Checked = false;
                            GetDeliveryDetails(deliveryId);
                        }
                    }
                    catch { General.Error($"Recheck Error {deliveryId}"); }

                }
                catch (Exception ee)
                {
                    General.Error(ee.ToString());
                    General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
                }

            }
        }



        /// <summary>
        /// Export to Sales here
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridCustomers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                pnlChangeDelivery.Hide();
                lblChangeDelivery.Text = string.Empty;
                if (e.RowIndex >= 0 && e.ColumnIndex == 5)
                {

                    int customerId = General.ParseInt(gridCustomers["clmCustomerId", e.RowIndex].Value.ToString());
                    string customername = gridCustomers["clmCustomer", e.RowIndex].Value.ToString();
                    int invoiceNo = General.ParseInt(Convert.ToString(gridCustomers["clmInvoiceNo", e.RowIndex].Value));

                    string deliveryTime = Convert.ToString(gridCustomers["clmDeliveryCompleted", e.RowIndex].Value);

                    if (deliveryTime == string.Empty)
                    {
                        General.ShowMessage(General.EnumMessageTypes.Error, "Delevery Not completed yet ", "Unable to make invoice");
                        return;
                    }
                    else
                    {
                        CustomerId = customerId;
                        CustomerName = customername;
                        InvoiceNo = invoiceNo;
                        this.DialogResult = DialogResult.OK;
                        try
                        {
                            this.Close();
                        }
                        catch
                        {
                            // this.BeginInvoke(new MethodInvoker(Close));

                        }
                    }
                }
                else if (e.RowIndex >= 0 && e.ColumnIndex == 6)
                {
                    pnlChangeDelivery.Show();
                    lblChangeDelivery.Text = $"Chaging {customerName}";
                }

            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }

        }
        private void ImportDeliveryItems(int deliveryId, int customerId)
        {
            try
            {
                General.ClearGrid(gridDeliveredItems);
                BAL.DeliveryBAL deliveryBAL = new BAL.DeliveryBAL();
                EDMX.delivery delivery = deliveryBAL.GetDeliveryDetails(deliveryId);
                if (delivery != null)
                {

                    List<EDMX.delivery_items> deliveryItems = delivery.delivery_items.ToList().Where(customer => customer.customer_id == customerId).ToList();
                    foreach (EDMX.delivery_items pi in deliveryItems)
                    {
                        gridDeliveredItems.Rows.Add(pi.delivery_item_id, pi.item.item_id, pi.item.item_name, pi.item.uom_setting.uom_name, pi.qty, pi.delivered_qty);
                    }
                    pnlDeliveryUpdate.Show();
                }
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        private void UpdateDeliveredQty()
        {
            try
            {
                if (General.ShowMessageConfirm() == DialogResult.Yes)
                {
                    if (chkDaily.Checked)
                    {
                        if (string.IsNullOrWhiteSpace(cmbPaymentMode.Text) || string.IsNullOrEmpty(cmbPaymentMode.Text) || !General.ValidatePaymentModes(cmbPaymentMode.Text))
                        {
                            General.ShowMessage(General.EnumMessageTypes.Error, "Please select valid payment mode");
                            cmbPaymentMode.Focus();
                            return;
                        }
                    }

                    List<EDMX.delivery_items> listDeliveries = new List<EDMX.delivery_items>();
                    foreach (DataGridViewRow row in gridDeliveredItems.Rows)
                    {

                        listDeliveries.Add(new EDMX.delivery_items()
                        {
                            delivery_item_id = General.ParseInt(row.Cells["clmDeliveryItemId_delivery"].Value.ToString()),
                            delivered_qty = General.ParseDecimal(row.Cells["clmQty_delivered"].Value.ToString()),
                            delivery_time = General.ConvertDateTimeServerFormat(dtpDeliveredTime.Value),
                            delivery_leaf = string.IsNullOrEmpty(txtDeliveryLeaf.Text) ? null : txtDeliveryLeaf.Text,
                            delivery_id = this.deliveryId,
                            customer_id = CustomerId
                        });
                    }
                    if (listDeliveries.Count > 0)
                    {
                        deliveryBAL.UpdateDeliveredItems(listDeliveries);
                        General.Action($"Delivery items updated for the Customer {gridCustomers["clmCustomer", gridCustomers.CurrentRow.Index].Value} . Delivered Time {dtpDeliveredTime.Text}");
                        General.ShowMessage(General.EnumMessageTypes.Success, "Delivery Updates are Successfully Saved");

                        //02.Aug.2021
                        //To save back date daily collection . those sale alreay created
                        try
                        {
                            if (chkDaily.Checked)
                            {
                                if (string.IsNullOrWhiteSpace(cmbPaymentMode.Text) || string.IsNullOrEmpty(cmbPaymentMode.Text) || !General.ValidatePaymentModes(cmbPaymentMode.Text))
                                {
                                    General.ShowMessage(General.EnumMessageTypes.Error, "Please select valid payment mode");
                                    cmbPaymentMode.Focus();
                                    return;
                                }

                                EDMX.daily_collection coll = new EDMX.daily_collection
                                {
                                    delivery_id = deliveryId,
                                    customer_id = CustomerId,
                                    daily_collection_id = 0,
                                    payment_mode = cmbPaymentMode.Text,
                                    status = 4,

                                };
                                deliveryBAL.UpdateDailyCollection(coll);
                            }
                        }
                        catch { }
                        GetDeliveryDetails(deliveryId);
                        chkDaily.Checked = false;
                        txtDeliveryLeaf.Clear();

                    }
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }

        }

        private void gridDeliveredItems_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (gridDeliveredItems.CurrentRow.Index >= 0 && gridDeliveredItems.CurrentCell.ColumnIndex > 0)
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(General.TxtOnlyDecimal);
                }
            }

        }



        private void gridCustomers_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //Delivery Time update
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex == 3)
                {
                    int customerId = General.ParseInt(gridCustomers["clmCustomerId", e.RowIndex].Value.ToString());
                    ImportDeliveryItems(this.deliveryId, customerId);
                    this.CustomerId = customerId;
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                foreach (DataGridViewRow dr in gridDeliveries.Rows)
                {
                    deliveryId = General.ParseInt(dr.Cells["clmDeliveryId"].Value.ToString());
                    GetDeliveryDetails(deliveryId);
                }

            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }

        private void chkDaily_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDaily.Checked)
            {
                cmbPaymentMode.Show();
                cmbPaymentMode.Text = "Cash";
            }
            else
                cmbPaymentMode.Hide();
        }

        private void dtpNewDeliveryDate_ValueChanged(object sender, EventArgs e)
        {
            try
            {

                var newDeliveryId = deliveryBAL.GetDeliveryIdByDate(General.ConvertDateServerFormat(dtpNewDeliveryDate.Value), deliveryId);
                txtDeliveryId.Text = newDeliveryId.ToString();
            }
            catch (Exception ex)
            {

                General.LogExceptionWithShowError(ex, $"Unable to load {ex.Message}");
            }
        }

        private void ChangeDeliveryId(long saleId, int deliveryId)
        {
            try
            {
                int newDeliveryId = 0;
                int.TryParse(txtDeliveryId.Text, out newDeliveryId);
                if (newDeliveryId > 0 && General.ShowMessageConfirm() == DialogResult.Yes)
                {
                    General.Error($"Changing delivery : {lblChangeDelivery.Text} , old delivery : {deliveryId} , new delivery {newDeliveryId}");
                    deliveryBAL.ChangeDeliveryId(saleId, deliveryId, newDeliveryId, lblChangeDelivery.Text);
                    General.Error($"Changed : {lblChangeDelivery.Text}");
                    General.ShowMessage(General.EnumMessageTypes.Success, "Updatetd");
                    txtDeliveryId.Clear();
                    lblChangeDelivery.Text = "";
                    pnlChangeDelivery.Hide();
                    dtpNewDeliveryDate.Value = dtpNewDeliveryDate.Value;
                    GetDeliveryDetails(deliveryId);
                    
                }
            }
            catch (Exception ex)
            {

                General.LogExceptionWithShowError(ex, $"Unable to update {ex.Message}");
            }
           
        }

        private void linkChangeDelivery_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ChangeDeliveryId(xSaleId, deliveryId);
        }
    }
    class DeliveryTrackButtonCollection
    {
        public Button BtnSave { get; set; }
        public Button BtnSearch { get; set; }
        public Button BtnCancel { get; set; }
        public Button BtnClose { get; set; }
        public Button BtnHide { get; set; }

    }
}
