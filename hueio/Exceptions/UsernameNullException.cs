using System;

namespace hueio
{
	public class UsernameNullException
	{
		public UsernameNullException ()
		{
		}
		
		public override string ToString ()
		{
			return string.Format ("[UsernameNullException] - username not set or set to null");
		}
	}
}

