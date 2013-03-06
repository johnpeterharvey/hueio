using System;
using System.Text;
using System.Collections;
using System.Drawing;

namespace hueio
{
    public class Lamp
    {
        private int lampNumber;
        public String name { get; set; }
        public bool state { get; set; }
        public double hue { get; set; }
        public double saturation { get; set; }
        public double brightness { get; set; }
        private int? transitionTime = null;

        public Lamp(int lampNumber, String name, bool state, double hue, double sat, double bri)
        {
            this.lampNumber = lampNumber;
            this.name = name;
            this.state = state;
            this.hue = hue;
            this.saturation = sat;
            this.brightness = bri;
        }

        public int GetLampNumber()
        {
            return this.lampNumber;
        }

        public void SetColor(int r, int g, int b)
        {
            HSVRGB hsvrgb = new HSVRGB();
            double hue, sat, bri;

            hsvrgb.ConvertToHSL(r, g, b, out hue, out sat, out bri);

            this.hue = hue;
            this.saturation = sat;
            this.brightness = bri;
        }

        public void SetTransitionTime(int timeMilli)
        {
            this.transitionTime = (int) Math.Round(timeMilli / 100d);
        }

        public int? GetTransitionTime()
        {
            return this.transitionTime;
        }

        public String GetJson()
        {
            ArrayList commands = new ArrayList();
            commands.Add("\"on\": " + (state == true ? "true" : "false"));

            commands.Add("\"hue\":" + Math.Round(hue * UInt16.MaxValue));
            commands.Add("\"sat\":" + Math.Round(saturation * 256));
            commands.Add("\"bri\": " + Math.Round(brightness * 256));
            
            if (transitionTime != null) commands.Add("\"transitiontime\": " + transitionTime);

            return String.Concat("{", String.Join(", ", commands.ToArray()), "}");
        }

    }
}
