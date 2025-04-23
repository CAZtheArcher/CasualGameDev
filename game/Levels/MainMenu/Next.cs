using Godot;
using System;

public partial class Next : Button{
    public Resource Data;
    int level = 0;
    RichTextLabel winLabel;
    EnemyManager enemyManager;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready(){
		this.Pressed += Clicked;
        //enemyManager = (EnemyManager)GetNode("res://Enemy/EnemyManager.cs");
        winLabel = (RichTextLabel)GetNode("/root/LevelScene/YouWin");
        Data = ResourceLoader.Load("/root/Data.tres");
        winLabel.Text = "[color=green][font=res://Fonts/VT323/VT323-Regular.ttf] [font_size=100][center]WAVE " + level + " COMPLETE[/center][/font_size][/font][/color]";
    }

    private void Clicked(){
        GD.PrintErr("Level transition");
        //enemyManager.LevelUpdate(level);
        GetTree().ChangeSceneToFile("res://Main.tscn");
    }
}
