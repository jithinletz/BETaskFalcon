using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace BETaskCustomerAPI.Common
{
    public static class PaymentFee
    {
        private const double V = 2.31;
        private const double V1 = 0;
        public static decimal feePercentage = (decimal)V;
        public static decimal feeAmount = (decimal)V1;
        public static decimal GetPaymentFee(decimal amount)
        {
            decimal fee = (amount * feePercentage / 100) + feeAmount;
            decimal feeRounded = Math.Round(fee, 2);
            string result = feeRounded.ToString("F2");
            return decimal.Parse(result);
        }
        public static decimal GetNetAmount(decimal amount)
        {
            decimal fee = ((amount * feePercentage / 100) + feeAmount);
            decimal feeRounded = Math.Round(fee, 2);
            decimal totalAmount = amount + feeRounded;
            string result = totalAmount.ToString("F2");
            return decimal.Parse(result);
        }
    }
}