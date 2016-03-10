using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MRTools
{
    public class DeployedTriggerUnit:ExecuteUnit
    {
        private System.Timers.Timer timer;
        private MainForm MR;
        public DeployedTriggerUnit(MainForm MR)
        {
            this.MR = MR;
        }

        public override bool execute()
        {
            return true;
        }

        private void Timer_execute(object obj, System.Timers.ElapsedEventArgs e)
        {
            timer.Close();
            MR.deployedState.triggerStateChange();
        }

        public override bool finish()
        {
            return true;
        }

        public override bool setSchedule()
        {
            int seconds = utility.returnSeconds(DateTime.Now, MR.MRStartTime);
            if (seconds > 0)
            {
                try
                {
                    timer = new System.Timers.Timer(1000 * seconds);
                    timer.Elapsed += new System.Timers.ElapsedEventHandler(new del(Timer_execute));
                    timer.AutoReset = false;
                    timer.Enabled = true;
                    return true;
                }
                catch (Exception ex)
                {
                    utility.logWriter.writeErrorLog(ex);
                    return false;
                }
            }
            else
            {
                MR.deployedState.triggerStateChange();
            }
            return true;
        }

        public override bool isUnderSchedule()
        {
            return true;
        }

        public override void removeTimer()
        {

        }
    }
}
