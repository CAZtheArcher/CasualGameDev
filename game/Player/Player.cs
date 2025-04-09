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

    public override void _Ready()
	{
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

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
		GetInput();
		MoveAndSlide();

        playerSprite.Rotation = GetAngleTo(GetGlobalMousePosition()) + offset;
    }
}