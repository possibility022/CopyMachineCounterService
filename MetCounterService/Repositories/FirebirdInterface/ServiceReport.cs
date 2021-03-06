﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.FirebirdInterface
{
    public class ServiceReport
    {
        public int DeviceID { get; set; }
        public DateTime DateOfServiceClosed { get; set; }
        public int Counter { get; set; }
        public string Technican { get; set; }
        public string ReportedProblem { get; set; }
        public string Description { get; set; }
        public string SeviceRecomendation { get; set; }
    }
}
