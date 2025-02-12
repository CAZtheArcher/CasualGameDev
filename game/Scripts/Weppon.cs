
using Godot;
using System;


public partial class Weppon : Node
{
    //im thinking of doing an item class inseted of an int but im runing out of time today
    PackedScene scene = GD.Load<PackedScene>("res://Projectile/Projectile.tscn");

   // public System.Windows.Input.MouseButtonState RightButton { get; }
    class item
    {

    }
    int[] weponSlots = new int[4];
    Projectile inst;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        weponSlots[0] = 1;
    }
    double fireRate = 1.0;
    // Called every frame. 'delta' is the elapsed time since the previous frame.
    //Vector2 cursorPos = GetLocalMousePosition();
    //Rotation += cursorPos.Angle() + InitRot;
    public override void _Process(double delta)
    {
        fireRate += delta;
        if (Input.IsMouseButtonPressed(MouseButton.Left) && (fireRate >= 1.0))
        {
            fireRate -= 1.0;
            WeponShot();
        }
        if (fireRate >= 1.5) { fireRate -= 1.0; }
    }
	//item nuber is like an id for the item 0 number is nothing there
	public void AddItem(int item)
	{
        int check = 0;
		for (int i = 0; i < weponSlots.Length; i++) 
		{
            if(weponSlots[i] == item)
            {

            }
			else if (weponSlots[i]== 0)
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
                Projectile inst = scene.Instantiate<Projectile>();
                inst.Init(1, 60, 1);
                AddChild(inst);
            }
            if (weponSlots[i] == 2)
            {
               //shot code gose here
            }
        }

    }
}
