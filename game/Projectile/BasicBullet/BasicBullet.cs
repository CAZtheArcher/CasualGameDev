using Godot;
using System;

/// <summary>
/// A basic bullet class to be fired by the player, for killing enemies.  
/// This is a child of the Projectile class.
/// </summary>
public partial class BasicBullet : Projectile
{
	public override void _Ready()
	{
		base._Ready();
        velocity = 600;
		damage = 5;
		pierce = 2;
	}

	public override void _Process(double delta)
	{
        base._Process(delta);
	}
}
