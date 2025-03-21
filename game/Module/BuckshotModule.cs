using Godot;
using System;

/// <summary>
/// Fires a BasicBullet.
/// </summary>
public partial class BuckshotModule : Module {
    private PackedScene buckshotScene;
    private Weapon parent;
    private short pelletCount;
    private short pelletSpreadVariance; // Degrees
    private Random rng;

    public override void _Ready()
    {
        buckshotScene = GD.Load<PackedScene>("res://Projectile/Buckshot/Buckshot.tscn");
        parent = GetParent<Weapon>();
        pelletCount = 9; // 9 is the pellet count of 00 buckshot, so I used it here
        pelletSpreadVariance = 30;
        rng = new Random();
    }

    /// <summary>Fires a BasicBullet.</summary>
    public override void Activate()
    {
        for(byte i = 0; i < pelletCount; i++){
            Buckshot pellet_instance = (Buckshot)buckshotScene.Instantiate();
            
            pellet_instance.GlobalPosition = parent.BulletSpawn.GlobalPosition;
            float rotation = parent.PlayerSprite.Rotation;
            rotation += (float)(rng.Next(pelletSpreadVariance) * Math.PI / 180);
            rotation -= (float)(rng.Next(pelletSpreadVariance) * Math.PI / 180 / 2);
            pellet_instance.Rotation = rotation;

            GetTree().Root.AddChild(pellet_instance);
        }
    }
}
