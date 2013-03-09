using System;
using System.Net;
using System.Collections.Generic;

namespace hueio
{
	public class Hueio
	{
		private IPAddress bridgeIP;
		private String usernameKey;
		
		private Messaging messaging;

		#region Getters for constructor config
		public IPAddress GetBridgeIP()
		{
			return this.bridgeIP;
		}

		public String GetUserKey()
		{
			return this.usernameKey;
		}
		#endregion
		
		public Hueio (IPAddress bridgeIP, String usernameKey)
		{
			this.bridgeIP = bridgeIP;
			this.usernameKey = usernameKey;
			
			this.messaging = new Messaging(this.bridgeIP.ToString(), this.usernameKey);
		}
		
		//Download the current state of lamps from the hue bridge
		public List<Lamp> GetLampList()
		{
			return messaging.DownloadLampList();
		}
		
		//Delegate void for altering lamp 
		//e.g. new LampStateChange((Lamp l) => l.state = false)
		public delegate void LampStateChange(Lamp lamp);
		
		public void ChangeAllLampStates(Delegate stateChange)
        {
			List<Lamp> lamps = messaging.DownloadLampList();
            foreach (Lamp lamp in lamps)
            {
                stateChange.DynamicInvoke(lamp);
            }
            messaging.SendMessage(lamps);
        }

        public void ChangeLampState(Lamp lamp, Delegate stateChange)
        {
            if (lamp == null)
            {
                return;
            }

            stateChange.DynamicInvoke(lamp);

            messaging.SendMessage(lamp);
        }
	}
}

