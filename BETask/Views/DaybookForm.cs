using BETask.Common;
using BETask.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BETask.BAL;
using EDMX = BETask.DAL.EDMX;
using System.Drawing;

namespace BETask.Views
{
    public partial class DaybookForm : Form
    {
        DDaybookButtonCollection button;
        AccountTransactionBAL accountTransaction = new AccountTransactionBAL();
        bool isDetailed = false;
        public enum EnumFormEvents
        {
            FormLoad,
            Cancel,
            Close,
            Search,
            Print,
            Detailed
        }
        public DaybookForm()
        {
            InitializeComponent();
        }

        private void DaybookForm_Load(object sender, EventArgs e)
        {
            button = new DDaybookButtonCollection
            {

                BtnCancel = btnCancel,
                BtnClose = btnClose,
                BtnPrint = btnPrint,
                BtnDetailed=btnDetailed,
                BtnSearch=btnSearch


            };
            Search();
        }
        private void ButtonActive(Enum activeEvent)
        {

            switch (activeEvent)
            {
                case EnumFormEvents.FormLoad:
                    Search();
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
                case EnumFormEvents.Detailed:
                    SearchDetailed();
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
            if (sender == button.BtnDetailed)
            {
                ButtonActive(EnumFormEvents.Detailed);
            }
            else if (sender == button.BtnCancel)
            {
                ButtonActive(EnumFormEvents.Cancel);
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
                isDetailed = false;
                General.ClearGrid(gridDaybook);
                var daybook = accountTransaction.Daybook(General.ConvertDateServerFormat(dtpDateFrom.Value));
                foreach (DAL.Model.DaybookModel day in daybook)
                {
                    gridDaybook.Rows.Add(day.TransactionType, day.Credit, day.Debit);
                }
                lblCredit.Text = String.Format("{0} {1}", "Total Credit", daybook.Sum(x => x.Credit));
                lblDebit.Text = String.Format("{0} {1}", "Total Debit", daybook.Sum(x => x.Debit));
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
        private void SearchDetailed()
        {
            try
            {
                isDetailed = true;
                General.ClearGrid(gridDaybook);
                var daybook = accountTransaction.DaybookDetailed(General.ConvertDateServerFormat(dtpDateFrom.Value));
                foreach (DAL.Model.DaybookDetailedModel day in daybook)
                {
                    gridDaybook.Rows.Add(day.TransactionType, day.Credit, day.Debit);
                  
                }
                foreach (DataGridViewRow dr in gridDaybook.Rows)
                {
                    dr.DefaultCellStyle.BackColor = Color.Yellow;
                }
                lblCredit.Text = String.Format("{0} {1}", "Total Credit", daybook.Sum(x => x.Credit));
                lblDebit.Text = String.Format("{0} {1}", "Total Debit", daybook.Sum(x => x.Debit));
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
        private void Print()
        {
            try
            {
                if (!isDetailed)
                    accountTransaction.PrintDaybookSummary(General.ConvertDateServerFormat(dtpDateFrom.Value));
                else
                    accountTransaction.PrintDaybookDetailed(General.ConvertDateServerFormat(dtpDateFrom.Value));
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
    }
    class DDaybookButtonCollection
    {
        public Button BtnSearch { get; set; }
        public Button BtnDetailed { get; set; }
        public Button BtnCancel { get; set; }
        public Button BtnClose { get; set; }
        public Button BtnPrint { get; set; }

    }
}
