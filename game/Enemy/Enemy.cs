using Godot;
using System;
using System.Reflection;

public partial class Enemy : RigidBody2D
{
    int damage = 1;
    [Export] float speed = 1f;
    Vector2 direction;
    int radius = 200;
    Random random = new Random();
    Node2D player;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        player = (Node2D)GetNode("../Player/PlayerBody");
        // Calculating spawn position (temp use of direction to determine it)
        direction = new Vector2((float)(random.NextDouble() * 2) - 1, 0);
        direction.Y = (float)(random.NextDouble() * 2) - 1;
        direction = direction.Normalized();
        Position = player.Position + (direction * radius);

        direction = Vector2.Zero;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        direction = (player.Position - Position).Normalized();
        Position += (speed * direction);
    }
    
    public override void _PhysicsProcess(double delta)
    {
        var collisionInfo = MoveAndCollide(Vector2.Zero, true);
        //GD.Print(direction);
        if (collisionInfo != null)
        {
            GD.Print("man down");
            this.QueueFree();
        }
    }
}