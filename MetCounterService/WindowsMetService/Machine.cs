using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;

namespace WindowsMetService
{
    [Serializable]
    class Machine
    {
        public string counterData;
        public string serialNumberData;

        public string url_serialNumber;
        public string url_counterData;

        public string mac;
        public IPAddress ip;

        public DateTime datetime;


        static WebClient client;

        public Machine(IPAddress ip)
        {
            this.ip = ip;
            if (SetUpMachine() == false)
            {
                Global.Log("Pobranie informacji nie powiodło się. Ip urządzenia: " + ip);
                counterData = "#wrongdevice#";
                serialNumberData = "#wrongdevice#";
            }
            datetime = DateTime.Now;
        }

        private bool SetUpMachine()
        {
            if (Global.GetMacAddress(this.mac) == "")
            {
                this.mac = Network.Finder.GetMacAddress(ip);
            }

            string[] links = LocalDatabase.GetMacWebMapping(this.mac);
            this.url_serialNumber = links[0];
            this.url_counterData = links[1];

            if ((links[0] == "") && (links[1] == ""))
                return false;

            serialNumberData = DownloadString(url_serialNumber);
            counterData = DownloadString(url_counterData);

            return true;
        }

        private string DownloadString(string url)
        {
            client = new WebClient();
            client.Encoding = Encoding.UTF8;

            string fullURL = "http://" + ip + url;
            try
            {
                return client.DownloadString(fullURL);
            }catch (Exception ex)
            {
                Global.Log("Nie udalo sie pobrac zawartosci. FullURL: " + fullURL + " Ex.Message: " + ex.Message);
                return "#null#";
            }
        }
    }
}
