using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BETaskSync.Common
{
   public static class General
    {
        public static void ClearGrid(DataGridView dg)
        {
            try
            {
                dg.Rows.Clear();
                REMOVE:
                if (dg.Rows.Count > 0)
                {
                    foreach (DataGridViewRow dr in dg.Rows)
                    {
                        if (dr != null)
                        {
                            dg.Rows.Remove(dr);
                        }
                    }
                    if (dg.Rows.Count > 0)
                        goto REMOVE;
                }
            }
            catch { }

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
                streamWriter.WriteLine("\n" + DateTime.Now.ToString() + " - " + message);
            }
            finally
            {
                if (streamWriter != null) streamWriter.Close();
                if (fileStream != null) fileStream.Close();
            }

        }
        public static decimal TruncateDecimalPlaces(decimal val, int places)
        {

            if (places < 0)
            {
                throw new ArgumentException("places");
            }
            // return Math.Round(val - Convert.ToDecimal((0.5 / Math.Pow(10, places))), places,MidpointRounding.AwayFromZero);
            // return decimal.Round(val, places,MidpointRounding.AwayFromZero);
            double numberOfDecimalPlaces = places;
            double _multiplier = Math.Pow(10, numberOfDecimalPlaces);
            decimal multiplier = Convert.ToDecimal(_multiplier);
            decimal roundedDownNumber = Math.Round(val * multiplier,MidpointRounding.AwayFromZero) / multiplier;
            return roundedDownNumber;

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
                streamWriter.WriteLine("\n" + DateTime.Now.ToString() + "Auto sync app" + " - " + message);
            }
            finally
            {
                if (streamWriter != null) streamWriter.Close();
                if (fileStream != null) fileStream.Close();
            }

        }
    }
}
