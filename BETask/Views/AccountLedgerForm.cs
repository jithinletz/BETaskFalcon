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

namespace BETask.Views
{
    public partial class AccountLedgerForm : Form
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
        LedgerButtonCollection button;

        public AccountLedgerForm()
        {
            InitializeComponent();
        }
        private void AccountGroupForm_Load(object sender, EventArgs e)
        {
            button = new LedgerButtonCollection
            {
                BtnNew = btnNew,
                BtnCancel = btnCancel,
                BtnClose = btnClose,
                BtnSave = btnSave
            };
            ButtonActive(EnumFormEvents.FormLoad);
            LoadAllAccountsAsync();
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
                    txtLedgerName.Focus();
                    if (btnNew.Text == "&Edit")
                    {
                        chkActive.Enabled = true;
                        isUpdate = true;
                    }
                    break;

                case EnumFormEvents.Cancel:
                    ButtonActive(EnumFormEvents.FormLoad);
                    pnlSaveConent.Enabled = false;
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
                    SaveLedger();
                    break;

                case EnumFormEvents.NodeClick:
                    button.BtnCancel.Enabled = true;
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
            switch (workMode)
            {
                case EnumWorkMode.Group:
                    if (General.IsTextboxEmpty(txtLedgerName)) { response = false; General.ShowMessage(General.EnumMessageTypes.Warning, "Please enter LedgerName"); } else response = true;
                    int level = 0;
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

        private void SaveLedger()
        {
            try
            {
                if (General.ShowMessageConfirm() == DialogResult.Yes)
                {
                    if (Validation(EnumWorkMode.Group))
                    {

                        AccountLedgerBAL accountLedgerBAL = new AccountLedgerBAL();
                        AccountLedgerModel accountLedgerModel = new AccountLedgerModel
                        {
                            Ledger_name = txtLedgerName.Text.Trim(),
                            Description = txtDescription.Text.Trim(),
                            Group_id = General.IsTextboxEmpty(txtGroupId) ? 0 : Convert.ToInt32(txtGroupId.Text),
                            Ledger_id = isUpdate ? Convert.ToInt32(txtLedgerId.Text) : 0,
                            Status = (chkActive.Checked ? 1 : 2),
                            EnableCostCnetr= (chkCostCenter.Checked ? 1 : 2),
                        };
                        accountLedgerBAL.SaveAccountLedger(accountLedgerModel);
                        General.Action($"Account Ledger Saved {txtLedgerName.Text} under  {txtGroupName.Text}");
                        General.ShowMessage(General.EnumMessageTypes.Success, "Account Ledger Successfully Saved ");
                        ButtonActive(EnumFormEvents.Cancel);
                        LoadAllAccountsAsync();
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
                DAL.DAL.AccountGroupDAL groupDAL = new DAL.DAL.AccountGroupDAL();
                int parentId = General.ParseInt(txtGroupId.Text);
                if (parentId > 0)
                {
                    result = true;
                    int level = groupDAL.GetGroupLevel(parentId);
                    _level = level;
                    if (level<=2)
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
        private async Task LoadAllAccountsAsync()
        {
            AccountGroupBAL accountGroupBAL = new AccountGroupBAL();
            try
            {
                trvContent.Nodes.Clear();
                List<AccountGroupModel> listAccount = accountGroupBAL.GetAllAccountGroup();

                List<AccountGroupModel> listAccountMain = listAccount.Where(x => x.Parent_id == 0).ToList();
                List<AccountLedgerModel> listLedgerAll = await LoadAllLedgerAsync(-1);

                foreach (AccountGroupModel account in listAccountMain)
                {
                    TreeNode tNode;
                    tNode = trvContent.Nodes.Add(account.Group_name);
                    tNode.Tag = account.Group_id;
                    List<AccountGroupModel> listAccountSub = listAccount.Where(x => x.Parent_id == account.Group_id).ToList();
                    if (listAccountSub.Count > 0)
                        await LoadAllAccountSubAsync(listAccount, account.Group_id,listLedgerAll);
                }
            }
            catch (Exception ee)
            {
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }

        private async Task<List<AccountLedgerModel>> LoadAllLedgerAsync(int groupId=-1)
        {
            AccountLedgerBAL accounLedgerBAL = new AccountLedgerBAL();
            List<AccountLedgerModel> listLedger=new List<AccountLedgerModel>();
            try
            {
                listLedger =await accounLedgerBAL.GetAllAccountLedgersAsync(groupId);
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
        private async Task LoadAllAccountSubAsync(List<AccountGroupModel> listAccount,int groupId, List<AccountLedgerModel> listLedgerAll )
        {
            try
            {

                int i = 0;
                List<AccountLedgerModel> listledger;
                List<AccountGroupModel> listAccountSub = listAccount.Where(x => x.Parent_id == groupId).ToList();
                //  for (int i = 0; i < nodeLevel; i++)
                {
                    int l2 = 0;
                    foreach (AccountGroupModel account in listAccountSub)
                    {
                        //Level 2 
                        TreeNode tNode;
                        tNode = trvContent.Nodes[trvContent.Nodes.Count - 1].Nodes.Add(account.Group_name);
                        tNode.Tag = account.Group_id;

                        List<AccountGroupModel> listAccountSub2 = listAccount.Where(x => x.Parent_id == account.Group_id).ToList();

                        i = 0;
                        int l3 = 0;
                        if (listAccountSub2.Count > 0)
                        {

                            foreach (AccountGroupModel account1 in listAccountSub2)
                            {
                                if (account1.Group_name == "Selling & Distribution Expense")
                                {
                                    string ss = "";
                                }
                                TreeNode tNode1;
                                tNode1 = trvContent.Nodes[trvContent.Nodes.Count - 1].Nodes[l2].Nodes.Add(account1.Group_name);
                                tNode1.Tag = account1.Group_id;


                                listledger = new List<AccountLedgerModel>();
                                listledger = listLedgerAll.Where(x => x.Group_id == account1.Group_id).ToList();
                                foreach (AccountLedgerModel ledger in listledger)
                                {
                                    TreeNode tNodeLedger;
                                    tNodeLedger = trvContent.Nodes[trvContent.Nodes.Count - 1].Nodes[l2].Nodes[i].Nodes.Add(ledger.Ledger_name);
                                    tNodeLedger.Tag = ledger.Ledger_id;
                                    tNodeLedger.ForeColor = Color.Blue;
                                    tNodeLedger.ToolTipText = ledger.Description;

                                }
                                i++;

                                List<AccountGroupModel> listAccountSub3 = listAccount.Where(x => x.Parent_id == account1.Group_id).ToList();

                                if (listAccountSub3.Count > 0)
                                {
                                    //int l3 = 0;
                                    //int j = 0;
                                    int j = listledger.Count >0?listledger.Count:0;
                                    foreach (AccountGroupModel account2 in listAccountSub3)
                                    {
                                        if (account2.Group_name == "Staff Accomodation")
                                        {
                                            string ss = "";
                                        }
                                        TreeNode tNode2;
                                        tNode2 = trvContent.Nodes[trvContent.Nodes.Count - 1].Nodes[l2].Nodes[l3].Nodes.Add(account2.Group_name);
                                        tNode2.Tag = account2.Group_id;

                                        if (listLedgerAll.Count > 0)
                                        {

                                            listledger = new List<AccountLedgerModel>();
                                            listledger = listLedgerAll.Where(x => x.Group_id == account2.Group_id).ToList();

                                            foreach (AccountLedgerModel ledger in listledger)
                                            {
                                                if (ledger.Ledger_name == "DECAL COST")
                                                {
                                                    string ss = "";
                                                }
                                                TreeNode tNodeLedger;
                                                tNodeLedger = trvContent.Nodes[trvContent.Nodes.Count - 1].Nodes[l2].Nodes[l3].Nodes[j].Nodes.Add(ledger.Ledger_name);
                                                tNodeLedger.Tag = ledger.Ledger_id;
                                                tNodeLedger.ForeColor = Color.Blue;
                                                tNodeLedger.ToolTipText = ledger.Description;

                                            }
                                            j++;
                                        }
                                        else
                                            l3++;


                                    }
                                    l3++;
                                }
                                else
                                    l3++;


                            }

                            l2++;
                        }
                        else
                            l3++;



                    }


                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
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

                if (e.Node.ForeColor == Color.Blue)
                {
                    txtLedgerId.Text = e.Node.Tag.ToString();
                    txtLedgerName.Text = e.Node.Text;
                    txtDescription.Text = e.Node.ToolTipText;
                    AccountLedgerBAL accountLedgerBAL = new AccountLedgerBAL();
                    if (accountLedgerBAL.IsCostCenterEnabled(Convert.ToInt32(e.Node.Tag.ToString())))
                        chkCostCenter.Checked = true;
                    else
                        chkCostCenter.Checked = false;

                    ButtonActive(EnumFormEvents.Update);
                }
                else
                {
                    txtGroupId.Text = e.Node.Tag.ToString();
                    txtGroupName.Text = e.Node.Text;
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
    }


    /// <summary>
    /// All Buttons should assigned to below class
    /// </summary>
    class LedgerButtonCollection
    {
        public Button BtnNew { get; set; }
        public Button BtnCancel { get; set; }
        public Button BtnClose { get; set; }
        public Button BtnSave { get; set; }
    }
}
