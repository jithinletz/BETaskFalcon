using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BETask.Common;
using BETask.BAL;
using BETask.Model;
using EDMX= BETask.DAL.EDMX;

namespace BETask.Views
{
    public partial class CostCenterForm : Form
    {
        bool isUpdate = false;
        public enum EnumFormEvents
        {
            FormLoad,
            New,
            Save,
            Update,
            Cancel,
            Close,
            NodeClick
        }
        public enum EnumWorkMode
        {
            Group,
            Ledger
        }
        CostCenterButtonCollection button;

        public CostCenterForm()
        {
            InitializeComponent();
        }
        private void CostCenterForm_Load(object sender, EventArgs e)
        {
            button = new CostCenterButtonCollection
            {
                BtnNew = btnNew,
                BtnCancel = btnCancel,
                BtnClose = btnClose,
                BtnSave = btnSave
            };
            ButtonActive(EnumFormEvents.FormLoad);
            txtParentCostCenterName.Text = "Primary";
            txtParentCostCenterId.Text = "0";
            LoadAllAccounts();
        }

        #region CustomFunction
        /// <summary>
        /// All Button action Controled from here
        /// </summary>
        /// <param name="changeButtonActiveEvents"></param>
        private void ButtonActive(Enum activeEvent)
        {

            switch (activeEvent)
            {
                case EnumFormEvents.FormLoad:
                    button.BtnNew.Enabled = true;
                    button.BtnSave.Enabled = false;
                    button.BtnClose.Enabled = true;
                    button.BtnCancel.Enabled = false;
                    button.BtnNew.Text = "&New";
                    isUpdate = false;
                    chkActive.Enabled = false;
                    break;

                case EnumFormEvents.New:
                    button.BtnNew.Enabled = false;
                    button.BtnSave.Enabled = true;
                    button.BtnClose.Enabled = true;
                    button.BtnCancel.Enabled = true;
                    pnlSaveConent.Enabled = true;
                    txtCostCenterName.Focus();
                    if (btnNew.Text == "&Edit")
                    {
                        chkActive.Enabled = true;
                        isUpdate = true;
                    }
                    int level = 0;
                    CheckLevel(out level);
                    if (level > 1)
                    {

                        lnkChangeParent.Show();
                        lnkChangeParent.Text = $"Change parent of (  {txtParentCostCenterName.Text} ) ";
                    }
                    break;

                case EnumFormEvents.Cancel:
                    ButtonActive(EnumFormEvents.FormLoad);
                    pnlSaveConent.Enabled = false;
                    lnkChangeParent.Hide();
                    General.ClearTextBoxes(this);
                    break;

                case EnumFormEvents.Close:
                   this.BeginInvoke(new MethodInvoker(Close));
                    break;
                case EnumFormEvents.Update:
                    button.BtnNew.Enabled = true;
                    button.BtnNew.Text = "&Edit";
                    button.BtnSave.Enabled = false;
                    button.BtnClose.Enabled = true;
                    button.BtnCancel.Enabled = true;
                    
                    break;

                case EnumFormEvents.Save:
                    SaveCostCenter();
                    break;

                case EnumFormEvents.NodeClick:
                    button.BtnCancel.Enabled = true;
                    txtCostCenterId.Clear();
                    txtCostCenterName.Clear();
                    txtDescription.Clear();
                    break;
                default:
                    break;

            }
        }
        /// <summary>
        /// Button Event Operations
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonEvents(object sender, EventArgs e)
        {
            if (sender == button.BtnNew)
            {
                ButtonActive(EnumFormEvents.New);
            }
            else if (sender == button.BtnCancel)
            {
                ButtonActive(EnumFormEvents.Cancel);
            }
            else if (sender == button.BtnSave)
            {
                ButtonActive(EnumFormEvents.Save);
            }
            else if (sender == button.BtnClose)
            {
                ButtonActive(EnumFormEvents.Close);
            }
        }

        /// <summary>
        /// Validation
        /// </summary>
        /// <param name="workMode"></param>
        private bool Validation(Enum workMode)
        {
            bool response = false;
            if (!rdbPrimary.Checked && !rdbSub.Checked)
            { response = false; General.ShowMessage(General.EnumMessageTypes.Warning, "Please select cost center type");return response; }
            else response = true;
            
            
            int level = 0;
            switch (workMode)
            {
                
                case EnumWorkMode.Group:
                    if (rdbSub.Checked && General.IsTextboxEmpty(txtParentCostCenterId)) { response = false; General.ShowMessage(General.EnumMessageTypes.Warning, "Please select primary category"); } else response = true;
                    if (General.IsTextboxEmpty(txtCostCenterName)) { response = false; General.ShowMessage(General.EnumMessageTypes.Warning, "Please enter cost center name"); } else response = true;
                   
                    if(!CheckLevel(out level)) { response = false; General.ShowMessage(General.EnumMessageTypes.Warning, "Level not support"); } else response = true;
                    break;
                default:
                    break;
            }
            return response;
        }

