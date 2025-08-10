using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BETask
{
    public class OrderStatusModel
    {
        [JsonProperty("reference_no")]
        public string ReferenceNo { get; set; }

        [JsonProperty("order_no")]
        public string OrderNo { get; set; }

        [JsonProperty("order_currncy")]
        public string OrderCurrency { get; set; }

        [JsonProperty("order_amt")]
        public decimal OrderAmt { get; set; }

        [JsonProperty("order_date_time")]
        public DateTime OrderDateTime { get; set; }

        [JsonProperty("order_bill_name")]
        public string OrderBillName { get; set; }

        [JsonProperty("order_bill_address")]
        public string OrderBillAddress { get; set; }

        [JsonProperty("order_bill_zip")]
        public string OrderBillZip { get; set; }

        [JsonProperty("order_bill_tel")]
        public string OrderBillTel { get; set; }

        [JsonProperty("order_bill_email")]
        public string OrderBillEmail { get; set; }

        [JsonProperty("order_bill_country")]
        public string OrderBillCountry { get; set; }

        [JsonProperty("order_ship_name")]
        public string OrderShipName { get; set; }

        [JsonProperty("order_ship_address")]
        public string OrderShipAddress { get; set; }

        [JsonProperty("order_ship_country")]
        public string OrderShipCountry { get; set; }

        [JsonProperty("order_ship_tel")]
        public string OrderShipTel { get; set; }

        [JsonProperty("order_bill_city")]
        public string OrderBillCity { get; set; }

        [JsonProperty("order_bill_state")]
        public string OrderBillState { get; set; }

        [JsonProperty("order_ship_city")]
        public string OrderShipCity { get; set; }

        [JsonProperty("order_ship_state")]
        public string OrderShipState { get; set; }

        [JsonProperty("order_ship_zip")]
        public string OrderShipZip { get; set; }

        [JsonProperty("order_ship_email")]
        public string OrderShipEmail { get; set; }

        [JsonProperty("order_notes")]
        public string OrderNotes { get; set; }

        [JsonProperty("order_ip")]
        public string OrderIp { get; set; }

        [JsonProperty("order_status")]
        public string OrderStatusValue { get; set; }

        [JsonProperty("order_fraud_status")]
        public string OrderFraudStatus { get; set; }

        [JsonProperty("order_status_date_time")]
        public DateTime OrderStatusDateTime { get; set; }

        [JsonProperty("order_capt_amt")]
        public decimal OrderCaptAmt { get; set; }

        [JsonProperty("order_card_name")]
        public string OrderCardName { get; set; }

        [JsonProperty("order_delivery_details")]
        public string OrderDeliveryDetails { get; set; }
    }

}
