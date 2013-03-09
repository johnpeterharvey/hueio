using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hueio
{
    public class JsonLampList
    {
        public Dictionary<int, JsonLamp> lights { get; set; }

        public List<Lamp> ConvertToHueLamps()
        {
            List<Lamp> lampSet = new List<Lamp>();
            foreach (int i in lights.Keys)
            {
                lampSet.Add(ConvertToHueLamp(i));
            }

            return lampSet;
        }

        public Lamp ConvertToHueLamp(int lampNumber)
        {
            JsonLamp jsonLamp = null;
            lights.TryGetValue(lampNumber, out jsonLamp);

            if (jsonLamp != null)
            {
                Lamp lamp = new Lamp(lampNumber, jsonLamp.name, jsonLamp.state.on, jsonLamp.state.GetHueAsDegree(), jsonLamp.state.GetSaturation(), jsonLamp.state.GetBrightness());
                return lamp;
            }

            return null;
        }
    }
}
