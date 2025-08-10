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
    public partial class BuildingForm : Form
    {
        public enum EnumFormEvents
        {
            FormLoad,
            New,
            Save,
            Cancel,
            Close,
            Update,
            Search
        }
        ButtonCollectionBuilding button;
        RouteBAL routeBAL = new RouteBAL();
        BuildingBAL buildingBAL = new BuildingBAL();
        List<EDMX.route> listRoute;
        List<EDMX.building> listbuilding;
        int _buildingId = 0;
        bool SortingInProgress;
        string[] selecteditems;
        public BuildingForm()
        {
            InitializeComponent();
        }
        private void ButtonActive(Enum activeEvent)
        {

            switch (activeEvent)
            {
                case EnumFormEvents.FormLoad:
                    
                    pnlSaveContent.Enabled = false;
                    button.BtnNew.Enabled = true;
                    button.BtnSave.Enabled = false;
                    button.BtnClose.Enabled = true;
                    button.BtnCancel.Enabled = false;
                    GetAllBuilding();
                    GetAllRoutes();
                    _buildingId = 0;
                    selecteditems = null;
                    break;

                case EnumFormEvents.Cancel:
                    ButtonActive(EnumFormEvents.FormLoad);
                    _buildingId = 0;
                    btnSave.Text = "&Save";                  
                    btnNew.Text = "&New";
                    General.ClearTextBoxes(this);
                    pnlSaveContent.Enabled = false;
                    selecteditems = null;
                    break;

                case EnumFormEvents.Close:
                    this.BeginInvoke(new MethodInvoker(Close));
                    break;

                case EnumFormEvents.Save:
                    SaveBuilding();
                    break;
                case EnumFormEvents.New:
                    button.BtnNew.Enabled = false;
                    button.BtnSave.Enabled = true;
                    button.BtnClose.Enabled = true;
                    button.BtnCancel.Enabled = true;
                    pnlSaveContent.Enabled = true;
                    selecteditems = null;
                    break;
                case EnumFormEvents.Update:
                    button.BtnNew.Enabled = true;
                    button.BtnCancel.Enabled = true;
                    btnNew.Text = "&Edit";
                    btnSave.Text = "&Update";
                    break;
                case EnumFormEvents.Search:                   
                    ButtonActive(EnumFormEvents.Cancel);
                    break;
                default:
                    break;

            }
        }
        private void ButtonEvents(object sender, EventArgs e)
        {
            if (sender == button.BtnNew)
            {
                ButtonActive(EnumFormEvents.New);
            }
            else if (sender == button.BtnCancel)
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
            else if (sender == button.BtnSearch)
            {
                ButtonActive(EnumFormEvents.Search);
            }
        }
        
        private void SaveBuilding()
        {
            try
            {
                if (txtBuilding.Text != string.Empty)
                {
                    if (General.ShowMessageConfirm() == DialogResult.Yes)
                    {
                        string route = string.Empty;
                        CheckedListBox.CheckedItemCollection coll = clbRoute.CheckedItems;
                        string[] strRoute = new string[coll.Count];
                        coll.CopyTo(strRoute, 0);
                        for (int i=0;i< coll.Count;i++)
                        {
                            route =$"{ route }{ strRoute[i]}";
                            if(i!= coll.Count - 1) { route = $"{ route},"; }
                        }

                        EDMX.building building = new EDMX.building
                        {
                            building_id = _buildingId,
                            building_name = txtBuilding.Text,
                            area = txtArea.Text,
                            route = route,
                            status = 1
                        };
                        buildingBAL.SaveBuilding(building);
                        General.Action($"Building Saved {txtBuilding.Text}");
                        General.ShowMessage(General.EnumMessageTypes.Success, "Building Successfully Saved");
                        ButtonActive(EnumFormEvents.Cancel);
                        GetAllBuilding();
                    }
                }
                else
                {
                    General.ShowMessage(General.EnumMessageTypes.Warning, "Please enter Building Name");
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }

        private void DeactivateBuilding(int idx,int status=1)
        {
            try
            {
                string mgs = "deactivate";
                if (status == 1) { mgs = "activate"; }
                if (General.ShowMessageConfirm($"Are you sure to {mgs}") == DialogResult.Yes)
                {
                    if (status == 1) { mgs = "activated"; } else { mgs = "deactivated"; }
                    EDMX.building building = new EDMX.building
                    {
                        building_id = Convert.ToInt32(dgBuilding[0, idx].Value),
                        building_name = dgBuilding[1, idx].Value.ToString(),
                        status = status
                    };
                    buildingBAL.SaveBuilding(building);
                    General.Action($"Building {txtBuilding.Text}  {mgs} ");
                    General.ShowMessage(General.EnumMessageTypes.Success, $"Building Successfully { mgs}");
                    ButtonActive(EnumFormEvents.Cancel);                   
                }

            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
        private void DeleteBuilding(int idx)
        {
            try
            {

                if (General.ShowMessageConfirm("Are you sure to delete ") == DialogResult.Yes)
                {

                    EDMX.building building = new EDMX.building
                    {
                        building_id = Convert.ToInt32(dgBuilding[0, idx].Value),
                        building_name = dgBuilding[1, idx].Value.ToString(),
                        status = 5
                    };
                    buildingBAL.SaveBuilding(building);
                    General.Action($"Building deleted {txtBuilding.Text}");
                    General.ShowMessage(General.EnumMessageTypes.Success, "Building Successfully deleted");
                    ButtonActive(EnumFormEvents.Cancel);                    
                }

            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
        private void GetAllBuilding()
        {

            try
            {
                string route = string.Empty;
                int count = 0;
                General.ClearGrid(dgBuilding);
                listbuilding = buildingBAL.GetAllBuildings(txtSearchBuiding.Text);
                foreach (EDMX.building obj in listbuilding.OrderBy(x=> x.status))
                {
                    if (obj.status == 1)
                    {
                        dgBuilding.Rows.Add(obj.building_id, obj.building_name, obj.route, obj.area, "Deactive", "Delete");
                    }else
                    {
                        dgBuilding.Rows.Add(obj.building_id, obj.building_name, obj.route, obj.area, "Active", "Delete");
                        dgBuilding.Rows[count].DefaultCellStyle.BackColor = Color.DimGray;
                    }
                    count++;
                }
                General.GridRownumber(dgBuilding);
                lblResultCount.Text = $"Total {dgBuilding.Rows.Count.ToString()} Buildings";
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
           
        }
        private void GetAllRoutes(string[] selectedRoute=null)
        {
            try
            {
               clbRoute.Items.Clear();
                bool isChecked = false;
               listRoute = routeBAL.GetAllRoutes();
                foreach (EDMX.route route in listRoute)
                {
                    isChecked = false;
                    if (selectedRoute != null && selectedRoute.Count()>0)
                    {
                        for (int i = 0; i < selectedRoute.Count(); i++)
                        {
                            if (selectedRoute[i].ToUpper() == route.route_name.ToUpper())
                                isChecked = true;                            
                        }
                    }
                    clbRoute.Items.Add(route.route_name, isChecked);
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
            
        }
        private void GetBuildingData(int _buildingId)
        {
            try
            {
                EDMX.building building = buildingBAL.GetBuilding(_buildingId);
                General.ClearTextBoxes(this);
                string strRoute = string.Empty;
                selecteditems = null;
                if (building != null)
                {
                    _buildingId = building.building_id;
                    txtBuilding.Text = building.building_name;
                    txtArea.Text = building.area;
                    strRoute = building.route;
                    if (!string.IsNullOrEmpty(strRoute))
                        selecteditems = strRoute.Split(',');
                    GetAllRoutes(selecteditems);
                    ListSort();
                    ButtonActive(EnumFormEvents.Update);
                }
                else
                {
                    General.ShowMessage(General.EnumMessageTypes.Warning, $" You can't edit deactived building details. Activate building first");
                    ButtonActive(EnumFormEvents.Cancel);
                }
               
            }
            catch (Exception ee)
            {
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
        private void ListSort()
        {
             SortingInProgress=true;
            List<object> checkedItems = new List<object>(clbRoute.CheckedItems.OfType<object>());
            List<object> uncheckedItems = new List<object>(clbRoute.Items.OfType<object>().Except(checkedItems));

            checkedItems.Sort();
            uncheckedItems.Sort();

            int item = 0;

            for (int checkd = 0; checkd < checkedItems.Count; checkd++, item++)
            {
                clbRoute.Items[item] = checkedItems[checkd];
                clbRoute.SetItemChecked(item, true);
            
            }

            for (int uncheckd = 0; uncheckd < uncheckedItems.Count; uncheckd++, item++)
            {
                clbRoute.Items[item] = uncheckedItems[uncheckd];
                clbRoute.SetItemChecked(item, false);
            }
            SortingInProgress = false;

        }
        private void BuildingForm_Load(object sender, EventArgs e)
        {

            button = new ButtonCollectionBuilding
            {
                BtnNew = btnNew,
                BtnCancel = btnCancel,
                BtnClose = btnClose,
                BtnSave = btnSave,
               // BtnSearch = btnSearch
            };
            ButtonActive(EnumFormEvents.FormLoad);
           
        }

        private void txtSearchBuiding_TextChanged(object sender, EventArgs e)
        {
            GetAllBuilding();
        }

        private void dgBuilding_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (dgBuilding.CurrentRow.Index >= 0 && dgBuilding.CurrentCell.ColumnIndex >= 0)
                {                    
                    _buildingId = Convert.ToInt32(dgBuilding["ClmBuildingId", dgBuilding.CurrentRow.Index].Value);
                    GetBuildingData(_buildingId);
                    ButtonActive(EnumFormEvents.Update);
                   
                }
            }
        }

        private void clbRoute_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            //if (!SortingInProgress)
            //{ ListSort(); }
        }

        private void dgBuilding_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgBuilding.CurrentRow.Index >= 0 && dgBuilding.CurrentCell.ColumnIndex >= 0)
            {
                if (dgBuilding[e.ColumnIndex, e.RowIndex].Value!=null)
                {
                    if (dgBuilding[e.ColumnIndex, e.RowIndex].Value.ToString().ToUpper() == "DEACTIVE")
                    {
                        DeactivateBuilding(e.RowIndex,2);

                    }
                    if (dgBuilding[e.ColumnIndex, e.RowIndex].Value.ToString().ToUpper() == "ACTIVE")
                    {
                        DeactivateBuilding(e.RowIndex, 1);

                    }
                    else if (dgBuilding[e.ColumnIndex, e.RowIndex].Value.ToString().ToUpper() == "DELETE")
                    {
                        DeleteBuilding(e.RowIndex);
                    }               
                    else
                    {
                        _buildingId = Convert.ToInt32(dgBuilding["ClmBuildingId", dgBuilding.CurrentRow.Index].Value);
                        GetBuildingData(_buildingId);                       
                    }
                }
                else
                {
                    _buildingId = Convert.ToInt32(dgBuilding["ClmBuildingId", dgBuilding.CurrentRow.Index].Value);
                    GetBuildingData(_buildingId);
                }
            }
        }
    }

    class ButtonCollectionBuilding
    {
        public Button BtnNew { get; set; }
        public Button BtnCancel { get; set; }
        public Button BtnClose { get; set; }
        public Button BtnSave { get; set; }
        public Button BtnSearch { get; set; }
    }
}
