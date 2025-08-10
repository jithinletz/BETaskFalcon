using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Web;

namespace BETaskCustomerAPI.Models
{
    public class Order
    {
        [JsonPropertyName("order_status")]
        public string OrderStatus { get; set; }

        [JsonPropertyName("amount")]
        public string Amount { get; set; }

        [JsonPropertyName("order_datetime")]
        public string OrderDatetime { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [JsonPropertyName("order_id")]
        public string OrderId { get; set; }

        [JsonPropertyName("tracking_id")]
        public long TrackingId { get; set; }
    }

}