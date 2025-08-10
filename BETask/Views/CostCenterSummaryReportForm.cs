using BETask.Common;
using BETask.BAL;
using BETask.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using EDMX = BETask.DAL.EDMX;
using Rep = BETask.DAL.DAL;
using System.Data;
using System.Drawing;

namespace BETask.Views
{
    public partial class CostCenterSummaryReportForm : Form
    {
        public enum EnumFormEvents
        {
            FormLoad,
            Cancel,
            Close,
            Search,
            Print,

        }
        CostCenterSummaryButtonCollection button;
        Rep.CostCenterDAL costCenter = new Rep.CostCenterDAL();

        public CostCenterSummaryReportForm()
        {
            InitializeComponent();
        }
        private void ButtonActive(Enum activeEvent)
        {

            switch (activeEvent)
            {
                case EnumFormEvents.FormLoad:
                    //Search();
                    break;
                case EnumFormEvents.Cancel:
                    ButtonActive(EnumFormEvents.FormLoad);
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
            if (sender == button.BtnSearch)
            {
                ButtonActive(EnumFormEvents.Search);
            }

            else if (sender == button.BtnClose)
            {
                ButtonActive(EnumFormEvents.Close);
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
                General.ClearGrid(dgItems);
                if (rdbSummary.Checked)
                {
                    List<EDMX.SP_CostCenterSummary_Result> listSummary = costCenter.GetCostCenterSummary(General.ConvertDateServerFormat(dtpDateFrom.Value), General.ConvertDateServerFormat(dtpDateTo.Value));
                    if (listSummary != null && listSummary.Count > 0)
                    {
                        foreach (EDMX.SP_CostCenterSummary_Result rs in listSummary)
                        {
                            dgItems.Rows.Add(rs.parentId, rs.parent, rs.Debit, rs.Credit, rs.Balance);
                        }
                        dgItems.Rows.Add("", "", listSummary.Sum(x => x.Debit), listSummary.Sum(x => x.Credit), listSummary.Sum(x => x.Balance));
                        General.GridBackcolorYellow(dgItems);

                    }
                }
                else
                {
                    int ledgerId = 0;

                    if (cmbLedgerAccount.SelectedValue != null)
                        ledgerId = int.Parse(cmbLedgerAccount.SelectedValue.ToString());
                    
                    DataTable tblData = costCenter.GetCostCenterSummaryGrouped(General.ConvertDateServerFormat(dtpDateFrom.Value), General.ConvertDateServerFormat(dtpDateTo.Value), 0, ledgerId);
                    if (tblData != null)
                    {
                        foreach (DataRow dr in tblData.Rows)
                        {
                            dgItems.Rows.Add(dr["GroupId"], dr["CostCenter"], dr["Debit"], dr["Credit"], dr["Balance"]);
                        }
                        decimal totalDebit = tblData.AsEnumerable()
                               .Sum(row => row.Field<decimal>("Debit"));
                        decimal totalCredit = tblData.AsEnumerable()
                              .Sum(row => row.Field<decimal>("Credit"));
                        decimal totalBalance = tblData.AsEnumerable()
                              .Sum(row => row.Field<decimal>("Balance"));
                        dgItems.Rows.Add("", "", totalDebit, totalCredit, totalBalance);
                        General.GridBackcolorYellow(dgItems);

                    }
                }
            }
            catch (Exception ex)
            {
                General.Error(ex.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ex.Message);
            }
        }
        private void LoadAllLedger()
        {
            BAL.AccountLedgerBAL accountLedgerBAL = new BAL.AccountLedgerBAL();

            try
            {

                //cmbLedgerAccount.Items.Clear();


                int groupId = 0;

                DAL.DAL.AccountLedgerDAL accountLedgerDAL = new DAL.DAL.AccountLedgerDAL();
                List<EDMX.account_ledger> listCustomer = accountLedgerDAL.GetAllAccountLedgerNonCustomer(groupId).Where(x=>x.enable_cost_center==1).ToList();


                cmbLedgerAccount.DataSource = listCustomer;
                cmbLedgerAccount.DisplayMember = "ledger_name";
                cmbLedgerAccount.ValueMember = "ledger_id";

                cmbLedgerAccount.SelectedIndex = -1;

            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error);
            }
        }
        private void Print()
        {
            try
            {
                List<EDMX.SP_CostCenterDetailed_Result> listCostCenter = costCenter.GetCostCenterDetailed(General.ConvertDateServerFormat(dtpDateFrom.Value), General.ConvertDateServerFormat(dtpDateTo.Value));
                if (listCostCenter != null && listCostCenter.Count > 0)
                {
                    DataTable tblData = General.ToDataTable(listCostCenter);
                    if (tblData != null && tblData.Rows.Count > 0)
                    {

                        BETask.Report.ReportForm reportForm = new Report.ReportForm(BETask.Report.ReportForm.EnumReportType.CostCenterSummary, $"Cost center summary date between {General.ConvertDateAppFormat(dtpDateFrom.Value)} and {General.ConvertDateAppFormat(dtpDateTo.Value)}", tblData);
                        reportForm.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                General.Error(ex.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ex.Message);
            }
        }
        private void FormLoad()
        {
            button = new CostCenterSummaryButtonCollection
            {
                BtnClose = btnClose,
                BtnPrint = btnPrint,
                BtnSearch = btnSearch
            };

            CompanyBAL companyBAL = new CompanyBAL();
            Model.CompanyModel company = companyBAL.GetCompanyDetails();
            if (company != null)
            {
                dtpDateFrom.Value = company.FinancialDateFrom;
                dtpDateTo.Value = company.FinancialDateTo;
            }
        }

        private void CostCenterSummaryReportForm_Load(object sender, EventArgs e)
        {
            FormLoad();
            Search();
        }

        private void rdbGroupedSummary_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbGroupedSummary.Checked)
            {
               
                LoadAllLedger();
                lblAccount.Show();
                cmbLedgerAccount.Show();
            }
        }

        private void rdbSummary_CheckedChanged(object sender, EventArgs e)
        {
            lblAccount.Hide();
            cmbLedgerAccount.Hide();
        }
    }
    class CostCenterSummaryButtonCollection
    {
        public Button BtnSearch { get; set; }
        public Button BtnCancel { get; set; }
        public Button BtnClose { get; set; }
        public Button BtnPrint { get; set; }


    }
}
