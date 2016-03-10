using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MRTools
{
    class ExeUnitTimeComparer:IComparer<ExecuteUnit>
    {
        public int Compare(ExecuteUnit u1, ExecuteUnit u2)
        {
            return (u1.executeTime.CompareTo(u2.executeTime));
        }
    }

    //class DeployTaskComparer : IComparable<DeployTask>
    //{
    //    public int Compare(DeployTask t1, DeployTask t2)
    //    {
    //        return (t1.Sequence.CompareTo(t2.Sequence));
    //    }
    //}
}
