﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copyinfo
{
    static class Global
    {
        public static Database.MongoTB database;

        public static void Initialize()
        {
            Database.MongoTB.Initialize();
            database = new Database.MongoTB();
        }

        public static void log(string message)
        {

        }

        public static void openEmailAttachment(string file)
        {
            file = "\"" + file + "\"";
            Process.Start(StaticSettings.RedReaderPath, file);
            //ProcessStartInfo startInfo = new ProcessStartInfo();
            //startInfo.FileName = file;
            //startInfo.Arguments = StaticSettings.RedReaderPath;
            //Process.Start(startInfo);
        }
    }
}
