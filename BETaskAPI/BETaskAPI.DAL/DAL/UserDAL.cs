using BETaskAPI.DAL.EDMX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BETaskAPI.DAL.DAL
{
    public class UserDAL
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// 

        public string TestAPI()
        {
            string result = "";
            try
            {
                using (var context = new betaskdbEntities())
                {
                    route _route = context.route.FirstOrDefault();
                    if (_route != null)
                        result = _route.route_name.ToString();



                }
            }
            catch
            {
                throw;
            }
            return result;
        }

        public employee ValidateUser(string userName, string password)
        {
            string result = string.Empty;
            employee objUser = null;
            try
            {
                using (var context = new betaskdbEntities())
                {
                    objUser = context.employee.Where(x => x.username == userName && x.password == password).FirstOrDefault();
                    if (objUser != null)
                    {
                        result = String.Format("{0} {1}", objUser.first_name, objUser.last_name);
                    }
                }
            }
            catch
            {
                throw;
            }

            return objUser;
        }
        public company GetCompany()
        {
            company _company = new company();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    _company = context.company.Where(x => x.status == 1).FirstOrDefault();


                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return _company;
        }
    }
}
