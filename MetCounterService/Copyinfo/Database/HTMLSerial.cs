﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MongoDB.Bson;

namespace Copyinfo.Database
{
    class HTMLSerial
    {
        public ObjectId id { get; set; }
        public string full_serial { get; set; }
    }
}
