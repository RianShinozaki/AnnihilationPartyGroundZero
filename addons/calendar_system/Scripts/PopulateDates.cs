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
		for(int i = 0; i < (int)firstDay; i++) {
			Node newDummy = dummyDate.Instantiate();
			AddChild(newDummy);
		}
		for(int i = 0; i < 31; i++) {
			Node newDate = date.Instantiate();
			newDate.GetNode<Label>("Panel/Day").Text = (i+1).ToString();
			AddChild(newDate);
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
#endif