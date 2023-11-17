using System.Diagnostics;
using Fantasy;


var process = Process.GetProcessesByName("ff7_en").FirstOrDefault() ??
              throw new ApplicationException("can't find the ff7 process!");

using var mem = new MemoryAccessor(process.Id);

// Cloud starts here
var cloudBaseAddress = new IntPtr(0xDBFD8D);

// Change the level
var cloudLevelAddress = IntPtr.Add(cloudBaseAddress, 0);
byte level = 69;
var levelBuffer = new byte[1] { level };
Console.WriteLine(mem.Write(cloudLevelAddress, levelBuffer, out _) ? "Level updated?" : "mem not written");

// Change the strength
var cloudStrengthAddress = IntPtr.Add(cloudBaseAddress, 1);
byte strength = 199;
var strengthBuffer = new byte[1] { strength };
Console.WriteLine(mem.Write(cloudStrengthAddress, strengthBuffer, out _) ? "Strength updated?" : "mem not written");

// Change the name
var cloudNameAddress = IntPtr.Add(cloudBaseAddress, 15);
var funnyName = new byte[10] { 48, 37, 46, 41, 51, 255, 255, 255, 255, 255 };
var cloudName = new byte[10] { 35, 76, 79, 85, 68, 255, 255, 255, 255, 255 };
Console.WriteLine(mem.Write(cloudNameAddress, funnyName, out _) ? "Name Written?" : "mem not written");