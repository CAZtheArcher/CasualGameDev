using Godot;
using System;

public partial class PlayerUiManager : Control
{
    ProgressBar healthbar;
    TextureProgressBar ammobar;

    public override void _Ready()
    {
        healthbar = (ProgressBar)GetNode("HealthBar");
        ammobar = (TextureProgressBar)GetNode("AmmoCount");
        healthbar.Value = healthbar.MaxValue;
        ammobar.Value = ammobar.MaxValue;
    }

    public void IncrimentHealth(int value)
    {
        healthbar.Value += value;
    }

    public void DecrimentHealth(int value)
    {
        healthbar.Value -= value;
    }

    public void IncrimentMaxHealth(int value)
    {
        healthbar.MaxValue += value;
        healthbar.Value = healthbar.MaxValue;
    }

    public void DecrimentMaxHealth(int value)
    {
        healthbar.MaxValue -= value;
        healthbar.Value = healthbar.MaxValue;
    }

    public void IncrementAmmo(int value)
    {
        ammobar.Value += value;
    }

    public void DecrementAmmo(int value)
    {
        ammobar.Value -= value;
    }

    public void IncrementMaxAmmo(int value)
    {
        ammobar.MaxValue += value;
        ammobar.Value = ammobar.MaxValue;
    }

    public void DecrementMaxAmmo(int value)
    {
        ammobar.MaxValue -= value;
        ammobar.Value = ammobar.MaxValue;
    }
}
