using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BETask.Views
{
    public partial class DSRMismatchForm : Form
    {
        DataSet ds;
        public DSRMismatchForm(DataSet ds)
        {
            InitializeComponent();
            grid1.DataSource = ds.Tables[0];
            grid2.DataSource = ds.Tables[1];
            this.ds = ds;
            CalulateQty();
        }

        private void DSRMismatchForm_Load(object sender, EventArgs e)
        {
            LoadPaymentModes();
        }

        private void LoadPaymentModes()
        {
            DataView view = new DataView(ds.Tables[0]);
            DataTable distinctValues = view.ToTable(true, "payment_mode");
            cmbPaymentMode.DataSource = null;
            foreach (DataRow dr in distinctValues.Rows)
                cmbPaymentMode.Items.Add(dr["payment_mode"]);
        }
        private void grid1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridView cntrl = (DataGridView)sender;
                if (cntrl.Rows[e.RowIndex].DefaultCellStyle.BackColor != Color.Yellow)
                    cntrl.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Yellow;
                else
                    cntrl.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                if (sender == grid1)
                {
                    if (grid1.Columns.Contains("customer_name"))
                    {
                        string cusName = grid1["customer_name", e.RowIndex].Value.ToString();
                        foreach (DataGridViewRow dr in grid2.Rows)
                        {
                            if (dr.Cells["customer_name"].Value.ToString() == cusName)
                                dr.DefaultCellStyle.BackColor = Color.Yellow;
                        }
                    }
                }
            }
        }

        private void linkRemove_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RemoveRows();
        }
        public void RemoveRows()
        {
            foreach (DataGridViewRow dr in grid1.Rows)
            {
                if (dr.DefaultCellStyle.BackColor == Color.Yellow)
                {
                    grid1.Rows.Remove(dr);
                }

            }
            foreach (DataGridViewRow dr in grid2.Rows)
            {
                if (dr.DefaultCellStyle.BackColor == Color.Yellow)
                {
                    grid2.Rows.Remove(dr);
                }

            }
        }

        private void linkReload_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            grid1.DataSource = null;
            grid2.DataSource = null;

            grid1.DataSource = String.IsNullOrEmpty(cmbPaymentMode.Text) ? ds.Tables[0] : ds.Tables[0].Select($"payment_mode='{cmbPaymentMode.Text}'").CopyToDataTable();
            grid2.DataSource = String.IsNullOrEmpty(cmbPaymentMode.Text) ? ds.Tables[1] : ds.Tables[1].Select($"PaymentMode='{cmbPaymentMode.Text}'").CopyToDataTable();
            CalulateQty();
        }

        private void CalulateQty()
        {
            try
            {
                int total1 = grid1.Rows.Cast<DataGridViewRow>()
                      .Sum(t => Convert.ToInt32(t.Cells["Soled"].Value));
                

                int total2 = grid2.Rows.Cast<DataGridViewRow>()
                    .Sum(t => Convert.ToInt32(t.Cells["Qty"].Value));

                lblQtyCheck.Text = $"{total1} - {total2}";
            }
            catch (Exception ex)
            { }
        }

        private void grid1_MouseClick(object sender, MouseEventArgs e)
        {
            DataGridView cntrl = (DataGridView)sender;
            if (e.Button==MouseButtons.Right && cntrl.CurrentCell.ColumnIndex>0)
                cntrl.Columns.RemoveAt(cntrl.CurrentCell.ColumnIndex);
        }
    }
}
