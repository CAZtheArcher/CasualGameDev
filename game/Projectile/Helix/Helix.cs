using Godot;
using System;

/// <summary>
/// A basic bullet class to be fired by the player, for killing enemies.  
/// This is a child of the Projectile class.
/// </summary>
public partial class Helix : Projectile
{
    float timeSinceInstantiation;

    public override void _Ready()
	{
		base._Ready();
        velocity = 600;
		damage = 5;
		pierce = 2;
        timeSinceInstantiation = 0;
    }

	public override void _Process(double delta)
	{
        // Move in the direction it was fired in
        // this.Position += this.Transform.X * velocity * (float)delta;

        timeSinceInstantiation += (float)delta;
        var currentYPosition = Position[1];
        currentYPosition = Mathf.Sin(timeSinceInstantiation);
        Position.Y = currentYPosition;

        base._Process(delta);
	}
}
