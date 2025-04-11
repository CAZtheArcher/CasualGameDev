using Godot;
using System;

public partial class MouseLook : Sprite2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		startRot = Rotation;
	}
	
	private float startRot = 0f;
	private float minPlayerRotate = (Mathf.Pi/180) * -360;
	private float maxPlayerRotate = (Mathf.Pi/180) * 360;

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Vector2 CursorPos = GetLocalMousePosition();

		Rotation += CursorPos.Angle() + startRot;
		Rotation = Mathf.Wrap(Rotation, minPlayerRotate, maxPlayerRotate);
	}
}
