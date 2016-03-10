using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml.Linq;
using System.Threading;
using ICSharpCode.SharpZipLib.Zip;
using System.Security.AccessControl;

namespace MRTools
{
    public partial class MakeMRFiles : Form
    {
        private string InfoFilePath;
        private string PackgeFolderPath;
        private string MRName;
        private string MRDate;
        private string RootFolder;
        private string StartTime;
        private string EndTime;
        private string Notice;
        private XElement Root;
        public Dictionary<string, string> deployTasks_Seq = new Dictionary<string, string>();
        public Dictionary<string, ListViewItem> MRTasks_Lvi = new Dictionary<string, ListViewItem>();
        public Dictionary<string, string> deployServerPath = new Dictionary<string, string>();
        public Dictionary<string, string> MRServerPath = new Dictionary<string, string>();
        private bool listview1_Sorter = true;
        private bool listview2_Sorter = true;
        private ZipClass zipOp = new ZipClass();

        public MakeMRFiles()
        {
            InitializeComponent();
            comboBox_Root.Text = utility.DefaultRootPath;
            comboBox_Root.Items.Add(utility.DefaultRootPath);
        }

        private bool check()
        {
            bool result = true;
            StringBuilder errorTaskList = new StringBuilder();
            if (dateTimePicker_Date.Value.CompareTo(DateTime.Now.Date) < 0)
            {
                MessageBox.Show("MR date is expired");
                result =  false;
            }
            if (dateTimePicker_Start.Value.CompareTo(dateTimePicker_End.Value) >= 0)
            {
                MessageBox.Show("MR start time must earlier than MR end time");
                result =  false;
            }
            foreach (var item in deployServerPath)
            {
                if (!File.Exists(item.Value))
                {
                    errorTaskList.Append("can't find execute file for deploy task: ").Append(item.Key).Append("\r\n");
                    result = false;
                }
            }
            foreach (var item in MRServerPath)
            {
                if (!File.Exists(item.Value))
                {
                    errorTaskList.Append("can't find execute file for MR task: ").Append(item.Key).Append("\r\n");
                    result = false;
                }
            }
            if (errorTaskList.Length != 0)
            {
                MessageBox.Show(errorTaskList.ToString());
            }
            return result;
        }

        private void generateFile()
        {
            createInfoFile();
            PackgeFolderPath = createFolder(MRName + "_" + MRDate);
            writeMRInfo();
            writeDeployTask(listView1);
            writeMRTask(listView2);
            ThreadPool.QueueUserWorkItem(new WaitCallback(Th_addToPackage));
        }

        private void createInfoFile()
        {
            InfoFilePath = MRName + "_" + MRDate + ".xml";
            FileStream fs = new FileStream(InfoFilePath, FileMode.Create);
            StreamWriter Sw = new StreamWriter(fs);
            Sw.Write("<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<Info />");
            Sw.Close();
            fs.Close();
            Root = XElement.Load(@InfoFilePath);
        }

        private string createFolder(string folderName)
        {
            try
            {
                string path = ".\\" + folderName;
                if (!Directory.Exists(path))
                {
                    DirectoryInfo di = Directory.CreateDirectory(path);
                    //var security = new DirectorySecurity();
                    //security.AddAccessRule(new FileSystemAccessRule("NETWORK SERVICE", FileSystemRights.FullControl, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow));
                    //security.AddAccessRule(new FileSystemAccessRule("Everyone", FileSystemRights.FullControl, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow));
                    //Directory.SetAccessControl(path, security);
                    return path;
                }
                else
                {
                    return path;
                }
            }
            catch (Exception ex)
            {
                utility.logWriter.writeErrorLog(ex);
                return "";
            }
        }

        public void Th_addToPackage(object obj)
        {
            foreach (string sourcePath in deployServerPath.Values)
            {
                string desPath = utility.genNewFilePath(PackgeFolderPath, utility.getFileNameFromPath(sourcePath));
                File.Copy(sourcePath, desPath, true);
            }
            foreach (string sourcePath in MRServerPath.Values)
            {
                string desPath = utility.genNewFilePath(PackgeFolderPath, utility.getFileNameFromPath(sourcePath));
                File.Copy(sourcePath, desPath, true);
            }
            zipOp.ZipFileFromDirectory(PackgeFolderPath, "./" + MRName + "_" + MRDate + ".zip", 6);
            if (Directory.Exists("./temp"))
            {
                Directory.Delete("./temp",true);
            }
            if (Directory.Exists(PackgeFolderPath))
            {
                Directory.Delete(PackgeFolderPath, true);
            }
        }

