using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace BETaskAPI.DAL
{
    /// <summary>
    /// AUTHOR : PRAKASH TMR    
    /// DATE: 09-12-2021
    /// </summary>
    public class SqlServerDBConnectionLogic
    {       
   
     static SqlConnection mMaincn = new SqlConnection();

        public static SqlConnection Maincn
        {
            get { return mMaincn; }
        }

        /// <summary>
        /// AUTHOR : PRAKASH TMR
        /// DISC: open DB Connection depnds Company and Branch
        /// DATE: 09-12-2021
        /// </summary> 
        /// 

        public bool SqlServerDBConnection(string Customer,string Location)
        {
            try
            {
                string strConnection = string.Empty;
                // Look for the connection String in the connectionStrings section.
                if (string.IsNullOrEmpty(Location))
                    strConnection = GetConnectionStringByName(Customer);
                else
                    strConnection = GetConnectionStringByName($"{Customer}_{Location}");

                // open Sql Connection using connection String

                return OpenDbConnection(strConnection);
            }
            catch
            {
                return false;

            }
           
        }

        /// <summary>
        /// AUTHOR : PRAKASH TMR
        /// DISC: GET CONNECTION STRING DEPENDS COMPANY AND BRANCH
        /// DATE: 09-12-2021
        /// </summary>

        static string GetConnectionStringByName(string CompanyName)
        {

            // Assume failure.
            string returnValue = string.Empty;
            try
            {
                
                // Look for the name in the connectionStrings section.
                System.Configuration.ConnectionStringSettings settings = System.Configuration.ConfigurationManager.ConnectionStrings[CompanyName.Trim()];

                // If found, return the connection string.
                if (settings != null)
                    returnValue = settings.ConnectionString;
                else
                {
                    switch (CompanyName)
                    {
                        case "FALCON_UAE":
                            CompanyName = "Falcon_UAE";
                            break;
                    }


                    settings = System.Configuration.ConfigurationManager.ConnectionStrings[CompanyName];

                    // If found, return the connection string.
                    if (settings != null)
                        returnValue = settings.ConnectionString;
                }

            }
            catch (Exception ex)
            {
                throw;
            }
            return returnValue;
        }

        public bool OpenDbConnection(String strConnection)
        {
            try
            {
                //SqlConnection conn = new SqlConnection(strConnection);
                if (CloseDbConnection())
                {
                    mMaincn = new SqlConnection(strConnection);
                    mMaincn.Open();
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public bool CloseDbConnection()
            {
                try
                {
                    if (Maincn.State == System.Data.ConnectionState.Open)
                        Maincn.Close();

                }
                catch (Exception ex)
                {
                return false;
                }
            return true;
        }
         
           
    }
}