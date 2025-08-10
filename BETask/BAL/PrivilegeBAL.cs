using System;
using EDMX = BETask.DAL.EDMX;
using REP = BETask.DAL.DAL;
using BETask.Model;
using BETask.Common;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace BETask.BAL
{
    public class PrivilegeBAL
    {
        REP.PrivilegeDAL privilegeDAL = new REP.PrivilegeDAL();
        public List<string> GetPrivilegeTypes()
        {
            try
            {
                return privilegeDAL.GetPrivilegeTypes();
            }
            catch (Exception ee)
            {
                throw;
            }
        }

        public List<EDMX.privilege_menu> GetAllPrivilege(string privilegeType)
        {
            try
            {
                return privilegeDAL.GetAllPrivilege(privilegeType);
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        public void UpdatePrivilege(EDMX.privileges _privileges)
        {
            try
            {
                privilegeDAL.UpdatePrivilege(_privileges);
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
                    reference_id = _privileges.user_id,
                    summary = $" Privilege Updatation privilegeId={_privileges.privilege_id} , Menu={_privileges.menu_id}  ",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
        }
        public List<EDMX.privileges> GetActivePrivileges(int employeeId)
        {
            try
            {
                return privilegeDAL.GetActivePrivileges(employeeId);
            }
            catch
            {
                throw;
            }
        }

    }
}
