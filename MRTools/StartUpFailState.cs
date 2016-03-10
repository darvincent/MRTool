using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace MRTools
{
    class StartUpFailState:State
    {
        public MainForm MR;
        private string stateName = "StartUpFailState";
        public StartUpFailState(MainForm mr)
        {
            this.MR = mr;
        }

        public void initial()
        {
            utility.logWriter.writeLog("state changed to: " + stateName);
            MR.Button1.Enabled = true;
            MR.Button2.Enabled = true;
            MR.Button_Deploy.Enabled = true;
            MR.Button_Clear.Enabled = true;
            MR.Button_Finish.Enabled = false;
            MR.Button_Restart.Enabled = true;
            MR.setErrorLabel(MR.Label1, "Schedule execute Failed");
            MR.setErrorLabel(MR.Label2, "Environment not ready for MR");
            MR.RichTextBox1.Text = " You can try to manual start all the tasks, Or email to Ebroker for issue handling";
        }

        public void deploy()
        {
            MR.threadpoolAction(new WaitCallback(MR.doDeploy));
        }

        public void restart()
        {
            MR.threadpoolAction(new WaitCallback(MR.doRestart));
        }

        public void finish()
        {
            MR.threadpoolAction(new WaitCallback(MR.doFinish));
        }

        public void triggerStateChange()
        {
        }
    }
}
