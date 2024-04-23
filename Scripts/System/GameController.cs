using Godot;
using System;

public enum DayOfWeek {
	Sunday,
	Monday,
	Tuesday,
	Wednesday,
	Thursday,
	Friday,
	Saturday
}
public enum Suspects {
    Engineer,
    Occultist,
    Teacher,
    Butcher,
    COUNT
}

public partial class GameController : Node
{

    [Signal]
    public delegate void SwitchSceneTransitionBeginEventHandler(string newScene);
    [Signal]
    public delegate void SwitchSceneEventHandler();
    [Signal]
    public delegate void MoneyChangedEventHandler();
    [Signal]
    public delegate void BeginIntroSequenceEventHandler();

    public enum GameState {
        Office,
        SuspectLocation,
        Dialogue,
        Transitioning,
        SuspectLog,
        FinalChoice
    }

    public enum Location {
        Office,
        Butcher,
        Teacher,
        Engineer,
        Occultist
    }

    public static Godot.Collections.Array days = new Godot.Collections.Array{
		"Friday",
		"Saturday",
		"Sunday",
		"Monday",
		"Tuesday",
		"Wednesday",
		"Thursday"
	};
    
	public static GameController Instance;
    
	public static float splitX;
	public static float split2X;
	public static float split3X;
	static float wishSplitX;
    public static Speaker theSpeaker;
    public static Location currentLocation;
    public static GameState currentState;


    const int MORNING = 0;
    const int EVENING = 1;
    const int NIGHTFALL = 2;

    public static int currentTime = 0;

    public static int currentDay = 1;

    public const int BUTCHER = 0;
    public const int SOFTWARE = 1;
    public const int TEACHER = 2;
    public const int BARTENDER = 3;
    public const int OCCULTER = 4;
    public const int OLDGUARD = 5;

    public static int steaks = 0;
    public static int hams = 0;
    public static int brokenPhones = 0;

    public static float money;
    public static Godot.Collections.Array items = new Godot.Collections.Array();

    [Export] public static DateInformation todayDateInformation;
	[Export] public string logFolder = "res://Data/CalendarSystem/December";

    public static float[] trustLevels = new float[5];

    public static short[] butcherMemory = new short[10];
    //0 -- marital status
    //1 -- has met :: 0 -- no :: 1 -- yes

    public static short[] teacherMemory = new short[10];
    //0 -- has kids :: 1 -- no :: 2 -- yes
    //1 -- has met :: 0 -- no :: 1 -- yes
    //2 -- worked w/o them :: 0 -- no :: 1 -- yes

    public static short[] engineerMemory = new short[10];
    //0 -- know his fav anime ? :: 0 -- no :: 2 -- yes
    //1 -- has met :: 0 -- no :: 1 -- yes
    //2 -- know job :: 0 -- ? :: 1 -- writer :: 2 -- unemployed :: 3 -- engineer
    //3 -- final dialogue

    public static short[] occultistMemory = new short[10];
    //0 -- has met :: 0 -- no :: 1 -- yes
    //1 -- fortunes pulled
    //2 -- final dialogue

    public static bool[] engineerQuestionFlags = new bool[10];
    public static bool[] butcherQuestionFlags = new bool[10];
    public static bool[] occultistQuestionFlags = new bool[10];
    public static bool[] teacherQuestionFlags = new bool[10];

    public static bool oldGuardCheck = false;
    
    public static Godot.Collections.Dictionary[] dialogueRecords = new Godot.Collections.Dictionary[31];

    public static int timesCalledOldGuard = 0;

    public static bool goodEnding = false;
    public static bool canReturnButtonAppear = false;

    public override void _Ready()
    {
        base._Ready();
        wishSplitX = 215;
        GetTree().CallDeferred(SceneTree.MethodName.ChangeSceneToFile, "res://MainGame2D.tscn");
        Instance = this;

        for(int i = 0; i < trustLevels.Length; i++) {
            trustLevels[i] = 0;
        }
        InitializeNewDay();

        money = 200;
    }
    public static void SetSplitX(float x) {
		wishSplitX = x;
	}
    public override void _Process(double delta)
    {
        

        base._Process(delta);
		splitX = Mathf.Lerp(splitX, wishSplitX, (float)delta*5);
		split2X = Mathf.Lerp(split2X, wishSplitX-2, (float)delta*4);
		split3X = Mathf.Lerp(split3X, wishSplitX-4, (float)delta*3);
    }

    public void DoIntro() {
        wishSplitX = 600;
        splitX = 600;
        split2X = 600;
        split3X = 600;
        EmitSignal(SignalName.BeginIntroSequence);
    }

    public void OnSwitchScene() {
        EmitSignal(SignalName.SwitchScene);
        if(currentLocation == Location.Office && currentTime == 0 && currentDay%7==0) {
            SetMoney(200);
        }
        
    }
    public void OnSwitchSceneTransitionBegin(string newScene) {
        EmitSignal(SignalName.SwitchSceneTransitionBegin, newScene);
        if(currentLocation == Location.Office) {
            wishSplitX = 215;
            currentState = GameState.Office;
            currentTime++;
            if(currentTime > 1) {
                currentDay++;
                currentTime = 0;
                InitializeNewDay();
            }
        } else {
            wishSplitX = 50;
            currentState = GameState.SuspectLocation;
        }
    }
    private void InitializeNewDay() {
        todayDateInformation = GD.Load<DateInformation>(logFolder + "/Day" + currentDay.ToString() + ".tres");
    }
    public static void AddToInventory(Item item) {
        items.Add(item);
    }
    public void ChangeMoney(float amount) {
        money += amount;
        EmitSignal(SignalName.MoneyChanged);
    }
    public void SetMoney(float amount) {
        money = amount;
        EmitSignal(SignalName.MoneyChanged);
    }
    public static string GetDay(int day) {
        return (string)days[(day-1)%7];
    }
}

