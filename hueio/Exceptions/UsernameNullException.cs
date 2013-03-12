using System;

namespace hueio
{
	public class UsernameFailedException
	{
		public UsernameFailedException ()
		{
		}
		
		public override string ToString ()
		{
			return string.Format ("[UsernameFailedException] - username given was null or not set");
		}
	}
}

