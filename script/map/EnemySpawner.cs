using Godot;
using System;

public partial class EnemySpawner : Node
{
    private Timer _spawnTimer;
    private (float, string)[] _spawnTimes;
    private Enemy _enemy;
    private int _currentSpawnIndex = 0;

    public EnemySpawner(int levelNr)
	{
        switch (levelNr)
        {
			case 1:
                _spawnTimes = new (float, string)[] {(10, "KnightEnemy"),(5, "KnightEnemy"),(2, "KnightEnemy")};
				break;
			default: break;
		}
	
        _spawnTimer = new Timer();
        AddChild(_spawnTimer);
        _spawnTimer.WaitTime = 6.0f;
        _spawnTimer.OneShot = false;
        _spawnTimer.Timeout += OnSpawnTimerTimeout;
    }

    public void SpawnTimerStart()
    {
        _spawnTimer.Start();
    }

    private void OnSpawnTimerTimeout()
    {
        if (_currentSpawnIndex < _spawnTimes.Length)
        {
            SpawnEnemy(_spawnTimes[_currentSpawnIndex].Item2);
            _currentSpawnIndex++;

            if (_currentSpawnIndex < _spawnTimes.Length)
            {
                _spawnTimer.WaitTime = _spawnTimes[_currentSpawnIndex - 1].Item1;
            }
        }
        else
        {
            _spawnTimer.Stop();
        }
    }

    private void SpawnEnemy(string enemyType)
    {
        switch(enemyType)
        {
            case "KnightEnemy":
                _enemy = (KnightEnemy)GD.Load<PackedScene>($"res://scene/enemy/KnightEnemy/{enemyType}.tscn").Instantiate();
                _enemy.Init();
                _enemy.Position = new Vector2(978,125);
                AddChild(_enemy);
                GD.Print(enemyType + "spawnt");
                break;
        }
    }
}

