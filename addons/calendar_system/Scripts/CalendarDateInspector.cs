using Godot;
using System;
using System.Globalization;
using System.Runtime.InteropServices;

[Tool]
public partial class CalendarDateInspector : Panel
{
	DateInformation currentDateInformation;
	public override void _Ready()
	{
		CalendarController.Instance.ChangedFocusedDate += OnChangedFocusedDate;
	}

	private void OnChangedFocusedDate(Date newDate) {
		GetNode<Label>("ScrollContainer/VBoxContainer/Label").Text = "Selected Day: " + newDate.day.ToString();
		currentDateInformation = newDate.myInformation;

		for(int i = 0; i < (int)Suspects.COUNT; i++) {
			string path = "ScrollContainer/VBoxContainer/" + ((Suspects)i).ToString() + "Log/EntryText";
			GD.Print("Populated field for " + path);
			GD.Print(currentDateInformation.logs[i]);
			GetNode<TextEdit>(path).Text = currentDateInformation.logs[i];
		}
	}

	private void _on_engineer_text_changed() {
		string path = "ScrollContainer/VBoxContainer/EngineerLog/EntryText";
		currentDateInformation.logs[(int)Suspects.Engineer] = GetNode<TextEdit>(path).Text;
	}
	private void _on_occultist_text_changed() {
		string path = "ScrollContainer/VBoxContainer/OccultistLog/EntryText";
		currentDateInformation.logs[(int)Suspects.Occultist] = GetNode<TextEdit>(path).Text;
	}
	private void _on_teacher_text_changed() {
		string path = "ScrollContainer/VBoxContainer/TeacherLog/EntryText";
		currentDateInformation.logs[(int)Suspects.Teacher] = GetNode<TextEdit>(path).Text;
	}
	private void _on_butcher_text_changed() {
		string path = "ScrollContainer/VBoxContainer/ButcherLog/EntryText";
		currentDateInformation.logs[(int)Suspects.Butcher] = GetNode<TextEdit>(path).Text;
	}
}
