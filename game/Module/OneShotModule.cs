using Godot;
using System;
using System.Timers;

/// <summary>
/// Fires a BasicBullet.
/// </summary>
public partial class OneShotModule : Module {
    private PackedScene OneShotScene;
   

    public override void _Ready()
    {
        base._Ready();
        OneShotScene = GD.Load<PackedScene>("res://Projectile/sniper/OneShot.tscn");
        spritePath = "res://Projectile/bullet.png";
     

    }
   
    /// <summary>Fires a BasicBullet.</summary>
    public override void Activate()
    {
       
        OneShot bullet_instance = (OneShot)OneShotScene.Instantiate();
        GetTree().Root.AddChild(bullet_instance);
        bullet_instance.GlobalPosition = parent.BulletSpawn.GlobalPosition;
        bullet_instance.Rotation = parent.PlayerSprite.Rotation;
       

    }
}
