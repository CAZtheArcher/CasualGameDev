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
		damage = 2;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		base._Process(delta);
	}

    public override void OnArea2DBodyEntered(Node2D body)
	{
		// Basic functionality.  If the projectile collides with an enemy, deletes both.
		// TODO:  Implement dealing damage, and reducing this projectile's pierce variable by 1
		try {
			Enemy enemy = (Enemy)body;
			enemy.EnemyDie();
            this.QueueFree();
        } catch {
            // This try/catch is here in case someone mucks up the collision layers/masks
            // BasicBullets are player bullets (not supposed to collide with non-enemies)
            GD.PrintErr("BasicBullet just collided with something other than an Enemy!  This is not supposed to happen.");
        }
	}

	public override void OnVisibleOnScreenNotifier2DScreenExited()
	{
		this.QueueFree();
	}
}
