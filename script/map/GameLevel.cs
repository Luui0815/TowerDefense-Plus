using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using TowerDefense;

public abstract partial class GameLevel : Node2D
{
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
        _levelControlBar = GetNode<LevelControlBar>("LevelControlBar");
        _levelControlBar.DisplayMoney(CurrentMoney);
        Vector2 Position = new Vector2(0, 0);

        PackedScene laneScene = GD.Load<PackedScene>("res://scene/map/MapLane.tscn");
        for (int i = 0; i<5; i++)
        {
            //MapLane lane = (MapLane) laneScene.Instantiate();
            MapLane lane = (MapLane)GD.Load<PackedScene>("res://scene/map/MapLane.tscn").Instantiate();
            lane.Init(1, GetFieldTypeRow(i),i);
            lane.Position = Position;
            lane.Name = "MapLane" + i;
            lane.EnemyCrossedLane += (laneNr) => OnEnemyCrossedLane(laneNr);
            lane.AllEnemiesDefeated += (laneNr) => OnAllEnemiesDefeated(laneNr);
            AddChild(lane);
            _lanes[i] = lane;
            Position.Y += 144;
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

    public void FillTowerContainer(List<string> towerNames)
    {
        //TODO: Fill item container

        PackedScene towerItemScene = GD.Load<PackedScene>("res://scene/map/TowerContainerItem.tscn");
        TowerConfig towerConfig = GetNode<TowerConfig>("/root/TowerConfig");
        foreach (string towerName in towerNames)
        {
            TowerSettings towerSettings = towerConfig.GetTowerSettingsByName(towerName);

            TowerContainerItem item = (TowerContainerItem) towerItemScene.Instantiate();
            item.Init(towerName, towerSettings.Cost);
            _levelControlBar.AddTowerButton(item);
        }
    }

    /*
    public Array<Enemy> GetEnemiesAcrossLanes()
    {
        //TODO: Add implementation
    }
    */

    public void ChangeMoney(int newMoney)
    {
        CurrentMoney = newMoney;
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
