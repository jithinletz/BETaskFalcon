using BETask.Common;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BETask.BAL;
using System.Linq;
using System.Data;

namespace BETask.Views
{
    public partial class BalanceSheetForm : Form
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
        BalanceSheetButtonCollection button;
        public BalanceSheetForm()
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
                    General.ClearTextBoxes(this);
                    General.ClearGrid(gridBalance);

                    break;
                case EnumFormEvents.Close:
                    this.BeginInvoke(new MethodInvoker(Close));
                    break;
                case EnumFormEvents.Search:
                    //Search();
                    Search();
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
                General.ClearGrid(gridBalance);
                BalanceSheetBAL balanceSheetBAL = new BalanceSheetBAL();

                decimal currentProfit = 0;
                ProfitandLossForm profitandLossForm = new ProfitandLossForm(dtpFrom.Value, dtpTo.Value);
                currentProfit = profitandLossForm.profitloss;

                List<DAL.Model.BalanceSheetModel> listBalanceSheet = balanceSheetBAL.BalanceSheet(General.ConvertDateServerFormat(dtpFrom.Value), General.ConvertDateServerFormat(dtpTo.Value), currentProfit);
                if (listBalanceSheet != null && listBalanceSheet.Count > 0)
                {
                    gridBalance.Rows.Add("Assets","", "Liability", "");
                    foreach (DAL.Model.BalanceSheetModel bal in listBalanceSheet)
                    {
                        //if (bal.AssetAmount != 0)
                        if(bal.Asset!=null)
                        {
                            gridBalance.Rows.Add(bal.Asset, bal.AssetAmount == 0 ? "" : bal.AssetAmount.ToString(), "", "");
                            if (bal.Asset.EndsWith("."))
                            {
                                gridBalance.Rows[gridBalance.Rows.Count - 1].Cells["clmDesc1"].Style.ForeColor = System.Drawing.Color.Blue;
                                gridBalance.Rows[gridBalance.Rows.Count - 1].Cells["clmAmt1"].Style.ForeColor = System.Drawing.Color.Blue;
                             
                            }
                            if (bal.Asset.Contains("Total Assets"))
                            {
                                gridBalance.Rows[gridBalance.Rows.Count - 1].Cells["clmDesc1"].Style.BackColor = System.Drawing.Color.Red;
                                gridBalance.Rows[gridBalance.Rows.Count - 1].Cells["clmAmt1"].Style.BackColor = System.Drawing.Color.Red;
                                gridBalance.Rows[gridBalance.Rows.Count - 1].Cells["clmDesc1"].Style.ForeColor = System.Drawing.Color.White;
                                gridBalance.Rows[gridBalance.Rows.Count - 1].Cells["clmAmt1"].Style.ForeColor = System.Drawing.Color.White;

                            }
                        }
                    }

                    //Liability
                    int i = 1;
                    foreach (DAL.Model.BalanceSheetModel bal in listBalanceSheet)
                    {
                    
                        //if (bal.LiabliltyAmount != 0)
                        if (bal.Liability != null)
                        {
                          
                            if (i > gridBalance.Rows.Count)
                            {
                                gridBalance.Rows.Add("","", "", "");
                            }
                            if (!bal.Liability.Contains("Total Liabilities"))
                            {
                                gridBalance["clmDesc2", i].Value = bal.Liability;
                                gridBalance["clmAmt2", i].Value = bal.LiabliltyAmount;
                            }
                            else
                            {
                               // if (i == gridBalance.Rows.Count)
                                {
                                    gridBalance["clmDesc2", gridBalance.Rows.Count-1].Value = bal.Liability;
                                    gridBalance["clmAmt2", gridBalance.Rows.Count-1].Value = bal.LiabliltyAmount;
                                }
                            }
                            
                            if (bal.Liability.EndsWith("."))
                            {
                                gridBalance.Rows[i].Cells["clmDesc2"].Style.ForeColor = System.Drawing.Color.Blue;
                                gridBalance.Rows[i].Cells["clmAmt2"].Style.ForeColor = System.Drawing.Color.Blue;
                            }
                            if (bal.Liability.Contains("Total Liabilities"))
                            {
                                gridBalance.Rows[gridBalance.Rows.Count - 1].Cells["clmDesc2"].Style.BackColor = System.Drawing.Color.Red;
                                gridBalance.Rows[gridBalance.Rows.Count - 1].Cells["clmAmt2"].Style.BackColor = System.Drawing.Color.Red;
                                gridBalance.Rows[gridBalance.Rows.Count - 1].Cells["clmDesc2"].Style.ForeColor = System.Drawing.Color.White;
                                gridBalance.Rows[gridBalance.Rows.Count - 1].Cells["clmAmt2"].Style.ForeColor = System.Drawing.Color.White;
                            }
                            i++;
                          
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                General.Error(ex.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ex.Message);
            }
        }
        private void Print()
        {
            try
            {
                BalanceSheetBAL balanceSheetBAL = new BalanceSheetBAL();
                if (gridBalance.Rows.Count > 0)
                    balanceSheetBAL.Print(General.ConvertDateServerFormat(dtpFrom.Value), General.ConvertDateServerFormat(dtpTo.Value), gridBalance);
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }

        private void BalanceSheetForm_Load(object sender, EventArgs e)
        {
            button = new BalanceSheetButtonCollection
            {
                BtnCancel = btnCancel,
                BtnClose = btnClose,
                BtnSearch = btnSearch,
                BtnPrint = btnPrint
            };
            CompanyBAL companyBal = new CompanyBAL();
            Model.CompanyModel company = companyBal.GetCompanyDetails();
            dtpFrom.Value = company.FinancialDateFrom;
            dtpTo.Value = company.FinancialDateTo;
        }
    }
    class BalanceSheetButtonCollection
    {
        public Button BtnCancel { get; set; }
        public Button BtnClose { get; set; }
        public Button BtnSearch { get; set; }
        public Button BtnPrint { get; set; }
    }
}
