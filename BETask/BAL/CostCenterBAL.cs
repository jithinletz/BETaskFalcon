using System;
using EDMX = BETask.DAL.EDMX;
using REP = BETask.DAL.DAL;
using BETask.Model;
using BETask.Common;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using BETask.Views;

namespace BETask.BAL
{
    public class CostCenterBAL
    {
        DAL.DAL.CostCenterDAL costCenterDAL = new DAL.DAL.CostCenterDAL();
        public int SaveCostCenter(EDMX.cost_center cost)
        {
            try
            {
                return costCenterDAL.SaveCostCenter(cost);
            }
            catch
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
                    summary = $" Saving of Cost center { cost.cost_center_name} Id ={cost.cost_center_id}",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
        }
        public List<EDMX.cost_center> GetAllCostCenter(int primaryCostCenter)
        {
            return costCenterDAL.GetAllCostCenter(primaryCostCenter);
        }
        public EDMX.cost_center GetCostCenter(int costCenterId, out string parentName)
        {
            return costCenterDAL.GetCostCenter(costCenterId,out parentName);
        }

        public int SaveCostCenterTransaction(EDMX.cost_center_transaction cost)
        {
            try
            {
                costCenterDAL.SaveCostCenterTransaction(cost);
                return cost.entry_id;

            }
            catch (Exception ex)
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
                    reference_id = cost.entry_id,
                    summary = $" Saving of Cost Center {cost}",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }


        }
        public int DeleteTransactionByEntryId(int entryId)
        {
            try
            {
                return costCenterDAL.DeleteTransactionByEntryId(entryId);
            }
            catch (Exception ex)
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
                    reference_id = entryId,
                    summary = $" Deleting of Cost Center {entryId}",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
        }
        public int DeleteTransactionByGUID(string guid)
        {
            try
            {
                return costCenterDAL.DeleteTransactionByGUID(guid);
            }
            catch (Exception ex)
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
                    summary = $" Deleting of Cost Center {guid}",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
        }
        public bool ValidateCostCenterAdded(DataGridView dgItems,string ledgerColumn= "clmLedger")
        {
            bool resp = true;
            if (dgItems.Rows.Count > 0)
            {
                try
                {
                    foreach (DataGridViewRow row in dgItems.Rows)
                    {
                        int ledgerId = Convert.ToInt32(row.Cells["clmLedgerId"].Value);
                        BAL.AccountLedgerBAL accountLedgerBAL = new BAL.AccountLedgerBAL();
                        if (accountLedgerBAL.IsCostCenterEnabled(ledgerId))
                        {
                            string _guid = Convert.ToString(row.Cells["clmCostEntryId"].Value);
                            string ledgerName = row.Cells[ledgerColumn].Value.ToString();
                            if (string.IsNullOrEmpty(_guid))
                            {
                                General.ShowMessage(General.EnumMessageTypes.Error, $" No cost center transaction added for the ledger {row.Cells[ledgerColumn].Value}");

                                //if (General.ShowMessageConfirm($"Cost center not added for {ledgerName}. Are you sure want to continue") == DialogResult.Yes)
                                //    resp = true;

                                //else
                                resp = false;
                                return resp;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return resp;
        }

        public bool ValidateCostCenter(List<DAL.EDMX.account_transaction> listTransaction)
        {
            try
            {
                if (listTransaction[0].transaction_number > 0)
                    return costCenterDAL.ValidateCostCenter(listTransaction);
                else return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
