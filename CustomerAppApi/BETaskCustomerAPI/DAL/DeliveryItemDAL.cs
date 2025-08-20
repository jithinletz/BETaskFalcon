using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BETaskAPI.Models;
using BETaskCustomerAPI.Models;
using System.Data.SqlClient;
using System.Data;
using BETaskAPI.Common;
using System.Text;

namespace BETaskAPI.DAL
{
    /// <summary>
    /// <summary>
    /// AUTHOR : PRAKASH TMR
    /// DISC: all functions related to Delivery Item Table
    /// DATE: 10-12-2021
    /// </summary> 
    /// </summary>
    public class DeliveryItemDAL
    {
        DataTable tblData = new DataTable();
        SqlServerDBConnectionLogic SqlServerDBConnectionLogic = new SqlServerDBConnectionLogic();
        public DeliveryHistory DashboardDeliveryRequest(string company, string location, string customerId, DateTime dateFrom, DateTime dateTo)
        {
            DeliveryHistory DeliveryHistory = new DeliveryHistory();
            List<DeliveryItem> lstDeliveryItemDetails = new List<DeliveryItem>();
            List<RechargeHistory> lstRechargeHistory = new List<RechargeHistory>();
            try
            {

                SqlServerDBConnectionLogic.SqlServerDBConnection(company, location);
                using (SqlCommand cmd = new SqlCommand("APP_DashboardDeliveryRequest ", SqlServerDBConnectionLogic.Maincn))
                {
                    cmd.Parameters.AddWithValue("@customerid", customerId);
                    cmd.Parameters.AddWithValue("@datefrom", General.ConvertDateServerFormatWithStartTime(dateFrom));
                    cmd.Parameters.AddWithValue("@dateto", General.ConvertDateServerFormatWithEndTime(dateTo));
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                DeliveryItem item = new DeliveryItem();
                                item.DeliveryDate = General.ConvertDateTimeServerFormat(Convert.ToDateTime(reader["delivery_time"]));
                                item.ItemName = Convert.ToString(reader["item_name"]);
                                item.Qty = Convert.ToDecimal(reader["delivered_qty"]);
                                item.NetAmount = Convert.ToDecimal(reader["net_amount"]);
                                item.DeliveredBy = Convert.ToString(reader["deliveredby"]);
                                lstDeliveryItemDetails.Add(item);
                            }
                        }
                    }

                }
                using (SqlCommand cmd = new SqlCommand("APP_GetRechargeHistory ", SqlServerDBConnectionLogic.Maincn))
                {
                    cmd.Parameters.AddWithValue("@customerid", customerId);
                    cmd.Parameters.AddWithValue("@datefrom", General.ConvertDateServerFormatWithStartTime(dateFrom));
                    cmd.Parameters.AddWithValue("@dateto", General.ConvertDateServerFormatWithEndTime(dateTo));
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                RechargeHistory item = new RechargeHistory();
                                item.DeliveryTime = General.ConvertDateTimeServerFormat(Convert.ToDateTime(reader["delivery_time"]));
                                item.PaymentMode = Convert.ToString(reader["payment_mode"]);
                                item.Amount = Convert.ToDecimal(reader["collected_amount"]);
                                item.CollectedBy = Convert.ToString(reader["collected_by"]);
                                lstRechargeHistory.Add(item);
                            }
                        }
                    }

                }
                DeliveryHistory.listDeliveryItem = lstDeliveryItemDetails;
                DeliveryHistory.listRechargeHistory = lstRechargeHistory;

            }
            catch (Exception ex)
            {
                throw;
            }
            return DeliveryHistory;
        }
        public bool SaveDeliveryRequest(DeliveryRequest objDeliveryRequest, string company, string location)
        {
            bool spStasus = false;
            List<Complaint> lstComplaintDetails = new List<Complaint>();
            SqlServerDBConnectionLogic.SqlServerDBConnection(company, location);
            var requestTime = System.TimeZoneInfo.ConvertTimeFromUtc(
                        DateTime.UtcNow,
                        TimeZoneInfo.FindSystemTimeZoneById("Arabian Standard Time"));
            int output = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("APP_SaveDeliveryRequest", SqlServerDBConnectionLogic.Maincn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@customerId", Convert.ToString(objDeliveryRequest.CustomerId));
                cmd.Parameters.AddWithValue("@customerName", Convert.ToString(objDeliveryRequest.CustomerName));
                cmd.Parameters.AddWithValue("@address1", Convert.ToString(objDeliveryRequest.Address1));
                cmd.Parameters.AddWithValue("@address2", Convert.ToString(objDeliveryRequest.Address2));
                cmd.Parameters.AddWithValue("@itemsCount", Convert.ToDecimal(objDeliveryRequest.ItemsCount));
                cmd.Parameters.AddWithValue("@netAmount", Convert.ToDecimal(objDeliveryRequest.NetAmount));
                cmd.Parameters.AddWithValue("@requestTime", requestTime);
                cmd.Parameters.AddWithValue("@otherDetails", Convert.ToString(objDeliveryRequest.OtherDetails));
                cmd.Parameters.AddWithValue("@status", Convert.ToInt32(objDeliveryRequest.Status));
                cmd.Parameters.Add("@tranId", SqlDbType.Int).Direction = ParameterDirection.Output;
                object objOut = cmd.ExecuteScalar();
                int requestId = Convert.ToInt32(objOut);
                spStasus = true;

                if (objDeliveryRequest.lstDeliveryRequestItem.Count > 0 && requestId > 0)
                {
                    SqlCommand cmd2 = new SqlCommand("APP_SaveDeliveryRequestItem", SqlServerDBConnectionLogic.Maincn);
                    cmd2.CommandType = CommandType.StoredProcedure;
                    foreach (DeliveryRequestItem objDeliveryRequestItem in objDeliveryRequest.lstDeliveryRequestItem)
                    {
                        cmd2.Parameters.Clear();
                        cmd2.Parameters.AddWithValue("@requestId", Convert.ToInt32(requestId));
                        cmd2.Parameters.AddWithValue("@itemId", Convert.ToString(objDeliveryRequestItem.ItemId));
                        cmd2.Parameters.AddWithValue("@qty", Convert.ToString(objDeliveryRequestItem.Qty));
                        cmd2.Parameters.AddWithValue("@rate", Convert.ToString(objDeliveryRequestItem.Rate));
                        cmd2.Parameters.AddWithValue("@netAmount", Convert.ToDecimal(objDeliveryRequestItem.NetAmount));
                        cmd2.Parameters.AddWithValue("@status", Convert.ToInt32(objDeliveryRequestItem.Status));
                        output = cmd2.ExecuteNonQuery();

                    }
                }



            }
            catch (Exception ex)
            {
                throw;
            }
            return spStasus;
        }
        public void SetMailContent(string company, string location, DeliveryRequest objDeliveryRequest)
        {
            try
            {
                if (objDeliveryRequest == null)
                    return;
                var requestTime = System.TimeZoneInfo.ConvertTimeFromUtc(
                           DateTime.UtcNow,
                           TimeZoneInfo.FindSystemTimeZoneById("Arabian Standard Time"));

                ItemDAL itemDAL = new ItemDAL();
                MailSettingsDAL objMailSettingsDAL = new MailSettingsDAL();
                StringBuilder sbMailContent = new StringBuilder();
                string itemName = string.Empty;
                string tab = "\t";
                sbMailContent.AppendLine("<html>");
                sbMailContent.AppendLine(tab + "<body>");
                sbMailContent.AppendLine("<style>table,th,td {border:1px solid black;}</style>");
                sbMailContent.Append("<h1><br/> Customer Details  </h1>");
                sbMailContent.Append("<table style=width: 100% >");
                sbMailContent.Append($"<tr><td> Order Time </td><td> {requestTime} </td></tr>");
                string customerType = objDeliveryRequest.CustomerId > 0 ? "Existing" : "New";
                sbMailContent.Append($"<tr><td> Customer Type </td><td> {customerType} </td></tr>");
                sbMailContent.Append($"<tr><td> Customer Name </td><td><b> {objDeliveryRequest.CustomerName} </b></td></tr >");
                sbMailContent.Append($"<tr><td> Address </td><td> {objDeliveryRequest.Address1} </th></ tr >");
                sbMailContent.Append($"<tr><td> Phone </td><td> {objDeliveryRequest.Address2} </th></ tr >");
                sbMailContent.Append($"<tr><td> Notes </td><td> {objDeliveryRequest.OtherDetails} </td></tr>");
                sbMailContent.AppendLine("</table>");

                sbMailContent.Append("<h1><br/> Order Item Details  </h1> ");
                sbMailContent.Append("<table style=width: 100% >");
                sbMailContent.Append("<tr><th style=width: 70% > Item Name </th><th> Qty </th><th> Rate </th></tr>");

                foreach (DeliveryRequestItem dri in objDeliveryRequest.lstDeliveryRequestItem)
                {
                    itemName = string.Empty;
                    Item objItem = itemDAL.GetItemDetailsById(company, location, dri.ItemId);
                    if (objItem.ItemId > 0) { itemName = objItem.ItemName; }
                    sbMailContent.Append($"<tr><td  style=padding: 20px>  {itemName} </td> <td style=padding: 20px> {dri.Qty} </td> <td  style=padding: 20px>  {dri.Rate} </td ></tr>");
                    sbMailContent.AppendLine(tab);
                }
                sbMailContent.AppendLine("</table>");
                sbMailContent.AppendLine(tab + "</body>");
                sbMailContent.AppendLine("</html>");

                MailSettings mailSett = objMailSettingsDAL.GetMailSettings(company, location);
                Logger.Error($"{mailSett.from_mail} , {mailSett.password}");
                objMailSettingsDAL.SendEmail(sbMailContent, mailSett, "jithin");
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                throw;
            }
        }

    }
}
