using Godot;
using System;

public partial class EnemySpawner : Node
{
    private Timer _spawnTimer;
    private (float, int)[] _spawnTimes, _endlessSpawnTimes;
    private Enemy _enemy;
    private int _currentSpawnIndex = 0, _endlessEnemyAmount = 15;
    private bool _finished = false, _endless = false;

    public bool Finished
    {
        get { return _finished; }
    }

    // Soldier - 1, Knight - 2, Bandit - 3, Pyro - 4, Giant - 5
    public EnemySpawner(int levelNr)
    {
        switch (levelNr)
        {
            case 1:
                _spawnTimes = new (float, int)[] { (30, 1), (10, 1), (5, 2), (20, 2), (1, 1), (1, 1), /*(1, 1), (1, 2), (1, 2), (1, 1), (1, 1), (1,1), (10, 1), (1, 3), (20, 3), (1, 2), (1, 2), (1, 2), (2, 1), (1, 1), (1, 1),(1, 4),(20, 4), (1, 2), (1, 2), (1, 2), (1, 2),(1, 1), (1, 1), (1, 1), (1, 1), (1, 1), (1, 1), (1, 1), (1, 3), (1, 3), (1, 3), (1, 4), (25, 4), (5,5), (4, 5),*/ (0,0)};
                break;
            default: break;
        }

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

    public void StartEndlessTimer(float waitTime)
    {
        GD.Print("Starte Endlos-Modus");
        _spawnTimer.Timeout -= OnSpawnTimerTimeout;
        _spawnTimer.Timeout += OnEndlessSpawnTimerTimeout;     
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
            StartEndlessTimer(20f);
        }
    }

    private void CreateEnemyWaveEndless()
    {
        _endlessSpawnTimes = new(float,int)[_endlessEnemyAmount];
        Random random = new();

        for(int i = 0; i < _endlessEnemyAmount; i++) 
        {
            float waitTime = random.Next(1, 2);
            int enemyId = _endlessEnemyAmount>16? random.Next(1, 6): random.Next(1, 5); // erst nach 2 Waves koennen giants spawnen
            _endlessSpawnTimes[i] = (waitTime, enemyId);
        }
        _endlessEnemyAmount++;
    }

    private void SpawnEnemy(int enemyId)
    {
        int random = GD.RandRange(0, 4);
        int lane = random + 1;

        switch (enemyId)
        {
            case 2:
                _enemy = (KnightEnemy)GD.Load<PackedScene>($"res://scene/enemy/KnightEnemy/KnightEnemy.tscn").Instantiate();
                _enemy.Position = new Vector2(1080, (random * 125) - 20);
                AddChild(_enemy);
                GD.Print("KnightEnemy spawnt auf Lane " + lane);
                break;
            case 3:
                _enemy = (BanditEnemy)GD.Load<PackedScene>($"res://scene/enemy/BanditEnemy/BanditEnemy.tscn").Instantiate();
                _enemy.Position = new Vector2(1080, (random * 125) + 10);
                AddChild(_enemy);
                GD.Print("BanditEnemy spawnt auf Lane " + lane);
                break;
            case 5:
                _enemy = (GiantEnemy)GD.Load<PackedScene>($"res://scene/enemy/GiantEnemy/GiantEnemy.tscn").Instantiate();
                _enemy.Position = new Vector2(1080, (random * 125) + 48);
                AddChild(_enemy);
                GD.Print("GiantEnemy spawnt auf Lane " + lane);
                break;
            case 1:
                _enemy = (SoldierEnemy)GD.Load<PackedScene>($"res://scene/enemy/SoldierEnemy/SoldierEnemy.tscn").Instantiate();
                _enemy.Position = new Vector2(1080, (random * 125) +50);
                AddChild(_enemy);
                GD.Print("SoldierEnemy spawnt auf Lane " + lane);
                break;
            case 4:
                _enemy = (PyroEnemy)GD.Load<PackedScene>($"res://scene/enemy/PyroEnemy/PyroEnemy.tscn").Instantiate();
                _enemy.Position = new Vector2(1080, (random * 125) + 35);
                AddChild(_enemy);
                GD.Print("PyroEnemy spawnt auf Lane " + lane);
                break;
            default: break;
        }
    }
}