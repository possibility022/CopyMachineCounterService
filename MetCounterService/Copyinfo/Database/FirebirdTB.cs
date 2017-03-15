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
        const string connectionString = "***REMOVED***Database=***REMOVED***;DataSource=***REMOVED***; Port = 3050; Dialect = 3; Charset = NONE; Role =; Connection lifetime = 15; Pooling = true; MinPoolSize = 0; MaxPoolSize = 50; Packet Size = 8192; ServerType = 0;";
        static FbConnection connection = new FbConnection(connectionString);

        const string sql_select_device = "SELECT URZADZENIE_KLIENT.NR_FABRYCZNY, " +        //0
                "MODEL_URZADZENIA.NAZWA_1, " +                              //1
                "MARKA_URZADZENIA.NAZWA_1, " +                              //2
                "URZADZENIE_KLIENT.DATA_INSTALACJI, " +                     //3
                "URZADZENIE_KLIENT.ID_MIEJSCE_INSTALACJI, " +                //4
                "ADRES_KLIENT.NR_LOKALU, " +                                 //5
                "ADRES_KLIENT.MIEJSCOWOSC, " +                               //6
                "ADRES_KLIENT.NR_DOMU, " +                                    //7
                "ADRES_KLIENT.KOD_POCZT, " +                                  //8
                "ADRES_KLIENT.POCZTA, " +                                     //9
                "ADRES_KLIENT.ULICA, " +                                     //10
                "URZADZENIE_KLIENT.ID_URZADZENIE_KLIENT " +                 //11
                "FROM URZADZENIE_KLIENT " +
                "INNER JOIN MODEL_URZADZENIA " +
                "ON URZADZENIE_KLIENT.ID_MODEL_URZADZENIA=MODEL_URZADZENIA.ID_MODEL_URZADZENIA " +
                "INNER JOIN MARKA_URZADZENIA " +
                "ON MODEL_URZADZENIA.ID_MARKA_URZADZENIA=MARKA_URZADZENIA.ID_MARKA_URZADZENIA " +
                "INNER JOIN ADRES_KLIENT " +
                "ON URZADZENIE_KLIENT.ID_MIEJSCE_INSTALACJI=ADRES_KLIENT.ID_ADRES_KLIENT ";

        internal static List<int> getServiceAgreementsDevices()
        {
            string sql = "SELECT ID_URZADZENIE_KLIENT FROM UMOWA_SERWISOWA_POZYCJA";

            List<int> ids = new List<int>();

            FbDataReader reader = executeCommand(sql);
            while(reader.Read())
            {
                ids.Add(reader.GetInt32(0));
            }

            return ids;
        }

        internal static List<int> getServiceAgreementsClients()
        {
            string sql = "SELECT ID_KLIENT FROM UMOWA_SERWISOWA";

            List<int> ids = new List<int>();

            FbDataReader reader = executeCommand(sql);
            while (reader.Read())
            {
                ids.Add(reader.GetInt32(0));
            }

            return ids;
        }

        private static FbDataReader executeCommand(string command)
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }

            FbCommand com = new FbCommand(command, connection);
            FbDataReader dr = com.ExecuteReader();

            return dr;
        }

        internal static List<Device> GetAllDevices()
        {
            string sql = sql_select_device;

                

            FbDataReader reader = executeCommand(sql);

            List<Device> devices = new List<Device>();

            while(reader.Read())
            {
                Address address = new Address();

                address.id = reader.GetInt32(4);
                address.apartment = reader.GetString(5);
                address.city = reader.GetString(6);
                address.house_number = reader.GetString(7);
                address.postcode = reader.GetString(8);
                address.post_city = reader.GetString(9);
                address.street = reader.GetString(10);

                Device device = new Device
                {
                    serial_number = reader.GetString(0),
                    model = reader.GetString(1),
                    provider = reader.GetString(2),
                    instalation_datetime = reader.GetDateTime(3),
                    service_agreement = DatabaseCache.serviceAgreementDevices.Contains(reader.GetInt32(11))
                };

                device.setAddress(address);

                devices.Add(device);
            }

            reader.Close();

            return devices;
        }

        internal static Device getDevice(string serial_number)
        {
            throw new NotImplementedException();
        }

        //internal static List<Device> getDevices()
        //{
        //    string sql = "SELECT URZADZENIE_KLIENT.NR_FABRYCZNY, MODEL_URZADZENIA.NAZWA_1, MARKA_URZADZENIA.NAZWA_1, URZADZENIE_KLIENT.DATA_INSTALACJI, URZADZENIE_KLIENT.ID_MIEJSCE_INSTALACJI " + //, MARKA_URZADZENIA.NAZWA_1 
        //        "FROM URZADZENIE_KLIENT " +
        //        "INNER JOIN MODEL_URZADZENIA " +
        //        "ON URZADZENIE_KLIENT.ID_MODEL_URZADZENIA=MODEL_URZADZENIA.ID_MODEL_URZADZENIA " +
        //        "INNER JOIN MARKA_URZADZENIA " +
        //        "ON MODEL_URZADZENIA.ID_MARKA_URZADZENIA=MARKA_URZADZENIA.ID_MARKA_URZADZENIA";

        //    FbCommand com = new FbCommand(sql, connection);
        //    FbDataReader dr = com.ExecuteReader();

        //    List<Device> devices = new List<Device>();

        //    while (dr.Read())
        //    {
        //        devices.Add(new Device
        //        {
        //            serial_number = dr.GetString(0),
        //            model = dr.GetString(1),
        //            provider = dr.GetString(2),
        //            instalation_datetime = dr.GetDateTime(3),
        //            instalation_address = dr.GetInt32(4)
        //        });
        //    }
        //    dr.Close();
        //    connection.Close();

        //    return devices;
        //}

        internal static List<Device> getDevices(string[] serial_numbers)
        {
            throw new NotImplementedException();
            string sql = sql_select_device + "WHERE URZADZENIE_KLIENT.NR_FABRYCZNY="+serial_numbers[0];

            

            for(int i = 1; i < serial_numbers.Length; i++)
            {
                sql += " OR URZADZENIE_KLIENT.NR_FABRYCZNY=" + serial_numbers[i];
            }
        }

        internal static Address getAddress(int id)
        {
            throw new NotImplementedException();
        }

        internal static Client getClient(int id)
        {
            throw new NotImplementedException();
        }

        #region test

        public static List<Device> test()
        {
            throw new NotImplementedException();
            //string connectionString = "***REMOVED***Database=C:\\Tom\\Firebird_Database\\as.GDK;DataSource=127.0.0.1; Port = 3050; Dialect = 3; Charset = NONE; Role =; Connection lifetime = 15; Pooling = true; MinPoolSize = 0; MaxPoolSize = 50; Packet Size = 8192; ServerType = 0;";

            //List<Device> dev = getDevices();


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

        #endregion
    }
}
