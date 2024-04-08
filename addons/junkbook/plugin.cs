using Godot;
using System;

[Tool]
public partial class plugin : EditorPlugin
{
	PackedScene editorAddon = ResourceLoader.Load<PackedScene>("res://addons/junkbook/Scenes/control.tscn");
    Control dockedScene;
	public override void _EnterTree()
    {
        base._EnterTree();
		dockedScene = (Control)editorAddon.Instantiate();
		AddControlToDock(DockSlot.LeftUr, dockedScene);
    }
    public override void _ExitTree()
    {
        base._ExitTree();
		RemoveControlFromDocks(dockedScene);
		dockedScene.QueueFree();
    }
}
