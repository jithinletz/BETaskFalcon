using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BETask.DAL.EDMX;
using System.Data.Entity;
using System.Data;

namespace BETask.DAL.DAL
{
  public  class RouteDAL
    {
        public void SaveRoute(route _route)
        {
            try
            {
                using (var context = new betaskdbEntities())
                {
                    context.Entry(_route).State = _route.route_id == 0 ? EntityState.Added : EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        public List<route_group> SaveRouteGroup(List<route_group> _route)
        {
            try
            {
                using (var context = new betaskdbEntities())
                {
                    int routeId = _route[0].route_id;
                    foreach (route_group rt in _route)
                    {
                        context.route_group.RemoveRange(context.route_group.Where(x => x.route_id == routeId).ToList());
                        context.SaveChanges();
                        context.route_group.AddRange(_route);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return _route;
        }
        public List<route_group> GetGroupRoute(int routeId)
        {
            try
            {
                using (var context = new betaskdbEntities())
                {
                    return context.route_group.AsNoTracking().Where(x => x.route_id == routeId).ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<route> GetAllRoutes(betaskdbEntities _context=null)
        {
            List<route> listRoute = new List<route>();
            try
            {
                if (_context != null)
                    listRoute = _context.route.AsNoTracking().Where(x => x.status == 1).OrderBy(x => x.route_name).ToList();
                else
                {
                    using (var context = new betaskdbEntities())
                    {
                        listRoute = context.route.Where(x => x.status == 1).OrderBy(x => x.route_name).ToList();
                    }
                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return listRoute;
        }
        public route GetRoute(int routeId)
        {
            route _route = new route();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    _route = context.route.Where(x => x.status == 1 && x.route_id==routeId).FirstOrDefault();
                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return _route;
        }
    }
}
