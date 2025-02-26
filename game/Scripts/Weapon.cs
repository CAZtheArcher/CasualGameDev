
using Godot;
using System;
using System.Collections.Generic;

public partial class Weapon : Node
{
    //im thinking of doing an item class inseted of an int but im runing out of time today
    PackedScene scene = GD.Load<PackedScene>("res://Projectile/Projectile.tscn");

    List<Projectile> projectiles = new List<Projectile>();

    double fireRate = 1.0;
    double shotReady = 1.0;
    int shotSpeed = 10;
    int weaponDmg = 1;

    // public System.Windows.Input.MouseButtonState RightButton { get; }
    class item
    {

    }
    int[] weaponSlots = new int[4];
    Projectile inst;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        weaponSlots[0] = 1;
    }
    // Called every frame. 'delta' is the elapsed time since the previous frame.
    //Vector2 cursorPos = GetLocalMousePosition();
    //Rotation += cursorPos.Angle() + InitRot;
    public override void _Process(double delta)
    {
        shotReady += delta;
        //if (shotReady >= fireRate) { shotReady = fireRate; }
        if (Input.IsMouseButtonPressed(MouseButton.Left) && (shotReady >= fireRate))
        {
            shotReady = 0;
            WeaponShot();
        }
    }
	//item number is like an id for the item, 0 is null
	public void AddItem(int item)
	{
        int check = 0;
		for (int i = 0; i < weaponSlots.Length; i++) 
		{
            if(weaponSlots[i] == item)
            {

            }
			else if (weaponSlots[i]== 0)
			{
				weaponSlots[i] = item;

                return;
			}
            else 
            {
                check++;            
            }
		}
        if (check == weaponSlots.Length)
        { 
        //prompt drop or swap
        }
	}
    //
    public void slotExspand()
	{
		Array.Resize(ref weaponSlots, weaponSlots.Length + 1);
    }
    //
    public void slotSrink()
    {
        Array.Resize(ref weaponSlots, weaponSlots.Length - 1);
    }
    //sdff
    public void WeaponShot()
	{
        for (int i = 0; i < weaponSlots.Length; i++)
        {
            if (weaponSlots[i] == 1)
            {
                projectiles.Add(scene.Instantiate<Projectile>());
                projectiles[projectiles.Count - 1].Init(shotSpeed, weaponDmg);
                AddChild(projectiles[projectiles.Count - 1]);
            }
            if (weaponSlots[i] == 2)
            {
                projectiles.Add(scene.Instantiate<Projectile>());
                projectiles[projectiles.Count - 1].Init(shotSpeed, weaponDmg);
                AddChild(projectiles[projectiles.Count - 1]);
            }
        }

    }
}
