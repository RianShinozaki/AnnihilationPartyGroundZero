using Godot;
using System;

public partial class Pointer : Sprite2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

    public override void _Process(double delta)
    {
        base._Process(delta);
		Position = GetViewport().GetMousePosition();
    }

}
