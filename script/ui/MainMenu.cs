using Godot;
using System;

public partial class MainMenu : Node
{
	private ConfirmationDialog _confirmationPopup;
	private OptionMenu _optionMenu;

    public void _init()
	{

	}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(_confirmationPopup != null)
		{
			check_for_user_input_in_popup();
		}

	}

	private void OnExitButtonPress()
	{
		_confirmationPopup = (ConfirmationDialog)GD.Load<PackedScene>("res://scene//ui//ConfirmationDialog.tscn").Instantiate();
		_confirmationPopup.Init("Willst du wirklich das Spiel verlassen?", "Spiel verlassen");
		AddChild(_confirmationPopup);

	}

	private void check_for_user_input_in_popup()
	{
        if (_confirmationPopup.Confirmed == true && _confirmationPopup.user_input == true)
        {
            GetTree().Quit();
        }

        if (_confirmationPopup.Confirmed == true && _confirmationPopup.user_input == true)
        {
            _confirmationPopup.QueueFree();
        }
    }

	public void _on_einstellungen_button_pressed()
	{
		_optionMenu = (OptionMenu)GD.Load<PackedScene>("res://scene//ui//OptionMenu.tscn").Instantiate();
		AddChild(_optionMenu);
	}
}

