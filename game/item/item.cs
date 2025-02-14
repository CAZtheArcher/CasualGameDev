using Godot;
using System;

public partial class item : RigidBody2D
{
    Weppon inst;
    [Export]
    int itemTipe;
    [Export]
    int itemLevel;

    Node2D player ;


    Weppon wepon;
    

    // Setup the collision signal
    public override void _Ready()
    {
        wepon  = (Weppon)GetNode("../InstantBulletTest");
        player = (Node2D)GetNode("../Player/PlayerBody");
        

    }
    //work in progres
    private void _on_Item_body_entered()
    {
        if (this.Position == player.Position)  // Check if the player collided with the item
        {
            GD.Print("works");
            wepon.AddItem(itemTipe);
            // Emit the signal for pickup
            QueueFree();  // Remove the item from the scene
        }
    }


// Called every frame. 'delta' is the elapsed time since the previous frame.
public override void _Process(double delta)
	{
	}
}
