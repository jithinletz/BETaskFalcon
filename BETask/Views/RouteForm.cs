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
    public partial class RouteForm : Form
    {
        public enum EnumFormEvents
        {
            FormLoad,
            New,
            Save,
            Cancel,
            Close,
            GroupClose,
            GroupSave

        }
        ButtonCollectionRoute button;
        RouteBAL routeBAL = new RouteBAL();
        BuildingBAL buildingBAL = new BuildingBAL();
        List<EDMX.route> listRoute;
        List<EDMX.building> listbuilding;
        int routeId = 0;

        public RouteForm()
        {
            InitializeComponent();
        }
        private void ButtonActive(Enum activeEvent)
        {

            switch (activeEvent)
            {
                case EnumFormEvents.FormLoad:
                    GetAllRoutes();
                    button.BtnClose.Enabled = true;
                    button.BtnCancel.Enabled = true;
                    break;

                case EnumFormEvents.Cancel:
                    ButtonActive(EnumFormEvents.FormLoad);
                    routeId = 0;
                    btnSave.Text = "&Save";
                    General.ClearTextBoxes(this);
                    break;

                case EnumFormEvents.Close:
                    this.BeginInvoke(new MethodInvoker(Close));
                    break;

                case EnumFormEvents.Save:
                    if (rbRoute.Checked) { SaveRoute(); }
                    else { SaveBuilding(); }
                    break;
                case EnumFormEvents.GroupClose:
                    pnlGroupRoute.Hide();
                    break;
                case EnumFormEvents.GroupSave:
                    SaveGroupRoute();
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
            else if (sender == button.BtnSubClose)
            {
                ButtonActive(EnumFormEvents.GroupClose);
            }
            else if (sender == button.BtnSubSave)
            {
                ButtonActive(EnumFormEvents.GroupSave);
            }
        }
        private void NextFocus(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {

                if (sender == txtRouteName)
                {
                    btnSave.Focus();
                }

            }
        }

        private void SaveBuilding()
        {
            try
            {
                if (txtRouteName.Text != string.Empty)
                {
                    if (General.ShowMessageConfirm() == DialogResult.Yes)
                    {

                        EDMX.building building = new EDMX.building
                        {
                            building_id = this.routeId,
                            building_name = txtRouteName.Text,
                            status = 1
                        };
                        buildingBAL.SaveBuilding(building);
                        General.Action($"Building Saved {txtRouteName.Text}");
                        General.ShowMessage(General.EnumMessageTypes.Success, "Building Successfully Saved");
                        ButtonActive(EnumFormEvents.Cancel);
                        GetAllBuilding();
                    }
                }
                else
                {
                    General.ShowMessage(General.EnumMessageTypes.Warning, "Please enter Route");
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }

        private void DeactivateBuilding(int idx)
        {
            try
            {

                if (General.ShowMessageConfirm() == DialogResult.Yes)
                {

                    EDMX.building building = new EDMX.building
                    {
                        building_id = Convert.ToInt32(gridRoutes[0, idx].Value),
                        building_name = gridRoutes[1, idx].Value.ToString(),
                        status = 2
                    };
                    buildingBAL.SaveBuilding(building);
                    General.Action($"Building de activated {txtRouteName.Text}");
                    General.ShowMessage(General.EnumMessageTypes.Success, "Building Successfully de activated");
                    ButtonActive(EnumFormEvents.Cancel);
                    GetAllRoutes();
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
                General.ClearGrid(gridRoutes);
                if (txtRouteName.Text.Length == 0)
                {
                    routeId = 0;
                    btnSave.Text = "&Save";
                    listbuilding = buildingBAL.GetAllBuildings();

                }
                else
                {
                    listbuilding = listbuilding.Where(x => x.building_name.ToLower().Contains(txtRouteName.Text.ToLower())).ToList();
                }
                foreach (EDMX.building building in listbuilding)
                {

                    gridRoutes.Rows.Add(building.building_id, building.building_name, "De activate");

                }
                General.GridRownumber(gridRoutes);
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
            rbBuilding.Checked = true;
        }

        private void SaveRoute()
        {
            try
            {
                if (txtRouteName.Text != string.Empty)
                {
                    if (General.ShowMessageConfirm() == DialogResult.Yes)
                    {

                        EDMX.route route = new EDMX.route
                        {
                            route_id = this.routeId,
                            route_name = txtRouteName.Text,
                            status = 1
                        };
                        routeBAL.SaveRoute(route);
                        General.Action($"Route Saved {txtRouteName.Text}");
                        General.ShowMessage(General.EnumMessageTypes.Success, "Route Successfully Saved");
                        ButtonActive(EnumFormEvents.Cancel);
                        GetAllRoutes();
                    }
                }
                else
                {
                    General.ShowMessage(General.EnumMessageTypes.Warning, "Please enter Route");
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
        private void DeactivateRoute(int idx)
        {
            try
            {

                if (General.ShowMessageConfirm() == DialogResult.Yes)
                {

                    EDMX.route route = new EDMX.route
                    {
                        route_id = Convert.ToInt32(gridRoutes[0, idx].Value),
                        route_name = gridRoutes[1, idx].Value.ToString(),
                        status = 2
                    };
                    routeBAL.SaveRoute(route);
                    General.Action($"Route de activated {txtRouteName.Text}");
                    General.ShowMessage(General.EnumMessageTypes.Success, "Route Successfully de activated");
                    ButtonActive(EnumFormEvents.Cancel);
                    GetAllRoutes();
                }

            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
        private void GetAllRoutes()
        {
            try
            {
                General.ClearGrid(gridRoutes);
                if (txtRouteName.Text.Length == 0)
                {
                    routeId = 0;
                    btnSave.Text = "&Save";
                    listRoute = routeBAL.GetAllRoutes();

                }
                else
                {
                    listRoute = listRoute.Where(x => x.route_name.ToLower().Contains(txtRouteName.Text.ToLower())).ToList();
                }
                foreach (EDMX.route route in listRoute)
                {
                    gridRoutes.Rows.Add(route.route_id, route.route_name, "De activate");
                }
                General.GridRownumber(gridRoutes);
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
            rbRoute.Checked = true;
        }
        private void GetAllRoutesSub()
        {
            try
            {
                DAL.DAL.RouteDAL routeDAL = new DAL.DAL.RouteDAL();
                List<EDMX.route_group> listXRoutes = routeDAL.GetGroupRoute(this.routeId);
                General.ClearGrid(gridSubRoute);

                List<EDMX.route> _listRoute = routeBAL.GetAllRoutes();

                foreach (EDMX.route route in _listRoute)
                {
                    bool xAdded = false;
                    if (listXRoutes.Any(x => x.sub_route_id == route.route_id))
                        xAdded = true;
                    gridSubRoute.Rows.Add(xAdded, route.route_id, route.route_name);
                }
                General.GridRownumber(gridSubRoute);
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
            rbRoute.Checked = true;
        }
        private void RouteForm_Load(object sender, EventArgs e)
        {

            button = new ButtonCollectionRoute
            {

                BtnCancel = btnCancel,
                BtnClose = btnClose,
                BtnSave = btnSave,
                BtnSubClose=btnGroupClose,
                BtnSubSave=btnGroupSave
            };
            ButtonActive(EnumFormEvents.FormLoad);
            rbRoute.Checked = true;
        }

        private void txtRouteName_TextChanged(object sender, EventArgs e)
        {
            if (rbRoute.Checked)
                GetAllRoutes();
            else GetAllBuilding();
        }

        private void gridRoutes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 1)
            {
                if (gridRoutes["clmRouteId", e.RowIndex].Value != null)
                {
                    routeId = General.ParseInt(gridRoutes["clmRouteId", e.RowIndex].Value.ToString());
                    btnSave.Text = "&Update";
                    txtRouteName.Text = gridRoutes["clmRouteName", e.RowIndex].Value.ToString();
                }
            }
            else if (e.RowIndex >= 0 && e.ColumnIndex == 2)
            {
                DeactivateRoute(e.RowIndex);
            }
        }

        private void rbBuilding_Click(object sender, EventArgs e)
        {
            GetAllBuilding();
        }

        private void rbRoute_Click(object sender, EventArgs e)
        {
            GetAllRoutes();
        }

        private void linkGroupRoute_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (pnlGroupRoute.Visible)
                pnlGroupRoute.Hide();
            else
            {
                if (this.routeId > 0)
                {
                    pnlGroupRoute.Show();
                    GetAllRoutesSub();
                }
            }
        }
        private void SaveGroupRoute()
        {
            try
            {
                if (this.routeId > 0 && gridSubRoute.Rows.Count > 0)
                {
                   
                    List<EDMX.route_group> group = new List<EDMX.route_group> { };
                    foreach (DataGridViewRow row in gridSubRoute.Rows)
                    {
                        if (row.Cells["clmCheckSubRoute"].Value.Equals(true))
                        {
                            group.Add(new EDMX.route_group
                            {
                                route_id = this.routeId,
                                sub_route_id = Convert.ToInt32(row.Cells["clmSubRouteId"].Value),
                                Status = 1,

                            });


                        }
                    }
                    routeBAL.SaveRouteGroup(group, txtRouteName.Text);
                    General.Action($"Group route saved {txtRouteName.Text}");
                    General.ShowMessage(General.EnumMessageTypes.Success, "Routes successfully added to group route");
                    pnlGroupRoute.Hide();
                    ButtonActive(EnumFormEvents.Cancel);
                    GetAllRoutes();
                }
            }
            catch (Exception ex)
            {
                General.LogExceptionWithShowError(ex, "Unable to save , something went wrong");
            }


        }
    }
    class ButtonCollectionRoute
    {
       
        public Button BtnCancel { get; set; }
        public Button BtnClose { get; set; }
        public Button BtnSave { get; set; }
        public Button BtnSubClose { get; set; }
        public Button BtnSubSave { get; set; }
    }
}
