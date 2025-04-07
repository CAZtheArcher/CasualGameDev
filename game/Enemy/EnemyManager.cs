using Godot;
using System;
using System.Collections;
using System.Collections.Generic;

public partial class EnemyManager : Node
{ 
    PackedScene scene = GD.Load<PackedScene>("res://Enemy/Enemy.tscn");
    PackedScene vacuumEnemyScene;
    List<Enemy> enemies = new List<Enemy>();
    PlayerUiManager UIManager;
    double spawnDelay = 2;
	double countdown;
	double totalTime = 0;
	int level = 0;

    Random random;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        countdown = spawnDelay;
        UIManager = (PlayerUiManager)GetNode("/root/Main/Player/PlayerBody/PlayerUi");

        random = new Random();
        vacuumEnemyScene = GD.Load<PackedScene>("res://Enemy/Vacuum/Vacuum.tscn");
        
        addEnemy(); 
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
		UIManager.DecrementTime(delta);
        countdown -= delta;
		totalTime += delta;

		switch (level)
		{
			case 0:
                if (countdown <= 0)
                {
                    countdown = spawnDelay;
                    addEnemy();
                }
                if (totalTime >= 60)
                {
                    level++;
                    //level transition scene
                }
                spawnDelay = 10 / totalTime;
                break;
            case 1:

                break;
		}
    }

    public void addEnemy()
    {
        if (random.NextSingle() < 0.35)
        {
            enemies.Add(vacuumEnemyScene.Instantiate<Vacuum>());
            AddChild(enemies[enemies.Count - 1]);
            return;
        }
        enemies.Add(scene.Instantiate<Enemy>());
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
}
