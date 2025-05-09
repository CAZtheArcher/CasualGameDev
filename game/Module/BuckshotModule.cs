using Godot;
using System;

/// <summary>
/// Fires a BasicBullet.
/// </summary>
public partial class BuckshotModule : Module {
    private PackedScene buckshotScene;
    private short pelletCount;
    private short pelletSpreadVariance; // Degrees
    private Random rng;
    //private float offset = 90 * (Mathf.Pi / 180);

    public override void _Ready()
    {
        base._Ready();
        buckshotScene = GD.Load<PackedScene>("res://Projectile/Buckshot/Buckshot.tscn");
        pelletCount = 9; // 9 is the pellet count of 00 buckshot, so I used it here
        pelletSpreadVariance = 30;
        rng = new Random();
        spritePath = "res://Projectile/Buckshot/Buckshot.png";
    }

    /// <summary>Fires a pelletCount pellets.</summary>
    public override void Activate()
    {
        for(byte i = 0; i < pelletCount; i++){
            Buckshot pellet_instance = (Buckshot)buckshotScene.Instantiate();
            
            pellet_instance.GlobalPosition = parent.BulletSpawn.GlobalPosition;
            float rotation = parent.PlayerSprite.Rotation;
            if(i != 0){ // First pellet has no spread.
                rotation += (float)(rng.Next(pelletSpreadVariance) * Math.PI / 180);
                rotation -= (float)(pelletSpreadVariance * Math.PI / 180 / 2);
            }
            pellet_instance.Rotation = rotation;

            GetTree().Root.AddChild(pellet_instance);
        }
    }
}
