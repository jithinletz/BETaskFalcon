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

namespace BETask.Views
{
    public partial class LoadingReport : Form
    {
        LoadingButtonCollection button;
        public enum EnumFormEvents

        {
            FormLoad,
            Close,
            Print,
            Show
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
                case EnumFormEvents.Print:
                    Print();
                    break;
                case EnumFormEvents.Show:
                    Search();
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

            else if (sender == button.BtnPrint)
            {
                ButtonActive(EnumFormEvents.Print);
            }
            else if (sender == button.BtnShow)
            {
                ButtonActive(EnumFormEvents.Show);
            }

        }
        public LoadingReport()
        {
            InitializeComponent();
        }

        private void Loading_Load(object sender, EventArgs e)
        {
            FormLoad();
        }

        private void Print()
        {
            try
            {
                int item = General.GetComboBoxSelectedValue(cmbItem);
                int employee = General.GetComboBoxSelectedValue(cmbEmployee);
                string header = $"Date {General.ConvertDateAppFormat(dtpFromDate.Value)} & {General.ConvertDateAppFormat(dtpToDate.Value)}";
                DateTime fromDate = General.ConvertDateServerFormat(dtpFromDate.Value);
                DateTime toDate = General.ConvertDateServerFormat(dtpToDate.Value);
                BAL.LoadingBAL loadingBAL = new LoadingBAL();
                loadingBAL.Print(item, employee, fromDate, toDate, header);

            }
            catch (Exception ex)
            {
                General.LogExceptionWithShowError(ex, "Unable to load report");
            }
        }
        private void GetAllEmployees(int _empId = 0)
        {
            try
            {
                BAL.EmployeeBAL employeeBAL = new BAL.EmployeeBAL();
                cmbEmployee.Items.Clear();
                List<EDMX.employee> _lstEmployee = employeeBAL.GetAllEmployee();
                foreach (EDMX.employee emp in _lstEmployee)
                {
                    string routeName = emp.route_id != null ? $"({emp.route.route_name})" : "";
                    ComboboxItem _cmbItem = new ComboboxItem()
                    {

                        Text = String.Format("{0} {1} {2}", emp.first_name, emp.last_name, routeName),
                        Value = emp.employee_id
                    };
                    cmbEmployee.Items.Add(_cmbItem);
                }
                if (_empId >= 1)
                    cmbEmployee.Text = _lstEmployee.Where(x => x.employee_id == _empId).FirstOrDefault().first_name;
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
        private void LoadItem()
        {
            try
            {
                BAL.ItemBAL itemBAL = new BAL.ItemBAL();
                List<EDMX.item> listItem = itemBAL.GetAllItem_Sellable();
                foreach (EDMX.item item in listItem)
                {

                    ComboboxItem _cmbItem = new ComboboxItem()
                    {
                        Text = item.item_name,
                        Value = item.item_id
                    };
                    cmbItem.Items.Add(_cmbItem);
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
                General.ClearGrid(dgLoading);
                LoadingBAL loadingBAL = new LoadingBAL();
                int item = General.GetComboBoxSelectedValue(cmbItem);
                int employee = General.GetComboBoxSelectedValue(cmbEmployee);
                DateTime fromDate = General.ConvertDateServerFormatWithStartTime(dtpFromDate.Value);
                DateTime toDate = General.ConvertDateServerFormatWithEndTime(dtpToDate.Value);
                List<EDMX.loading> listLoad = loadingBAL.GetAllLoad(item, employee, fromDate, toDate);
                if(listLoad!=null)
                {
                    foreach(EDMX.loading load in listLoad)
                    {
                        dgLoading.Rows.Add(General.ConvertDateAppFormat(load.load_date),$"{load.employee.first_name} {load.employee.last_name}", load.helper, load.item.item_name, load.old_stock, load.empty, load.damage, load.balance, load.new_load, load.total_load, load.@short, load.extra, load.new_stock, load.offload, load.act_stock,load.remarks);
                    
                    }

                    decimal sale = 0;
                    try{ sale=listLoad.Sum(x => decimal.Parse(x.remarks));}catch { }
                    dgLoading.Rows.Add($"({listLoad.Count.ToString()}) rows","","","",listLoad.Sum(w=>w.old_stock), listLoad.Sum(w => w.empty), listLoad.Sum(w => w.damage), listLoad.Sum(w => w.balance), listLoad.Sum(w => w.new_load), listLoad.Sum(w => w.total_load), listLoad.Sum(w => w.@short), listLoad.Sum(w => w.extra), listLoad.Sum(w => w.new_stock), listLoad.Sum(w => w.offload), listLoad.Sum(w => w.act_stock),sale);
                    General.GridBackcolorYellow(dgLoading);
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }

        private void NextFocus(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                General.NextFocus(sender, e);
            }
        }
        private void FormLoad()
        {
            button = new LoadingButtonCollection
            {
                BtnClose = btnClose,
                BtnPrint = btnPrint,
                BtnShow = btnShow
            };
            GetAllEmployees();
            LoadItem();
        }

        class LoadingButtonCollection
        {

            public Button BtnClose { get; set; }
            public Button BtnPrint { get; set; }
            public Button BtnShow { get; set; }

        }
    }
}
