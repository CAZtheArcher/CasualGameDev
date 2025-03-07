using Godot;
using System;
using System.Collections.Generic;
using static System.Formats.Asn1.AsnWriter;

public partial class itmeHolder : Node
{
    // Called when the node enters the scene tree for the first time.
    List<item> item = new List<item>();
    PackedScene scene = GD.Load<PackedScene>("res://item/items.tscn");
    public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
    {
    }
    public void addItem(Module Mod, Vector2 Position)
	{
        item.Add(scene.Instantiate<item>());
        item[item.Count - 1].spawn(Position, Mod);
        AddChild(item[item.Count - 1]);
    }
}
