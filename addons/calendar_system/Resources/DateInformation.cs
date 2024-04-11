using Godot;
using System;

[GlobalClass][Tool]
public partial class DateInformation : Resource
{
	[Export] public int day;
	[Export(PropertyHint.MultilineText)]public string EngineerLog;
	[Export(PropertyHint.MultilineText)] public string TeacherLog;
	[Export(PropertyHint.MultilineText)] public string ButcherLog;
	[Export(PropertyHint.MultilineText)] public string OccultistLog;

	[Export(PropertyHint.Flags, "Engineer,Teacher,Butcher,Occultist")]
public int Available { get; set; } = 0;

	[Export(PropertyHint.MultilineText)] public string ExecuteMethods;
}
