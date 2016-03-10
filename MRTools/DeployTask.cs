using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Text.RegularExpressions;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace MRTools
{
    public class DeployTask:Task
    {
        private int sequence;
        public int Sequence
        {
            get { return sequence; }
        }
        private int executeDuration;
        public DeployTask(XmlNode taskInfo, MainForm mf)
        {
            MR = mf;
            taskName = taskInfo.SelectSingleNode("name").InnerText.Trim();
            if (taskInfo.SelectSingleNode("IsInPackage").InnerText.Trim().ToUpper().Equals("Y"))
            {
                path = MR.MRFolderPath + "\\" + taskInfo.SelectSingleNode("path").InnerText.Trim();
                path = Regex.Replace(path, @"/", @"\");
            }
            else
            {
                path = Regex.Replace(taskInfo.SelectSingleNode("path").InnerText.Trim(), @"/", @"\");
            }
            executeFileName = Regex.Split(path, @"\\").Last();
            sequence = Convert.ToInt32(taskInfo.SelectSingleNode("sequence").InnerText.Trim());
            executeDuration = Convert.ToInt32(taskInfo.SelectSingleNode("executeDuration").InnerText.Trim());
            process = new Process();
            FileInfo file = new FileInfo(path);
            process.StartInfo.WorkingDirectory = file.Directory.FullName;
            process.StartInfo.FileName = path;
            process.StartInfo.CreateNoWindow = false;

            StringBuilder sb = new StringBuilder("Create Deploy task:\r\n");
            sb.Append("taskName: ").Append(taskName).Append("\r\n");
            sb.Append("path: ").Append(path).Append("\r\n");
            sb.Append("executeFileName: ").Append(executeFileName).Append("\r\n");
            sb.Append("sequence: ").Append(sequence).Append("\r\n");
            sb.Append("executeDuration: ").Append(executeDuration).Append("\r\n");
            utility.logWriter.writeLog(sb.ToString());
        }

        public bool doStart()
        {
            bool result = true;
            try
            {
                process.Start();
                utility.logWriter.writeLog(taskName + " Executed successfully ");
                Thread.Sleep(executeDuration);
                //MR.Invoke(new MainForm.Del_addLogDetail(MR.addNormalLog), new object[] { taskName + " executed successfully. " });
            }
            catch (Exception ex)
            {
                utility.logWriter.writeLog(taskName + " Executed failed ");
                utility.logWriter.writeErrorLog(ex);
                //MR.Invoke(new MainForm.Del_addLogDetail(MR.addErrorLog), new object[] { taskName + " executed failed !!! " });
                result = false;
            }
            return result;
        }
    }
}
