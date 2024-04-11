using Godot;
using System;
using System.IO;

[Tool]
public partial class Date : Node
{
	public DateInformation myInformation;
	public int day;

    public override void _Ready()
    {
        base._Ready();
		  myInformation = GD.Load<DateInformation>(CalendarController.Instance.resourcePath + "/Day" + day.ToString() + ".tres");
    }
    private void _on_edit_pressed() {
      EditorInterface.Singleton.InspectObject(myInformation);
    }
    private void _on_copy_pressed() {
      CalendarController.Instance.copiedDate = myInformation;
    }
    private void _on_paste_pressed() {
      DateInformation newDate = CalendarController.Instance.copiedDate.Duplicate() as DateInformation;
      newDate.day = day;
      myInformation = newDate;
      
      DirAccess thisDir = DirAccess.Open(CalendarController.Instance.resourcePath);
      thisDir.Remove("Day" + day.ToString() + ".tres");

		  ResourceSaver.Singleton.Save(myInformation, CalendarController.Instance.resourcePath + "/Day" + day.ToString() + ".tres");
    }
}
