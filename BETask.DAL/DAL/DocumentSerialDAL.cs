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
    public class DocumentSerialDAL
    {
        public enum EnumDocuments { SALE, ACCOUNT,PAYMENT,RECIEPT,DO,DOINV,AGREEMENT,DOSALE,PETTY,JOURNAL };//DOSALE for DO Delivery
        public static string GetNextDocument(string docType)
        {
            int nextSerial = 0;
            string nextSerialWithPrefix = string.Empty;
            try
            {
                using (var context = new betaskdbEntities())
                {
                    var doc = context.document_serial.Where(x => x.document_type == docType).FirstOrDefault();
                    nextSerial = doc.next_number;
                    if (doc.prefix != null)
                    {
                        nextSerialWithPrefix = String.Format("{0}{1}", doc.prefix, doc.next_number);
                    }
                    else nextSerialWithPrefix = doc.next_number.ToString();
                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return nextSerialWithPrefix;
        }

        public static string GetNextDocument(string docType,bool padding)
        {
            int nextSerial = 0;
            string nextSerialWithPrefix = string.Empty;
            try
            {
                using (var context = new betaskdbEntities())
                {
                    var doc = context.document_serial.Where(x => x.document_type == docType).FirstOrDefault();
                    nextSerial = doc.next_number;
                    string withpadding = nextSerial.ToString().PadLeft(5, '0');
                    if (doc.prefix != null)
                    {
                        nextSerialWithPrefix = String.Format("{0}{1}", doc.prefix, withpadding);
                    }
                    else nextSerialWithPrefix = doc.next_number.ToString();
                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return nextSerialWithPrefix;
        }


        public string GetNextDocument(string docType, betaskdbEntities context)
        {
            int nextSerial = 0;
            string nextSerialWithPrefix = string.Empty;
            try
            {
                // using (var context = new betaskdbEntities())
                {
                    var doc = context.document_serial.Where(x => x.document_type == docType).FirstOrDefault();
                    if (doc != null)
                    {
                        nextSerial = doc.next_number;

                        if (doc.prefix != null)
                        {
                            nextSerialWithPrefix = String.Format("{0}{1}", doc.prefix, doc.next_number);
                        }
                        else nextSerialWithPrefix = doc.next_number.ToString();
                    }
                    else
                    {
                        throw new Exception("No document serial found");
                    }
                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return nextSerialWithPrefix;
        }
        public void UpdateNextDocument(Enum docType, betaskdbEntities context)
        {
            try
            {
                string docTypeEnum = docType.ToString();
                document_serial doc = context.document_serial.Where(d => d.document_type == docTypeEnum).FirstOrDefault();
                doc.next_number = doc.next_number + 1;
                context.Entry(doc).State = EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ee)
            {
                throw;
            }
        }
    }
}
