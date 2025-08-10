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
    public partial class AccountGroupForm : Form
    {
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
        ButtonCollection button;

        public AccountGroupForm()
        {
            InitializeComponent();
        }
        private void AccountGroupForm_Load(object sender, EventArgs e)
        {
            button = new ButtonCollection
            {
                BtnNew = btnNew,
                BtnCancel = btnCancel,
                BtnClose = btnClose,
                BtnSave = btnSave
            };
            ButtonActive(EnumFormEvents.FormLoad);
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
                    break;

                case EnumFormEvents.New:
                    button.BtnNew.Enabled = false;
                    button.BtnSave.Enabled = true;
                    button.BtnClose.Enabled = true;
                    button.BtnCancel.Enabled = true;
                    pnlSaveConent.Enabled = true;
                    int level = 0;
                    CheckLevel(out level);
                    if (level > 2)
                    {
                        
                        lnkChangeParent.Show();
                        lnkChangeParent.Text = $"Change parent of (  {txtParentName.Text} ) ";
                    }
                    txtGroupName.Focus();
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

                case EnumFormEvents.Save:
                    SaveGroup();
                    break;

                case EnumFormEvents.NodeClick:
                    btnCancel.Enabled = true;
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

        private bool CheckLevel( out int _level)
        {
            bool result = false;
            _level = 0;
            try
            {
                DAL.DAL.AccountGroupDAL groupDAL = new DAL.DAL.AccountGroupDAL();
                int parentId = General.ParseInt(txtParentId.Text);
                if (parentId > 0)
                {
                    result = true;
                    int level = groupDAL.GetGroupLevel(parentId);
                    _level = level;
                    if (level > 3)
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
        /// Validation
        /// </summary>
        /// <param name="workMode"></param>
        private bool Validation(Enum workMode)
        {
            bool response = false;
            switch (workMode)
            {
                case EnumWorkMode.Group:
                    if (General.IsTextboxEmpty(txtGroupName)) { response = false;General.ShowMessage(General.EnumMessageTypes.Warning, "Please enter GroupName"); } else response = true;
                    if (General.IsTextboxEmpty(txtParentName)) { response = false; General.ShowMessage(General.EnumMessageTypes.Warning, "Please eselect parent"); } else response = true;
                    int level = 0;
                    if (!CheckLevel(out level)) { response = false; General.ShowMessage(General.EnumMessageTypes.Warning, "Maximum level of group reached"); } else response = true;
                    break;
                default:
                    break;
            }
            return response;
        }

        private void LoadChangeParent()
        {
            txtGroupName.Text = txtParentName.Text;
            txtGroupId.Text = txtParentId.Text;
            txtParentId.Clear();
            txtParentName.Clear();
        }

        #endregion CustomFunction


        /// <summary>
        /// Heare ALL Save Update Load Works
        /// </summary>
        #region SaveUpdateLoad

        private void SaveGroup()
        {
            try
            {
                if (General.ShowMessageConfirm() == DialogResult.Yes)
                {
                    if (Validation(EnumWorkMode.Group))
                    {
                        if (!lnkChangeParent.Visible)
                        {
                            AccountGroupBAL accountGroupBAL = new AccountGroupBAL();
                            AccountGroupModel accountGroupModel = new AccountGroupModel
                            {
                                Group_id = 0,
                                Group_name = txtGroupName.Text.Trim(),
                                Description = txtDescription.Text.Trim(),
                                Parent_id = General.IsTextboxEmpty(txtParentId) ? 0 : Convert.ToInt32(txtParentId.Text)
                            };

                            accountGroupBAL.SaveAccountGroup(accountGroupModel);
                        }
                        //Change parent level
                        else
                        {
                            AccountGroupBAL accountGroupBAL = new AccountGroupBAL();
                            AccountGroupModel accountGroupModel = new AccountGroupModel
                            {
                                Group_id= General.IsTextboxEmpty(txtGroupId) ? 0 : Convert.ToInt32(txtGroupId.Text),
                                Group_name = txtGroupName.Text.Trim(),
                                Description = txtDescription.Text.Trim(),
                                Parent_id = General.IsTextboxEmpty(txtParentId) ? 0 : Convert.ToInt32(txtParentId.Text)
                            };

                            accountGroupBAL.SaveAccountGroup(accountGroupModel);
                        }

                        General.Action($"Account Group Saved {txtGroupName.Text} under  {txtParentName.Text}");
                        General.ShowMessage(General.EnumMessageTypes.Success, "Account Group Successfully Saved ");
                        ButtonActive(EnumFormEvents.Cancel);
                        LoadAllAccounts();
                    }
                }
               
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void LoadAllAccounts()
        {
            AccountGroupBAL accountGroupBAL = new AccountGroupBAL();
            try
            {
                trvContent.Nodes.Clear();
                List<AccountGroupModel> listAccount = accountGroupBAL.GetAllAccountGroup();

                List<AccountGroupModel> listAccountMain = listAccount.Where(x => x.Parent_id == 0).ToList();
                foreach (AccountGroupModel account in listAccountMain)
                {
                    TreeNode tNode;
                    tNode = trvContent.Nodes.Add(account.Group_name);
                    tNode.Tag = account.Group_id;
                    List<AccountGroupModel> listAccountSub = listAccount.Where(x => x.Parent_id == account.Group_id).ToList();
                    if (listAccountSub.Count > 0)
                        LoadAllAccountSub(listAccount, account.Group_id);
                }
            }
            catch (Exception ee)
            {
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }

        /// <summary>
        /// Fill Sub Account
        /// </summary>
        private void LoadAllAccountSub(List<AccountGroupModel> listAccount,int groupId,int nodeLevel=1 )
        {
            try
            {
                if (groupId == 3)
                {
                    string ss = "";
                }
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
                        
                        if (listAccountSub2.Count > 0)
                        {
                            int l3 = 0;
                            foreach (AccountGroupModel account1 in listAccountSub2)
                            {
                                TreeNode tNode1;
                                tNode1 = trvContent.Nodes[trvContent.Nodes.Count - 1].Nodes[l2].Nodes.Add(account1.Group_name);
                                //  tNode1 = trvContent.Nodes[trvContent.Nodes.Count - 1].Nodes[trvContent.Nodes[trvContent.Nodes.Count - 1].Nodes.Count-1].Nodes.Add(account1.Group_name);
                                tNode1.Tag = account1.Group_id;

                                List<AccountGroupModel> listAccountSub3 = listAccount.Where(x => x.Parent_id == account1.Group_id).ToList();

                                if (listAccountSub3.Count > 0)
                                {
                                    int i = 0;
                                    foreach (AccountGroupModel account2 in listAccountSub3)
                                    {
                                        if (account2.Group_name == "DECAL COST")
                                        {
                                            string SS = "";
                                        }
                                        TreeNode tNode2;
                                        tNode2 = trvContent.Nodes[trvContent.Nodes.Count - 1].Nodes[l2].Nodes[l3].Nodes.Add(account2.Group_name);
                                        // tNode2 = trvContent.Nodes[trvContent.Nodes.Count - 1].Nodes[trvContent.Nodes[trvContent.Nodes.Count - 1].Nodes.Count - 1].Nodes[i].Nodes.Add(account2.Group_name);
                                        tNode2.Tag = account2.Group_id;
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

        #endregion SaveUpdate

        #region NextFocus
        private void NextFocus(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (sender == txtDescription)
                    btnSave.Focus();
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
                txtParentId.Text = e.Node.Tag.ToString();
                txtParentName.Text = e.Node.Text;
                ButtonActive(EnumFormEvents.NodeClick);

            }
            catch (Exception ee)
            {
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
        #endregion Treeview

        private void lnkChangeParent_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LoadChangeParent();
        }
    }


    /// <summary>
    /// All Buttons should assigned to below class
    /// </summary>
    class ButtonCollection
    {
        public Button BtnNew { get; set; }
        public Button BtnCancel { get; set; }
        public Button BtnClose { get; set; }
        public Button BtnSave { get; set; }
    }
}
