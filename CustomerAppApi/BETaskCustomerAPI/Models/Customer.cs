using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BETaskAPI.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Address1 { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string POBox { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string LoginKey { get; set; }

        //public string TRN { get; set; }
        //public string LAT { get; set; }
        //public string LNG { get; set; }
        public string Remarks { get; set; }
        // public string Route { get; set; }
        // public int DistanceRadious { get; set; } = 0; // For checking customer distance while delivery. In meters
        public decimal Outstanding { get; set; }
        public string Payment_mode { get; set; }
        public string Delivery_interval { get; set; }
        //public int RateIncludeTax { get; set; } = 1;
        //prakash Tmr Added
        public decimal WalletBalance { get; set; }
        public string WalletNumber { get; set; }
        public List<CustomerAggrement> customerAggrements { get; set; }

        public string APP_CustomerName { get; set; }
        public string APP_Email { get; set; }
        public string APP_Phone { get; set; }
        public string APP_Address1 { get; set; }
        public string APP_Address2 { get; set; }
    }

    public class CustomerProfile
    {
        public string Company { get; set; }
        public string Location { get; set; }


        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Address1 { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }


        public string APP_CustomerName { get; set; }
        public string APP_Email { get; set; }
        public string APP_Phone { get; set; }
        public string APP_Address1 { get; set; }
        public string APP_Address2 { get; set; }
        public string APP_Password { get; set; }

        public string LoginKey { get; set; }

    }
    public class CustomerAggrement
    {
        public int CustomerAggrementId { get; set; }
        public string ItemName { get; set; }
        // public int CustomerId { get; set; }
        public int ItemId { get; set; }
        public decimal MaxQty { get; set; }
        public decimal UnitPrice { get; set; }
    }



    public class CustomerCoupon
    {
        public int CustomerId { get; set; }
        public string WalletNumber { get; set; }
        public decimal Amount { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal UnitPriceWithTax { get; set; }
        public int Qty { get; set; }
        public string LoginKey { get; set; }
        public int EnablePaymentGateway { get; set; }
        public int EnableOffer { get; set; }
        public string OnlinePaymentComingSoonMessage = "We will enable online payment for you shortly";
        public string NoOfferMessage = "No Offers are available for you";
        public int IsProfileUpdated { get; set; }
        public string ProfileNotUpdateMessage = "It is necessary to update your profile to get latest offers";

        public List<CustomerOffer> CustomerOffers { get; set; }
        public Customer Customer { get; set; }
    }

    public class CustomerOffer
    {
        public int OfferId { get; set; }
        public string OfferName { get; set; }
        public decimal Amount { get; set; }
        public string OfferCategory { get; set; }
        public decimal PaymentFee { get; set; }
        public decimal NetAmount { get; set; }
        public string Notification { get; set; }

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
}