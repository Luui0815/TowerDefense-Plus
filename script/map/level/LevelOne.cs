using Godot;
using System;
using TowerDefense;

public partial class LevelOne : GameLevel
{
    //TODO: Change start money
    public override int CurrentMoney 
    { 
        get; 
        set; 
    } = 999;

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

}
