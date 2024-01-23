using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.Text.Json;
using TowerDefense;

public partial class PlayerData : Node
{
    private readonly List<int> _completedLevels = new();
    private Array<string> _unlockedTowers = new(){
        "knight",
        "wall",
        "goldmine",
        "archer",
        "caltrop_trap",
    };

    /// <summary>
    /// An array containing the numbers of all completed levels
    /// </summary>
    public List<int> CompletedLevels
    {
        get
        {
            return _completedLevels;
        }
    }

    /// <summary>
    /// An array containing the names of all unlocked towers
    /// </summary>
    public Array<string> UnlockedTowers
    {
        get
        {
            return _unlockedTowers;
        }
    }

    /// <summary>
    /// The active level
    /// </summary>
    public Level CurrentLevel {
        get; set;
    }

    public override void _Ready()
    {
        if (!FileAccess.FileExists("user://player.dat"))
        {
            Save();
        }
        else
        {
            Load();
        }
    }

    /// <summary>
    /// Adds a level number to the completed level numbers array
    /// </summary>
    /// <param name="levelNr">The level number to be saved as a completed level</param>
    /// <returns>False, if the level was already completed</returns>
    public bool AddCompletedLevelNumber(int levelNr)
    {
        if (_completedLevels.Contains(levelNr))
        {
            return false;
        }
        _completedLevels.Add(levelNr);
        return true;
    }

    /// <summary>
    /// Clears the completed levels list
    /// </summary>
    public void ResetCompletedLevels()
    {
        _completedLevels.Clear();
    }

    /// <summary>
    /// Adds a tower type to the unlocked towers array
    /// </summary>
    /// <param name="towerName">The tower name to be saved as an unlocked tower</param>
    /// <returns>False, if the tower was already unlocked</returns>
    public bool AddUnlockedTower(string towerName)
    {
        if (_unlockedTowers.Contains(towerName))
        {
            return false;
        }
        _unlockedTowers.Add(towerName);
        return true;
    }

    /// <summary>
    /// Resets the unlocked towers list
    /// </summary>
    public void ResetTowers()
    {
        _unlockedTowers =  new(){
            "knight",
            "wall",
            "goldmine",
            "archer",
            "caltrop_trap",
        };
    }

    /// <summary>
    /// Loads the saved player data from the local data file
    /// </summary>
    /// <exception cref="System.IO.FileLoadException">When the data file could not be accessed</exception>
    /// <exception cref="JsonException">When the data file contains invalid JSON</exception>
	public void Load()
    {
        if (!FileAccess.FileExists("user://player.dat"))
        {
            return;
        }

        using FileAccess saveFile = FileAccess.Open("user://player.dat", FileAccess.ModeFlags.Read);
        if (saveFile == null)
        {
            Error fileError = FileAccess.GetOpenError();
            GD.PrintErr($"File Error: {FileAccess.GetOpenError()}");
            throw new System.IO.FileLoadException($"File load exception: {fileError}");
        }

        string fileData = saveFile.GetLine();
        var json = new Json();
        Error parseResult = json.Parse(fileData);
        if (parseResult != Error.Ok)
        {
            string errorMsg = $"JSON Parse Error: {json.GetErrorMessage()} in {fileData} at line {json.GetErrorLine()}";
            GD.Print(errorMsg);
            throw new JsonException(errorMsg);
        }

        var dataDict = new Godot.Collections.Dictionary<string, Variant>((Dictionary)json.Data);

        Array<int> savedLevels = (Array<int>)dataDict["CompletedLevels"];
        _completedLevels.Clear();
        foreach(int level in savedLevels)
        {
            _completedLevels.Add(level);
        }

        Array<string> savedUnlocks = (Array<string>)dataDict["UnlockedTowers"];
        foreach (string unlockedTower in savedUnlocks)
        {
            if (!_unlockedTowers.Contains(unlockedTower))
            {
                _unlockedTowers.Add(unlockedTower);
            }
        }
    }

    /// <summary>
    /// Saves the player data to a local data file
    /// </summary>
    /// <exception cref="System.IO.FileLoadException">When the data file could not be accessed</exception>()
	public void Save()
    {
        Array<int> saveArray = new Array<int>();
        foreach(int level in _completedLevels)
        {
            saveArray.Add(level);
        }
        var dataDict = new Godot.Collections.Dictionary<string, Variant>()
        {
            {"CompletedLevels", saveArray},
            {"UnlockedTowers", _unlockedTowers}
        };
        string jsonData = Json.Stringify(dataDict);

        using FileAccess saveFile = FileAccess.Open("user://player.dat", FileAccess.ModeFlags.Write);
        if (saveFile == null)
        {
            Error fileError = FileAccess.GetOpenError();
            GD.PrintErr($"File Error: {FileAccess.GetOpenError()}");
            throw new System.IO.FileLoadException($"File load exception: {fileError}");
        }
        saveFile.StoreLine(jsonData);
    }
}
