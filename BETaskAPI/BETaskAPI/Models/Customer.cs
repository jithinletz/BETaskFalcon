using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace BETaskAPI.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string POBox { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string TRN { get; set; }
        public string LAT { get; set; }
        public string LNG { get; set; }
        public string Remarks { get; set; }
        public string Route { get; set; }
        public int DistanceRadious { get; set; } = 0; // For checking customer distance while delivery. In meters
        public decimal Outstanding { get; set; }
        public string Payment_mode { get; set; }
        public string Delivery_interval { get; set; }
        public int RateIncludeTax { get; set; } = 1;
        public string WalletNumber { get; set; }
        public decimal WalletBalance { get; set; }
        public string Building { get; set; }
        public int EmployeeId { get; set; }
        public int NewCustomer { get; set; } = 1;

        public List<CustomerAggrement> customerAggrements { get; set; }
    }

    public class CustomerAggrement {
        public int CustomerAggrementId { get; set; }
        public int CustomerId { get; set; }
        public int ItemId { get; set; }
        public decimal MaxQty { get; set; }
        public decimal UnitPrice { get; set; }
    }

   
     
    public class CustomerCoupon {
        public int CustomerId { get; set; }
        public string WalletNumber { get; set; }
        public decimal Amount { get; set; }

    }

    public class CustomerItem
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public decimal Rate { get; set; }
        public int Qty { get; set; }
    }

    public class CustomerOutstandigReport
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Route { get; set; }
        public decimal Outstanding { get; set; }
    }

    public class CustomerPaymentMode
    {
        public string PaymentMode { get; set; }
        public int PaymentModeLocked { get; set; } = Convert.ToInt32(ConfigurationManager.AppSettings["PaymentLocked"]);
        public string CouponCode { get; set; }
        public List<CustomerDivision> Division { get; set; }
    }
    public class CustomerDivision
    {
        public int DivisionId { get; set; }
        public string DivisionName { get; set; }
    }

    public class Punch
    {
        public long PunchId { get; set; }
        public int EmployeeId { get; set; }
        public string Lat { get; set; }
        public string Lng { get; set; }
        public string LocationDetails { get; set; }
        public string PunchType  { get; set; }//IN or OUT
        public DateTime PunchDate { get; set; }
        public string Remarks { get; set; }
        public DateTime AppDate { get; set; }
        public string AppVersion { get; set; }

    }

    public class PunchStatus
    {

        public long PunchId { get; set; }
        public string PunchType { get; set; }//IN or OUT
        public string PunchDate { get; set; }
        public string PunchIn { get; set; }
        public string PunchOut { get; set; }
        public string EmployeeName { get; set; }


    }


}