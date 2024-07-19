using System;
using Godot;

public partial class DialogueBoxBridge : Node {

    public Node dialogueBox;


    public override void _Ready() {
        base._Ready();
        dialogueBox = GetChild(0);
        GameController.theDialogueBoxBridge = this;
    }

    public void Start(string ID) => dialogueBox.Call("start", ID);
    public void Proceeed(string ID) => dialogueBox.Call("proceed", ID);
    public void Stop() => dialogueBox.Call("stop");
    public void Reset() => dialogueBox.Call("reset");
    public void SetVariable(String varName, int type, dynamic value, int op = 0) => dialogueBox.Call("set_variable", varName, type, value, op);
    public void GetVariable(String varName) => dialogueBox.Call("get_variable", varName);
}
