using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copyinfo.Database
{
    class Delegates
    {
        public delegate bool AddStringToDatabase_Client (string value, string _id);
        public delegate bool AddIntToDatabase_Client(int value, string _id);

        public AddStringToDatabase_Client add_email = null;
        public AddStringToDatabase_Client add_www = null;
        public AddIntToDatabase_Client add_phone = null;
        public AddIntToDatabase_Client add_fax = null;
        
    }
}
