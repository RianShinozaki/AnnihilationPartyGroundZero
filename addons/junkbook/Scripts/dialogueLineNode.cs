using Godot;
using System;

[Tool]
public partial class dialogueLineNode : Button
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Duplicate();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void _on_pressed() {
		Text = "what.";
		Control duplicated = (Control)Duplicate();
		GetParent().AddChild(duplicated);
		duplicated.Position += new Vector2(0, 100);
	}
}
