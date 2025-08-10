using BETask.DAL.DAL;
using BETask.Model;
using EDMX = BETask.DAL.EDMX;
using EDMXApp = BETask.APP.EDMX;
using System;
using System.Collections.Generic;
using System.Data;
using RPT = BETask.Report.ReportForm;
using BETask.Common;
using System.Diagnostics;

namespace BETask.BAL
{
    public class CustomerAggrementBAL
    {
        public CustomerAggrementDAL objCustomerAggrement = new CustomerAggrementDAL();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="purchase"></param>
        public void SaveCustomerAggrement(List<EDMX.customer_aggrement> items, int customerId)
        {
            try
            {
                objCustomerAggrement.SaveCustomerAggrement(items, customerId);
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
                    reference_id = customerId,
                    summary = $" Updating Customer Agrrement Customerr Id {customerId} Items Count {items.Count}",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
        }
        public void UpdateCustomerAgreementFromAsset(EDMX.customer_asset asset)
        {
            try
            {
                decimal qty = 0;
                if (asset.delivery_type.ToLower() == "return")
                    qty = asset.qty * -1;
                else
                    qty = asset.qty;

                EDMX.customer_aggrement agreement = new EDMX.customer_aggrement
                {
                    customer_id = asset.customer_id,
                    item_id = asset.item_id,
                    max_qty = qty,
                    unit_price = Convert.ToDecimal(asset.amount),
                    remarks = asset.remarks,
                    serail_number = asset.barcode,
                    show_app = 1,
                    status = 1
                };
                objCustomerAggrement.UpdateCustomerAggrementFromAsset(agreement);

            }
            catch (Exception ex)
            {
                throw new Exception("Asset updated , Error while updating customer agreement");
            }
        }
        public void CloseCustomerAgreement(int customerId)
        {
            try
            {
                objCustomerAggrement.CloseAgreement(customerId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error while update Customer Agreement");
            }
        }

        public void CloseCustomerAgreementItem(DAL.EDMX.customer_asset asset)
        {
            try
            {
                var agreements = objCustomerAggrement.CloseAgreementItem(asset);
            }
            catch (Exception ex)
            {
                throw new Exception("Error while update Customer Agreement");
            }
        }

        public List<EDMXApp.customer_aggrement_temp> GetCustomerAgreementTemp(int customerId)
        {
            try
            {
                APP.DAL.CustomerAppDAL customerApp = new APP.DAL.CustomerAppDAL();
                return customerApp.GetCustomerAgreementTemp(customerId);
            }
            catch { throw; }
        }

        public void RemoveAgreement(int agreementId, int customerId)
        {
            try
            {
             
                objCustomerAggrement.RemoveAgreement(agreementId, customerId);
                EDMX.customer_aggrement aggrement = objCustomerAggrement.GetSingleAgreementById(agreementId);
                //BETask.APP.DAL.CustomerAppDAL customerAppDAL = new APP.DAL.CustomerAppDAL();
                //customerAppDAL.RemoveAgreement(aggrement.item_id, aggrement.max_qty, customerId);

            }
            catch { throw; }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public List<EDMX.customer_aggrement> GetCustomerAggrements(int customerId, int status = 1)
        {
            try
            {
                return objCustomerAggrement.GetCustomerAggrements(customerId,status);
            }
            catch { throw; }
        }


    }
}
