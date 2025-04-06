using Godot;
using System;

/// <summary>
/// A Module is a "thing" that can be slotted into a weapon.
/// Modules are activated one at a time, one per time the weapon is fired.
/// </summary>
public abstract partial class Module : Node
{
    /// <summary>
    /// What happens when this module is activated.
    /// <para>Some examples of what this method is designed for include:</para>
    /// <para>    -Firing one projectile towards the cursor (e.g. a pistol shot)</para>
    /// <para>    -Firing three projectiles sequentially towards the cursor (e.g. a rifle burst)</para>
    /// <para>    -Firing multiple projectiles in a cone (e.g. a shotgun blast)</para>
    /// <para>    -Telling the Weapon containing this module to fire the next two modules at the same time (e.g. "doubleshot")</para>
    /// </summary>
    public abstract void Activate();

    protected string spritePath;
    public string SpritePath{
        get { return spritePath; }
    }

    protected Weapon parent;
    public override void _Ready()
    {
        parent = GetParent<Weapon>();
    }


}
