﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using System.IO;
using System.Net;
using System.Diagnostics;


namespace WindowsMetService
{
    class Global
    {
        static System.Diagnostics.EventLog eventLog1;

        public static void SetSystemEventLog(EventLog log)
        {
            if (eventLog1 == null) eventLog1 = log;
        }

        public static StreamReader ExecuteCommandLine(string file, string arguments = "")
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = true;
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.FileName = file;
            startInfo.Arguments = arguments;

            Process process = Process.Start(startInfo);

            return process.StandardOutput;
        }

        #region regexmethods

        private const string regexIP = "(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)(\\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)){3}";
        private const string regexMac = "((([0-9]|[a-zA-Z]){2}-){5}([0-9]|[a-zA-Z]){2})";

        public static IPAddress GetAddressIp(string text)
        {
            IPAddress ip = new IPAddress(new byte[] { 0, 0, 0, 0 });
            Regex regex = new Regex(regexIP);
            Match match = regex.Match(text);

            match.NextMatch();
            try
            {
                ip = IPAddress.Parse(match.Value);
            }catch(Exception ex)
            {
                
            }
            return ip;
        }

        public static string GetMacAddress(string text)
        {
            if (text == null) return "";
            Regex regex = new Regex(regexMac);
            Match match = regex.Match(text);

            match.NextMatch();

            return match.Value;
        }
        #endregion

        #region LOG
        public static void Log(string message)
        {
#if DEBUG
            LocalDatabase.Log(message);
#else
            LocalDatabase.Log(message);
            if (eventLog1 != null && LocalDatabase.SaveLogToSystem) eventLog1.WriteEntry(message);
            Console.WriteLine(message);
#endif
        }
#endregion

    }
}
