using BETask.Common;
using BETask.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BETask.BAL;
using EDMX = BETask.DAL.EDMX;
using System.Data;
using RPT = BETask.Report.ReportForm;


namespace BETask.Views
{
    public partial class SaleAnalyseReport : Form
    {
        SaleBAL saleBAL = new SaleBAL();
        public enum EnumFormEvents
        {
            FormLoad,
            Close,
            Print,
            Search

        }
        SaleAnalyseButtonCollection button;
        public SaleAnalyseReport()
        {
            InitializeComponent();
        }
        private void ButtonEvents(object sender, EventArgs e)
        {


            if (sender == button.BtnPrint)
            {
                ButtonActive(EnumFormEvents.Print);
            }
            else if (sender == button.BtnClose)
            {
                ButtonActive(EnumFormEvents.Close);
            }
            else if (sender == button.BtnSearch)
            {
                ButtonActive(EnumFormEvents.Search);
            }
        }
        private void ButtonActive(Enum activeEvent)
        {

            switch (activeEvent)
            {
                case EnumFormEvents.FormLoad:

                    break;

                case EnumFormEvents.Search:
                    if (chkItemwise.Checked && cmbProductName.Text != "")
                        SearchItemwise();
                    else
                        Search();
                    break;

                case EnumFormEvents.Close:
                    this.BeginInvoke(new MethodInvoker(Close));
                    break;

                case EnumFormEvents.Print:
                    Print();
                    break;
                default:
                    break;

            }
        }
        private void Print()
        {
            try
            {
                string header = $"{General.companyName} - {dtpDateFrom.Text }";
                if (chkItemwise.Checked && cmbProductName.Text != "")
                    header = $"{General.companyName} - {dtpDateFrom.Text } - {cmbProductName.Text}";
                DataTable tblReport = new DataTable();
                BETask.Report.DSReports.MonthlyAnalyseDataTable monthlyAnalyseDataTable = new Report.DSReports.MonthlyAnalyseDataTable();
                tblReport = monthlyAnalyseDataTable.Clone();
                foreach (DataGridViewRow gr in gridReport.Rows)
                {
                    DataRow dr = tblReport.NewRow();
                    dr["Route"] = gr.Cells[0].Value;
                    dr[1] = gr.Cells[1].Value;
                    dr[2] = gr.Cells[2].Value;
                    dr[3] = gr.Cells[3].Value;
                    dr[4] = gr.Cells[4].Value;
                    dr[5] = gr.Cells[5].Value;
                    dr[6] = gr.Cells[6].Value;
                    dr[7] = gr.Cells[7].Value;
                    dr[8] = gr.Cells[8].Value;
                    dr[9] = gr.Cells[9].Value;
                    dr[10] = gr.Cells[10].Value;
                    dr[11] = gr.Cells[11].Value;
                    dr[12] = gr.Cells[12].Value;
                    tblReport.Rows.Add(dr);
                }
                if (tblReport != null && tblReport.Rows.Count > 0)
                {
                    RPT reportForm = new RPT(RPT.EnumReportType.MonthlyAnalyse, header, tblReport);
                    reportForm.Show();
                }
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
                List<EDMX.SP_SalesMonthlyAnalysis_Result> listMonthly = saleBAL.GetSaleMonthlyAnaysis(Convert.ToInt32(dtpDateFrom.Text));
                if (listMonthly != null && listMonthly.Count > 0)
                {
                    GridTemplateMonthly();
                    List<string> routes = listMonthly.Select(x => x.RouteName).Distinct().ToList();
                    decimal jan = 0, feb = 0, mar = 0, apr = 0, may = 0, jun = 0, jul = 0, aug = 0, sep = 0, oct = 0, nov = 0, dec = 0;
                    foreach (string rt in routes)
                    {

                        List<EDMX.SP_SalesMonthlyAnalysis_Result> _list = listMonthly.Where(x => x.RouteName == rt).ToList();
                        jan = 0; feb = 0; mar = 0; apr = 0; may = 0; jun = 0; jul = 0; aug = 0; sep = 0; oct = 0; nov = 0; dec = 0;
                        foreach (EDMX.SP_SalesMonthlyAnalysis_Result mo in _list)
                        {

                            switch (mo.Month)
                            {
                                case 1:
                                    jan = Convert.ToDecimal(mo.SaleAmoumt);
                                    break;
                                case 2:
                                    feb = Convert.ToDecimal(mo.SaleAmoumt);
                                    break;
                                case 3:
                                    mar = Convert.ToDecimal(mo.SaleAmoumt);
                                    break;
                                case 4:
                                    apr = Convert.ToDecimal(mo.SaleAmoumt);
                                    break;
                                case 5:
                                    may = Convert.ToDecimal(mo.SaleAmoumt);
                                    break;
                                case 6:
                                    jun = Convert.ToDecimal(mo.SaleAmoumt);
                                    break;
                                case 7:
                                    jul = Convert.ToDecimal(mo.SaleAmoumt);
                                    break;
                                case 8:
                                    aug = Convert.ToDecimal(mo.SaleAmoumt);
                                    break;
                                case 9:
                                    sep = Convert.ToDecimal(mo.SaleAmoumt);
                                    break;
                                case 10:
                                    oct = Convert.ToDecimal(mo.SaleAmoumt);
                                    break;
                                case 11:
                                    nov = Convert.ToDecimal(mo.SaleAmoumt);
                                    break;
                                case 12:
                                    dec = Convert.ToDecimal(mo.SaleAmoumt);
                                    break;

                            }

                        }
                        gridReport.Rows.Add(rt, jan, feb, mar, apr, may, jun, jul, aug, sep, oct, nov, dec);
                    }
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
        private void SearchItemwise()
        {
            try
            {
                int itemId = 0;
                if (!String.IsNullOrEmpty(cmbProductName.Text))
                {
                    Object selectedProduct = cmbProductName.SelectedItem;
                    itemId = (int)((BETask.Views.ComboboxItem)selectedProduct).Value;
                }
                List<EDMX.SP_SalesMonthlyItemAnalysis_Result> listMonthly = saleBAL.GetSaleMonthlyItemAnaysis(Convert.ToInt32(dtpDateFrom.Text), itemId);
                if (listMonthly != null && listMonthly.Count > 0)
                {
                    GridTemplateMonthly();
                    List<string> routes = listMonthly.Select(x => x.RouteName).Distinct().ToList();
                    decimal jan = 0, feb = 0, mar = 0, apr = 0, may = 0, jun = 0, jul = 0, aug = 0, sep = 0, oct = 0, nov = 0, dec = 0;
                    foreach (string rt in routes)
                    {

                        List<EDMX.SP_SalesMonthlyItemAnalysis_Result> _list = listMonthly.Where(x => x.RouteName == rt).ToList();
                        jan = 0; feb = 0; mar = 0; apr = 0; may = 0; jun = 0; jul = 0; aug = 0; sep = 0; oct = 0; nov = 0; dec = 0;
                        foreach (EDMX.SP_SalesMonthlyItemAnalysis_Result mo in _list)
                        {

                            switch (mo.Month)
                            {
                                case 1:
                                    jan = Convert.ToDecimal(mo.Qty);
                                    break;
                                case 2:
                                    feb = Convert.ToDecimal(mo.Qty);
                                    break;
                                case 3:
                                    mar = Convert.ToDecimal(mo.Qty);
                                    break;
                                case 4:
                                    apr = Convert.ToDecimal(mo.Qty);
                                    break;
                                case 5:
                                    may = Convert.ToDecimal(mo.Qty);
                                    break;
                                case 6:
                                    jun = Convert.ToDecimal(mo.Qty);
                                    break;
                                case 7:
                                    jul = Convert.ToDecimal(mo.Qty);
                                    break;
                                case 8:
                                    aug = Convert.ToDecimal(mo.Qty);
                                    break;
                                case 9:
                                    sep = Convert.ToDecimal(mo.Qty);
                                    break;
                                case 10:
                                    oct = Convert.ToDecimal(mo.Qty);
                                    break;
                                case 11:
                                    nov = Convert.ToDecimal(mo.Qty);
                                    break;
                                case 12:
                                    dec = Convert.ToDecimal(mo.Qty);
                                    break;

                            }

                        }
                        gridReport.Rows.Add(rt, jan, feb, mar, apr, may, jun, jul, aug, sep, oct, nov, dec);
                    }
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
        private void GridTemplateMonthly()
        {
            gridReport.Columns.Clear();
            DataGridViewTextBoxColumn clmRoute = new DataGridViewTextBoxColumn(); clmRoute.HeaderText = "Route Name"; clmRoute.Width = 250;
            DataGridViewTextBoxColumn clm1 = new DataGridViewTextBoxColumn(); clm1.HeaderText = "January"; clm1.Width = 100;
            DataGridViewTextBoxColumn clm2 = new DataGridViewTextBoxColumn(); clm2.HeaderText = "February"; clm2.Width = 100;
            DataGridViewTextBoxColumn clm3 = new DataGridViewTextBoxColumn(); clm3.HeaderText = "March"; clm3.Width = 100;
            DataGridViewTextBoxColumn clm4 = new DataGridViewTextBoxColumn(); clm4.HeaderText = "April"; clm4.Width = 100;
            DataGridViewTextBoxColumn clm5 = new DataGridViewTextBoxColumn(); clm5.HeaderText = "May"; clm5.Width = 100;
            DataGridViewTextBoxColumn clm6 = new DataGridViewTextBoxColumn(); clm6.HeaderText = "June"; clm6.Width = 100;
            DataGridViewTextBoxColumn clm7 = new DataGridViewTextBoxColumn(); clm7.HeaderText = "July"; clm7.Width = 100;
            DataGridViewTextBoxColumn clm8 = new DataGridViewTextBoxColumn(); clm8.HeaderText = "August"; clm8.Width = 100;
            DataGridViewTextBoxColumn clm9 = new DataGridViewTextBoxColumn(); clm9.HeaderText = "September"; clm9.Width = 100;
            DataGridViewTextBoxColumn clm10 = new DataGridViewTextBoxColumn(); clm10.HeaderText = "October"; clm10.Width = 100;
            DataGridViewTextBoxColumn clm11 = new DataGridViewTextBoxColumn(); clm11.HeaderText = "November"; clm11.Width = 100;
            DataGridViewTextBoxColumn clm12 = new DataGridViewTextBoxColumn(); clm12.HeaderText = "December"; clm12.Width = 100;
            gridReport.Columns.Add(clmRoute);
            gridReport.Columns.Add(clm1);
            gridReport.Columns.Add(clm2);
            gridReport.Columns.Add(clm3);
            gridReport.Columns.Add(clm4);
            gridReport.Columns.Add(clm5);
            gridReport.Columns.Add(clm6);
            gridReport.Columns.Add(clm7);
            gridReport.Columns.Add(clm8);
            gridReport.Columns.Add(clm9);
            gridReport.Columns.Add(clm10);
            gridReport.Columns.Add(clm11);
            gridReport.Columns.Add(clm12);
        }

        private void FormLoad()
        {
            button = new SaleAnalyseButtonCollection
            {
                BtnClose = btnClose,
                BtnPrint = btnPrint,
                BtnSearch = btnSearch
            };
            LoadProducts();
        }
        private void LoadProducts()
        {
            try
            {
                ItemBAL itemBAL = new ItemBAL();
                List<EDMX.item>   listProducts = itemBAL.GetAllItem_Sellable();
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
              //  General.ShowMessage(General.EnumMessageTypes.Error);
            }
        }
        private void SaleAnalyseReport_Load(object sender, EventArgs e)
        {
            FormLoad();
        }

        private void chkItemwise_CheckedChanged(object sender, EventArgs e)
        {
            if (chkItemwise.Checked)
            {
                cmbProductName.Show();
            }
            else
                cmbProductName.Hide();
        }
    }
    class SaleAnalyseButtonCollection
    {

        public Button BtnPrint { get; set; }
        public Button BtnSearch { get; set; }
        public Button BtnClose { get; set; }

    }
}