        private void writeMRInfo()
        {
            XElement basicInfo =
                new XElement("basicInfo",
                            new XElement("MRName", MRName),
                            new XElement("RootFolder",RootFolder),
                            new XElement("Date", MRDate),
                            new XElement("StartTime", StartTime),
                            new XElement("EndTime", EndTime),
                            new XElement("Notice", Notice)
                );
            Root.Add(basicInfo);
            Root.Save(InfoFilePath);
        }

        private void writeDeployTask(ListView lv)
        {
            XElement deployTasks = new XElement("DeployTasks");
            Root.Add(deployTasks);
            for (int i = 0; i < lv.Items.Count; i++)
            {
                XElement task = new XElement("task",
                                                            new XElement("name", lv.Items[i].SubItems[1].Text),
                                                            new XElement("IsInPackage", lv.Items[i].SubItems[2].Text),
                                                            new XElement("path", lv.Items[i].SubItems[3].Text),
                                                            new XElement("sequence", lv.Items[i].SubItems[4].Text),
                                                            new XElement("executeDuration", lv.Items[i].SubItems[5].Text)
                                                         );
                deployTasks.Add(task);
            }
            Root.Save(InfoFilePath);
        }

        private void writeMRTask(ListView lv)
        {
            XElement MRTasks = new XElement("MRTasks");
            Root.Add(MRTasks);
            for (int i = 0; i < lv.Items.Count; i++)
            {
                XElement task = new XElement("task",
                                                            new XElement("name", lv.Items[i].SubItems[1].Text),
                                                            new XElement("IsInPackage", lv.Items[i].SubItems[2].Text),
                                                            new XElement("path", lv.Items[i].SubItems[3].Text),
                                                            new XElement("port", lv.Items[i].SubItems[4].Text),
                                                            new XElement("startTime", lv.Items[i].SubItems[5].Text),
                                                            new XElement("endTime", lv.Items[i].SubItems[6].Text),
                                                            new XElement("startDuration", lv.Items[i].SubItems[7].Text),
                                                            new XElement("doRestart", lv.Items[i].SubItems[8].Text)
                                                         );
                MRTasks.Add(task);
            }
            Root.Save(InfoFilePath);
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ActDeployTask form = new ActDeployTask(this, listView1.SelectedItems[0]);
                form.Show();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ActDeployTask form = new ActDeployTask(this);
            form.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ActMRTask form = new ActMRTask(this);
            form.Show();
        }

        private void listView2_DoubleClick(object sender, EventArgs e)
        {
            if (listView2.SelectedItems.Count > 0)
            {
                ActMRTask form = new ActMRTask(this,listView2.SelectedItems[0]);
                form.Show();  
            }
        }

        private void Button_Generate_Click(object sender, EventArgs e)
        {
            MRName = textBox_MRName.Text.Trim();
            RootFolder = comboBox_Root.Text.Trim();
            Notice = richTextBox_Notice.Text.Trim();
            if (MRName.Equals("") || RootFolder.Equals(""))
            {
                MessageBox.Show("Please fill all the MRInfo");
                return;
            }
            else
            {
                MRDate = dateTimePicker_Date.Value.ToShortDateString();
                StartTime = dateTimePicker_Start.Value.ToLongTimeString();
                EndTime = dateTimePicker_End.Value.ToLongTimeString();
                if (check())
                {
                    generateFile();
                    MessageBox.Show("Info file and package has been genenrated to program path");
                }
            }
        }

        private void button_Load_Click(object sender, EventArgs e)
        {
            Load form = new Load(this);
            form.Show();
        }

        public void load(string infoPath,string packagePath)
        {
            utility.logWriter.writeLog("load infoPath: " + infoPath + "\r\npackagePath: " + packagePath);
            if (!infoPath.Equals(""))
            {
                loadMRInfo(infoPath);
                string tempFolderPath = "";
                if (!packagePath.Equals(""))
                {
                    tempFolderPath = loadPackge(packagePath);
                }
                loadDeployTask(infoPath,tempFolderPath);
                loadMRTask(infoPath,tempFolderPath);
            }
        }

        private string loadPackge(string loadPackagePath)
        {
            string tempFolderPath = createFolder("temp");
            zipOp.UnZip(loadPackagePath,tempFolderPath);
            return tempFolderPath;
        }

