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

namespace BETask.Views
{
    public partial class SaleMergingForm : Form
    {
        SaleMergingButtonCollection button;
       
        public enum EnumFormEvents

        {
            FormLoad,
            Cancel,
            Close,
            Save,
            Search,
            Print
        }

        private void ButtonActive(Enum activeEvent)
        {

            switch (activeEvent)
            {
                case EnumFormEvents.FormLoad:                    
                    break;
                case EnumFormEvents.Cancel:
                    ButtonActive(EnumFormEvents.FormLoad);                    
                    dgSalemerging.Enabled = false;
                    General.ClearTextBoxes(this);
                    General.ClearGrid(dgSalemerging);
                    break;
                case EnumFormEvents.Close:
                    this.BeginInvoke(new MethodInvoker(Close));
                    break;
                case EnumFormEvents.Save:
                    Merge();
                    
                    break;
                case EnumFormEvents.Print:
                    //Print();
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
            if (sender == button.BtnSave)
            {
                ButtonActive(EnumFormEvents.Save);
            }
            else if (sender == button.BtnCancel)
            {
                ButtonActive(EnumFormEvents.Cancel);
            }
            else if (sender == button.BtnLoad)
            {
                ButtonActive(EnumFormEvents.Search);
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
        public SaleMergingForm()
        {
            InitializeComponent();
        }

        private void GetAllSourceCustomer()
        {
            try
            {
                //CustomerBAL _customerBAL = new CustomerBAL();
                //List<Model.CustomerModel> _lstCustomers = _customerBAL.GetAllCustomers(0, string.Empty, 1);
                DAL.DAL.CustomerDAL customerDAL = new DAL.DAL.CustomerDAL();

                List<EDMX.customer> _listCustomer = customerDAL.GetAllCustomers(0, "", 1, 0);

                //foreach (Model.CustomerModel customer in _lstCustomers)
                foreach (EDMX.customer customer in _listCustomer)
                {
                    ComboboxItem _cmbItem = new ComboboxItem()
                    {
                        Text = customer.customer_name,
                        Value = customer.customer_id
                    };
                    cmbSourceCustomer.Items.Add(_cmbItem);
                    cmbDestinationCustomer.Items.Add(_cmbItem);
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }

       

        private void Total()
        {
            try
            {
                decimal total=0;
                for (int i = 0; i <= dgSalemerging.Rows.Count - 2; i++)
                {
                    if (!string.IsNullOrEmpty(dgSalemerging.Rows[i].Cells["clmNetAmount"].Value.ToString()))
                        total += decimal.Parse(dgSalemerging.Rows[i].Cells["clmNetAmount"].Value.ToString());
                }
                dgSalemerging.Rows[dgSalemerging.Rows.Count - 1].Cells["clmNetAmount"].Value = total;
            }
            catch { }
        }

        private void SaleMergingForm_Load(object sender, EventArgs e)
        {
            button = new SaleMergingButtonCollection
            {
                BtnCancel = btnCancel,
                BtnSave = btnMerge,
                BtnClose = btnClose,
                BtnPrint = btnPrint,
                BtnLoad = btnLoad

            };

            GetAllSourceCustomer();
            
        }

        private void LoadSourceDivsion(int customerId)
        {
            try
            {
                
                BETask.DAL.DAL.CustomerDAL customerDAL = new DAL.DAL.CustomerDAL();
                List<EDMX.customer_division> listDivision = customerDAL.GetCustomerDivision(customerId);
                if(listDivision!=null && listDivision.Count>0)
                {
                    foreach (EDMX.customer_division dv in listDivision)
                    {
                        ComboboxItem _cmbItem = new ComboboxItem()
                        {
                            Text = dv.division_name,
                            Value = dv.division_id
                        };
                        cmbSourceDivision.Items.Add(_cmbItem);                        
                    }
                }
            }
            catch (Exception ex)
            {
                General.LogExceptionWithShowError(ex, "unable to load division");
            }
            
        }


        private void Search()
        {
            try
            {
                General.ClearGrid(dgSalemerging);
                SaleMergingBAL saleMergingBAL = new SaleMergingBAL();
                int customerId = General.GetComboBoxSelectedValue(cmbSourceCustomer);
                int divisionId = General.GetComboBoxSelectedValue(cmbSourceDivision);
                DateTime fromDate = General.ConvertDateServerFormatWithStartTime(dtpFromDate.Value);
                DateTime toDate = General.ConvertDateServerFormatWithEndTime(dtpToDate.Value);
                List<EDMX.sales> listSales = saleMergingBAL.GetAllSales(customerId, divisionId, fromDate, toDate);
                if(listSales != null)
                {
                    foreach(EDMX.sales sales in listSales)
                    {
                        dgSalemerging.Rows.Add(sales.sales_id,sales.sales_number, sales.sales_date, sales.net_amount,sales.delivery_leaf,"View","Remove");
                    }
                    dgSalemerging.Rows.Add("","", "Total", listSales.Sum(x=>x.net_amount),"","");
                    General.GridBackcolorYellow(dgSalemerging);
                    General.GridRownumber(dgSalemerging);
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }

        private void Merge()
        {
            try
            {
                int customerId = General.GetComboBoxSelectedValue(cmbDestinationCustomer);
                int divisionId = General.GetComboBoxSelectedValue(cmbDestinationDivision);
                if (dgSalemerging.Rows.Count > 0)
                {
                    if (customerId > 0)
                    {
                        if (General.ShowMessageConfirm($"Are you sure want to transfer the listed sale to {cmbDestinationCustomer.Text}") == DialogResult.Yes)
                        {
                            SaleMergingBAL saleMergingBAL = new SaleMergingBAL();
                            List<int> listSales = new List<int>();

                            foreach (DataGridViewRow row in dgSalemerging.Rows)
                            {
                                if (row.Cells["clmSaleid"].Value != null && !string.IsNullOrEmpty(row.Cells["clmSaleid"].Value.ToString()))
                                {
                                    int saleId = Convert.ToInt32(row.Cells["clmSaleid"].Value);
                                    listSales.Add(saleId);
                                }
                            }
                            if (customerId > 0)
                            {
                                int resp = saleMergingBAL.SalesMerge(listSales, customerId, divisionId);
                                General.ShowMessage(General.EnumMessageTypes.Success, $"{resp} Sale Merging Succesfully Saved", "Saved");
                                if (resp > 0)
                                    Search();
                            }
                        }
                    }
                }
                else
                {
                    General.ShowMessage(General.EnumMessageTypes.Warning, "Please select destination customer");
                }
            }
            catch (Exception ex)
            {
                General.LogExceptionWithShowError(ex, "Something went wrong. Unable to update");
            }
            
        }

        private void LoadDestinationDivsion(int customerId)
        {
            try
            {

                BETask.DAL.DAL.CustomerDAL customerDAL = new DAL.DAL.CustomerDAL();
                List<EDMX.customer_division> listDivision = customerDAL.GetCustomerDivision(customerId);
                if (listDivision != null && listDivision.Count > 0)
                {
                    foreach (EDMX.customer_division dv in listDivision)
                    {
                        ComboboxItem _cmbItem = new ComboboxItem()
                        {
                            Text = dv.division_name,
                            Value = dv.division_id
                        };                        
                        cmbDestinationDivision.Items.Add(_cmbItem);
                    }
                }
            }
            catch (Exception ex)
            {
                General.LogExceptionWithShowError(ex, "unable to load division");
            }
        }

        private void NextFocus(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                General.NextFocus(sender, e);
            }
        }

        class SaleMergingButtonCollection
        {
            public Button BtnLoad { get; set; }
            public Button BtnCancel { get; set; }
            public Button BtnClose { get; set; }
            public Button BtnPrint { get; set; }
            public Button BtnSave { get; set; }

        }

        

        private void cmbSourceCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            int customerId = General.GetComboBoxSelectedValue(cmbSourceCustomer);
            LoadSourceDivsion(customerId);
        }

        private void cmbDestinationDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            int customerId = General.GetComboBoxSelectedValue(cmbDestinationCustomer);
            LoadDestinationDivsion(customerId);
        }

        private void dgSalemerging_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >=0 && e.ColumnIndex>=0)
            {
                try
                {
                    int saleId = dgSalemerging["clmSaleid", e.RowIndex].Value!=null? Convert.ToInt32(dgSalemerging["clmSaleid", e.RowIndex].Value):0;
                    string saleNumber = dgSalemerging["clmSaleNumber", e.RowIndex].Value.ToString();
                    if (saleId > 0)
                    {
                        //Delete
                        if (e.ColumnIndex == dgSalemerging.Columns.Count - 1)
                        {
                            dgSalemerging.Rows.RemoveAt(e.RowIndex);
                        }
                        //View
                        if (e.ColumnIndex == dgSalemerging.Columns.Count - 2)
                        {
                            SaleForm sale = new SaleForm(saleNumber);
                            sale.ShowDialog();
                        }
                    }
                }
                catch (Exception ex)
                {
                    
                }
                Total();
            }
        }

       
    }
}
