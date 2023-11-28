using Godot;
using System;

public partial class OptionMenu : Window
{
	private AudioStreamPlayer _audioPlayer;
	private DateTime _soundChangedTime;
	private double _soundVolume;
	private bool _testSoundPlayed = false;
	private bool _scrolling = false;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_audioPlayer = GetNode<AudioStreamPlayer>("Sound_Test");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if((DateTime.Now-_soundChangedTime).TotalMilliseconds>100 && _soundVolume!=0 &&_testSoundPlayed!=true &&_scrolling==false)
		{
			//Lautstaerke berechnen max.24 db
			_audioPlayer.VolumeDb = (float)_soundVolume * 24 / 100;

			_audioPlayer.Playing = true;
			_testSoundPlayed=true;
		}
		_scrolling = false;
	}
	private void OnVolumeScrollbarValueChanged(double value)
	{
		_soundVolume = value;
		_soundChangedTime = DateTime.Now;
		_testSoundPlayed = false;
	}

	private void OnCloseRequested()
	{
		//SetSount(value); //Luca
		Hide();
		this.QueueFree();
	}

	private void OnVolumeScrollbarScrolling()
	{
		_scrolling = true;
	}

}


