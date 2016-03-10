using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MRTools
{
    public class utility
    {
        public static readonly string DefaultRootPath = "C:\\oms-bin-rehearsal";
        public static LogWriter logWriter = new LogWriter("log.txt");
        public static DateTime addDate(string Date,string exeTime)
        {
            return Convert.ToDateTime(Date + " " + exeTime);
        }

        public static int returnSeconds(DateTime start, DateTime end)
        {
            try
            {
                TimeSpan t = end - start;
                return Convert.ToInt32(t.TotalSeconds);
            }
            catch (Exception ex)
            {
                logWriter.writeErrorLog(ex);
                return -1;
            }
        }

        public static string getFileNameFromPath(string path)
        { 
            path = Regex.Replace(path, @"/", @"\");
            return Regex.Split(path, @"\\").Last();
        }

        public static string genNewFilePath(string folderpath, string fileName)
        {
            if (folderpath.Equals(""))
            {
                return fileName;
            }
            else
            {
                return folderpath + "\\" + fileName;
            }
        }
    }
}
