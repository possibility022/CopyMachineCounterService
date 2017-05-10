using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copyinfo
{
    static class Global
    {
        public static void Initialize()
        {
            Database.MongoTB.Initialize();
            Database.LocalCache.FirebirdServiceCache.Initialize();
            Database.DAO.Initialize();
            Other.Email.Initialize();
        }

        public static void Log(string message)
        {

        }

        public static void OpenEmailAttachment(string file)
        {
            file = "\"" + file + "\"";
            Process.Start(StaticSettings.RedReaderPath, file);
            //ProcessStartInfo startInfo = new ProcessStartInfo();
            //startInfo.FileName = file;
            //startInfo.Arguments = StaticSettings.RedReaderPath;
            //Process.Start(startInfo);
        }

        public static void SearchThisStringInGoogleMaps(string search)
        {
            System.Diagnostics.Process.Start("http://maps.google.com/?q=" + search);
        }
    }
}
