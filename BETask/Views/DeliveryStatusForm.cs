using BETask.Common;
using BETask.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BETask.BAL;
using EDMX = BETask.DAL.EDMX;
using EDMXAPP = BETask.APP.EDMX;

namespace BETask.Views
{
    public partial class DeliveryStatusForm : Form
    {
        SynchronizationBAL sync = new SynchronizationBAL();
        public DeliveryStatusForm()
        {
            InitializeComponent();
            Reload();
        }
        private void Reload()
        {
            lblError.Text = "";
            DeliveryStatus();
            DeliveryReturnStatus();
            DeliveryCollectionStatus();
        }
        private void DeliveryStatus()
        {
            try
            {
                General.ClearGrid(gridStatus);
                var listDelivery = sync.RPTDeliveryStatus(General.ConvertDateServerFormat(dtpDeliveryDate.Value));
                if (listDelivery != null)
                {
                    foreach (EDMXAPP.RPT_DeliveryStatus_Result rpt in listDelivery)
                    {
                        gridStatus.Rows.Add(rpt.first_name,rpt.route_name,rpt.item_name,rpt.TotalDelivery,rpt.Delivered,rpt.TobeDelivered);
                    }
                }
            }
            catch (Exception ee)
            {
                lblError.Text = ee.Message;
            }
        }
        private void DeliveryReturnStatus()
        {
            try
            {
                General.ClearGrid(gridReturnStatus);
                var listDelivery = sync.RPTDeliveryReturnStatus(General.ConvertDateServerFormat(dtpDeliveryDate.Value));
                if (listDelivery != null)
                {
                    foreach (EDMXAPP.RPT_DeliveryReturnStatus_Result rpt in listDelivery)
                    {
                        gridReturnStatus.Rows.Add(rpt.first_name, rpt.route_name, rpt.item_name, rpt.ReturnQty);
                    }
                }
            }
            catch (Exception ee)
            {
                lblError.Text = ee.Message;
            }
        }
        private void DeliveryCollectionStatus()
        {
            try
            {
                General.ClearGrid(gridCollectionStatus);
                var listDelivery = sync.RPTCollectionStatus(General.ConvertDateServerFormat(dtpDeliveryDate.Value));
                if (listDelivery != null)
                {
                    foreach (EDMXAPP.RPT_CollectionStatus_Result rpt in listDelivery)
                    {
                        gridCollectionStatus.Rows.Add(rpt.first_name, rpt.route_name,rpt.Collected, rpt.Payment_Mode);
                    }
                }
            }
            catch (Exception ee)
            {
                lblError.Text = ee.Message;
            }
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {

            Reload();
        }

        private void DeliveryStatusForm_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Reload();
        }
    }
}
