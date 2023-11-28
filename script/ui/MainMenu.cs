using Godot;
using System;

public partial class MainMenu : Node
{
	private ConfirmationDialog _confirmationPopup;

	public override void _Process(double delta)
	{
		if(_confirmationPopup != null)
		{
			CheckForUserInputInPopup();
		}
	}

	private void OnExitButtonPress()
	{
		_confirmationPopup = (ConfirmationDialog)GD.Load<PackedScene>("res://scene//ui//ConfirmationDialog.tscn").Instantiate();
		_confirmationPopup.Init("Willst du wirklich das Spiel verlassen?", "Spiel verlassen");
		AddChild(_confirmationPopup);

	}

	private void CheckForUserInputInPopup()
	{
		if (_confirmationPopup.Confirmed)
		{
			_confirmationPopup.QueueFree();
			GetTree().Quit();
		}
	}

	public void OnSettingsButtonPressed()
	{
		OptionMenu optionMenu = (OptionMenu)GD.Load<PackedScene>("res://scene//ui//OptionMenu.tscn").Instantiate();
		AddChild(optionMenu);
	}
}

