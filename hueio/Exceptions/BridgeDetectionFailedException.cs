using System;

namespace hueio
{
	public class BridgeDetectionFailedException
	{
		public BridgeDetectionFailedException ()
		{
		}
		
		public override string ToString ()
		{
			return string.Format("[BridgeDetectionFailedException] - Unable to autodetect bridge IP");
		}
	}
}

