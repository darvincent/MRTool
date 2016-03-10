using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Drawing;

namespace MRTools
{
    class DeployErrorState:State
    {
        public MainForm MR;
        private string stateName = "DeployErrorState";
        public DeployErrorState(MainForm mr)
        {
            this.MR = mr;
        }

        public void initial()
        {
            utility.logWriter.writeLog("state changed to: " + stateName);
            MR.setErrorLabel(MR.Label1, "Deploy ERROR");
            MR.setErrorLabel(MR.Label2, "Please verify your input files");
            MR.RichTextBox1.Text = "Try to deploy one more time Or Email to Ebroker for issue reporting ";
            MR.Button1.Enabled = true;
            MR.Button2.Enabled = true;
            MR.Button_Deploy.Enabled = true;
            MR.Button_Clear.Enabled = true;
            MR.Button_Finish.Enabled = false;
            MR.Button_Restart.Enabled = false;
        }

        public void deploy()
        {
            MR.threadpoolAction(new WaitCallback(MR.doDeploy));
        }

        public void restart()
        { 
            
        }

        public void finish()
        { 
            
        }

        public void triggerStateChange()
        {
        }
    }
}
