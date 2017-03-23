using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirebirdSql.Data;
using FirebirdSql.Data.FirebirdClient;

namespace Copyinfo.Database
{
    class Firebird
    {
        const string connectionString = "***REMOVED***Database=***REMOVED***;DataSource=***REMOVED***; Port = 3050; Dialect = 3; Charset = NONE; Role =; Connection lifetime = 15; Pooling = true; MinPoolSize = 0; MaxPoolSize = 50; Packet Size = 8192; ServerType = 0;";
        //const string connectionString = "***REMOVED***Database=D:\\data\\test.fdb;DataSource=192.168.1.9; Port = 3050; Dialect = 3; Charset = NONE; Role =; Connection lifetime = 15; Pooling = true; MinPoolSize = 0; MaxPoolSize = 50; Packet Size = 8192; ServerType = 0;";
        static FbConnection connection = new FbConnection(connectionString);

        internal static List<int> getServiceAgreementsDevices()
        {
            string sql = "SELECT ID_URZADZENIE_KLIENT FROM UMOWA_SERWISOWA_POZYCJA";

            List<int> ids = new List<int>();

            FbDataReader reader = executeCommand(sql);
            while (reader.Read())
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

        #region Device
        //String is divided for inserting WHERE between this parts if needed.
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
                "URZADZENIE_KLIENT.ID_URZADZENIE_KLIENT, " +                 //11
                "URZADZENIE_KLIENT.ID_URZADZENIE_KLIENT_STATUS, " +           //12
                "URZADZENIE_KLIENT.ID_KLIENT " +                            //13
                "FROM URZADZENIE_KLIENT ";


        const string sql_sekect_device_inner_join =
                "INNER JOIN MODEL_URZADZENIA " +
                "ON URZADZENIE_KLIENT.ID_MODEL_URZADZENIA=MODEL_URZADZENIA.ID_MODEL_URZADZENIA " +
                "INNER JOIN MARKA_URZADZENIA " +
                "ON MODEL_URZADZENIA.ID_MARKA_URZADZENIA=MARKA_URZADZENIA.ID_MARKA_URZADZENIA " +
                "INNER JOIN ADRES_KLIENT " +
                "ON URZADZENIE_KLIENT.ID_MIEJSCE_INSTALACJI=ADRES_KLIENT.ID_ADRES_KLIENT ";

        private static Device readDevice(FbDataReader reader)
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
                service_agreement = DatabaseCache.serviceAgreementDevices.Contains(reader.GetInt32(11)),
                status = reader.GetInt32(12),
                client_id = reader.GetInt32(13)
            };

            device.address = address;

            return device;
        }

        internal static List<Device> GetAllDevices()
        {
            string sql = sql_select_device + sql_sekect_device_inner_join;

            FbDataReader reader = executeCommand(sql);

            List<Device> devices = new List<Device>();

            while (reader.Read())
            {
                Device d = readDevice(reader);
                if (d.status == 1) devices.Add(d);
            }

            reader.Close();

            return devices;
        }

        internal static Device GetDevice(string serial_number)
        {
            string sql = sql_select_device + sql_sekect_device_inner_join + " WHERE URZADZENIE_KLIENT.NR_FABRYCZNY='" + serial_number + "'";

            FbDataReader reader = executeCommand(sql);
            Device device = null;

            while (reader.Read())
            {
                device = readDevice(reader);
                if (device.status == 1)
                    break;
            }

            reader.Close();
            connection.Close();

            return device;
        }

        internal static List<Device> getDevices(string[] serial_numbers)
        {

            string sql = sql_select_device + " WHERE URZADZENIE_KLIENT.NR_FABRYCZNY=" + serial_numbers[0];

            for (int i = 1; i < serial_numbers.Length; i++)
            {
                sql += " OR URZADZENIE_KLIENT.NR_FABRYCZNY=" + serial_numbers[i];
            }

            sql += " " + sql_sekect_device_inner_join;

            FbDataReader reader = executeCommand(sql);

            List<Device> devices = new List<Device>();

            while (reader.Read())
            {
                devices.Add(readDevice(reader));
            }

            reader.Close();
            connection.Close();

            return devices;
        }

