using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Drawing;

namespace MRTools
{
    class InitialState: State
    {
        public MainForm MR;
        public InitialState(MainForm mr)
        {
            this.MR = mr;
        }

        public void initial()
        {
            MR.setNormalLabel(MR.Label1, "No Schedule");
            MR.setNormalLabel(MR.Label2, "");
            MR.RichTextBox1.Text = "Please import MR information file for deplyment ";
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
