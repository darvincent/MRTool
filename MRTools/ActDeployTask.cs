using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MRTools
{
    public partial class ActDeployTask : Form
    {
        private int Type;
        private MakeMRFiles MMRForm;
        private ListViewItem lvi;
        private string executeFilePath = "";
        public ActDeployTask(MakeMRFiles form)
        {
            // new
            InitializeComponent();
            button1.Text = "Add";
            button2.Text = "Clear";
            Type = 0;
            MMRForm = form;
        }

        public ActDeployTask(MakeMRFiles form, ListViewItem lvi)
        {
            //edit
            InitializeComponent();
            button1.Text = "Save";
            button2.Text = "Delete";
            Type = 1;    
            MMRForm = form;
            this.lvi = lvi;
            string taskName = lvi.SubItems[1].Text;
            TB_TaskName.Text =taskName ;
            if (MMRForm.deployServerPath.ContainsKey(taskName))
            {
                TB_ExecuteFile.Text = MMRForm.deployServerPath[taskName];
            }
            CB_IsInPackage.Checked = lvi.SubItems[2].Text == "Y" ? true : false;
            TB_Path.Text = lvi.SubItems[3].Text;
            TB_Sequence.Text = lvi.SubItems[4].Text;
            TB_Duration.Text = lvi.SubItems[5].Text; 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sequence = TB_Sequence.Text.Trim();
            string duration = TB_Duration.Text.Trim();
            string taskName = TB_TaskName.Text.Trim();
            string path = TB_Path.Text.Trim();
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
            if (sequence.Equals(""))
            {
                MessageBox.Show("Please input sequence");
                return;
            }
            else
            {
                try
                {
                    if (Convert.ToInt32(sequence) <= 0)
                    {
                        MessageBox.Show("sequence must be positive");
                        return;
                    }
                }
                catch
                {
                    MessageBox.Show("sequence must be integer");
                    return;
                }
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
            if (Type == 0)
            {
                if (MMRForm.deployTasks_Seq.ContainsValue(sequence))
                {
                    MessageBox.Show("the sequence " + sequence + " is already been set");
                    return;
                }
                lvi = new ListViewItem();
                lvi.SubItems.Add(taskName);
                lvi.SubItems.Add(CB_IsInPackage.Checked ? "Y" : "N");
                lvi.SubItems.Add(path);
                lvi.SubItems.Add(sequence);
                lvi.SubItems.Add(duration);
                MMRForm.Listview1.BeginUpdate();
                MMRForm.Listview1.Items.Insert(0, lvi);
                MMRForm.Listview1.EndUpdate();
                MMRForm.deployTasks_Seq.Add(taskName, sequence);
                if (TB_ExecuteFile.Enabled && !executeFilePath.Equals(""))
                {
                    MMRForm.deployServerPath.Add(taskName, executeFilePath);
                }
            }
            else
            {
                string exSeq = lvi.SubItems[4].Text;
                string exTaskName = lvi.SubItems[1].Text;
                if (MMRForm.deployTasks_Seq.ContainsValue(sequence) && exSeq != sequence)
                {
                    MessageBox.Show("the sequence " + sequence + " is already been set");
                    return;
                }
                MMRForm.Listview1.BeginUpdate();
                lvi.SubItems[1].Text = taskName;
                lvi.SubItems[2].Text = CB_IsInPackage.Checked ? "Y" : "N";
                lvi.SubItems[3].Text = path;
                lvi.SubItems[4].Text = sequence;
                lvi.SubItems[5].Text = duration;
                MMRForm.Listview1.EndUpdate();
                MMRForm.deployTasks_Seq.Remove(exTaskName);
                MMRForm.deployTasks_Seq.Add(taskName, sequence);
                if (MMRForm.deployServerPath.ContainsKey(exTaskName))
                {
                    MMRForm.deployServerPath.Remove(exTaskName);
                }
                if (TB_ExecuteFile.Enabled && !executeFilePath.Equals(""))
                {
                    MMRForm.deployServerPath.Add(taskName, executeFilePath);
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
                MMRForm.Listview1.BeginUpdate();
                MMRForm.Listview1.Items.Remove(lvi);
                MMRForm.Listview1.EndUpdate();
                string taskName = lvi.SubItems[1].Text;
                MMRForm.deployTasks_Seq.Remove(taskName);
                if (MMRForm.deployServerPath.ContainsKey(taskName))
                {
                    MMRForm.deployServerPath.Remove(taskName);
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
    }
}
