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
    public partial class ChartDeliveryStatusForm : Form
    {
        public ChartDeliveryStatusForm()
        {
            InitializeComponent();
        }
        BETask.DAL.Model.ChartDataModel chartData = new DAL.Model.ChartDataModel();
        private void Delivery()
        {
            try
            {
                chartDelivery.Series["srCount"].Points.Clear();
                chartSales.Series["srSales"].Points.Clear();
                List<DAL.Model.ChartDelivery> listDelivery = chartData.listDelivery != null ? chartData.listDelivery : null;
                foreach (DAL.Model.ChartDelivery dl in listDelivery)
                {
                    chartDelivery.Series["srCount"].Points.AddXY(dl.Route, dl.Delivery);
                    chartSales.Series["srSales"].Points.AddXY(dl.Route, dl.Sales);
                }
                chartDelivery.Series["srCount"].IsValueShownAsLabel = true;
                chartSales.Series["srSales"].IsValueShownAsLabel = true;

                chartDelivery.ChartAreas["ChartArea1"].AxisX.Interval = 1;
                chartSales.ChartAreas["ChartArea1"].AxisX.Interval = 1;
                
            }
            catch (Exception ee) {}
        }

        private void Collection()
        {
            try
            {
                chartCollection.Series["srCollection"].Points.Clear();
                List<DAL.Model.ChartCollection> listCollection = chartData.listCollection != null ? chartData.listCollection : null;
                foreach (DAL.Model.ChartCollection dl in listCollection)
                {
                    chartCollection.Series["srCollection"].Points.AddXY(dl.PaymentMode, dl.Amount);
                   
                }
                chartCollection.Series["srCollection"].IsValueShownAsLabel = true;
            }
            catch (Exception ee) { }
        }
        private void LoadProducts()
        {
            try
            {
                ItemBAL itemBAL = new ItemBAL();
                List<EDMX.item> listProducts = itemBAL.GetAllItem_Sellable();
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
        private void LoadChartData()
        {
            try
            {
                lblError.Text = "";
                SaleBAL saleBAL = new SaleBAL();
                int itemId = 15;
                if (!String.IsNullOrEmpty(cmbProductName.Text))
                {
                    Object selectedProduct = cmbProductName.SelectedItem;
                    itemId = (int)((BETask.Views.ComboboxItem)selectedProduct).Value;
                }
                chartData = saleBAL.GetChartData(General.ConvertDateServerFormatWithStartTime(dtpDateFrom.Value), General.ConvertDateServerFormatWithEndTime(dtpDateFrom.Value), itemId);
                Delivery();
                Collection();
            }
            catch (Exception ee)
            {

            }
        }

        private void ChartDeliveryStatusForm_Load(object sender, EventArgs e)
        {
            LoadProducts();
            LoadChartData();
            timer1.Start();
        }

        private void dtpDateFrom_ValueChanged(object sender, EventArgs e)
        {
            LoadChartData();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            LoadChartData();
        }

        private void cmbProductName_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadChartData();
        }

      
    }
}
