using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BETask.DAL.EDMX;
using BETask.Model;

namespace BETask
{
    public static class CustomMapper
    {
        public static List<AccountLedgerModel> MapAccountLedger(List<account_ledger> ledgers)
        {
            return ledgers.Select(ledger => new AccountLedgerModel
            {
                Ledger_id = ledger.ledger_id,
                Ledger_name = ledger.ledger_name,
                Description = ledger.description,
                Group_id = ledger.group_id,
                Status = ledger.status,
                GroupName = "Default Group Name", // Placeholder or actual logic to set GroupName
                EnableCostCnetr = ledger.enable_cost_center ?? 0 // Handle null value
            }).ToList();
        }
    }
}
