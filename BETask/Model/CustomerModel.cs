using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BETask.Model
{
    class CustomerModel
    {
        public int Customer_Id { get; set; }
        public string Customer_Name { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string POBox { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public int Customer_Type { get; set; }
        public string Remarks { get; set; }
        public string ContactPerson { get; set; }
        public string Trn { get; set; }
        public string WalletNumber { get; set; }
        public decimal WalletBalance { get; set; }
        public int status { get; set; }
        public int LedgerId { get; set; }
        public string Lat { get; set; }
        public string Lng { get; set; }
        public int RouteId { get; set; }
        public int BuildingId { get; set; }
        // public string BuildingName { get; set; }
        public int CloudSunc { get; set; }
        public string Paymentmode { get; set; }
        public string DeliveryInterval { get; set; }
        public string AddedOn { get; set; }
        public int CustomerTempId { get; set; }
        public string agreement { get; set; }
        public int Status { get; set; } = 1;
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public int NewCustomer { get; set; }
        public decimal CreditLimit { get; set; }
        public int OfferId { get; set; }
        public string OfferName { get; set; }
        public int EnableOnlinePayment { get; set; }
        public int EnableOffer { get; set; }
        public string LocationDistance { get; set; }
        public bool is_group { get; set; }
        public int group_id { get; set; } 

    }
}
