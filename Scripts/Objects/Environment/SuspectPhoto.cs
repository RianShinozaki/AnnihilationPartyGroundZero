using Godot;
using System;

public partial class SuspectPhoto : Clickable
{
	[Export] public GameController.Location myLocation;
	[Export] public string goToScene;
	bool clicked;
    BoardController myParent;
    TextureRect image;

    public override void _Ready()
    {
        base._Ready();
        myParent = GetParent<BoardController>();
    }
    public override void _Process(double delta)
    {
        base._Process(delta);
    }

    public override void CheckActive()
    {
        if(myLocation == GameController.Location.Office) {
            if(GameController.currentState != GameController.GameState.SuspectLocation || !GameController.canReturnButtonAppear) {
                Visible = false;
                active = false;
            } else {
                Visible = true;
                active = true;
            }
        } else {
            if(GameController.currentState != GameController.GameState.Office) {
                Visible = false;
                active = false;
            } else {
                Visible = true;
                active = true;
            }
        }
        if(GameController.currentTime == 2) {
            Visible = false;
            active = false;
        }
        if(Phone.Instance.isRinging) {
            Visible = false;
            active = false;
        }
    }

    public override void OnClick()
    {
        base.OnClick();
        //Office to a suspect location
        clicked = true;
        if(myLocation != GameController.Location.Office) {
            myParent.MoveFromOffice(myLocation, goToScene);
        }
        else {
            GameController.currentLocation = myLocation;
            GameController.Instance.OnSwitchSceneTransitionBegin(goToScene);
            GameController.canReturnButtonAppear =false;
        }
    }
	public void OnSwitchScene() {
		if(clicked) {

		}
	}
}
