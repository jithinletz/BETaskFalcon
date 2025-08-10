using BETask.DAL.DAL;
using BETask.Model;
using EDMX = BETask.DAL.EDMX;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using BETask.Common;
using System.Data;
using RPT = BETask.Report.ReportForm;

namespace BETask.BAL
{
    public class ProductionBAL
    {
        ProductionDAL production = new ProductionDAL();
        public void SaveProductionMapping(EDMX.production_mapping productionMap, List<EDMX.production_mapping_rowmaterial> listRawmaterial)
        {
            try
            {
                production.SaveProduction_Mapping(productionMap, listRawmaterial);
            }
            catch { throw; }
            finally
            {
                var st = new StackTrace();
                var sf = st.GetFrame(1);
                EDMX.user_log user_Log = new EDMX.user_log()
                {
                    username = General.userName,
                    module = this.GetType().Name,
                    module_action = sf.GetMethod().Name,
                    reference_id = productionMap.item_id,
                    summary = $" Saving Production Map , Row materials={listRawmaterial.Count}  ",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
        }
        public List<EDMX.production_mapping_rowmaterial> GetMappedDetails(int item_id)
        {
            try
            {
                return production.GetMappedDetails(item_id);
            }
            catch { throw; }
        }
        public List<EDMX.production_mapping> GetMappedProducts()
        {
            try
            {
                return production.GetMappedProducts();
            }
            catch { throw; }
        }

        public void SaveProduction(EDMX.production _production, List<EDMX.production_rawmaterial> listRawmaterial)
        {
            try
            {
                production.SaveProduction(_production, listRawmaterial);
            }
            catch { throw; }
            finally
            {
                var st = new StackTrace();
                var sf = st.GetFrame(1);
                EDMX.user_log user_Log = new EDMX.user_log()
                {
                    username = General.userName,
                    module = this.GetType().Name,
                    module_action = sf.GetMethod().Name,
                    reference_id = _production.item_id,
                    summary = $" Saving Production Date={_production.production_date} Qty={_production.qty} ",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
        }
        public List<EDMX.production> GetProduction()
        {
            try
            {
                return production.LoadProduction();
            }
            catch { throw; }
        }
        public List<EDMX.production> GetProduction_ByDate(DateTime prodDate)
        {
            try
            {
                return production.LoadProduction_ByDate(General.ConvertDateServerFormat( prodDate));
            }
            catch { throw; }
        }

        public List<EDMX.production_rawmaterial> GetProductionRawmaterial(int productionId)
        {
            try
            {
                return production.LoadProduction_Rawmaterials(productionId);
            }
            catch { throw; }
        }
        public void DeleteProduction(int productionId)
        {
            try
            {
                production.DeleteProduction(productionId);
            }
            catch { throw; }
            finally
            {
                var st = new StackTrace();
                var sf = st.GetFrame(1);
                EDMX.user_log user_Log = new EDMX.user_log()
                {
                    username = General.userName,
                    module = this.GetType().Name,
                    module_action = sf.GetMethod().Name,
                    reference_id = productionId,
                    summary = $" Deleting Production ",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
        }

        public void SaveItemDamage(EDMX.item_damage itemDamage)
        {
            try
            {
                production.SaveItemDamage(itemDamage);
            }
            catch (Exception ee)
            {
                throw;
            }
            finally
            {
                var st = new StackTrace();
                var sf = st.GetFrame(1);
                EDMX.user_log user_Log = new EDMX.user_log()
                {
                    username = General.userName,
                    module = this.GetType().Name,
                    module_action = sf.GetMethod().Name,
                    reference_id = 0,
                    summary = $" ItemDamage",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
        }
        public void DamageReturn(int damageId)
        {
            try
            {
                production.SaveItemDamageReturn(damageId);
            }
            catch (Exception ee)
            {
                throw;
            }
            finally
            {
                var st = new StackTrace();
                var sf = st.GetFrame(1);
                EDMX.user_log user_Log = new EDMX.user_log()
                {
                    username = General.userName,
                    module = this.GetType().Name,
                    module_action = sf.GetMethod().Name,
                    reference_id = 0,
                    summary = $" ItemDamage Return",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
        }
        public List<EDMX.item_damage> GetItemDamageByDate(DateTime date)
        {
            try
            {
              return  production.GetItemDamageByDate(date);
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        public List<EDMX.item_damage> GetItemDamageReport(DateTime dateFrom,DateTime dateTo,int employeeId)
        {
            try
            {
                return production.GetItemDamageReport(dateFrom,dateTo,employeeId);
            }
            catch (Exception ee)
            {
                throw;
            }
        }

        #region Report
        public List<EDMX.production> SearchProduction(DateTime prodDateFrom, DateTime prodDateTo, int itemId)
        {
            try
            {
                return production.SearchProduction(prodDateFrom, prodDateTo, itemId);
            }
            catch { throw; }
        }

        public void SearchProduction_Rawmaterial(DateTime prodDateFrom, DateTime prodDateTo, int itemId)
        {
            try
            {
                List<EDMX.production_rawmaterial> listProduction= production.SearchProduction_Rawmaterial(prodDateFrom, prodDateTo, itemId);
                if (listProduction != null && listProduction.Count > 0)
                {
                    DataTable tblProduction = new DataTable();
                    BETask.Report.DSReports.ProductionReportDataTable productionReportDataTable = new Report.DSReports.ProductionReportDataTable();
                    tblProduction = productionReportDataTable.Clone();
                    foreach (EDMX.production_rawmaterial prod in listProduction)
                    {
                        DataRow row = tblProduction.NewRow();
                        row["ItemName"] = prod.item.item_name;
                        row["Packing"] = prod.item.uom_setting.uom_name;
                        row["ProductionDate"] = General.ConvertDateAppFormat(prod.production.production_date);
                        row["Qty"] = prod.qty;
                        row["Cost"] = prod.item_value;
                        tblProduction.Rows.Add(row);
                    }
                    if (tblProduction != null && tblProduction.Rows.Count > 0)
                    {
                        RPT reportForm = new RPT(RPT.EnumReportType.ProductionRawmaterialReport, "", tblProduction);
                        reportForm.Show();
                    }
                }
            }
            catch { throw; }
        }

        public void Print(DateTime prodDateFrom, DateTime prodDateTo, int itemId)
        {
            List<EDMX.production> listProduction = SearchProduction(prodDateFrom, prodDateTo, itemId);
            if (listProduction != null && listProduction.Count > 0)
            {
                DataTable tblProduction = new DataTable();
                BETask.Report.DSReports.ProductionReportDataTable productionReportDataTable = new Report.DSReports.ProductionReportDataTable();
                tblProduction = productionReportDataTable.Clone();
                foreach (EDMX.production prod in listProduction)
                {
                    DataRow row = tblProduction.NewRow();
                    row["ItemName"] = prod.item.item_name ;
                    row["Packing"] = prod.item.uom_setting.uom_name;
                    row["ProductionDate"] = General.ConvertDateAppFormat(prod.production_date);
                    row["Qty"] = prod.qty;
                    row["Cost"] = prod.cost;
                    tblProduction.Rows.Add(row);
                }
                if (tblProduction != null && tblProduction.Rows.Count > 0)
                {
                    RPT reportForm = new RPT(RPT.EnumReportType.ProductionReport, "", tblProduction);
                    reportForm.Show();
                }
            }
        }

        public void PrintDamageReport(DateTime dateFrom, DateTime dateTo, int employeeId,string header)
        {
            List<EDMX.item_damage> listDamage = GetItemDamageReport(dateFrom, dateTo, employeeId);
            if (listDamage != null && listDamage.Count > 0)
            {
                DataTable tblDamage = new DataTable();
                BETask.Report.DSReports.DamageReportDataTable damageReportDataTable = new Report.DSReports.DamageReportDataTable();
                tblDamage = damageReportDataTable.Clone();
                foreach (EDMX.item_damage prod in listDamage)
                {
                    DataRow row = tblDamage.NewRow();
                    row["Date"] = General.ConvertDateAppFormat(prod.damage_date);
                    row["Item"] =prod.item.item_name;
                    row["Employee"] =$"{prod.employee.first_name} {prod.employee.last_name}";
                    row["Qty"] = prod.qty;
                    row["Remarks"] = prod.remarks;
                    tblDamage.Rows.Add(row);
                }
                if (tblDamage != null && tblDamage.Rows.Count > 0)
                {
                    RPT reportForm = new RPT(RPT.EnumReportType.DamageReport, header, tblDamage);
                    reportForm.Show();
                }
            }
        }

        #endregion Report



    }
}
