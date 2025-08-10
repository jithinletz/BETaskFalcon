using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using BETask.Common;
using BETask.BAL;
using System.Windows.Forms;
using EDMX = BETask.DAL.EDMX;

namespace BETask.Views
{
    public partial class OfferForm : Form
    {
        OfferButtonCollection button;
        public enum EnumFormEvents
        {
            Close,
            FormLoad,            
            Save
        }

        private void ButtonActive(Enum activeEvent)
        {

            switch (activeEvent)
            {
                case EnumFormEvents.FormLoad:
                    break;
                case EnumFormEvents.Save:
                    SaveOffer();
                    break;
                case EnumFormEvents.Close:
                    this.BeginInvoke(new MethodInvoker(Close));
                    break;
                default:
                    break;

            }
        }

        private void ButtonEvents(object sender, EventArgs e)
        {
            if (sender == button.BtnSave)
            {
                ButtonActive(EnumFormEvents.Save);
            }
            else if (sender == button.BtnClose)
            {
                ButtonActive(EnumFormEvents.Close);
            }
        }
        public OfferForm()
        {
            InitializeComponent();
        }

        private void LoadItem()
        {
            try
            {
                BAL.ItemBAL itemBAL = new BAL.ItemBAL();
                List<EDMX.item> listItem = itemBAL.GetAllItem_Sellable();
                foreach (EDMX.item item in listItem)
                {

                    ComboboxItem _cmbItem = new ComboboxItem()
                    {
                        Text = item.item_name,
                        Value = item.item_id
                    };
                    cmbItemName.Items.Add(_cmbItem);


                }

            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error);
            }
        }

        private EDMX.offer GetOfferItems()
        {
            EDMX.offer offer = new EDMX.offer();

            try
            {
                int itemId = 0;               

                if (cmbItemName.SelectedItem != null)
                {
                    itemId = General.GetComboBoxSelectedValue(cmbItemName);
                    offer = new EDMX.offer
                    {
                        added_on = DateTime.Now,                        
                        item_id = itemId,
                        offer_name = txtOfferName.Text,
                        qty = General.ParseDecimal(txtQty.Text),                        
                        amount = General.ParseDecimal(txtAmount.Text),
                        remarks = txtRemarks.Text,
                        status = (ckbStatus.Checked ? 1 : 2)
                    };
                }
            }
            catch { throw; }
            return offer;
        }


        private void GetOffers()
        {
            try
            {                
                General.ClearGrid(dgOffers);
                OfferBAL offerBAL = new OfferBAL();                
                List<EDMX.offer> listOffers = new List<EDMX.offer>();
                listOffers = offerBAL.GetOffers();

                if (listOffers != null && listOffers.Count > 0)
                {
                    foreach (EDMX.offer offers in listOffers)
                    {
                        dgOffers.Rows.Add(offers.offer_id, offers.offer_name, offers.qty,offers.amount,offers.category,offers.rate_exclude_vat,offers.vat,offers.rate_include_vat,offers.status==1?"De Activate":"Activate");
                        if (offers.status == 2)
                            General.GridBackcolorRed(dgOffers);
                    }                    
                }                
            }
            catch (Exception ee)
            {
                throw ee;
            }
        }
        private void SaveOffer()
        {
            try
            {
                if (General.ShowMessageConfirm() == DialogResult.Yes)
                {

                    OfferBAL offerBAL = new OfferBAL();
                    EDMX.offer offer = GetOfferItems();
                    if (offer != null && offer.item_id != 0)
                    {
                        offerBAL.SaveOffer(offer);
                        General.Action($"Offer Details succesfully saved OfferName={txtOfferName.Text},Item={cmbItemName.Text} , Qty={txtQty.Text}, Amount={txtAmount.Text}, Remarks={txtRemarks.Text} , AddedOn ={DateTime.Now}");
                        General.ShowMessage(General.EnumMessageTypes.Success, "Offer Details succesfully saved", "Saved");                      
                        txtOfferName.Clear();
                        txtAmount.Clear();
                        txtQty.Clear();
                        txtRemarks.Clear();
                        cmbItemName.Text = null;
                        GetOffers();

                    }

                }

            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }

        }
        private void FormLoad()
        {
            button = new OfferButtonCollection
            {
                BtnClose = btnClose,
                BtnSave = btnSave               
            };
            LoadItem();
            GetOffers();
            ckbStatus.Checked = true;
        }

        private void NextFocus(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                General.NextFocus(sender, e);
            }
        }

        private void OfferForm_Load(object sender, EventArgs e)
        {
            FormLoad();           
        }

        class OfferButtonCollection
        {           
            public Button BtnSave { get; set; }
            public Button BtnClose { get; set; }

        }

        private void dgOffers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dgOffers.ColumnCount - 1)
            {
                try
                {
                    if (General.ShowMessageConfirm("Are you sure want to remove this") == DialogResult.Yes)
                    {
                        if (dgOffers["clmOfferId", e.RowIndex].Value != null)
                        {
                            int offerId = Convert.ToInt32(dgOffers["clmOfferId", e.RowIndex].Value);
                            if (offerId > 0)
                            {
                                OfferBAL offerBAL = new OfferBAL();
                                offerBAL.RemoveOffer(offerId);
                                General.ShowMessage(General.EnumMessageTypes.Success, "Removed");
                                GetOffers();
                            }
                        }
                    }
                }
                catch(Exception ex)
                {
                    throw ex;
                }
            }
        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            General.TxtOnlyDecimal(sender, e);
        }

        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            General.TxtOnlyDecimal(sender, e);
        }

       
    }
}
