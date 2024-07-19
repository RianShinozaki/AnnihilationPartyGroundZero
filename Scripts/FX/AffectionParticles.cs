using Godot;
using System;
using System.Collections;

public partial class AffectionParticles : PooledParticle
{
	// Called when the node enters the scene tree for the first time.
	[Export] public float launchAngle = 0; //In degrees
	[Export] public float variance = 15;
	[Export] public float launchSpeed = 50;
	[Export] public float gravity = 9.8f;
	[Export] public float maxFallSpeed = 10;
	[Export] public float maxLifeTime = 1;
	float lifeTime = 0;
	[Export] public float horizontalDampening = 1;
	[Export] public Curve wiggleCurve;
	[Export] public Curve alphaCurve;
	float initWiggleOffset;

	Vector2 vel = new Vector2(0, 0);
	public override void _Ready()
	{
		base._Ready();
		myResPath = "res://Objects/FX/AffectionParticles.tscn";

	}

	public override void Spawn() {
	}
	public override void Init() {
		lifeTime = 0;
		RandomNumberGenerator rand = new RandomNumberGenerator();
		float launchAngleRand = rand.RandfRange(-variance, variance);
		vel = new Vector2(launchSpeed * Mathf.Cos(Mathf.DegToRad(launchAngle + launchAngleRand)), -launchSpeed * Mathf.Sin(Mathf.DegToRad(launchAngle + launchAngleRand)));
		initWiggleOffset = rand.RandfRange(0, 1);
	}

	public override void _Process(double delta)
	{
		if(!active) return;
		
		float d = (float)delta;
		lifeTime += d;
		GlobalPosition += vel * d;
		
		if(vel.Y < maxFallSpeed)
			vel.Y += gravity * d;
		
		if(vel.X != 0 && lifeTime > maxLifeTime/2) 
			vel.X += horizontalDampening * -Mathf.Sign(vel.X) * d;

		if(lifeTime > maxLifeTime) {
			Despawn();
		}
		Modulate = new Color(1, 1, 1, alphaCurve.Sample(lifeTime/maxLifeTime));

		Rotation = wiggleCurve.Sample((lifeTime + initWiggleOffset) % 1) * 0.5f;
	}
}
