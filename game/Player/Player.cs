using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;

public partial class Player : CharacterBody2D
{
    float speed = 300;
	int health = 5;
	public Vector2 direction = new Vector2();
    public override void _Ready()
	{
	}

	public void GetInput()
	{
		var input_direction = Input.GetVector("left", "right", "up", "down");
		direction = input_direction;
		Velocity = input_direction * speed;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
		GetInput();
		MoveAndSlide();
    }
}