using BETask.Common;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BETask.BAL;
using System.Linq;

namespace BETask.Views
{
    public partial class TrialBalanceForm : Form
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
        TrialBalanceButtonCollection button;
        ProfitandLossBAL profitandLoss = new ProfitandLossBAL();
        public TrialBalanceForm()
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
                    General.ClearGrid(gridTrial);

                    break;
                case EnumFormEvents.Close:
                    this.BeginInvoke(new MethodInvoker(Close));
                    break;
                case EnumFormEvents.Search:
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
                General.ClearGrid(gridTrial);
                List<DAL.Model.TrailBalanceModel> listTrial = new List<DAL.Model.TrailBalanceModel>();
                if (rdbSummary.Checked)
                    listTrial = profitandLoss.TrialBalance(General.ConvertDateServerFormat(dtpFrom.Value), General.ConvertDateServerFormat(dtpTo.Value),chkOpening.Checked);
                else
                    listTrial = profitandLoss.TrialBalanceDetailed(General.ConvertDateServerFormat(dtpFrom.Value), General.ConvertDateServerFormat(dtpTo.Value));
                if (listTrial != null && listTrial.Count > 0)
                {
                    decimal netDebit = 0, netCredit = 0;
                    foreach (DAL.Model.TrailBalanceModel tl in listTrial)
                    {

                        decimal debit = tl.Debit , credit = tl.Credit;
                        //decimal bal = tl.Debit - tl.Credit;
                        //decimal debit = bal > 0 ? bal : 0;
                        //decimal credit = bal <0 ? bal*-1 : 0;

                        //if (tl.Description == "Closing Stock")
                        //{
                        //    debit = tl.Debit;
                        //    credit = 0;
                        //}

                        netDebit += debit;
                        netCredit += credit;

                        gridTrial.Rows.Add(tl.Description, debit!=0?debit.ToString():"", credit!=0?credit.ToString():"");
                    }
                    // gridTrial.Rows.Add("", netDebit, netCredit);
                    gridTrial.Rows.Add("", listTrial.Sum(x=>x.Debit), listTrial.Sum(x => x.Credit));
                    General.GridBackcolorYellow(gridTrial);
                    General.GridRownumber(gridTrial);
                }
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
                if (rdbSummary.Checked)
                    profitandLoss.PrintTrailBalanceSummary(General.ConvertDateServerFormat(dtpFrom.Value), General.ConvertDateServerFormat(dtpTo.Value));
                else
                    profitandLoss.PrintTrailBalance(General.ConvertDateServerFormat(dtpFrom.Value), General.ConvertDateServerFormat(dtpTo.Value));
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
        private void FormLoad()
        {
            DateTime dt = new DateTime(DateTime.Today.Year-1,1,1);
            dtpFrom.Value = dt;
            button = new TrialBalanceButtonCollection
            {
                BtnCancel = btnCancel,
                BtnClose = btnClose,
                BtnSearch = btnSearch,
                BtnPrint = btnPrint
            };
        }

        private void TrialBalanceForm_Load(object sender, EventArgs e)
        {
            FormLoad();
        }
    }
    class TrialBalanceButtonCollection
{
    public Button BtnCancel { get; set; }
    public Button BtnClose { get; set; }
    public Button BtnSearch { get; set; }
    public Button BtnPrint { get; set; }
}
}
