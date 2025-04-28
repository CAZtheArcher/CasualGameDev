using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;

public partial class EnemyManager : Node
{
    public Data data;
    PackedScene scene = GD.Load<PackedScene>("res://Enemy/Enemy.tscn");
    PackedScene vacuumEnemyScene;
    List<Enemy> enemies = new List<Enemy>();
    int[] levels = { 30, 60, 90, 120 };
    PlayerUiManager UIManager;
    double spawnDelay = 2;
	double countdown;
	double totalTime = 0;
    RichTextLabel levelWin;

    Random random;

	public override void _Ready()
    {
        DirAccess.MakeDirAbsolute("res://Data.tres");
        countdown = spawnDelay;

        data = ResourceLoader.Load<Data>("res://Data.tres");
        UIManager = (PlayerUiManager)GetNode("/root/Main/Player/PlayerBody/PlayerUi");

        random = new Random();
        vacuumEnemyScene = GD.Load<PackedScene>("res://Enemy/Vacuum/Vacuum.tscn");

        addEnemy(); 
    }

    public override void _Process(double delta)
    {
		UIManager.DecrementTime(delta);
        countdown -= delta;
		totalTime += delta;

		switch (data.level)
		{
			case 0:
                if (countdown <= 0)
                {
                    countdown = spawnDelay;
                    addEnemy();
                }
                spawnDelay = 10 / totalTime;
                break;
            case 1:
                if (countdown <= 0)
                {
                    countdown = spawnDelay;
                    addEnemy();
                }
                spawnDelay = 10 / totalTime;
                break;
            case 2:
                if (countdown <= 0)
                {
                    countdown = spawnDelay;
                    addEnemy();
                }
                spawnDelay = 10 / totalTime;
                break;
            case 3:
                if (countdown <= 0)
                {
                    countdown = spawnDelay;
                    addEnemy();
                }
                spawnDelay = 10 / totalTime;
                break;
        }
    }

    public void LevelUpdate(int lev)
    {
        data.level = lev;
    }

    public int LevelTrans()
    {
            CallDeferred("DelaySwitch");
            GD.Print(levels[data.level + 1]);
            return levels[data.level + 1];
    }

    public void addEnemy()
    {
        Enemy enemy;
        if(random.NextSingle() < 0.35){enemy = vacuumEnemyScene.Instantiate<Vacuum>();}
        else{enemy = scene.Instantiate<Enemy>();}
        enemies.Add(enemy);
        AddChild(enemies[enemies.Count - 1]);
    }

    public void addEnemies(int quantity)
    {
        for (int i = 0; i < quantity; i++)
        {
            enemies.Add(scene.Instantiate<Enemy>());
            AddChild(enemies[enemies.Count - 1]);
        }
    }
    public void DelaySwitch()
    {
        GetTree().ChangeSceneToFile("res://Levels/MainMenu/LevelScene.tscn");
    }

    public void GameWin()
    {
        GetTree().ChangeSceneToFile("res://Levels/MainMenu/WinScreen.tscn");
    }
}
