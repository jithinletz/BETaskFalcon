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
    public class RouteBAL
    {
        RouteDAL routeDAL = new RouteDAL();
        public void SaveRoute(EDMX.route _route)
        {
            try
            {
                routeDAL.SaveRoute(_route);
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
                    reference_id = 0,
                    summary = $" Saving of Route { _route.route_name}",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
               
            }
        }
        public void SaveRouteGroup(List<EDMX.route_group> _route,string routename)
        {
            try
            {
                List<EDMX.route_group> listRoute= routeDAL.SaveRouteGroup(_route);

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
                    reference_id = _route[0].route_id,
                    summary = $" Saving of  group route { routename}",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
                try
                {
                    SynchronizationBAL sync = new SynchronizationBAL();
                    sync.Route();
                }
                catch (Exception ee)
                {
                    General.Error(ee.ToString());
                }
            }
        }
        public List<EDMX.route> GetAllRoutes(betaskdbEntities context = null)
        {
            
            try
            {

                return routeDAL.GetAllRoutes(context);
               
            }
            catch (Exception ee)
            {
                throw;
            }
          
        }
        public EDMX.route GetRoute(int routeId)
        {

            try
            {

                return routeDAL.GetRoute(routeId);

            }
            catch (Exception ee)
            {
                throw;
            }

        }
    }
}
