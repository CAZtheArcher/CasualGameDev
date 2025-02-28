using Godot;
using System;

public partial class item : RigidBody2D
{ 
    Weapon inst;
    [Export]
    int itemTipe;
    [Export]
    int itemLevel;

    Node2D player;


    Weapon weapon;

    // Setup the collision signal
    public override void _Ready()
    {
        weapon = (Weapon)GetNode("/root/Main/Player/PlayerBody/PlayerSprite/WeaponSprite");
        player = (Node2D)GetNode("/root/Main/Player/PlayerBody");


    }
    public void spawn(Vector2 posishon, int itemTipe)
    {
        this.Position = posishon;
        this.itemTipe = itemTipe;
    }
    //work in progres
    private void _on_Item_body_entered()
    {
        if (((this.Position.Y + 20 > player.Position.Y) && (this.Position.Y - 20 < player.Position.Y)) && ((this.Position.X + 20 > player.Position.X) && (this.Position.X - 20 < player.Position.X)))  // Check if the player collided with the item
        {
            GD.Print("works");
            // weapon.AddItem(itemTipe);
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
