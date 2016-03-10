using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace MRTools
{
    class FinishedMRState:State
    {
        public MainForm MR;
        private string stateName = "FinishedMRState";
        public FinishedMRState(MainForm mr)
        {
            this.MR = mr;
        }

        public void initial() 
        {
            utility.logWriter.writeLog("state changed to: " + stateName);
            if (MR.IsOverMREndTime())
            {
                MR.setNormalLabel(MR.Label1, "MR End");
                MR.setNormalLabel(MR.Label2, "It's over MR end time");
                MR.RichTextBox1.Text = "You can re-deploy by click deploy button";
                MR.Button1.Enabled = true;
                MR.Button2.Enabled = true;
                MR.Button_Deploy.Enabled = true;
                MR.Button_Clear.Enabled = true;
                MR.Button_Finish.Enabled = false;
                MR.Button_Restart.Enabled = false;
            }
            else
            {
                MR.setNormalLabel(MR.Label1, "Finished MR");
                MR.setNormalLabel(MR.Label2, "MR servers have been shutdown");
                MR.RichTextBox1.Text = "If need restart servers, click restart button";
                MR.Button1.Enabled = true;
                MR.Button2.Enabled = true;
                MR.Button_Deploy.Enabled = true;
                MR.Button_Clear.Enabled = true;
                MR.Button_Finish.Enabled = false;
                MR.Button_Restart.Enabled = false;
            }
        }

        public void deploy()
        {
            MR.threadpoolAction(new WaitCallback(MR.doDeploy));
        }

        public void restart()
        {
            //MR.threadpoolAction(new WaitCallback(MR.doRestart));
        }

        public void finish()
        {

        }

        public void triggerStateChange()
        {
        }
    }
}
