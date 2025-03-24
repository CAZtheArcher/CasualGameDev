using Godot;
using System.Collections;
using System.Collections.Generic;

public partial class EnemyManager : Node
{ 
    PackedScene scene = GD.Load<PackedScene>("res://Enemy/Enemy.tscn");
	List<Enemy> enemies = new List<Enemy>();
    PlayerUiManager UIManager;
    double time = 2;
	double countdown;
	double totalTime = 0;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		countdown = time;
        UIManager = (PlayerUiManager)GetNode("/root/Main/Player/PlayerBody/PlayerUi");
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
    {
		UIManager.DecrementTime(delta);
        countdown -= delta;
		totalTime += delta;
		if (countdown <= 0)
        {
            countdown = time;
			addEnemy();
		}
		if (totalTime >= 60)
		{
			//game end
		}		
		else if (totalTime >= 40)
			time = 0.2;
		else if (totalTime >= 20)
            time = 1;
    }

    public void addEnemy()
    {
        enemies.Add(scene.Instantiate<Enemy>());
        AddChild(enemies[enemies.Count - 1]);
    }
}
