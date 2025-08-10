using System;
using System.Collections.Generic;
using System.Linq;
using BETaskAPI.DAL.EDMX;
using System.Data.Entity;
using System.Data;
using BETaskAPI.DAL.Model;

namespace BETaskAPI.DAL.DAL
{
    public class LedgerMappingDAL
    {
        public enum EnumLedgerMapGroupTypes { SUPPLIER, CUSTOMER, BANKACCOUNTS }
        public enum EnumLedgerMap { PURCHASE, VATONPURCHASE, ROUNDOFF, DISCOUNTRECIEVED, CASH, SALE, DISCOUNTALLOWED, VATONSALE, COMPANYLEDGER, PDCRECIEVED, PDCISSUED, DEPOSITCOLLECTION, COUPONBOOKLIABILITY, PETTYCASH, CUSTOMER, ONLINEPAYMENTBANK }




        public void SaveLedgerMapSetting(ledger_mapping ledgerMap)
        {
            try
            {
                using (var context = new betaskdbEntities())
                {
                    if (ledgerMap.group_id == 0)
                        ledgerMap.group_id = null;
                    if (ledgerMap.ledger_id == 0)
                        ledgerMap.ledger_id = null;
                    context.Entry(ledgerMap).State = ledgerMap.ledger_mapping_id == 0 ? EntityState.Added : EntityState.Modified;
                    context.SaveChanges();

                }
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        public ledger_mapping GetLegerMapping(Enum mapType)
        {
            string _mapType = mapType.ToString();
            ledger_mapping mapLedger = new ledger_mapping();
            try
            {
                using (var context = new betaskdbEntities())
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
        public List<ledger_mapping> GetPurchaseLegers()
        {

            List<ledger_mapping> listLedger = new List<ledger_mapping>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    listLedger = context.ledger_mapping.Where(x => x.status == 1 &&
                    (x.ledger_type == (EnumLedgerMap.PURCHASE.ToString()) || x.ledger_type == EnumLedgerMap.ROUNDOFF.ToString()
                    || x.ledger_type == EnumLedgerMap.DISCOUNTRECIEVED.ToString() || x.ledger_type == EnumLedgerMap.VATONPURCHASE.ToString()
                    || x.ledger_type == EnumLedgerMap.PETTYCASH.ToString())).ToList();
                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return listLedger;
        }
       

        public List<ledger_mapping> GetSalesLegers()
        {

            List<ledger_mapping> listLedger = new List<ledger_mapping>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    listLedger = context.ledger_mapping.Where(x => x.status == 1 &&
                    (x.ledger_type == (EnumLedgerMap.SALE.ToString()) || x.ledger_type == EnumLedgerMap.ROUNDOFF.ToString()
                    || x.ledger_type == EnumLedgerMap.DISCOUNTALLOWED.ToString() || x.ledger_type == EnumLedgerMap.VATONSALE.ToString()
                    || x.ledger_type == EnumLedgerMap.CASH.ToString())).ToList();
                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return listLedger;
        }
        public SaleAccountPostModel SetSaleAccountLedger(SaleAccountPostModel saleAccountPosting)
        {
            try
            {

                List<EDMX.ledger_mapping> listLedger = GetSalesLegers();


                foreach (EDMX.ledger_mapping ledger in listLedger)
                {

                    if (ledger.ledger_type == LedgerMappingDAL.EnumLedgerMap.SALE.ToString())
                    {
                        saleAccountPosting.SalesLedger = Convert.ToInt32(ledger.ledger_id);
                    }
                    else if (ledger.ledger_type == LedgerMappingDAL.EnumLedgerMap.CASH.ToString())
                    {
                        saleAccountPosting.CashSaleLedger = Convert.ToInt32(ledger.ledger_id);
                    }
                    else if (ledger.ledger_type == LedgerMappingDAL.EnumLedgerMap.VATONSALE.ToString())
                    {
                        saleAccountPosting.VatOnSaleLedger = Convert.ToInt32(ledger.ledger_id);
                    }
                    else if (ledger.ledger_type == LedgerMappingDAL.EnumLedgerMap.ROUNDOFF.ToString())
                    {
                        saleAccountPosting.RoundOffLedger = Convert.ToInt32(ledger.ledger_id);
                    }
                    else if (ledger.ledger_type == LedgerMappingDAL.EnumLedgerMap.DISCOUNTALLOWED.ToString())
                    {
                        saleAccountPosting.DiscountAllowedLedger = Convert.ToInt32(ledger.ledger_id);
                    }
                }

            }
            catch
            {
                throw;
            }
            return saleAccountPosting;
        }
        public List<ledger_mapping> GetCompanyLedger()
        {
            string ledgerType = LedgerMappingDAL.EnumLedgerMap.COMPANYLEDGER.ToString();
            List<ledger_mapping> listLedger = new List<ledger_mapping>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    listLedger = context.ledger_mapping.Include(l => l.account_ledger).Where(x => x.ledger_type == ledgerType && x.status == 1).ToList();
                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return listLedger;
        }
        public List<ledger_mapping> GetAllLedgerMapping()
        {

            List<ledger_mapping> listLedger = new List<ledger_mapping>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    listLedger = context.ledger_mapping.Include(l => l.account_ledger).Include(x => x.account_group).OrderBy(x => x.ledger_type).ToList();
                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return listLedger;
        }

        public string CheckPDCMissingConfiguration()
        {
            string message = string.Empty;
            try
            {
                using (var context = new betaskdbEntities())
                {
                    if (!context.ledger_mapping.Any(x => x.ledger_type == LedgerMappingDAL.EnumLedgerMap.PDCISSUED.ToString()))
                    {
                        message = "PDC issue ledger not created or mapped\n";
                    }
                    if (!context.ledger_mapping.Any(x => x.ledger_type == LedgerMappingDAL.EnumLedgerMap.PDCRECIEVED.ToString()))
                    {
                        message += " PDC isssue ledger not created or mapped ";
                    }
                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return message;
        }

    }
}

