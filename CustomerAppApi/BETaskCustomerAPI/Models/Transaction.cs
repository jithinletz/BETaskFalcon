using BETaskCustomerAPI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace BETaskAPI.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Transaction: AbstractCompanyBase
    {

        public int TransactionId { get; set; }
        public string ReferenceId { get; set; }
        public int CustomerId { get; set; }
        public string CustomerIP { get; set; }
        public DateTime APPDate { get; set; }
        public int OfferId { get; set; }
        public decimal Amount { get; set; }
        public string OfferName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string GateWay { get; set; }
        public int Status { get; set; }
        public string Version { get; set; } = "V.0.0.0";
        public string TId { get; set; }
        public string OtherNotes { get; set; }
        public string TrackingId { get; set; }
    


    }
    /// <summary>
    /// 
    /// </summary>
    public class TransactionResponse:AbstractCompanyBase
    {
        public string ReferenceId { get; set; }
        public decimal AmountReceived { get; set; }
        public string PaymentReferenceId { get; set; }
        public string PaymentMode { get; set; }
        public string StatusText { get; set; }
        public string Response { get; set; }
        public string Version { get; set; } = "V.0.0.1";

    }

    public class TransactionHistory
    {
        public int TransactionId { get; set; }
        public string ReferenceId { get; set; }
        public string StartDate { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public string StatusText { get; set; }
        public string DetailedRespose { get; set; }

    }
}

