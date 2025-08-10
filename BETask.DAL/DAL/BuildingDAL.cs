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
  public  class BuildingDAL
    {
        public void SaveBuilding(building _Building)
        {
            try
            {
                using (var context = new betaskdbEntities())
                {
                    context.Entry(_Building).State = _Building.building_id == 0 ? EntityState.Added : EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ee)
            {
                throw;
            }
        }

        public List<building> GetAllBuildings(betaskdbEntities _context=null)
        {
            List<building> listBuilding = new List<building>();
            try
            {
                if (_context != null)
                    listBuilding = _context.building.AsNoTracking().Where(x => x.status == 1).OrderBy(x => x.building_name).ToList();
                else
                {
                    using (var context = new betaskdbEntities())
                    {
                        listBuilding = context.building.AsNoTracking().Where(x => x.status == 1).OrderBy(x => x.building_name).ToList();
                    }
                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return listBuilding;
        }

        public building GetBuilding(int _buildingId)
        {
            building objBuilding = new building();
            try
            {
                using (var context = new betaskdbEntities())
                {                  

                    objBuilding = context.building.Where(x => x.status == 1 && x.building_id== _buildingId).FirstOrDefault();
                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return objBuilding;
        }

        public List<building> GetAllBuildings(string searchValue)
        {
            List<building> listBuilding = new List<building>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                   // listBuilding = context.building.Where(x => x.status == 1).OrderBy(x => x.building_name).ToList();
                    var _context = context.building.Where(x => x.status==1 || x.status == 2);
                    if (!string.IsNullOrEmpty(searchValue))
                        _context = _context.Where(u => u.building_name.Contains(searchValue)||u.route.Contains(searchValue) || u.area.Contains(searchValue));
                        listBuilding = _context.OrderBy(o => o.building_name).ToList();
                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return listBuilding;
        }
    }
}
