using Godot;
using System;

/// <summary>
/// A basic bullet class to be fired by the player, for killing enemies.  
/// This is a child of the Projectile class.
/// </summary>
public partial class BasicBullet : Projectile
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
        velocity = 250;
		damage = 3;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		base._Process(delta);
	}
}
