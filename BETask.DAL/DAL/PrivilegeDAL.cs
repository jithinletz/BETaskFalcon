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
  public  class PrivilegeDAL
    {
        public enum Privileges { AllowBackDate }

        public List<string> GetPrivilegeTypes()
        {
            List<string> listPrivilegeTypes = new List<string>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    listPrivilegeTypes = context.privilege_menu.Select(x=>x.menu_type).Distinct().ToList();
                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return listPrivilegeTypes;
        }
        public List<privilege_menu> GetAllPrivilege(string privilegeType)
        {
            List<privilege_menu> listPrivilege = new List<privilege_menu>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    listPrivilege = context.privilege_menu.Where(x=>x.status==1 && x.menu_type==privilegeType).OrderBy(x=>x.menu_type).ToList();
                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return listPrivilege;
        }
        public void UpdatePrivilege(privileges _privileges)
        {
            try
            {
                using (var context = new betaskdbEntities())
                {
                    context.Entry(_privileges).State = _privileges.privilege_id == 0 ? EntityState.Added : EntityState.Deleted;
                    context.SaveChanges();
                }
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        public List<EDMX.privileges> GetActivePrivileges(int employeeId)
        {
            List<EDMX.privileges> listActivePrivileges = new List<privileges>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    listActivePrivileges = context.privileges.Include(x => x.privilege_menu).Where(e => e.user_id == employeeId).ToList();
                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return listActivePrivileges;
        }
        public bool IsPriviligeProvided(int userId,Enum prvilegeType,betaskdbEntities context)
        {
            try
            {
                string prv = prvilegeType.ToString();
                var privilege = context.privilege_menu.FirstOrDefault(x => x.menu_name == prv && x.status==1);
                if (privilege == null) return false;
                else
                {
                    if (context.privileges.Any(x => x.user_id == userId && x.menu_id == privilege.menu_id && x.status == 1))
                        return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return false;
        }
    }
}
