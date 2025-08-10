using BETask.Common;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BETask.BAL;
using System.Linq;
using System.Data;

namespace BETask.Views
{
    public partial class ProfitandLossForm : Form
    {
        public enum EnumFormEvents
        {
            FormLoad,
            New,
            Search,
            Cancel,
            Close,
            Print,

        }
       public decimal profitloss = 0;
        PandLButtonCollection button;
        BAL.ProfitandLossBAL profitandLoss = new ProfitandLossBAL();
        public ProfitandLossForm()
        {
            InitializeComponent();
        }
        public ProfitandLossForm(DateTime dateFrom,DateTime dateTo)
        {
            InitializeComponent();
            dtpFrom.Value = dateFrom;
            dtpTo.Value = dateTo;
            SearchNew();
        }
        private void ButtonActive(Enum activeEvent)
        {

            switch (activeEvent)
            {
                case EnumFormEvents.FormLoad:

                    break;
                case EnumFormEvents.Cancel:
                    ButtonActive(EnumFormEvents.FormLoad);
                    General.ClearTextBoxes(this);
                    General.ClearGrid(gridPandL);

                    break;
                case EnumFormEvents.Close:
                    this.BeginInvoke(new MethodInvoker(Close));
                    break;
                case EnumFormEvents.Search:
                    //Search();
                    SearchNew();
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

            if (sender == button.BtnCancel)
            {
                ButtonActive(EnumFormEvents.Cancel);
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
       
        private void Search()
        {
            try
            {
                General.ClearGrid(gridPandL);
                List<DAL.Model.ProfitandLossModel> listPandL = profitandLoss.GenerateProfitandLoss(General.ConvertDateServerFormat(dtpFrom.Value),General.ConvertDateServerFormat( dtpTo.Value));
                if (listPandL != null && listPandL.Count > 0)
                {
                    foreach (DAL.Model.ProfitandLossModel pl in listPandL)
                    {
                        string amount1 = "", amount2 = "", amount3 = "";
                        if (pl.amount1 != 0) amount1 = pl.amount1.ToString();
                        if (pl.amount2 != 0) amount2 = pl.amount2.ToString();
                        if (pl.amount3 != 0) amount3 = pl.amount3.ToString();
                        gridPandL.Rows.Add(pl.Description, amount1, amount2, amount3);
                        
                    }
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
      
        private void SearchNew()
        {
            try
            {
                General.ClearGrid(gridPLNew);
                DAL.Model.ProfitandLossModelNew PL = profitandLoss.GenerateProfitandLossNew(General.ConvertDateServerFormat(dtpFrom.Value), General.ConvertDateServerFormat(dtpTo.Value));

                gridPLNew.Rows.Add("Opening Stock.", PL.OpeningStock, "Sales", PL.Sale-PL.SaleReturn);
                gridPLNew.Rows.Add("", "", "    Sale ", (PL.Sale-(PL.SaleReturn+PL.SaleTax )));
                gridPLNew.Rows.Add("", "", "    Sales Return", PL.SaleReturn);
                //gridPLNew.Rows.Add("", "", "    Sales Tax", PL.SaleTax);
                gridPLNew.Rows.Add("Purchase.", PL.Purchase-(PL.PurchaseReturn+PL.PurchaseTax), "Closing Stock.", PL.ClosingStock);
                gridPLNew.Rows.Add("    Purchase", PL.Purchase, "", "");
                gridPLNew.Rows.Add("    Purchase Return", PL.PurchaseReturn, "", "");
               // gridPLNew.Rows.Add("    Purchase Tax", PL.PurchaseTax, "", "");

                decimal totalDExp = 0, totalCR = 0, totalDR = 0,grossProfit=0;
                if (PL.ListDirectExp != null)
                {
                    totalDExp = PL.ListDirectExp.Sum(x => x.amount1);
                    gridPLNew.Rows.Add("Direct Expense.", PL.ListDirectExp.Sum(x => x.amount1), "", "");
                    foreach (var dexp in PL.ListDirectExp)
                    {
                        if ((dexp.amount1 == -1 || dexp.amount1 == 0) && dexp.amount2 == -1)
                        {
                            gridPLNew.Rows.Add($"  {dexp.Description}", dexp.amount3, "", "");
                            General.GridForecolorBlue(gridPLNew);
                        }
                        else
                            gridPLNew.Rows.Add($"     {dexp.Description}", dexp.amount1, "", "");
                    }
                }
                totalCR = (PL.Sale - (PL.SaleReturn+PL.SaleTax))+PL.ClosingStock;
                totalDR = PL.OpeningStock + (PL.Purchase - (PL.PurchaseReturn+PL.PurchaseTax)) + totalDExp;
                grossProfit = totalCR - totalDR;
                if ( dtpTo.Value<= General.softwareStartDate)
                {
                    grossProfit = 0; totalCR = 0;
                }
                gridPLNew.Rows.Add("Gross Profit.", grossProfit, "", "");
                gridPLNew.Rows.Add("", totalCR, "", totalCR);
                gridPLNew.Rows.Add("_".PadRight(300,'_'), "_".PadRight(200, '_'), "_".PadRight(300, '_'), "_".PadRight(200, '_'));
                gridPLNew.Rows.Add("", "", "Gross Profit.", grossProfit);
                General.GridBackcolorYellow(gridPLNew);

                decimal totalINExp = 0, totalINIncome = 0;
                if (PL.ListINDirectExp != null)
                {
                    totalINExp = PL.ListINDirectExp.Sum(x => x.amount1);
                    gridPLNew.Rows.Add("Indirect Expense.", PL.ListINDirectExp.Sum(x => x.amount1), "", "");
                    foreach (var dexp in PL.ListINDirectExp)
                    {
                        if ((dexp.amount1 == -1 || dexp.amount1 == 0) && dexp.amount2 == -1)
                        {
                            gridPLNew.Rows.Add($"  {dexp.Description}", dexp.amount3, "", "");
                            General.GridForecolorBlue(gridPLNew);
                        }
                        else
                            gridPLNew.Rows.Add($"     {dexp.Description}", dexp.amount1, "", "");
                    }
                }
                if (PL.ListINDirectIncome != null && PL.ListINDirectIncome.Count > 0)
                {
                    totalINIncome = PL.ListINDirectIncome.Sum(x => x.amount1);
                    gridPLNew.Rows.Add("", "","Indirect Income.", PL.ListINDirectIncome.Sum(x => x.amount1) );
                    foreach (var dexp in PL.ListINDirectIncome)
                    {
                        if ((dexp.amount1 == -1 || dexp.amount1 == 0) && dexp.amount2 == -1)
                        {
                            gridPLNew.Rows.Add("", "", $"  {dexp.Description}", dexp.amount3);
                            General.GridForecolorBlue(gridPLNew);
                        }
                        else
                            gridPLNew.Rows.Add("", "", $"     {dexp.Description}", dexp.amount1);
                       
                    }
                }
                if (PL.ListDirectIncome != null && PL.ListDirectIncome.Count>0)
                {
                    totalINIncome = PL.ListDirectIncome.Sum(x => x.amount1);
                    gridPLNew.Rows.Add("", "", "Direct Income.", PL.ListDirectIncome.Sum(x => x.amount1));
                    foreach (var dexp in PL.ListDirectIncome)
                    {
                        if ((dexp.amount1 == -1 || dexp.amount1 == 0) && dexp.amount2 == -1)
                        {
                            gridPLNew.Rows.Add("", "", $"  {dexp.Description}", dexp.amount3);
                            General.GridForecolorBlue(gridPLNew);
                        }
                        else
                            gridPLNew.Rows.Add("", "", $"     {dexp.Description}", dexp.amount1);

                    }
                }
                decimal netProfit = 0, netCR = 0;
                netCR = grossProfit + totalINIncome;
                gridPLNew.Rows.Add("", netCR, "",netCR);
                netProfit = netCR - totalINExp;
                gridPLNew.Rows.Add("Net Profit#", netProfit, "", "");
                General.GridBackcolorRed(gridPLNew);
                profitloss = netProfit;
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
        private void Print()
        {
            try
            {
                if (gridPLNew.Rows.Count > 0)
                {
                    PostProfitandLoss();
                    profitandLoss.Print(General.ConvertDateServerFormat(dtpFrom.Value), General.ConvertDateServerFormat(dtpTo.Value), gridPLNew);
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
        private void PostProfitandLoss()
        {
            try
            {
                CompanyBAL _company = new CompanyBAL();
                Model.CompanyModel company = _company.GetCompanyDetails();
                if (company != null)
                {
                    if (dtpFrom.Value.ToString("dd/MM/yyyy") == company.FinancialDateFrom.ToString("dd/MM/yyyy") && dtpTo.Value.ToString("dd/MM/yyyy") == company.FinancialDateTo.ToString("dd/MM/yyyy"))
                    {
                        if (General.ShowMessageConfirm("Are you want to consider the amount as current year profit/loss") == DialogResult.Yes)
                        {
                            decimal debit = 0, credit = 0;
                            if (profitloss > 0)
                                credit = profitloss;
                            else
                                debit = profitloss * -1;
                            profitandLoss.PostProfitAndLoss(debit, credit, General.ConvertDateServerFormat(dtpTo.Value));
                        }
                    }
                }
            }
            catch (Exception ex)
            { }
        }

        private void ProfitandLossForm_Load(object sender, EventArgs e)
        {
            button = new PandLButtonCollection
            {
                BtnCancel=btnCancel,
                BtnClose=btnClose,
                BtnSearch=btnSearch,
                BtnPrint=btnPrint
            };
            CompanyBAL companyBal = new CompanyBAL();
            Model.CompanyModel company = companyBal.GetCompanyDetails();
            dtpFrom.Value = company.FinancialDateFrom;
            dtpTo.Value = company.FinancialDateTo;
        }

        private void gridPLNew_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && (e.ColumnIndex == 0 || e.ColumnIndex == 2))
                {
                    string desc = gridPLNew[e.ColumnIndex, e.RowIndex].Value.ToString().ToLower();
                    string date = "";
                    if (desc.Contains("opening stock"))
                        date = dtpFrom.Value.ToString("yyyy-MM-dd");
                    else if (desc.Contains("closing stock"))
                        date = dtpTo.Value.AddDays(1).ToString("yyyy-MM-dd");

                    if (date != "")
                        GetClosingValueDetailed(date);
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
        private void GetClosingValueDetailed(string date)
        {
            try
            {
                ItemTransactionBAL itemTransaction = new ItemTransactionBAL();
                DataTable tblData = itemTransaction.GetClosingValueDetailed(date);
                if (tblData != null)
                {
                    gridClosing.DataSource = tblData;
                    pnlSearch.Show();
                    gridClosing.Columns[0].Width = 80;
                    gridClosing.Columns[1].Width = 200;
                }
            }
            catch (Exception ee)
            {
                throw;
            }
        }

        private void linkCloseSearch_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pnlSearch.Hide();
        }
    }
    class PandLButtonCollection
    {
        public Button BtnCancel { get; set; }
        public Button BtnClose { get; set; }
        public Button BtnSearch { get; set; }
        public Button BtnPrint { get; set; }
    }
}
