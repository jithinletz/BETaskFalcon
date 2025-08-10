using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BETask.DAL.EDMX;
using System.Data.Entity;
using System.Data;


namespace BETask.DAL.DAL
{
    public class ProductionDAL
    {
        public void SaveProduction_Mapping(production_mapping productionMap, List<production_mapping_rowmaterial> listRawmaterial)
        {
            try
            {
                using (var context = new betaskdbEntities())
                {
                    context.Entry(productionMap).State = productionMap.mapping_id == 0 ? EntityState.Added : EntityState.Modified;
                    context.SaveChanges();
                    SaveProduction_Mapping_Rawmaterail(productionMap, listRawmaterial);

                }
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        private void SaveProduction_Mapping_Rawmaterail(production_mapping productionMap, List<production_mapping_rowmaterial> listRawmaterial)
        {
            try
            {
                using (var context = new betaskdbEntities())
                {
                    List<production_mapping_rowmaterial> _listRawmaterial = context.production_mapping_rowmaterial.Where(x => x.mapping_id == productionMap.mapping_id).OrderBy(x => x.item.item_name).ToList();
                    context.production_mapping_rowmaterial.RemoveRange(_listRawmaterial);
                    context.SaveChanges();

                    foreach (production_mapping_rowmaterial raw in listRawmaterial)
                    {
                        raw.mapping_id = productionMap.mapping_id;
                        context.Entry(raw).State = raw.mapping_rowmaterial_id == 0 ? EntityState.Added : EntityState.Modified;
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ee)
            {
                throw;
            }
        }

        public List<production_mapping_rowmaterial> GetMappedDetails(int item_id)
        {
            List<production_mapping_rowmaterial> listRawmaterial = new List<production_mapping_rowmaterial>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    listRawmaterial = context.production_mapping_rowmaterial.Include(x => x.item).Include(x => x.item.uom_setting).Include(x => x.production_mapping).Where(x => x.status == 1 && x.production_mapping.status == 1 && x.production_mapping.item_id == item_id && x.mapping_id == x.production_mapping.mapping_id).OrderBy(x => x.item.item_name).ToList();
                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return listRawmaterial;
        }
        public List<production_mapping> GetMappedProducts()
        {
            List<production_mapping> listProducts = new List<production_mapping>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    listProducts = context.production_mapping.Include(x => x.item).Include(x => x.item.uom_setting).Where(x => x.status == 1).OrderBy(x => x.item.item_name).ToList();
                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return listProducts;
        }

        public void SaveProduction(production production, List<production_rawmaterial> listRawmaterial)
        {
            try
            {
                using (var context = new betaskdbEntities())
                {
                    using (DbContextTransaction transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            context.Entry(production).State = production.production_id == 0 ? EntityState.Added : EntityState.Modified;
                            context.SaveChanges();
                            SaveProduction_Rawmaterail(production, listRawmaterial, context);
                            ItemTransactionDAL itemTransactionDAL = new ItemTransactionDAL();
                            itemTransactionDAL.SaveItemTransaction_Production(production, context);
                            itemTransactionDAL.SaveItemTransaction_ProductionRawmaterial(listRawmaterial, context);
                            transaction.Commit();
                        }
                        catch (Exception ee)
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }

                }
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        private void SaveProduction_Rawmaterail(production production, List<production_rawmaterial> listRawmaterial, betaskdbEntities context)
        {
            try
            {
                //using (var context = new betaskdbEntities())
                {

                    foreach (production_rawmaterial raw in listRawmaterial)
                    {
                        raw.production_id = production.production_id;
                        context.Entry(raw).State = raw.production_rawmaterial_id == 0 ? EntityState.Added : EntityState.Modified;
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        public List<production> LoadProduction()
        {
            List<production> listProducts = new List<production>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    listProducts = context.production.Include(x => x.item).Include(x => x.item.uom_setting).Where(x => x.status == 1).OrderBy(x => x.item.item_name).OrderByDescending(x => x.production_date).ToList().Take(250).ToList();
                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return listProducts;
        }


        public List<production> LoadProduction_ByDate(DateTime prodDate)
        {
            List<production> listProducts = new List<production>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    listProducts = context.production.Include(x => x.item).Include(x => x.item.uom_setting).Where(x => x.status == 1 && x.production_date.Year==prodDate.Year && x.production_date.Month==prodDate.Month && prodDate.Day==prodDate.Day).OrderBy(x => x.item.item_name).OrderByDescending(x => x.production_date).ToList().Take(250).ToList();
                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return listProducts;
        }
        public List<production> SearchProduction(DateTime prodDateFrom, DateTime prodDateTo, int itemId)
        {
            List<production> listProducts = new List<production>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    var productions = context.production.Include(x => x.item).Include(x => x.item.uom_setting).Where(x => x.status == 1 && (x.production_date >= prodDateFrom && x.production_date <= prodDateTo));

                    listProducts = productions.OrderBy(x => x.production_date).ToList();
                    if (itemId > 0)
                    {
                        listProducts = productions.Where(x => x.item_id == itemId).OrderBy(x => x.production_date).ToList();
                    }
                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return listProducts;
        }

      ////  public production GetItemProductionDetails(DateTime prodDateFrom, DateTime prodDateTo, int itemId)
      //  {
      //      production Production = new production();
      //      List<production> listProducts = new List<production>();
      //      try
      //      {
      //          using (var context = new betaskdbEntities())
      //          {
      //              var productions = context.production.Include(x => x.item).Include(x => x.item.uom_setting).Where(x => x.status == 1 && x.item_id == itemId && (x.production_date >= prodDateFrom && x.production_date <= prodDateTo));

      //              listProducts = productions.OrderBy(x => x.production_date).ToList();

                   
      //          }
      //      }
      //      catch (Exception ee)
      //      {
      //          throw;
      //      }
      //      return production;
      //  }

        public List<production_rawmaterial> SearchProduction_Rawmaterial(DateTime prodDateFrom, DateTime prodDateTo, int itemId)
        {
            //   List<production_rawmaterial> listRawMaterial = new List<production_rawmaterial>();
            List<production_rawmaterial> listRawMaterial = new List<production_rawmaterial>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    //var rawmeterial = context.production_rawmaterial.Include(m=>m.production).Include(i=>i.item).Include(p=>p.item.uom_setting).Where(x=>x.production.production_date>= prodDateFrom && x.production.production_date <= prodDateTo).GroupBy(x => x.item_id).
                    //    Select(g => new { itemId = g.Key,itemName=g.Select(y=>y.item.item_name).FirstOrDefault(),packing=g.Select(p=>p.item.uom_setting.uom_name).FirstOrDefault(), qty = g.Sum(x => x.qty),itemvalue=g.Sum(x=>x.item_value) }).ToList();

                    listRawMaterial = context.production_rawmaterial.Include(m => m.production).Include(i => i.item).Include(p => p.item.uom_setting).
                        Where(x => x.production.production_date >= prodDateFrom && x.production.production_date <= prodDateTo && (itemId > 0 ? x.production.item_id == itemId : x.item_id > 0)).OrderBy(x => x.item_id).ThenBy(x => x.production.production_date).ToList();


                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return listRawMaterial;
        }
        public List<production_rawmaterial> LoadProduction_Rawmaterials(int productionId)
        {
            List<production_rawmaterial> listRawmaterial = new List<production_rawmaterial>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    listRawmaterial = context.production_rawmaterial.Include(x => x.item).Include(x => x.item.uom_setting).Where(x => x.status == 1 && x.production_id == productionId).OrderBy(x => x.item.item_name).ToList();
                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return listRawmaterial;
        }

        public void DeleteProduction(int productionId)
        {
            try
            {
                using (var context = new betaskdbEntities())
                {

                    var Production = new production()
                    {
                        production_id = productionId,
                        status = 2,
                    };

                    context.production.Attach(Production);
                    context.Entry(Production).Property(x => x.status).IsModified = true;
                    context.SaveChanges();

                    var rawmaterial = context.production_rawmaterial.Where(f => f.production_id == productionId).ToList();
                    rawmaterial.ForEach(a =>
                    {
                        a.status = 2;
                        //a.property2 = value2;
                    });
                    context.SaveChanges();

                    try
                    {
                        //Reversing stock
                        ItemTransactionDAL itemTransactionDAL = new ItemTransactionDAL();
                        production _production = context.production.Where(x => x.production_id == productionId && x.status==2).FirstOrDefault();
                        if (_production != null && _production.item_id>0)
                        {
                            itemTransactionDAL.SaveItemTransaction_Production_Delete(_production, context);

                            //Reversing Rawmeterials=
                            List<production_rawmaterial> listRawmaterial = context.production_rawmaterial.Where(x => x.production_id == productionId).ToList();
                            if (listRawmaterial != null)
                            {
                                itemTransactionDAL.SaveItemTransaction_ProductionRawmaterial_Delete(listRawmaterial, context);
                            }
                        }
                    }
                    catch { }


                }
            }
            catch
            {
                throw;

            }

        }

        public void SaveItemDamage(EDMX.item_damage itemDamage)
        {
            try
            {
                using (var context = new betaskdbEntities())
                {
                    using (DbContextTransaction transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            context.Entry(itemDamage).State = itemDamage.item_damage_id == 0 ? EntityState.Added : EntityState.Modified;
                            context.SaveChanges();
                            ItemTransactionDAL itemTransactionDAL = new ItemTransactionDAL();
                            itemTransactionDAL.SaveItemTransaction_Damage(itemDamage, context);
                            if (itemDamage.item_damage_id > 0)
                                itemTransactionDAL.SaveItemTransaction_Damage_ScrapUpdate(itemDamage, context);
                            transaction.Commit();
                        }
                        catch (Exception ee)
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch
            {
                throw;

            }
        }

        //Remove Item damage saved
        public void SaveItemDamageReturn(int damageId)
        {
            try
            {
                using (var context = new betaskdbEntities())
                {
                    using (DbContextTransaction transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            item_damage itemDamage = context.item_damage.Where(x => x.item_damage_id == damageId).FirstOrDefault();
                            ItemTransactionDAL itemTransactionDAL = new ItemTransactionDAL();
                            itemTransactionDAL.SaveItemTransaction_DamageReturn(itemDamage, context);
                            if (itemDamage.item_damage_id > 0)
                                itemTransactionDAL.SaveItemTransaction_Damage_ScrapUpdateReturn(itemDamage, context);

                            itemDamage.status = 2;
                            context.Entry(itemDamage).State = itemDamage.item_damage_id == 0 ? EntityState.Added : EntityState.Modified;
                            context.SaveChanges();
                          
                            
                            transaction.Commit();
                        }
                        catch (Exception ee)
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch
            {
                throw;

            }
        }
        public List<EDMX.item_damage> GetItemDamageByDate(DateTime date)
        {
            List<EDMX.item_damage> listItemDamage = new List<item_damage>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    listItemDamage = context.item_damage.Include(i => i.item).Include(u=>u.item.uom_setting).Include(x=>x.employee).Where(x => x.damage_date == date).ToList();
                }
            }

            catch
            {
                throw;

            }
            return listItemDamage;
        }
        public List<EDMX.item_damage> GetItemDamageReport(DateTime dateFrom, DateTime dateTo, int employeeId)
        {
            List<EDMX.item_damage> listItemDamage = new List<item_damage>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    listItemDamage = context.item_damage.Include(i => i.item).Include(x => x.employee).Where(x => x.damage_date >= dateFrom && x.damage_date<=dateTo && x.status==1).OrderBy(x=>x.damage_date).ThenBy(x=>x.employee_id).ToList();
                    if (employeeId > 0)
                        listItemDamage = listItemDamage.Where(x => x.employee_id == employeeId).ToList();
                }
            }

            catch
            {
                throw;

            }
            return listItemDamage;
        }
        public class ItemRawmaterialReport
        {
            public int ItemId { get; set; }
            public string ItemName { get; set; }
            public string Packing { get; set; }
            public decimal Qty { get; set; }
            public decimal ItemValue { get; set; }
        }
    }
}
