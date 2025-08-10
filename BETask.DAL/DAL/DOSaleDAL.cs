using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BETask.DAL.EDMX;
using System.Data.Entity;
using System.Data;
using BETask.DAL.Model;
using System.Data.Entity.Validation;

namespace BETask.DAL.DAL
{
    public class DOSaleDAL
    {
        /// <summary>
        /// /Prakash tmr added 
        /// DO sales from search Table
        /// </summary>
        /// 
        /// <returns></returns>
        /// 
        public List<sales> SearchDOinSales(DateTime dateFrom, DateTime dateTo, int routeId, int groupId = 0)
        {
            List<sales> listSale = new List<sales>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    UpdateSaleDONumberForDoNotGenerated(dateFrom, dateTo, context);

                    // Query that returns SalesDTO instead of sales
                    var templistSale = context.sales
                        .Where(x => x.do_id == null && x.do_number != null &&
                                    x.sales_date >= dateFrom && x.sales_date <= dateTo &&
                                    x.payment_mode == "DO" && x.status == 1 &&
                                    (routeId == 0 ? x.customer.route_id > 0 : x.customer.route_id == routeId)
                                    && (groupId == 0 ? x.customer.group_id == 0 : x.customer.group_id == groupId)) // Filter by route
                        .GroupBy(g => new { g.customer_id })
                        .Select(s => new SalesDTO
                        {
                            CustomerId = s.Key.customer_id,
                            CustomerName = s.Max(gs => gs.customer.customer_name),
                            SalesDate = s.Max(gs => gs.sales_date),
                            PaymentMode = s.Max(gs => gs.payment_mode),
                            Status = s.Max(gs => gs.status),
                            TotalDiscount = s.Sum(gs => gs.total_discount),
                            TotalBeforeVat = s.Sum(gs => gs.total_beforevat),
                            TotalVat = s.Sum(gs => gs.total_vat),
                            NetAmount = s.Sum(gs => Math.Round(gs.net_amount, 2)),
                            GrossAmount = s.Sum(gs => Math.Round(gs.gross_amount, 2))
                        }).ToList(); // Get as list of SalesDTO

                    // Convert the SalesDTO objects to sales objects
                    foreach (var dto in templistSale)
                    {
                        listSale.Add(new sales
                        {
                            customer_id = dto.CustomerId,
                            remarks = dto.CustomerName, // Temporarily store the customer name in remarks
                            sales_date = dto.SalesDate,
                            payment_mode = dto.PaymentMode,
                            gross_amount = dto.GrossAmount,
                            total_discount = dto.TotalDiscount,
                            total_beforevat = dto.TotalBeforeVat,
                            total_vat = dto.TotalVat,
                            net_amount = dto.NetAmount,
                            status = dto.Status
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return listSale;
        }

        public List<sales> SearchDOinSalesOl(DateTime dateFrom, DateTime dateTo, int routeId)
        {
            List<sales> listSale = new List<sales>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    UpdateSaleDONumberForDoNotGenerated(dateFrom, dateTo, context);
                    //listSale = context.sales.Include(x => x.customer).Where(x => x.sales_date >= dateFrom && x.sales_date <= dateTo && x.payment_mode =="DO" && x.status == 1).ToList();

                    List<sales> listSaleTemp = context.sales.Where(x => x.do_id == null && x.do_number != null && x.sales_date >= dateFrom && x.sales_date <= dateTo && x.payment_mode == "DO" && x.status == 1).ToList();
                    var templistSale = listSaleTemp.Select(x =>
                                      new
                                      {
                                          customer_id = x.customer_id,
                                          customer_name = x.customer.customer_name,
                                          total_discount = x.total_discount,
                                          total_beforevat = x.total_beforevat,
                                          total_vat = x.total_vat,
                                          gross_amount = x.gross_amount,
                                          net_amount = x.net_amount,
                                          sales_date = x.sales_date,
                                          payment_mode = x.payment_mode,
                                          do_id = x.do_id,
                                          do_number = x.do_number,
                                          route_id = x.customer.route_id,
                                          status = x.status,


                                      }
                                          ).Where(x => x.status == 1 && routeId == 0 ? x.route_id > 0 : x.route_id == routeId)
                                            .GroupBy(g => new { g.customer_id })
                                            .Select(s =>
                                                  new
                                                  {
                                                      customer_id = s.Key.customer_id,
                                                      customer_name = s.Max(gs => gs.customer_name),
                                                      sales_date = s.Max(gs => gs.sales_date),
                                                      payment_mode = s.Max(gs => gs.payment_mode),
                                                      status = s.Max(gs => gs.status),
                                                      total_discount = s.Sum(gs => gs.total_discount),
                                                      total_beforevat = s.Sum(gs => gs.total_beforevat),
                                                      total_vat = s.Sum(gs => gs.total_vat),
                                                      net_amount = s.Sum(gs => Math.Round(gs.net_amount, 2)),
                                                      gross_amount = s.Sum(gs => Math.Round(gs.gross_amount, 2)),
                                                  }
                                            );


                    foreach (var prop in templistSale)
                    {
                        listSale.Add(new sales
                        {
                            customer_id = prop.customer_id,
                            remarks = prop.customer_name,//temperorly used to save customer name
                            sales_date = prop.sales_date,
                            payment_mode = prop.payment_mode,
                            gross_amount = prop.gross_amount,
                            total_discount = prop.total_discount,
                            total_beforevat = prop.total_beforevat,
                            total_vat = prop.total_vat,
                            net_amount = prop.net_amount,
                            status = prop.status
                        });
                    }


                }

            }
            catch (Exception ex)
            {
                throw;
            }
            return listSale;
        }
        public List<sales> SearchDOinSalesFilterDivision(DateTime dateFrom, DateTime dateTo, int customerId, List<int> divisionIds)
        {
            List<sales> listSale = new List<sales>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    //listSale = context.sales.Include(x => x.customer).Where(x => x.sales_date >= dateFrom && x.sales_date <= dateTo && x.payment_mode =="DO" && x.status == 1).ToList();
                    List<sales> listSaleTemp = context.sales.Where(x => x.do_id == null && x.do_number != null && x.sales_date >= dateFrom && x.sales_date <= dateTo && x.payment_mode == "DO" && x.status == 1 && x.customer_id == customerId).ToList();
                    if (divisionIds != null && divisionIds.Count > 0)
                    {
                        listSaleTemp = listSaleTemp.Where(x => divisionIds.Contains(Convert.ToInt32(x.division_id))).ToList();
                    }
                    var templistSale = listSaleTemp.Select(x =>
                                      new
                                      {
                                          customer_id = x.customer_id,
                                          customer_name = x.customer.customer_name,
                                          total_discount = x.total_discount,
                                          total_beforevat = x.total_beforevat,
                                          total_vat = x.total_vat,
                                          gross_amount = x.gross_amount,
                                          net_amount = x.net_amount,
                                          sales_date = x.sales_date,
                                          payment_mode = x.payment_mode,
                                          do_id = x.do_id,
                                          do_number = x.do_number,
                                          route_id = x.customer.route_id,
                                          status = x.status,


                                      }
                                          ).GroupBy(g => new { g.customer_id })
                                            .Select(s =>
                                                  new
                                                  {
                                                      customer_id = s.Key.customer_id,
                                                      customer_name = s.Max(gs => gs.customer_name),
                                                      sales_date = s.Max(gs => gs.sales_date),
                                                      payment_mode = s.Max(gs => gs.payment_mode),
                                                      status = s.Max(gs => gs.status),
                                                      total_discount = s.Sum(gs => gs.total_discount),
                                                      total_beforevat = s.Sum(gs => gs.total_beforevat),
                                                      total_vat = s.Sum(gs => gs.total_vat),
                                                      net_amount = s.Sum(gs => Math.Round(gs.net_amount, 2)),
                                                      gross_amount = s.Sum(gs => Math.Round(gs.gross_amount, 2)),
                                                  }
                                            );

                    foreach (var prop in templistSale)
                    {
                        listSale.Add(new sales
                        {
                            customer_id = prop.customer_id,
                            remarks = prop.customer_name,//temperorly used to save customer name
                            sales_date = prop.sales_date,
                            payment_mode = prop.payment_mode,
                            gross_amount = prop.gross_amount,
                            total_discount = prop.total_discount,
                            total_beforevat = prop.total_beforevat,
                            total_vat = prop.total_vat,
                            net_amount = prop.net_amount,
                            status = prop.status
                        });
                    }


                }

            }
            catch (Exception ex)
            {
                throw;
            }
            return listSale;
        }
        public List<sales> SearchSalesWithCustomerId(DateTime dateFrom, DateTime dateTo, int customerId, int routeId = 0, List<int> divisionIds = null)
        {
            List<sales> listSale = new List<sales>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    listSale = context.sales.Include(x => x.sales_item.Select(i => i.item)).Include(x => x.customer_division).Where(x => x.do_number != null && x.sales_date >= dateFrom && x.sales_date <= dateTo && x.payment_mode == "DO" && x.status == 1 && x.customer_id == customerId && x.do_id == null).ToList();

                    if (divisionIds != null && divisionIds.Count > 0)
                    {
                        listSale = listSale.Where(x => divisionIds.Contains(Convert.ToInt32(x.division_id))).ToList();
                    }
                    if (routeId > 0)
                    {
                        listSale = listSale.Where(x => x.customer.route_id == routeId).ToList();
                    }

                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return listSale;
        }
        /// <summary>
        /// Update do_number on sales where do_number is null and Do not generated
        /// </summary>
        public int UpdateSaleDONumberForDoNotGenerated(DateTime dateFrom, DateTime dateTo, betaskdbEntities context)
        {
            int count = 0;
            try
            {
                DocumentSerialDAL documentSerialDAL = new DocumentSerialDAL();

                var saleList = context.sales.Where(x => x.sales_date >= dateFrom && x.sales_date <= dateTo && x.do_number == null && x.status == 1 && x.payment_mode == "DO" && x.net_amount > 0).ToList();
                foreach (var sl in saleList)
                {
                    H:
                    string doNumber = documentSerialDAL.GetNextDocument(DocumentSerialDAL.EnumDocuments.DO.ToString(), context);
                    if (context.sales.Any(x => x.do_number == doNumber))
                    {
                        documentSerialDAL.UpdateNextDocument(DocumentSerialDAL.EnumDocuments.DO, context);
                        goto H;

                    }
                    count++;
                    context.sales.Attach(sl);
                    sl.do_number = doNumber;
                    context.Entry(sl).Property(x => x.do_number).IsModified = true;
                    documentSerialDAL.UpdateNextDocument(DocumentSerialDAL.EnumDocuments.DO, context);
                }
                if (saleList.Count > 0)
                    context.SaveChanges();

            }
            catch (Exception ex)
            {
                throw;
            }
            return count;
        }

