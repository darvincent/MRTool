using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Xml;
using System.Xml.Linq;
using System.Threading;

namespace MRTools
{
    public class MRTask:Task
    {
        private DateTime startTime;
        public DateTime StartTime
        {
            get { return startTime; }
        }
        private DateTime endTime;
        public DateTime EndTime
        {
            get { return endTime; }
        }
        private int duration;
        public int Duration
        {
            get { return duration; }
        }
        public string TaskName
        {
            get { return taskName; }
        }
        private string port;
        private bool isDoRestart = false;

        public MRTask(XmlNode taskInfo, MainForm mf)
        {
            MR = mf;
            startTime = MR.MRStartTime;
            endTime = DateTime.MinValue;
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
            startTime = utility.addDate(MR.MRDate,taskInfo.SelectSingleNode("startTime").InnerText.Trim());
            MR.MRExecuteUnits_add(new StartUnit(this));
            if (!taskInfo.SelectSingleNode("endTime").InnerText.Trim().Equals(""))
            {
                endTime = utility.addDate(MR.MRDate, taskInfo.SelectSingleNode("endTime").InnerText.Trim());
                MR.MRExecuteUnits_add(new ShutUnit(this));
            }
            port = taskInfo.SelectSingleNode("port").InnerText.Trim();
            duration = Convert.ToInt32(taskInfo.SelectSingleNode("startDuration").InnerText.Trim());
            isDoRestart = taskInfo.SelectSingleNode("doRestart").InnerText.Trim().ToUpper().Equals( "Y") ? true : false;

            process = new Process();
            FileInfo file = new FileInfo(path);
            process.StartInfo.WorkingDirectory = file.Directory.FullName;
            process.StartInfo.FileName = path;
            process.StartInfo.CreateNoWindow = false;

            StringBuilder sb = new StringBuilder("Create Deploy task:\r\n");
            sb.Append("taskName: ").Append(taskName).Append("\r\n");
            sb.Append("path: ").Append(path).Append("\r\n");
            sb.Append("executeFileName: ").Append(executeFileName).Append("\r\n");
            sb.Append("startTime: ").Append(startTime.ToLongTimeString()).Append("\r\n");
            if (endTime != DateTime.MinValue)
            {
                sb.Append("endTime: ").Append(endTime.ToLongTimeString()).Append("\r\n");
            }
            sb.Append("port: ").Append(port).Append("\r\n");
            sb.Append("duration: ").Append(duration).Append("\r\n");
            sb.Append("isDoRestart: ").Append(isDoRestart).Append("\r\n");
            utility.logWriter.writeLog(sb.ToString());
        }

        public bool isValid()
        {
            bool result = true;
            if (duration < 0)
            {
                result = false;
            }
            if (endTime !=DateTime.MinValue && startTime.CompareTo(endTime) >= 0)
            {
                result = false;
            }
            if (!File.Exists(path))
            {
                result = false;
            }
            return result;
        }

        public bool doStart()
        {
            bool result = true;
            try
            {
                if (isDoRestart)
                {
                    if (doShut())
                    {
                        string fileType = executeFileName.Split('.').Last().ToLower();
                        if (fileType.Equals("cmd") || fileType.Equals("bat"))
                        {
                            process.Start();
                            process.WaitForExit();
                            if (process.HasExited)
                            {
                                if (process.ExitCode == 1)
                                {
                                    result = false;
                                }
                                else
                                {
                                    result = true;
                                }
                            }
                        }
                        else
                        {
                            process.Start();
                        }
                    }
                    else
                    {
                        result = false;
                    }
                }
            }
            catch (Exception ex)
            {
                utility.logWriter.writeErrorLog(ex);
                result = false;
            }

            if (result)
            {
                utility.logWriter.writeLog(taskName + " Executed successfully ");
                //MR.Invoke(new MainForm.Del_addLogDetail(MR.addNormalLog), new object[] { taskName + " executed successfully. " });
            }
            else
            {
                utility.logWriter.writeLog(taskName + " Executed Failed!!! ");
                MR.Invoke(new MainForm.Del_addLogDetail(MR.addErrorLog), new object[] { taskName + " executed failed !!! " });
            }
            Thread.Sleep(duration);
            return result;
        }

        public bool doShut()
        {
            bool result = true;
            if (endTime != DateTime.MinValue)
            {
                try
                {
                    if (process != null )
                    {
                        if (port.Equals(""))
                        {
                            shutProcessByName(executeFileName);
                        }
                        else
                        {
                            shutProcessByID(port);
                        }
                        utility.logWriter.writeLog(taskName + " shut down successfully");
                        //MR.Invoke(new MainForm.Del_addLogDetail(MR.addNormalLog), new object[] { taskName + " shut down successfully. " });
                    }
                }
                catch (Exception ex)
                {
                    utility.logWriter.writeLog(taskName + " shut down failed");
                    utility.logWriter.writeErrorLog(ex);
                    MR.Invoke(new MainForm.Del_addLogDetail(MR.addErrorLog), new object[] { taskName + " shut down failed !!! " });
                    result= false;
                }
            }
            Thread.Sleep(1000);
            return result;
        }

        private void shutProcessByID(string port)
        {
            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            process.StandardInput.WriteLine("netstat -aon|findstr "+port);
            process.StandardInput.WriteLine("exit");
            string temp = Regex.Split(process.StandardOutput.ReadToEnd(), "\r\n")[4].Trim();
            string ID = Regex.Split(temp, " ").Last();
            process.Start();
            process.StandardInput.WriteLine("TASKKILL /F /IM " + ID);
            process.Close();
        }

        private void shutProcessByName(string executeName)
        {
            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            process.StandardInput.WriteLine("TASKKILL /F /IM " + executeName);
            process.Close();
        }

    }

    //public class checkTask : Task
    //{   
    //    public checkTask(string taskName, string path, DateTime executeTime):base(taskName,path,executeTime)
    //    {
    //    }
    //    public override bool doExeTask()
    //    {
    //        try
    //        {
    //            Process pro = new Process();
    //            FileInfo file = new FileInfo(base.Path);
    //            pro.StartInfo.WorkingDirectory = file.Directory.FullName;
    //            pro.StartInfo.FileName = Path;
    //            pro.StartInfo.CreateNoWindow = false;
    //            pro.Start();
    //            pro.WaitForExit();
    //            string checkReport = MainForm.RootFolderPath+"/OmsChecker/check_succeed.html";
    //            if (File.Exists(checkReport))
    //            {
    //                isSuccess = true;
    //            }
    //            else
    //            {
    //                isSuccess = false;
    //            }               
    //        }
    //        catch (Exception ex)
    //        {
    //            utility.logWriter.writeLog(TaskName + " Executed Failed\r\n" + ex.ToString());
    //            isSuccess = false;
    //        }
    //        //MainForm.executeLogInfo.Add(this);
    //        utility.logWriter.writeLog(TaskName + " Executed successfully");
    //        return IsSuccess;
    //    }

    //}
}
