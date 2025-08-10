using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel;

namespace BETask.Common
{
  

   static class General
    {
        public static int companyId = 1,locationId = 1;
        public static string companyName { get; set; }
        public static string userName { get; set; } = "LETZ";
        public static int userId { get; set; } 
        public static string cloudConnection { get; set; }
        public static int lastRouteOutstandingUpdated { get; set; } = 0;
        public static DateTime lastDsrLoaded { get; set; } = DateTime.Now;
        public static DateTime softwareStartDate { get; set; } = DateTime.Today;
        public static bool viewLoadingDsr{ get; set; }
    public enum EnumMessageTypes { Warning,Error,Success }
        public static string errorLabel{ get; set; }
        public static void ClearTextBoxes(Control control)
        {
            try
            {
                foreach (Control c in control.Controls)
                {
                    if (c is TextBox)
                    {
                        if (c.Tag != null)
                        {
                            if (c.Tag.ToString().Equals("cSearch"))
                            {
                                continue;
                            }
                        }
                            ((TextBox)c).Clear();

                        if (c.Tag == "Dec")
                        {
                            c.Text = "0.00";
                        }

                    }

                    if (c.HasChildren)
                    {
                        ClearTextBoxes(c);
                    }


                    if (c is CheckBox)
                    {
                        if (c.Tag == null)
                            ((CheckBox)c).Checked = true;
                        else
                            ((CheckBox)c).Checked = false;
                    }

                    if (c is RadioButton)
                    {
                        ((RadioButton)c).Checked = false;
                    }
                    if (c is ComboBox)
                    {
                        c.Text = string.Empty;
                    }
                }
            }
            catch (Exception ee)
            {
                string ss = ee.ToString();
            }
        }

