using Godot;
using System;
using System.Collections.Generic;
using System.Reflection;

public partial class Enemy : RigidBody2D
{
    int damage = 1;
    [Export] float velocity; // This is the max speed of the enemy
    float acceleration; // This is the change in speed
    float secondsToReachMaxSpeed;
    int radius = 400;
    Random random = new Random();
    Node2D player;
    Control UIManager;
    Vector2 direction;

    PackedScene scene = GD.Load<PackedScene>("res://item/items.tscn");
    List<item> item = new List<item>();
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        player = (Node2D)GetNode("/root/Main/Player/PlayerBody");
        UIManager = (Control)GetNode("/root/Main/Player/PlayerBody/PlayerUi");
        // Calculating spawn position (temp use of direction to determine it)
        direction = new Vector2((float)(random.NextDouble() * 2) - 1, 0);
        direction.Y = (float)(random.NextDouble() * 2) - 1;
        direction = direction.Normalized();
        Position = player.Position + (direction * radius);
        direction = Vector2.Zero;
        CollisionMask = 1;
        CollisionLayer = 2;

        velocity = 50f;
        secondsToReachMaxSpeed = 0.5f;
        acceleration = velocity / secondsToReachMaxSpeed;
    }


    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {

        

        //GetTree().Root.AddChild(item[item.Count - 1]);
    }

    public override void _PhysicsProcess(double delta)
    {
        direction = (player.GlobalPosition - GlobalPosition).Normalized();
        if (random.NextDouble() <= 0.15)
        {
            direction = new Vector2((float)(random.NextDouble() * 2) - 1, 0);
            direction.Y = (float)(random.NextDouble() * 2) - 1;
            direction = direction.Normalized();
            //Position += (2 * direction);
            ApplyCentralForce(2 * direction);
        }
        else
        {
            //Position += (speed * direction);
            ApplyCentralForce(velocity * direction);
            //ApplyTorque(direction);// PATRICK TODO: Convert this to ana ctual direction (its a vector)
        }
            

        // Prevents the enemy from infinitely accelerating.
        if(Mathf.Abs(LinearVelocity.X) > velocity)
        {
            Vector2 clampedVelocity = LinearVelocity.Normalized() * velocity;
            LinearVelocity = clampedVelocity;
        } 


        var collisionInfo = MoveAndCollide(Vector2.Zero, true);
        //GD.Print(direction);
        if (collisionInfo != null)
        {
            GD.Print("Enemy collided with " + collisionInfo.GetCollider());
            UIManager.Call("DecrimentHealth", 5);
            this.QueueFree();
            if (collisionInfo.GetCollider().Equals("CharacterBody2D"))
            {
                GD.Print("Enemy collided with the player");
                //Knockback();
            }
        }
    }

    public void EnemyDie()
    {
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
        direction = (Position - player.Position).Normalized();
        //Position += (knockbackAmount * direction);
        ApplyCentralForce(direction * knockbackAmount * 200);
        await ToSignal(GetTree().CreateTimer(1f/3f), "timeout");
        ApplyCentralForce(-direction * knockbackAmount/3);
        await ToSignal(GetTree().CreateTimer(1f / 3f), "timeout");
        ApplyCentralForce(-direction * knockbackAmount/3);
    }


    /*public void Spawn(Type eT)
    {
        this.enemyType = eT;
    }*/
}