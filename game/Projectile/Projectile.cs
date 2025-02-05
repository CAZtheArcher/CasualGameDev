using Godot;
using System;

public partial class Projectile : RigidBody2D
{
    [Export]
    int velocity;
    [Export]
    Vector2 directionOfTravel;
    [Export]
    int damage;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready(){

	}

	public void InitRadians(int velocity, int directionAsRadians, int damage) {
		this.velocity = velocity;
		this.directionOfTravel = 
            new Vector2(
                (float)Math.Cos(directionAsRadians), 
                (float)Math.Sin(directionAsRadians));
		this.damage = damage;
		this.Rotation = directionAsRadians;
    }

    public void Init(int velocity, int directionAsDegrees, int damage)
    {
        this.velocity = velocity;
        this.directionOfTravel = 
            new Vector2(
                (float)Math.Cos(Math.Round(directionAsDegrees * Math.PI / 180)), 
                (float)Math.Sin(Math.Round(directionAsDegrees * Math.PI / 180)));
        this.damage = damage;
        this.Rotation = (float)Math.Round(directionAsDegrees * Math.PI / 180);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta){
		// Move in the direction it was fired in
		this.Position = new Vector2(
			this.Position.X + directionOfTravel.X * velocity, 
			this.Position.Y + directionOfTravel.Y * velocity);
	}

    //public int Velocity { get; set; }
    //public Vector2 Direction { get; set; }
    //public int Damage { get; set; }
}
