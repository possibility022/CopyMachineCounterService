using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirebirdSql.Data;
using FirebirdSql.Data.FirebirdClient;

namespace Copyinfo.Database
{
    class FirebirdTB
    {

        static string connectionString = "***REMOVED***Database=***REMOVED***;DataSource=***REMOVED***; Port = 3050; Dialect = 3; Charset = NONE; Role =; Connection lifetime = 15; Pooling = true; MinPoolSize = 0; MaxPoolSize = 50; Packet Size = 8192; ServerType = 0;";
        static FbConnection connection = new FbConnection(connectionString);

        public static void Initialize()
        {
            
        }


        public static List<Device> test()
        {
            //string connectionString = "***REMOVED***Database=C:\\Tom\\Firebird_Database\\as.GDK;DataSource=127.0.0.1; Port = 3050; Dialect = 3; Charset = NONE; Role =; Connection lifetime = 15; Pooling = true; MinPoolSize = 0; MaxPoolSize = 50; Packet Size = 8192; ServerType = 0;";

            List<Device> dev = getDevices();


            FirebirdSql.Data.FirebirdClient.FbConnection connection = new FirebirdSql.Data.FirebirdClient.FbConnection(connectionString);
            
            //connection.Open();

            //System.Data.DataTable table = connection.GetSchema();
            FbConnection conn = new FbConnection(connectionString);
            conn.Open();
            String sql = "SELECT * FROM URZADZENIE_KLIENT";
            FbCommand com = new FbCommand(sql, conn);
            FbDataReader dr = com.ExecuteReader();

            List<string> data = new List<string>();

            while (dr.Read())
            {
                data.Add(dr.GetString(7));
            }
            dr.Close();


            sql = "SELECT URZADZENIE_KLIENT.NR_FABRYCZNY, MODEL_URZADZENIA.NAZWA_1, MARKA_URZADZENIA.NAZWA_1 " + //, MARKA_URZADZENIA.NAZWA_1 
                "FROM URZADZENIE_KLIENT " +
                "INNER JOIN MODEL_URZADZENIA " +
                "ON URZADZENIE_KLIENT.ID_MODEL_URZADZENIA=MODEL_URZADZENIA.ID_MODEL_URZADZENIA " +
                "INNER JOIN MARKA_URZADZENIA " +
                "ON MODEL_URZADZENIA.ID_MARKA_URZADZENIA=MARKA_URZADZENIA.ID_MARKA_URZADZENIA";

            com = new FbCommand(sql, conn);
            dr = com.ExecuteReader();

            data = new List<string>();
            List<Device> devices = new List<Device>();

            while (dr.Read())
            {
                devices.Add(new Device
                {
                    serial_number = dr.GetString(0),
                    model = dr.GetString(1),
                    provider = dr.GetString(2)
                });
            }
            dr.Close();
            conn.Close();

            return devices;
        }

        internal static Device getDevice(string serial_number)
        {
            throw new NotImplementedException();
        }

        internal static List<Device> getDevices()
        {
            string sql = "SELECT URZADZENIE_KLIENT.NR_FABRYCZNY, MODEL_URZADZENIA.NAZWA_1, MARKA_URZADZENIA.NAZWA_1, URZADZENIE_KLIENT.DATA_INSTALACJI, URZADZENIE_KLIENT.ID_MIEJSCE_INSTALACJI " + //, MARKA_URZADZENIA.NAZWA_1 
                "FROM URZADZENIE_KLIENT " +
                "INNER JOIN MODEL_URZADZENIA " +
                "ON URZADZENIE_KLIENT.ID_MODEL_URZADZENIA=MODEL_URZADZENIA.ID_MODEL_URZADZENIA " +
                "INNER JOIN MARKA_URZADZENIA " +
                "ON MODEL_URZADZENIA.ID_MARKA_URZADZENIA=MARKA_URZADZENIA.ID_MARKA_URZADZENIA";

            FbCommand com = new FbCommand(sql, connection);
            FbDataReader dr = com.ExecuteReader();

            List<Device> devices = new List<Device>();

            while (dr.Read())
            {
                devices.Add(new Device
                {
                    serial_number = dr.GetString(0),
                    model = dr.GetString(1),
                    provider = dr.GetString(2),
                    instalation_datetime = dr.GetDateTime(3),
                    instalation_address = dr.GetInt32(4)
                });
            }
            dr.Close();
            connection.Close();

            return devices;
        }

        internal static Address getAddress(int id)
        {
            throw new NotImplementedException();
        }
    }
}
