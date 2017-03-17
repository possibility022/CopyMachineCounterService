using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copyinfo.Other
{
    class ArrayOperations
    {
        public static string[] AddToArray(string[] array, ref string value)
        {
            List<string> buffor = array.ToList();
            buffor.Add(value);
            return buffor.ToArray();
        }
    }
}
