using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace MRTools
{
    public partial class ActMRTask : Form
    {
        private int Type;
        private MakeMRFiles MMRForm;
        private ListViewItem lvi;
        private string executeFilePath = "";
        public ActMRTask(MakeMRFiles form)
        {
            InitializeComponent();
            button1.Text = "Add";
            button2.Text = "Clear";
            Type = 0;
            MMRForm = form;
        }

        public ActMRTask(MakeMRFiles form,ListViewItem lvi)
        {
            InitializeComponent();
            button1.Text = "Save";
            button2.Text = "Delete";
            Type = 1;
            MMRForm = form;
            this.lvi = lvi;
            string taskName = lvi.SubItems[1].Text;
            TB_TaskName.Text = taskName;
            if (MMRForm.MRServerPath.ContainsKey(taskName))
            {
                TB_ExecuteFile.Text = MMRForm.MRServerPath[taskName];
            }
            CB_IsInPackage.Checked = lvi.SubItems[2].Text == "Y" ? true : false;
            TB_Path.Text = lvi.SubItems[3].Text;
            TB_Port.Text = lvi.SubItems[4].Text;
            dateTimePicker1.Value = Convert.ToDateTime(lvi.SubItems[5].Text);
            if (lvi.SubItems[6].Text.Equals(""))
            {
                dateTimePicker2.Value = DateTime.Now;
            }
            else
            {
                CB_doShut.Checked = true;
                dateTimePicker2.Value = Convert.ToDateTime(lvi.SubItems[6].Text);
            }
            TB_Duration.Text = lvi.SubItems[7].Text;
            CB_doRestart.Checked = lvi.SubItems[8].Text == "Y" ? true : false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string taskName = TB_TaskName.Text.Trim();
            string duration = TB_Duration.Text.Trim();
            string path = TB_Path.Text.Trim();
            string port = TB_Port.Text.Trim();
            string endTime = dateTimePicker2.Enabled ? dateTimePicker2.Value.ToLongTimeString() : "";
            if (taskName.Equals(""))
            {
                MessageBox.Show("Please input task name");
                return;
            }
            if (path.Equals(""))
            {
                MessageBox.Show("Please input path");
                return;
            }
            if (duration.Equals(""))
            {
                MessageBox.Show("Please input task execute duration");
                return;
            }
            else
            {
                try
                {
                    if (Convert.ToInt32(duration) <= 0)
                    {
                        MessageBox.Show("Dutation must be positive");
                        return;
                    }
                }
                catch
                {
                    MessageBox.Show("Dutation must be integer");
                    return;
                }
            }
            if (!port.Equals(""))
            {
                try
                {
                    if (Convert.ToInt32(port) <= 0)
                    {
                        MessageBox.Show("Port must be positive");
                        return;
                    }
                }
                catch
                {
                    MessageBox.Show("Port must be integer");
                    return;
                }
            }
            if (dateTimePicker2.Enabled && dateTimePicker1.Value.CompareTo(dateTimePicker2.Value) >= 0)
            {
                MessageBox.Show("Task start time must earlier than end time");
                return;
            }
            if (Type == 0)
            {
                if (MMRForm.MRTasks_Lvi.ContainsKey(taskName))
                {
                    MessageBox.Show("taskName: " + taskName + " already exists");
                    return;
                }

                lvi = new ListViewItem();
                lvi.SubItems.Add(taskName);
                lvi.SubItems.Add(CB_IsInPackage.Checked ? "Y" : "N");
                lvi.SubItems.Add(path);
                lvi.SubItems.Add(port);
                lvi.SubItems.Add(dateTimePicker1.Value.ToLongTimeString());
                lvi.SubItems.Add(endTime);
                lvi.SubItems.Add(duration);
                lvi.SubItems.Add(CB_doRestart.Checked ? "Y" : "N");
                MMRForm.Listview2.BeginUpdate();
                MMRForm.Listview2.Items.Insert(0, lvi);
                MMRForm.Listview2.EndUpdate();
                MMRForm.MRTasks_Lvi.Add(taskName, lvi);
                if (TB_ExecuteFile.Enabled && !executeFilePath.Equals(""))
                {
                    MMRForm.MRServerPath.Add(taskName, executeFilePath);
                }
            }
            else
            {
                string exTaskName = lvi.SubItems[1].Text;
                if (!exTaskName.Equals(taskName) && MMRForm.MRTasks_Lvi.ContainsKey(taskName))
                {
                    MessageBox.Show("taskName: " + taskName + " already exists");
                    return;
                }
                MMRForm.Listview2.BeginUpdate();
                lvi.SubItems[1].Text = taskName;
                lvi.SubItems[2].Text = CB_IsInPackage.Checked ? "Y" : "N";
                lvi.SubItems[3].Text = path;
                lvi.SubItems[4].Text = port;
                lvi.SubItems[5].Text = dateTimePicker1.Value.ToLongTimeString();
                lvi.SubItems[6].Text = endTime;
                lvi.SubItems[7].Text = duration;
                lvi.SubItems[8].Text = CB_doRestart.Checked ? "Y" : "N";
                MMRForm.Listview2.EndUpdate();
                if (MMRForm.MRServerPath.ContainsKey(exTaskName))
                {
                    MMRForm.MRServerPath.Remove(exTaskName);
                }
                if (TB_ExecuteFile.Enabled && !executeFilePath.Equals(""))
                {
                    MMRForm.MRServerPath.Add(taskName, executeFilePath);
                }
            }
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Type == 0)
            {
                foreach (Control box in this.Controls)
                {
                    if (box is ComboBox || box is TextBox || box is RichTextBox)
                    {
                        box.Text = "";
                    }
                }
                foreach (CheckBox b in this.Controls)
                {
                    b.Checked = false;
                }
            }
            else
            {
                MMRForm.Listview2.BeginUpdate();
                MMRForm.Listview2.Items.Remove(lvi);
                MMRForm.Listview2.EndUpdate();
                string taskName = lvi.SubItems[1].Text;
                MMRForm.MRTasks_Lvi.Remove(taskName);
                if (MMRForm.MRServerPath.ContainsKey(taskName))
                {
                    MMRForm.MRServerPath.Remove(taskName);
                }
            }
            this.Close();
        }

        private void button_import_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog fileDialog = new OpenFileDialog();
                fileDialog.Multiselect = false;
                fileDialog.Title = "Please Select the execute file";
                //fileDialog.Filter = "XML(*.xml*)|*.xml";
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    string file = fileDialog.FileName;
                    TB_ExecuteFile.Text = file;
                    executeFilePath = file;
                    TB_Path.Text = utility.getFileNameFromPath(executeFilePath);
                }
            }
            catch (Exception ex)
            {
                utility.logWriter.writeErrorLog(ex);
            }
        }

        private void CB_IsInPackage_CheckedChanged(object sender, EventArgs e)
        {
            if (CB_IsInPackage.Checked)
            {
                TB_ExecuteFile.Enabled = true;
                button_import.Enabled = true;
            }
            else
            {
                TB_ExecuteFile.Clear();
                executeFilePath = "";
                TB_ExecuteFile.Enabled = false;
                button_import.Enabled = false;
            }
        }

        private void CB_doShut_CheckedChanged(object sender, EventArgs e)
        {
            if (CB_doShut.Checked)
            {
                dateTimePicker2.Enabled = true;
            }
            else
            {
                dateTimePicker2.Enabled = false;
            }
        }

    }
}
