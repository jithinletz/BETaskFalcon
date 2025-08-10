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
    /// DISC: all functions related to Company table
    /// DATE: 10-12-2021
    /// </summary> 
    /// </summary>
    public class CompanyDAL
    {
        SqlServerDBConnectionLogic SqlServerDBConnectionLogic = new SqlServerDBConnectionLogic();
        DataTable tblData = new DataTable();

        public List<string> GetCompanyLocations(string company)
        {
            List<string> lstLocations = new List<string>();
            try
            {
                SqlServerDBConnectionLogic.SqlServerDBConnection(company, "");

                using (SqlCommand cmd = new SqlCommand("APP_GetCompanyLocation", SqlServerDBConnectionLogic.Maincn))
                {
                    cmd.Parameters.AddWithValue("@companyName", company);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                lstLocations.Add($"{Convert.ToString(reader["city"])}-{Convert.ToString(reader["whatsapp"])}" );
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return lstLocations;
        }

        public List<Company> GetCompanyDetails(string companyName, string location)
        {
            List<Company> lstCompanyDetails = new List<Company>();
            try
            {
                SqlServerDBConnectionLogic.SqlServerDBConnection(companyName, location);

                using (SqlCommand cmd = new SqlCommand("APP_GetCompanyDetails", SqlServerDBConnectionLogic.Maincn))
                {
                    cmd.Parameters.AddWithValue("@companyName", companyName);
                    cmd.Parameters.AddWithValue("@location", location);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Company item = new Company();
                                item.CompanyId = Convert.ToInt32(reader["company_id"]);
                                item.CompanyName = Convert.ToString(reader["company_name"]);
                                item.Address1 = Convert.ToString(reader["address1"]);
                                item.Address2 = Convert.ToString(reader["address2"]);
                                item.City = Convert.ToString(reader["city"]);
                                item.Mobile = Convert.ToString(reader["mobile"]);
                                item.Phone = Convert.ToString(reader["Phone"]);
                                item.Email = Convert.ToString(reader["Email"]);
                                item.Web = Convert.ToString(reader["web"]);
                                item.Facebook = Convert.ToString(reader["facebook"]);
                                item.Whatsapp = Convert.ToString(reader["whatsapp"]);
                                item.Instagram = Convert.ToString(reader["instagram"]);
                                item.Twitter = Convert.ToString(reader["twitter"]);
                                item.POBox = Convert.ToString(reader["pobox"]);
                                lstCompanyDetails.Add(item);
                            }
                        }
                    }

                }

                
            }
            catch (Exception ex)
            {
                return null;
            }
            return lstCompanyDetails;
        }

    }
}