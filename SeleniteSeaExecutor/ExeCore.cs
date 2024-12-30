using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniteSeaExecutor
{
    public static class ExeCore
    {
        public static string LocalDirectory => AppDomain.CurrentDomain.BaseDirectory;
        public static string WorkingDirectory { get; set; } = "";
    }
}
