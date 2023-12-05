using Godot;
using System;
using TowerDefense;

public partial class LevelOne : GameLevel
{
    public LevelOne()
    {
        //TODO: Change start money
        _currentMoney = 999;
    }

    protected override int LevelNumber
    {
        get;
    } = 1;

    //TODO: Configure spawns
    protected override string[] SpawnConfigs
    {
        get;
    } = new string[3] {
        "", "", ""
    };

    protected override FieldType[,] FieldTypes
    {
        get;
    } = new FieldType[5, 10] {
        { FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal },
        { FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal },
        { FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal },
        { FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal },
        { FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal }
    };

    //public void Init(FieldType ft)//Lane Initialisieren
    //{
    //    FieldType[] FieldLane = new FieldType[10];
    //    Vector2 Position = new Vector2(0,0);

    //    for (int i = 0;i<=5;i++)
    //    {
    //        //Copy one lane
    //        for (int j = 0; j < 10; j++)
    //        {
    //            FieldLane[j] = FieldTypes[i, j];
    //        }

    //        MapLane ML = (MapLane)GD.Load<PackedScene>("res://scene/map/MapLane.tscn").Instantiate();
    //        ML.Init(1,FieldLane,i);
    //        ML.Position = Position;
    //        AddChild(ML);
    //        Position.Y += 144;
    //    }
    //}
}
