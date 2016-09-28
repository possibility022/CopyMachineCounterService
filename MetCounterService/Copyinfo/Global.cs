using System;
using System.Collections.Generic;
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
    }
}
