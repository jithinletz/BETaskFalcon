using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BETask.BAL;
using BETask.Common;
using BETask.Model;
using EDMX = BETask.DAL.EDMX;

namespace BETask.Views
{
    public partial class ProductionReportForm : Form
    {
        public enum EnumFormEvents
        {
            FormLoad,
            Cancel,
            Close,
            Search,
            Print,
            PrintRawmaterial
        }
        ProductionReportButtonCollection button ;
        List<EDMX.item> listProducts = new List<EDMX.item>();
        public ProductionReportForm()
        {
            InitializeComponent();
        }
        private void ButtonActive(Enum activeEvent)
        {

            switch (activeEvent)
            {
                case EnumFormEvents.FormLoad:
                    Search();
                    break;
                case EnumFormEvents.Cancel:
                    ButtonActive(EnumFormEvents.FormLoad);
                    cmbProductName.Text = string.Empty;
                    break;
                case EnumFormEvents.Close:
                    this.BeginInvoke(new MethodInvoker(Close));
                    break;
                case EnumFormEvents.Search:
                    Search();
                    break;
                case EnumFormEvents.Print:
                    Print();
                    break;
                case EnumFormEvents.PrintRawmaterial:
                    PrintRawmaterial();
                    break;

                default:
                    break;

            }
        }
        private void ButtonEvents(object sender, EventArgs e)
        {
            if (sender == button.BtnSearch)
            {
                ButtonActive(EnumFormEvents.Search);
            }
            else if (sender == button.BtnCancel)
            {
                ButtonActive(EnumFormEvents.Cancel);
            }
            else if (sender == button.BtnClose)
            {
                ButtonActive(EnumFormEvents.Close);
            }
            else if (sender == button.BtnPrint)
            {
                ButtonActive(EnumFormEvents.Print);
            }
            else if (sender == button.BtnRawmaterial)
            {
                ButtonActive(EnumFormEvents.PrintRawmaterial);
            }
        }
        private void LoadProducts()
        {
            try
            {
                ItemBAL itemBAL = new ItemBAL();
                listProducts = itemBAL.GetAllItem_Sellable();
                foreach (EDMX.item item in listProducts)
                {
                    ComboboxItem _cmbItem = new ComboboxItem()
                    {
                        Text = item.item_name,
                        Value = item.item_id
                    };
                    cmbProductName.Items.Add(_cmbItem);
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error);
            }
        }

        private void Search()
        {
            try
            {
                General.ClearGrid(gridProduction);
                ProductionBAL productionBAL = new ProductionBAL();
                int itemId = 0;
                if (cmbProductName.Text != "")
                {
                    Object selectedItem = cmbProductName.SelectedItem;
                    itemId = (int)((BETask.Views.ComboboxItem)selectedItem).Value;
                }
                List<EDMX.production> listProduction = productionBAL.SearchProduction(General.ConvertDateServerFormat(dtpDateFrom.Value), General.ConvertDateServerFormat(dtpDateTo.Value), itemId);
                if (listProduction!=null && listProduction.Count > 0)
                {
                    foreach (EDMX.production prod in listProduction)
                    {
                        gridProduction.Rows.Add(General.ConvertDateAppFormat(prod.production_date), prod.item_id,prod.item.item_name,prod.item.uom_setting.uom_name,prod.qty,prod.cost,prod.remarks,prod.production_id);
                    }
                    lblTotalCost.Text =String.Format("{0} {1}","Total Cost",listProduction.Sum(x => x.cost).ToString());
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error);
            }
        }
       

        private void Print()
        {
            try
            {
                if (gridProduction.Rows.Count > 0)
                {
                    int itemId = 0;
                    if (cmbProductName.Text != "")
                    {
                        Object selectedItem = cmbProductName.SelectedItem;
                        itemId = (int)((BETask.Views.ComboboxItem)selectedItem).Value;
                    }
                    ProductionBAL productionBAL = new ProductionBAL();
                    productionBAL.Print(General.ConvertDateServerFormat(dtpDateFrom.Value), General.ConvertDateServerFormat(dtpDateTo.Value), itemId);
                }
                else
                {
                    General.ShowMessage(General.EnumMessageTypes.Warning, "No Data to print");
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error);
            }
        }

        private void PrintRawmaterial()
        {
            try
            {
               // General.ClearGrid(gridProduction);
                ProductionBAL productionBAL = new ProductionBAL();
                int itemId = 0;
                if (cmbProductName.Text != "")
                {
                    Object selectedItem = cmbProductName.SelectedItem;
                    itemId = (int)((BETask.Views.ComboboxItem)selectedItem).Value;
                }
                 productionBAL.SearchProduction_Rawmaterial(General.ConvertDateServerFormat(dtpDateFrom.Value), General.ConvertDateServerFormat(dtpDateTo.Value), itemId);
               
                //if (listProduction != null && listProduction.Count > 0)
                //{
                //    foreach (EDMX.production prod in listProduction)
                //    {
                //        gridProduction.Rows.Add(General.ConvertDateAppFormat(prod.production_date), prod.item_id, prod.item.item_name, prod.item.uom_setting.uom_name, prod.qty, prod.cost, prod.remarks, prod.production_id);
                //    }
                //    lblTotalCost.Text = String.Format("{0} {1}", "Total Cost", listProduction.Sum(x => x.cost).ToString());
                //}
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error);
            }
        }

        private void ProductionReport_Load(object sender, EventArgs e)
        {
            button = new ProductionReportButtonCollection()
            {
                BtnSearch = btnSearch,
                BtnCancel = btnCancel,
                BtnClose = btnClose,
                BtnPrint=btnPrint,
                BtnRawmaterial=btnPrintRawmaterial
            };
            Search();
            LoadProducts();
        }

    }
    class ProductionReportButtonCollection
    {
        public Button BtnSearch { get; set; }
        public Button BtnCancel { get; set; }
        public Button BtnClose { get; set; }
        public Button BtnPrint { get; set; }
        public Button BtnRawmaterial { get; set; }

    }
}
