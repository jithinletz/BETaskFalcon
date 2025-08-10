using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using BETask.Common;
using BETask.BAL;
using System.Windows.Forms;
using EDMX = BETask.DAL.EDMX;
using BETask.Model;

namespace BETask.Views
{
    public partial class CustomerAssetReportForm : Form
    {
        List<DAL.EDMX.route> listRoute;
        CustomerAssetReportButtonCollection button;
        public enum EnumFormEvents

        {
            FormLoad,           
            Close,            
            Print,
            Show
        }

        private void ButtonActive(Enum activeEvent)
        {

            switch (activeEvent)
            {
                case EnumFormEvents.FormLoad:                   
                    break;                
                case EnumFormEvents.Close:
                    this.BeginInvoke(new MethodInvoker(Close));
                    break;               
                case EnumFormEvents.Print:
                    PrintAll();
                    break;
                case EnumFormEvents.Show:
                    Search();
                    break;
                default:
                    break;

            }
        }

        private void ButtonEvents(object sender, EventArgs e)
        {
            if (sender == button.BtnClose)
            {
                ButtonActive(EnumFormEvents.Close);
            }

            else if (sender == button.BtnPrint)
            {
                ButtonActive(EnumFormEvents.Print);
            }
            else if (sender == button.BtnShow)
            {
                ButtonActive(EnumFormEvents.Show);
            }

        }
        public CustomerAssetReportForm()
        {
            InitializeComponent();
        }

        private void PrintAll()
        {

            try
            {
                int routeId = General.GetComboBoxSelectedValue(cmbRoute);
                string deliveryMode = cmbReturnType.Text;
                DateTime fromDate = General.ConvertDateServerFormat(dtpFromDate.Value);
                DateTime toDate = General.ConvertDateServerFormat(dtpToDate.Value);
                string header = cmbRoute.Text;
                string barcode = txtBarcode.Text;
                BAL.CustomerAssetBAL customerAssetBAL = new CustomerAssetBAL();
                customerAssetBAL.PrintAll(routeId,header, deliveryMode, fromDate, toDate,barcode);
            }
            catch (Exception ex)
            {
                General.LogExceptionWithShowError(ex, "Unable to load report");
            }
        }
        private void FormLoad()
        {
            button = new CustomerAssetReportButtonCollection
            {
                BtnClose = btnClose,
                BtnPrint = btnPrint,
                BtnShow = btnShow
            };

            GetAllRoutes();
            Search();
        }

        

        private void Search()
        {
            try
            {
                General.ClearGrid(dgAssetReport);
                CustomerAssetBAL customerBAL = new CustomerAssetBAL();
                int routeId = General.GetComboBoxSelectedValue(cmbRoute);
                string deliveryMode =cmbReturnType.Text;
                DateTime fromDate = General.ConvertDateServerFormat(dtpFromDate.Value);
                DateTime toDate = General.ConvertDateServerFormat(dtpToDate.Value);
                string barcode = txtBarcode.Text;
                List<EDMX.customer_asset> listAsset = customerBAL.GetAllCustomerAsset(routeId ,deliveryMode,fromDate,toDate,barcode);
                if (listAsset != null)
                {
                    foreach (EDMX.customer_asset asset in listAsset)
                    {
                        dgAssetReport.Rows.Add(asset.customer.customer_name, asset.item.item_name, General.ConvertDateAppFormat(asset.delivery_date),asset.qty,asset.other_details);
                    }
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }

        private void GetAllRoutes()
        {
            try
            {
                RouteBAL routeBAL = new RouteBAL();                
                listRoute = routeBAL.GetAllRoutes();                
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

        private void NextFocus(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                General.NextFocus(sender, e);
            }
        }
        private void CustomerAssetReportForm_Load(object sender, EventArgs e)
        {
            FormLoad();
            


        }

        class CustomerAssetReportButtonCollection
        {
         
            public Button BtnClose { get; set; }
            public Button BtnPrint { get; set; }
            public Button BtnShow { get; set; }

        }

       
        private void dgAssetReport_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtBarcode_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void dtpToDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dtpFromDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void cmbReturnType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void cmbRoute_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
