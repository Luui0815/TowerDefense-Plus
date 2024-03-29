using Godot;
using System;
using TowerDefense;

namespace TowerDefense
{
    public enum EnemyType
    {
        Soldier,
        Knight,
        Bandit,
        Pyro,
        Giant
    }
}

public partial class EnemySpawner : Node
{
    private Timer _spawnTimer;
    private (int, EnemyType)[] _spawnTimes, _endlessSpawnTimes;
    private int _currentSpawnIndex = 0, _endlessEnemyAmount = 17;
    private bool _finished = false;

    public bool Finished
    {
        get { return _finished; }
    }

    public EnemySpawner((int, EnemyType)[] spawnConfig)
    {
        _spawnTimes = spawnConfig;
        _spawnTimer = new Timer
        {
            Name = "SpawnTimer"
        };
        AddChild(_spawnTimer);
        _spawnTimer.WaitTime = 20.0f;
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
            _finished = true;
        }
    }

    public void StartEndlessMode(float waitTime)
    {
        GD.Print("Starte Endlos-Modus");
        _spawnTimer.Timeout -= OnSpawnTimerTimeout;
        _spawnTimer.Timeout += OnEndlessSpawnTimerTimeout;     
        StartEndlessWave(waitTime);
    }

    private void StartEndlessWave(float waitTime)
    {
        _spawnTimer.WaitTime = waitTime;
        _spawnTimer.Start();
        _currentSpawnIndex = 0;
        CreateEnemyWaveEndless();
    }

    private void OnEndlessSpawnTimerTimeout()
    {
        if (_currentSpawnIndex < _endlessSpawnTimes.Length)
        {
            SpawnEnemy(_endlessSpawnTimes[_currentSpawnIndex].Item2);
            _currentSpawnIndex++;

            if (_currentSpawnIndex < _endlessSpawnTimes.Length)
            {
                _spawnTimer.WaitTime = _endlessSpawnTimes[_currentSpawnIndex - 1].Item1;
            }
        }
        else
        {
            _spawnTimer.Stop();
            StartEndlessWave(20f);
        }
    }

    private void CreateEnemyWaveEndless()
    {
        _endlessSpawnTimes = new(int,EnemyType)[_endlessEnemyAmount];
        Random random = new();

        for(int i = 0; i < _endlessEnemyAmount; i++) 
        {
            int waitTime = random.Next(1, 2);
            int enemyId = _endlessEnemyAmount > 22 ? random.Next(1, 6) : random.Next(1, 5); // erst nach einigen Waves koennen giants spawnen
            _endlessSpawnTimes[i] = (waitTime, (EnemyType) enemyId);
        }
        _endlessEnemyAmount++;
    }

    private void SpawnEnemy(EnemyType enemyType)
    {
        int randomLine = GD.RandRange(0, 4);
        int lane = randomLine + 1;
        Enemy enemy = (Enemy)GD.Load<PackedScene>($"res://scene/enemy/{enemyType}Enemy/{enemyType}Enemy.tscn").Instantiate();
        int positionOffset = 0;
        switch (enemyType)
        {
            case EnemyType.Knight:
                positionOffset = 82;
                break;
            case EnemyType.Bandit:
                positionOffset = 10;
                break;
            case EnemyType.Giant:
                positionOffset = 48;
                break;
            case EnemyType.Soldier:
                positionOffset = 55;
                break;
            case EnemyType.Pyro:
                positionOffset = 35;
                break;
        }
        enemy.Position = new Vector2(1080, ((randomLine * 125) + (randomLine*10)) + positionOffset);
        AddChild(enemy);
        GD.Print($"{enemyType} spawnt auf Lane {lane}");
    }
}