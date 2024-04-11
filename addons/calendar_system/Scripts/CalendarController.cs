using Godot;
using System;
using System.Diagnostics;
using System.IO;

[Tool]
public partial class CalendarController : Control
{
	public String resourcePath;
	FileDialog loadFolderDialog;
	ConfirmationDialog initFolderDialog;
	AcceptDialog loadFailDialog;
	public CalendarSettings myCalendarSettings;
	public Date focusedDate;
	
	public static CalendarController Instance;
	public DateInformation copiedDate;

	[Signal]
	public delegate void CalendarLoadedEventHandler();
	[Signal]
	public delegate void ChangedFocusedDateEventHandler(Date newDate);
	public override void _Ready()
	{
		
	}
    public override void _EnterTree()
    {
        base._EnterTree();
		Instance = this;

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
	}
	private void _on_load_folder_dialog_dir_selected(String path) {
		GD.Print("Calendar resource path selected");
		resourcePath = path;
		if(ResourceLoader.Exists(resourcePath + "/settings.tres")) {
			LoadData();
		}
		else {
			GD.Print("No calendar settings found"); 
			DirAccess thisDir = DirAccess.Open(resourcePath);
			thisDir.ListDirBegin();
			string nextFile = thisDir.GetNext();
			if(nextFile != "") {
				loadFailDialog.PopupCentered();
				GD.Print(nextFile);
			}
			else {
				thisDir.ListDirEnd();
				initFolderDialog.PopupCentered();
			}
		}
	}

	private void _on_init_folder_dialog_confirmed() {
		GD.Print("Initializing calendar resource folder");
		myCalendarSettings = new CalendarSettings();
		myCalendarSettings.firstDay = DayOfWeek.Sunday;
		myCalendarSettings.days = 31;

		ResourceSaver.Singleton.Save(myCalendarSettings, resourcePath + "/settings.tres");

		for(int i = 0; i < 31; i++) {
			DateInformation newDateInformation = new DateInformation();
			newDateInformation.day = i+1;
			ResourceSaver.Singleton.Save(newDateInformation, resourcePath + "/Day" +(i+1).ToString() + ".tres");
		}
		LoadData();
		
	}

	private void LoadData() {
		myCalendarSettings = GD.Load<CalendarSettings>(resourcePath + "/settings.tres");
		EmitSignal(SignalName.CalendarLoaded);
	}
	private void _on_calendarsettings_pressed() {
      	EditorInterface.Singleton.InspectObject(myCalendarSettings);
	}
	private void _on_reload_pressed() {
		LoadData();
	}
}
