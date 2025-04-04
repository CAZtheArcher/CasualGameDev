using Godot;
using System;

public partial class Item : RigidBody2D
{ 
    Weapon inst; 
    [Export]
    Module itemType;
    [Export]
    int itemLevel;

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
    //work in progres
    private void _on_Item_body_entered()
    {
        if (((this.Position.Y + 20 > player.Position.Y) && (this.Position.Y - 20 < player.Position.Y)) && ((this.Position.X + 20 > player.Position.X) && (this.Position.X - 20 < player.Position.X)))  // Check if the player collided with the item
        {
            //GD.Print(itemType);
            weaponManager.LeftWeapon.AddModule(itemType);
            // Emit the signal for pickup
            QueueFree();  // Remove the item from the scene
        }
    }


    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        _on_Item_body_entered();

    }
}
