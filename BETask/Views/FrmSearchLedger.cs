using BETask.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using BETask.DAL;
using BETask.DAL.EDMX;

namespace BETask.Views
{
    public partial class FrmSearchLedger : Form
    {
        string CodeTextname = "";
        string NameTextboxName = "";
        Panel panel = null;

        public FrmSearchLedger(Panel pnl,string codeTextname,string nameTextboxName)
        {
            InitializeComponent();

            

            BindAccountGroups();
            this.cboAccountsGroups.SelectedValue = 0;

            this.StartPosition = FormStartPosition.CenterScreen;
            this.CodeTextname = codeTextname;
            this.NameTextboxName = nameTextboxName;

            this.panel = pnl;
        }

        private void FrmSearchLedger_Load(object sender, EventArgs e)
        {
            btnSearch.Click += BtnClicks;
            txtLedgerName.KeyDown += Txt_KeyDown;
            this.KeyDown += FrmSearchLedger_KeyDown;
            this.gridViewSearch.DoubleClick += GridViewSearch_DoubleClick;
            this.gridViewSearch.KeyDown += GridViewSearch_KeyDown;

            BtnClicks(btnSearch, null);
        }

        private void GridViewSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                if (panel != null)
                {
                    panel.Controls[CodeTextname].Text = gridViewSearch[0, gridViewSearch.CurrentCell.RowIndex].Value.ToString();
                    panel.Controls[NameTextboxName].Text = gridViewSearch[1, gridViewSearch.CurrentCell.RowIndex].Value.ToString();
                    this.Dispose();
                }
            }
        }

        private void GridViewSearch_DoubleClick(object sender, EventArgs e)
        {
            if(panel != null)
            {
                panel.Controls[CodeTextname].Text = gridViewSearch[0, gridViewSearch.CurrentCell.RowIndex].Value.ToString();
                panel.Controls[NameTextboxName].Text = gridViewSearch[1, gridViewSearch.CurrentCell.RowIndex].Value.ToString();
                this.Dispose();

            }
        }

        private void Txt_KeyDown(object sender, KeyEventArgs e)
        {
            if(sender == txtLedgerName)
            {
                if(e.KeyCode == Keys.Enter)
                {
                    BtnClicks(btnSearch, null);
                }
                else if(e.KeyCode == Keys.Down)
                {
                    gridViewSearch.Focus();
                }
            }
        }

        private void FrmSearchLedger_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Down)
            {
                gridViewSearch.Focus();
            }
            else if(e.KeyCode == Keys.Escape)
            {
                this.Dispose();
            }
        }

        private void BtnClicks(object sender, EventArgs e)
        {
            if(sender == btnSearch)
            {
                BindLedger();
            }
        }

        private void BindAccountGroups()
        {
            DAL.DAL.AccountLedgerDAL accountLedgerDAL = new DAL.DAL.AccountLedgerDAL();
            try
            {
                List<account_group> lst = accountLedgerDAL.GetAccountsGroups();
                cboAccountsGroups.DataSource = lst;
                cboAccountsGroups.DisplayMember = "group_name";
                cboAccountsGroups.ValueMember = "group_id";
                
            }
            catch (Exception Ex)
            {
                General.Error(Ex.Message);
            }
        }

        void BindLedger()
        {
            
            DAL.DAL.AccountLedgerDAL accountLedgerDAL = new DAL.DAL.AccountLedgerDAL();
            try
            {
                int groupid = int.Parse(cboAccountsGroups.SelectedValue == null ? "0" : cboAccountsGroups.SelectedValue.ToString());
               
                List<account_ledger> ledger = accountLedgerDAL.SearchLedger(txtLedgerName.Text.Trim().ToString(), groupid).OrderBy(t => t.ledger_name).ToList();

                gridViewSearch.DataSource = ledger;

                gridViewSearch.Columns[0].HeaderText = "Id";
                gridViewSearch.Columns[1].HeaderText = "Ledger Name";
                gridViewSearch.Columns[2].HeaderText = "Descriptions";

                gridViewSearch.Columns[0].DataPropertyName = "ledger_id";
                gridViewSearch.Columns[1].DataPropertyName = "ledger_name";
                //gridViewSearch.Columns[2].DataPropertyName = "group_id";
                gridViewSearch.Columns[6].DataPropertyName = "account_group.group_name";

                for (int i = 0; i < gridViewSearch.Columns.Count; i++)
                {
                    gridViewSearch.Columns[i].Visible = true;
                }

                for (int i = 3; i < gridViewSearch.Columns.Count; i++)
                {
                    gridViewSearch.Columns[i].Visible = false;
                }

                gridViewSearch.Columns[0].Width = 100;
                gridViewSearch.Columns[1].Width = 300;
                gridViewSearch.Columns[2].Width = 250;

                gridViewSearch.Columns[0].DisplayIndex = 0;
                gridViewSearch.Columns[1].DisplayIndex = 1;
                gridViewSearch.Columns[2].DisplayIndex = 2;

               

               
            }
            catch (Exception ex)
            {
                General.ShowMessage(General.EnumMessageTypes.Error, ex.Message);
            }
        }
    }
}
