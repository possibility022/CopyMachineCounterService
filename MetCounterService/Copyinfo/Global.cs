using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copyinfo
{
    static class Global
    {
        public static Database.MongoDB database;

        public static void Initialize()
        {
            Database.MongoDB.Initialize();
            database = new Database.MongoDB();
        }
    }
}
