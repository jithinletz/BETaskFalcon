using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BETaskSync.Common;
using BETaskAPI.DAL.DAL;
using BETaskSync.EDMX;
using edmx = BETaskAPI.DAL.EDMX;
using edmxLocal = BETaskSync.EDMX;
using System.Data.Entity;

namespace BETaskSync.Repository
{
   public class LedgerMappingDAL
    {
        public enum EnumLedgerMapGroupTypes { SUPPLIER, CUSTOMER, BANKACCOUNTS }
        public enum EnumLedgerMap { PURCHASE, VATONPURCHASE, ROUNDOFF, DISCOUNTRECIEVED, CASH, SALE, DISCOUNTALLOWED, VATONSALE, COMPANYLEDGER }




        public EDMX.ledger_mapping GetLegerMapping(Enum mapType)
        {
            string _mapType = mapType.ToString();
            EDMX.ledger_mapping mapLedger = new EDMX.ledger_mapping();
            try
            {
                using (var context = new betaskdbEntitiesLocal())
                {
                    mapLedger = context.ledger_mapping.Where(x => x.status == 1 && x.ledger_type == _mapType && x.status == 1).FirstOrDefault();
                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return mapLedger;
        }
    }
}
