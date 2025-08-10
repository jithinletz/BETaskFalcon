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
    public class AccountGroupDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="account"></param>
        public void SaveAccountGroup(account_group account)
        {
            try
            {
                using (var context = new betaskdbEntities())
                {
                    context.Entry(account).State = account.group_id == 0 ? EntityState.Added : EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<account_group> GetAllAccountGroup(int company_id,int location_id)
        {
            List<account_group> listAccountGroup = new List<account_group>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                   listAccountGroup = context.account_group.Where( x => x.status == 1 && x.company_id==company_id&&x.location_id==location_id).OrderBy(x => x.group_id).ToList();
                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return listAccountGroup;
        }
        public List<account_group> GetAllAccountGroupHasLedger(int company_id, int location_id)
        {
            List<account_group> listAccountGroup = new List<account_group>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    //listAccountGroup = context.account_group.Include(l=>l.account_ledger).Where(x => x.status == 1 && x.company_id == company_id && x.location_id == location_id ).OrderBy(x => x.group_name).ToList();

                    listAccountGroup = (from ag in context.account_group
                                                         where context.account_ledger.Any(al => al.group_id == ag.group_id)
                                                         select ag).OrderBy(x=>x.group_name).ToList();
                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return listAccountGroup;
        }
        public int GetGroupLevel(int groupId)
        {
            int level = 1;
            try
            {
                using (var context = new betaskdbEntities())
                {
                   int parentId= context.account_group.Where(x => x.group_id == groupId).FirstOrDefault().parent_id;
                    if (parentId > 0)
                    {
                        level++;
                        account_group acc = context.account_group.Where(x => x.group_id == parentId).FirstOrDefault();
                        while (acc.parent_id !=0)
                        {
                            if (acc != null)
                            {
                                level++;
                                acc = context.account_group.Where(x => x.group_id == acc.parent_id).FirstOrDefault();
                            }
                        }
                       
                    }


                }
            }
            catch
            {
                throw;
            }
            return level;
        }

    }
}
