using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using TowerDefense;

public abstract partial class GameLevel : Node2D
{
    [Signal]
    public delegate void MoneyChangedEventHandler(int _actmoney);

    protected int _currentMoney = 0;
    private readonly HashSet<int> _completedLanes = new();
    private readonly MapLane[] _lanes = new MapLane[5];
    //private EnemySpawner _spawner;
    private LevelControlBar _levelControlBar;
    private bool _levelStarted = false;

    public bool LevelStarted
    {
        get
        {
            return _levelStarted;
        }
    }

    public MapLane[] Lanes
    {
        get
        {
            return _lanes;
        }
    }

    public int CurrentMoney
    {
        get
        {
            return _currentMoney;
        }

        set
        {
            _currentMoney = Math.Clamp(value, 0, 99999);
            _levelControlBar.DisplayMoney(_currentMoney);
        }
    }

    protected abstract int LevelNumber
    {
        get;
    }

    protected abstract string[] SpawnConfigs
    {
        get;
    }

    protected abstract FieldType[,] FieldTypes
    {
        get;
    }

    public override void _Ready()
    {
        Name = "Level";
        _levelControlBar = GetNode<LevelControlBar>("LevelControlBar");
        _levelControlBar.DisplayMoney(CurrentMoney);

        Vector2 position = Vector2.Zero;
        PackedScene laneScene = GD.Load<PackedScene>("res://scene/map/MapLane.tscn");
        for (int i = 0; i < 5; i++)
        {
            MapLane lane = (MapLane)laneScene.Instantiate();
            lane.Init(i, GetFieldTypeRow(i));
            lane.Position = position;
            lane.Name = "MapLane" + i;
            lane.EnemyCrossedLane += OnEnemyCrossedLane;
            lane.AllEnemiesDefeated += OnAllEnemiesDefeated;

            AddChild(lane);
            _lanes[i] = lane;
            position.Y += 144;
        }

        foreach (MapLane lane in _lanes)
        {
            foreach (MapField field in lane.Fields)
            {
                field.DefenderPlaced += (towerCost) => ChangeMoney(_currentMoney - towerCost);
            }
        }

        List<string> strings = new List<string>
        {
            "knight",
            "spearman",
            "goldmine",
            "wall",
            "archer",
            "fire_trap"
        };
        FillTowerContainer(strings);
    }

    /// <summary>
    /// Fills the tower inventory with the specified towers
    /// </summary>
    /// <param name="towerNames">The names of the towers added to the inventory</param>
    public void FillTowerContainer(List<string> towerNames)
    {
        PackedScene towerItemScene = GD.Load<PackedScene>("res://scene/map/TowerContainerItem.tscn");
        TowerConfig towerConfig = GetNode<TowerConfig>("/root/TowerConfig");
        foreach (string towerName in towerNames)
        {
            TowerSettings towerSettings = towerConfig.GetTowerSettingsByName(towerName);

            TowerContainerItem item = (TowerContainerItem)towerItemScene.Instantiate();
            item.Init(towerName, towerSettings.Cost);
            _levelControlBar.AddTowerButton(item);
            MoneyChanged += item.UpdateItemStatus;
        }
        EmitSignal(SignalName.MoneyChanged, _currentMoney);
    }

    /*
    public Array<Enemy> GetEnemiesAcrossLanes()
    {
        //TODO: Add implementation
    }
    */

    /// <summary>
    /// Changes the money amount which the player has
    /// </summary>
    /// <param name="newMoney">The new money amount</param>
    public void ChangeMoney(int newMoney)
    {
        CurrentMoney = newMoney;
        EmitSignal(SignalName.MoneyChanged, _currentMoney);
    }

    /// <summary>
    /// Adds money to the players money amount
    /// </summary>
    /// <param name="moneyAmount">Money added to the player</param>
    public void AddMoney(int moneyAmount)
    {
        ChangeMoney(moneyAmount + _currentMoney);
    }

    protected void OnStartLevelButtonPressed(Button button)
    {
        button.QueueFree();
        _levelStarted = true;
        //_spawner.Start();
    }

    protected void OnPauseLevelButtonPressed()
    {
        GetTree().Paused = true;
        //TODO: Open pause screen
    }

    private void OnEnemyCrossedLane(int laneNr)
    {
        //TODO: Show defeat screen
    }

    private void OnAllEnemiesDefeated(int laneNr)
    {
        /*
        if (_spawner.Finished)
        {
            bool alreadyCompleted = _completedLanes.Add(laneNr);
            if (!alreadyCompleted && _completedLanes.Count == 5)
            {
                //TODO: Show victory screen
            }
        }
        */
    }

    private FieldType[] GetFieldTypeRow(int index)
    {
        index = Math.Clamp(index, 0, 4);
        return Enumerable.Range(0, FieldTypes.GetLength(1))
            .Select(x => FieldTypes[index, x])
            .ToArray();
    }
}
