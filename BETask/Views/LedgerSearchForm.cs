using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL=BETask.DAL.DAL;
using BETask.Common;

namespace BETask.Views
{
    public partial class LedgerSearchForm : Form
    {
        
        public enum EnumFormEvents
        {
            FormLoad,
            New,
            Save,
            Cancel,
            Close,
            Search,
            Print
        }
        LedgerSearchButtonCollection button;
        DAL.DAL.LedgerSearchDAL ledger = new DAL.DAL.LedgerSearchDAL();
        public int ledgerId = 0;
        public string ledgerName = string.Empty;
        public LedgerSearchForm()
        {
            InitializeComponent();
            txtLedgerName.Focus();
        }
        private void ButtonActive(Enum activeEvent)
        {

            switch (activeEvent)
            {
                case EnumFormEvents.FormLoad:
                   
                    
                    break;
                case EnumFormEvents.Cancel:
                   
                    //ResetForms();
                    break;
                case EnumFormEvents.Close:
                    this.BeginInvoke(new MethodInvoker(Close));
                    break;
                
                
                case EnumFormEvents.Search:
                  
                    break;
               
                default:
                    break;

            }
        }
        private void ButtonEvents(object sender, EventArgs e)
        {
           
            if (sender == button.BtnClose)
            {
                ButtonActive(EnumFormEvents.Close);
            }
           
        }

        private void Search()
        {
            try
            {
                if (txtLedgerName.Text.Length >= 3)
                {
                    List<DAL.EDMX.SP_LedgerSearch_Result> listLedger = ledger.LedgerSearch(txtLedgerName.Text);
                    if (listLedger != null && listLedger.Count > 0)
                    {
                        var Dtatable = General.ToDataTable(listLedger);
                        dgItems.DataSource = Dtatable;
                        dgItems.Columns[1].Width = 300;
                        dgItems.Columns[2].Width = 300;
                    }
                    else
                    {
                        dgItems.DataSource = new DataTable();
                    }
                    txtLedgerName.Focus();
                }
            }
            catch (Exception ex)
            { }
        }
        private void FormLoad()
        {
            button = new LedgerSearchButtonCollection
            {
             
                BtnClose = btnClose,
               
            };
            
            Search();
        }

        private void SubmitItem()
        {
            try
            {
                if (dgItems.Rows.Count > 0)
                {
                    int ridx = dgItems.CurrentRow.Index;
                    int _ledgerId = 0;
                    int.TryParse(dgItems[0, ridx].Value.ToString(), out _ledgerId);
                    ledgerId = _ledgerId;
                    ledgerName = dgItems[1, ridx].Value.ToString();
                    this.DialogResult = DialogResult.OK;
                    try
                    {
                        this.Close();
                    }
                    catch
                    {
                        this.BeginInvoke(new MethodInvoker(Close));

                    }
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error);
                this.BeginInvoke(new MethodInvoker(Close));
            }
        }

        private void LedgerSearchForm_Load(object sender, EventArgs e)
        {
            FormLoad();
        }

        private void txtLedgerName_TextChanged(object sender, EventArgs e)
        {
            
            Search();
        }

        private void dgItems_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                SubmitItem();
            }
        }

        private void dgItems_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            SubmitItem();
        }

        private void txtLedgerName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Down)
            {
                dgItems.Focus();
            }
        }

        private void LedgerSearchForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                ButtonActive(EnumFormEvents.Close);
            }
        }
    }
    class LedgerSearchButtonCollection
    {
        public Button BtnNew { get; set; }
        public Button BtnCancel { get; set; }
        public Button BtnClose { get; set; }
        public Button BtnSave { get; set; }
        public Button BtnSearch { get; set; }
        public Button BtnPrint { get; set; }
    }
}
