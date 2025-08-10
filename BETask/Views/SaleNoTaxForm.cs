using BETask.Common;
using BETask.BAL;
using BETask.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using EDMX = BETask.DAL.EDMX;


namespace BETask.Views
{
    public partial class SaleNoTaxForm : Form
    {
       
        SaleNoTaxButtonCollection button;
        SaleBAL saleBAL = new SaleBAL();
        public SaleNoTaxForm()
        {
            InitializeComponent();
        }
        public enum EnumFormEvents
        {
            FormLoad,            
            Close,
            Search,
        }
        private void ButtonActive(Enum activeEvent)
        {

            switch (activeEvent)
            {
                case EnumFormEvents.FormLoad:
                    Search();
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

            else if (sender == button.BtnClose)
            {
                ButtonActive(EnumFormEvents.Close);
            }
        }

        private void SaleNoTaxForm_Load(object sender, EventArgs e)
        {
            button = new SaleNoTaxButtonCollection()
            {
                BtnSearch = btnSearch,               
                BtnClose = btnClose
            };
            Search();
        }

        class SaleNoTaxButtonCollection
        {
            public Button BtnSearch { get; set; }
            public Button BtnCancel { get; set; }
            public Button BtnClose { get; set; }

        }

        private void Search()
        {
            try
            {
                
                General.ClearGrid(gridSaleNoTax);

                List<EDMX.sales_item> listSale = saleBAL.SearchSaleNoTax(General.ConvertDateServerFormatWithStartTime(dtpDateFrom.Value), General.ConvertDateServerFormatWithEndTime(dtpDateTo.Value));
                foreach (EDMX.sales_item sale in listSale)
                {
                    gridSaleNoTax.Rows.Add(General.ConvertDateAppFormat(sale.sales.sales_date), sale.sales.customer.customer_name, sale.sales_id, sale.sales.sales_number, sale.qty, sale.rate, sale.vat_amount, sale.net_amount, sale.sales.payment_mode, sale.delivery_item_id, sale.item.item_name);//, sale.sales.customer.customer_name
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
