using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class WeaponManager : Node
{
    /// <summary> The player's arsenal of weapons.  Holds both equipped, and unequipped weapons. </summary>
	private List<Weapon> weapons;
    /// <summary> The `weapons` index that contains the left hand weapon </summary>
	private int leftWeaponIndex;
    /// <summary> The `weapons` index that contains the right hand weapon </summary>
    private int rightWeaponIndex;

    /// <summary> A reference to the weapon in the left hand. </summary>
    public Weapon LeftWeapon { get => weapons[leftWeaponIndex]; }
    /// <summary> A reference to the weapon in the right hand. </summary>
    public Weapon RightWeapon { get => weapons[rightWeaponIndex]; }
    /// <summary> A reference to the weapon scene, to be instantiated whenever a weapon needs to be created. </summary>
    private PackedScene weaponScene;

    /// <summary> Used to update the 'next bullet indicator' in the bottom right corner of the UI </summary>
    private PlayerUiManager UIManager;

    public override void _Ready(){
        leftWeaponIndex = -1;
        rightWeaponIndex = -1;
        weapons = new List<Weapon>();
        weaponScene = GD.Load<PackedScene>("res://Player/Weapon/Weapon.tscn");
        UIManager = (PlayerUiManager)GetNode("/root/Main/Player/PlayerBody/PlayerUi");

        foreach (Node child in GetChildren()){
            if (child is Weapon sprite)
                InitWeapon(sprite);
        }
        weapons[0].AddModule(new BasicBulletModule());
        Weapon w2 = (Weapon)weaponScene.Instantiate();
        AddWeapon(w2);
        w2.RemoveModule();
        w2.AddModule(new OneShotModule());

        String[] spritePaths = LeftWeapon.GetNextModuleIcons();
        UIManager.CallDeferred(nameof(UIManager.UpdateBulletSprite), spritePaths[0], spritePaths[1], spritePaths[2], false);
        spritePaths = RightWeapon.GetNextModuleIcons();
        UIManager.CallDeferred(nameof(UIManager.UpdateBulletSprite), spritePaths[0], spritePaths[1], spritePaths[2], true);
    }

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

    /// <summary>
    /// Adds an existing Weapon to WeaponManager's list of weapons without causing an error. 
    /// <para>If this is the only weapon in the list, binds it to LMB</para>
    /// <para>If this is the second weapon in the list, binds it to RMB</para>
    /// </summary>
    public void InitWeapon(Weapon weapon){
        weapons.Add(weapon);
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
