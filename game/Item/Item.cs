using Godot;
using System;

public partial class Item : Area2D
{
    Weapon inst;
    [Export] Module itemType;
    [Export] int itemLevel;
    [Export] Weapon weaponType;
    bool playerIsColliding;

    Node2D player;

    WeaponManager weaponManager;

    public override void _Ready()
    {
        weaponManager = (WeaponManager)GetNode("/root/Main/Player/PlayerBody/PlayerSprite/WeaponManager");
        player = (Node2D)GetNode("/root/Main/Player/PlayerBody");
        playerIsColliding = false;
    }

    public void spawn(Vector2 position, Module itemType)
    {
        this.Position = position;
        this.itemType = itemType;
    }

    private void CollisionDetected(Node2D body)
    {
        if (body.GetType() == typeof(Player))
        {
            GD.Print("PlayerCollisionDetected - Item.cs");
            playerIsColliding = true;
        }
        else
        {
            GD.PrintErr("Item (" + this + ") just collided with " + body.GetType() + " (" + body + ") which is not a Player. This should not happen.");
        }
    }

    private void CollisionNoLongerDetected(Node2D body)
    {
        if (body.GetType() == typeof(Player))
        {
            GD.Print("PlayerCollisionNoLongerDetected - Item.cs");
            playerIsColliding = false;
        }
        else
        {
        }
    }


    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {

        if (Input.IsActionPressed("swapL") && playerIsColliding){
            GD.Print("Left Weapon Swap");
            weaponManager.LeftWeapon.AddModule(itemType);
            playerIsColliding = false;
            QueueFree();
        }
        else if (Input.IsActionPressed("swapR") && playerIsColliding){
            GD.Print("Right Weapon Swap");
            weaponManager.RightWeapon.AddModule(itemType);
            playerIsColliding = false;
            QueueFree();
        }
    }
}
