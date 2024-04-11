#if TOOLS
using Godot;
using System;


[Tool]
public partial class PopulateDates : GridContainer
{
	
	PackedScene date = ResourceLoader.Load<PackedScene>("res://addons/calendar_system/Scenes/Date.tscn");
	PackedScene dummyDate = ResourceLoader.Load<PackedScene>("res://addons/calendar_system/Scenes/DummyDate.tscn");
    Control dockedScene;
	[Export] DayOfWeek firstDay;

	public override void _Ready()
	{
		CalendarController.Instance.CalendarLoaded += onCalendarLoaded;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void onCalendarLoaded() {
		Godot.Collections.Array<Node> children = GetChildren();
		foreach(Node child in children) {
			child.QueueFree();
		}
		
		GD.Print("Loading dates into calendar");
		CalendarSettings settings = (CalendarSettings)CalendarController.Instance.myCalendarSettings;
		firstDay = settings.firstDay; 
		
		for(int i = 0; i < (int)firstDay; i++) {
			Node newDummy = dummyDate.Instantiate();
			AddChild(newDummy);
		}
		for(int i = 0; i < settings.days; i++) {
			Date newDate = date.Instantiate<Date>();
			newDate.GetNode<Label>("Panel/Day").Text = (i+1).ToString();
			newDate.day = i+1;
			AddChild(newDate);
		}
	}
}
#endif