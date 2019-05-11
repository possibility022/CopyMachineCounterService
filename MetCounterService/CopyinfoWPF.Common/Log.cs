using NLog;
using NLog.Config;
using NLog.Targets;
using System;
using System.IO;


namespace CopyinfoWPF.Common
{
    public static class Log
    {

        private const string Layout = "${longdate}|${level:uppercase=true}|Thread: [${threadid}] | Message: \t${message}\t${exception:format=tostring,StackTrace,Data}";

        private const string DateTimeFileFormat = "dd-M-yyyy_HH-mm-ss";

        private const string LogsFolder = "Logs";

        public static Logger Logger { get; private set; }

        public static string FilePath { get; private set; }

        public static void ConfigureNLog()
        {
            var config = new LoggingConfiguration();

            FilePath = GenerateFileName();
            PrepareFolder(FilePath);

            var logFile = new FileTarget() { FileName = FilePath, Layout = Layout };
            

            config.AddRule(LogLevel.Info, LogLevel.Fatal, logFile);

            LogManager.Configuration = config;
            Logger = LogManager.GetCurrentClassLogger();
        }

        private static void PrepareFolder(string path)
        {
            path = Path.GetDirectoryName(path);
            if (Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
            }
        }

        private static string GenerateFileName(string namePrefix = "CopyInfoWPF")
        {
            var fileNameBase = DateTime.Now.ToString(DateTimeFileFormat);
            var fileName = $"{namePrefix}{fileNameBase}.log";
            fileName = Path.Combine(LogsFolder, fileName);

            for (int i = 0; i < 100; i++)
            {
                if (!System.IO.File.Exists(fileName))
                {
                    break;
                }
                else
                {
                    fileName = Path.Combine(LogsFolder, $"{namePrefix}{fileNameBase}_{i}.log");
                }
            }

            if (System.IO.File.Exists(fileName))
                throw new Exception();

            return fileName;
        }

        public static void LogProductInfo(string message)
        {
            Logger.Trace(message);
        }

        public static void Info(string message)
        {
            Logger.Info(message);
        }

        public static void Error(Exception ex, string message)
        {
            Logger.Error(ex, message);
        }

        public static void Error(string message)
        {
            Logger.Error(message);
        }

        public static void Error(Exception ex)
        {
            Logger.Error(ex);
        }
    }
}
