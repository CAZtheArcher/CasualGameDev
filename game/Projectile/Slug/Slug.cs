using Godot;
using System;

/// <summary>
/// A basic bullet class to be fired by the player, for killing enemies.  
/// This is a child of the Projectile class.
/// </summary>
public partial class Slug : Projectile
{
    private int knockbackAmount;
    private Vector2 spawnPoint; // Used to calculate knockback direction 

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
        velocity = 550;
		pierce = 1;
		damage = 9;
        knockbackAmount = 750;
        spawnPoint = this.Position;
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        base._Process(delta);
	}

    public override void OnArea2DBodyEntered(Node2D body)
    {
        // Slug rounds knock enemies backwards.
        try
        {
            Enemy enemy = (Enemy)body;
            enemy.DecreaseHealth(damage);

            enemy.Knockback(knockbackAmount);

            pierce -= 1;
            // pierce -= enemy.PierceResistance // (Large enemies can be harder to pierce) - Not implemented yet
            if (pierce < 1) this.QueueFree();
        }
        catch
        {
            GD.PrintErr("Slug just collided with something other than an Enemy!  This is not supposed to happen.");
        }
    }
}
