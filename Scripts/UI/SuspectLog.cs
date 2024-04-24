using Godot;
using Microsoft.VisualBasic;
using System;

public partial class SuspectLog : Control
{
	[Export] public DateInformation[] logs = new DateInformation[31];


	[Export] public Label EngineerButton;
	[Export] public Label TeacherButton;
	[Export] public Label ButcherButton;
	[Export] public Label OccultistButton;
	[Export] public Label Date;
	[Export] public string logFolder;
	public int currentLogDay;
	public static SuspectLog Instance;

	public Godot.Collections.Array days = new Godot.Collections.Array{
		"Friday",
		"Saturday",
		"Sunday",
		"Monday",
		"Tuesday",
		"Wednesday",
		"Thursday"
		};
    public override void _Ready()
    {
        base._Ready();
		Instance = this;
		GameController.Instance.SwitchScene += _onSwitchScene;

		for(int i = 0; i < 31; i++) {
			logs[i] = GD.Load<DateInformation>(logFolder + "/Day" +(i+1).ToString() + ".tres");
		}
    }
    public void Init() {
		//currentLogDay = 1;
		currentLogDay = Mathf.Max(1, GameController.currentDay-1);
		OnDayChanged();

		Rotation = Mathf.Pi * 2/3;
		Tween tween = GetTree().CreateTween();
		tween.TweenProperty(this, "rotation", 0, 0.5f).SetTrans(Tween.TransitionType.Sine);
		tween.Finished += switchToSuspectLog;
		Visible = true;

		
	}
	public void OnDayChanged() {
		if(currentLogDay > GameController.currentDay-1 && GameController.currentDay > 1) {
			EngineerButton.Text = "";
			TeacherButton.Text = "";
			ButcherButton.Text = "";
			OccultistButton.Text = "";
		} else if (GameController.currentDay > 1){
			EngineerButton.Text = logs[currentLogDay-1].EngineerLog;
			TeacherButton.Text = logs[currentLogDay-1].TeacherLog;
			ButcherButton.Text = logs[currentLogDay-1].ButcherLog;
			OccultistButton.Text = logs[currentLogDay-1].OccultistLog;
		} else {
			EngineerButton.Text = "No reports have come in yet. Check tomorrow.";
			TeacherButton.Text = "";
			ButcherButton.Text = "";
			OccultistButton.Text = "";
		}
		String thisDayName = (String)days[(currentLogDay-1)%7];
		Date.Text = thisDayName + ", December " + currentLogDay.ToString("D2") + ", 50XX";

		if(currentLogDay == 2 && GameController.currentDay > 2) {
			GameController.engineerMemory[0] = 1;
		}
		if(currentLogDay == 15 || currentLogDay == 16 || currentLogDay == 17) {
			if(GameController.engineerMemory[3] == 0) {
				GameController.engineerMemory[3] = 1;
				GameController.engineerQuestionFlags[4] = true;
			}

		}
		if(currentLogDay == 22) {
			if(GameController.teacherMemory[5] == 0) {
				GameController.teacherMemory[5] = 1;
				GameController.teacherQuestionFlags[4] = true;
			}

		}
		if(currentLogDay == 12) {
			if(GameController.butcherMemory[5] == 0) {
				GameController.butcherMemory[5] = 1;
				GameController.butcherQuestionFlags[4] = true;
			}

		}
		if(currentLogDay == 19 || currentLogDay == 26) {
			if(GameController.occultistMemory[5] == 0) {
				GameController.occultistMemory[5] = 1;
				GameController.occultistQuestionFlags[5] = true;
			}

		}
	}

	private void _on_next_day_pressed() {
		currentLogDay = Mathf.Clamp(currentLogDay + 1, 1, Mathf.Max(1, GameController.currentDay-1));
		OnDayChanged();
	}
	private void _on_previous_day_pressed() {
		currentLogDay = Mathf.Clamp(currentLogDay - 1, 1, Mathf.Max(1, GameController.currentDay-1));
		OnDayChanged();
	}
	private void _on_close_pressed() {
		GameController.currentState = GameController.GameState.Transitioning;
		Tween tween = GetTree().CreateTween();
		tween.TweenProperty(this, "rotation",  Mathf.Pi * 2/3, 0.5f).SetTrans(Tween.TransitionType.Sine);
		tween.Finished += switchToOffice;

	}
	private void _onSwitchScene() {
		Tween tween = GetTree().CreateTween();
		tween.TweenProperty(this, "rotation",  Mathf.Pi * 2/3, 0.5f).SetTrans(Tween.TransitionType.Sine);
	}
	private void switchToSuspectLog() {
		GameController.currentState = GameController.GameState.SuspectLog;
	}
	private void switchToOffice() {
		GameController.currentState = GameController.GameState.Office;
		Visible = false;
	}

}
