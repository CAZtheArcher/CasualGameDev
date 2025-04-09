using Godot;
using System;

public partial class Item : Area2D
{ 
    Weapon inst; 
    [Export]
    Module itemType;
    [Export]
    int itemLevel;
    [Export]
    Weapon weaponType;

    Node2D player;

    WeaponManager weaponManager;

    // Setup the collision signal
    public override void _Ready()
    {
        weaponManager = (WeaponManager)GetNode("/root/Main/Player/PlayerBody/PlayerSprite/WeaponManager");
        player = (Node2D)GetNode("/root/Main/Player/PlayerBody");
    }

    public void spawn(Vector2 position, Module itemType)
    {
        this.Position = position;
        this.itemType = itemType;
    }

    private void CollisionDetected(Node2D body)
    {
        if(body.GetType() == typeof(Player)){
            weaponManager.LeftWeapon.AddModule(itemType);
            QueueFree();  // Remove the item from the scene
        }
        else{
            GD.PrintErr("Item (" + this + ") just collided with " + body.GetType() + " (" + body + ") which is not a Player. This should not happen.");
        }
    }

    public override void _Process(double delta)
    {
    }
}
