using System;
using System.Net;
using Newtonsoft.Json;

namespace hueio
{
	public static class UPnPConfigFetcher
	{
		public static UPnPConfig GetBridgeInfo()
		{
			WebClient webClient = new WebClient();
            webClient.BaseAddress = "http://www.meethue.com/api/nupnp";
            String json = webClient.DownloadString(webClient.BaseAddress);
			
			if (json == null || json.Length == 0)
			{
				return null;
			}
			
			return JsonConvert.DeserializeObject<UPnPConfig>(json);
		}
	}
}