        public List<sales> SearchSalesWithCustomerIdDoRemoved(DateTime dateFrom, DateTime dateTo, int customerId, int routeId = 0, List<int> divisionIds = null)
        {
            List<sales> listSale = new List<sales>();
            try
            {
                using (var context = new betaskdbEntities())
                {

                    listSale = context.sales.Include(x => x.sales_item.Select(i => i.item)).Include(x => x.customer_division).Where(x => x.sales_date >= dateFrom && x.sales_date <= dateTo && x.payment_mode == "DO" && x.status == 5 && x.customer_id == customerId).ToList();

                    if (divisionIds != null && divisionIds.Count > 0)
                    {
                        listSale = listSale.Where(x => divisionIds.Contains(Convert.ToInt32(x.division_id))).ToList();
                    }
                    if (routeId > 0)
                    {
                        listSale = listSale.Where(x => x.customer.route_id == routeId).ToList();
                    }

                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return listSale;
        }
        public List<sales> SearchSalesByDOId(int doId, int customerId, int routeId = 0)
        {
            List<sales> listSale = new List<sales>();
            try
            {
                using (var context = new betaskdbEntities())
                {

                    listSale = context.sales.Include(x => x.sales_item.Select(i => i.item)).Include(x => x.customer_division).Where(x => x.do_number != null && x.do_id == doId && x.payment_mode == "DO" && x.status == 1 && x.customer_id == customerId).ToList();

                    if (routeId > 0)
                    {
                        listSale = listSale.Where(x => x.customer.route_id == routeId).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return listSale;
        }
        public List<sales> SearchSalesByDate(DateTime dateFrom, DateTime dateTo, int customerId, int routeId = 0)
        {
            List<sales> listSale = new List<sales>();
            try
            {
                using (var context = new betaskdbEntities())
                {

                    listSale = context.sales.Include(x => x.sales_item.Select(i => i.item)).Include(x => x.customer_division).Where(x => x.sales_date >= dateFrom && x.sales_date <= dateTo && x.payment_mode == "DO" && x.status == 5 && x.customer_id == customerId).ToList();

                    if (routeId > 0)
                    {
                        listSale = listSale.Where(x => x.customer.route_id == routeId).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return listSale;
        }
        public List<do_sales> SearchDOSalesWithCustomerId(DateTime dateFrom, DateTime dateTo, int customerId, int routeId = 0)
        {
            List<do_sales> listDOSale = new List<do_sales>();
            try
            {
                using (var context = new betaskdbEntities())
                {

                    listDOSale = context.do_sales.Include(x => x.do_sales_item).Where(x => x.do_invoice_number != null && x.do_date >= dateFrom && x.do_date <= dateTo && x.status == 1 && x.customer_id == customerId).ToList();

                    if (routeId > 0)
                    {
                        listDOSale = listDOSale.Where(x => x.customer.route_id == routeId).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return listDOSale;
        }
        public List<do_sales> SearchDOSalesByDOId(int doId, int customerId, int routeId = 0)
        {
            List<do_sales> listDOSale = new List<do_sales>();
            try
            {
                using (var context = new betaskdbEntities())
                {

                    listDOSale = context.do_sales.Include(x => x.do_sales_item).Where(x => x.do_invoice_number != null && x.do_id == doId && x.status == 1 && x.customer_id == customerId).ToList();

                    if (routeId > 0)
                    {
                        listDOSale = listDOSale.Where(x => x.customer.route_id == routeId).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return listDOSale;
        }

        private string CreateSale(do_sales ds, betaskdbEntities context)
        {
            string saleNumber = "";
            try
            {
                // foreach (do_sales ds in do_sales)
                {
                    if (ds != null)
                    {
                        //This contain sales of selected customer
                        List<EDMX.do_sales_item> listCustomerSales = ds.do_sales_item.ToList();
                        List<EDMX.sales_item> listXSales = new List<sales_item>();

                        //getting items from sales_item
                        foreach (EDMX.do_sales_item si in listCustomerSales)
                        {
                            listXSales.AddRange(context.sales_item.Where(x => x.sales_id == si.sales_id && x.status == 1).ToList());

                        }

                        List<int> itemsId = listXSales.Select(x => x.item_id).Distinct().ToList();
                        List<sales_item> listItemsToSave = new List<sales_item>();

                        //creating itemList from existem sales to create new sale
                        for (int i = 0; i < itemsId.Count; i++)
                        {
                            int itemId = itemsId[i];
                            item it = context.item.FirstOrDefault(x => x.item_id == itemId);
                            tax_setting tax = context.tax_setting.FirstOrDefault(x => x.tax_id == it.tax);
                            decimal taxRate = tax.tax_value ?? 0;
                            decimal rate = listXSales.FirstOrDefault(x => x.item_id == itemId).rate;
                            decimal qty = listXSales.Where(x => x.item_id == itemId && x.rate > 0).Sum(x => x.qty);
                            decimal focQty = listXSales.Where(x => x.item_id == itemId && x.rate == 0).Sum(x => x.qty);
                            decimal grossAmount = qty * rate;//listXSales.Where(x => x.item_id == itemsId[i]).Sum(x => x.gross_amount);
                            decimal taxValue = (grossAmount * taxRate / 100);
                            decimal netAmount = grossAmount + taxValue;

                            listItemsToSave.Add(new sales_item
                            {
                                item_id = itemsId[i],
                                gross_amount = grossAmount,
                                qty = qty + focQty,
                                discount = 0,
                                rate = rate,
                                total_beforvat = grossAmount,
                                net_amount = netAmount,
                                vat_amount = taxValue,
                                status = 1,
                            });
                            // decimal netAmount= listXSales.Where(x => x.item_id == itemsId[i]).Sum(x => x.gross_amount);

                        }
                        saleNumber = ds.do_invoice_number;
                        sales sale = new sales
                        {
                            sales_number = ds.do_invoice_number,
                            customer_id = ds.customer_id,
                            gross_amount = listItemsToSave.Sum(x => x.gross_amount),
                            total_beforevat = listItemsToSave.Sum(x => x.total_beforvat),
                            net_amount = listItemsToSave.Sum(x => x.net_amount),
                            total_discount = listItemsToSave.Sum(x => x.discount),
                            total_vat = listItemsToSave.Sum(x => x.vat_amount),
                            balance_amount = listItemsToSave.Sum(x => x.net_amount),
                            cash_paid = 0,
                            old_leaf_count = 0,
                            sales_date = ds.do_date,
                            payment_mode = SaleDAL.EnumPaymentModes.DO.ToString(),
                            roundup = 0,
                            status = 1,
                            do_number = ds.do_invoice_number,//DOIN1794
                            do_id = ds.do_id,//2337 primery key do_sales
                            sales_item = listItemsToSave,
                            remarks = ds.remarks,
                            delivery_leaf = ds.do_invoice_number,
                            route_id = ds.route_id,
                            lpo_number = "",
                        };

                        SaleDAL saleDAL = new SaleDAL();
                        SaleAccountPostModel postModel = new SaleAccountPostModel();
                        long newSaleId = saleDAL.SaveSale(sale, context);

                        foreach (sales_item sl in listXSales)
                        {
                            sl.status = 5;
                            context.Entry(sl).State = EntityState.Modified;
                            sales _sale = context.sales.FirstOrDefault(x => x.sales_id == sl.sales_id);
                            if (_sale != null)
                            {
                                _sale.status = 5;
                                _sale.do_number = ds.do_invoice_number;
                                _sale.do_id = ds.do_id;
                                _sale.remarks = $"New DO Sale invoice generated for {saleNumber},";
                                _sale.lpo_number = saleNumber;
                                context.Entry(_sale).State = EntityState.Modified;

                            }

                            AccountTransactionDAL transactionDAL = new AccountTransactionDAL();
                            transactionDAL.DeleteAccountTransactions(AccountTransactionDAL.EnumTransactionTypes.SALE, sl.sales_id, context);
                        }

                        context.SaveChanges();
                    }
                }

            }
            catch (DbEntityValidationException ex)
            {
                string err = "";
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        err = ($"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}");
                    }
                }
                throw;
            }
            return saleNumber;
        }
        public int SaveDOSale(List<EDMX.do_sales> _listDOSale, ref string saleNumber)
        {

            int _doId = 0;
            try
            {
                string doNumber = string.Empty;

                if (_listDOSale[0].group_id != null && _listDOSale[0].group_id > 0)
                {
                    SaveDOSaleRoute(_listDOSale, ref saleNumber, ref _doId);
                }
                else
                {
                    using (var context = new betaskdbEntities())
                    {
                        foreach (EDMX.do_sales _doSale in _listDOSale)
                        {
                            using (DbContextTransaction transaction = context.Database.BeginTransaction())
                            {
                                try
                                {


                                    doNumber = DocumentSerialDAL.GetNextDocument(DocumentSerialDAL.EnumDocuments.DOINV.ToString(),true);
                                    _doSale.do_invoice_number = doNumber;
                                    if (_doSale.route_id == 0)
                                        _doSale.route_id = null;
                                    if (_doSale.group_id == 0)
                                        _doSale.group_id = null;
                                    context.Entry(_doSale).State = _doSale.do_id == 0 ? EntityState.Added : EntityState.Modified;
                                    context.SaveChanges();
                                    _doId = _doSale.do_id;
                                    //Update Document Next Serial
                                    if (_doId > 0)
                                    {
                                        document_serial doc = context.document_serial.Where(docType => docType.document_type == DocumentSerialDAL.EnumDocuments.DOINV.ToString()).FirstOrDefault();
                                        doc.next_number = doc.next_number + 1;
                                        context.Entry(doc).State = EntityState.Modified;
                                        context.SaveChanges();

                                    }
                                    //Update sales with do id
                                    if (_doId > 0)
                                    {

                                        foreach (EDMX.do_sales_item dsi in _doSale.do_sales_item)
                                        {
                                            sales _Sales = context.sales.Where(s => s.sales_id == dsi.sales_id).FirstOrDefault();
                                            _Sales.do_id = _doId;
                                            context.Entry(_Sales).State = EntityState.Modified;
                                            context.SaveChanges();
                                        }
                                    }

                                    //CreateSale
                                    saleNumber = CreateSale(_doSale, context);

                                    transaction.Commit();
                                }
                                catch (Exception ee)
                                {
                                    if (transaction != null)
                                        transaction.Rollback();
                                    throw;
                                }
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return _doId;
        }

        private void SaveDOSaleRoute(List<do_sales> _listDOSale, ref string saleNumber, ref int _doId)
        {
            try
            {
                string doNumber = string.Empty;
                using (var context = new betaskdbEntities())
                {

                    //DOIN1794 eg
                    doNumber = DocumentSerialDAL.GetNextDocument(DocumentSerialDAL.EnumDocuments.DOINV.ToString(),true);

                    using (DbContextTransaction transaction = context.Database.BeginTransaction())

                    {
                        var doCreateSale = _listDOSale[0];
                        int count = 0;
                        var itemsToAdd = new List<EDMX.do_sales_item>();
                        foreach (EDMX.do_sales _doSale in _listDOSale)
                        {
                            try
                            {
                                _doSale.do_invoice_number = doNumber;
                                if (_doSale.route_id == 0)
                                    _doSale.route_id = null;
                                if (_doSale.group_id == 0)
                                    _doSale.group_id = null;
                                context.Entry(_doSale).State = _doSale.do_id == 0 ? EntityState.Added : EntityState.Modified;
                                context.SaveChanges();
                                _doId = _doSale.do_id;

                                //Update sales with do id
                                if (_doId > 0)
                                {

                                    foreach (EDMX.do_sales_item dsi in _doSale.do_sales_item)
                                    {
                                        sales _Sales = context.sales.Where(s => s.sales_id == dsi.sales_id).FirstOrDefault();
                                        _Sales.do_id = _doId;//Primary key of do_sales
                                        context.Entry(_Sales).State = EntityState.Modified;

                                        itemsToAdd.Add(dsi);
                                    }

                                }

                            }
                            catch (Exception ee)
                            {
                                if (transaction != null)
                                    transaction.Rollback();
                                throw;
                            }
                            count++;
                        }
                        context.SaveChanges();

                        //CreateSale

                        int groupCustomerId =Convert.ToInt32( _listDOSale[0].group_id);

                        doCreateSale.customer_id = groupCustomerId;
                        int routeId =Convert.ToInt32( context.customer.AsNoTracking().FirstOrDefault(x=>x.customer_id== groupCustomerId).route_id);
                        List<long> ids = itemsToAdd.Select(x=>x.sales_id).ToList();
                        var deliveryLeafNumbers = context.sales
                                                    .Where(s => ids.Contains(s.sales_id))
                                                    .Select(s => s.delivery_leaf)
                                                    .ToList();

                        string deliveryLeafNumbersCommaSeparated = string.Join(",", deliveryLeafNumbers);                        //var leafs = context.sales.Select(x=>x.delivery_leaf).Where(x=> );

                        doCreateSale.gross_amount = itemsToAdd.Sum(x => x.gross_amount);
                        doCreateSale.net_amount = itemsToAdd.Sum(x => x.net_amount);
                        doCreateSale.total_vat = itemsToAdd.Sum(x => x.total_vat);
                        doCreateSale.total_beforevat = itemsToAdd.Sum(x => x.total_beforevat);
                        doCreateSale.do_sales_item = itemsToAdd;
                        doCreateSale.remarks = deliveryLeafNumbersCommaSeparated;
                        doCreateSale.route_id = routeId;
                        saleNumber = CreateSale(doCreateSale, context);
                        //Update Document Next Serial
                        if (_doId > 0)
                        {
                            document_serial doc = context.document_serial.Where(docType => docType.document_type == DocumentSerialDAL.EnumDocuments.DOINV.ToString()).FirstOrDefault();
                            doc.next_number = doc.next_number + 1;
                            context.Entry(doc).State = EntityState.Modified;
                            context.SaveChanges();

                        }
                        transaction.Commit();

                    }

                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public do_sales SearchDOSales(int _doId)
        {
            do_sales doSale = new do_sales();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    doSale = context.do_sales.Include(x => x.do_sales_item).Include(c => c.customer).Where(x => x.do_id == _doId).FirstOrDefault(); ;

                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return doSale;
        }
        public List<do_sales> SearchDOSales(DateTime dateFrom, DateTime dateTo, int customerId = 0, int routeId = 0, string DoInvNo = "", string salesDoNo = "")
        {
            List<do_sales> listDOSale = new List<do_sales>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    listDOSale = context.do_sales.Include(x => x.do_sales_item).Include(c => c.customer).Where(x => x.do_date >= dateFrom && x.do_date <= dateTo).ToList(); //&& (customerId != 0 ? x.customer_id== customerId : x.customer_id > 0 ) && (routeId != 0 ? x.route_id == routeId:x.route_id > 0)  ).ToList() ;
                    if (customerId > 0)
                    {
                        listDOSale = listDOSale.Where(x => x.customer_id == customerId).ToList();
                    }
                    if (routeId > 0)
                    {
                        listDOSale = listDOSale.Where(x => x.route_id == routeId).ToList();
                    }

                    if (DoInvNo != string.Empty)
                    {
                        listDOSale = listDOSale.Where(x => x.do_invoice_number.ToUpper() == DoInvNo.ToUpper()).ToList();
                    }
                    if (salesDoNo != string.Empty)
                    {
                        do_sales_item DOSaleItem = context.do_sales_item.Include(x => x.sales).Include(d => d.do_sales).Where(x => x.sales.do_number == salesDoNo).FirstOrDefault();
                        // listDOSale = context.do_sales_item.Include(x=> x.sales).Include(d => d.do_sales).Where(x =>x.sales.do_number== salesDoNo).ToList();
                        if (DOSaleItem != null && DOSaleItem.do_id > 0)
                            listDOSale = context.do_sales.Include(x => x.do_sales_item).Include(c => c.customer).Where(x => x.do_id == DOSaleItem.do_id).ToList();
                    }
                }

            }
            catch (Exception ex)
            {
                throw;
            }
            return listDOSale;
        }
        public List<do_sales> SearchDOSales(DateTime _dateFrom, DateTime _dateTo)
        {
            List<do_sales> listDOSale = new List<do_sales>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    listDOSale = context.do_sales.Include(x => x.do_sales_item).Include(c => c.customer).Where(x => x.do_date >= _dateFrom && x.do_date <= _dateTo && x.status == 1).ToList();

                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return listDOSale;
        }
        public List<do_sales> GetPendingInvoices(DateTime date, int customerId = 0)
        {
            List<do_sales> listPending = new List<do_sales>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    listPending = context.do_sales.AsNoTracking().Include(c => c.customer).Include(r => r.customer.route).Where(x => x.status == 1 && x.do_date <= date && x.net_amount > x.amount_paid && customerId == 0 ? x.customer_id > 0.00 : x.customer_id == customerId).OrderBy(x => x.customer.route.route_name).ThenBy(x => x.customer.customer_name).ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return listPending;
        }
        public void UpdateDOReciept(int doId, decimal paidAmount, DateTime date)
        {
            try
            {
                using (var context = new betaskdbEntities())
                {
                    EDMX.do_sales _do_Sales = context.do_sales.FirstOrDefault(x => x.do_id == doId);
                    if (_do_Sales != null)
                    {
                        _do_Sales.amount_paid += paidAmount;
                        _do_Sales.remarks += $"{paidAmount} recieved on {date.ToShortDateString()} ,";
                        context.Entry(_do_Sales).Property(x => x.amount_paid).IsModified = true;
                        context.Entry(_do_Sales).Property(x => x.remarks).IsModified = true;
                        context.SaveChanges();
                    }

                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void UpateDeliveryLeaf(int saleId, string leafNo)
        {
            try
            {
                using (var context = new betaskdbEntities())
                {
                    using (DbContextTransaction transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            sales xSale = context.sales.FirstOrDefault(x => x.sales_id == saleId && x.status == 1);
                            if (context.delivery_book.Any(x => x.leaf_no == leafNo))
                            {
                                if (xSale != null)
                                {
                                    context.sales.Attach(xSale);
                                    xSale.delivery_leaf = leafNo;
                                    context.Entry(xSale).Property(x => x.delivery_leaf).IsModified = true;

                                    //Update delivery
                                    List<delivery_items> listItems = context.delivery_items.Where(x => x.sales_id == saleId && x.status == 4).ToList();
                                    if (listItems != null && listItems.Count > 0)
                                    {
                                        foreach (delivery_items dl in listItems)
                                        {
                                            context.delivery_items.Attach(dl);
                                            dl.delivery_leaf = leafNo;
                                            context.Entry(dl).Property(x => x.delivery_leaf).IsModified = true;

                                            if (dl.daily_collection_id != null)
                                            {
                                                daily_collection xColl = context.daily_collection.FirstOrDefault(x => x.daily_collection_id == dl.daily_collection_id);
                                                if (xColl != null)
                                                {

                                                    context.daily_collection.Attach(xColl);
                                                    xColl.delivery_leaf = leafNo;
                                                    context.Entry(xColl).Property(x => x.delivery_leaf).IsModified = true;

                                                }
                                            }
                                        }
                                        context.SaveChanges();
                                    }

                                }

                            }
                            else
                                throw new Exception(" **** Invalid Leaf Number **** ");
                            transaction.Commit();
                        }
                        catch
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }


    }

    public class SalesDTO
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public DateTime SalesDate { get; set; }
        public string PaymentMode { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal TotalBeforeVat { get; set; }
        public decimal TotalVat { get; set; }
        public decimal NetAmount { get; set; }
        public decimal GrossAmount { get; set; }
        public int Status { get; set; }
    }

}
