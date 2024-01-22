using Godot;
using TowerDefense;

namespace TowerDefense {
	public enum Level {
		One,
		Two
	}
}

public partial class LevelSelectionMenu : Node
{
	private PlayerData _playerData;

    public override void _Ready()
    {
        _playerData = GetNode<PlayerData>("/root/PlayerData");
    }

    public void OnSettingsButtonPressed()
    {
        OptionMenu optionMenu = (OptionMenu)GD.Load<PackedScene>("res://scene//ui//OptionMenu.tscn").Instantiate();
        AddChild(optionMenu);
    }

	public void OnGoBackButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://scene/ui/MainMenu.tscn");
	}

	public void OnSelectLevel1ButtonPressed()
	{
		_playerData.CurrentLevel = Level.One;
		GetTree().ChangeSceneToFile("res://scene/ui/TowerSelectionMenu.tscn");
	}

	public void OnSelectLevel2ButtonPressed()
	{
		_playerData.CurrentLevel = Level.Two;
		GetTree().ChangeSceneToFile("res://scene/ui/TowerSelectionMenu.tscn");
	}
}
