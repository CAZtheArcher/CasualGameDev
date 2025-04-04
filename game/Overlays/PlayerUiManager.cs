using Godot;
using System;

public partial class PlayerUiManager : Control
{
    ProgressBar timer;
    RichTextLabel killLabel;
    RichTextLabel timerLabel;
    ProgressBar healthbar;
    Sprite2D nextBullet;
    Sprite2D nextNextBullet;
    Sprite2D nextNextNextBullet;
    int kills = 0;
    bool winCon = true;
    bool firstErr = false;

    public override void _Ready()
    {
        timer = (ProgressBar)GetNode("Timer");
        killLabel = (RichTextLabel)GetNode("Kills");
        timerLabel = (RichTextLabel)GetNode("TimerDisplay");
        healthbar = (ProgressBar)GetNode("HealthBar");
        nextBullet = (Sprite2D)GetNode("NextBullet/DisplayBullet");
        nextNextBullet = (Sprite2D)GetNode("NextBullet2/DisplayBullet");
        nextNextNextBullet = (Sprite2D)GetNode("NextBullet3/DisplayBullet");
        healthbar.Value = healthbar.MaxValue;
    }

    public void DecrementTime(double delta)
    {
        timer.Value -= delta;
        int timerValue = Convert.ToInt32(Math.Ceiling(timer.Value));
        timerLabel.Text = "[color=white][font=res://Fonts/VT323/VT323-Regular.ttf][font_size=25] Time Remaining: " + timerValue + "[/font_size][/font][/color]";
        if (timer.Value <= 0 && winCon)
        {
            //win condition/level transition
            CallDeferred("GameWin");
            winCon = false;
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

        if (healthbar.Value <= 0) { CallDeferred("GameOver"); }
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
        if (healthbar.Value <= 0) { CallDeferred("GameOver"); }
    }

    public void ShotFired()
    {

    }

    public void UpdateBulletSprite(string sprite1, string sprite2, string sprite3)
    {
        try
        {
            nextBullet.Texture = GD.Load<Texture2D>(sprite1);
            // This prevents a null ref exception that pops up every load
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
        catch (Exception ex) {
            if (firstErr) { GD.Print("Error: " + ex.Message); }
            firstErr = true;
        }
    }

    public void GameOver()
    {
        GetTree().ChangeSceneToFile("res://Levels/MainMenu/GameOver.tscn");
    }

    public void GameWin()
    {
        GetTree().ChangeSceneToFile("res://Levels/MainMenu/WinScreen.tscn");
    }
}
