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
    public partial class Load : Form
    {
        MakeMRFiles MMR;
        private string infoPath = "";
        private string packagePath = "";
        public Load(MakeMRFiles form)
        {
            InitializeComponent();
            this.MMR = form;
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
                    textBox1.Text = fileDialog.FileName;
                    infoPath = fileDialog.FileName;
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
                    textBox2.Text = fileDialog.FileName;
                    packagePath = fileDialog.FileName;
                }
            }
            catch (Exception ex)
            {
                utility.logWriter.writeErrorLog(ex);
            }
        }

        private void button_Deploy_Click(object sender, EventArgs e)
        {
            MMR.load(infoPath, packagePath);
            this.Close();
        }
    }
}
