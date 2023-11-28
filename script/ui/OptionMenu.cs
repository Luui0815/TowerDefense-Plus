using Godot;
using System;

public partial class OptionMenu : Window
{
	private AudioStreamPlayer _audioPlayer;
	private double _soundVolume;
	private bool _testSoundPlayed = false;
	private bool _scrolling = false;

	public override void _Ready()
	{
		_audioPlayer = GetNode<AudioStreamPlayer>("Sound_Test");
	}

	public override void _Process(double delta)
	{
		if(!_audioPlayer.Playing && _soundVolume!=0 && !_testSoundPlayed && !_scrolling)
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
		_testSoundPlayed = false;
	}

	private void OnCloseRequested()
	{
		//SetSount(value); //Luca
		QueueFree();
	}

	private void OnVolumeScrollbarScrolling()
	{
		_scrolling = true;
	}

}


