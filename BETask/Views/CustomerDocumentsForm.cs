using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using BETask.Common;
using BETask.BAL;
using System.Windows.Forms;
using EDMX = BETask.DAL.EDMX;
using BETask.Model;
using BETask.APP.DAL;


namespace BETask.Views
{
    public partial class CustomerDocumentsForm : Form
    {
        string baseUrl = "http://betask_srb_test.letzservices.com/";
        int CustomerId { get; set; }
        CustomerDocumentsButtonCollection button;
        public enum EnumFormEvents
        {
            FormLoad,
            Close
        }

        private void ButtonActive(Enum activeEvent)
        {

            switch (activeEvent)
            {
                case EnumFormEvents.FormLoad:
                    break;
                case EnumFormEvents.Close:
                    this.BeginInvoke(new MethodInvoker(Close));
                    break;
                default:
                    break;

            }
        }

        private void FormLoad()
        {
            button = new CustomerDocumentsButtonCollection
            {
                BtnClose = btnClose

            };
        }

        public CustomerDocumentsForm()
        {
            InitializeComponent();
        }
        public CustomerDocumentsForm(int customerId)
        {
            InitializeComponent();
            this.CustomerId = customerId;
            DAL.DAL.CompanyDAL company = new DAL.DAL.CompanyDAL();
            DAL.EDMX.system_settings setting = company.GetSystemSettings();
            baseUrl = setting.cloud_url;
            if (string.IsNullOrEmpty(baseUrl))
            {
                General.ShowMessage(General.EnumMessageTypes.Error,"Cloud url not upadated");
                this.Close();
            }
        }

    
        private void NextFocus(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                General.NextFocus(sender, e);
            }
        }

        private void ButtonEvents(object sender, EventArgs e)
        {
            if (sender == button.BtnClose)
            {
                ButtonActive(EnumFormEvents.Close);
            }
        }

        private void CustomerDocuments_Load(object sender, EventArgs e)
        {
            FormLoad();
            GetCustomerDocuments(this.CustomerId);
        }

        private void GetCustomerDocuments(int CustomerId)
        {
            try
            {
                General.ClearGrid(dgCustomerDocuments);
                CustomerAppDAL customerAppDAL = new CustomerAppDAL();
                List<APP.EDMX.customer_upload> listDocument = customerAppDAL.GetAllCustomerDocuments(CustomerId);
                if(listDocument != null)
                {
                    foreach(APP.EDMX.customer_upload document in listDocument)
                    {
                        dgCustomerDocuments.Rows.Add(document.document_type, document.description, document.filename,"View");
                    }
                }
            }
            catch(Exception ex)
            {
                General.Error(ex.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ex.Message);
            }           
        }
       
        class CustomerDocumentsButtonCollection
        {

            public Button BtnClose { get; set; }

        }       
        private void dgCustomerDocuments_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgCustomerDocuments.Columns.Count - 1)
            {
                string imgName = dgCustomerDocuments.CurrentRow.Cells["clmFileName"].Value.ToString();
                string url = $"{baseUrl}uploads/{imgName}";
                DocumentViewerForm documentViewerForm = new DocumentViewerForm(url);
                documentViewerForm.ShowDialog();
            }
        }
    }
}