        #endregion CustomFunction


        /// <summary>
        /// Heare ALL Save Update Load Works
        /// </summary>
        #region SaveUpdateLoad

        private void SaveCostCenter()
        {
            try
            {
                if (General.ShowMessageConfirm() == DialogResult.Yes)
                {
                    if (rdbPrimary.Checked || Validation(EnumWorkMode.Group))
                    {

                        CostCenterBAL accountLedgerBAL = new CostCenterBAL();

                        DAL.EDMX.cost_center cost = new DAL.EDMX.cost_center
                        {
                            cost_center_id = General.IsTextboxEmpty(txtCostCenterId) ? 0 : Convert.ToInt32(txtCostCenterId.Text),
                            cost_center_name=txtCostCenterName.Text,
                            parent_id= General.IsTextboxEmpty(txtParentCostCenterId) ? 0 : Convert.ToInt32(txtParentCostCenterId.Text),
                            status =chkActive.Checked?1:2

                        };

                       
                        accountLedgerBAL.SaveCostCenter(cost);
                        General.Action($"Cost center Saved {txtCostCenterName.Text} under  {txtParentCostCenterName.Text}");
                        General.ShowMessage(General.EnumMessageTypes.Success, "Cost Center Successfully Saved ");
                        ButtonActive(EnumFormEvents.Cancel);
                        LoadAllAccounts();
                    }
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                if (ee.ToString().Contains("Violation of UNIQUE KEY "))
                {
                    General.ShowMessage(General.EnumMessageTypes.Error, "Provided ledger name already exist");
                }
                else
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
        private bool CheckLevel(out int _level)
        {
            bool result = false;
            _level = 0;
            try
            {
                DAL.DAL.CostCenterDAL groupDAL = new DAL.DAL.CostCenterDAL();
                int parentId = General.ParseInt(txtParentCostCenterId.Text);
                if (parentId > 0)
                {
                    result = true;
                    int level = groupDAL.GetGroupLevel(parentId);
                    _level = level;
                    if (level>1)
                        result = false;
                }

            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        private void LoadAllAccounts()
        {
            CostCenterBAL costCenterBAL = new CostCenterBAL();
            try
            {
                trvContent.Nodes.Clear();
                List<EDMX.cost_center> listAccount = costCenterBAL.GetAllCostCenter(-1);

                List<EDMX.cost_center> listAccountMain = listAccount.Where(x => x.parent_id == 0).ToList();
                foreach (EDMX.cost_center account in listAccountMain)
                {
                    TreeNode tNode;
                    tNode = trvContent.Nodes.Add(account.cost_center_name);
                    tNode.Tag = account.cost_center_id;
                    List<EDMX.cost_center> listAccountSub = listAccount.Where(x => x.parent_id == account.cost_center_id).ToList();
                    if (listAccountSub.Count > 0)
                        LoadAllAccountSub(listAccount, account.cost_center_id);
                }
            }
            catch (Exception ee)
            {
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }

        private List<AccountLedgerModel> LoadAllLedger(int groupId=-1)
        {
            AccountLedgerBAL accounLedgerBAL = new AccountLedgerBAL();
            List<AccountLedgerModel> listLedger=new List<AccountLedgerModel>();
            try
            {
               
                listLedger = accounLedgerBAL.GetAllAccountLedger(groupId);

              
            }
            catch (Exception ee)
            {
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
            return listLedger;
        }



        /// <summary>
        /// Fill Sub Account
        /// </summary>
        private void LoadAllAccountSub(List<EDMX.cost_center> listAccount, int groupId, int nodeLevel = 1)
        {
            try
            {
                if (groupId == 3)
                {
                    string ss = "";
                }
                List<EDMX.cost_center> listAccountSub = listAccount.Where(x => x.parent_id == groupId).ToList();
                //  for (int i = 0; i < nodeLevel; i++)
                {
                    int l2 = 0;
                    foreach (EDMX.cost_center account in listAccountSub)
                    {
                        //Level 2 
                        TreeNode tNode;
                        tNode = trvContent.Nodes[trvContent.Nodes.Count - 1].Nodes.Add(account.cost_center_name);
                        tNode.Tag = account.cost_center_id;

                        List<EDMX.cost_center> listAccountSub2 = listAccount.Where(x => x.parent_id == account.cost_center_id).ToList();

                        if (listAccountSub2.Count > 0)
                        {
                            int l3 = 0;
                            foreach (EDMX.cost_center account1 in listAccountSub2)
                            {
                                TreeNode tNode1;
                                tNode1 = trvContent.Nodes[trvContent.Nodes.Count - 1].Nodes[l2].Nodes.Add(account1.cost_center_name);
                                //  tNode1 = trvContent.Nodes[trvContent.Nodes.Count - 1].Nodes[trvContent.Nodes[trvContent.Nodes.Count - 1].Nodes.Count-1].Nodes.Add(account1.Group_name);
                                tNode1.Tag = account1.cost_center_id;

                                List<EDMX.cost_center> listAccountSub3 = listAccount.Where(x => x.parent_id == account1.cost_center_id).ToList();

                                if (listAccountSub3.Count > 0)
                                {
                                    int i = 0;
                                    foreach (EDMX.cost_center account2 in listAccountSub3)
                                    {
                                        if (account2.cost_center_name == "DECAL COST")
                                        {
                                            string SS = "";
                                        }
                                        TreeNode tNode2;
                                        tNode2 = trvContent.Nodes[trvContent.Nodes.Count - 1].Nodes[l2].Nodes[l3].Nodes.Add(account2.cost_center_name);
                                        // tNode2 = trvContent.Nodes[trvContent.Nodes.Count - 1].Nodes[trvContent.Nodes[trvContent.Nodes.Count - 1].Nodes.Count - 1].Nodes[i].Nodes.Add(account2.Group_name);
                                        tNode2.Tag = account2.cost_center_name;
                                        i++;

                                    }
                                    l3++;
                                }
                                else
                                    l3++;

                            }
                            l2++;
                        }



                    }


                }
            }
            catch (Exception ee)
            {
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }

        private void ShowAllLedgers()
        {
            try
            {
                General.ClearGrid(gridReport);
                AccountLedgerBAL accountLedgerBAL = new AccountLedgerBAL();
                List<AccountLedgerModel> listLedger = accountLedgerBAL.GetAllAccountLedger(-1);
                foreach (AccountLedgerModel ledger in listLedger)
                {
                    gridReport.Rows.Add(ledger.Ledger_name,ledger.GroupName);
                }
            }
            catch (Exception ee)
            {
                
                throw;
            }
        }

        #endregion SaveUpdate

        #region NextFocus
        private void NextFocus(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (sender == txtDescription)
                    General.NextFocus(sender, e, btnSave);
                else
                    General.NextFocus(sender, e);

            }
        }

        #endregion NextFocus

        #region Treeview
        private void trvContent_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                CostCenterBAL costCenterBAL = new CostCenterBAL();
                if (e.Node.Level==1)
                {
                    rdbSub.Checked = true;
                    string parentName = string.Empty;
                    var costCenter = costCenterBAL.GetCostCenter(Convert.ToInt32(e.Node.Tag),out parentName);
                    txtCostCenterId.Text = costCenter.cost_center_id.ToString();
                    txtCostCenterName.Text = costCenter.cost_center_name;
                    txtDescription.Text = costCenter.cost_center_name;
                    txtParentCostCenterId.Text = costCenter.parent_id.ToString();
                    txtParentCostCenterName.Text = parentName;
                    chkActive.Enabled = true;
                    ButtonActive(EnumFormEvents.Update);
                }
                else
                {
                    txtParentCostCenterId.Text = e.Node.Tag.ToString();
                    txtParentCostCenterName.Text = e.Node.Text;
                    rdbPrimary.Checked = true;
                    ButtonActive(EnumFormEvents.NodeClick);
                }

            }
            catch (Exception ee)
            {
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
        #endregion Treeview

        private void linkReport_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                pnlReport.Show();
                ShowAllLedgers();
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error);

            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            pnlReport.Hide();
        }

        private void rdbPrimary_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbPrimary.Checked)
            {
                txtParentCostCenterName.Text = "Primary";
                txtParentCostCenterId.Text = "0";
            }
        }

        private void rdbSub_CheckedChanged(object sender, EventArgs e)
        {
           // txtParentCostCenterName.Clear();
           // txtParentCostCenterId.Clear();
        }

        private void lnkChangeParent_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LoadChangeParent();
        }
        private void LoadChangeParent()
        {
            txtCostCenterName.Text = txtParentCostCenterName.Text;
            txtCostCenterId.Text = txtParentCostCenterId.Text;
            txtParentCostCenterId.Clear();
            txtParentCostCenterName.Clear();
        }
    }


    /// <summary>
    /// All Buttons should assigned to below class
    /// </summary>
    class CostCenterButtonCollection
    {
        public Button BtnNew { get; set; }
        public Button BtnCancel { get; set; }
        public Button BtnClose { get; set; }
        public Button BtnSave { get; set; }
    }
}
