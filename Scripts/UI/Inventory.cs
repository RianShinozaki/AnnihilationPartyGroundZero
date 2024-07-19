using Godot;
using System;

public partial class Inventory : Control
{
	public static Inventory instance;
	[Export] public VBoxContainer itemsContainer;
	AnimationTree animTree;
	bool open = false;
	public override void _Ready()
	{
		instance = this;
		
		animTree = GetNode<AnimationTree>("AnimationTree");
		animTree.Set("parameters/conditions/closed", true);
		animTree.Set("parameters/conditions/open", false);
		
		Visible = true;
	}
	public override void _Process(double delta)
	{
	}

	public void UpdateInventory() {

	}
	private void _on_button_pressed() {
		if(open) return;

		animTree.Set("parameters/conditions/closed", false);
		animTree.Set("parameters/conditions/open", true);
		open = true;
	}
	private void _on_close_button_pressed() {
		if(!open) return;

		animTree.Set("parameters/conditions/closed", true);
		animTree.Set("parameters/conditions/open", false);
		open = false;
	}
}
