
using Godot;
using System;
using System.Collections.Generic;
using static System.Formats.Asn1.AsnWriter;

public partial class Weapon : Sprite2D
{
    /// <summary> The Marker2D in the player scene that bullets spawn at </summary>
    private Marker2D bulletSpawn;
    public Marker2D BulletSpawn { get => bulletSpawn; }
    /// <summary> The player sprite. Used to get player rotation. </summary>
    private Sprite2D playerSprite;
    public Sprite2D PlayerSprite { get => playerSprite; }

    /// <summary>The seconds that must pass before another module can be activated</summary>
    private double fireRate;
    private double timeSinceLastShot;

    private Module[] weaponModules;
    /// <summary> The number of modules *currently* slotted into the weapon.</summary>
    private short weaponModulesSize;
    private short currentModule;

    private WeaponManager weaponManager;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        weaponManager = (WeaponManager)GetNode("../");
        bulletSpawn = (Marker2D)GetChild(0);
        playerSprite = (Sprite2D)GetNode("/root/Main/Player/PlayerBody/PlayerSprite");
        fireRate = 1f / 8f; // 8 per second
        timeSinceLastShot = fireRate; // Can fire immediately upon spawning.
        weaponModules = new Module[4]; // Weapon can hold a default 4 modules.
        weaponModulesSize = 0; // There is a single BasicBulletModule slotted into the weapon.
        AddModule(new BasicBulletModule());// Weapon has one BasicBulletModule installed by default.
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
    public void AddModule(Module module)
	{
		for (int i = 0; i < weaponModules.Length; i++) 
		{
            if(weaponModules[i] == null)
            {
                weaponModules[i] = module;
                // Modules need to be added as children of Weapon to be able to add things to the scene.
                AddChild(weaponModules[i]);
                weaponModulesSize++;
                return;
            }
		}
        GD.PrintErr("Weapon.AddModule - Weapon is at module capacity, nothing was changed.");
	}

    /// <summary> Removes the module closest to the end of weaponModules.
    /// <para>Also increments weaponModulesSize and adds the module as a child.</para></summary>
    /// <param name="module">The module that will be added.</param>
    public void RemoveModule()
    {
        for (int i = weaponModules.Length - 1; i >= 0 ; i--)
        {
            if (weaponModules[i] != null)
            {
                Module justRemoved = weaponModules[i];
                weaponModules[i] = null;
                RemoveChild(justRemoved);
                weaponModulesSize--;
                return;
            }
        }
        GD.PrintErr("Weapon.AddModule - Weapon is at module capacity, nothing was changed.");
    }

    public void SlotExpand() { Array.Resize(ref weaponModules, weaponModules.Length + 1); }
    public void SlotShrink() { Array.Resize(ref weaponModules, weaponModules.Length - 1); }

    public String[] GetNextModuleIcons()
    {
        string[] sprites = { "", "", "" };
        int count = 0;
        for (int i = currentModule; i < currentModule + 3; i++)
        {
            int accessModule = i;
            while (accessModule >= weaponModulesSize) { accessModule -= weaponModulesSize; if (weaponModulesSize == 0) { accessModule = 0; break; } }
            sprites[count] = weaponModules[accessModule].SpritePath;
            count++;
        }
        return sprites;
    }

}
