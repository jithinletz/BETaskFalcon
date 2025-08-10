using System;
using EDMX = BETask.DAL.EDMX;
using REP = BETask.DAL.DAL;
using BETask.Model;
using BETask.Common;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace BETask.BAL
{

    class AccountGroupBAL
    {
        REP.AccountGroupDAL accountGroupRep = new REP.AccountGroupDAL();
        public void SaveAccountGroup(AccountGroupModel accountGroupModel)
        {
            try
            {
                
                EDMX.account_group accountDAL = new EDMX.account_group()
                {
                    company_id = General.companyId,
                    location_id = General.locationId,
                    group_id =  accountGroupModel.Group_id,
                    group_name = accountGroupModel.Group_name,
                    description = accountGroupModel.Description,
                    parent_id = accountGroupModel.Parent_id,
                    status = 1
                };
                accountGroupRep.SaveAccountGroup(accountDAL);
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
                    summary = $" Saving of Account Group { accountGroupModel.Group_name}",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
        }
        public List<AccountGroupModel> GetAllAccountGroup()
        {
            List<AccountGroupModel> listAccountGroup = new List<AccountGroupModel>();
            try
            {
                List<EDMX.account_group> listAccountGroupDAL = accountGroupRep.GetAllAccountGroup(General.companyId,General.locationId);
                foreach (EDMX.account_group account in listAccountGroupDAL)
                {
                    listAccountGroup.Add(new AccountGroupModel()
                    {
                        Group_id=account.group_id,
                        Group_name=account.group_name,
                        Parent_id=account.parent_id,
                       
                    });
                }

            }
            catch (Exception ee)
            {
                throw;
            }
            return listAccountGroup;
        }
    }
}
