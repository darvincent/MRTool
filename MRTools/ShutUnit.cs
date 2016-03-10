using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MRTools
{
    public class ShutUnit:ExecuteUnit
    {
        private MRTask task;
        private System.Timers.Timer timer;
        public ShutUnit(MRTask t)
        {
            task = t;
            executeTime = t.EndTime;
        }

        public override bool execute()
        {
            return task.doShut();
        }

        public override bool finish()
        {
            bool result = true;
            try
            {
                if (timer != null)
                {
                    if (timer.Enabled)
                    {
                        timer.Close();
                        result = result && task.doShut();
                    }
                }
            }
            catch (Exception ex)
            {
                utility.logWriter.writeErrorLog(ex);
                result = false;
            }
            return result;
        }

        private void Timer_execute(object obj, System.Timers.ElapsedEventArgs e)
        {
            lock (MainForm.obj)
            {
                if (!task.doShut())
                {
                    if (executeTime.CompareTo(task.MR.MREndTime) > 0)
                    {
                        task.MR.isTasksAfterMROK = false;
                    }
                    if (executeTime.CompareTo(task.MR.MRStartTime) < 0)
                    {
                        task.MR.isTasksBeforeMROK = false;
                    }
                }
                task.MR.lateForScheduleUnits_add(this);
                timer.Close();
                if (isDoingMRStateTrigger)
                {
                    task.MR.doingMRState.triggerStateChange();
                }
            }
        }

        public override bool setSchedule()
        {
            int seconds = utility.returnSeconds(DateTime.Now, executeTime);
            if (seconds > 0)
            {
                try
                {
                    timer = new System.Timers.Timer(1000 * seconds);
                    timer.Elapsed += new System.Timers.ElapsedEventHandler(new del(Timer_execute));
                    timer.AutoReset = false;
                    timer.Enabled = true;
                    utility.logWriter.writeLog(task.TaskName + " set shut down timer at: " + executeTime);
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
                task.MR.lateForScheduleUnits_add(this);
                utility.logWriter.writeLog("schedule for shut " + task.TaskName + " is expired");
            }
            return true;
        }

        public override bool isUnderSchedule()
        {
            if (timer != null)
            {
                return timer.Enabled;
            }
            else
            {
                return false;
            }
        }

        public override void removeTimer()
        {
            if (timer != null)
            {
                if (timer.Enabled)
                {
                    timer.Close();
                }
            }
        }

    }
}
