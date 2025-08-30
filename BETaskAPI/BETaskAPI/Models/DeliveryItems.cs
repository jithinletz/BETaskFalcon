using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BETaskAPI.Models
{
    public class DeliveryItems
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public decimal Qty { get; set; }
        public decimal Rate { get; set; }
        public decimal NetAmount { get; set; }
        public decimal CustomerItemMaxQty { get; set; }
    }

    public abstract class DailyCollectionAbstract
    {
        public int DailyCollectionId { get; set; }
        public int DeliveryId { get; set; }
        public int CustomerId { get; set; }
        public decimal NetAmount { get; set; }
        public decimal CollectedAmount { get; set; }
        public string PaymentMode { get; set; }
        public int EmployeeId { get; set; }
        public string Remarks { get; set; }
        public int Status { get; set; }
        public string CouponNumber { get; set; }
        public int DivisionId { get; set; }
        public int IsDeposit { get; set; } = 2;
        public int IsRefund { get; set; } = 2;
        public int OldLeafCount { get; set; }
        public int IsMailSend { get; set; }
        public int CustomerEmail { get; set; }

    }

    public class DailyCollection : DailyCollectionAbstract
    {
    }

    public class DailyCollectionWithItem : DailyCollectionAbstract
    {
        public List<CustomerItem> CustomerItem { get; set; }

    }



    public class ItemMini
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int Qty { get; set; }
    }
    public class ItemCart
    {
        public int DeliveryId { get; set; }
        public int CustomerId { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int Qty { get; set; }
        public decimal Rate { get; set; }
    }



    public class DeliveryReturn
    {
        public int CustomerId { get; set; }
        public int EmployeeId { get; set; } 
        public string Remarks { get; set; }
        public int ReturnType { get; set; } = 1;//1 is gallon return , 2 is permanat return
        public List<DeliveryReturnItem> ReturnItems { get; set; }
    }

        public class DeliveryReturnItem {  
     
        public int ItemId { get; set; }
        public decimal Qty { get; set; } 
   

    }

    public class DeliveryItemCustomerwise
    {
        public string CustomerName { get; set; }
        public int ItemId { get; set; }
        public string DeliveryTime { get; set; }
        public string ItemName { get; set; }
        public decimal Delivered { get; set; }
        public decimal NetAmount { get; set; }


    }
    public class DeliveryReturnItemCustomerwise
    {
        public string CustomerName { get; set; }
        public int ItemId { get; set; }
        public string ReturnDate { get; set; }
        public string ItemName { get; set; }
        public decimal Returned { get; set; }
       

    }
}