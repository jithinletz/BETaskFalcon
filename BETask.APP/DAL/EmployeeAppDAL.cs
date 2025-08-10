using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BETask.APP.EDMX;
using System.Data.Entity;
using System.Data;


namespace BETask.APP.DAL
{
   public class EmployeeAppDAL
    {
        public void SaveEmployee(employee _employee)
        {
            try
            {
                using (var context = new betaskdbEntitiesAPP())
                {
                    var xCustomer = context.employee.Where(x => x.employee_id == _employee.employee_id).FirstOrDefault();
                    if (xCustomer == null)
                    {
                        context.Entry(_employee).State = EntityState.Added;
                    }
                    else
                    {
                        xCustomer.username = _employee.username;
                        xCustomer.password = _employee.password;
                        xCustomer.phone = _employee.phone;
                        xCustomer.route_id = _employee.route_id;
                        xCustomer.first_name = _employee.first_name;
                        xCustomer.last_name = _employee.last_name;
                        xCustomer.status = _employee.status;

                        xCustomer.department = _employee.department;
                        xCustomer.designation = _employee.designation;
                        xCustomer.dob = _employee.dob;
                        xCustomer.resign_date = _employee.resign_date;
                        xCustomer.email = _employee.email;
                        xCustomer.other_details = _employee.other_details;
                        xCustomer.gender = _employee.gender;
                        context.Entry(xCustomer).State = EntityState.Modified;
                    }

                    context.SaveChanges();

                }
            }
            catch(Exception ee)
            {
                string ss = ee.ToString();
                throw;
            }
        }
    }
}