        /// <summary>
        /// Ta metoda pobiera urządzenia które są przypisane do klienta. (Aktywne urządzenia)
        /// </summary>
        /// <param name="id">Id klienta</param>
        /// <returns>Lista urządzeń klienta.</returns>
        internal static List<Device> GetDevices(int id)
        {
            string sql = sql_select_device + sql_sekect_device_inner_join;
            sql += " WHERE URZADZENIE_KLIENT.ID_KLIENT=" + id.ToString();

            FbDataReader reader = executeCommand(sql);

            List<Device> devices = new List<Device>();

            while (reader.Read())
            {
                Device d = readDevice(reader);
                if (d.status == 1) devices.Add(d);
            }

            reader.Close();
            connection.Close();

            return devices;
        }
        #endregion

        #region address

        const string sql_select_address = "SELECT ADRES_KLIENT.NR_LOKALU, " + //1
                "ADRES_KLIENT.MIEJSCOWOSC, " +                               //2
                "ADRES_KLIENT.NR_DOMU, " +                                    //3
                "ADRES_KLIENT.KOD_POCZT, " +                                  //4
                "ADRES_KLIENT.POCZTA, " +                                     //5
                "ADRES_KLIENT.ULICA " +                                       //6
                "FROM ADRES_KLIENT";

        internal static Address GetAddress(int id)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region client

        const string sql_select_clients = "SELECT ID_KLIENT, " + //0
            "NIP, " +               //1
            "NAZWA_SKR, " +         //2
            "NAZWA_2, " +           //3
            "TELEFON, " +           //4
            "FAX, " +               //5
            "EMAIL, " +             //6
            "ADRES_WWW, " +         //7
            "OPIS, " +              //8
            "NR_LOKALU, " +         //9
            "MIEJSCOWOSC, " +       //10
            "NR_DOMU, " +           //11
            "KOD_POCZT, " +         //12
            "POCZTA, " +            //13
            "ULICA " +                //14
            "FROM KLIENT";

        protected static Client readClient(FbDataReader reader)
        {
            Client c = new Client
            {
                id = reader.GetInt32(0),
                ser_agr = DatabaseCache.serviceAgreementClients.Contains(reader.GetInt32(0)),
                NIP = reader.GetString(1),
                name = reader.GetString(2) + " " + reader.GetString(3),
                p_numbers = reader.GetString(4).Split(','),
                f_numbers = reader.GetString(5).Split(','),
                emails = reader.GetString(6).Split(','),
                //devices = null,
                wwwsites = reader.GetString(7).Split(','),
                notes = reader.GetString(8)
            };

            Address a = new Address
            {
                apartment = reader.GetString(9),
                city = reader.GetString(10),
                house_number = reader.GetString(11),
                postcode = reader.GetString(12),
                post_city = reader.GetString(13),
                street = reader.GetString(14)
            };

            c.GetAddress(a);

            return c;
        }

        internal static List<Client> GetAllClients()
        {
            string sql = sql_select_clients;

            FbDataReader reader = executeCommand(sql);

            List<Client> clients = new List<Client>();

            while(reader.Read())
            {
                clients.Add(readClient(reader));
            }

            return clients;
        }

        internal static Client GetClient(int id)
        {
            string sql = sql_select_clients + " WHERE ID_KLIENT=" + id.ToString();
            FbDataReader reader = executeCommand(sql);
            Client client = null;
            while (reader.Read())
            {
                client = readClient(reader);
            }

            return client;
        }

        internal static Client GetClient(string nip)
        {
            string sql = sql_select_clients + " WHERE NIP='" + nip + "'";
            FbDataReader reader = executeCommand(sql);
            Client client = null;
            while (reader.Read())
            {
                client = readClient(reader);
            }

            return client;
        }

        #endregion

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
