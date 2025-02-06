using Godot;
using System;

public partial class LookAtMouse : Sprite2D
{
	// Called when the node enters the scene tree for the first time.
	private float InitRot = 0f;
	public override void _Ready()
	{
		InitRot = Rotation;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	private float minRotRadians = (Mathf.Pi / 180) * -360;
	private float maxRotRadians = (Mathf.Pi / 180) * 360;
	public override void _Process(double delta)
	{
		Vector2 cursorPos = GetLocalMousePosition();

		Rotation += cursorPos.Angle() + InitRot;
		Rotation = Mathf.Wrap(Rotation, minRotRadians, maxRotRadians);
	}
}
