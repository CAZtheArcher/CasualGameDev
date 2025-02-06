using Godot;
using System;

public partial class Enemy : Node
{
    //CollisionObject2D player = get_node("Node2D/CharacterBody2D/CollisionShape2D");
    int damage = 1;
    int speed = 2;
    Vector2 position;
    Vector2 direction = new Vector2(0, 0);
    int radius = 10;
    Random random = new Random();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }

    Enemy()
    {
        // Calculating spawn position
        direction = new Vector2((float)(random.NextDouble() * 2) - 1, 0);
        direction.Y = (float)(random.NextDouble() * 2) - 1;
        direction = direction.Normalized();
        //position = player.GlobalPosition + (direction * radius);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        //direction = (position - player.GlobalPosition).Normalized();
        this.position += speed * direction;
    }
}