using Godot;
using System;
using System.Text.RegularExpressions;

public partial class TeacherNPC : DialogueCaller
{
	[Export] public Sprite2D NPCSprite;
	[Export] public AnimationPlayer animPlayer;
	[Export] public System.Int32 myAvailabilityMask = 0b0010;
	DialogueBoxBridge bridge;
	[Export] public Resource myDialogueResource;
	PooledParticleEmitter particleEmitter;

	float trustAtStartOfMeeting;

	public override void _Ready()
    {
		trustAtStartOfMeeting = GameController.trustLevels[GameController.TEACHER];
        base._Ready();

		animPlayer.Play("Establish");
		if(false || GameController.currentTime != 0 || (GameController.todayDateInformation.Available & myAvailabilityMask) == 0) {
			textbox_system.Instance.Initialize(-100);
			NPCSprite.Visible = false;
			return;
		}
		
		bridge = GameController.theDialogueBoxBridge;
		bridge.dialogueBox.Set("dialogue_data", myDialogueResource);
		particleEmitter = GetNode<PooledParticleEmitter>("PooledParticleEmitter");

		Callable callable = new Callable(this, MethodName._on_dialogue_signal);
		bridge.dialogueBox.Connect("dialogue_signal", callable);
    }
	private void _on_animation_player_animation_finished(StringName anim_name) {
		if(anim_name == "Establish" && NPCSprite.Visible) {
			animPlayer.Play("Intro");
		}
	}
    public override void _Process(double delta)
    {
		base._Process(delta);
    }
	private void _on_dialogue_signal(string value) {
		switch(value) {
			case "trustPlus":
				for(int i = 0; i < 4; i++){
					PooledParticle part = particleEmitter.Spawn("res://Objects/FX/AffectionParticles.tscn", particleEmitter.GlobalPosition);
					AffectionParticles affectPart = (AffectionParticles)part;
					affectPart.launchAngle = -60 + 40*i + 90;
					affectPart.Init();
				}
				GameController.trustLevels[GameController.TEACHER] += 0.5f;
				bridge.SetVariable("trust", 3, GameController.trustLevels[GameController.TEACHER]);
				break;
		}
	}

	public void Init() {
		if((bool)GameController.teacherMemory["has_met"] == true) {
			bridge.Start("normal");
		} else {
			bridge.Start("intro");
		}
	}

}
