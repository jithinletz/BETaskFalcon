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

namespace BETask.Views
{
    public partial class PunchForm : Form
    {
        EmployeeBAL EmployeeBAL = new EmployeeBAL();
        PunchSearchButtonCollection button;
        DataTable tblReport;
        int punchId = 0;
        public enum EnumFormEvents
        {
            FormLoad,
            Cancel,
            Close,
            Search,
            New,
            Save,
            Print
        }
        public PunchForm()
        {
            InitializeComponent();
        }
        private void ButtonActive(Enum activeEvent)
        {

            switch (activeEvent)
            {
                case EnumFormEvents.FormLoad:
                    // Search();
                    break;

                case EnumFormEvents.Close:
                    this.BeginInvoke(new MethodInvoker(Close));
                    break;
                case EnumFormEvents.Search:
                    Search();
                    break;
                case EnumFormEvents.New:
                    New();
                    break;
                case EnumFormEvents.Cancel:
                    Cancel();
                    break;
                case EnumFormEvents.Save:
                    Save();
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

            else if (sender == button.BtnNew)
            {
                ButtonActive(EnumFormEvents.New);
            }
            else if (sender == button.BtnSave)
            {
                ButtonActive(EnumFormEvents.Save);
            }
            else if (sender == button.BtnPrint)
            {
                ButtonActive(EnumFormEvents.Print);
            }
        }

        private void FormLoad()
        {
            button = new PunchSearchButtonCollection()
            {
                BtnSearch = btnSearch,
                BtnClose = btnClose,
                BtnCancel = btnCancel,
                BtnSave = btnSave,
                BtnNew = btnNew,
                BtnPrint=btnPrint
            };
            GetAllEmployees();
            button.BtnNew.Enabled = true;
            button.BtnSave.Enabled = false;
        }
        private void New()
        {
            int employeeId = General.GetComboBoxSelectedValue(cmbEmployee);
            if (employeeId == 0)
            {
                General.ShowMessage(General.EnumMessageTypes.Warning, "Please select employee first");
            }
            else
            {
                if (General.ShowMessageConfirm($"Are you sure want to add attendance for employee {cmbEmployee.Text}", "Confirm") == DialogResult.Yes)
                {
                    button.BtnNew.Enabled = false;
                    button.BtnSave.Enabled = true;
                    cmbEmployee.Text = string.Empty;
                    Reset();
                }
            }
        }
        private void Cancel()
        {
            button.BtnNew.Enabled = true;
            button.BtnSave.Enabled = false;
            Reset();
        }

        private void Reset()
        {
            txtReason.Clear();
            dtpPuncIn.Value = DateTime.Now;
            dtpPunchOut.Value = DateTime.Now;
            punchId = 0;
            btnSave.Text = "Save";
            cmbEmployee.Text = "";

        }

        private void Search()
        {
            try
            {
                int employeeId = General.GetComboBoxSelectedValue(cmbEmployee);
                 tblReport = EmployeeBAL.GetPunchReport(General.ConvertDateServerFormat(dtpFromDate.Value), General.ConvertDateServerFormat(dtpToDate.Value), employeeId);
                if (tblReport != null)
                {
                    gridPunch.DataSource = tblReport;
                    HideColumns();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        private void Save()
        {
            try
            {


                if (!ValidateDate())
                {
                    General.ShowMessage(General.EnumMessageTypes.Warning, "Invalid dates");
                    return;
                }
                int employeeId = General.GetComboBoxSelectedValue(cmbEmployee);

                if (employeeId == 0)
                {
                    General.ShowMessage(General.EnumMessageTypes.Warning, "Select Employee first");
                    return;
                }

                if (btnSave.Text.ToLower() == "update" || btnSave.Text.ToLower() == "save" )
                {
                    if (General.ShowMessageConfirm($"Are you sure want to update attendace for employee {cmbEmployee.Text}", "Confirm") == DialogResult.Yes)
                    {

                        DAL.Model.PunchModel punchModel = new DAL.Model.PunchModel
                        {
                            PunchId = punchId>0?punchId:-1 ,
                            PunchIn = General.ConvertDateTimeServerFormat(dtpPuncIn.Value),
                            PunchOut = General.ConvertDateTimeServerFormat(dtpPunchOut.Value),
                            Lng = "0.00",
                            Lat = "0.00",
                            LocationDetails = "Office",
                            Remarks = txtReason.Text,
                            EmployeeId = employeeId,
                            PunchDate = General.ConvertDateServerFormat(dtpPuncIn.Value),
                            AppDate = DateTime.Now,
                            Status=1
                        };

                        EmployeeBAL.UpdatePunchReport(punchModel);
                        General.ShowMessage(General.EnumMessageTypes.Success, "Updated");
                        Reset();
                        Search();
                    }
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


                if (gridPunch.Rows.Count > 0)
                {
                    EmployeeBAL.PrintPunchReport(dtpFromDate.Value,dtpToDate.Value,tblReport);
                }
                
            }
            catch (Exception ee)
            {

                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }


        private bool ValidateDate()
        {
            return dtpPuncIn.Value.Date == dtpPunchOut.Value.Date;
        }

        private void HideColumns()
        {
            int[] gridColumns = { 0, 1, 9, 10, 11, 12 };
            foreach (int i in gridColumns)
            {
                gridPunch.Columns[i].Visible = false;
                gridPunch.Columns[4].Width = 150;
                gridPunch.Columns[5].Width = 150;
                gridPunch.Columns[6].Width = 50;
                gridPunch.Columns[7].Width = 250;
                gridPunch.Columns[8].Width = 250;
            }

        }

        private void GetAllEmployees()
        {
            try
            {
                BAL.EmployeeBAL employeeBAL = new BAL.EmployeeBAL();
                var _lstEmployee = employeeBAL.GetAllEmployee();
                foreach (var emp in _lstEmployee)
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

        class PunchSearchButtonCollection
        {
            public Button BtnSearch { get; set; }
            public Button BtnCancel { get; set; }
            public Button BtnClose { get; set; }
            public Button BtnSave { get; set; }
            public Button BtnNew { get; set; }
            public Button BtnPrint { get; set; }
        }

        private void PunchForm_Load(object sender, EventArgs e)
        {
            FormLoad();
        }

        private void gridPunch_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                try
                {
                    int row = e.RowIndex;
                    punchId = Convert.ToInt32(gridPunch.Rows[row].Cells[0].Value);
                    cmbEmployee.Text = Convert.ToString(gridPunch.Rows[row].Cells[3].Value);

                    dtpPuncIn.Value = DateTime.Parse(gridPunch.Rows[row].Cells[4].Value.ToString());
                    dtpPunchOut.Value = dtpPuncIn.Value;
                    if (!string.IsNullOrEmpty(gridPunch.Rows[row].Cells[5].Value.ToString()))
                    {
                        dtpPunchOut.Value = DateTime.Parse(gridPunch.Rows[row].Cells[5].Value.ToString());
                    }
                    button.BtnSave.Text = "Update";
                    button.BtnNew.Enabled = false;
                    button.BtnSave.Enabled = true;

                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        }
    }
}
