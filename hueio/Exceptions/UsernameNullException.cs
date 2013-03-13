using System;

namespace hueio
{
	public class UsernameNullException : ApplicationException
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

