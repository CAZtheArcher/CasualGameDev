using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class WeaponManager : Node
{
	private List<Weapon> weapons;
	private int leftWeaponIndex;
	private int rightWeaponIndex;
    public Weapon LeftWeapon { get => weapons[leftWeaponIndex]; }
    public Weapon RightWeapon { get => weapons[rightWeaponIndex]; }
    private PackedScene weaponScene;

    /// <summary> playerUI for bullets to display </summary>
    private PlayerUiManager UIManager;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready(){
        leftWeaponIndex = -1;
        rightWeaponIndex = -1;
        weapons = new List<Weapon>();
        weaponScene = GD.Load<PackedScene>("res://Player/Weapon/Weapon.tscn");
        UIManager = (PlayerUiManager)GetNode("/root/Main/Player/PlayerBody/PlayerUi");

        foreach (Node child in GetChildren()){
            if (child is Weapon sprite)
                AddWeapon(sprite);
        }
        weapons[0].AddModule(new BuckshotModule());
        Weapon w2 = (Weapon)weaponScene.Instantiate();
        AddWeapon(w2);
        w2.RemoveModule();
        w2.AddModule(new SlugModule());

        String[] spritePaths = LeftWeapon.GetNextModuleIcons();
        UIManager.CallDeferred(nameof(UIManager.UpdateBulletSprite), spritePaths[0], spritePaths[1], spritePaths[2], false);
        spritePaths = RightWeapon.GetNextModuleIcons();
        UIManager.CallDeferred(nameof(UIManager.UpdateBulletSprite), spritePaths[0], spritePaths[1], spritePaths[2], true);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta){
        if (leftWeaponIndex >= 0) // If there is a weapon in the left hand
        {
            if (Input.IsActionPressed("fire") && weapons[leftWeaponIndex].CanFire())
            {
                //GD.Print("LEFT weapon fired." + LeftWeapon + leftWeaponIndex);
                weapons[leftWeaponIndex].ActivateNextModule();
                String[] spritePaths = LeftWeapon.GetNextModuleIcons();
                UIManager.UpdateBulletSprite(spritePaths[0], spritePaths[1], spritePaths[2], false);
            }
        }
        if (rightWeaponIndex >= 0) // If there is a weapon in the right hand
        {
            if (Input.IsActionPressed("altFire") && weapons[rightWeaponIndex].CanFire())
            {
                //GD.Print("Right weapon fired." + RightWeapon + rightWeaponIndex);
                weapons[rightWeaponIndex].ActivateNextModule();
                String[] spritePaths = RightWeapon.GetNextModuleIcons();
                UIManager.UpdateBulletSprite(spritePaths[0], spritePaths[1], spritePaths[2], true);
            }
        }
    }

    /// <summary>
    /// Adds a Weapon to WeaponManager's list of weapons. 
    /// <para>If this is the only weapon in the list, binds it to LMB</para>
    /// <para>If this is the second weapon in the list, binds it to RMB</para>
    /// </summary>
    public void AddWeapon(Weapon weapon){
        weapons.Add(weapon);
        AddChild(weapon, true);
        if (weapons.Count == 1) { 
            leftWeaponIndex = 0; 
        }
        if (weapons.Count == 2) { 
            rightWeaponIndex = 1; 
        }
    }

    /// <returns>True if weapon is the left weapon</returns>
    public bool IsWeaponInLeftHand(Weapon weapon) {
        return weapon == weapons[leftWeaponIndex]; 
    }
}
