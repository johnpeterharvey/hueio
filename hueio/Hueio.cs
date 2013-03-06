using System;
using System.Net;
using System.Collections.Generic;

namespace hueio
{
	public class Hueio
	{
		private IPAddress bridgeIP;
		private String usernameKey;

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
		}

		public List<Lamp> 


	}
}

