using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyinfoWPF.Model
{
    [Obsolete]
    public class MachineRecord
    {
        internal DateTime datetime { get; set; }
        internal string serial_number { get; set; }
        internal int print_counter_black_and_white { get; set; }

        public string print_counter_color { get; internal set; }
        public string scan_counter { get; internal set; }
        public string tonerlevel_c { get; internal set; }
        public string tonerlevel_y { get; internal set; }
        public string tonerlevel_m { get; internal set; }
        public string tonerlevel_k { get; internal set; }
    }
}
