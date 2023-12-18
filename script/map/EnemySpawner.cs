using Godot;
using System;

public partial class EnemySpawner : Node
{
    private Timer _spawnTimer;
    private (float, string)[] _spawnTimes;
    private Enemy _enemy;
    private int _currentSpawnIndex = 0;
    private bool finished = false;

    public bool Finished
    {
        get { return finished; }
    }

    public EnemySpawner(int levelNr)
    {
        switch (levelNr)
        {
            case 1:
                _spawnTimes = new (float, string)[] { (15, "KnightEnemy"), (10, "KnightEnemy"), (5, "BanditEnemy"), (1, "BanditEnemy"), (5, "GiantEnemy"), (0, "") };
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
            finished = true;
        }
    }

    private void SpawnEnemy(string enemyType)
    {
        int random = GD.RandRange(0, 4);
        int lane = random + 1;

        switch (enemyType)
        {
            case "KnightEnemy":
                _enemy = (KnightEnemy)GD.Load<PackedScene>($"res://scene/enemy/{enemyType}/{enemyType}.tscn").Instantiate();
                _enemy.Init();
                _enemy.Position = new Vector2(978, (random * 125) - 30);
                AddChild(_enemy);
                GD.Print(enemyType + " spawnt auf Lane " + lane);
                break;
            case "BanditEnemy":
                _enemy = (BanditEnemy)GD.Load<PackedScene>($"res://scene/enemy/{enemyType}/{enemyType}.tscn").Instantiate();
                _enemy.Init();
                _enemy.Position = new Vector2(978, (random * 125) - 25);
                AddChild(_enemy);
                GD.Print(enemyType + " spawnt auf Lane " + lane);
                break;
            case "GiantEnemy":
                _enemy = (GiantEnemy)GD.Load<PackedScene>($"res://scene/enemy/{enemyType}/{enemyType}.tscn").Instantiate();
                _enemy.Init();
                _enemy.Position = new Vector2(978, (random * 125) - 30);
                AddChild(_enemy);
                GD.Print(enemyType + " spawnt auf Lane " + lane);
                break;
            default: break;
        }
    }
}