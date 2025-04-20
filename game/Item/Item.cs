using Godot;
using System;

public partial class Item : Area2D
{ 
    Weapon inst; 
    [Export] Module itemType;
    [Export] int itemLevel;
    [Export] Weapon weaponType;

    Node2D player;

    WeaponManager weaponManager;

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
            if(weaponManager.LeftWeapon.AddModule(itemType))
                QueueFree();  // Remove the item from the scene, if AddModule was successful
        }
        else{
            GD.PrintErr("Item (" + this + ") just collided with " + body.GetType() + " (" + body + ") which is not a Player. This should not happen.");
        }
        if (((this.Position.Y + 20 > player.Position.Y) && (this.Position.Y - 20 < player.Position.Y)) && ((this.Position.X + 20 > player.Position.X) && (this.Position.X - 20 < player.Position.X)))  // Check if the player collided with the item
        {
            //GD.Print(itemType);
            if(Input.IsActionPressed("swapL"))
            {
                weaponManager.LeftWeapon.AddModule(itemType);
                QueueFree();
            }
            else if (Input.IsActionPressed("swapR"))
            {
                weaponManager.RightWeapon.AddModule(itemType);
                QueueFree();
            }
            //weaponManager.LeftWeapon.AddModule(itemType);
            // Emit the signal for pickup
            //QueueFree();  // Remove the item from the scene
        }
    }


    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        _on_Item_body_entered();

    }
}
