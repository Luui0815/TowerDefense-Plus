using Godot;
using System;

public partial class OptionMenu : Window
{
	private AudioStreamPlayer _soundplayer;
	private DateTime _soundchanged_time;
	private double _soundVolume;
	private bool _volume_Testsound_played = false;
	private bool _scrolling = false;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        _soundplayer = GetNode<AudioStreamPlayer>("Sound_Test");
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if((DateTime.Now-_soundchanged_time).TotalMilliseconds>100 && _soundVolume!=0 &&_volume_Testsound_played!=true &&_scrolling==false)
		{
            //Lautstaerke berechnen max.24 db
            _soundplayer.VolumeDb = (float)_soundVolume * 24 / 100;

            _soundplayer.Playing = true;
			_volume_Testsound_played=true;
        }
		_scrolling = false;
	}
    private void _on_sound_scroll_bar_value_changed(double value)
    {
		_soundVolume = value;
		_soundchanged_time = DateTime.Now;
        _volume_Testsound_played = false;
    }

	private void _on_close_requested()
	{
        //SetSount(value); //Luca
        Hide();
		this.QueueFree();
	}

	private void _on_sound_scroll_bar_scrolling()
	{
		_scrolling = true;
	}

}


