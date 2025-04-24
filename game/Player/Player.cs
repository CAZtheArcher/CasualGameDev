using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;

public partial class Player : CharacterBody2D
{
    float speed = 300;
	int health = 5;
    public float offset = 90 * (Mathf.Pi / 180);
    public Vector2 direction = new Vector2();
    private float minRotRadians;
    private float maxRotRadians;
    private float InitRot = 0f;
    private Sprite2D playerSprite;
    bool isPawsd ;
    Control pause;
    public override void _Ready()
	{
        isPawsd = false;
        pause = (Control)GetNode("/root/Main/Player/PlayerBody/pause");
        GetTree().Paused = false;
        pause.Hide();
        minRotRadians = (Mathf.Pi / 180) * -360;
        maxRotRadians = (Mathf.Pi / 180) * 360;
        playerSprite = (Sprite2D)GetNode("PlayerSprite");
    }

	public void GetInput()
	{
		var input_direction = Input.GetVector("left", "right", "up", "down");
        if(this.Position.X <= -2313.0)
        {
            Vector2 thing = new Vector2(-2310, Position.Y);
            
            GD.PrintErr("dos work?");
            Position = thing;
            direction = input_direction;
            Velocity = input_direction * speed;

            return;
        }
        else if (this.Position.Y <= -1536.0)
        {
            GD.PrintErr("dos work?");
            Vector2 thing = new Vector2(Position.X, -1530);
            Position = thing;
            direction = input_direction;
            Velocity = input_direction * speed;

            return;
        }
        else
        {
            direction = input_direction;
            Velocity = input_direction * speed;
        }

        if(this.Position.Y >= 2206.0)
        {
            GD.PrintErr("dos work?");
            Vector2 thing = new Vector2(Position.X, 2200);
            Position = thing;
            direction = input_direction;
            Velocity = input_direction * speed;
            return;
        }

        else if(this.Position.X >= 1249.0)
        {
            GD.PrintErr("dos work?");
            Vector2 thing = new Vector2(1244, Position.Y);
            Position = thing;
            direction = input_direction;
            Velocity = input_direction * speed;
            return;
        }
        else
        {
            direction = input_direction;
            Velocity = input_direction * speed;
        }



    }
    public void pouse()
    {
        if(isPawsd == true && Input.IsActionPressed("pause"))
        {
            GetTree().Paused = false;
            pause.Hide();
            isPawsd = false;
        }
        else if(isPawsd == false && Input.IsActionPressed("pause"))
        {
            GetTree().Paused = true;
            pause.Show();
            isPawsd = true;
        }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
		GetInput();
		MoveAndSlide();
        pouse();
        playerSprite.Rotation = GetAngleTo(GetGlobalMousePosition()) + offset;
    }
}