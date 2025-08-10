using BETask.Common;
using BETask.BAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Data;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using System.Reflection;
using System.Drawing;

namespace BETask.Views
{
    public partial class OnlinePaymentTransactionsForm : Form
    {
        OnlinePaymentTransaction button;
        OrderStatusModel OrderStatus;
        string currentStatus = string.Empty;
        public enum EnumFormEvents
        {
            FormLoad,
            Cancel,
            Close,
            Search,
            Print,
            Status
        }
        public OnlinePaymentTransactionsForm()
        {
            InitializeComponent();
        }
        private void FormLoad()
        {
            button = new OnlinePaymentTransaction()
            {
                BtnSearch = btnSearch,
                BtnClose = btnClose,
                BtnPrint = btnPrint,
                BtnStatus = btnStatus
            };
            cmbMode.SelectedIndex = 0;
            SearchAsync(DateTime.Now, DateTime.Now, cmbMode.Text);
            timer1.Start();
        }
        private void ButtonActive(Enum activeEvent)
        {

            switch (activeEvent)
            {

                case EnumFormEvents.Cancel:
                    dgTransactions.DataSource = null;
                    break;
                case EnumFormEvents.Close:
                    this.BeginInvoke(new MethodInvoker(Close));
                    break;
                case EnumFormEvents.Search:
                    SearchAsync(General.ConvertDateServerFormat(dtpDateFrom.Value), General.ConvertDateServerFormat(dtpDateTo.Value), cmbMode.Text);
                    break;
                case EnumFormEvents.Print:
                    PrintAsync();
                    break;
                case EnumFormEvents.Status:
                    GetTransactionStatusAsync();
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
            else if (sender == button.BtnStatus)
            {
                ButtonActive(EnumFormEvents.Status);
            }

        }

        private async Task SearchAsync(DateTime dateFrom, DateTime dateTo, string status)
        {
            DAL.DAL.SynchronizationDAL sync = new DAL.DAL.SynchronizationDAL(null);
            try
            {
                lblSumary.Text = "";
                lblReference.Text = "";

                dgTransactions.DataSource = null;
                DataTable tblData = await sync.GetOnlineTransactionReport(General.ConvertDateServerFormat(dateFrom), General.ConvertDateServerFormat(dateTo), status, 0);
                if (tblData != null && tblData.Rows.Count > 0)
                {
                    lblNoData.Hide();
                    dgTransactions.DataSource = tblData;
                    var amountRecieved = tblData != null ? tblData.Compute("Sum(amount_received)", "") : 0;
                    var collectionAmount = tblData != null ? tblData.Compute("Sum(amount)", "") : 0;
                    var fee = tblData != null ? (Convert.ToDecimal(amountRecieved) - Convert.ToDecimal(collectionAmount)) : 0;
                    if (cmbMode.Text.ToLower().Contains("successful"))
                        lblSumary.Text = $"Transaction Count : {tblData.Rows.Count} , Collection :{collectionAmount} , Recieved : {amountRecieved} , Fee :{fee} ";

                    if (cmbMode.Text == "No Status Recevied" && dgTransactions.Rows.Count > 0)
                        linkValidateAll.Show();
                    else
                        linkValidateAll.Hide();

                }
                else
                    lblNoData.Show();

            }
            catch (Exception ex)
            {
                lblNoData.Hide();
                General.LogExceptionWithShowError(ex, $"Error {ex.Message}");
            }
        }
        private void dgTransactions_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void OnlinePaymentTransactionsForm_Load(object sender, EventArgs e)
        {
            FormLoad();
            General.SetScreenSize(sender, e, this);
        }

        private void dgTransactions_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    string voucher = dgTransactions["referance_id", e.RowIndex].Value.ToString();
                    ViewJournalForm viewJournalForm = new ViewJournalForm(voucher);
                    viewJournalForm.ShowDialog();

                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
        private async Task PrintAsync()
        {
            try
            {
                SynchronizationBAL synchronizationBAL = new SynchronizationBAL();
                await synchronizationBAL.PrintOnlineReportAsync(General.ConvertDateServerFormat(dtpDateFrom.Value), General.ConvertDateServerFormat(dtpDateTo.Value), cmbMode.Text, 0);
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }

        private async Task GetTransactionStatusAsync(bool showMessage = true)
        {
            try
            {
                if (!string.IsNullOrEmpty(lblReference.Text))
                {
                    string apiUrl = "http://falconpaymentstatus.letzservices.com//api/Transaction/GetStatusCheckHash";
                    OrderStatusModel orderDetail = await GetStatusCheckHashAsync(apiUrl, lblReference.Text);
                    if (orderDetail != null)
                    {
                        FillRichTextBox(orderDetail);
                        pnlStatus.Show();
                    }
                    if (showMessage)
                        General.ShowMessage(General.EnumMessageTypes.Warning, orderDetail == null ? "No data" : orderDetail.OrderStatusValue);
                }
                else
                {
                    if (showMessage)
                        General.ShowMessage(General.EnumMessageTypes.Warning, "Select any records");
                }
            }
            catch (Exception ex)
            {
                General.LogExceptionWithShowError(ex, $"Unable to get status {ex.Message}");
            }
        }

        public async Task<OrderStatusModel> GetStatusCheckHashAsync(string apiUrl, string requestPayload)
        {
            HttpClient client = new HttpClient();

            try
            {
                // Set up the request content
                var content = new StringContent(requestPayload, Encoding.UTF8, "application/json");

                // Make the POST request
                HttpResponseMessage response = await client.PostAsync(apiUrl, content);
                response.EnsureSuccessStatusCode();

                // Read the response content
                string responseBody = await response.Content.ReadAsStringAsync();

                // Deserialize the response into OrderStatusModel
                OrderStatusModel orderStatus = JsonConvert.DeserializeObject<OrderStatusModel>(responseBody);

                return orderStatus;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Request error: {e.Message}");
                return null;
            }
        }

        private void FillRichTextBox(OrderStatusModel orderStatus)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                Type type = typeof(OrderStatusModel);
                PropertyInfo[] properties = type.GetProperties();

                foreach (PropertyInfo property in properties)
                {
                    var value = property.GetValue(orderStatus) ?? "null";
                    sb.AppendLine($"{property.Name}: {value}");
                }

                textStatus.Text = sb.ToString();
                this.OrderStatus = new OrderStatusModel();
                this.OrderStatus = orderStatus;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        private void linkClose_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pnlStatus.Hide();
        }

        private void linkUpdate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            UpdateCollectionAsync();
        }
        private async Task UpdateCollectionAsync(bool showMessage = true)
        {
            try
            {
                if (string.IsNullOrEmpty(currentStatus) || currentStatus.ToLower()=="awaited" || currentStatus.ToLower()== "initiated")
                {
                    if (!showMessage || General.ShowMessageConfirm() == DialogResult.Yes)
                    {
                        DAL.TransactionResponse transactionResponse = new DAL.TransactionResponse
                        {
                            ReferenceId = OrderStatus.OrderNo,
                            AmountReceived = OrderStatus.OrderCaptAmt,
                            PaymentReferenceId = OrderStatus.ReferenceNo,
                            PaymentMode = OrderStatus.OrderCardName,
                            StatusText = OrderStatus.OrderStatusValue,
                            Response = textStatus.Text,
                        };

                        if (transactionResponse != null && !string.IsNullOrEmpty(OrderStatus.OrderStatusValue))
                        {
                            DAL.DAL.SynchronizationDAL synchronization = new DAL.DAL.SynchronizationDAL(null);
                            synchronization.UpdateTransactionReponse(transactionResponse);
                            if(showMessage)
                            General.ShowMessage(General.EnumMessageTypes.Success, $"Status successfully changed as {OrderStatus.OrderStatusValue}");
                            pnlStatus.Hide();
                            if (showMessage)
                                await SearchAsync(General.ConvertDateServerFormat(dtpDateFrom.Value), General.ConvertDateServerFormat(dtpDateTo.Value), cmbMode.Text);
                        }
                    }
                }
                else
                {
                    General.ShowMessage(General.EnumMessageTypes.Warning, "Status already updated and cannot be modified");
                }
            }
            catch (Exception ex)
            {
                General.ShowMessage(General.EnumMessageTypes.Error, $"Error while updating status {ex.Message}");
            }
        }

