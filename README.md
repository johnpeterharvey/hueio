# hueio
=====

A C# DLL for communicating and altering the state of (sets of) Philips Hue LED bulbs

Information needed to use:
- IP Address of your Hue bridge
- A user key valid for connecting (see http://rsmck.co.uk/hue)

---

Import into your project

Instantiate an instance of the class, telling it where your bridge is, and the user key to connect with
```csharp
Hueio hueio = new Hueio(IPAddress.Parse("192.168.0.50"), "f60234ee15619cad163eb5ceb688");
```

Getting the set of connected lamps
```csharp
List<Lamp> lamps = hueio.GetLampList();
```

Changing one lamp state involves calling ChangeLampState
```csharp
hueio.ChangeLampState(lamp, new Hueio.LampStateChange((Lamp l) => l.brightness = 100));
```

To alter all lamp states simultaneously, call ChangeAllLampStates on the instance
```csharp
hueio.ChangeAllLampStates(new Hueio.LampStateChange((Lamp l) => l.state = true));
```
---

Still being worked on, improving over time. If you've got any suggestions, let me know.
