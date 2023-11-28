using Godot;
using System;

public partial class MainMenu : Node
{
	private ConfirmationPopup _confirmationPopup;

	private void OnExitButtonPress()
	{
		_confirmationPopup = (ConfirmationPopup)GD.Load<PackedScene>("res://scene//ui//ConfirmationPopup.tscn").Instantiate();
		_confirmationPopup.Init("Willst du wirklich das Spiel verlassen?", "Spiel verlassen");
		_confirmationPopup.Confirmed += () => GetTree().Quit();
		AddChild(_confirmationPopup);
	}

	public void OnSettingsButtonPressed()
	{
		OptionMenu optionMenu = (OptionMenu)GD.Load<PackedScene>("res://scene//ui//OptionMenu.tscn").Instantiate();
		AddChild(optionMenu);
	}

	public void OnLevelSelectionButtonPressed()
	{
		//TODO: Implement scene switch
	}
}