        public static void BindPaymentModes(ComboBox combo)
        {
            try
            {
                Model.PaymentModesModel paymentModes = new Model.PaymentModesModel();
                combo.Items.Clear();
                foreach (string pay in paymentModes.PaymentModes)
                {
                    combo.Items.Add(pay);
                }
                combo.SelectedIndex = 0;
            }
            catch { }
        }
        public static bool ValidatePaymentModes(string mode)
        {
            try
            {
                Model.PaymentModesModel paymentModes = new Model.PaymentModesModel();
                if (!paymentModes.PaymentModes.Any(x => x == mode))
                {
                    return false;
                }
            }
            catch { throw; }
            return true;
            
        }
        public static int batchNumber { get; set; }
        public static void ShowMessage(Enum messageType,string message="Un Expected Error Please try again some time",string caption="User Message")
        {

            switch (messageType)
            {
                case EnumMessageTypes.Warning:
                    MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                case EnumMessageTypes.Error:
                    MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case EnumMessageTypes.Success:
                    MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
            }
        }
        public static DialogResult ShowMessageConfirm(string message= "Are you sure want to save this",string caption= "Please Confirm")
        {
           return MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }
        public static bool IsTextboxEmpty(TextBox textBox)
        {
            return String.IsNullOrEmpty(textBox.Text) ? true : false;
        }
        public static int IsTextboxEmptyMakeZero(string text)
        {
            return String.IsNullOrEmpty(text) ? 0 :Convert.ToInt32(text);
        }
        public static decimal ParseDecimal(string text)
        {
            decimal val = 0;
            decimal.TryParse(text, out val);
            return val;
        }
        public static int ParseInt(string text)
        {
            int val = 0;
            int.TryParse(text, out val);
            return val;
        }
        public static void ClearGrid(DataGridView dg)
        {
            // Ensure any pending edits are committed or canceled
            if (dg.IsCurrentCellInEditMode)
            {
                dg.EndEdit();
                dg.CancelEdit();
            }

            // Optionally, end any current row edits
            if (dg.IsCurrentRowDirty)
            {
                dg.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }

            // Suspend layout updates
            dg.SuspendLayout();

            try
            {
                // Clear all rows
                dg.Rows.Clear();
            }
            catch (InvalidOperationException ex)
            {
                // Handle the specific exception if needed
                Error($"ClearGridAdvanced {dg.Name} {ex.Message}");
            }
            finally
            {
                // Resume layout updates
                dg.ResumeLayout();
            }

        }
        public static void ClearGridAdvanced(DataGridView dg)
        {
            // Ensure any pending edits are committed or canceled
            if (dg.IsCurrentCellInEditMode)
            {
                dg.EndEdit();
                dg.CancelEdit();
            }

            // Optionally, end any current row edits
            if (dg.IsCurrentRowDirty)
            {
                dg.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }

            // Suspend layout updates
            dg.SuspendLayout();

            try
            {
                // Clear all rows
                dg.Rows.Clear();
            }
            catch (InvalidOperationException ex)
            {
                // Handle the specific exception if needed
                Error($"ClearGridAdvanced {dg.Name} {ex.Message}" );
            }
            finally
            {
                // Resume layout updates
                dg.ResumeLayout();
            }

        }
        public static void GridRownumber(DataGridView grid)
        {
            foreach (DataGridViewRow row in grid.Rows)
            {
                row.HeaderCell.Value = String.Format("{0}", row.Index + 1);
            }
        }
        public static void GridBackcolorYellow(DataGridView grid)
        {
            if(grid.Rows.Count>0)
            grid.Rows[grid.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Yellow;
        }
        public static void GridBackcolorRed(DataGridView grid)
        {
            if (grid.Rows.Count > 0)
            {
                grid.Rows[grid.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Red;
                grid.Rows[grid.Rows.Count - 1].DefaultCellStyle.ForeColor = Color.White;
            }
        }
        public static void GridBackcolorPink(DataGridView grid)
        {
            if (grid.Rows.Count > 0)
            {
                grid.Rows[grid.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Pink;
                grid.Rows[grid.Rows.Count - 1].DefaultCellStyle.ForeColor = Color.Black;
            }
        }
        public static void GridBackcolorOrange(DataGridView grid)
        {
            if (grid.Rows.Count > 0)
            {
                grid.Rows[grid.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Orange;
                grid.Rows[grid.Rows.Count - 1].DefaultCellStyle.ForeColor = Color.Black;
            }
        }
        public static void GridBackcolorGreen(DataGridView grid)
        {
            if (grid.Rows.Count > 0)
            {
                grid.Rows[grid.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Green;
                grid.Rows[grid.Rows.Count - 1].DefaultCellStyle.ForeColor = Color.White;
            }
        }

        public static void GridForecolorBlue(DataGridView grid)
        {
            if (grid.Rows.Count > 0)
            {
                grid.Rows[grid.Rows.Count - 1].DefaultCellStyle.ForeColor = Color.Blue;
                //grid.Rows[grid.Rows.Count - 1].DefaultCellStyle.ForeColor = Color.Black;
            }
        }
        public static decimal TruncateDecimalPlaces(decimal val, int places=3)
        {
            decimal roundedNumber = Math.Round(val, places, MidpointRounding.AwayFromZero);
            return roundedNumber;

            //return Math.Round(val, places);
            //if (places < 0)
            //{
            //    throw new ArgumentException("places");
            //}
            //return Math.Round(val - Convert.ToDecimal((0.5 / Math.Pow(10, places))), places);
        }
     
        public static void TxtOnlyDecimal(object sender, KeyPressEventArgs e)
        {
            int ascii = Convert.ToInt16(e.KeyChar);
            if (!((ascii >= 48 && ascii <= 57) || (ascii == 46) || (ascii == 8) || (ascii==45)))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="pnl"></param>
        /// <returns></returns>
        public static bool DecimalValidationText(TextBox text)
        {
            text.Text = String.IsNullOrEmpty(text.Text) ? "0" : text.Text;
            text.Text = Convert.ToDecimal(text.Text).ToString();
            Regex regex = new Regex(@"^[0-9]*(\.[0-9]{1,2})?$");
            if (Convert.ToDecimal(text.Text) < 0)
            {
                 regex = new Regex(@"^-?[0-9]\d*(\. \-\d{1,2})?$");
            }
           
            if (regex.IsMatch(text.Text))
                return true;
           else
            {
                text.Focus();
            }
            return false;
        }

       

        public static void NextFocus(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                SendKeys.Send("{Tab}");
            }
        }
        public static void NextFocus(object sender, KeyEventArgs e,Control NextControl)
        {
            if (e.KeyData == Keys.Enter)
            {
                NextControl.Focus();
            }
        }
        public static void Error(string message)
        {
            FileStream fileStream = null;
            StreamWriter streamWriter = null;
            try
            {
                string logFilePath = Application.StartupPath + "\\Log\\";//"c:\\LogError\\";

                logFilePath = logFilePath + "AppErrorLog" + "-" + DateTime.Today.ToString("yyyyMMdd") + "." + "txt";

                if (logFilePath.Equals("")) return;
                #region Create the Log file directory if it does not exists 
                DirectoryInfo logDirInfo = null;
                FileInfo logFileInfo = new FileInfo(logFilePath);
                logDirInfo = new DirectoryInfo(logFileInfo.DirectoryName);
                if (!logDirInfo.Exists) logDirInfo.Create();
                #endregion Create the Log file directory if it does not exists 

                if (!logFileInfo.Exists)
                {
                    fileStream = logFileInfo.Create();
                }
                else
                {
                    fileStream = new FileStream(logFilePath, FileMode.Append);
                }
                streamWriter = new StreamWriter(fileStream);
                streamWriter.WriteLine("\n" + DateTime.Now.ToString() + "{" + userName + "}" + " - " + message);
            }
            finally
            {
                if (streamWriter != null) streamWriter.Close();
                if (fileStream != null) fileStream.Close();
            }

        }
        public static void ErrorMustcheck(string message)
        {
            FileStream fileStream = null;
            StreamWriter streamWriter = null;
            try
            {
                string logFilePath = Application.StartupPath + "\\Log\\";//"c:\\LogError\\";

                logFilePath = logFilePath + "MustcheckErrorLog" + "-" + DateTime.Today.ToString("yyyyMMdd") + "." + "txt";

                if (logFilePath.Equals("")) return;
                #region Create the Log file directory if it does not exists 
                DirectoryInfo logDirInfo = null;
                FileInfo logFileInfo = new FileInfo(logFilePath);
                logDirInfo = new DirectoryInfo(logFileInfo.DirectoryName);
                if (!logDirInfo.Exists) logDirInfo.Create();
                #endregion Create the Log file directory if it does not exists 

                if (!logFileInfo.Exists)
                {
                    fileStream = logFileInfo.Create();
                }
                else
                {
                    fileStream = new FileStream(logFilePath, FileMode.Append);
                }
                streamWriter = new StreamWriter(fileStream);
                streamWriter.WriteLine("\n" + DateTime.Now.ToString() + "{" + userName + "}" + " - " + message);
            }
            finally
            {
                if (streamWriter != null) streamWriter.Close();
                if (fileStream != null) fileStream.Close();
            }

        }
        public static void Action(string message)
        {
            FileStream fileStream = null;
            StreamWriter streamWriter = null;
            try
            {
                string logFilePath = Application.StartupPath + "\\Log\\";//"c:\\LogError\\";

                logFilePath = logFilePath + "AppActionLog" + "-" + DateTime.Today.ToString("yyyyMMdd") + "." + "txt";

                if (logFilePath.Equals("")) return;
                #region Create the Log file directory if it does not exists 
                DirectoryInfo logDirInfo = null;
                FileInfo logFileInfo = new FileInfo(logFilePath);
                logDirInfo = new DirectoryInfo(logFileInfo.DirectoryName);
                if (!logDirInfo.Exists) logDirInfo.Create();
                #endregion Create the Log file directory if it does not exists 

                if (!logFileInfo.Exists)
                {
                    fileStream = logFileInfo.Create();
                }
                else
                {
                    fileStream = new FileStream(logFilePath, FileMode.Append);
                }
                streamWriter = new StreamWriter(fileStream);
                streamWriter.WriteLine("\n" + DateTime.Now.ToString() + "{" + userName + "}" + " - " + message);
            }
            finally
            {
                if (streamWriter != null) streamWriter.Close();
                if (fileStream != null) fileStream.Close();
            }

        }
        public static DateTime ConvertDateServerFormat(DateTime dateTime)
        {
            //return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day);
            return DateTime.Parse(dateTime.ToString("yyyy/MM/dd"));
        }
        public static DateTime ConvertDateServerFormatWithCurrentTime(DateTime dateTime)
        {
            //return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day);
            DateTime tm = DateTime.Now;
            DateTime dt = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day,tm.Hour , tm.Minute, tm.Second);
            return DateTime.Parse(dt.ToString("yyyy/MM/dd HH:mm:ss"));
        }
        public static DateTime ConvertDateServerFormatWithStartTime(DateTime dateTime)
        {
            
            DateTime dt = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 00, 00,00);

            return DateTime.Parse(dt.ToString("yyyy/MM/dd HH:mm:ss"));
        }
        public static DateTime ConvertDateServerFormatWithEndTime(DateTime dateTime)
        {
          
            DateTime dt = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 23, 59, 59);
            
            return DateTime.Parse(dt.ToString("yyyy/MM/dd HH:mm:ss")); 
        }
        public static DateTime ConvertDateTimeServerFormat(DateTime dateTime)
        {
            //return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day);
            return DateTime.Parse(dateTime.ToString("yyyy/MM/dd hh:mm tt"));
        }
        public static DateTime ConvertDateServerFormat_string(string dateTime)
        {
            //return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day);
            return  DateTime.Parse(DateTime.Parse(dateTime).ToString("yyyy/MM/dd"));
        }
        public static string ConvertDateAppFormat(DateTime dateTime)
        {
            //return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day);
            return dateTime.ToString("dd/MM/yyyy");
        }
        public static string ConvertDateAppFormat(string dateTime)
        {
            //return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day);
            return Convert.ToDateTime( dateTime).ToString("dd/MM/yyyy");
        }
        public static string ConvertDateTimeAppFormat(DateTime dateTime)
        {
            //return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day);
            return dateTime.ToString("dd/MM/yyyy hh:mm tt");
        }
        

        public static void Backup()
        {
            if (!System.IO.Directory.Exists(Application.StartupPath + "\\Backup"))
                System.IO.Directory.CreateDirectory(Application.StartupPath + "\\Backup");
            string file = $"Betaskdb{DateTime.Today.Year}{DateTime.Today.Month}{DateTime.Today.Day}{DateTime.Today.Hour}.bak";
            string dest = Application.StartupPath + "\\Backup\\Betask" + file;

            string que = ("Backup database betaskdb to disk='" + dest + "'");
            try
            {
                string conn = @"Data Source=.;Initial Catalog=betaskdb;Persist Security Info=True;User ID=usr_betask;Password=pwd_betask;";
                SqlConnection con = new SqlConnection(conn);
                con.Open();
                SqlCommand cmd;
                SqlDataReader dr;

                cmd = new SqlCommand(que, con);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ee)
            {
                try
                {
                    string conn = @"Data Source=.,4022;Initial Catalog=betaskdb;Persist Security Info=True;User ID=usr_betask;Password=pwd_betask;";
                    SqlConnection con = new SqlConnection(conn);
                    con.Open();
                    SqlCommand cmd;
                    SqlDataReader dr;
                }
                catch(Exception ex)
                {
                    Error(ex.ToString());
                    MessageBox.Show(ee.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return;
            }
        }
       
       
        public static string NumToWord(decimal Number, bool Chq)
        {
            if (Number < 0) return "";
            string snum = Number.ToString();
            snum = string.Format("{0:0.00}", Number);
            snum = snum.PadLeft(15, '0');
            string n2w = "";

            ConvertNum(ref n2w, snum.Substring(0, 1), "Thousand ");
            ConvertNum(ref n2w, snum.Substring(1, 3), "Billion ");
            ConvertNum(ref n2w, snum.Substring(4, 2), "Million ");
            ConvertNum(ref n2w, snum.Substring(6, 3), "Thousand ");
            ConvertNum(ref n2w, snum.Substring(9, 3), "");
            if (int.Parse(snum.Substring(snum.Length - 2, 2)) > 0)
            {
                //if (n2w != "") n2w = n2w + "and dirham " + int.Parse(snum.Substring(snum.Length - 2, 2)) + "/100.";
                if (n2w != "") n2w = n2w + "and " + int.Parse(snum.Substring(snum.Length - 2, 2)) + "/100.";
            }
            if (Chq)
            {
                return n2w + " Only";
            }
            else
            {
                return $"({BETask.Properties.Settings.Default.defCurrency}. " + n2w + " Only)";
            }
        }
        private static void ConvertNum(ref string N2W, string sn, string ps)
        {
            sn = sn.Trim();
            int no = 0;
            long Vn = long.Parse(sn);
            if (Vn > 0)
            {
                string[] Ones = { "", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Ninteen" };
                string[] Tens = { "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };
                do
                {
                    if (Vn > 99)
                    {
                        no = int.Parse(sn.Substring(0, 1));
                        N2W = N2W + Ones[no] + " Hundred ";
                        Vn = Vn - no * 100;
                    }
                    else if (Vn > 19)
                    {
                        no = int.Parse(sn.Substring(0, 1));
                        if (no > 0)
                        {
                            N2W = N2W + Tens[no - 2] + " ";
                            Vn = Vn - no * 10;
                        }
                    }
                    else
                    {
                        N2W = N2W + Ones[Vn] + " ";
                        break; ;
                    }
                    sn = Vn.ToString().Trim();
                } while (Vn >= 0);
                N2W = N2W + ps;

            }


        }
        public static bool CheckFinancialDate(DateTime date)
        {
            BETask.BAL.CompanyBAL company = new BAL.CompanyBAL();
            if (!company.CheckFinancialDate(date))
            {
                ShowMessage(General.EnumMessageTypes.Warning, "Invalid financial year", "Invalid financial year");
                return false;
            }
            return true;
        }

            public static bool CheckForServerConnection()
        {
            try
            {
                using (var client = new System.Net.WebClient())
                {
                    client.Proxy = null;
                    using (var stream = client.OpenRead("http://www.bookbyq.com"))
                    {
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        public static DataTable ToDataTable<T>(this IList<T> data)
        {
            PropertyDescriptorCollection properties =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }
        public static System.Guid GenerateGuid()
        {
            System.Guid guid = System.Guid.NewGuid();
            return guid;
        }

        public static int GetComboBoxSelectedValue(ComboBox combo)
        {
            int _value = 0;
            if (!String.IsNullOrEmpty(combo.Text) && combo.SelectedItem != null)
            {
                Object selected= combo.SelectedItem;
                _value = (int)((BETask.Views.ComboboxItem)selected).Value;
            }
            return _value;
        }
        public static void LogExceptionWithShowError(Exception ex,string message)
        {
            Error(ex.ToString());
            ShowMessage(General.EnumMessageTypes.Error, $"{message} \n {ex.Message}");
        }

        public static void SetScreenSize(object sender, EventArgs e,Form form)
        {
            // Get the current screen's working area (exclude taskbar and other docked elements)
            Rectangle screen = Screen.PrimaryScreen.Bounds;

            // Set the form's size to 70% of the screen's width and height
            form.Width = (int)(screen.Width * 0.7);
            form.Height = (int)(screen.Height * 0.7);

            // Center the form on the screen
            form.StartPosition = FormStartPosition.CenterParent;
            form.Left = (screen.Width - form.Width) / 3;
            form.Top = (screen.Height - form.Height) / 4;
        }
        public static void SetScreenSize_customer(object sender, EventArgs e, Form form)
        {
            // Get the current screen's working area (exclude taskbar and other docked elements)
            Rectangle screen = Screen.PrimaryScreen.Bounds;

            // Set the form's size to 70% of the screen's width and height
            form.Width = (int)(screen.Width * 0.75);
            form.Height = (int)(screen.Height * 0.83);

            // Center the form on the screen
            form.StartPosition = FormStartPosition.CenterParent;
            form.Left = (screen.Width - form.Width) / 4;
            form.Top = (screen.Height - form.Height) / 8;
        }

    }
}
