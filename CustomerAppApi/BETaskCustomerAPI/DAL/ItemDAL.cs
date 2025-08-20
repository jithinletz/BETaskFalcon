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
    /// DISC: all functions related to item table
    /// DATE: 10-12-2021
    /// </summary> 
    /// </summary>
    public class ItemDAL
    {
        DataTable tblData = new DataTable();
        SqlServerDBConnectionLogic SqlServerDBConnectionLogic = new SqlServerDBConnectionLogic();
        public List<Item> GetItemDetails(string company, string location, string itemName)
        {
            List<Item> lstItemDetails = new List<Item>();
            try
            {
                SqlServerDBConnectionLogic.SqlServerDBConnection(company, location);
                using (SqlCommand cmd = new SqlCommand("APP_GetItemDetails", SqlServerDBConnectionLogic.Maincn))
                {
                    cmd.Parameters.AddWithValue("@itemName", itemName);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Item itm = new Item();
                                itm.ItemId = reader.IsDBNull(reader.GetOrdinal("item_id")) ? 0 : Convert.ToInt32(reader["item_id"]);
                                itm.ItemName = reader.IsDBNull(reader.GetOrdinal("item_Name")) ? string.Empty : Convert.ToString(reader["item_Name"]);
                                itm.Price = reader.IsDBNull(reader.GetOrdinal("price")) ? 0 : Convert.ToInt32(reader["price"]);
                                itm.Currency = reader.IsDBNull(reader.GetOrdinal("currency")) ? string.Empty : Convert.ToString(reader["currency"]);
                                itm.ImagePath = reader.IsDBNull(reader.GetOrdinal("image_path")) ? string.Empty : Convert.ToString(reader["image_path"]);
                                itm.Description = reader.IsDBNull(reader.GetOrdinal("description")) ? string.Empty : Convert.ToString(reader["description"]);
                                lstItemDetails.Add(itm);
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {

            }
            return lstItemDetails;
        }

        public List<Item> GetItemDetailsByCategory(string company, string location, string categoryName)
        {
            List<Item> lstItemDetails = new List<Item>();
            SqlServerDBConnectionLogic.SqlServerDBConnection(company, location);
            try
            {
                using (SqlCommand cmd = new SqlCommand("APP_GetItemDetailsByCategory", SqlServerDBConnectionLogic.Maincn))
                {
                    cmd.Parameters.AddWithValue("@categoryName", categoryName);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Item itm = new Item();
                                itm.ItemId = Convert.ToInt32(reader["item_id"]);
                                itm.ItemName = Convert.ToString(reader["item_Name"]);
                                itm.Price = Convert.ToInt32(reader["price"]);
                                itm.Currency = Convert.ToString(reader["currency"]);
                                itm.ImagePath = Convert.ToString(reader["image_path"]);
                                lstItemDetails.Add(itm);
                            }
                        }
                    }

                }
                return lstItemDetails;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public Item GetItemDetailsById(string company, string location, int itemId)
        {
            Item objItemDetails = new Item();
            try
            {
                SqlServerDBConnectionLogic.SqlServerDBConnection(company, location);
                using (SqlCommand cmd = new SqlCommand("APP_GetItemDetailsByItemId", SqlServerDBConnectionLogic.Maincn))
                {
                    cmd.Parameters.AddWithValue("@itemId", itemId);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {

                                objItemDetails.ItemId = Convert.ToInt32(reader["item_id"]);
                                objItemDetails.ItemName = Convert.ToString(reader["item_Name"]);
                                objItemDetails.Price = Convert.ToInt32(reader["price"]);
                                objItemDetails.Currency = Convert.ToString(reader["currency"]);
                                objItemDetails.ImagePath = Convert.ToString(reader["image_path"]);

                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {

            }
            return objItemDetails;
        }

    }
}