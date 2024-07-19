using Godot;
using System;
using System.ComponentModel;

public partial class PooledParticle : Node2D
{
	public bool active = false;
	PooledParticleEmitter emitter;
	public string myResPath;

	public override void _Ready()
	{
		emitter = GetParent<PooledParticleEmitter>();
		active = false;
		Hide();
	}
	//Spawn happens upon particle spawn
	public virtual void Spawn() {

	}
	//Init must be called by the emitter to ensure the particle has the desired values first
	public virtual void Init() {

	}
	public PooledParticle Spawn(Vector2 position) {
		Show();
		Init();
		GlobalPosition = position;
		active = true;
		return this;
	}
	public void Despawn() {
		Hide();
		active = false;
		emitter.ReAddToQueue(this);
	}
}
