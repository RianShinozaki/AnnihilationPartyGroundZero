#if TOOLS
using Godot;
using System;

[Tool]
public partial class calendar_system : EditorPlugin
{
	PackedScene editorAddon = ResourceLoader.Load<PackedScene>("res://addons/calendar_system/Scenes/DockTab.tscn");
    Control dockedScene;

	public override void _EnterTree()
	{
		dockedScene = (Control)editorAddon.Instantiate();
		EditorInterface.Singleton.GetEditorMainScreen().AddChild(dockedScene);

		_MakeVisible(false);
	}

	public override void _ExitTree()
	{
		if(dockedScene != null) {
			dockedScene.QueueFree();
		}
	}

	 public override bool _HasMainScreen()
    {
        return true;
    }

    public override void _MakeVisible(bool visible)
    {
        if (dockedScene != null)
        {
            dockedScene.Visible = visible;
        }
    }

    public override string _GetPluginName()
    {
        return "Calendar";
    }

    public override Texture2D _GetPluginIcon()
    {
        // Must return some kind of Texture for the icon.
        return EditorInterface.Singleton.GetEditorTheme().GetIcon("Node", "EditorIcons");
    }
}
#endif
