using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;

public partial class Enemy : RigidBody2D
{
    /// <summary> The damage that this enemy does to the player upon collision. </summary>
    protected int damage;
    /// <summary> This is the max speed of the enemy.  Since this is a physics object with mass, this is *not* how many Godot measurement unit the enemey can travel every frame. </summary>
    [Export] protected float velocity; 
    /// <summary> The distance from the player, in any direction, that this enemy will spawn when it is instantiated by EnemyManager. </summary>
    protected int radius;

    /// <summary> Used to determine if an item should be dropped when this enemy dies. </summary>
    protected Random random;
    /// <summary> Reference to the player, used to allow the enemy to track and approach the player. </summary>
    protected Player player;
    /// <summary> Reference to UIManager, which is attached to player.  
    /// <para> This reference is used to decrease player health and track total enemy kills. </para></summary>
    protected PlayerUiManager UIManager;
    /// <summary> Reference to the Item scene, used when the enemy dies to instantiate an Item. </summary>
    protected PackedScene scene;
    /// <summary> The list of items that this enemy has already dropped. </summary>
    protected List<Item> item;
    /// <summary> The enemy's sprite. </summary>
    protected Sprite2D sprite;

    /// <summary> Vector pointing from the enemy to the player. </summary>
    protected Vector2 direction;

    /// <summary> The current health of the enemy.  Enemies die at 0 health. </summary>
    protected int health;
    /// <summary> Times how long an enemy has flashed red, after taking damage. </summary>
    protected double hitTimer;
    /// <summary> Controls how long an enemy will flash red upon taking damage. </summary>
    protected double totalHitTimer;
    /// <summary> Prevents the spawning of multiple items if the enemy is killed multiple times in a single frame. </summary>
    protected bool alreadyDied = false; 

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
        sprite = (Sprite2D)GetNode("EnemySprite");

        // Calculating spawn position (temp use of direction to determine it)
        direction = new Vector2((float)(random.NextDouble() * 2) - 1, 0);
        direction.Y = (float)(random.NextDouble() * 2) - 1;
        direction = direction.Normalized();
        Position = player.Position + (direction * radius);
        direction = Vector2.Zero;

        health = 10;
        hitTimer = 0.0;
        totalHitTimer = 0.12;
    }


    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        CheckHitTimer(delta);
    }

    public void CheckHitTimer(double delta)
    {
        if (hitTimer >= 0)
        {
            hitTimer -= delta;
            if (hitTimer <= 0.0)
            {
                sprite.Modulate = new Color(1, 1, 1, 1);
            }
        }
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
        UIManager.DecrementHealth(damage);
        //GD.Print("Collided with the player");
    }

    public void DecreaseHealth(int val){
        health -= val;
        hitTimer = totalHitTimer;
        sprite.Modulate = new Color(1, 0, 0, 1);
        //GD.Print("DMG dealt: " + health);
        if (health <= 0){
            EnemyDie();
        }
    }

    public void EnemyDie()
    {
        UIManager.CallDeferred("IncrementKills");
        // Prevents multiple dice rolls for an item drop, if multiple Projectiles kill this enemy in the same frame.
        if (!alreadyDied) {
            alreadyDied = true;
            if ((float)(random.Next(5)) == 0)
            {
                item.Add(scene.Instantiate<Item>());
                if ((float)(random.Next(2)) == 0)
                {
                    item[item.Count - 1].spawn(this.Position, new BuckshotModule());
                }
                else
                {
                    item[item.Count - 1].spawn(this.Position, new SlugModule());
                }
                GetTree().Root.CallDeferred("add_child", item[item.Count - 1]);
                //GD.Print("Item spawned");
            }
        }
        this.QueueFree();
    }

    public virtual void Knockback(int knockbackAmount = 10)
    {
        LinearVelocity = Vector2.Zero;
        direction = (GlobalPosition - player.GlobalPosition).Normalized();
        ApplyCentralForce(direction * knockbackAmount);
        //GD.Print("Knockback applied in " + direction);
    }

    /*public void Spawn(Type eT)
    {
        this.enemyType = eT;
    }*/
}