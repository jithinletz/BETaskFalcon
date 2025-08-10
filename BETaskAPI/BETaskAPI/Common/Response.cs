using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BETaskAPI.Common
{
    public class Response<T>
    {
      
        public bool IsError { get; set; }
        public string Message { get; set; }
        public T Result { get; set; }
        public int TotalRecords { get; set; }
    }

    public class OutstandingReportResponse<T>
    {

        public bool IsError { get; set; }
        public string Message { get; set; }
        public decimal TotalOutstanding { get; set; }
        public T Result { get; set; }
        public int TotalRecords { get; set; }

    }

    public class CustomerwiseDeliveryReportResponse<T>
    {

        public bool IsError { get; set; }
        public string Message { get; set; }
        public decimal TotalDelivery { get; set; }
        public decimal TotalAmount { get; set; }
        public T Result { get; set; }
        public int TotalRecords { get; set; }

    }
}