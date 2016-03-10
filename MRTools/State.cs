using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MRTools
{
   public interface State
    {
        void deploy();
        void restart();
        void finish();
        void initial();
        void triggerStateChange();
    }
}
