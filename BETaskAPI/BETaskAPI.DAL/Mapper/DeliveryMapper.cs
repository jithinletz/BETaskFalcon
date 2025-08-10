using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BETaskAPI.DAL.EDMX;

namespace BETaskAPI.DAL
{
   public class DeliveryMapper
    {
        public sales_item MapToSalesItem(delivery_items deliveryItem)
        {
            return new sales_item
            {
                sales_item_id = 0, 
                sales_id = 0,
                item_id = deliveryItem.item_id,
                qty = deliveryItem.qty,
                rate = deliveryItem.rate,
                gross_amount = deliveryItem.gross_amount,
                discount = deliveryItem.discount,
                total_beforvat = deliveryItem.total_beforvat,
                vat_amount = deliveryItem.vat_amount,
                net_amount = deliveryItem.net_amount,
                status = deliveryItem.status,
                delivery_item_id = deliveryItem.delivery_item_id
            };
        }

        public List<sales_item> MapToSalesItemList(List<delivery_items> deliveryItems)
        {
            List<sales_item> salesItems = new List<sales_item>();

            foreach (var deliveryItem in deliveryItems)
            {
                salesItems.Add(MapToSalesItem(deliveryItem));
            }

            return salesItems;
        }
    }
}
