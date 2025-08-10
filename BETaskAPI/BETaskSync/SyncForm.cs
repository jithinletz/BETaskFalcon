using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BETaskSync.Common;
using System.Windows.Forms;
using edmx = BETaskAPI.DAL.EDMX;

namespace BETaskSync
{
    public partial class SyncForm : Form
    {
        Repository.CustomerSyncRep custSync = new Repository.CustomerSyncRep();
        public SyncForm()
        {
            InitializeComponent();
        }
        public void GetPendingCustomer()
        {
            try
            {
                General.ClearGrid(gridNewCustomers);
                List<edmx.customer_temp> listCustomer = custSync.GetCustomerList();
                if (listCustomer != null && listCustomer.Count > 0)
                {
                   
                    foreach (edmx.customer_temp cs in listCustomer)
                    {
                        gridNewCustomers.Rows.Add(cs.customer_id,cs.customer_name,cs.route,cs.address1,$"{cs.building_name}-{cs.apartment}",cs.saved_time);
                    }
                }
                lblPending.Text = $"To be updated {listCustomer.Count().ToString()}";
            }
            catch (Exception ex)
            {
                General.Error(ex.ToString());
                lblError.Text = ex.Message;
            }
        }
        private void SyncCustomers()
        {
            try
            {
                foreach (DataGridViewRow dr in gridNewCustomers.Rows)
                {
                    if (dr.Cells["clmTempId"].Value != null)
                    {
                        int custTempId = Convert.ToInt32(dr.Cells["clmTempId"].Value.ToString());
                        try
                        {
                          
                                //upating Status
                               custSync.UpdateCustomerTempStatus(custTempId);
                            lblError.Text = custTempId.ToString();
                            Application.DoEvents();

                        //SavingCustomer
                        
                            edmx.customer_temp customerTemp= custSync.GetCustomerTempDetails(custTempId);
                            EDMX.customer customer = GetCustomer(customerTemp);
                            int savedId = 0;
                            if (customerTemp != null && customer.customer_name != null)
                            {
                                savedId= custSync.SaveCustomer(customer);
                            }

                            if (savedId > 0)
                            {
                                //Saving to App
                                EDMX.customer savedCustomer = custSync.GetCustomerLocal(customer.customer_id);
                                if (savedCustomer != null)
                                {
                                    edmx.customer customerApp = GetCustomerApp(savedCustomer);
                                    custSync.SaveCustomerToApp(customerApp);
                                    General.ErrorMustcheck($"customer saved {customer.customer_id} - {customer.customer_name}");
                                }

                                //Adding to saved grid
                                gridCustomersUpdated.Rows.Add(customerTemp.customer_id, customerTemp.customer_name, customerTemp.route, customerTemp.address1, DateTime.Now);
                            }
                        }
                        catch (Exception ex)
                        {
                            General.Error(ex.ToString());
                            lblError.Text = ex.Message;
                            custSync.UpdateCustomerTempStatus(custTempId,3);
                        }
                    }
                }
                GetPendingCustomer();
            }
            catch (Exception ex)
            {
                General.Error(ex.ToString());
                lblError.Text = ex.Message;
            }
           
        }

        // Setting customer for Local
        private EDMX.customer  GetCustomer(edmx.customer_temp _customer)
        {
            EDMX.customer customer = new EDMX.customer();
            try
            {
                int routeId = custSync.GetRouteId(_customer.route);
                int buildingId = 0;
                if (_customer.building_name != null)
                     buildingId = custSync.GetBuilding(_customer.building_name);
                if (routeId > 0)
                {
                    customer = new EDMX.customer()
                    {
                        address1 = _customer.address1,
                        address2 = _customer.address2,
                        city = _customer.area,
                        customer_id = 0,
                        email = _customer.email,
                        mobile = _customer.mobile,
                        customer_name = _customer.customer_name,
                        phone = _customer.phone,
                        pobox = _customer.pobox,
                        street = _customer.apartment,
                        customer_type = 1,
                        contact_person = null,
                        remarks = _customer.remarks,
                        trn = _customer.trn,
                        wallet_number = "",
                        status = 1,
                        lat = _customer.lat,
                        lng = _customer.lng,
                        route_id = routeId,
                        ledger_id = 0,
                        wallet_balance =0,
                        cloud_sync = 1,
                        delivery_interval=_customer.delivery_interval,
                        payment_mode=_customer.payment_mode,
                        customer_temp_id=_customer.customer_id,
                        building_id= buildingId,
                        added_time=DateTime.Now,
                       employee_id=_customer.employee_id,
                       new_customer=_customer.new_customer,
                       credit_limit=0
                        
                        
                    };

                    List<edmx.customer_aggrement_temp> listTempAgreement = _customer.customer_aggrement_temp.ToList();
                    List<EDMX.customer_aggrement> _aggrement = new List<EDMX.customer_aggrement>();
                    if (_customer.customer_aggrement_temp != null && _customer.customer_aggrement_temp.Count > 0)
                    {
                        foreach (edmx.customer_aggrement_temp agTemp in listTempAgreement)
                        {
                            decimal unitRate = agTemp.unit_price;
                            EDMX.item item = custSync.GetItem(agTemp.item_id);
                            decimal tax = 5;
                            if (item != null)
                            {
                                if (agTemp.is_tax_included == 1)
                                {
                                    if (item.tax_setting != null)
                                    {
                                        tax = Convert.ToDecimal(item.tax_setting.tax_value);
                                    }
                                    decimal withTax = agTemp.unit_price;
                                    decimal taxDeducted = ((withTax * 100) / (100 + tax));
                                    
                                    taxDeducted = General.TruncateDecimalPlaces(taxDeducted, 2);
                                    unitRate = taxDeducted;
                                }

                                _aggrement.Add(new EDMX.customer_aggrement
                                {
                                    item_id = agTemp.item_id,
                                    max_qty = agTemp.max_qty,
                                    unit_price = unitRate,

                                });
                            }
                        }
                        customer.customer_aggrement = _aggrement;
                    }
                }
            }
            catch (Exception ex)
            {
                General.Error(ex.ToString());
                lblError.Text = ex.Message;
                throw;
            }
            return customer;

        }

