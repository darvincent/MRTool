using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace MRTools
{
    class DoingMRState:State
    {
        private MainForm MR;
        private string stateName = "DoingMRState";
        public DoingMRState(MainForm mr)
        {
            this.MR = mr;
        }

        public void initial()
        {
            utility.logWriter.writeLog("state changed to: " + stateName);
            MR.Button1.Enabled = false;
            MR.Button2.Enabled = false;
            MR.Button_Deploy.Enabled = false;
            MR.Button_Clear.Enabled = false;
            MR.Button_Finish.Enabled = true;
            MR.Button_Restart.Enabled = true;
            MR.setNormalLabel(MR.Label1, "Doing MR");
            MR.setNormalLabel(MR.Label2, "MR is under progress");
            MR.RichTextBox1.Text = "you can manually finish if your MR procedure is done";
        }

        public void deploy()
        { 
            
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
            if (MR.isTasksAfterMROK)
            {
                MR.setState(MR.finishedMRState);
            }
            else
            {
                MR.setState(MR.endUpFailState);
            }
        }

    }
}
