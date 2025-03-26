using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

public partial class Enemy : RigidBody2D
{
    int damage = 1;
    [Export] float velocity; // This is the max speed of the enemy
    int radius = 400;
    Random random = new Random();
    Player player;
    PlayerUiManager UIManager;
    Vector2 direction;
    EnemyManager man;

    PackedScene scene = GD.Load<PackedScene>("res://item/items.tscn");
    List<item> item = new List<item>();
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        man = (EnemyManager)GetNode("/root/Main/enemyManager");
        ContactMonitor = true;
        MaxContactsReported = 999;
        player = (Player)GetNode("/root/Main/Player/PlayerBody");
        UIManager = (PlayerUiManager)GetNode("/root/Main/Player/PlayerBody/PlayerUi");
        // Calculating spawn position (temp use of direction to determine it)
        direction = new Vector2((float)(random.NextDouble() * 2) - 1, 0);
        direction.Y = (float)(random.NextDouble() * 2) - 1;
        direction = direction.Normalized();
        Position = player.Position + (direction * radius);
        direction = Vector2.Zero;
        CollisionMask = 1;
        CollisionLayer = 2;

        velocity = 100f;
    }


    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        //GetTree().Root.AddChild(item[item.Count - 1]);
    }

    public override void _PhysicsProcess(double delta)
    {
        direction = (player.GlobalPosition - GlobalPosition).Normalized();

        // I hate physics, this is probably my 6th implementation of getting
        // the enemy to move with physics.
        // This code finds the desired direction it wants to travel, the
        // difference between that and its current direction, and then
        // moves towards its desired direction (limtied by velocity)
        Vector2 desired_velocity = direction * velocity;
        Vector2 velocity_diff = desired_velocity - LinearVelocity;
        Vector2 force = velocity_diff.LimitLength(velocity);
        ApplyCentralForce(force);

    }

    public void CollisionDetected(Node body)
    {
        Knockback(500);
        player.TakeDamage(damage);
        UIManager.DecrementHealth(damage);
        GD.Print("Collided with the player");
    }

    public void EnemyDie()
    {
        UIManager.IncrementKills();
        if ((float)(random.Next(3)) == 0)
        {
            item.Add(scene.Instantiate<item>());
            item[item.Count - 1].spawn(this.Position, new BuckshotModule());
            GetTree().Root.CallDeferred("add_child", item[item.Count - 1]);
            GD.Print("Item spawned");
        }
        this.QueueFree();
    }

    //PATRICK TODO: Make this use RigidBody's physics magic, so the enemy doesn't just teleport backwards lmao
    public async void Knockback(int knockbackAmount = 10)
    {
        direction = (GlobalPosition - player.GlobalPosition).Normalized();
        //Position += (knockbackAmount * direction);
        ApplyCentralForce(direction * knockbackAmount);

        //await ToSignal(GetTree().CreateTimer(1f/3f), "timeout");
        //ApplyCentralForce(-direction * knockbackAmount/3);
        //await ToSignal(GetTree().CreateTimer(1f / 3f), "timeout");
        //ApplyCentralForce(-direction * knockbackAmount/3);
    }


    /*public void Spawn(Type eT)
    {
        this.enemyType = eT;
    }*/
}