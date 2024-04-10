using Godot;
using System;

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
}
