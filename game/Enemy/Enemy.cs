using Godot;
using System;
using System.Collections.Generic;
using System.Reflection;

public partial class Enemy : RigidBody2D
{
    int damage = 1;
    [Export] float speed = 1f;
    int radius = 200;
    Random random = new Random();
    Node2D player;
    Vector2 direction;

    PackedScene scene = GD.Load<PackedScene>("res://item/items.tscn");
    List<item> item = new List<item>();
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        player = (Node2D)GetNode("/root/Main/Player/PlayerBody");
        // Calculating spawn position (temp use of direction to determine it)
        direction = new Vector2(random.Next(2) - 1, 0);
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
            // randomly spawn item on death
            if ((float)(random.Next(0)) == 0)
            {
            item.Add(scene.Instantiate<item>());
            item[item.Count - 1].spawn(new Vector2(200, 200), 1);
            AddChild(item[item.Count - 1]);
            }

            GD.Print("man down");
            this.QueueFree();
        }
    }
}