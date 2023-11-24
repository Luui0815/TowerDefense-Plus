using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.Text.Json;

public partial class LevelConfig : Node
{
    private readonly System.Collections.Generic.Dictionary<int, LevelSettings> _loadedLevels = new();
    
    public System.Collections.Generic.Dictionary<int, LevelSettings> LoadedLevels
    {
        get
        {
           return _loadedLevels; 
        }
    }

    public LevelSettings GetLevelByNumber(int number)
    {
        return LoadedLevels.GetValueOrDefault(number, null);
    }

    public void Load()
    {
        if (!FileAccess.FileExists("res://config/levels.json"))
        {
            return;
        }

        using FileAccess levelFile = FileAccess.Open("res://config/levels.json", FileAccess.ModeFlags.Read);
        if (levelFile == null)
        {
            Error fileError = FileAccess.GetOpenError();
            GD.PrintErr($"File Error: {FileAccess.GetOpenError()}");
            throw new System.IO.FileLoadException($"File load exception: {fileError}");
        }

        string fileData = "";
        while (levelFile.GetPosition() < levelFile.GetLength())
        {
            fileData += levelFile.GetLine().StripEdges();
        }

        var json = new Json();
        Error parseResult = json.Parse(fileData);
        if (parseResult != Error.Ok)
        {
            string errorMsg = $"JSON Parse Error: {json.GetErrorMessage()} in {fileData} at line {json.GetErrorLine()}";
            GD.Print(errorMsg);
            throw new JsonException(errorMsg);
        }

        var levelDict = new Godot.Collections.Dictionary<string, Variant>((Dictionary)json.Data);

        Array<Godot.Collections.Dictionary<string, Variant>> levels = (Array<Godot.Collections.Dictionary<string, Variant>>) levelDict["levels"];

        foreach(Godot.Collections.Dictionary<string, Variant> level in levels)
        {
            int levelNumber = (int) level["levelNr"];
            LevelSettings levelSettings = new((string) level["map"], (int) level["startMoney"], (string) level["unlocksTower"], (string[]) level["fields"], (string[]) level["spawnConfig"]);
            _loadedLevels.Add(levelNumber, levelSettings);
        }
    }
}
