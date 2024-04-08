using Godot;
using System;

[Tool]
public partial class DialogueNode : Control
{
	bool moveMode;
	Vector2 mouseDelta;

	public override void _Ready()
	{
	}


	public override void _Process(double delta)
	{
		if(moveMode) {
			Position = GetViewport().GetMousePosition() + mouseDelta;
		}
	}

	private void _on_move_button_down() {
		moveMode = true;
		mouseDelta = Position - GetViewport().GetMousePosition();
	}
	private void _on_move_button_up() {
		moveMode = false;
	}
}
