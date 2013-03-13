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
		private String bridgeMAC;
        private String username;
        
        public Messaging(string bridgeIP, string bridgeMAC)
        {
            this.bridgeIP = bridgeIP;
            this.bridgeMAC = bridgeMAC;
        }
		
		public void SetUsername(String username)
		{
			this.username = username;
		}
		
		#region Getters for constructor config
		public String GetBridgeIP()
		{
			return this.bridgeIP;
		}
		
		public String GetBridgeMAC()
		{
			return this.bridgeMAC;
		}

		public String GetUserName()
		{
			return this.username;
		}
		#endregion

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
			List<Thread> spawned = new List<Thread>();
            foreach (Lamp lamp in lampStates)
            {
				Thread lampchange = new Thread(new ParameterizedThreadStart(SendMessage));
                spawned.Add(lampchange);
				lampchange.Start(lamp);
            }
			
			foreach (Thread rejoin in spawned)	
			{
				rejoin.Join();
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
			
			if (jsonString != null && jsonString.Length != 0) {
				JsonLampList lampList = JsonConvert.DeserializeObject<JsonLampList>(jsonString);
            	return lampList.ConvertToHueLamps();
			} else {
				return new List<Lamp>();
			}
		}
    }
}
