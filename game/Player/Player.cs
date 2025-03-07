using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;

public partial class Player : CharacterBody2D
{
    float speed = 400;
	int health = 5;
	
    public override void _Ready()
	{
	}

	public void GetInput()
	{
		var input_direction = Input.GetVector("left", "right", "up", "down");
		Velocity = input_direction * speed;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
		GetInput();
		MoveAndSlide();
    }

	public void TakeDamage(int damage)
	{
		health -= damage;
		if (health <= 0)
		{
			Death();
		}
	}

	public void Death()
	{
		Debug.WriteLine("YOU DIED");
	}
}