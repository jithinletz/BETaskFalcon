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
using BETask.Model;

namespace BETask.Views
{
    public partial class DeliveryComparisonForm : Form
    {
        BAL.DeliveryBAL deliveryBAL = new DeliveryBAL();

        public int DeliveryId { get; set; }
        public DeliveryComparisonForm(int deliveryId)
        {
            InitializeComponent();
            DeliveryId = deliveryId;
        }

        private void DeliveryComparisonForm_Load(object sender, EventArgs e)
        {
            FormLoad();
        }

        private void FormLoad()
        {
            LoadLocalDeliveryData();
        }

        private void LoadLocalDeliveryData()
        {
            try
            {
                General.ClearGrid(gridLocalDeliveryData);
                var result = deliveryBAL.GetLocalDeliveryReport(DeliveryId);
                gridLocalDeliveryData.DataSource = result;
                gridLocalDeliveryData.Columns[0].Width = 160;
                gridLocalDeliveryData.Columns[1].Width = 130;
                gridLocalDeliveryData.Columns[2].Width = 160;
                gridLocalDeliveryData.Columns[3].Width = 60;
                gridLocalDeliveryData.Columns[4].Width = 40;
                General.GridRownumber(gridAppDeliveryData);
            }
            catch (Exception ex)
            {
                General.Error(ex.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ex.Message);
            }
        }
    }
}
