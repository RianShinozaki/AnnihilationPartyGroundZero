using Godot;
using System;

public partial class DialogueCaller : Node
{
	public bool initialized = false;
    public override void _Ready()
    {
        base._Ready();
    }
    public override void _Process(double delta)
    {
        base._Process(delta);
		if(!initialized) {
			initialized = true;
			GameController.theDialogueCaller = this;
		}
    }
}
