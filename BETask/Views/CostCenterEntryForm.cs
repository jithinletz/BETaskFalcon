using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BETask.Common;
using System.Windows.Forms;
using EDMX = BETask.DAL.EDMX;
using System.Text;

namespace BETask.Views
{
    public partial class CostCenterEntryForm : Form
    {
        public enum EnumFormEvents
        {
            FormLoad,
            New,
            Save,
            Cancel,
            Close,
            Print
        }
        DAL.DAL.CostCenterDAL costCenterDAL = new DAL.DAL.CostCenterDAL();
        BAL.CostCenterBAL costCenterBAL = new BAL.CostCenterBAL();
        CostEntryButtonCollection button;
        public string guid { get; set; }
        public string savedCostCenter = string.Empty;
        public bool validated = false;
        int ledgerId = 0; decimal ledgerAmount = 0;
        bool isDebit = true;

        public CostCenterEntryForm(string _guid, int _ledgerId, string _ledgerName, decimal _amount,bool _debit)
        {
            InitializeComponent();
            this.ledgerId = _ledgerId;
            this.guid = _guid;
            this.ledgerAmount = _amount;
            lblLedgerName.Text = _ledgerName;
            lblLedgerTotal.Text = _amount.ToString();
            this.isDebit = _debit;
            if (string.IsNullOrEmpty(_guid))
                guid = General.GenerateGuid().ToString();
            else
                GetSavedCostTransaction(this.guid, true);
        }
        private void ButtonActive(Enum activeEvent)
        {

            switch (activeEvent)
            {

                case EnumFormEvents.Close:
                    if (CloseValidation())
                    {
                        this.DialogResult = DialogResult.OK;
                        GetSavedCostTransaction(this.guid);
                        this.BeginInvoke(new MethodInvoker(Close));
                    }
                    break;
                case EnumFormEvents.Save:
                    SaveCostCenter();
                    break;
                default:
                    break;

            }
        }
        private void ButtonEvents(object sender, EventArgs e)
        {
            
             if (sender == button.BtnSave)
            {
                ButtonActive(EnumFormEvents.Save);
            }
            else if (sender == button.BtnClose)
            {
                ButtonActive(EnumFormEvents.Close);
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
        private void FormLoad()
        {
            button = new CostEntryButtonCollection
            {
                BtnClose = btnClose,
                BtnSave = btnSave,

            };
            LoadPrimaryCostCenter();
            LoadSubCostCenter(-1);
        }
        private bool Validation()
        {
            bool resp = true;
            int costCenterId =0;
            if (!String.IsNullOrEmpty(cmbCostCenter.Text) && cmbCostCenter.SelectedItem != null)
            {
                Object selectedRoute = cmbCostCenter.SelectedItem;
                costCenterId = (int)((BETask.Views.ComboboxItem)selectedRoute).Value;
                resp = false;
            }
            if (costCenterId == 0)
            {
                General.ShowMessage(General.EnumMessageTypes.Error,"Invalid cost center", "Select Cost Center");
                cmbCostCenter.Focus();
                cmbCostCenter.DroppedDown = true;
                resp = false;
            }
            if (General.IsTextboxEmpty(txtAmount))
            {
                General.ShowMessage(General.EnumMessageTypes.Error, "Invalid Amount");
                txtAmount.Focus();
                resp = false;
            }
            return resp;
        }
        private void Calculation()
        {
            try
            {
                decimal amount = 0;
                foreach (DataGridViewRow dr in dgItems.Rows)
                {
                    amount += General.TruncateDecimalPlaces(Convert.ToDecimal(dr.Cells["clmAmount"].Value));
                }
                lblTotalAmount.Text = amount.ToString();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        private string GetSavedCostTransaction(string guid,bool fillGrid=false)
        {
            string savedCosts = string.Empty;
            try
            {
                General.ClearGrid(dgItems);
                List<EDMX.cost_center_transaction> csList = costCenterDAL.GetSavedTransactionByGUID(guid);
                StringBuilder builder = new StringBuilder();
                if (csList != null && csList.Count > 0)
                {
                    foreach (EDMX.cost_center_transaction cs in csList)
                    {
                        if (fillGrid)
                        {
                            dgItems.Rows.Add(cs.entry_id,cs.cost_center_id, cs.cost_center.cost_center_name, this.isDebit?cs.debit:cs.credit);
                        }
                        string nl = Environment.NewLine;
                        builder.Append($"{cs.cost_center.cost_center_name.PadRight(15)} {(this.isDebit ? cs.debit : cs.credit)}");
                        builder.Append(nl);
                        
                        
                    }
                    savedCosts = builder.ToString();
                    this.savedCostCenter = savedCosts;
                   
                }
                Calculation();
            }
            catch (Exception ex)
            {
            }
            return savedCosts;
        }
        private void SaveCostCenter()
        {
            try
            {
                EDMX.cost_center_transaction cost = GetCostCenter();
                if (cost != null)
                {
                   int entryId= costCenterBAL.SaveCostCenterTransaction(cost);
                    dgItems.Rows.Add(entryId,cost.cost_center_id, cmbCostCenter.Text, txtAmount.Text);
                    cmbCostCenter.Text = "";
                    txtAmount.Clear();
                    Calculation();
                    cmbCostCenter.Focus();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        private EDMX.cost_center_transaction GetCostCenter()
        {
            EDMX.cost_center_transaction cost = null;
            try
            {
                int costCenterId = 0;
                if (!String.IsNullOrEmpty(cmbCostCenter.Text) && cmbCostCenter.SelectedItem != null)
                {
                    Object selectedRoute = cmbCostCenter.SelectedItem;
                    costCenterId = (int)((BETask.Views.ComboboxItem)selectedRoute).Value;
                 
                }
                decimal amount = 0;
                amount=General.ParseDecimal(txtAmount.Text);
                if (costCenterId > 0 && amount>=0)
                {
                    cost = new EDMX.cost_center_transaction
                    {
                        ledger_id = this.ledgerId,
                        reference_id = this.guid.ToString(),
                        cost_center_id = costCenterId,
                        debit = this.isDebit ? amount : 0,
                        credit = !this.isDebit ? amount : 0,
                        status = 3,

                    };
                }
            }
            catch
            {
                throw;
            }
            return cost;
        }

        private bool CloseValidation()
        {
            bool _validated = false;
            try
            {
                decimal ledgerAmount = Convert.ToDecimal(lblLedgerTotal.Text);
                decimal costAmount = Convert.ToDecimal(lblTotalAmount.Text);
                if (costAmount == 0)
                    _validated = true;
                else if (ledgerAmount != costAmount)
                {
                    _validated = false;
                }
                else
                    _validated = true;
            }
            catch (Exception ex)
            {
                General.ShowMessage(General.EnumMessageTypes.Error, "Error in validation");
            }
            return _validated;
        }

        private void CostCenterEntryForm_Load(object sender, EventArgs e)
        {
            FormLoad();
        }

        private void cmbPrimaryCostCenter_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int costCenterId = -1;
                if (!String.IsNullOrEmpty(cmbPrimaryCostCenter.Text) && cmbPrimaryCostCenter.SelectedItem!=null)
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
        private void NextFocus(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                General.NextFocus(sender, e);

            }
        }
        private void ValidateDecimalPercision(object sender, EventArgs e)
        {
            TextBox text = (TextBox)sender;
            General.DecimalValidationText(text);
        }
        private void DecimalOnly(object sender, KeyPressEventArgs e)
        {
            General.TxtOnlyDecimal(sender, e);
        }

        private void CostCenterEntryForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                ButtonActive(EnumFormEvents.Close);
            }
        }

        private void dgItems_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.KeyData == Keys.Delete)
            {
                if (dgItems["clmEntryId", dgItems.CurrentRow.Index].Value!=null)
                {
                    int entryId = Convert.ToInt32(dgItems["clmEntryId", dgItems.CurrentRow.Index].Value);
                    if (entryId > 0)
                    {
                        DeleteByEntryId(entryId);
                        GetSavedCostTransaction(this.guid,true);

                    }
                }
            }
        }
        private void DeleteByEntryId(int entryId)
        {
            try
            {
                costCenterBAL.DeleteTransactionByEntryId(entryId);
            }
            catch(Exception ex)
            {
                General.Error(ex.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, "Unable to delete entry");
            }
        }

      
    }
    class CostEntryButtonCollection
    {
        public Button BtnNew { get; set; }
        public Button BtnCancel { get; set; }
        public Button BtnClose { get; set; }
        public Button BtnSave { get; set; }
        public Button BtnSearch { get; set; }
        public Button BtnPrint { get; set; }
    }
}
