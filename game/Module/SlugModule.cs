using Godot;
using System;

/// <summary>
/// Fires a slug.
/// </summary>
public partial class SlugModule : Module {
    private PackedScene slugScene;
    private Weapon parent;

    public override void _Ready()
    {
        slugScene = GD.Load<PackedScene>("res://Projectile/Slug/Slug.tscn");
        parent = GetParent<Weapon>();
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
