using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections;
using System.Threading;
using System.Xml;
using ICSharpCode.SharpZipLib.Zip;

namespace MRTools
{
    public partial class MainForm : Form
    {
        private string RootFolderPath;
        private string MRInfoPath;
        private string PackagePath;
        private string Notice;
        public string MRFolderPath;
        private Config INI;

        public delegate void Del_stateReceive(MRTask task);
        public delegate void Del_addLogDetail(string detail);
        public delegate void Del_UIdisplay();

        public State state;
        public State initialState;
        public State deployedState;
        public State deployErrorState;
        public State doingMRState;
        public State startUpFailState;
        public State finishedMRState;
        public State endUpFailState;

        private List<ExecuteUnit> MRExecuteUnits = new List<ExecuteUnit>();
        private List<ExecuteUnit> lateForScheduleUnits = new List<ExecuteUnit>();
        private List<DeployTask> DeployTasks = new List<DeployTask>();
        public bool isTasksBeforeMROK;
        public bool isTasksAfterMROK;
        private bool isLateForBeforeMRSchedule;
        public bool IsLateForBeforeMRSchedule
        {
            get { return isLateForBeforeMRSchedule; }
        }
        private bool isDeployed = false;

        private ExecuteUnit DeployedStateTrigger;
        private ExecuteUnit DoingMRStateTrigger;

        private string MRName;
        public string MRDate;
        public DateTime MRStartTime;
        public DateTime MREndTime;

        public static object obj = new object();
        private XmlDocument config;
        private ZipClass zipOp = new ZipClass();

        public MainForm()
        {
            try
            {
                INI = new Config(AppDomain.CurrentDomain.BaseDirectory + "config.ini");
            }
            catch (Exception ex)
            {
                utility.logWriter.writeErrorLog(ex);
                MessageBox.Show("Can't find configuration file, program would exit!");
                this.Close();
            }
            RootFolderPath = INI.iniReadValue("MRTool", "RootPath");
            if (RootFolderPath.Equals(""))
            {
                RootFolderPath = utility.DefaultRootPath;
            }
            MRFolderPath = RootFolderPath;

            InitializeComponent();

            initialState = new InitialState(this);
            deployedState = new DeployedState(this);
            deployErrorState = new DeployErrorState(this);
            doingMRState = new DoingMRState(this);
            startUpFailState = new StartUpFailState(this);
            finishedMRState = new FinishedMRState(this);
            endUpFailState = new EndUpFailState(this);

            utility.logWriter.writeLog("Program starts successfully");
        }

        private void loadDeployedMR()
        {
            string xmlPath = INI.iniReadValue("MRTool", "DeployedMRConfigPath").Trim();
            string packPath = INI.iniReadValue("MRTool", "DeployedMRPackagePath").Trim();
            MRFolderPath = INI.iniReadValue("MRTool", "DeployedMRFolderPath").Trim();
            if (xmlPath.Equals("") || packPath.Equals(""))
            {
                return;
            }
            MRInfoPath = xmlPath;
            PackagePath = packPath;
            textBox1.Text = xmlPath;
            textBox2.Text = packPath;

            if (getMRInfo())
            {
                if (IsOverMREndTime())
                {
                    setState(finishedMRState);
                }
                else
                {
                    if (setMRSchedule())
                    {
                        setState(deployedState);
                    }
                    else
                    {
                        setState(deployErrorState);
                    }
                }
            }
            else
            {
                setState(deployErrorState);
            }
            isDeployed = true;
        }

        public void setState(State state)
        {
            this.state = state;
            this.Invoke(new Del_UIdisplay(state.initial));
        }

        public void MRExecuteUnits_add(ExecuteUnit unit)
        {
            MRExecuteUnits.Add(unit);
            MRExecuteUnits.Sort(new ExeUnitTimeComparer());
        }

        public void lateForScheduleUnits_add(ExecuteUnit unit)
        {
            lateForScheduleUnits.Add(unit);
            lateForScheduleUnits.Sort(new ExeUnitTimeComparer());
        }

