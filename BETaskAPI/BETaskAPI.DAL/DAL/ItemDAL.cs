using BETaskAPI.DAL.EDMX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BETaskAPI.DAL.DAL
{
   public class ItemDAL
    {
        public List<item> GetItems()
        {
            List<item> listItem = new List<item>();
            try
            {
                using (var context = new betaskdbEntities())
                { 
                    listItem = context.item.Where(x=>x.sellable==1 && x.status==1 && x.description.ToLower().Contains("sell")).ToList(); 
                }
            }
            catch
            {
                throw;
            }
            return listItem;
        }

        public decimal GetItemTaxValue(int itemId)
        {
            item _item = new item();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    _item = context.item.Find(itemId);
                    var tax_setting = context.tax_setting.Find(_item.tax);
                    return tax_setting?.tax_value?? 0;
                }
            }
            catch
            {
                throw;
            }
        }

    }
}
