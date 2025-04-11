using Godot;
using System;

public partial class Drop : Button
{
    // Called when the node enters the scene tree for the first time.
    private Control pause;
    private Control otherpause;
    Module mod;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //pause = (Control)GetNode("/root/Main/Player/PlayerBody/SwapDrop");
        weapon = (Weapon)GetNode("/root/Main/Player/PlayerBody/PlayerSprite/WeaponManager/Weapon");
        pause = (Control)GetNode("/root/Main/Player/PlayerBody/SwapDrop");
        otherpause = (Control)GetNode("/root/Main/Player/PlayerBody/pause");
        this.Pressed += Clicked;
        
    }
    Weapon weapon;

    // Setup the collision signal
    private void Clicked()
    {
        //weapon.butonSwap(1, mod);
        GetTree().Paused = false;
        pause.Hide();
        otherpause.Hide();
    }
}
