using Godot;
using System;

public partial class OptionMenu : Window
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
    private void _on_sound_scroll_bar_value_changed(double value)
    {
		GD.Print(value);
		//SetSount(value);
    }

	private void _on_close_requested()
	{
		Hide();
		this.QueueFree();
	}

}


