using BETask.Common;
using BETask.BAL;
using BETask.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using EDMX = BETask.DAL.EDMX;
using System.Data;

namespace BETask.Views
{
    public partial class DamageReportForm : Form
    {
        DamageReportButtonCollection button;
        public enum EnumFormEvents
        {
            FormLoad,
            Cancel,
            Close,
            Search,
            Print
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
                    cmbEmployee.Text = string.Empty;
                    break;
                case EnumFormEvents.Close:
                    this.BeginInvoke(new MethodInvoker(Close));
                    break;
                case EnumFormEvents.Search:
                    Search();
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
        public DamageReportForm()
        {
            InitializeComponent();
        }
        private void Search()
        {
            try
            {
                General.ClearGrid(gridItems);
                int employeeId = 0;
                if (!String.IsNullOrEmpty(cmbEmployee.Text))
                {
                    Object selectedEmployee = cmbEmployee.SelectedItem;
                    employeeId = (int)((BETask.Views.ComboboxItem)selectedEmployee).Value;
                }
                ProductionBAL productMapBAL = new ProductionBAL();
                List<EDMX.item_damage> listItem = productMapBAL.GetItemDamageReport(General.ConvertDateServerFormat(dtpDateFrom.Value), General.ConvertDateServerFormat(dtpDateTo.Value),employeeId);
                if (listItem != null && listItem.Count > 0)
                {

                    foreach (EDMX.item_damage raw in listItem)
                    {
                        gridItems.Rows.Add(General.ConvertDateAppFormat(raw.damage_date),$"{raw.employee.first_name} {raw.employee.last_name}", raw.item.item_name, raw.qty,raw.remarks);
                    }
                    General.GridRownumber(gridItems);
                    lblQty.Text = listItem.Sum(x => x.qty).ToString();
                }
                else
                {
                    // ButtonActive(EnumFormEvents.Other);
                }
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
               
                int employeeId = 0;
                if (!String.IsNullOrEmpty(cmbEmployee.Text))
                {
                    Object selectedEmployee = cmbEmployee.SelectedItem;
                    employeeId = (int)((BETask.Views.ComboboxItem)selectedEmployee).Value;
                }
                string header = $"{General.companyName} Date between {dtpDateFrom.Text} and {dtpDateTo.Text} {cmbEmployee.Text}";
                ProductionBAL productMapBAL = new ProductionBAL();
                productMapBAL.PrintDamageReport(General.ConvertDateServerFormat(dtpDateFrom.Value), General.ConvertDateServerFormat(dtpDateTo.Value), employeeId, header);
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
        private void GetAllEmployees()
        {
            try
            {
                BAL.EmployeeBAL employeeBAL = new BAL.EmployeeBAL();
                List<EDMX.employee> _lstEmployee = employeeBAL.GetAllEmployee();
                foreach (EDMX.employee emp in _lstEmployee)
                {
                    ComboboxItem _cmbItem = new ComboboxItem()
                    {
                        Text = String.Format("{0} {1}", emp.first_name, emp.last_name),
                        Value = emp.employee_id
                    };
                    cmbEmployee.Items.Add(_cmbItem);
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
        private void FormLoad()
        {
            button = new DamageReportButtonCollection()
            {
                BtnSearch = btnSearch,
                BtnCancel = btnCancel,
                BtnClose = btnClose,
                BtnPrint = btnPrint
            };
            GetAllEmployees();
        }

        private void DamageReportForm_Load(object sender, EventArgs e)
        {
            FormLoad();
        }
    }
    class DamageReportButtonCollection
    {
        public Button BtnSearch { get; set; }
        public Button BtnCancel { get; set; }
        public Button BtnClose { get; set; }
        public Button BtnPrint { get; set; }

    }
}
