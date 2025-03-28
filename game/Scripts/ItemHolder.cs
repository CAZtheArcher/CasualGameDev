using Godot;
using System;
using System.Collections.Generic;
using static System.Formats.Asn1.AsnWriter;

public partial class ItemHolder : Node
{
    // Called when the node enters the scene tree for the first time.
    List<Item> item = new List<Item>();
    PackedScene scene = GD.Load<PackedScene>("res://Item/Item.tscn");
    public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
    {
    }

    public void AddItem(Module Mod, Vector2 Position)
	{
        item.Add(scene.Instantiate<Item>());
        item[item.Count - 1].spawn(Position, Mod);
        AddChild(item[item.Count - 1]);
    }
}
