using BETask.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BETask.BAL;
using EDMX = BETask.DAL.EDMX;
using BETask.Model;

namespace BETask.Views
{
    public partial class DeliveryBookForm : Form
    {
        public enum EnumFormEvents
        {
            FormLoad,
            New,
            Save,
            Search,
            Cancel,
            Close,
            Print,
            Add
        }
        int _customerId = 0, _bookId = 0,_routeId=0;
        string _bookNo = "";
        DeliveryBookButtonCollection button;
        List<EDMX.delivery_book> listDeliveryBook = null;

        public DeliveryBookForm()
        {
            InitializeComponent();
        }


        private void ButtonEvents(object sender, EventArgs e)
        {
            if (sender == button.BtnSave)
            {
                ButtonActive(EnumFormEvents.Save);
            }
            else if (sender == button.BtnCancel)
            {
                ButtonActive(EnumFormEvents.Cancel);
            }
            else if (sender == button.BtnSearch)
            {
                ButtonActive(EnumFormEvents.Search);
            }

            else if (sender == button.BtnClose)
            {
                ButtonActive(EnumFormEvents.Close);
            }
            else if (sender == button.BtnNew)
            {
                ButtonActive(EnumFormEvents.New);
            }
            else if (sender == button.BtnAdd)
            {
                ButtonActive(EnumFormEvents.Add);
            }

        }
        private void ButtonActive(Enum activeEvent)
        {

            switch (activeEvent)
            {
                case EnumFormEvents.FormLoad:

                    break;
                case EnumFormEvents.Cancel:
                    ButtonActive(EnumFormEvents.FormLoad);
                    Cancel();
                    break;
                case EnumFormEvents.Close:
                    this.BeginInvoke(new MethodInvoker(Close));
                    break;
                case EnumFormEvents.Save:
                    SaveBook();
                    break;
                case EnumFormEvents.Search:
                    Search();
                    break;
                case EnumFormEvents.Add:
                    AddNewLeaf();
                    break;

                case EnumFormEvents.New:
                    txtBookNo.Focus();
                    _bookNo = "";
                    General.ClearTextBoxes(this);
                    General.ClearGrid(gridLeaf);
                    button.BtnAdd.Show();
                    if (this._customerId > 0)
                        pnlSave.Enabled = true;
                    else
                        General.ShowMessage(General.EnumMessageTypes.Warning, "Please select customer first");
                    break; ;
                default:
                    break;

            }
        }
        private void Cancel()
        {
            General.ClearTextBoxes(this);
            pnlSave.Enabled = false;
            this._customerId = 0;
            this._bookId = 0;
            lblCustomerName.Text = "";
            btnNewBook.Hide();
            General.ClearGrid(gridLeaf);
            button.BtnAdd.Show();
            gridLeaf.Columns["clmCustomer"].Visible = false;
        }
        private void AddNewLeaf()
        {
            try
            {
                if (string.IsNullOrEmpty(txtPrefix.Text) || string.IsNullOrEmpty(txtLeafStart.Text) || string.IsNullOrEmpty(txtLeafEnd.Text))
                    General.ShowMessage(General.EnumMessageTypes.Warning, "Invalid prefix or leaf numbers");
                else if(Convert.ToInt32(txtLeafStart.Text)>= Convert.ToInt32(txtLeafEnd.Text))
                    General.ShowMessage(General.EnumMessageTypes.Warning, "Invalid leaf numbers");

                else
                {
                    General.ClearGrid(gridLeaf);
                    int start = Convert.ToInt32(txtLeafStart.Text);
                    int end = Convert.ToInt32(txtLeafEnd.Text);
                    for (int i = start; i <= end; i++)
                    {
                        string leafNumber = $"{txtPrefix.Text}{i}";
                        gridLeaf.Rows.Add(0, leafNumber, "", "", "");
                    }
                    General.GridRownumber(gridLeaf);

                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
        private bool Validation()
        {
            bool resp = false;
            try
            {
                DAL.DAL.CouponDAL coupon = new DAL.DAL.CouponDAL();
                resp = coupon.IsDeliveryBookExist(txtBookNo.Text.Trim());
                if (resp)
                {
                    General.ShowMessage(General.EnumMessageTypes.Error, "Book number already exist. please use another one");
                    txtBookNo.Focus();
                    resp = false;
                }
                else if (gridLeaf.Rows.Count <= 0)
                {
                    General.ShowMessage(General.EnumMessageTypes.Error, "No leafs to save");
                }
                else if (string.IsNullOrEmpty(txtBookNo.Text))
                {
                    General.ShowMessage(General.EnumMessageTypes.Error, "Invalid book number");
                    txtBookNo.Focus();
                }

                else
                    resp = true;
            }
            catch (Exception ex)
            {
                throw;
            }
         
            return resp;
        }
        private void SaveBook()
        {
            try
            {
                SynchronizationBAL sync = new SynchronizationBAL();
                if (sync.CloudConnectionStatus(General.cloudConnection))
                {
                    if (Validation())
                    {
                        List<EDMX.delivery_book> listLeafs = GetLeaf();
                        if (listLeafs.Count > 0)
                        {
                            int resultApp = 0;
                            CouponBAL couponBAL = new CouponBAL();
                            int result = couponBAL.SaveDeliveryBook(listLeafs, out resultApp);
                            General.ShowMessage(General.EnumMessageTypes.Success, $"Successfully saved book \n Leafs saved {result} \n Leaf save in cloud {resultApp}");
                            ButtonActive(EnumFormEvents.Cancel);
                            GetDeliveryBookbyRoute(this._routeId);
                        }
                    }
                }
                else
                {
                    General.ShowMessage(General.EnumMessageTypes.Error, "No Connection with cloud . Please check your internet");
                }
            }
            catch (Exception ee)
            {
                if (ee.InnerException != null && ee.InnerException.InnerException.ToString().Contains("Violation of UNIQUE KEY"))
                {
                    General.ShowMessage(General.EnumMessageTypes.Error, "Some of leaf already exist");
                }
                else
                General.LogExceptionWithShowError(ee, "Unable to save delivery book");
            }
        }
        private void DeactivateDeliveryLeaf(int customerId, string bookNo, int bookId, string leaf)
        {

        }
        private List<EDMX.delivery_book> GetLeaf()
        {
            List<EDMX.delivery_book> delivery_Book = new List<EDMX.delivery_book>();
            try
            {
                if (gridLeaf.Rows.Count > 0)
                {
                    foreach (DataGridViewRow dr in gridLeaf.Rows)
                    {
                        delivery_Book.Add(new EDMX.delivery_book
                        {
                            book_number = txtBookNo.Text,
                            employee_id=this._customerId,
                            issue_date = General.ConvertDateServerFormat(dtpIssued.Value),
                            leaf_no = dr.Cells["clmLeaf"].Value.ToString(),
                            status = 1,
                            delivery_id=null,
                            route_id=this._routeId
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return delivery_Book;
        }

        private void GetDeliveryBook(int employeeId)
        {
            try
            {
                General.ClearGrid(gridDeliveryBooks);
                DAL.DAL.CouponDAL couponDAL = new DAL.DAL.CouponDAL();
                List<EDMX.delivery_book> listBook = couponDAL.GetDeliveryBook(employeeId);
                this.listDeliveryBook = listBook;
                if (listBook != null && listBook.Count > 0)
                {
                    List<string> books = listBook.Select(x => x.book_number).Distinct().ToList();
                    foreach (string bk in books)
                    {
                        string issued = General.ConvertDateAppFormat(listBook.Where(x => x.book_number == bk).FirstOrDefault().issue_date);
                        int min = listBook.Where(x => x.book_number == bk).Min(x => x.delivery_book_id);
                        int max = listBook.Where(x => x.book_number == bk).Max(x => x.delivery_book_id);
                        string minLeaf = listBook.FirstOrDefault(x => x.delivery_book_id == min).leaf_no;
                        string maxLeaf = listBook.FirstOrDefault(x => x.delivery_book_id == max).leaf_no;
                        int redeemed = listBook.Where(x => x.book_number == bk && x.status==4).ToList().Count();
                        int balance = listBook.Where(x => x.book_number == bk && x.status == 1).ToList().Count();
                        gridDeliveryBooks.Rows.Add(bk, issued, minLeaf, maxLeaf, redeemed, balance,"Transfer");
                    }
                }
            }
            catch (Exception ex)
            { General.LogExceptionWithShowError(ex, "Unable load customer delivery book"); }
        }
        private void GetDeliveryBookbyRoute(int routeId)
        {
            try
            {
                General.ClearGrid(gridDeliveryBooks);
                DAL.DAL.CouponDAL couponDAL = new DAL.DAL.CouponDAL();
                List<EDMX.delivery_book> listBook = couponDAL.GetDeliveryBookByRoute(routeId);
                this.listDeliveryBook = listBook;
                if (listBook != null && listBook.Count > 0)
                {
                    List<string> books = listBook.Select(x => x.book_number).Distinct().ToList();
                    foreach (string bk in books)
                    {
                        string issued = General.ConvertDateAppFormat(listBook.Where(x => x.book_number == bk).FirstOrDefault().issue_date);
                        int min = listBook.Where(x => x.book_number == bk).Min(x => x.delivery_book_id);
                        int max = listBook.Where(x => x.book_number == bk).Max(x => x.delivery_book_id);
                        string minLeaf = listBook.FirstOrDefault(x => x.delivery_book_id == min).leaf_no;
                        string maxLeaf = listBook.FirstOrDefault(x => x.delivery_book_id == max).leaf_no;
                        int redeemed = listBook.Where(x => x.book_number == bk && x.status == 4).ToList().Count();
                        int balance = listBook.Where(x => x.book_number == bk && x.status == 1).ToList().Count();
                        gridDeliveryBooks.Rows.Add(bk, issued, minLeaf, maxLeaf, redeemed, balance,"Transfer");
                    }
                }
            }
            catch (Exception ex)
            { General.LogExceptionWithShowError(ex, "Unable load customer delivery book"); }
        }
        private void FillLeaf(string bookNo)
        {
            try
            {
                gridLeaf.Columns["clmCustomer"].Visible = false;
                txtBookNo.Text = bookNo;
                General.ClearGrid(gridLeaf);
                button.BtnAdd.Hide();
                if (listDeliveryBook != null && listDeliveryBook.Count > 0)
                {
                        
                    var book = listDeliveryBook.Where(x => x.book_number == bookNo).OrderBy(x => x.delivery_book_id).ToList();
                    foreach (EDMX.delivery_book bk in book)
                    {
                        string deactivateTitle = "De Activate";
                        if(bk.status==2)
                            deactivateTitle = "De Activated";
                        if (bk.status == 4)
                            deactivateTitle = "Redeemed";
                        string customerName = bk.customer!=null?bk.customer.customer_name:"";
                        string redeemdate = bk.redeemed_date == null ? "" : General.ConvertDateTimeAppFormat(Convert.ToDateTime(bk.redeemed_date));
                        gridLeaf.Rows.Add(bk.delivery_book_id,bk.leaf_no, redeemdate, bk.delivery_id,deactivateTitle,customerName);
                        if (bk.status == 2)
                            General.GridBackcolorRed(gridLeaf);
                        if (bk.status == 4)
                            gridLeaf.Columns["clmCustomer"].Visible = true;

                    }
                }
            }
            catch (Exception ex)
            { General.LogExceptionWithShowError(ex, "Unable load delivery book leafs"); }
        }
        private void Search()
        {
            try
            {
                if (!string.IsNullOrEmpty(txtLeafSearch.Text))
                {
                    DAL.DAL.CouponDAL couponDAL = new DAL.DAL.CouponDAL();
                    List<EDMX.delivery_book> listBook = couponDAL.GetBookbyLeaf(txtLeafSearch.Text);
                    if (listBook != null && listBook.Count > 0)
                    {
                        GetDeliveryBook(listBook[0].employee_id);
                        FillLeaf(listBook[0].book_number);
                        this._customerId = listBook[0].employee_id;
                        lblCustomerName.Text = listBook[0].customer.customer_name;
                        btnNewBook.Show();
                        for (int i = 0; i < gridLeaf.Rows.Count; i++)
                        {
                            if (gridLeaf["clmLeaf", i].Value.ToString() == txtLeafSearch.Text)
                            {
                                gridLeaf.ClearSelection();
                                int nRowIndex =i;
                                gridLeaf.Rows[nRowIndex].Selected = true;
                                gridLeaf.Rows[nRowIndex].Cells[0].Selected = true;
                            }
                        }
                    }
                }
            }
            catch { }
           
        }
        
        private void LoadEmployee()
        {
            try
            {
                EmployeeBAL employeeBAL = new EmployeeBAL();
               List<EDMX.employee> listEmployee = employeeBAL.GetAllEmployee();
                General.ClearGrid(gridEmployee);
                foreach (EDMX.employee employee in listEmployee)
                {
                    if (employee.route != null && employee.route_id != null)
                        gridEmployee.Rows.Add($"{employee.first_name} {employee.last_name}", employee.route.route_name, employee.route_id, employee.employee_id);
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, $"Unable to save employee something went wrong{ee.Message}");
            }
        }
       
      
        private void FormLoad()
        {
            button = new DeliveryBookButtonCollection
            {
                BtnSave = btnSave,
                BtnClose = btnClose,
                BtnNew=btnNewBook,
                BtnAdd=btnAdd,
                BtnSearch=btnSearch,
                BtnCancel=btnCancel
            };
           
          
            LoadEmployee();
            GetAllRoutes();
        }
        private void DecimalOnly(object sender, KeyPressEventArgs e)
        {
            General.TxtOnlyDecimal(sender, e);
        }

        private void cmbRouteSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadEmployee();
        }

        private void dgCustomers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                ButtonActive(EnumFormEvents.Cancel);
                this._customerId = Convert.ToInt32(gridEmployee["ClmCustomerId", e.RowIndex].Value.ToString());
                this._routeId = Convert.ToInt32(gridEmployee["clmRouteId", e.RowIndex].Value.ToString());
                if ( e.ColumnIndex == 0)
                {
                   
                    lblCustomerName.Text = gridEmployee["clmName", e.RowIndex].Value.ToString();
                    GetDeliveryBook(this._customerId);

                }
               else if ( e.ColumnIndex == 1)
                {

                    lblCustomerName.Text = gridEmployee["clmDRoute", e.RowIndex].Value.ToString();
                    GetDeliveryBookbyRoute(this._routeId);
                }
                button.BtnNew.Show();
            }
        }

        private void gridDeliveryBooks_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == gridDeliveryBooks.ColumnCount - 1)
            {
                if (!pnlRouteChange.Visible)
                {
                    pnlRouteChange.Show();
                    lblBookNo.Text = gridDeliveryBooks["clmBookNo", e.RowIndex].Value.ToString();
                }
                else
                {
                    pnlRouteChange.Hide();
                    lblBookNo.Text = "";
                }
            }
            else
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    string bookNo = gridDeliveryBooks["clmBookNo", e.RowIndex].Value.ToString();
                    this._bookNo = bookNo;
                    FillLeaf(bookNo);
                    pnlSave.Enabled = true;
                }
            }
        }

        private void gridLeaf_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex==4)
            {
                try
                {
                    
                    if (string.IsNullOrEmpty(gridLeaf["clmReddeemed", e.RowIndex].Value.ToString()))
                    {
                        if (General.ShowMessageConfirm("Are you sure want to de activate this leaf") == DialogResult.Yes)
                        {
                            int bookId = Convert.ToInt32(gridLeaf["clmBookId", e.RowIndex].Value.ToString());
                            string leafNo = gridLeaf["clmLeaf", e.RowIndex].Value.ToString();
                            CouponBAL couponBAL = new CouponBAL();
                            couponBAL.DeactivateDeliveryLeaf(this._customerId, this._bookNo,bookId, leafNo);
                            General.ShowMessage(General.EnumMessageTypes.Success, $"{leafNo} De activated");
                            GetDeliveryBook(this._customerId);
                            FillLeaf(this._bookNo);

                        }
                    }
                    else
                    {
                        General.ShowMessage(General.EnumMessageTypes.Error, "Unable to de activate");
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }

            }
        }
        private void NextFocus(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                General.NextFocus(sender, e);
        }

       

