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
    public class LedgerSearchDAL
    {
      
        public List<SP_LedgerSearch_Result> LedgerSearch(string ledgerName)
        {
           
            try
            {
                using (var context = new betaskdbEntities())
                {
                    return context.SP_LedgerSearch(ledgerName,100).ToList();
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
