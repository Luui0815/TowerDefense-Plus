using Godot;
using System;
using System.Collections.Generic;
using TowerDefense;

public abstract partial class GameLevel : Node2D
{
    private readonly HashSet<int> _completedLanes = new();
    //private EnemySpawner _spawner;
    private bool _levelStarted = false;

    public bool LevelStarted
    {
        get
        {
            return _levelStarted;
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

    }

    public void FillTowerContainer()
    {

    }

    /*
    public Array<Enemy> GetEnemiesAcrossLanes()
    {
        //TODO: Add implementation
    }
    */

    protected void OnStartLevelButtonPressed()
    {
        _levelStarted = true;
        //_spawner.Start();
    }

    protected void OnPauseLevelButtonPressed()
    {
        GetTree().Paused = true;
        //TODO: Open pause screen
    }

    protected void OnEnemyCrossedLane(int laneNr)
    {
        //TODO: Show defeat screen
    }

    protected void OnAllEnemiesDefeated(int laneNr)
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
}
