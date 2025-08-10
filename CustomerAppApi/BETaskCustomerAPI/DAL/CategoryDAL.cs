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
    /// DISC: all functions related to Item Category
    /// DATE: 10-12-2021
    /// </summary> 
    /// </summary>
    public class CategoryDAL
    {      
        DataTable tblData = new DataTable();
        SqlServerDBConnectionLogic SqlServerDBConnectionLogic = new SqlServerDBConnectionLogic();
        public List<Category> GetCategoryImagePath(string company, string location)
        {
            List<Category> lstCategory = new List<Category>();
            try
            {
                SqlServerDBConnectionLogic.SqlServerDBConnection(company, location);
                using (SqlCommand cmd = new SqlCommand("APP_GetCategoryImagePath", SqlServerDBConnectionLogic.Maincn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Category Category = new Category();
                                Category.CategoryName = Convert.ToString(reader["category"]);
                                Category.CategoryImagePath = Convert.ToString(reader["category_image_path"]);
                                lstCategory.Add(Category);
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
            }
            return lstCategory;
        }
    }
}