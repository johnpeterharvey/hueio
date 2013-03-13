using System;
using System.Net;
using System.Collections.Generic;

namespace hueio
{
	public class Hueio
	{
		private Messaging messaging;

		#region Getters for constructor config
		public String GetBridgeIP()
		{
			return messaging.GetBridgeIP();
		}
		
		public String GetBridgeMAC()
		{
			return messaging.GetBridgeMAC();
		}

		public String GetUserName()
		{
			return messaging.GetUserName();
		}
		#endregion
		
		#region Constructors
		//Detect IP from hue page
		public Hueio()
		{
			AttemptAutoConfig();
		}
		
		//Manual IP configuration
		public Hueio(IPAddress bridgeIP)
		{
			if (bridgeIP != null)
			{
				InitializeMessaging(bridgeIP.ToString(), null);
			} else {
				//If the user gave a null IP, try autodetection
				AttemptAutoConfig();
			}
		}
		
		private void AttemptAutoConfig()
		{
			//Request the bridge IP and other details from the hue upnp interface
			UPnPConfig bridgeConfig = UPnPConfigFetcher.GetBridgeInfo();
			
			if (bridgeConfig != null)
			{
				InitializeMessaging(bridgeConfig.internalipaddress, bridgeConfig.macaddress);
			} else {
				throw new BridgeDetectionFailedException();
			}
		}
		
		private void InitializeMessaging(String bridgeIP, String bridgeMAC)
		{
			messaging = new Messaging(bridgeIP, bridgeMAC);
		}
		#endregion
		
		//Register a new user with the bridge
		//Requires the Link button to be pressed before calling
		public void AddUserToBridge(String username)
		{
			//TODO
		}
		
		//Set the username to authenticate with
		public void SetUsername(String username)
		{
			messaging.SetUsername(username);
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

