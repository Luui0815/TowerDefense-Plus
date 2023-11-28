using Godot;
using System;

public partial class OptionMenu : Window
{
	private AudioStreamPlayer _audioPlayer;
	private double _soundVolume;
	private bool _testSoundPlayed = false;

	public override void _Ready()
	{
		_audioPlayer = GetNode<AudioStreamPlayer>("Sound_Test");
	}

	public override void _Process(double delta)
	{
		if(!_audioPlayer.Playing && !_testSoundPlayed)
		{
			_audioPlayer.VolumeDb = Mathf.LinearToDb((float) _soundVolume / 100);

			_audioPlayer.Playing = true;
			_testSoundPlayed=true;
		}
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

}


