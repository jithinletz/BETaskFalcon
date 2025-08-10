using BETask.Common;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BETask.BAL;
using System.Linq;

namespace BETask.Views
{
    public partial class TrialBalanceDatewiseForm : Form
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
        TrialBalanceDatewiseButtonCollection button;
        ProfitandLossBAL profitandLoss = new ProfitandLossBAL();
        public TrialBalanceDatewiseForm()
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
                Application.DoEvents();
                List<DAL.Model.TrailBalanceDatewiseModel> listTrial = new List<DAL.Model.TrailBalanceDatewiseModel>();
                if (rdbSummary.Checked)
                    listTrial = profitandLoss.TrialBalanceDatewise(General.ConvertDateServerFormat(dtpFrom.Value), General.ConvertDateServerFormat(dtpTo.Value),chkOpening.Checked);
                else
                    listTrial = profitandLoss.TrialBalanceDetailedDatewise(General.ConvertDateServerFormat(dtpFrom.Value), General.ConvertDateServerFormat(dtpTo.Value),true,chkExcludeCustomer.Checked);
                if (listTrial != null && listTrial.Count > 0)
                {
                    foreach (DAL.Model.TrailBalanceDatewiseModel tl in listTrial)
                    {

                        gridTrial.Rows.Add(tl.Description, tl.OpeningDebit != 0 ? tl.OpeningDebit.ToString() : "", tl.OpeningCredit != 0 ? tl.OpeningCredit.ToString() : "", tl.Debit!=0? tl.Debit.ToString():"", tl.Credit!=0? tl.Credit.ToString():"", tl.ClosingDebit != 0 ? tl.ClosingDebit.ToString() : "", tl.ClosingCredit != 0 ? tl.ClosingCredit.ToString() : "");
                    }
                    // gridTrial.Rows.Add("", netDebit, netCredit);
                    gridTrial.Rows.Add("", listTrial.Sum(x=>x.OpeningDebit), listTrial.Sum(x => x.OpeningCredit), listTrial.Sum(x=>x.Debit), listTrial.Sum(x => x.Credit), listTrial.Sum(x => x.ClosingDebit), listTrial.Sum(x => x.ClosingCredit));
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
                    profitandLoss.PrintTrailBalanceSummaryDatewise(General.ConvertDateServerFormat(dtpFrom.Value), General.ConvertDateServerFormat(dtpTo.Value));
                else
                    profitandLoss.PrintTrailBalanceDatewise(General.ConvertDateServerFormat(dtpFrom.Value), General.ConvertDateServerFormat(dtpTo.Value),chkExcludeCustomer.Checked);
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
            button = new TrialBalanceDatewiseButtonCollection
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
    class TrialBalanceDatewiseButtonCollection
    {
    public Button BtnCancel { get; set; }
    public Button BtnClose { get; set; }
    public Button BtnSearch { get; set; }
    public Button BtnPrint { get; set; }
}
}
