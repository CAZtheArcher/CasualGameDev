using Godot;
using System;

public partial class PlayerUiManager : Control
{
    ProgressBar timer;
    Label killLabel;
    ProgressBar healthbar;
    Sprite2D nextBullet;
    Sprite2D nextNextBullet;
    Sprite2D nextNextNextBullet;
    int kills = 0;

    public override void _Ready()
    {
        timer = (ProgressBar)GetNode("Timer");
        killLabel = (Label)GetNode("Kills");
        healthbar = (ProgressBar)GetNode("HealthBar");
        nextBullet = (Sprite2D)GetNode("NextBullet/DisplayBullet");
        nextNextBullet = (Sprite2D)GetNode("NextNextBullet/DisplayBullet");
        nextNextNextBullet = (Sprite2D)GetNode("NextNextNextBullet/DisplayBullet");
        healthbar.Value = healthbar.MaxValue;
    }

    public void DecrementTime(double delta)
    {
        timer.Value -= delta;
        if (timer.Value < 0)
        {
            //win condition/level transition
            GD.Print("YOU WIN");
        }
    }

    public void IncrementHealth(int value)
    {
        healthbar.Value += value;
        if (healthbar.Value > healthbar.MaxValue) { healthbar.Value = healthbar.MaxValue; }
    }

    public void IncrementKills()
    {
        kills++;
        killLabel.Text = "Kills: " + kills;
    }

    public void DecrementHealth(int value)
    {
        healthbar.Value -= value;
        if (healthbar.Value <= 0) { GameOver(); }
    }

    public void IncrementMaxHealth(int value)
    {
        healthbar.MaxValue += value;
        healthbar.Value = healthbar.MaxValue;
    }

    public void DecrementMaxHealth(int value)
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
