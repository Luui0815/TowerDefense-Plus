using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using TowerDefense;

public abstract partial class GameLevel : Node2D
{
    private readonly HashSet<int> _completedLanes = new();
    private readonly MapLane[] _lanes = new MapLane[5];
    //private EnemySpawner _spawner;
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

    public abstract int CurrentMoney
    {
        get; set;
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
        PackedScene laneScene = GD.Load<PackedScene>("res://scene//map/MapLane.tscn");
        for (int i = 0; i<5; i++)
        {
            MapLane lane = (MapLane) laneScene.Instantiate();
            lane.Init(i, GetFieldTypeRow(i));
            lane.EnemyCrossedLane += (laneNr) => OnEnemyCrossedLane(laneNr);
            lane.AllEnemiesDefeated += (laneNr) => OnAllEnemiesDefeated(laneNr);
            AddChild(lane);
            _lanes[i] = lane;
        }
    }

    public void FillTowerContainer()
    {
        //TODO: Fill item container
    }

    /*
    public Array<Enemy> GetEnemiesAcrossLanes()
    {
        //TODO: Add implementation
    }
    */

    protected void OnStartLevelButtonPressed()
    {
        GetNode<Button>("StartButton").QueueFree();
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
