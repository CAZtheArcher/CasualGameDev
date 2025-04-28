using Godot;
using System;

public partial class Next : Button{
    Data data;
    RichTextLabel winLabel;
    EnemyManager enemyManager;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready(){
		this.Pressed += Clicked;
        //enemyManager = (EnemyManager)GetNode("res://Enemy/EnemyManager.cs");
        winLabel = (RichTextLabel)GetNode("/root/LevelScene/YouWin");
        data = ResourceLoader.Load<Data>("res://Data.tres");
        winLabel.Text = "[color=green][font=res://Fonts/VT323/VT323-Regular.ttf] [font_size=100][center]WAVE " + data.level + " COMPLETE[/center][/font_size][/font][/color]";
    }

    private void Clicked(){
        data.ChangeLevel();
        GD.PrintErr("Level transition to " + data.level);
        //enemyManager.LevelUpdate(level);
        GetTree().ChangeSceneToFile("res://Main.tscn");
    }
}
