using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;

public partial class Player : CharacterBody2D
{
    float speed = 250;
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
		direction = input_direction;
		Velocity = input_direction * speed;
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