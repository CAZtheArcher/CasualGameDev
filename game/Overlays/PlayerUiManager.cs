using Godot;
using System;

public partial class PlayerUiManager : Control
{
    ProgressBar timer;
    RichTextLabel killLabel;
    RichTextLabel timerLabel;
    ProgressBar healthbar;
    Sprite2D leftWeaponEquipped;
    Sprite2D rightWeaponEquipped;
    int kills = 0;
    bool firstErr = false;

    public override void _Ready()
    {
        timer = (ProgressBar)GetNode("Timer");
        killLabel = (RichTextLabel)GetNode("Kills");
        timerLabel = (RichTextLabel)GetNode("TimerDisplay");
        healthbar = (ProgressBar)GetNode("HealthBar");
        leftWeaponEquipped = (Sprite2D)GetNode("LeftWeapon/EquippedLeftWeapon");
        rightWeaponEquipped = (Sprite2D)GetNode("RightWeapon/EquippedRightWeapon");
        healthbar.Value = healthbar.MaxValue;
    }

    public void DecrementTime(double delta)
    {
        timer.Value -= delta;
        int timerValue = Convert.ToInt32(Math.Ceiling(timer.Value));
        timerLabel.Text = "[color=white][font=res://Fonts/VT323/VT323-Regular.ttf][font_size=25] Time Remaining: " + timerValue + "[/font_size][/font][/color]";
        if (timer.Value <= 0)
        {
            //win condition/level transition
            CallDeferred("GameWin");
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
        killLabel.Text = "[color=white][font=res://Fonts/VT323/VT323-Regular.ttf][font_size=25] Kills: " + kills + " [/font_size][/font][/color]";
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

    public void UpdateBulletSprite(string sprite, bool isLeftWeapon = true)
    {
        try{
            if (isLeftWeapon){
                leftWeaponEquipped.Texture = GD.Load<Texture2D>(sprite);
            }
            else{
                rightWeaponEquipped.Texture = GD.Load<Texture2D>(sprite);
            }
        }
        catch (Exception ex){
            if (firstErr) { GD.Print("UpdateBulletSprite Error: " + ex.Message); }
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
