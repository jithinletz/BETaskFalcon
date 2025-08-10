using BETask.Common;
using BETask.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BETask.BAL;
using EDMX = BETask.DAL.EDMX;


namespace BETask.Views
{
    public partial class LedgerMappingForm : Form
    {
        public enum EnumFormEvents
        {
            FormLoad,
            New,
            Save,
            Cancel,
            Close,
            Print,
            Approve
        }
        LedgerMappingButtonCollection button;
        public LedgerMappingForm()
        {
            InitializeComponent();
        }
        private void ButtonActive(Enum activeEvent)
        {

            switch (activeEvent)
            {
                case EnumFormEvents.FormLoad:
                    LoadAllAccounts();
                    LoadAllLedger();
                    Search();
                    break;
                case EnumFormEvents.Cancel:
                    ButtonActive(EnumFormEvents.FormLoad);
                    General.ClearTextBoxes(this);
                    break;
                case EnumFormEvents.Close:
                    this.BeginInvoke(new MethodInvoker(Close));
                    break;
                case EnumFormEvents.Save:
                    SaveMapping();
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
            else if (sender == button.BtnCancel)
            {
                ButtonActive(EnumFormEvents.Cancel);
            }

            else if (sender == button.BtnClose)
            {
                ButtonActive(EnumFormEvents.Close);
            }
            else if (sender == button.BtnApproveALl)
            {
                ButtonActive(EnumFormEvents.Approve);
            }

        }
        private void LoadAllAccounts()
        {
            AccountGroupBAL accountGroupBAL = new AccountGroupBAL();
            try
            {
             
                List<AccountGroupModel> listAccount = accountGroupBAL.GetAllAccountGroup();

                //List<AccountGroupModel> listAccountMain = listAccount.Where(x => x.Parent_id == 0).ToList();
                foreach (AccountGroupModel account in listAccount)
                {
                    ComboboxItem _cmbItem = new ComboboxItem()
                    {
                        Text = account.Group_name,
                        Value = account.Group_id
                    };
                    cmbGroup.Items.Add(_cmbItem);
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
     
        private void LoadAllLedger()
        {
            BAL.AccountLedgerBAL accountLedgerBAL = new BAL.AccountLedgerBAL();

            try
            {
                int  groupId =-1;

                if (!String.IsNullOrEmpty(cmbGroup.Text))
                {
                    Object selectedGroup = cmbGroup.SelectedItem;
                    groupId = (int)((BETask.Views.ComboboxItem)selectedGroup).Value;
                }

                List<Model.AccountLedgerModel> listLedger = accountLedgerBAL.GetAllAccountLedger(groupId).OrderBy(x => x.Ledger_name).ToList();
                foreach (Model.AccountLedgerModel ledger in listLedger)
                {
                    ComboboxItem _cmbItem = new ComboboxItem()
                    {
                        Text = ledger.Ledger_name,
                        Value = ledger.Ledger_id
                    };
                    cmbLedgerAccount.Items.Add(_cmbItem);
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error);
            }
        }

        private void Search()
        {
            try
            {
                General.ClearGrid(dgItems);
                BAL.AccountLedgerBAL accountLedgerBAL = new BAL.AccountLedgerBAL();
                List<EDMX.ledger_mapping> listLedger = accountLedgerBAL.GetAllLedgerMapping();
                if (listLedger != null && listLedger.Count > 0)
                {
                    foreach (EDMX.ledger_mapping ledger in listLedger)
                    {
                        string group = ledger.account_group == null ? "" : $"{ledger.group_id}-{ledger.account_group.group_name}";
                        string _ledger = ledger.account_ledger == null ? "" : $"{ledger.ledger_id}-{ledger.account_ledger.ledger_name}";
                        dgItems.Rows.Add(ledger.ledger_mapping_id, ledger.ledger_type,group,_ledger,ledger.status);
                    }
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
        private void SaveMapping()
        {
            try
            {
                if (General.ShowMessageConfirm() == DialogResult.Yes)
                {
                    if (!string.IsNullOrEmpty(txtSettings.Text))
                    {
                        if (txtPassword.Text != "LetZLedMaP")
                        {
                            General.ShowMessage(General.EnumMessageTypes.Error, "Invalid Password");
                            return;
                        }
                        int ledgerId = 0, groupId = 0;

                        if (!String.IsNullOrEmpty(cmbGroup.Text))
                        {
                            Object selectedGroup = cmbGroup.SelectedItem;
                            groupId = (int)((BETask.Views.ComboboxItem)selectedGroup).Value;
                        }
                        if (!String.IsNullOrEmpty(cmbLedgerAccount.Text))
                        {
                            Object selectedLedger = cmbLedgerAccount.SelectedItem;
                            ledgerId = (int)((BETask.Views.ComboboxItem)selectedLedger).Value;
                        }
                        AccountLedgerBAL accountLedgerBAL = new AccountLedgerBAL();
                        EDMX.ledger_mapping ledger = new EDMX.ledger_mapping
                        {
                            ledger_mapping_id = 0,
                            ledger_type = txtSettings.Text,
                            group_id = groupId,
                            ledger_id = ledgerId,
                            status = 1

                        };
                        if (groupId > 0 || ledgerId > 0)
                        {
                            accountLedgerBAL.SaveLedgerMapSetting(ledger);
                            General.Action($"Ledger mapping saved {txtSettings.Text} ");
                            General.ShowMessage(General.EnumMessageTypes.Success, "Succefully Saved");
                            Search();
                        }
                    }
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }

        }

        private void FormLoad()
        {
            button = new LedgerMappingButtonCollection
            {

                BtnCancel = btnCancel,
                BtnClose = btnClose,
                BtnSave = btnSave,
        
            };
            ButtonActive(EnumFormEvents.FormLoad);
        }

        private void LedgerMappingForm_Load(object sender, EventArgs e)
        {
            FormLoad();
        }

        private void cmbGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadAllLedger();
        }
    }
    class LedgerMappingButtonCollection
    {
        public Button BtnCancel { get; set; }
        public Button BtnClose { get; set; }
        public Button BtnSave { get; set; }
        public Button BtnApproveALl { get; set; }
    }
}
