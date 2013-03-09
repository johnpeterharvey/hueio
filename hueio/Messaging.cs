using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Threading;
using Newtonsoft.Json;

namespace hueio
{
    class Messaging
    {
        private String bridgeIP;
        private String username;
        
        public Messaging(string bridgeIP, string username)
        {
            this.bridgeIP = bridgeIP;
            this.username = username;
        }

        public void SendMessage(Lamp lampState)
        {
            WebClient webClient = new WebClient();
            webClient.BaseAddress = "http://" + bridgeIP + "/api/" + username + "/lights/" + lampState.GetLampNumber() + "/state";

            Stream writeData = webClient.OpenWrite(webClient.BaseAddress, "PUT");
            writeData.Write(Encoding.ASCII.GetBytes(lampState.GetJson()), 0, lampState.GetJson().Length);
            writeData.Close();
        }

        private void SendMessage(object lampState)
        {
            Lamp lamp = (Lamp)lampState;
            SendMessage(lamp);
        }

        public void SendMessage(List<Lamp> lampStates)
        {
            foreach (Lamp lamp in lampStates)
            {
                new Thread(new ParameterizedThreadStart(SendMessage)).Start(lamp);
            }
        }

        public String DownloadState()
        {
            WebClient webClient = new WebClient();
            webClient.BaseAddress = "http://" + bridgeIP + "/api/" + username + "/";
            return webClient.DownloadString(webClient.BaseAddress);
        }
		
		public List<Lamp> DownloadLampList()
		{
			String jsonString = DownloadState();
			JsonLampList lampList = JsonConvert.DeserializeObject<JsonLampList>(jsonString);
            return lampList.ConvertToHueLamps();
		}
    }
}
