using BETaskAPI.DAL.EDMX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;


namespace BETaskAPI.DAL.DAL
{
  public  class UploadDAL
    {
        public void UploadDocument(customer_upload upload)
        {
            try
            {
                using (var context = new betaskdbEntities())
                {
                    context.Entry(upload).State = upload.document_id == 0 ? EntityState.Added : EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
