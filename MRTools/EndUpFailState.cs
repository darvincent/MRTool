using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;


namespace MRTools
{
    class EndUpFailState:State
    {
        public MainForm MR;
        private string stateName = "EndUpFailState";
        public EndUpFailState(MainForm mr)
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
            MR.Button_Restart.Enabled = false;
            MR.setErrorLabel(MR.Label1, "Close MR tasks Failed");
            MR.setErrorLabel(MR.Label2, "Some MR tasks remain running");
            MR.RichTextBox1.Text = "You can try to finish the tasks manually, Or email to Ebroker for issue reporting";
        }

        public void deploy()
        { 
            
        }

        public void restart()
        { 
            
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
