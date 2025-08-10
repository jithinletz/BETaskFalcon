using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BETaskAPI.DAL.EDMX;
using System.Data.Entity;
using System.Data;
using BETaskAPI.DAL.Model;
using System.Data.SqlClient;

namespace BETask.DAL.DAL
{
    public enum EnumTransactionTypes {  SALE, DRETURN }
    public class ItemTransactionDAL
    {
        public void SaveItemTransaction(item_transaction item, betaskdbEntities context)
        {
            try
            {
                context.Entry(item).State = EntityState.Added;
                context.SaveChanges();
            }
            catch (Exception ee)
            {
                throw;
            }
        }



        public void SaveItemTransaction_Sales(betaskdbEntities context, List<sales_item> listItem)
        {
            try
            {
                int _custId = listItem[0].sales.customer_id;
                string supplier = context.customer.Find(_custId).customer_name;
                foreach (sales_item item in listItem)
                {

                    var xItem = context.item.Where(i => i.item_id == item.item_id).FirstOrDefault();

                    item_transaction itemTran = new item_transaction()
                    {
                        item_id = item.item_id,
                        item_cost = xItem.cost,
                        qty_added = 0,
                        qty_reduced = item.qty,
                        transaction_date = item.sales.sales_date,
                        closing_stock = xItem.Stock - item.qty,
                        closing_value = xItem.cost * (xItem.Stock - item.qty),
                        narration = $"Sale to {supplier}",
                        transaction_type = EnumTransactionTypes.SALE.ToString(),
                        transaction_type_id = item.sales_id,
                        status = 1

                    };
                    context.Entry(itemTran).State = EntityState.Added;

                    xItem.Stock = item.item.Stock - item.qty;
                    context.Entry(xItem).State = EntityState.Modified;

                }
            }
            catch (Exception ee)
            {
                throw;
            }
        }
       
        public void SaveItemTransaction_DeliveryReturn(betaskdbEntities context,List<delivery_return> items)
        {
            try
            {
                foreach (delivery_return item in items)
                {
                    //Checking Exception
                    int exceptionItemId = 0;
                    if (context.item_return_exception.Any(x => x.item_id == item.item_id))
                    {
                        var itException = context.item_return_exception.AsNoTracking().FirstOrDefault(x => x.item_id == item.item_id && x.status == 1);
                        if (itException != null)
                            exceptionItemId = itException.return_item_id;
                    }
                    var xItem = context.item.FirstOrDefault(i => i.item_id == (exceptionItemId == 0 ? item.item_id : exceptionItemId));
                    decimal cost = xItem.cost;
                    if (exceptionItemId > 0)
                    {
                        cost = 0;
                       
                    }


                    item_transaction itemTran = new item_transaction()
                    {
                        item_id = xItem.item_id,
                        //item_cost = cost,
                        item_cost = cost,
                        qty_added = item.qty,
                        qty_reduced = 0,
                        transaction_date = item.return_date,
                        closing_stock = xItem.Stock + item.qty,
                        closing_value = xItem.cost * (xItem.Stock + item.qty),
                        narration = "Delivery Return Item",
                        transaction_type = EnumTransactionTypes.DRETURN.ToString(),
                        transaction_type_id = item.delivery_return_id,
                        status = 1

                    };
                    context.Entry(itemTran).State = EntityState.Added;

                    xItem.Stock = xItem.Stock + item.qty;
                    context.Entry(xItem).State = EntityState.Modified;
                }
            }
            catch (Exception ee)
            {
                throw;
            }
        }

        public void RemoveItemOpening(int itemTransactionId)
        {
            try
            {
                using (betaskdbEntities context = new betaskdbEntities())
                {
                    item_transaction it = context.item_transaction.Where(x => x.item_transaction_id == itemTransactionId).FirstOrDefault();
                    if (it != null)
                    {
                        it.status = 2;
                        context.Entry(it).State = EntityState.Modified;
                        context.SaveChanges();
                    }
                }
            }
            catch
            {
                throw;
            }
        }

    }
}
