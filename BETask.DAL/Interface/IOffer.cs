using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BETask.DAL.EDMX;
using System.Data.Entity;
using System.Data;
using BETask.DAL.Model;
using System.Data.SqlClient;
using BETask.DAL.DAL;

namespace BETask.DAL.Interface
{
    interface IOffer
    {
        void SaveOffer(offer offer);
        List<offer> GetOffers(betaskdbEntities _context = null);
         void RemoveOffer(int offerId);
    }
}
