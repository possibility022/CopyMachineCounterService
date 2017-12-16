using CopyinfoWPF.Common;
using System;

namespace CopyinfoWPF.Database
{
    class ServiceReport
    {
        public int DeviceID { get; set; }
        public DateTime DateOfServiceClosed { get; set; }
        public int Counter { get; set; }
        public string Technican { get; set; }
        public string ReportedProblem { get; set; }
        public string Description { get; set; }
        public string SeviceRecomendation { get; set; }

        public override string ToString()
        {
            return DateOfServiceClosed.ToString(CommonData.DateTimeFormat) + " " + Technican;
        }
    }
}
