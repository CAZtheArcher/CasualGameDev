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
        if (healthbar.Value > healthbar.MaxValue) { healthbar.Value = healthbar.MaxValue; }
    }

    public void DecrimentHealth(int value)
    {
        healthbar.Value -= value;
        if (healthbar.Value <= 0) { GameOver(); }
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
        if (healthbar.Value <= 0) { GameOver(); }
    }

    public void ShotFired()
    {

    }

    public void GameOver()
    {
        GetTree().ChangeSceneToFile("res://Levels/MainMenu/GameOver.tscn");
    }
}