        class OnlinePaymentTransaction
        {
            public Button BtnSearch { get; set; }
            public Button BtnCancel { get; set; }
            public Button BtnClose { get; set; }
            public Button BtnPrint { get; set; }
            public Button BtnStatus { get; set; }

        }

        private void dgTransactions_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                string refNo = dgTransactions["tracking_id", e.RowIndex].Value.ToString();
                currentStatus = dgTransactions["status_text", e.RowIndex].Value.ToString().ToLower();
                lblReference.Text = refNo;
            }
        }

        private void linkValidateAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ValidateAllNoResponseTransactions();
        }

        private void ValidateAllNoResponseTransactions(bool noMessage=false)
        {
            try
            {
                if ((cmbMode.Text == "No Status Recevied" || cmbMode.Text== "Initiated") && dgTransactions.Rows.Count > 0)
                {
                    if (noMessage || General.ShowMessageConfirm("Are you sure want to validate All. \n Do not do at working hours. It may cause to slow the entire system") == DialogResult.Yes)
                    {
                        ValidateAllRecords();
                    }
                }
            }
            catch (Exception ex)
            {
                General.LogExceptionWithShowError(ex, $"Error {ex.Message}");
            }
        }

        private async Task ValidateAllRecords()
        {
            try
            {
                
                foreach (DataGridViewRow dr in dgTransactions.Rows)
                {
                    General.Error($"Updating payment tracking Id: {dr.Cells["tracking_id"].Value}");
                    string refNo = dr.Cells["tracking_id"].Value.ToString();
                    currentStatus = dr.Cells["status_text"].Value.ToString().ToLower();
                    lblReference.Text = refNo;
                    await GetTransactionStatusAsync(false);
                    Application.DoEvents();
                    System.Threading.Thread.Sleep(500);
                    await UpdateCollectionAsync(false);
                    pnlStatus.Hide();
                    Application.DoEvents();
                    System.Threading.Thread.Sleep(500);
                    Random random = new Random();
                    Color randomColor = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
                    lblReference.BackColor = randomColor;
                }
                await SearchAsync(General.ConvertDateServerFormat(dtpDateFrom.Value), General.ConvertDateServerFormat(dtpDateTo.Value), cmbMode.Text);
            }
            catch (Exception ex)
            {
                General.LogExceptionWithShowError(ex, $"Error {ex.Message}");
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            cmbMode.Text = "No Status Recevied";
            SearchAsync(DateTime.Now, DateTime.Now, cmbMode.Text);
            ValidateAllNoResponseTransactions(true);
            Application.DoEvents();
            ButtonActive(EnumFormEvents.Search);
            cmbMode.Text = "Initiated";
            SearchAsync(DateTime.Now, DateTime.Now, cmbMode.Text);
            ValidateAllNoResponseTransactions(true);
            Application.DoEvents();
            ButtonActive(EnumFormEvents.Search);
            this.Text = $"Online Payment Transactions - last validated on {DateTime.Now}";

        }
    }
}
