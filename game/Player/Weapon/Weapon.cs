
using Godot;
using System;
using System.Collections.Generic;
using static System.Formats.Asn1.AsnWriter;

public partial class Weapon : Sprite2D
{
    /// <summary> Array that holds this weapon's modules. This array's length is the max number of modules that this weapon can hold. </summary>
    private Module[] weaponModules;
    /// <summary> The number of modules *currently* slotted into the weapon. </summary>
    private short weaponModulesSize;
    /// <summary> The module that will be activated the next time ActivateNextModule() is called. </summary>
    private short currentModule;

    /// <summary> The Marker2D in the player scene that bullets spawn at </summary>
    private Marker2D bulletSpawn;
    public Marker2D BulletSpawn { get => bulletSpawn; }
    /// <summary> The player sprite. Used to get player rotation. </summary>
    private Sprite2D playerSprite;
    public Sprite2D PlayerSprite { get => playerSprite; }

    /// <summary> The seconds that must pass before another module can be activated. </summary>
    private double fireRate;
    /// <summary> The seconds that have passed since a module was last activated. </summary>
    private double timeSinceLastShot;


    Control pause;
    private WeaponManager weaponManager;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        pause = (Control)GetNode("/root/Main/Player/PlayerBody/SwapDrop");
        pause.Hide();
        weaponManager = (WeaponManager)GetNode("../");
        bulletSpawn = (Marker2D)GetChild(0);
        playerSprite = (Sprite2D)GetNode("/root/Main/Player/PlayerBody/PlayerSprite");
        fireRate = 0.433f; // 8 per second
        timeSinceLastShot = fireRate; // Can fire immediately upon spawning.
        weaponModules = new Module[1]; // Weapon can hold a default 4 modules.
        weaponModulesSize = 0; // There is a single BasicBulletModule slotted into the weapon.
        currentModule = 0; // Weapon fires the module in slot 1 (index 0) first.
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        timeSinceLastShot += delta;
    }

    /// <summary> Returns true if timeSinceLastShot > fireRate </summary>
    public bool CanFire(){return timeSinceLastShot >= fireRate;}

    /// <summary> Activates the next Module in the weapon. </summary>
    public void ActivateNextModule()
    {
        weaponModules[currentModule].Activate();
        currentModule++;
        // If there are no modules after this one, loop back to the start.
        if (currentModule == weaponModulesSize) { currentModule = 0; }
        timeSinceLastShot = 0;
    }

    /// <summary> Adds 'module' to the weapon, at the first available empty spot.
    /// <para>Also increments weaponModulesSize and adds the module as a child.</para></summary>
    /// <param name="module">The module that will be added.</param>
    /// <returns>True, if the module was added.  False if it was not added.</returns>
    public bool AddModule(Module module)
	{
        RemoveModule();
        weaponModules[0] = module;
        AddChild(weaponModules[0]);
        weaponModulesSize++;
        //GD.PrintErr("Weapon.AddModule - Weapon is at module capacity, nothing was changed.");
        return false;
	}

    /// <summary> Removes the module closest to the end of weaponModules.
    /// <para>Also increments weaponModulesSize and adds the module as a child.</para></summary>
    /// <param name="module">The module that will be added.</param>
    
    public void RemoveModule(int num = -1)
    {
        if(num == -1)
        {
            for (int i = weaponModules.Length - 1; i >= 0; i--) {
                if (weaponModules[i] != null)
                {
                    Module justRemoved = weaponModules[i];
                    weaponModules[i] = null;
                    RemoveChild(justRemoved);
                    weaponModulesSize--;
                    return;
                }
            }
        }
        //GD.PrintErr("Weapon.AddModule - Weapon is at module capacity, nothing was changed.");
    }

    public void SlotExpand() { Array.Resize(ref weaponModules, weaponModules.Length + 1); }
    public void SlotShrink() { Array.Resize(ref weaponModules, weaponModules.Length - 1); }

    public String GetNextModuleIcons()
    {
        string sprite;
        int accessModule = 0;
        sprite = weaponModules[accessModule].SpritePath;
        switch(sprite){
            case "res://Projectile/bullet.png":
            sprite = "res://Overlays/Pistol(BasicBullet).png";
                break;
            case "res://Projectile/Slug/Slug.png":
            sprite = "res://Overlays/RiotGrenade(Slug).png";
                break;
            case "res://Projectile/Helix/Helix.png":
            sprite = "res://Overlays/HelixGun.png";
                break;
            case "res://Projectile/Buckshot/Buckshot.png":
            sprite = "res://Overlays/Shotgun(Pellets).png";
                break;
            default:
                break;
        }
        return sprite;
    }
    public void butonSwap(int num, Module mod)
    {
        RemoveModule(num);
        AddModule(mod);
    }
    public void unpase()
    {
        pause.Hide();
        GetTree().Paused = false;
    }



}
