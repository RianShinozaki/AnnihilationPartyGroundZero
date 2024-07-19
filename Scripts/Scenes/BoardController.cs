using Godot;
using System;
using System.ComponentModel;
using System.Linq.Expressions;

public partial class BoardController : Node2D
{
	TextureRect image;
	GameController.Location nextLoc;
	string nextScene;
	Sprite2D closeUpBG;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		image = GetNode<TextureRect>("TextureRect");
		closeUpBG = GetNode<SubViewport>("CloseUpView").GetNode<Sprite2D>("Background");
		closeUpBG.Position = new Vector2(-280, -100);
		closeUpBG.Scale = new Vector2(0.7f, 0.7f);
		((ShaderMaterial)image.Material).SetShaderParameter("split_slope",2.6f);

	}

	public override void _Process(double delta)
	{
	}
	public void MoveFromOffice(GameController.Location location, string goToScene) {
		nextScene = goToScene;
		nextLoc = location;

		Tween tween = GetTree().CreateTween();
		tween.SetTrans(Tween.TransitionType.Sine);
		Callable callable = new Callable(this, MethodName.SetSplitSlope);
		tween.TweenMethod(callable, 2.6f, -2.6f, 0.75f);
		tween.TweenMethod(callable, -2.6f, -2.6f, 0.25f);
		tween.TweenCallback(Callable.From(CloseUpZoomEnd));
	}
	public void CloseUpZoomEnd() {
		GameController.currentLocation = nextLoc;
		GameController.Instance.OnSwitchSceneTransitionBegin(nextScene);
		GameController.canReturnButtonAppear =false;
		
		Tween tween = GetTree().CreateTween();
		tween.SetTrans(Tween.TransitionType.Sine);
		tween.Parallel().TweenProperty(closeUpBG, "position", new Vector2(-450, -100), 0.75f);
		tween.Parallel().TweenProperty(closeUpBG, "scale", new Vector2(0.575f, 0.575f), 0.75f);
		tween.TweenCallback(Callable.From(CloseUpZoomEndEnd));
	}
	//If anyone sees this, please help me or kill me
	public void CloseUpZoomEndEnd() {
		Tween tween = GetTree().CreateTween();
		tween.TweenProperty(closeUpBG, "position", new Vector2(-450, -100), 0.75f);
		tween.TweenCallback(Callable.From(CloseUpZoomEndEndEnd));
	}
	//Oh lord. What have I become?
	public void CloseUpZoomEndEndEnd() {
		Tween tween = GetTree().CreateTween();
		tween.SetTrans(Tween.TransitionType.Sine);

		tween.Parallel().TweenProperty(closeUpBG, "position", new Vector2(-500, -100), 1.5f);
		tween.Parallel().TweenProperty(closeUpBG, "scale", new Vector2(0.545f, 0.545f), 1.5f);
	}
	void SetSplitSlope(float val) {
		((ShaderMaterial)image.Material).SetShaderParameter("split_slope",val);
	}
	
}
