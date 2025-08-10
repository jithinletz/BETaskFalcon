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
    public partial class CostCenterSearchForm : Form
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
        CostSearchButtonCollection button;
       public int PrimaryCostCenter { get; set; }
        public int SubCostCenter { get; set; }

        public string SearchValue { get; set; }

        public CostCenterSearchForm()
        {
            InitializeComponent();
           
        }
        private void ButtonActive(Enum activeEvent)
        {

            switch (activeEvent)
            {

                case EnumFormEvents.Close:
                   
                        this.DialogResult = DialogResult.OK;
                        this.BeginInvoke(new MethodInvoker(Close));
                    
                    break;
              
                default:
                    break;

            }
        }
        private void ButtonEvents(object sender, EventArgs e)
        {
            
             if (sender == button.BtnClose)
            {
                PrimaryCostCenter = General.GetComboBoxSelectedValue(cmbPrimaryCostCenter);
                SubCostCenter = General.GetComboBoxSelectedValue(cmbCostCenter);
                SearchValue = $"{cmbPrimaryCostCenter.Text} - {cmbCostCenter.Text}";
                this.DialogResult = DialogResult.OK;
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
            button = new CostSearchButtonCollection
            {
                BtnClose = btnClose,
               
            };
            LoadPrimaryCostCenter();
            LoadSubCostCenter(-1);
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

    }
    class CostSearchButtonCollection
    {
        public Button BtnNew { get; set; }
        public Button BtnCancel { get; set; }
        public Button BtnClose { get; set; }
        public Button BtnSave { get; set; }
        public Button BtnSearch { get; set; }
        public Button BtnPrint { get; set; }
    }
}
