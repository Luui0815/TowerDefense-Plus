using Godot;
using System;

public partial class LevelSelectionMenu : Node
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
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
		GetTree().ChangeSceneToFile("res://scene/ui/TowerSelectionMenu.tscn");
	}
}