        private void cmbRoute_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (General.ShowMessageConfirm("Are you sure want to transfer route ?") == DialogResult.Yes)
            {
                int routeId = General.GetComboBoxSelectedValue(cmbRoute);
                if (routeId > 0)
                {
                    CouponBAL couponBAL = new CouponBAL();
                    int result = couponBAL.TransferDeliveryBook(lblBookNo.Text,routeId);
                    General.ShowMessage(General.EnumMessageTypes.Success, $"{result} leafs updated");
                    pnlRouteChange.Hide();
                    LoadEmployee();
                }
            }
        }


        private void DeliveryBookForm_Load(object sender, EventArgs e)
        {
            FormLoad();
        }
        private void GetAllRoutes()
        {
            try
            {
                RouteBAL routeBAL = new RouteBAL();
                List<EDMX.route> listRoute = routeBAL.GetAllRoutes();
                cmbRoute.Items.Clear();
                foreach (EDMX.route route in listRoute)
                {
                    ComboboxItem _cmbItem = new ComboboxItem()
                    {
                        Text = route.route_name,
                        Value = route.route_id
                    };
                    cmbRoute.Items.Add(_cmbItem);

                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
    }
   
    class DeliveryBookButtonCollection
    {
        public Button BtnCancel { get; set; }
        public Button BtnClose { get; set; }
        public Button BtnSave { get; set; }
        public Button BtnSearch { get; set; }
        public Button BtnNew { get; set; }
        public Button BtnAdd { get; set; }
    }
}
