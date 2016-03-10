using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Drawing;

namespace MRTools
{
    class DeployedState:State
    {
        public MainForm MR;
        private string stateName = "DeployedState";
        public DeployedState(MainForm mr)
        {
            this.MR = mr;
        }

        public void initial()
        {
            utility.logWriter.writeLog("state changed to: " + stateName);
            MR.setNormalLabel(MR.Label1, "Deployment done");
            MR.Button1.Enabled = true;
            MR.Button2.Enabled = true;
            MR.Button_Deploy.Enabled = true;
            MR.Button_Clear.Enabled = true;
            MR.Button_Finish.Enabled = false;      
            if (!MR.IsLateForBeforeMRSchedule)
            {
                MR.setNormalLabel(MR.Label2, "In accordance with the schedule to start tasks");
                MR.RichTextBox1.Text = "Tasks will auto start ";
                MR.Button_Restart.Enabled = false;
            }
            else
            {
                MR.setErrorLabel(MR.Label2, "It's late for some tasks shceduling");
                MR.RichTextBox1.Text = "please manually restart those tasks by click \"Restart\" button";
                MR.Button_Restart.Enabled = true;
            }
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
            
        }

        public void triggerStateChange()
        {
            if (MR.isTasksBeforeMROK)
            {
                MR.setState(MR.doingMRState);
            }
            else
            {
                MR.setState(MR.startUpFailState);
            }
        }

    }
}
