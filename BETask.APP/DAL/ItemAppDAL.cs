using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BETask.APP.EDMX;
using System.Data.Entity;
using System.Data;

namespace BETask.APP.DAL
{
   public class ItemAppDAL
    {
        public void SaveItem(item _item)
        {
            try
            {
                using (var context = new betaskdbEntitiesAPP())
                {
                    var xItem = context.item.Where(x => x.item_id == _item.item_id).FirstOrDefault();
                    if (xItem == null)
                    {
                        context.Entry(_item).State = EntityState.Added;
                    }
                    else
                    {
                       
                        xItem.barcode = _item.barcode;
                        xItem.brand = _item.brand;
                        xItem.cost = _item.cost;
                        xItem.description = _item.description;
                        xItem.item_name = _item.item_name;
                        xItem.opening_stock = _item.opening_stock;
                        xItem.purchase_rate = _item.purchase_rate;
                        xItem.sale_rate = _item.sale_rate;
                        xItem.sellable = _item.sellable;
                        xItem.status = _item.status;
                        xItem.Stock = _item.Stock;
                        xItem.stockable = _item.stockable;
                        xItem.tax = _item.tax;
                        xItem.uom = _item.uom;
                        xItem.rawmeterial = _item.rawmeterial;
                        context.Entry(xItem).State = EntityState.Modified;
                    }

                    context.SaveChanges();

                }
            }
            catch (Exception ee)
            {
                string ss = ee.ToString();
                throw;
            }
        }
    }
}