        //Setting customer for cloud
        private edmx.customer GetCustomerApp(EDMX.customer _customer)
        {
            edmx.customer customer = new edmx.customer();
            try
            {
               
                if (_customer !=null)
                {
                    customer = new edmx.customer()
                    {
                        address1 = _customer.address1,
                        address2 = _customer.address2,
                        city = _customer.city,
                        customer_id = _customer.customer_id,
                        email = _customer.email,
                        mobile = _customer.mobile,
                        customer_name = _customer.customer_name,
                        phone = _customer.phone,
                        pobox = _customer.pobox,
                        street = _customer.street,
                        customer_type = 1,
                        contact_person = _customer.contact_person,
                        remarks = _customer.remarks,
                        trn = _customer.trn,
                        wallet_number = _customer.wallet_number,
                        status = 1,
                        lat = _customer.lat,
                        lng = _customer.lng,
                        route_id = _customer.route_id,
                        building_id=_customer.building_id,
                        ledger_id = _customer.ledger_id,
                        wallet_balance = 0,
                        delivery_interval = _customer.delivery_interval,
                        payment_mode = _customer.payment_mode,
                        customer_temp_id = _customer.customer_temp_id,
                        outstanding_amount=0,
                        credit_limit=_customer.credit_limit,
                        employee_id=_customer.employee_id,
                        new_customer=_customer.new_customer,
                        
                      

                    };

                    List<edmx.customer_aggrement> _aggrement = new List<edmx.customer_aggrement>();

                    if (_customer.customer_aggrement != null && _customer.customer_aggrement.Count > 0)
                    {
                        foreach (EDMX.customer_aggrement ag in _customer.customer_aggrement)
                        {
                           

                                _aggrement.Add(new edmx.customer_aggrement
                                {
                                    item_id = ag.item_id,
                                    max_qty = ag.max_qty,
                                    unit_price = ag.unit_price,
                                    customer_id=ag.customer_id,
                                    status=1

                                });
                            }
                        }
                        customer.customer_aggrement = _aggrement;
                    }
                
            }
            catch (Exception ex)
            {
                General.Error(ex.ToString());
                lblError.Text = ex.Message;
                throw;
            }
            return customer;

        }

        private void SyncForm_Load(object sender, EventArgs e)
        {
            FormLoad();
        }
        private void FormLoad()
        {
            int interval = 0;
            try
            {
                interval= BETaskSync.Properties.Settings.Default.SyncInterval;
                if (interval > 0)
                {
                    timer1.Interval = interval;
                    this.Text += $" Sync Interval is {BETaskSync.Properties.Settings.Default.SyncInterval/60000}";
                }
                else
                {
                    this.Text += $" Sync Interval is 1 Miniute";
                }
            }
            catch { }
            Application.DoEvents();
            GetPendingCustomer();
            Application.DoEvents();
            System.Threading.Thread.Sleep(1000);
            SyncCustomers();
            Application.DoEvents();
            
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            GetPendingCustomer();
            lblNextSync.Text = $"Last Check Time {DateTime.Now}";
            Application.DoEvents();
            if (gridNewCustomers.Rows.Count > 0)
            {
                lblNextSync.Text = $"Last Sync Time {DateTime.Now}";
                Application.DoEvents();
                //GetPendingCustomer();
                SyncCustomers();
                
            }
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            FormLoad();
        }

        private void linkUpdate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (pnlManualUpdate.Visible)
                pnlManualUpdate.Hide();
            else
                pnlManualUpdate.Show();
        }

        private List<int> GetManualUpdateIds()
        {
            List<string> _ids = richTextBox1.Text.Split(',').ToList();
            List<int> ids = new List<int> { };
            foreach (string s in _ids)
            {
                int id = 0;
                if (int.TryParse(s, out id))
                {
                    ids.Add(id);
                }
            }
            return ids;
        }
        private void UpdateManualCustomers(List<int> ids)
        {
            
            List<EDMX.customer> listCustomer= custSync.GetCustomerLocalManual(ids);
            if (listCustomer != null && listCustomer.Count > 0)
            {
                foreach (EDMX.customer cs in listCustomer)
                {
                    edmx.customer customerApp = GetCustomerApp(cs);
                    custSync.SaveCustomerToApp(customerApp);
                    General.ErrorMustcheck($"customer saved {cs.customer_id} - {cs.customer_name}");
                

                //Adding to saved grid
               // gridCustomersUpdated.Rows.Add(cs.customer_id, cs.customer_name, cs.route==null?"":cs.route.route_name, cs.address1, DateTime.Now);
                    richTextBox1.Text = richTextBox1.Text.Replace(cs.customer_id.ToString(),"#");
        }
            }
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateManualCustomers(GetManualUpdateIds());
        }
    }
}
