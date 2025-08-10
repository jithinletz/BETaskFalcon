using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BETaskAPI.Models
{
    public class ItemDeliveryReport
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public Nullable<decimal> Total { get; set; }
        public Nullable<decimal> Completed { get; set; }
    }

    public class DailyCollectionReport
    { 
        public string DateCollected { get; set; }
        public decimal TotalCollected { get; set; }
        public decimal AmountClosed { get; set; }
        public decimal BalanceAmount { get; set; }

    }

    public class DailyCollectionDetailReport
    {
        public string DeliveryTime { get; set; }
        public decimal NetAmount { get; set; }
        public decimal CollectedAmount { get; set; }
        public string Status { get; set; }
        public string CustomerName { get; set; }

    }
    public class DailyCollectionAllPaymentsReport
    {
        public string CollectionDate { get; set; }
        public decimal DeliveryAmount { get; set; }
        public decimal ColelctedAmount { get; set; }
        public decimal Cash { get; set; }
        public decimal Credit { get; set; }
        public decimal Coupon { get; set; }
        public decimal Bank { get; set; }

    }
    public class DailyCollectionDetailAllPaymentReport
    {
        public string DeliveryTime { get; set; }
        public decimal NetAmount { get; set; }
        public decimal CollectedAmount { get; set; }
        public string Status { get; set; }
        public string CustomerName { get; set; }
        public string PaymentMode { get; set; }
        //public string DeliveryId { get; set; }

    }

    public class RouteItemSummaryReport
    {
        public decimal TotalItems { get; set; }
        public string ItemName { get; set; }
        public string RouteName { get; set; }
    }


}