using Godot;
using System;

public partial class PlayerUiManager : Control
{
    ProgressBar healthbar;
    Sprite2D nextBullet;
    Sprite2D nextNextBullet;
    Sprite2D nextNextNextBullet;

    public override void _Ready()
    {
        healthbar = (ProgressBar)GetNode("HealthBar");
        nextBullet = (Sprite2D)GetNode("NextBullet/DisplayBullet");
        nextNextBullet = (Sprite2D)GetNode("NextNextBullet/DisplayBullet");
        nextNextNextBullet = (Sprite2D)GetNode("NextNextNextBullet/DisplayBullet");
        healthbar.Value = healthbar.MaxValue;
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

    public void UpdateBulletSprite(string sprite1, string sprite2, string sprite3)
    {
        Texture2D newTexture = GD.Load<Texture2D>(sprite1);
        nextBullet.Texture = newTexture;
        newTexture = GD.Load<Texture2D>(sprite2);
        nextNextBullet.Texture = newTexture;
        newTexture = GD.Load<Texture2D>(sprite3);
        nextNextNextBullet.Texture = newTexture;
    }

    public void GameOver()
    {
        GetTree().ChangeSceneToFile("res://Levels/MainMenu/GameOver.tscn");
    }
}
