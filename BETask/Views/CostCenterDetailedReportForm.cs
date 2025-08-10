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
    public partial class CostCenterDetailedReportForm : Form
    {
        public enum EnumFormEvents
        {
            FormLoad,
            Cancel,
            Close,
            Search,
            Print,
           
        }
        DAL.DAL.CostCenterDAL costCenterDAL = new DAL.DAL.CostCenterDAL();
        CostCenteDetailedButtonCollection button;
        Rep.CostCenterDAL costCenter = new Rep.CostCenterDAL();
        private bool isFormLoaded=false;

        public CostCenterDetailedReportForm()
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

        private void LoadPrimaryCostCenter()
        {
            try
            {
                cmbPrimaryCostCenter.Items.Clear();
                List<DAL.EDMX.cost_center> listCostCenter = costCenterDAL.GetPrimaryCostCenter();
                foreach (DAL.EDMX.cost_center cost in listCostCenter)
                {
                    ComboboxItem _cmbItem = new ComboboxItem()
                    {
                        Text = cost.cost_center_name,
                        Value = cost.cost_center_id
                    };
                    cmbPrimaryCostCenter.Items.Add(_cmbItem);
                }

            }
            catch (Exception ex)
            {
                General.Error(ex.ToString());
            }
        }
        private void LoadSubCostCenter(int primaryCost)
        {
            try
            {
                cmbCostCenter.Items.Clear();
                List<DAL.EDMX.cost_center> listCostCenter = costCenterDAL.GetAllSubCostCenter(primaryCost);
                foreach (DAL.EDMX.cost_center cost in listCostCenter)
                {
                    ComboboxItem _cmbItem = new ComboboxItem()
                    {
                        Text = cost.cost_center_name,
                        Value = cost.cost_center_id
                    };
                    cmbCostCenter.Items.Add(_cmbItem);
                }

            }
            catch (Exception ex)
            {
                General.Error(ex.ToString());
            }
        }

        private void cmbPrimaryCostCenter_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                int costCenterId = -1;
                if (!String.IsNullOrEmpty(cmbPrimaryCostCenter.Text) && cmbPrimaryCostCenter.SelectedItem != null)
                {
                    Object selectedRoute = cmbPrimaryCostCenter.SelectedItem;
                    costCenterId = (int)((BETask.Views.ComboboxItem)selectedRoute).Value;
                }
                LoadSubCostCenter(costCenterId);
            }
            catch (Exception ex)
            {
                General.Error(ex.Message);
            }
        }
        private void Search()
        {
            try
            {
                lblTotal.Text = "0.00";
                int primaryId = General.GetComboBoxSelectedValue(cmbPrimaryCostCenter);
                int costCenterId = General.GetComboBoxSelectedValue(cmbCostCenter);
                int ledgerId = GetLedgerId();
                General.ClearGrid(dgItems);
                List<EDMX.SP_CostCenterDatewiseDetailed_Result> listTran = costCenter.GetCostCenterDatewiseDetailed(General.ConvertDateServerFormat(dtpDateFrom.Value), General.ConvertDateServerFormat(dtpDateTo.Value), primaryId, costCenterId, ledgerId);
                if (listTran != null && listTran.Count > 0)
                {
                    foreach (EDMX.SP_CostCenterDatewiseDetailed_Result rs in listTran)
                    {
                        dgItems.Rows.Add(General.ConvertDateAppFormat(rs.transaction_date), rs.ledger_name, rs.parent, rs.cost_center_name, rs.debit, rs.credit, rs.transaction_type);
                    }
                    dgItems.Rows.Add("", "", "", "", listTran.Sum(x => x.debit), listTran.Sum(x => x.credit));
                    General.GridBackcolorYellow(dgItems);
                    lblTotal.Text = (listTran.Sum(x => x.debit) - listTran.Sum(x => x.credit)).ToString();
                }
            }
            catch (Exception ex)
            {
                General.Error(ex.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ex.Message);
            }
        }

        private int GetLedgerId()
        {
            int ledgerId = 0;
            if (cmbLedgerAccount.SelectedValue != null)
                ledgerId = int.Parse(cmbLedgerAccount.SelectedValue.ToString());
            return ledgerId;
        }

        private void Print()
        {
            try
            {
                int primaryId = General.GetComboBoxSelectedValue(cmbPrimaryCostCenter);
                int costCenterId = General.GetComboBoxSelectedValue(cmbCostCenter);
                int ledgerId = GetLedgerId(); 

                List<EDMX.SP_CostCenterDatewiseDetailed_Result> listCostCenter = costCenter.GetCostCenterDatewiseDetailed(General.ConvertDateServerFormat(dtpDateFrom.Value), General.ConvertDateServerFormat(dtpDateTo.Value),primaryId,costCenterId, ledgerId);
                if (listCostCenter != null && listCostCenter.Count > 0)
                {
                    BETask.Report.DSReports.CostCenterDatewiseDetailedDataTable costCenterDatewiseDetailedDataTable = new Report.DSReports.CostCenterDatewiseDetailedDataTable();
                    DataTable tblData = new DataTable();
                    tblData = costCenterDatewiseDetailedDataTable.Clone();
                    if (listCostCenter != null && listCostCenter.Count > 0)
                    {
                        foreach (EDMX.SP_CostCenterDatewiseDetailed_Result rs in listCostCenter)
                        {
                            DataRow row = tblData.NewRow();
                            row["TransactionDate"] =General.ConvertDateAppFormat( rs.transaction_date);
                            row["LedgerName"] = rs.ledger_name;
                            row["Parent"] = rs.parent;
                            row["CostCenterName"] = rs.cost_center_name;
                            row["TransactionType"] = rs.transaction_type;
                            row["Debit"] = rs.debit;
                            row["Credit"] = rs.credit;
                            row["Narration"] = rs.narration;
                            tblData.Rows.Add(row);
                        }
                        BETask.Report.ReportForm reportForm = new Report.ReportForm(BETask.Report.ReportForm.EnumReportType.CostCenterDetailed, $"Cost center detailed date between {General.ConvertDateAppFormat(dtpDateFrom.Value)} and {General.ConvertDateAppFormat(dtpDateTo.Value)}", tblData);
                        reportForm.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                General.Error(ex.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error,ex.Message);
            }
        }
        private void FormLoad()
        {
            button = new CostCenteDetailedButtonCollection
            {
                BtnClose = btnClose,
                BtnPrint = btnPrint,
                BtnSearch=btnSearch
            };

            CompanyBAL companyBAL = new CompanyBAL();
            Model.CompanyModel company = companyBAL.GetCompanyDetails();
            if (company != null)
            {
                dtpDateFrom.Value = company.FinancialDateFrom;
                dtpDateTo.Value = company.FinancialDateTo;
            }
            LoadPrimaryCostCenter();
            LoadSubCostCenter(-1);
            LoadAccountGroup();
            isFormLoaded = true;
            LoadAllLedger();
        }
        private void LoadAllLedger()
        {
            BAL.AccountLedgerBAL accountLedgerBAL = new BAL.AccountLedgerBAL();

            try
            {


                int groupId = -1;
                if (cmbGroup.SelectedValue != null)
                    groupId = int.Parse(cmbGroup.SelectedValue.ToString());

                groupId = groupId == 0 ? -1 : groupId;

                DAL.DAL.AccountLedgerDAL accountLedgerDAL = new DAL.DAL.AccountLedgerDAL();
                List<EDMX.account_ledger> listCustomer = accountLedgerDAL.GetAllAccountLedgerNonCustomer(groupId);


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
        private void LoadAccountGroup()
        {
            try
            {
                DAL.DAL.AccountGroupDAL accountGroupDAL = new DAL.DAL.AccountGroupDAL();
                List<DAL.EDMX.account_group> listGroup = accountGroupDAL.GetAllAccountGroupHasLedger(General.companyId, General.locationId);
                cmbGroup.DataSource = listGroup;
                cmbGroup.DisplayMember = "group_name";
                cmbGroup.ValueMember = "group_id";
                cmbGroup.SelectedIndex = -1;
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error);
            }
        }
        private void CostCenterSummaryReportForm_Load(object sender, EventArgs e)
        {
            FormLoad();
            //Search();
        }

        private void cmbGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isFormLoaded)
            {

                LoadAllLedger();
            }
        }
    }
    class CostCenteDetailedButtonCollection
    {
        public Button BtnSearch { get; set; }
        public Button BtnCancel { get; set; }
        public Button BtnClose { get; set; }
        public Button BtnPrint { get; set; }


    }
}
