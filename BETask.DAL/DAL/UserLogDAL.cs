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
    public static class UserLogDAL
    {
        public static void Log(user_log _log)
        {
            try
            {
                using (var context = new betaskdbEntities())
                {
                    var dQuery = context.Database.SqlQuery<DateTime>("SELECT getdate()");
                    DateTime dbDate = dQuery.AsEnumerable().First();
                    _log.server_time = dbDate;
                    context.Entry(_log).State = EntityState.Added;
                    context.SaveChanges();
                }
            }
            catch (Exception ee)
            {
                throw;
            }
        }
    }
}
