using System;
using hueio;

namespace test
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Hueio hue = new Hueio();

			hue.SetUsername("f607842d6d3a0b305105e192b5ceb688");

			foreach (Lamp l in hue.GetLampList())
			{
				Console.WriteLine(l.name + " - " + l.brightness);
			}

			hue.ChangeAllLampStates(new Hueio.LampStateChange((Lamp l) => l.state = false));
		}
	}
}