        private bool createMRFolder()
        {
            try
            {
                MRFolderPath =RootFolderPath+ "/" + MRName + "-" + MRDate;
                if (!Directory.Exists(MRFolderPath))
                {
                    Directory.CreateDirectory(MRFolderPath);
                }
                string MRInfoConfig = MRFolderPath+"/MRInfoConfig.xml";
                if (!MRInfoPath.Equals(MRInfoConfig))
                {
                    File.Copy(MRInfoPath, MRInfoConfig, true);
                    MRInfoPath = MRInfoConfig;
                }
                utility.logWriter.writeLog("create folder successfully, path: " + MRFolderPath);
                return true;
            }
            catch (Exception ex)
            {
                utility.logWriter.writeErrorLog(ex);
                return false;
            }
        }

        private void deleteTempFile(string path)
        {
            DirectoryInfo di = new DirectoryInfo(path);
            foreach (FileInfo fi in di.GetFiles())
            {
                if (Regex.IsMatch(fi.Name, @".tmp"))
                {
                    fi.Delete();
                }
            }
        }

        private bool unZip()
        {
            try
            {
                if (PackagePath != "")
                {
                    string Package = MRFolderPath + "/MRPackage.Zip";
                    if (!Package.Equals(PackagePath))
                    {
                        File.Copy(PackagePath, Package, true);
                        PackagePath = Package;
                        deleteTempFile(MRFolderPath);
                        zipOp.UnZip(PackagePath, MRFolderPath);
                        utility.logWriter.writeLog("unzip MR package successfully");
                    }
                }
                else
                {
                    utility.logWriter.writeLog("No MR package imported, so no need to unZip");
                }
                return true;
            }
            catch (Exception ex)
            {
                utility.logWriter.writeErrorLog(ex);
                return false;
            }
        }

        private bool getMRInfo()
        {
            try
            {
                config = new XmlDocument();
                config.Load(@MRInfoPath);
                MRName = config.SelectSingleNode("Info/basicInfo/MRName").InnerText;
                MRDate = config.SelectSingleNode("Info/basicInfo/Date").InnerText;
                MRStartTime = utility.addDate(MRDate,config.SelectSingleNode("Info/basicInfo/StartTime").InnerText);
                MREndTime = utility.addDate(MRDate,config.SelectSingleNode("Info/basicInfo/EndTime").InnerText);
                Notice = config.SelectSingleNode("Info/basicInfo/Notice").InnerText;
                return true;
            }
            catch (Exception ex)
            {
                utility.logWriter.writeErrorLog(ex);
                return false;
            }
        }

        private bool initialMRTasks()
        {
            try
            {
                XmlNodeList xnl = config.SelectSingleNode("Info/MRTasks").ChildNodes;
                foreach (XmlNode xn in xnl)
                {
                    MRTask t = new MRTask(xn, this);
                }
                return true;
            }
            catch (Exception ex)
            {
                utility.logWriter.writeErrorLog(ex);
                return false;
            }
        }

        private bool initialDeployTasks()
        {
            try
            {
                XmlNodeList xnl = config.SelectSingleNode("Info/DeployTasks").ChildNodes;
                foreach (XmlNode xn in xnl)
                {
                    DeployTask t = new DeployTask(xn, this);
                    DeployTasks.Add(t);
                }
                return true;
            }
            catch (Exception ex)
            {
                utility.logWriter.writeErrorLog(ex);
                return false;
            }
        }

        private bool doDeployTasks()
        {
            bool result = true;
            if (!initialDeployTasks())
            {
                result = false;
            }
            else
            {
                for(int i = 0; i<DeployTasks.Count;i++)
                {
                    result = result && DeployTasks[i].doStart();
                }
            }
            return result;
        }

        public bool IsOverMREndTime()
        {
            if (Convert.ToDateTime(MREndTime).CompareTo(DateTime.Now) < 0)
            {
                return true;
            }
            return false;
        }

