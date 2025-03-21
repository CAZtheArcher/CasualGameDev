using Godot;
using System;

/// <summary>
/// A basic bullet class to be fired by the player, for killing enemies.  
/// This is a child of the Projectile class.
/// </summary>
public partial class Buckshot : Projectile
{
	private int speedVariance; // Range of speed in which this pellet can move
	private float baseVelocity;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
		speedVariance = 120;
        velocity = 200;
		baseVelocity = velocity;
		velocity += new Random().Next(speedVariance) - speedVariance / 2;
		pierce = 1;
		damage = 1;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        // Pellet speed starts at velocity and slows/speeds up until it reaches baseVelocity.
		// This is done so when th pellets are shot, they form a 'clump' that looks cool.
        if (velocity > baseVelocity + 1) velocity -= (float)delta * 20;
		if (velocity < baseVelocity - 1) velocity += (float)delta * 20;
        base._Process(delta);
	}
}
