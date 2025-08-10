using System;
using BETask.Common;
using System.Data;
using System.Collections.Generic;
using System.Windows.Forms;
using BETask.BAL;
using EDMX = BETask.DAL.EDMX;

namespace BETask.Views
{
    public partial class MiscWalletDifferenceForm : Form
    {
        public MiscWalletDifferenceForm()
        {
            InitializeComponent();
        }

        private void MiscWalletDifferenceForm_Load(object sender, EventArgs e)
        {
            Search();
        }
        private void Search()
        {
            try
            {
                DAL.DAL.MiscellaneousReportDAL reportDAL = new DAL.DAL.MiscellaneousReportDAL();
                DataTable tblReport = reportDAL.WalletDifferenceReport();
                gridCustomer.DataSource = tblReport;
                General.GridRownumber(gridCustomer);
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }

        private void gridCustomer_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    string cutomer = gridCustomer[2, e.RowIndex].Value.ToString();
                    int cutomerId = Convert.ToInt32(gridCustomer[1, e.RowIndex].Value.ToString());
                    Views.WalletForm wallet = new WalletForm(cutomer, cutomerId);
                    wallet.ShowDialog();
                }
            }
            catch(Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
    }
}
