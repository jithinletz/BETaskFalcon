using BETask.Common;
using BETask.BAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Data;
using EDMX = BETask.APP.EDMX;

namespace BETask.Views
{
    public partial class DeliveryRequestForm : Form
    {
        public enum EnumFormEvents
        {
            FormLoad,
            Cancel,
            Close,
            Search
        }
        public int saleId = 0;
        SaleBAL saleBAL = new SaleBAL();
        BAL.SynchronizationBAL objSynchronizationBAL = new BAL.SynchronizationBAL();        
        List<EDMX.app_delivery_request> listAppDeliveryRequest = new List<EDMX.app_delivery_request>();
        DeliveryRequestButtonCollection button;
        public DeliveryRequestForm()
        {
            InitializeComponent();
        }
        private void ButtonActive(Enum activeEvent)
        {

            switch (activeEvent)
            {
                case EnumFormEvents.FormLoad:
                    Search();
                    pnlDelievryRequestItem.Visible = false;
                    break;
                case EnumFormEvents.Cancel:
                    pnlDelievryRequestItem.Visible = false;                    
                    ButtonActive(EnumFormEvents.FormLoad);                  
                    break;
                case EnumFormEvents.Close:
                    this.BeginInvoke(new MethodInvoker(Close));
                    break;
                case EnumFormEvents.Search:
                    Search();
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
            else if (sender == button.BtnCancel)
            {
                ButtonActive(EnumFormEvents.Cancel);
            }

            else if (sender == button.BtnClose)
            {
                ButtonActive(EnumFormEvents.Close);
            }
            
        }
       

        private void Search()
        {
            try
            {

                General.ClearGrid(dgvDeliveryRequest);
                string custStatus = string.Empty;
                
                DateTime dateFrom = General.ConvertDateServerFormatWithStartTime(dtpDateFrom.Value);
                DateTime dateTo = General.ConvertDateServerFormatWithEndTime(dtpDateTo.Value);
                listAppDeliveryRequest = objSynchronizationBAL.GetAppDeliveryRequest(dateFrom, dateTo);
                foreach (EDMX.app_delivery_request objDeliveryRequest in listAppDeliveryRequest.OrderBy(x=> x.customer_id))
                {
                    custStatus = objDeliveryRequest.customer_id > 0 ? "Exsisting" : "New";
                    dgvDeliveryRequest.Rows.Add(objDeliveryRequest.request_id, objDeliveryRequest.customer_name, custStatus, objDeliveryRequest.address1, objDeliveryRequest.address2,
                                                objDeliveryRequest.items_count, objDeliveryRequest.net_amount, objDeliveryRequest.request_time, objDeliveryRequest.other_details, "View Item", "Update");

                }

            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error);
            
            }

        }


        private void DeliveryRequestForm_Load(object sender, EventArgs e)
        {
            button = new DeliveryRequestButtonCollection()
            {
                BtnSearch = btnSearch,
                BtnCancel = btnCancel,
                BtnClose = btnClose
            };
            Search();
          
        }

        private void btnPanelClose_Click(object sender, EventArgs e)
        {
            pnlDelievryRequestItem.Visible = false;
        }

        private void dgvDeliveryRequest_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                try
                {
                    if (dgvDeliveryRequest[e.ColumnIndex, e.RowIndex].Value != null)
                    {
                        EDMX.app_delivery_request objDeliveryRequest = new EDMX.app_delivery_request();
                        if (dgvDeliveryRequest[e.ColumnIndex, e.RowIndex].Value.ToString().ToUpper() == "VIEW ITEM")
                        {
                            DataTable tblDeliveryItem = new DataTable();
                            int requestId = Convert.ToInt32(dgvDeliveryRequest[0, e.RowIndex].Value);
                            tblDeliveryItem = objSynchronizationBAL.GetAppDeliveryItemByRequestId(requestId);
                            General.ClearGrid(dgvDeliveryItems);
                            if (tblDeliveryItem != null)
                            {
                                foreach (DataRow dr in tblDeliveryItem.Rows)
                                {
                                    dgvDeliveryItems.Rows.Add(dr["ItemId"].ToString(), dr["ItemName"].ToString(), dr["Rate"].ToString(), dr["Qty"].ToString(), dr["Net"].ToString());
                                }
                                pnlDelievryRequestItem.Visible = true;
                            }

                        }
                        else if (dgvDeliveryRequest[e.ColumnIndex, e.RowIndex].Value.ToString().ToUpper() == "UPDATE")
                        {
                            if (General.ShowMessageConfirm($"Are you sure to update") == DialogResult.Yes)
                            {
                                int requestId = Convert.ToInt32(dgvDeliveryRequest[0, e.RowIndex].Value);
                                objDeliveryRequest = objSynchronizationBAL.GetAppDeliveryRequestByRequestId(requestId);
                                if (objDeliveryRequest != null && Convert.ToInt32(objDeliveryRequest.request_id) > 0)
                                {
                                    objDeliveryRequest.status = 4;
                                    objSynchronizationBAL.SaveDeliveryRequest(objDeliveryRequest);
                                    General.ShowMessage(General.EnumMessageTypes.Success, $"Delivery successfully updated");
                                    ButtonActive(EnumFormEvents.FormLoad);
                                }
                            }

                        }
                    }
                }
                catch (Exception ee)
                {
                    General.Error(ee.ToString());
                    General.ShowMessage(General.EnumMessageTypes.Error);
                    
                }
            }
        }
    }
    class DeliveryRequestButtonCollection
    {
        public Button BtnSearch { get; set; }
        public Button BtnCancel { get; set; }
        public Button BtnClose { get; set; }
        public Button BtnPrint { get; set; }

    }
}
