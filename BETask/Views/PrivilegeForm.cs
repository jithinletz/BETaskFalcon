using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BETask.Common;
using BETask.BAL;
using BETask.Model;
using EDMX = BETask.DAL.EDMX;

namespace BETask.Views
{
    public partial class PrivilegeForm : Form
    {
        public enum EnumFormEvents
        {
            FormLoad,
            New,
            Save,
            Cancel,
            Close,

        }
        BAL.PrivilegeBAL privilegeBAL = new PrivilegeBAL();
        ButtonCollectionPrivilege button;
        RouteBAL routeBAL = new RouteBAL();
        int rowIntex = -1;

        int routeId = 0;
        public PrivilegeForm()
        {
            InitializeComponent();
        }
        private void ButtonActive(Enum activeEvent)
        {

            switch (activeEvent)
            {
                case EnumFormEvents.FormLoad:

                    button.BtnClose.Enabled = true;
                    button.BtnCancel.Enabled = true;
                    break;

                case EnumFormEvents.Cancel:
                    ButtonActive(EnumFormEvents.FormLoad);
                    routeId = 0;
                    //btnSave.Text = "&Save";
                    General.ClearTextBoxes(this);
                    break;

                case EnumFormEvents.Close:
                    this.BeginInvoke(new MethodInvoker(Close));
                    break;

                case EnumFormEvents.Save:
                    //SavePrivilege();
                    break;
                default:
                    break;

            }
        }
        private void ButtonEvents(object sender, EventArgs e)
        {
            if (sender == button.BtnCancel)
            {
                ButtonActive(EnumFormEvents.Cancel);
            }
            else if (sender == button.BtnSave)
            {
                ButtonActive(EnumFormEvents.Save);
            }
            else if (sender == button.BtnClose)
            {
                ButtonActive(EnumFormEvents.Close);
            }
        }
        private void NextFocus(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {

                if (sender == cmbEmployee)
                {
                    //btnSave.Focus();
                }

            }
        }

        private void GetPrivilegeTypes()
        {
            try
            {
                cmbPrivilegeType.Items.Clear();
                List<string> listPivilegeTypes = privilegeBAL.GetPrivilegeTypes();
                if (listPivilegeTypes != null && listPivilegeTypes.Count > 0)
                {
                    foreach (string privilege in listPivilegeTypes)
                    {
                        ComboboxItem _cmbItem = new ComboboxItem()
                        {
                            Text = privilege,
                            Value = privilege
                        };
                        cmbPrivilegeType.Items.Add(_cmbItem);
                        cmbPrivilegeType.SelectedIndex = -1;
                    }
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
        private void GetAllPrivilege()
        {
            try
            {
                int empId = 0;
                if (cmbEmployee.SelectedItem != null)
                {
                    Object selectedEmployee = cmbEmployee.SelectedItem;
                    empId = (int)((BETask.Views.ComboboxItem)selectedEmployee).Value;
                }
                if (cmbEmployee.Text != string.Empty)
                {
                    //Getting Saved Privelges
                    List<EDMX.privileges> listActivePrivileges = privilegeBAL.GetActivePrivileges(empId);

                    General.ClearGrid(gridPrivileges);
                    List<EDMX.privilege_menu> listPivilege = privilegeBAL.GetAllPrivilege(cmbPrivilegeType.Text);
                    if (listPivilege != null && listPivilege.Count > 0)
                    {
                        foreach (EDMX.privilege_menu privilege in listPivilege)
                        {
                            if (listActivePrivileges != null)
                            {
                                var xMenu = listActivePrivileges.Where(x => x.menu_id == privilege.menu_id).FirstOrDefault();
                                if (xMenu != null)
                                {
                                    gridPrivileges.Rows.Add(xMenu.privilege_id, privilege.menu_id, "Revoke", privilege.menu_name);
                                    gridPrivileges.Rows[gridPrivileges.Rows.Count-1].DefaultCellStyle.BackColor = Color.Green;
                                }
                                else
                                {
                                    gridPrivileges.Rows.Add(0, privilege.menu_id, "Grand", privilege.menu_name);
                                }
                            }
                            else
                                gridPrivileges.Rows.Add(0, privilege.menu_id, "Grand", privilege.menu_name);
                        }
                    }
                }
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
                List<EDMX.employee> _lstEmployee = null;
                _lstEmployee = employeeBAL.GetAllEmployee();
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


        private void SavePrivilege(EDMX.privileges privileges)
        {
            try
            {
                if (cmbEmployee.Text != string.Empty)
                {
                    // if (General.ShowMessageConfirm() == DialogResult.Yes)
                    {

                        privilegeBAL.UpdatePrivilege(privileges);
                        // General.ShowMessage(General.EnumMessageTypes.Success, "Privilege Successfully Saved");
                     //   ButtonActive(EnumFormEvents.Cancel);

                    }
                }
                else
                {
                    General.ShowMessage(General.EnumMessageTypes.Warning, "Please Select Employee");
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }

        private void RouteForm_Load(object sender, EventArgs e)
        {

            button = new ButtonCollectionPrivilege
            {

                BtnCancel = btnCancel,
                BtnClose = btnClose,
                //BtnSave = btnSave
            };
            ButtonActive(EnumFormEvents.FormLoad);
            GetAllEmployees();
            GetPrivilegeTypes();
        }




        private void cmbPrivilegeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetAllPrivilege();
        }

        private void gridPrivileges_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 2)
            {
                if (gridPrivileges["clmStatus", e.RowIndex].Value != null)
                {
                    int menuId = General.ParseInt(gridPrivileges["clmMenuId", e.RowIndex].Value.ToString());
                    int privilegeId = General.ParseInt(gridPrivileges["clmPrivilegeId", e.RowIndex].Value.ToString());
                    int empId = 0;
                    if (cmbEmployee.SelectedItem != null)
                    {
                        Object selectedEmployee = cmbEmployee.SelectedItem;
                        empId = (int)((BETask.Views.ComboboxItem)selectedEmployee).Value;
                    }
                    EDMX.privileges privileges = new EDMX.privileges
                    {
                        menu_id = menuId,
                        privilege_id = privilegeId,
                        user_id = empId,
                        status = 1
                    };
                    SavePrivilege(privileges);
                }
                if (gridPrivileges.Rows[e.RowIndex].DefaultCellStyle.BackColor != Color.Green)
                {
                    gridPrivileges.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Green;
                    gridPrivileges["clmStatus", e.RowIndex].Value = "Revoke";
                }
                else
                {
                    gridPrivileges.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                    gridPrivileges["clmStatus", e.RowIndex].Value = "Grant";
                }
            }
        }
    }
    class ButtonCollectionPrivilege
    {
       
        public Button BtnCancel { get; set; }
        public Button BtnClose { get; set; }
        public Button BtnSave { get; set; }
    }
}
