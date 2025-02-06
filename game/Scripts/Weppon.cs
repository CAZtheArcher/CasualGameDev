using Godot;
using System;

public partial class Weppon : Node
{
    //im thinking of doing an item class inseted of an int but im runing out of time today
    PackedScene scene = GD.Load<PackedScene>("res://Projectile/Projectile.tscn");
    class item
    {

    }
    int[] weponSlots = new int[4];
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        Projectile inst = scene.Instantiate<Projectile>();
        inst.Init(1 , 90, 1);
        inst.GlobalPosition = new Vector2(10, 10);
        AddChild(inst);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	//item nuber is like an id for the item 0 number is nothing there
	public void AddItem(int item)
	{
        int check = 0;
		for (int i = 0; i < weponSlots.Length; i++) 
		{
			if (weponSlots[i]== 0)
			{
				weponSlots[i] = item;

                return;
			}
            else 
            {
                check++;            
            }
		}
        if (check == weponSlots.Length)
        { 
        //promt drop or swap
        }
	}
    //
    public void slotExspand()
	{
		Array.Resize(ref weponSlots, weponSlots.Length + 1);
    }
    //
    public void slotSrink()
    {
        Array.Resize(ref weponSlots, weponSlots.Length - 1);
    }
    //sdff
    public void WeponShot()
	{
        for (int i = 0; i < weponSlots.Length; i++)
        {
            if (weponSlots[i] == 1)
            {
              //shot code gose here
            }
            if (weponSlots[i] == 2)
            {
               //shot code gose here
            }
        }

    }
}
