using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BETask.APP.EDMX;
using System.Data.Entity;
using System.Data;
using System.Data.SqlClient;

namespace BETask.APP.DAL
{
   public class TransactionAppDAL
    {
        public DataTable GetOnlineTransaction(DateTime dateFrom,DateTime dateTo,string mode)
        {
            DataTable tblTransactions=new DataTable();
            try
            {
                using (var context = new betaskdbEntitiesAPP())
                {
                    using (SqlConnection conn = new SqlConnection(context.Database.Connection.ConnectionString))
                    {
                        conn.Open();
                    }
                    }
            }
            catch (Exception ex)
            {
                throw;
            }
            return tblTransactions;
        }
    }
}
