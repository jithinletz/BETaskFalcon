using System;
using EDMX = BETask.DAL.EDMX;
using REP = BETask.DAL.DAL;
using BETask.Model;
using BETask.Common;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using RPT = BETask.Report.ReportForm;
using BETask.DAL.DAL;

namespace BETask.BAL
{
    public class SaleMergingBAL
    {
        BETask.DAL.DAL.SaleMergingDAL saleDAL = new REP.SaleMergingDAL();

        public List<EDMX.sales> GetAllSales(int customerId, int divisionId, DateTime fromDate, DateTime toDate)
        {
            try
            {
                return saleDAL.GetAllSales(customerId, divisionId, fromDate, toDate);
            }
            catch(Exception ex)
            {
                throw ex; 

            }
        }

        public int SalesMerge(List<int> listSale, int customerId,int divisionId)
        {
            try
            {
                return saleDAL.SalesMerge(listSale,customerId, divisionId);
            }
            catch
            {
                throw;
            }
        }
            
        
       
        
    }   
}
