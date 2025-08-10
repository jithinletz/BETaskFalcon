using BETask.Common;
using BETask.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BETask.BAL;
using EDMX = BETask.DAL.EDMX;

namespace BETask.Views
{
    public partial class DeliveryIDGeneratorForm : Form
    {
        public enum EnumFormEvents
        {
            FormLoad,
            New,
            Save,
            Cancel,
            Close,
            Print,

        }
        DeliveryIDGeneratorButtonCollection button;

        public DeliveryIDGeneratorForm()
        {
            InitializeComponent();
        }
        private void ButtonActive(Enum activeEvent)
        {

            switch (activeEvent)
            {
                case EnumFormEvents.FormLoad:

                    break;
                case EnumFormEvents.Cancel:
                    ButtonActive(EnumFormEvents.FormLoad);
                    General.ClearTextBoxes(this);
                    break;
                case EnumFormEvents.Close:
                    this.BeginInvoke(new MethodInvoker(Close));
                    break;
                case EnumFormEvents.Save:
                    GenerateDeliveryId();
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

            else if (sender == button.BtnClose)
            {
                ButtonActive(EnumFormEvents.Close);
            }
          

        }
        private void LoadEmployee()
        {
            try
            {
                General.ClearGrid(gridRoutes);
                EmployeeBAL employeeBAL = new EmployeeBAL();
                List<EDMX.employee> listEmployee = employeeBAL.GetAllEmployeeRoutewise();
                if (listEmployee != null && listEmployee.Count > 0)
                {
                    foreach (EDMX.employee emp in listEmployee)
                        gridRoutes.Rows.Add(emp.route_id,emp.employee_id,true,$"{emp.first_name} {emp.last_name} - {emp.route.route_name}");
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
        private void GenerateDeliveryId()
        {
            try
            {
                if (General.ShowMessageConfirm() == DialogResult.Yes)
                {
                    List<EDMX.employee> listEmployee = new List<EDMX.employee>();
                    foreach (DataGridViewRow dr in gridRoutes.Rows)
                    {
                        if (Convert.ToBoolean(dr.Cells["clmSelect"].Value))
                        {
                            listEmployee.Add(new EDMX.employee
                            {
                                employee_id = Convert.ToInt32(dr.Cells["clmEmployeeId"].Value),
                                route_id = Convert.ToInt32(dr.Cells["clmRouteId"].Value),
                            });
                        }
                    }
                    if (listEmployee.Count > 0)
                    {
                        BAL.DeliveryBAL deliveryBAL = new DeliveryBAL();
                        deliveryBAL.GenerateDeliveryId(dtpDeliveryDateFrom.Value, dtpDeliveryDateTo.Value, listEmployee);
                        General.ShowMessage(General.EnumMessageTypes.Success, "Delivery ids successfully generated");
                    }
                    Search(General.ConvertDateServerFormat(dtpDeliveryDateFrom.Value), General.ConvertDateServerFormat(dtpDeliveryDateTo.Value));
                }

            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
        private void Search(DateTime dateFrom, DateTime dateTo)
        {
            try
            {
                General.ClearGrid(gridSearh);
                BAL.DeliveryBAL deliveryBAL = new DeliveryBAL();
                List<EDMX.delivery> listDelivery= deliveryBAL.SearchDeliveryIdGenerated(dateFrom,dateTo);
                if (listDelivery != null && listDelivery.Count > 0)
                {
                    foreach (EDMX.delivery del in listDelivery)
                    {
                        gridSearh.Rows.Add(del.delivery_id,General.ConvertDateAppFormat(del.delivery_date),$"{del.employee.first_name} {del.employee.last_name} - {del.route.route_name}",del.customer_count);
                    }
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
            button = new DeliveryIDGeneratorButtonCollection
            {

                BtnCancel = btnCancel,
                BtnClose = btnClose,
                BtnSave = btnSave,
            };
            LoadEmployee();
            Search(General.ConvertDateServerFormat(dtpDeliveryDateFrom.Value), General.ConvertDateServerFormat(dtpDeliveryDateTo.Value.AddDays(7)));
        }

        private void DeliveryIDGeneratorForm_Load(object sender, EventArgs e)
        {
            FormLoad();
        }

        private void dtpDeliveryDateFrom_ValueChanged(object sender, EventArgs e)
        {
            Search(General.ConvertDateServerFormat(dtpDeliveryDateFrom.Value), General.ConvertDateServerFormat(dtpDeliveryDateTo.Value));
        }

        private void gridRoutes_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex != 2)
                e.Cancel = true;
        }
    }
    class DeliveryIDGeneratorButtonCollection
    {
        public Button BtnCancel { get; set; }
        public Button BtnClose { get; set; }
        public Button BtnSave { get; set; }
    }
}
