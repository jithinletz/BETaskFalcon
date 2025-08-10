using System;
using BETask.Common;
using System.Data;
using System.Collections.Generic;
using System.Windows.Forms;
using BETask.BAL;
using EDMX = BETask.DAL.EDMX;

namespace BETask.Views
{
    public partial class MiscDeliverySaleCollectionCompare : Form
    {
        public enum EnumFormEvents
        {
            FormLoad,
            Cancel,
            Close,
            Search,
            SearchDifference,
            Print,
            UpdateClose
        }
        public bool isDifferenceSearch = false;
        DeliverySaleCollectionCompareButtonCollection button;
        public MiscDeliverySaleCollectionCompare()
        {
            InitializeComponent();
        }
        public MiscDeliverySaleCollectionCompare(DateTime date)
        {
            InitializeComponent();
            dtpDateFrom.Value = date;
            dtpDateTo.Value = date;
            Search();
        }
        private void ButtonActive(Enum activeEvent)
        {

            switch (activeEvent)
            {
               
                case EnumFormEvents.Close:
                    this.BeginInvoke(new MethodInvoker(Close));
                    break;
                case EnumFormEvents.Search:
                    Search();
                    break;
                case EnumFormEvents.Print:
                  //  Print();
                    break;
                case EnumFormEvents.UpdateClose:
                    pnlEditColl.Hide();
                    break;
                case EnumFormEvents.SearchDifference:
                    SearchDifference();
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
            else if (sender == button.BtnClose)
            {
                ButtonActive(EnumFormEvents.Close);
            }
            else if (sender == button.BtnUpdateClose)
            {
                ButtonActive(EnumFormEvents.UpdateClose);
            }
            else if (sender == button.BtnSearchDifference)
            {
                ButtonActive(EnumFormEvents.SearchDifference);
            }

        }

        private void MiscWalletDifferenceForm_Load(object sender, EventArgs e)
        {
            button = new DeliverySaleCollectionCompareButtonCollection()
            {
                BtnSearch = btnSearch,
                BtnClose = btnClose,
                BtnSearchDifference=btnSearchDifference,
                BtnUpdateClose=btnUpdateClose
            };
            Search();
        }
        private void Search()
        {
            try
            {
                isDifferenceSearch = false;
                DAL.DAL.MiscellaneousReportDAL reportDAL = new DAL.DAL.MiscellaneousReportDAL();
                DataTable tblReport = reportDAL.DeliveySaleCompare(General.ConvertDateServerFormat( dtpDateFrom.Value), General.ConvertDateServerFormat(dtpDateTo.Value));
                gridCustomer.DataSource = tblReport;
                General.GridRownumber(gridCustomer);
                gridCustomer.Columns[1].Width = 80;
                gridCustomer.Columns[2].Width = 80;
                gridCustomer.Columns[5].Width = 80;
                gridCustomer.Columns[6].Width = 80;
                gridCustomer.Columns[7].Width = 80;
                gridCustomer.Columns[3].Width = 180;
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
        private void SearchDifference()
        {
            try
            {
                isDifferenceSearch = true;
                DAL.DAL.MiscellaneousReportDAL reportDAL = new DAL.DAL.MiscellaneousReportDAL();
                DataTable tblReport = reportDAL.MiscSaleTotalDifference(General.ConvertDateServerFormat(dtpDateFrom.Value), General.ConvertDateServerFormat(dtpDateTo.Value));
                gridCustomer.DataSource = tblReport;
                General.GridRownumber(gridCustomer);
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }

        private void gridCustomer_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    string cutomer = gridCustomer[2, e.RowIndex].Value.ToString();
                    int cutomerId = Convert.ToInt32(gridCustomer[1, e.RowIndex].Value.ToString());
                    Views.WalletForm wallet = new WalletForm(cutomer, cutomerId);
                    wallet.ShowDialog();
                }
            }
            catch(Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }

       
        int customerId ;
        int deliveryId ;
        string customerName;
        DateTime date = new DateTime();
        private void gridCustomer_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                try
                { 
                 date = Convert.ToDateTime(gridCustomer["delivery_date",e.RowIndex].Value);
                 customerId = Convert.ToInt32(gridCustomer["customer_id", e.RowIndex].Value);
                 deliveryId = Convert.ToInt32(gridCustomer["delivery_id", e.RowIndex].Value);
                    customerName = gridCustomer["customer_name", e.RowIndex].Value.ToString();
                    LoadDetails(date,customerId,deliveryId);
                }
                catch (Exception ex)
                {
                    General.Error(ex.ToString());
                    General.ShowMessage(General.EnumMessageTypes.Error);
                }
            }
        }
        private void LoadDetails(DateTime date, int customerId, int deliveryId)
        {
            try
            {
                DAL.DAL.MiscellaneousReportDAL reportDAL = new DAL.DAL.MiscellaneousReportDAL();
                DataSet ds = reportDAL.DeliveySaleCompare_Details(date, customerId, deliveryId);
                if (ds != null)
                {
                    gridSale.DataSource = ds.Tables[0];
                    gridCollection.DataSource = ds.Tables[1];
                    gridCollection.Columns[0].Width = 150;
                    gridDelivery.DataSource = ds.Tables[2];
                    gridDelivery.Columns[0].Width = 150;
                    pnlEditColl.Show();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void MiscDeliverySaleCollectionCompare_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
                pnlEditColl.Hide();
        }

        private void gridSale_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                string salesNumber = gridSale["sales_number", e.RowIndex].Value.ToString();
                SaleForm saleForm = new SaleForm(salesNumber);
                saleForm.ShowDialog();
            }
        }

        private void gridCollection_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
             
                DailyCollectionForm collForm = new DailyCollectionForm(date,customerName,customerId);
                collForm.ShowDialog();
            }
        }
    }

    class DeliverySaleCollectionCompareButtonCollection
    {
        public Button BtnSearch { get; set; }
        public Button BtnSearchDifference { get; set; }

        public Button BtnClose { get; set; }
        public Button BtnUpdateClose { get; set; }

    }
}
