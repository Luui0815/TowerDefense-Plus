using Godot;
using System;

public partial class OptionMenu : Window
{
	private AudioStreamPlayer _audioPlayer;
	private Timer _volumeTimer;
	private Label _currentVolumeLabel;
	private double _testSoundLength;

	public override void _Ready()
	{
		_audioPlayer = GetNode<AudioStreamPlayer>("TestSoundPlayer");
		_testSoundLength = _audioPlayer.Stream.GetLength();
		_volumeTimer = GetNode<Timer>("VolumeTimer");
		_currentVolumeLabel = GetNode<Label>("CurrentVolumeLabel");
	}

	private void OnVolumeScrollbarValueChanged(double value)
	{
		_currentVolumeLabel.Text = Convert.ToString((int) value);

		if (!_audioPlayer.Playing && _volumeTimer.TimeLeft == 0)
		{
			_audioPlayer.VolumeDb = Mathf.LinearToDb((float) value / 100);
			_audioPlayer.Playing = true;

			_volumeTimer.Start(_testSoundLength * 2);
		}
	}

	private void OnCloseRequested()
	{
		//SetSount(value); //Luca
		QueueFree();
	}

}


