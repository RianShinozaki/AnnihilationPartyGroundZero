using Godot;

public partial class DialogueBoxBridge : Node {

    Node dialogueBox;

    public override void _Ready() {
        base._Ready();

        dialogueBox = GetChild(0);
    }

    public void Start(string ID) => dialogueBox.Call("start", ID);
    public void Proceeed(string ID) => dialogueBox.Call("proceed", ID);
    public void Stop() => dialogueBox.Call("stop");
    public void Reset() => dialogueBox.Call("reset");
}
