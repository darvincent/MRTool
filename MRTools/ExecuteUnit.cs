using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MRTools
{
    public abstract class ExecuteUnit
    {
        public DateTime executeTime;
        public bool isDeployedStateTrigger = false;
        public bool isDoingMRStateTrigger = false;
        protected delegate void del(object obj, System.Timers.ElapsedEventArgs e);
        public ExecuteUnit()
        { 
        
        }
        public ExecuteUnit(MRTask t)
        { 
        
        }

        public virtual bool execute()
        {
            return true;
        }

        public virtual bool finish()
        {
            return true;
        }

        public virtual bool setSchedule()
        {
            return true;
        }

        public virtual bool isUnderSchedule()
        {
            return true;
        }

        public virtual void removeTimer()
        {

        }
    }
}
