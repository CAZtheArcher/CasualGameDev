using Godot;
using System;
using System.Diagnostics.Metrics;

/// <summary>
/// Projectile is the base class used for player and enemy projectiles.
/// It contains base values that all projectiles need to function, like speed and damage.
/// <para>*It is never instantiated or attached to scenes*</para>
/// </summary>
public abstract partial class Projectile : Node2D
{
    /// <summary> Speed that the projectile travels</summary>
    [Export]
    protected int velocity;
    /// <summary> Damage this projectile deals to what it collides with, upon collision</summary>
    [Export]
    protected int damage;
    /// <summary> # of targets this projectile can damage before it is destroyed.
    /// Reduced by 1 each time an enemy is damaged.</summary>
    [Export]
    protected int pierce; 

    // Called when the node enters the scene tree for the first time.
    public override void _Ready(){

    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta){
        // Move in the direction it was fired in
        this.Position += this.Transform.X * velocity * (float)delta;
	}

    /// <summary>
    /// This method is called whenever Projectile detects a collision with another object.
    /// **To hook it up, select the Area2D in your projectile's .tscn, navigate to the
    /// "Node" tab of the inspector window, right click "body_entered(body: Node2D)", select
    /// connect, and in the pop up box, connect it to the Projectile child script.
    /// </summary>
    /// <param name="body">The body that this projectile collided with</param>
    public abstract void OnArea2DBodyEntered(Node2D body);
}
