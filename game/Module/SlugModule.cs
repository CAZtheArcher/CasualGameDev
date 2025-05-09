using Godot;
using System;

/// <summary>
/// Fires a slug.
/// </summary>
public partial class SlugModule : Module {
    private PackedScene slugScene;
    //private float offset = 90 * (Mathf.Pi / 180);

    public override void _Ready()
    {
        base._Ready();
        slugScene = GD.Load<PackedScene>("res://Projectile/Slug/Slug.tscn");
        
        spritePath = "res://Projectile/Slug/Slug.png";
    }

    /// <summary>Fires a slug.</summary>
    public override void Activate()
    {
        Slug bullet_instance = (Slug)slugScene.Instantiate();
        GetTree().Root.AddChild(bullet_instance);
        bullet_instance.GlobalPosition = parent.BulletSpawn.GlobalPosition;
        bullet_instance.Rotation = parent.PlayerSprite.Rotation;
    }
}
