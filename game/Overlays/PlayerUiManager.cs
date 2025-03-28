using Godot;
using System;

public partial class PlayerUiManager : Control
{
    ProgressBar timer;
    RichTextLabel killLabel;
    ProgressBar healthbar;
    Sprite2D nextBullet;
    Sprite2D nextNextBullet;
    Sprite2D nextNextNextBullet;
    int kills = 0;

    public override void _Ready()
    {
        timer = (ProgressBar)GetNode("Timer");
        killLabel = (RichTextLabel)GetNode("Kills");
        healthbar = (ProgressBar)GetNode("HealthBar");
        nextBullet = (Sprite2D)GetNode("NextBullet/DisplayBullet");
        nextNextBullet = (Sprite2D)GetNode("NextBullet2/DisplayBullet");
        nextNextNextBullet = (Sprite2D)GetNode("NextBullet3/DisplayBullet");
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
        killLabel.Text = "[color=white][font=res://Fonts/VT323/VT323-Regular.ttf][font_size=25] Kills: " + kills + "[/font_size][/font][/color]";
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
        
        nextBullet.Texture = GD.Load<Texture2D>(sprite1);
        // This prevents the a null ref exceptions that pops up every load
        if (sprite1 == sprite2)
        {
            nextNextBullet.Texture = nextBullet.Texture;
            if (sprite2 == sprite3)
            {
                nextNextNextBullet.Texture = nextNextBullet.Texture;
                return;
            }
        }
        nextNextBullet.Texture = GD.Load<Texture2D>(sprite2);
        nextNextNextBullet.Texture = GD.Load<Texture2D>(sprite3);
    }

    public void GameOver()
    {
        GetTree().ChangeSceneToFile("res://Levels/MainMenu/GameOver.tscn");
    }
}
