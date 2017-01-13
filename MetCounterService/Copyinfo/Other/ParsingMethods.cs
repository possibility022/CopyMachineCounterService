using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copyinfo.Other
{
    class ParsingMethods
    {
        private static void RemoveFromString(ref string v, string[] what)
        {
            foreach (string w in what)
                v.Replace(v, w);
        }

        private static void RemoveFromString(ref string v, string what)
        {
            v.Replace(what, "");
        }

        public static int ParseStringToPhoneNumber(ref string value)
        {
            string[] remove = { " ", "+", "-", "." };
            RemoveFromString(ref value, remove);

            int newValue = -1;

            try
            {
                 newValue = Int32.Parse(value);
            } catch (Exception ex)
            {
                
            }

            return newValue;
        }
    }
}
