using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace MRTools
{
    public abstract class Task
    {
        public MainForm MR;
        protected  Process process;
        protected string taskName;
        protected  string path;
        protected  string executeFileName;
    }
}
