using Godot;
using System;

public partial class CutIn : Node
{
	AnimationPlayer animPlayer;
	RichTextLabel myText;

	public override void _Ready()
	{
		animPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		myText = GetNode<RichTextLabel>("RichTextLabel");
		GameController.Instance.PlayCutIn += onPlayCutIn;
	}

	private void onPlayCutIn(string text, Color color) {
		myText.Text = text;
		myText.Modulate = color;
		animPlayer.Play("CutIn");
	}
}
