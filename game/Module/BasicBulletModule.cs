using Godot;
using System;

/// <summary>
/// Fires a BasicBullet.
/// </summary>
public partial class BasicBulletModule : Module {
    private PackedScene basicBulletScene;

    public override void _Ready()
    {
        base._Ready();
        basicBulletScene = GD.Load<PackedScene>("res://Projectile/BasicBullet/BasicBullet.tscn");
        spritePath = "res://Projectile/bullet.png";
    }

    /// <summary>Fires a BasicBullet.</summary>
    public override void Activate()
    {
        BasicBullet bullet_instance = (BasicBullet)basicBulletScene.Instantiate();
        GetTree().Root.AddChild(bullet_instance);
        bullet_instance.GlobalPosition = parent.BulletSpawn.GlobalPosition;
        bullet_instance.Rotation = parent.PlayerSprite.Rotation;
    }
}
