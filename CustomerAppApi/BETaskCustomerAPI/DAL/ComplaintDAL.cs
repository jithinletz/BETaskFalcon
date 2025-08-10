using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BETaskAPI.Models;
using System.Data.SqlClient;
using System.Data;
namespace BETaskAPI.DAL
{
    /// <summary>
    /// <summary>
    /// AUTHOR : PRAKASH TMR
    /// DISC: all functions related to  Complaints
    /// DATE: 10-12-2021
    /// </summary> 
    /// </summary>
    public class ComplaintDAL
    {        
        DataTable tblData = new DataTable();

        SqlServerDBConnectionLogic SqlServerDBConnectionLogic = new SqlServerDBConnectionLogic();
        public List<Complaint> GetComplaintDetails(string company, string location, int complaintId)
        {
            List<Complaint> lstComplaintDetails = new List<Complaint>();
            try
            {
                SqlServerDBConnectionLogic.SqlServerDBConnection(company, location);
                using (SqlCommand cmd = new SqlCommand("APP_GetComplaintDetailsById", SqlServerDBConnectionLogic.Maincn))
                {
                    cmd.Parameters.AddWithValue("@complaintId", complaintId);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Complaint item = new Complaint();
                                item.ComplaintId = Convert.ToInt32(reader["complaint_id"]);
                                item.CustomerName = Convert.ToString(reader["customer_name"]);
                                item.Message = Convert.ToString(reader["message"]);
                                item.UserId = Convert.ToInt32(reader["userid"]);
                                item.Mobile = Convert.ToString(reader["mobile"]);
                                item.Email = Convert.ToString(reader["email"]);
                                item.Status = Convert.ToInt32(reader["status"]);
                                item.Date = Convert.ToDateTime(reader["complaint_date"]);
                                item.ComplaintType = Convert.ToString(reader["complaint_type"]);
                                lstComplaintDetails.Add(item);
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return lstComplaintDetails;
        }

        public bool SaveComplaint(Complaint objComplaint ,string company, string location)
        {
                bool spStasus = false;
                List<Complaint> lstComplaintDetails = new List<Complaint>();
                SqlServerDBConnectionLogic.SqlServerDBConnection(company, location);
           
                try
                {
                    SqlCommand cmd= new SqlCommand("APP_SaveComplaint", SqlServerDBConnectionLogic.Maincn);
                    cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@ComplaintId",Convert.ToInt32(objComplaint.ComplaintId));
                    cmd.Parameters.AddWithValue("@CustomerName", Convert.ToString(objComplaint.CustomerName));
                    cmd.Parameters.AddWithValue("@Mobile", Convert.ToString(objComplaint.Mobile));
                    cmd.Parameters.AddWithValue("@UserId", Convert.ToString(objComplaint.UserId));
                    cmd.Parameters.AddWithValue("@Email", Convert.ToString(objComplaint.Email));
                    cmd.Parameters.AddWithValue("@Message", Convert.ToString(objComplaint.Message));
                    cmd.Parameters.AddWithValue("@ComplaintType", Convert.ToString(objComplaint.ComplaintType));
                    cmd.Parameters.AddWithValue("@Status", Convert.ToInt32(objComplaint.Status));

                    int output=cmd.ExecuteNonQuery();
                if (output == 1) { spStasus = true; } else { spStasus = false; };
                }
                catch(Exception ex)
                {
                    return spStasus;
                }
            return spStasus;
        }
    }
}