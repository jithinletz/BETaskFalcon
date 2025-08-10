using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BETask.DAL.EDMX;
using System.Data.Entity;
using System.Data;
using System.ComponentModel;
using System.Drawing;

namespace BETask.DAL.DAL
{
   public class SaleMergingDAL
    {

        public List<EDMX.sales> GetAllSales(int customerId,int divisionId,DateTime fromDate,DateTime toDate)
        {
            List<EDMX.sales> listSaleMerging = new List<sales>();
            try
            {
                using (var context = new betaskdbEntities())
                {

                    listSaleMerging = context.sales.AsNoTracking().Where(x => x.customer_id == customerId && (divisionId>0? x.division_id==divisionId:(x.division_id>0 ||x.division_id==null)) && x.sales_date>=fromDate && x.sales_date<=toDate && x.status==1).OrderBy(x => x.customer_id).ToList();

                }
            }
            catch
            {
                throw;
            }
            return listSaleMerging;
        }

        public  int SalesMerge(List<int> listSale, int customerId,int divisionId)
        {
            int resp = 0;
            try
            {
                using (var context = new betaskdbEntities())
                {
                    using (DbContextTransaction transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            foreach (int saleId in listSale)

                            {

                                sales xSale = context.sales.FirstOrDefault(x => x.sales_id == saleId);
                                if (xSale != null)
                                {
                                    int newCustomerLedger = Convert.ToInt32(context.customer.AsNoTracking().FirstOrDefault(x => x.customer_id == customerId).ledger_id);
                                    int oldCustomerLedger = Convert.ToInt32(context.customer.AsNoTracking().FirstOrDefault(x => x.customer_id == xSale.customer_id).ledger_id);
                                    context.sales.Attach(xSale);
                                    xSale.remarks = $"{xSale.remarks}.Old customer {xSale.customer_id}.";
                                    xSale.customer_id = customerId;

                                    if (divisionId > 0)
                                    {
                                        xSale.division_id = divisionId;
                                        context.Entry(xSale).Property(x => x.division_id).IsModified = true;
                                    }
                                    context.Entry(xSale).Property(x => x.customer_id).IsModified = true;

                                    //Account update
                                    List<account_transaction> listTransaction = context.account_transaction.Where(x => x.transaction_type_id == xSale.sales_id && x.transaction_type.ToLower() == "sale" && x.status == 1).ToList();
                                    if (listTransaction != null && listTransaction.Count > 0)
                                    {
                                        foreach (account_transaction ac in listTransaction)
                                        {
                                            if (context.account_ledger.Any(x => x.ledger_id == ac.ledger_id && x.description.ToLower() == "customer"))
                                            {
                                                if (ac.ledger_id == oldCustomerLedger)
                                                {
                                                    context.account_transaction.Attach(ac);
                                                    ac.ledger_id = newCustomerLedger;
                                                    ac.narration = $"{ac.narration} .old ledger {oldCustomerLedger}.";
                                                    context.Entry(ac).Property(x => x.ledger_id).IsModified = true;
                                                }
                                            }
                                        }
                                    }

                                    //Delivery
                                    List<delivery_items> listDelivery = context.delivery_items.Where(x => x.sales_id == xSale.sales_id).ToList();
                                    if (listDelivery != null)
                                    {
                                        foreach (delivery_items dl in listDelivery)
                                        {
                                            if (dl != null)
                                            {
                                                dl.customer_id = customerId;
                                                context.delivery_items.Attach(dl);
                                                context.Entry(dl).Property(x => x.customer_id).IsModified = true;

                                                //Daily Collection
                                                if (dl.daily_collection_id != null)
                                                {
                                                    daily_collection xColl = context.daily_collection.FirstOrDefault(x => x.daily_collection_id == dl.daily_collection_id && x.status == 4);
                                                    if (xColl != null)
                                                    {
                                                        xColl.customer_id = customerId;
                                                        context.daily_collection.Attach(xColl);
                                                        context.Entry(xColl).Property(x => x.customer_id).IsModified = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    
                                    resp++;
                                }
                            }
                            context.SaveChanges();
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
            return resp;
        }

    }
}
