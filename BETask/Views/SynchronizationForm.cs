using BETask.Common;
using BETask.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BETask.BAL;
using EDMX = BETask.DAL.EDMX;
using EDMXAPP = BETask.APP.EDMX;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace BETask.Views
{
    public partial class SynchronizationForm : Form
    {
        SyncButtonCollection button;
        SynchronizationBAL sync = new SynchronizationBAL();
        public SynchronizationForm()
        {
            InitializeComponent();
        }
        private void ButtonEvents(object sender, EventArgs e)
        {
            lblResult.Text = "";
            if (sender == button.CustomerDateUpdation)
            {
                CustomerDateUpdation();
            }
            else if (sender == button.CustomerOutstanding)
            {
                result = 0;
                lblResult.Text = $" Total {sync.CustomerOustatingCount()}  Customer outstanding and wallet being updated";
                Application.DoEvents();
                backgroundWorker1.RunWorkerAsync(1000);
            }
            else if (sender == button.Route)
            {
                Routes();
            }
            else if (sender == button.CustomerCollection)
            {
                OutstandingDailyCollection();
            }
            else if (sender == button.ReturnItems)
            {
                ReturnItems();
            }
            else if (sender == button.Delivery)
            {
                Delivery();
            }
            else if (sender == button.Backup)
            {
                Backup();
            }
            else if (sender == button.RouteCustomerOutstanding)
            {
                CustomerOutstandingRoute();
            }
            else if (sender == button.Building)
            {
                int res = sync.Building();
                lblResult.Text = res.ToString();
            }
            else if (sender == button.WalletGeneration)
            {
                WalletGeneration();
            }
            else if (sender == button.WalletSync)
            {
                WalletSyncAsync();
            }
            else if (sender == button.EnableLocation)
            {
                if (ValidateNumberTextBox(txtRange))
                    EnableLocation();
                else
                    General.ShowMessage(General.EnumMessageTypes.Error, "Not a valid range , should be a number");
            }
            else if (sender == button.DisableLocation)
            {
                if (string.IsNullOrEmpty(txtRange.Text) || Convert.ToInt32(txtRange.Text) == 0)
                    DisableLocation();
                else
                    General.ShowMessage(General.EnumMessageTypes.Error, "Not a valid range , should be empty or Zero");
            }
            else if (sender == button.UpdateCreditLimit)
            {
                UpdateCreditLimit();
            }
        }
        private bool ValidateNumberTextBox(TextBox textBox)
        {
            // Regular expression pattern for numbers (optional sign, digits)
            string numberPattern = @"^[-+]?\d+$";

            // Use Regex.IsMatch to check if the textbox value matches the pattern
            bool isValid = Regex.IsMatch(textBox.Text, numberPattern);

            return isValid;
        }
        private async void EnableLocation()
        {
            try
            {
                int routeId = General.GetComboBoxSelectedValue(cmbRoute);
                int effected = await sync.UpdateLocation(routeId, txtRange.Text.Trim().Replace("[","").Replace("]",""), true);
                General.ShowMessage(General.EnumMessageTypes.Success, $"{effected} Customers location enabled", "Location update");
            }
            catch (Exception ex)
            {
                General.LogExceptionWithShowError(ex, "Error while update location");
            }
        }
        private async void DisableLocation()
        {
            try
            {
                int routeId = General.GetComboBoxSelectedValue(cmbRoute);
                int effected = await sync.UpdateLocation(routeId, txtRange.Text.Trim().Replace("[", "").Replace("]", ""), false);
                General.ShowMessage(General.EnumMessageTypes.Success, $"{effected} Customers location disabled", "Location update");
            }
            catch (Exception ex)
            {
                General.LogExceptionWithShowError(ex, "Error while update location");
            }
        }

        private async void UpdateCreditLimit(decimal creditLimit=0)
        {
            try
            {
                if (string.IsNullOrEmpty(txtRange.Text))
                {
                    General.ShowMessage(General.EnumMessageTypes.Warning, "No credit limit added");
                    return;
                }
                else
                {
                    string rangeData = txtRange.Text;
                    if (decimal.TryParse(txtRange.Text.Replace("[", "").Replace("]", ""), out creditLimit))
                    {
                        if (General.ShowMessageConfirm($"Areyou sure want to update {cmbRoute.Text} credit limit as {creditLimit}") == DialogResult.Yes)
                        {
                            int routeId = General.GetComboBoxSelectedValue(cmbRoute);
                            int effected = await sync.UpdateCreditLimit(routeId, creditLimit);
                            General.ShowMessage(General.EnumMessageTypes.Success, $"{effected} Customers credit limit updated", "credit limit update");
                        }
                    }
                    else
                    {
                        General.ShowMessage(General.EnumMessageTypes.Warning, "Invalid amount");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                General.LogExceptionWithShowError(ex, "Error while update location");
            }
        }
        private void WalletGeneration()
        {
            try
            {
                BETask.DAL.DAL.WalletDAL walletDAL = new DAL.DAL.WalletDAL();
                lblWalletStatus.Text =$"{walletDAL.WalletPendingCount().ToString()} To be update...";
                Application.DoEvents();
                System.Threading.Thread.Sleep(500);
                List<EDMX.customer> listCustomer = walletDAL.GenerateWalletNumber();
                Application.DoEvents();
                lblWalletStatus.Text = $"{listCustomer.Count} updated. Updating cloud .. please wait";
                System.Threading.Thread.Sleep(500);
                int result=sync.WalletGenerationAPP(listCustomer);
                lblWalletStatus.Text= $"{result} updated.";

            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, "Sorry Unable to process ");
            }
        }
        public delegate void SomeDelegate();
        public static SomeDelegate someDelObj = null;
        private async System.Threading.Tasks.Task WalletSyncAsync()
        {
            try
            {
                int routeId = 0;
                if (!String.IsNullOrEmpty(cmbRoute.Text))
                {
                    Object selectedRoute = cmbRoute.SelectedItem;
                    routeId = (int)((BETask.Views.ComboboxItem)selectedRoute).Value;
                }

                    BETask.DAL.DAL.WalletDAL walletDAL = new DAL.DAL.WalletDAL();
                
                List<EDMX.customer> listCustomer = walletDAL.GenerateWalletSync(routeId);
                Application.DoEvents();
                lblWalletStatus.Text = $"{listCustomer.Count} to be updated. Updating cloud .. please wait";
                System.Threading.Thread.Sleep(500);
                lblWalletStatus.Text = $"{result} updated.";

            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, "Sorry Unable to process ");
            }
        }


        private void OutstandingDailyCollection()
        {
            try
            {
                int res = sync.OutstandingDailyCollection();
                lblResult.Text = $"{res} records updated";
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, "Sorry Unable to process ");

            }
        }
        private void Backup()
        {
            try
            {
                General.Backup();
                lblResult.Text = "Backup created";
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
       
        private async Task<int> CustomerOutstandingRoute()
        {
            int res = 0;
            try
            {
                int routeId = 0;
                if (!String.IsNullOrEmpty(cmbRoute.Text))
                {
                    Object selectedRoute = cmbRoute.SelectedItem;
                    routeId = (int)((BETask.Views.ComboboxItem)selectedRoute).Value;
                }
                lblResult.Text = "Updating customer outstanding...";
                res =await sync.CustomerOutstandingRoutewise(routeId);
                lblResult.Text = $" {res} updated";

            }
            catch (Exception ee)
            {
                throw;
            }
            return res;
        }
        private void Routes()
        {
            try
            {
              int res=  sync.Route();
                lblResult.Text = $"{res} records updated";
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
               
            }
        }
        private void CustomerDateUpdation()
        {
            try
            {
                int res = sync.CustomerDateUpdation();
                if (res>0)
                { lblResult.Text = $"{res} records updated"; }
                else
                { lblResult.Text = $"Sorry Unable to process try again"; }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, "Sorry Unable to process ");

            }
        }
        private void ReturnItems()
        {
            try
            {
              int result=   sync.DeliveryReturnItems();
                lblResult.Text = $"{result} rows updated";
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
        private void Delivery()
        {
            try
            {
                sync.DeliveryFromApp();
                sync.Delivery(dtpDate.Value);
                lblResult.Text = $"updated";
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
        private void Coupon()
        {
            try
            {
                sync.RedeemedCoupon();
                lblResult.Text = $"updated";
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
        private void SynchronizationForm_Load(object sender, EventArgs e)
        {
            button = new SyncButtonCollection
            {
                CustomerOutstanding = btnCustomerOutstanding,
                Route = btnRoute,
                CustomerCollection = btnCustomerCollection,
                ReturnItems = btnDeliveryReturnItems,
                Delivery = btnDelivery,
                Coupon = btnCoupon,
                Backup = btnBackup,
                RouteCustomerOutstanding = btnRouteCustomerOutstanding,
                CustomerDateUpdation = btnCustomerDateUpdation,
                Building = btnBuilding,
                WalletGeneration = btnWalletGeneration,
                WalletSync = btnWalletSync,
                EnableLocation=btnEnableLocation,
                DisableLocation=btnDisableLocation,
                UpdateCreditLimit=btnUpdateCreditLimit
            };
            GetAllRoutes();
        }

        int result = 0;
        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
          
           
           
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            this.backgroundWorker1.CancelAsync();
            lblResult.Text = $"{result} Done";
        }

        private void backgroundWorker1_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            string ss = "ss";
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

        
    }

    class SyncButtonCollection
    {
        public Button CustomerDateUpdation { get; set; }
        public Button CustomerOutstanding { get; set; }
        public Button RouteCustomerOutstanding { get; set; }
        public Button Route { get; set; }
        public Button CustomerCollection { get; set; }
        public Button ReturnItems { get; set; }
        public Button Delivery { get; set; }
        public Button Coupon { get; set; }
        public Button WalletGeneration { get; set; }
        public Button WalletSync { get; set; }
        public Button Building { get; set; }
        public Button Backup { get; set; }
        public Button EnableLocation { get; set; }
        public Button DisableLocation { get; set; }
        public Button UpdateCreditLimit { get; set; }

    }
}
