using Godot;
using System;
using System.Diagnostics;
using System.IO;

[Tool]
public partial class CalendarController : Control
{
	String resourcePath;
	FileDialog loadFolderDialog;
	ConfirmationDialog initFolderDialog;
	AcceptDialog loadFailDialog;
	CalendarSettings myCalendarSettings;
	public override void _Ready()
	{
		
	}
    public override void _EnterTree()
    {
        base._EnterTree();

		loadFolderDialog = GetNode<FileDialog>("CalendarSystem/OptionsGroup/LoadFolderDialog");
		initFolderDialog = GetNode<ConfirmationDialog>("CalendarSystem/OptionsGroup/InitFolderDialog");
		loadFailDialog = GetNode<AcceptDialog>("CalendarSystem/OptionsGroup/LoadFailDialog");
		//Control editor_interface = EditorInterface.Singleton.GetBaseControl();
		//loadFolderDialog.GetParent().RemoveChild(loadFolderDialog);
		//editor_interface.AddChild(loadFolderDialog);
    }

    public override void _Process(double delta)
	{
	}

	private void _on_load_button_pressed() {
		loadFolderDialog.Popup();
		GD.Print("hello?");
	}
	private void _on_load_folder_dialog_dir_selected(String path) {
		GD.Print("Calendar resource path selected");
		resourcePath = path;
		if(ResourceLoader.Exists(resourcePath + "settings")) {
			myCalendarSettings = ResourceLoader.Load<CalendarSettings>(resourcePath + "settings");
		}
		else {
			GD.Print("No calendar settings"); 
			DirAccess thisDir = DirAccess.Open(resourcePath);
			string[] files = thisDir.GetFiles();
			if(files.Length > 0) loadFailDialog.PopupCentered();
			else {
				initFolderDialog.PopupCentered();
			}
		}
		//string[] files = Directory.GetFiles(path);
		//if(files.Length > 0) GD.Print("Not an empty directory!");
	}

	private void _on_init_folder_dialog_confirmed() {
		myCalendarSettings = new CalendarSettings(DayOfWeek.Sunday);
		ResourceSaver.Singleton.Save(myCalendarSettings, resourcePath);
	}
}
