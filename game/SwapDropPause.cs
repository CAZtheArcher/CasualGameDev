using Godot;
using System;

public partial class SwapDropPause : Control
{
    private Control _pause;
    public override void _Ready()
    {
        _pause = GetNode<Control>("SwapDropPause");
        _pause.Hide();
    }
    private void Pause()
	{
		GetTree().Paused = true;
	}
}
