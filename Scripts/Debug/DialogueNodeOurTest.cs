using Godot;
using System;

public partial class DialogueNodeOurTest : Control
{
	[Export] DialogueBoxBridge dialogueBox;
	
	private void _on_button_pressed() {
		dialogueBox.Start("start");
	}
}
