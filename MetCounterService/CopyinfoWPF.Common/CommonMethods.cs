using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyinfoWPF.Common
{
    public class CommonMethods
    {
        public static void SearchThisStringInGoogleMaps(string search)
        {
            System.Diagnostics.Process.Start("http://maps.google.com/?q=" + search);
        }
    }
}
