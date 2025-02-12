using Godot;
using System;

public partial class item : RigidBody2D
{
    Weppon inst;
    [Export]
    int itemTipe;
    [Export]
    int itemLevel;
    

    PackedScene scene = GD.Load<PackedScene>("res://Scripts/Weppon.tscn");
    

    // Setup the collision signal
    public override void _Ready()
    {

    }
    //work in progres
    private void _on_Item_body_entered(Node body)
    {
        if (body == body)  // Check if the player collided with the item
        {
            inst = scene.Instantiate<Weppon>();
            inst.AddItem(itemTipe);
            // Emit the signal for pickup
            QueueFree();  // Remove the item from the scene
        }
    }


// Called every frame. 'delta' is the elapsed time since the previous frame.
public override void _Process(double delta)
	{
	}
}
