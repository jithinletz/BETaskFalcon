using BETask.DAL.DAL;
using BETask.Model;
using EDMX = BETask.DAL.EDMX;
using System;
using System.Collections.Generic;
using System.Data;
using RPT = BETask.Report.ReportForm;
using BETask.Common;
using System.Diagnostics;
using System.Linq;
using BETask.DAL.EDMX;

namespace BETask.BAL
{
    public class BuildingBAL
    {
        BuildingDAL BuildingDAL = new BuildingDAL();
        public void SaveBuilding(EDMX.building _building)
        {
            try
            {
                BuildingDAL.SaveBuilding(_building);
            }
            catch (Exception ee)
            {
                throw;
            }
            finally
            {
                var st = new StackTrace();
                var sf = st.GetFrame(1);
                EDMX.user_log user_Log = new EDMX.user_log()
                {
                    username = General.userName,
                    module = this.GetType().Name,
                    module_action = sf.GetMethod().Name,
                    reference_id = _building.building_id,
                    summary = $" Saving of Building { _building.building_name}",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
               
            }
        }
        public List<EDMX.building> GetAllBuildings(betaskdbEntities context = null)
        {

            try
            {

                return BuildingDAL.GetAllBuildings(context);

            }
            catch (Exception ee)
            {
                throw;
            }

        }
        public EDMX.building GetBuilding(int _buildingId)
        {

            try
            {

                return BuildingDAL.GetBuilding(_buildingId);

            }
            catch (Exception ee)
            {
                throw;
            }

        }
        public List<EDMX.building> GetAllBuildings(string searchValue )
        {

            try
            {

                return BuildingDAL.GetAllBuildings(searchValue);

            }
            catch (Exception ee)
            {
                throw;
            }

        }
    }
}