        private void loadMRInfo(string infoPath)
        {
            Root = XElement.Load(@infoPath);
            XElement basicInfo = Root.Element("basicInfo");
            MRName = basicInfo.Element("MRName").Value;
            RootFolder = basicInfo.Element("RootFolder").Value;
            MRDate = basicInfo.Element("Date").Value;
            StartTime = basicInfo.Element("StartTime").Value;
            EndTime = basicInfo.Element("EndTime").Value;
            Notice = basicInfo.Element("Notice").Value;
            textBox_MRName.Text = MRName;
            comboBox_Root.Text = RootFolder;
            if (!comboBox_Root.Items.Contains(RootFolder))
            {
                comboBox_Root.Items.Add(RootFolder);
            }
            dateTimePicker_Date.Value = Convert.ToDateTime(MRDate);
            dateTimePicker_Start.Value = Convert.ToDateTime(StartTime);
            dateTimePicker_End.Value = Convert.ToDateTime(EndTime);
            richTextBox_Notice.Text = Notice;
        }

        private void loadDeployTask(string infoPath, string packageFolderPath)
        {
            IEnumerable<XElement> deployTasks = from task in Root.Elements("DeployTasks").Elements("task")
                                               select task;
            if(deployTasks.Count()>0)
            {
                listView1.BeginUpdate();
                listView1.Items.Clear();
                deployTasks_Seq.Clear();
                deployServerPath.Clear();
                foreach (XElement task in deployTasks)
                {
                    string name = task.Element("name").Value.Trim();
                    if (!deployTasks_Seq.ContainsKey(name))
                    {
                        ListViewItem lvi = new ListViewItem();
                        string sequence = task.Element("sequence").Value.Trim();
                        string isInPackage = task.Element("IsInPackage").Value.Trim();
                        string path = task.Element("path").Value.Trim();
                        lvi.SubItems.Add(name);
                        lvi.SubItems.Add(isInPackage);
                        lvi.SubItems.Add(path);
                        lvi.SubItems.Add(sequence);
                        lvi.SubItems.Add(task.Element("executeDuration").Value.Trim());
                        listView1.Items.Add(lvi);
                        deployTasks_Seq.Add(name, sequence);
                        if (isInPackage.Equals("Y"))
                        {
                            deployServerPath.Add(name, utility.genNewFilePath(packageFolderPath, path));
                        }
                    }
                    else
                    {
                        utility.logWriter.writeLog("Deploy Task " + name + " already loaded");
                    }
                }
                listView1.EndUpdate();
            }  
        }

        private void loadMRTask(string infoPath, string packageFolderPath)
        {
            IEnumerable<XElement> MRTasks = from task in Root.Elements("MRTasks").Elements("task")
                                                select task;
            if (MRTasks.Count() > 0)
            {
                listView2.BeginUpdate();
                listView2.Items.Clear();
                MRTasks_Lvi.Clear();
                MRServerPath.Clear();
                foreach (XElement task in MRTasks)
                {
                    string name = task.Element("name").Value.Trim();
                    string isInPackage = task.Element("IsInPackage").Value.Trim();
                    string path = task.Element("path").Value.Trim();
                    if (!MRTasks_Lvi.ContainsKey(name))
                    {
                        ListViewItem lvi = new ListViewItem();
                        lvi.SubItems.Add(name);
                        lvi.SubItems.Add(isInPackage);
                        lvi.SubItems.Add(path);
                        lvi.SubItems.Add(task.Element("port").Value.Trim());
                        lvi.SubItems.Add(task.Element("startTime").Value.Trim());
                        lvi.SubItems.Add(task.Element("endTime").Value.Trim());
                        lvi.SubItems.Add(task.Element("startDuration").Value.Trim());
                        lvi.SubItems.Add(task.Element("doRestart").Value.Trim());
                        listView2.Items.Add(lvi);
                        MRTasks_Lvi.Add(name, lvi);
                        if (isInPackage.Equals("Y"))
                        {
                            MRServerPath.Add(name, utility.genNewFilePath(packageFolderPath, path));
                        }
                    }
                    else
                    {
                        utility.logWriter.writeLog("MR Task " + name + " already loaded");
                    }
                }
                listView2.EndUpdate();
            }  
        }

        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            int dataType = 0;
            if (e.Column == 4)
            {
                dataType = 1;
            }
            listView1.ListViewItemSorter = new ListViewItemsComparer(e.Column, dataType, listview1_Sorter);
            listView1.Sort();
            listview1_Sorter = !listview1_Sorter;
        }

        private void listView2_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            int dataType = 0;
            if (e.Column == 5 || e.Column == 6)
            {
                dataType = 2;
            }
            listView2.ListViewItemSorter = new ListViewItemsComparer(e.Column, dataType, listview2_Sorter);
            listView2.Sort();
            listview2_Sorter = !listview2_Sorter;
        }

    }
}
