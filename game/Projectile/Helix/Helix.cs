using Godot;
using System;

/// <summary>
/// A basic bullet class to be fired by the player, for killing enemies.  
/// This is a child of the Projectile class.
/// </summary>
public partial class Helix : Projectile
{
    float timeSinceInstantiation;
    /// <summary> How many times, per second, this helix will move like a wave</summary>
    int cyclesPerSecond;
    /// <summary> How large the wave movement will be</summary>
    int cycleDistance;

    bool reversed;

    public override void _Ready()
	{
		base._Ready();
        velocity = 600;
		damage = 5;
		pierce = 2;

        timeSinceInstantiation = 0;
        cyclesPerSecond = (int)velocity / 25;
        cycleDistance = 120;
        reversed = false;
    }

	public override void _Process(double delta)
	{
        timeSinceInstantiation += (float)delta;
        Vector2 waveVector = new Vector2(
            Mathf.Sin(timeSinceInstantiation * cyclesPerSecond) * cycleDistance, 
            Mathf.Sin(timeSinceInstantiation * cyclesPerSecond) * cycleDistance);
        waveVector = waveVector.Rotated(Rotation - 0.785398f/*45 degrees*/);

        if(reversed) this.Position += new Vector2(-waveVector.X * (float)delta, -waveVector.Y * (float)delta);
        else this.Position += new Vector2(waveVector.X * (float)delta, waveVector.Y * (float)delta);

        // Move in the direction it was fired in
        //this.Position += this.Transform.Y * -1 * velocity * (float)delta;
        base._Process(delta);
    }

    /// <summary>Reverses the direction in which the helix projectile wiggles</summary>
    public void ReverseWave()
    {
        reversed = !reversed;
    }
}