        private bool setMRSchedule()
        {
            isTasksBeforeMROK = true;
            isTasksAfterMROK = true;
            isLateForBeforeMRSchedule = false;
            this.Invoke(new Del_UIdisplay(clearLogs));
            bool result = true;
            result = result && initialMRTasks();
            if (MRExecuteUnits.Count > 0) 
            {
                for (int i = 0; i < MRExecuteUnits.Count; i++)
                {
                    result = result && MRExecuteUnits[i].setSchedule();
                    if (MRExecuteUnits[i].executeTime.CompareTo(MRStartTime) < 0 && MRExecuteUnits[i].executeTime.CompareTo(DeployedStateTrigger.executeTime) > 0)
                    {
                        DeployedStateTrigger = MRExecuteUnits[i];
                    }
                }
                
                if (MRExecuteUnits.Last().executeTime.CompareTo(MREndTime) > 0)
                {
                    DoingMRStateTrigger = MRExecuteUnits.Last();
                    DoingMRStateTrigger.isDoingMRStateTrigger = true;
                }
                if (lateForScheduleUnits.Count > 0)
                {
                    isLateForBeforeMRSchedule = true;
                }
                //this.Invoke(new MainForm.Del_addLogDetail(addNormalLog), new object[] { "MR environment will be ready at " + DeployedStateTrigger.executeTime });
                //this.Invoke(new MainForm.Del_addLogDetail(addNormalLog), new object[] { "System will be resumed at " + MRExecuteUnits.Last().executeTime });
            }

            if (DeployedStateTrigger != null)
            {
                DeployedStateTrigger.isDeployedStateTrigger = true;
            }
            else
            {
                DeployedStateTrigger = new DeployedTriggerUnit(this);
                DeployedStateTrigger.setSchedule();
            }
            if (DoingMRStateTrigger == null)
            {
                DoingMRStateTrigger = new DoingMRTriggerUnit(this);
                DoingMRStateTrigger.setSchedule();
            }
            return result;
        }

        public bool restart()
        {
            lock (obj)
            {
                bool result = true;
                for (int i = 0; i < lateForScheduleUnits.Count; i++)
                {
                    result = result && lateForScheduleUnits[i].execute();
                }
                return result;
            }
        }

        private bool finish()
        {
            bool result = true;
            for (int i = 0; i < MRExecuteUnits.Count; i++)
            {
                result = result && MRExecuteUnits[i].finish();
            }
            return result;
        }

        private bool deploy()
        {
            utility.logWriter.writeLog("deploy started");
            if (!createMRFolder())
            {
                utility.logWriter.writeLog("deploy failed, Create MR folder failed ");
                return false;
            }
            if (!unZip())
            {
                utility.logWriter.writeLog("deploy failed, Unzip Failed ");
                return false;
            }
            if (!doDeployTasks())
            {
                utility.logWriter.writeLog("deploy failed, do deploy tasks failed ");
                return false;
            }
            if (!setMRSchedule())
            {
                utility.logWriter.writeLog("deploy failed, set Schedules failed ");
                return false;
            }
            utility.logWriter.writeLog("deploy successfully ");
            isDeployed = true;
            INI.iniWriteValue("MRTool", "DeployedMRFolderPath", MRFolderPath);
            INI.iniWriteValue("MRTool", "DeployedMRConfigPath", MRInfoPath);
            INI.iniWriteValue("MRTool", "DeployedMRPackagePath", PackagePath);
            return true;
        }

