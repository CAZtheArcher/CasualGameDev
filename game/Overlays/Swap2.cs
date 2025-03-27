using Godot;
using System;

public partial class Swap2 : Button
{
    private Control pause;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //pause = (Control)GetNode("/root/Main/Player/PlayerBody/SwapDrop");
        weapon = (Weapon)GetNode("/root/Main/Player/PlayerBody/PlayerSprite/WeaponSprite");
        this.Pressed += Clicked;
    }
    Weapon weapon;

    // Setup the collision signal
    private void Clicked()
    {
        weapon.butonSwap(1);
    }
}
