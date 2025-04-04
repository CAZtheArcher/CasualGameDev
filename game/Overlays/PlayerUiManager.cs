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
    Sprite2D weaponTwoNextBullet;
    Sprite2D weaponTwoNextNextBullet;
    Sprite2D weaponTwoNextNextNextBullet;
    int kills = 0;
    bool winCon = true;
    bool firstErr = false;

    public override void _Ready()
    {
        timer = (ProgressBar)GetNode("Timer");
        killLabel = (RichTextLabel)GetNode("Kills");
        healthbar = (ProgressBar)GetNode("HealthBar");
        nextBullet = (Sprite2D)GetNode("NextBullet/DisplayBullet");
        nextNextBullet = (Sprite2D)GetNode("NextBullet2/DisplayBullet");
        nextNextNextBullet = (Sprite2D)GetNode("NextBullet3/DisplayBullet");
        weaponTwoNextBullet = (Sprite2D)GetNode("NextBullet4/DisplayBullet");
        weaponTwoNextNextBullet = (Sprite2D)GetNode("NextBullet5/DisplayBullet");
        weaponTwoNextNextNextBullet = (Sprite2D)GetNode("NextBullet6/DisplayBullet");
        healthbar.Value = healthbar.MaxValue;
    }

    public void DecrementTime(double delta)
    {
        timer.Value -= delta;
        if (timer.Value <= 0 && winCon)
        {
            //win condition/level transition
            //GD.Print("YOU WIN");
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

    public void UpdateBulletSprite(string sprite1, string sprite2, string sprite3){
        // If which weapon is not specified, defaults to left.
        // Ensures compatibility with pre-existing code. 
        UpdateBulletSprite(sprite1, sprite2, sprite3, true);
    }

    public void UpdateBulletSprite(string sprite1, string sprite2, string sprite3, bool isLeftWeapon)
    {
        try{
            if (isLeftWeapon){
                nextBullet.Texture = GD.Load<Texture2D>(sprite1);
                // This prevents a null ref exception that pops up every load
                if (sprite1 == sprite2){
                    nextNextBullet.Texture = nextBullet.Texture;
                    if (sprite2 == sprite3){
                        nextNextNextBullet.Texture = nextNextBullet.Texture;
                        return;
                    }
                }
                nextNextBullet.Texture = GD.Load<Texture2D>(sprite2);
                nextNextNextBullet.Texture = GD.Load<Texture2D>(sprite3);
            }
            else{
                weaponTwoNextBullet.Texture = GD.Load<Texture2D>(sprite1);
                // This prevents a null ref exception that pops up every load
                if (sprite1 == sprite2){
                    weaponTwoNextNextBullet.Texture = nextBullet.Texture;
                    if (sprite2 == sprite3){
                        weaponTwoNextNextNextBullet.Texture = nextNextBullet.Texture;
                        return;
                    }
                }
                weaponTwoNextNextBullet.Texture = GD.Load<Texture2D>(sprite2);
                weaponTwoNextNextNextBullet.Texture = GD.Load<Texture2D>(sprite3);
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