        private bool ifDeploy()
        {
            if (isDeployed)
            {
                DialogResult result = MessageBox.Show("A deployment already applied, re-deploy?", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (result.ToString() == "OK")
                {
                    clearDeployment();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        private void clearDeployment()
        {
            DeployTasks.Clear();
            for (int i = 0; i < MRExecuteUnits.Count; i++)
            {
                MRExecuteUnits[i].finish();
                MRExecuteUnits[i].removeTimer();
            }
            MRExecuteUnits.Clear();
            lateForScheduleUnits.Clear();

            isDeployed = false;
            label6.Text = "";
            label7.Text = "";
            label8.Text = "";
            //richTextBox2.Text = "";
            setState(initialState);
        }

        public void doDeploy(object obj)
        {
            if (ifDeploy())
            {
                if (getMRInfo())
                {
                    if (IsOverMREndTime())
                    {
                        setState(finishedMRState);
                    }
                    else
                    {
                        if (deploy())
                        {
                            setState(deployedState);
                        }
                        else
                        {
                            setState(deployErrorState);
                            for (int i = 0; i < MRExecuteUnits.Count; i++)
                            {
                                MRExecuteUnits[i].removeTimer();
                            }
                        }
                    }
                    this.Invoke(new Del_UIdisplay(setMRInfo));
                }
                else
                {
                    setState(deployErrorState);
                }
            }
        }

        public void doFinish(object obj)
        {
            if (finish())
            {
                setState(finishedMRState);
            }
            else
            {
                setState(endUpFailState);
            }
        }

        public void doRestart(object obj)
        {
            if (restart())
            {
                isLateForBeforeMRSchedule = false;
                if (DeployedStateTrigger.isUnderSchedule())
                {
                    setState(deployedState);
                }
                else
                {
                    setState(doingMRState);
                }
            }
            else
            {
                setState(startUpFailState);
            }
        }

        public void threadpoolAction(WaitCallback function)
        {
            try
            {
                ThreadPool.QueueUserWorkItem(function);
            }
            catch (Exception ex)
            {
                utility.logWriter.writeErrorLog(ex);
            }
        }

        public void setNormalLabel(Label label, string text)
        {
            label.ForeColor = Color.SeaGreen;
            label.Text = text;
        }

        public void setErrorLabel(Label label, string text)
        {
            label.ForeColor = Color.IndianRed;
            label.Text = text;
        }

        public void setMRInfo()
        {
            try
            {
                label6.Text = MRName;
                label7.Text = MRStartTime.ToShortDateString();
                label8.Text = MRStartTime.ToString("HH:mm:ss") + " -- " + MREndTime.ToString("HH:mm:ss");
                richTextBox2.Text = Notice;
            }
            catch (Exception ex)
            {
                utility.logWriter.writeErrorLog(ex);
            }
        }

        public void addNormalLog(string detail)
        {
            ListViewItem lvi = new ListViewItem();
            lvi.SubItems.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            lvi.SubItems.Add(detail);
            listView1.BeginUpdate();
            listView1.Items.Add(lvi);
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listView1.Columns[0].Width = 0;
            listView1.EndUpdate();
        }

        public void addErrorLog(string detail)
        {
            ListViewItem lvi = new ListViewItem();
            lvi.SubItems.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            lvi.SubItems.Add(detail);
            listView1.BeginUpdate();
            listView1.Items.Add(lvi);
            lvi.BackColor = Color.IndianRed;
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listView1.Columns[0].Width = 0;
            listView1.EndUpdate();
        }

        public void clearLogs()
        {
            listView1.BeginUpdate();
            listView1.Items.Clear();
            listView1.EndUpdate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog fileDialog = new OpenFileDialog();
                fileDialog.Multiselect = true;
                fileDialog.Title = "Please Select the MR Info file";
                fileDialog.Filter = "XML(*.xml*)|*.xml";
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    string file = fileDialog.FileName;
                    textBox1.Text = file;
                    MRInfoPath = file;
                }
            }
            catch (Exception ex)
            {
                utility.logWriter.writeErrorLog(ex);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog fileDialog = new OpenFileDialog();
                fileDialog.Multiselect = true;
                fileDialog.Title = "Please Select the MRServer package";
                fileDialog.Filter = "Zip(*.zip*)|*.zip";
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    string file = fileDialog.FileName;
                    textBox2.Text = file;
                    PackagePath = file;
                }
            }
            catch (Exception ex)
            {
                utility.logWriter.writeErrorLog(ex);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            setState(initialState);
            loadDeployedMR();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim().Equals( ""))
            {
                MessageBox.Show("Please input MR information file");
            }
            else
            {
                state.deploy();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            clearDeployment();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            state.restart();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to shutdown all the MR servers ?", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (result.ToString() == "OK")
            {
                state.finish();
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("If exits program, all the MR tasks won't execute, still exit?", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (result.ToString() != "OK")
            {
                e.Cancel = true;
                return;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MakeMRFiles form = new MakeMRFiles();
            form.Show();
        }

        //public bool createTaskFile(XmlNodeList xnl)
        //{
        //    try
        //    {
        //        TaskPath = RootFolderPath + "/OmsChecker/Task.txt";
        //        FileStream fs;
        //        if (!File.Exists(TaskPath))
        //        {
        //            fs = new FileStream(TaskPath, FileMode.Create, FileAccess.Write);
        //        }
        //        else
        //        {
        //            fs = new FileStream(TaskPath, FileMode.Open, FileAccess.Write);
        //        }
        //        StreamWriter sw = new StreamWriter(fs);
        //        foreach (XmlNode xn in xnl)
        //        {
        //            sw.Write(xn.InnerText + "\r\n");
        //        }
        //        sw.Close();
        //        fs.Close();
        //        utility.logWriter.writeLog("create omsCheck task file successfully");
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        utility.logWriter.writeErrorLog(ex);
        //        return false;
        //    }
        //}

        //private string createFile(string content, string file)
        //{
        //    try
        //    {
        //        FileStream fs;
        //        if (!File.Exists(file))
        //        {
        //            fs = new FileStream(file, FileMode.Create, FileAccess.Write);
        //        }
        //        else
        //        {
        //            fs = new FileStream(file, FileMode.Open, FileAccess.Write);
        //        }
        //        StreamWriter sw = new StreamWriter(fs);
        //        sw.Write(content);
        //        sw.Flush();
        //        sw.Close();
        //        fs.Close();
        //        utility.logWriter.writeLog("create file successfully, path:" + file);
        //        return file;
        //    }
        //    catch (Exception ex)
        //    {
        //        utility.logWriter.writeLog("create file failed: " + ex.ToString());
        //        return null;
        //    }
        //}

        //public Task backUpDB(DateTime time)
        //{
        //    string backupPath = config.iniReadValue("OMSDATA", "BackupPath").Trim();
        //    string sqlcmd = "\"BACKUP DATABASE [omsdata] TO  DISK = '" + backupPath + "/omsdata_MR" + DateTime.Now.ToShortDateString() + ".db'\"";
        //    string content = "tclsh ../DBOperation/DBOperation.tcl "+sqlcmd+" \r\nexit";
        //    return new Task("Omsdata_Backup", createFile(content, MRFolderPath + "/" + "Omsdata_Backup" + ".bat"), time);
        //}

        //public Task clearDB(DateTime time)
        //{
        //    string sqlcmd = "\"delete from OMSTrade_File;delete from Exch_File;delete from TradeFeed_File;delete from Trans_File\"";
        //    string content = "tclsh ../DBOperation/DBOperation.tcl " + sqlcmd + " \r\nexit";
        //    return new Task("Omsdata_Clear", createFile(content, MRFolderPath + "/Omsdata_Clear.bat"), time);
        //}

        //public Task OMSCheck(DateTime time)
        //{
        //    return new checkTask("OmsCheck", "C:/oms-bin-rehearsal/OmsChecker/OMSChecker.cmd", time);
        //}

        //private void deleteFolder(string folderPath)
        //{
        //    try
        //    {
        //        foreach (string f in Directory.GetFileSystemEntries(folderPath))
        //        {
        //            if (File.Exists(f))
        //            {
        //                FileInfo fi = new FileInfo(f);
        //                if (fi.Attributes.ToString().IndexOf("Readonly") != 1)
        //                {
        //                    fi.Attributes = FileAttributes.Normal;
        //                }
        //                File.Delete(f);
        //            }
        //            else
        //            {
        //                deleteFolder(f);
        //            }
        //        }
        //        Directory.Delete(folderPath);
        //        utility.logWriter.writeLog("delete old servers folder successfully. path: " + folderPath);
        //    }
        //    catch (Exception ex)
        //    {
        //        utility.logWriter.writeErrorLog(ex);
        //    }
        //}
    }
}
