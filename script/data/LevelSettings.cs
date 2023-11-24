using Godot;
using System;

public partial class LevelSettings : RefCounted
{
    private MapType _map;
    private int _startMoney;
    private string _tower;
    private FieldType[,] _fieldTypes = new FieldType[5,10];
    private string[] _spawnConfig;

    public LevelSettings(string map, int startMoney, string unlocksTower, string[] fields, string[] spawnConfig)
    {
        //TODO: Handle exceptions at map
        _map = (MapType) Enum.Parse(typeof(MapType), map);
        _startMoney = startMoney;
        _tower = unlocksTower;
        _spawnConfig = spawnConfig;

        ConvertConfiguredFields(fields);
    }

    public enum MapType{

    }

    public enum FieldType{

    }

    private void ConvertConfiguredFields(string[] fields)
    {
        if (fields.Length > 5)
        {
            GD.PrintErr($"Invalid level configuration: Fields has {fields.Length} elements but should have <= 5 elements!");
            return;
        }

        for(int i = 0; i<fields.Length; i++)
        {
            string laneConfig = fields[i];
            if (laneConfig.Length > 10)
            {
                GD.PrintErr($"Invalid level configuration: Lane has {laneConfig.Length} fields but should have <= 10 elements!");
            }

            for (int j=0; j<Math.Min(laneConfig.Length, 10); j++)
            {
                //TODO: Handle exceptions at map
                _fieldTypes[i,j] = (FieldType) Convert.ToInt32(laneConfig[j]);
            }
        }
    }
}
