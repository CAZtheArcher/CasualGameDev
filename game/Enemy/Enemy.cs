using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;

public partial class Enemy : RigidBody2D
{
    protected int damage;
    [Export] protected float velocity; // This is the max speed of the enemy
    protected int radius;

    protected Random random;
    protected Player player;
    protected PlayerUiManager UIManager;
    protected PackedScene scene;
    protected List<Item> item;

    protected Vector2 direction;
    // Called when the node enters the scene tree for the first time.

    private int health = 10;
    public override void _Ready(){
        // These two make collision work.
        ContactMonitor = true;
        MaxContactsReported = 999;

        damage = 1;
        velocity = 100f;
        radius = 400;

        random = new Random();
        player = (Player)GetNode("/root/Main/Player/PlayerBody");
        UIManager = (PlayerUiManager)GetNode("/root/Main/Player/PlayerBody/PlayerUi");
        scene = GD.Load<PackedScene>("res://Item/Item.tscn");
        item = new List<Item>();

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
        //GetTree().Root.AddChild(item[item.Count - 1]);
    }

    public override void _PhysicsProcess(double delta)
    {
        direction = (player.GlobalPosition - GlobalPosition).Normalized();
        //Rotation = 0; // Prevents collsions with the player from spinning the enemy

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
        Knockback(800);
        player.TakeDamage(damage);
        UIManager.DecrementHealth(damage);
        GD.Print("Collided with the player");
    }

    public void DecreaseHealth(int val){
        health -= val;
        GD.Print("DMG dealt" + health);
        if (health <= 0){
            EnemyDie();
        }
    }

    public void EnemyDie()
    {
        UIManager.IncrementKills();
        if ((float)(random.Next(3)) == 0)
        {
            item.Add(scene.Instantiate<Item>());
            item[item.Count - 1].spawn(this.Position, new BuckshotModule());
            GetTree().Root.CallDeferred("add_child", item[item.Count - 1]);
            GD.Print("Item spawned");
        }
        this.QueueFree();
    }

    public virtual void Knockback(int knockbackAmount = 10)
    {
        LinearVelocity = Vector2.Zero;
        direction = (GlobalPosition - player.GlobalPosition).Normalized();
        ApplyCentralForce(direction * knockbackAmount);
        GD.Print("Knockback applied in " + direction);
    }

    /*public void Spawn(Type eT)
    {
        this.enemyType = eT;
    }*/
}