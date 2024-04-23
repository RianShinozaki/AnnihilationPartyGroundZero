using Godot;
using System;

public partial class DayChange : Button
{
	[Export] int val;
	private void _on_pressed() {
		GD.Print("move up a day");
		GameController.currentDay += val;
		if(GameController.currentDay > 30) {
			GameController.currentDay = 30;
		}
		GameController.Instance.OnSwitchScene();
	}
}
