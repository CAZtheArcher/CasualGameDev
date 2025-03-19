using Godot;
using System;
using System.Collections.Generic;
using System.Reflection;

public partial class Enemy : RigidBody2D
{
    int damage = 1;
    [Export] float speed = 1f;
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
    }


    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        direction = (player.GlobalPosition - GlobalPosition).Normalized();
        if (random.NextDouble() <= 0.15)
        {
            direction = new Vector2((float)(random.NextDouble() * 2) - 1, 0);
            direction.Y = (float)(random.NextDouble() * 2) - 1;
            direction = direction.Normalized();
            Position += (2 * direction);
        }
        else
            Position += (speed * direction);
        

        //GetTree().Root.AddChild(item[item.Count - 1]);
    }

    public override void _PhysicsProcess(double delta)
    {
        var collisionInfo = MoveAndCollide(Vector2.Zero, true);
        GD.Print(direction);
        if (collisionInfo != null)
        {
            GD.Print("man down");
            UIManager.Call("DecrimentHealth", 5);
            this.QueueFree();
            GD.Print("collision detected with " + collisionInfo.GetCollider());
            if (collisionInfo.GetCollider().Equals("CharacterBody2D"))
            {
                GD.Print("it's the player");
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


    public void Knockback(int knockbackAmount = 10)
    {
        direction = (Position - player.Position).Normalized();
        Position += (knockbackAmount * direction);
    }


    /*public void Spawn(Type eT)
    {
        this.enemyType = eT;
    }*/
}