using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeverBounce.Utilities
{
  public  class Log
    {
        public static void WriteLog(string strLog)
        {
            try
            {
                System.IO.StreamWriter log;
                System.IO.FileStream fileStream = null;
                System.IO.DirectoryInfo logDirInfo = null;
                System.IO.FileInfo logFileInfo;
                string logFilePath = "C:\\Logs\\";
                logFilePath = logFilePath + "Log-" + System.DateTime.Today.ToString("MM-dd-yyyy") + "." + "txt";
                logFileInfo = new System.IO.FileInfo(logFilePath);
                logDirInfo = new System.IO.DirectoryInfo(logFileInfo.DirectoryName);
                if (!logDirInfo.Exists) logDirInfo.Create();
                if (!logFileInfo.Exists)
                {
                    fileStream = logFileInfo.Create();
                }
                else
                {
                    fileStream = new System.IO.FileStream(logFilePath, System.IO.FileMode.Append);
                }
                log = new System.IO.StreamWriter(fileStream);
                log.WriteLine(strLog);
                log.Close();
            }
            catch (Exception)
            {


            }

        }
    }
}
