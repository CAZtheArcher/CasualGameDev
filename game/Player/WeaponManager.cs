using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class WeaponManager : Node
{
	private List<Sprite2D> weapons;
	private int equippedLeftWeaponIndex;
	private int equippedRightWeaponIndex;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		foreach(Node weapon in GetChildren()) weapons.Add((Sprite2D)weapon);

    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
