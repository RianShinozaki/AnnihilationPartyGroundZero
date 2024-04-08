using Godot;
using System;

public enum DayOfWeek {
	Sunday,
	Monday,
	Tuesday,
	Wednesday,
	Thursday,
	Friday,
	Saturday
}

[GlobalClass]
public partial class CalendarSettings : Resource
{
	public DayOfWeek firstDay;

	public CalendarSettings(DayOfWeek theFirstDay) {
		firstDay = theFirstDay;
	}

}
