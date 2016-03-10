using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

namespace MRTools
{
    public class LogWriter
    {
        private string filePath;
        private FileStream fs;

        public LogWriter(string path)
        {
            filePath = path;
            fs = new FileStream(filePath, FileMode.Append);

        }
        public void writeLog(string message)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(doWrite), message);
        }

        public void writeErrorLog(Exception ex)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(doWrite), "ERROR> "+ex.ToString());
        }

        private void doWrite(object obj)
        {
            string output = new StringBuilder("[").Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff")).Append("]: ").Append((string)obj).Append("\r\n").ToString();
            byte[] bytes = Encoding.GetEncoding("UTF-8").GetBytes(output);
            fs.Write(bytes, 0, bytes.Length);
            fs.Flush();
        }
    }
}
