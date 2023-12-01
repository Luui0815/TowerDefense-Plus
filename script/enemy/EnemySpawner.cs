using Godot;
using System;

public partial class EnemySpawner : Node
{
	private Timer _spawnTimer = new();
	private (float, string)[] _spawnTimes = {(2, "Archer"), (3, "Spearman"),(6, "Knight"),(6, "Catapult")};
	private int _currentSpawnIndex = 0;
	public override void _Ready()
	{
		InitializeTimer();
	}

	public override void _Process(double delta)
	{
	}

	private void InitializeTimer()
	{
		_spawnTimer.WaitTime = 1;
		_spawnTimer.OneShot = false;
		_spawnTimer.Timeout += OnSpawnTimerTimeout;
		AddChild(_spawnTimer);
		_spawnTimer.Start();
	}

	private void OnSpawnTimerTimeout()
	{
		if(_currentSpawnIndex < _spawnTimes.Length)
		{
			SpawnEnemy(_spawnTimes[_currentSpawnIndex].Item2);
			_currentSpawnIndex++;

			if (_currentSpawnIndex < _spawnTimes.Length)
			{
				_spawnTimer.WaitTime = _spawnTimes[_currentSpawnIndex].Item1 - _spawnTimes[_currentSpawnIndex - 1].Item1;
			}
		}
		else
		{
			_spawnTimer.Stop();
		}
	}

	private void SpawnEnemy(string enemy)
	{
		int lane = GD.RandRange(1, 5);
		GD.Print("Gegner " + enemy + " auf Lane " + lane + " gespawnt.");
	}
}
