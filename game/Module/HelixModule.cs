using Godot;
using System;

/// <summary>
/// Fires a BasicBullet.
/// </summary>
public partial class HelixModule : Module {
    private PackedScene helixScene;

    public override void _Ready()
    {
        base._Ready();
        helixScene = GD.Load<PackedScene>("res://Projectile/Helix/Helix.tscn");
        spritePath = "res://Projectile/Helix/Helix.png";
    }

    /// <summary>Fires a BasicBullet.</summary>
    public override void Activate()
    {
        for (int i = 0; i < 2; i++)
        {
            Helix helix_instance = (Helix)helixScene.Instantiate();
            GetTree().Root.AddChild(helix_instance);
            helix_instance.GlobalPosition = parent.BulletSpawn.GlobalPosition;
            helix_instance.Rotation = parent.PlayerSprite.Rotation;
            if(i==1) helix_instance.ReverseWave();
        }
        
    }
}
