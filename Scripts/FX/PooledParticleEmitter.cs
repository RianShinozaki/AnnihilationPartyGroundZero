using Godot;
using System;
using System.Security.AccessControl;

public partial class PooledParticleEmitter : Node2D
{
	[Export] private Godot.Collections.Dictionary _PoolInit = new Godot.Collections.Dictionary
	{
	};
	private Godot.Collections.Dictionary _PoolObjects = new Godot.Collections.Dictionary
	{
	};
	
	float emissionDelay = 2f;
	float timer = 0;
	string myResPath;
	public override void _Ready()
	{
		foreach (var (objPath, amount) in _PoolInit)
		{
			_PoolObjects[(string)objPath] = new Godot.Collections.Array<Node>();
			for(var i = 0; i < (int)amount; i++) {
				String theObj = (String)objPath;
				Node scene = ResourceLoader.Load<PackedScene>(theObj).Instantiate();
				AddChild(scene);
				((Godot.Collections.Array<Node>)_PoolObjects[(string)objPath]).Insert(0, scene);
			}
		}
	}

	public override void _Process(double delta)
	{

	}
	public PooledParticle Spawn(string resPath, Vector2 position) {
		//If there are particles in queue
		PooledParticle theParticle;
		if (((Godot.Collections.Array<Node>)_PoolObjects[(string)resPath]).Count > 0) {
			theParticle = (PooledParticle)((Godot.Collections.Array<Node>)_PoolObjects[(string)resPath])[^1];
			((Godot.Collections.Array<Node>)_PoolObjects[(string)resPath]).RemoveAt(((Godot.Collections.Array<Node>)_PoolObjects[(string)resPath]).Count-1);
			theParticle.Spawn(position);
		} 
		//No more particles left -- add a new one
		else {
			Node scene = ResourceLoader.Load<PackedScene>(resPath).Instantiate();
			AddChild(scene);
			theParticle = (PooledParticle)scene;
			theParticle.Spawn(position);
		}
		return theParticle;
	}
	public void ReAddToQueue(PooledParticle particle ) {
		((Godot.Collections.Array<Node>)_PoolObjects[particle.myResPath]).Insert(0, particle);
	}
}
