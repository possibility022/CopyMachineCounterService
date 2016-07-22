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
        public string ip;

        public DateTime datetime;


        static WebClient client;

        public Machine(string ip)
        {
            this.ip = ip;
            if (setUpMachine() == false)
            {
                Global.Log("setupmachine in constructor return false");
                counterData = "#null#";
                serialNumberData = "#null#";
            }
        }

        private bool setUpMachine()
        {
            IPAddress addres;
            if (Global.getMacAddress(this.mac) == "")
            {
                if (IPAddress.TryParse(this.ip, out addres))
                {
                    this.mac = Network.Finder.getMacAddress(addres);
                }
                else return false;
            }

            string[] links = LocalDatabase.getMacWebMapping(this.mac);
            this.url_serialNumber = links[0];
            this.url_counterData = links[1];



            if ((links[0] == "") && (links[1] == ""))
                return false;

            serialNumberData = downloadString(url_serialNumber);
            counterData = downloadString(url_counterData);

            return true;
        }

        private string downloadString(string url)
        {
            client = new WebClient();

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
