using Godot;
using System;

public partial class Drop : Button
{
    private Control pause;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        //pause = (Control)GetNode("/root/Main/Player/PlayerBody/SwapDrop");
        this.Pressed += Clicked;
    }

    private void Clicked()
    {
        pause.Hide();
        GetTree().Paused = false;
    }
}
