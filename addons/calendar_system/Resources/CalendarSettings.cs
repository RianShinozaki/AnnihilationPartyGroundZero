using Godot;
using System;
using System.Runtime.InteropServices;



[GlobalClass]
[Tool]
public partial class CalendarSettings : Resource
{
	[Export] public DayOfWeek firstDay;

}
