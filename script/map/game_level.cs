using Godot;
using System;

public partial class game_level : Node2D
{
	private Button _pauseButton;
	private Button _exitButton;

	public override void _Ready()
	{
		_pauseButton = GetNode<Button>("PauseButton");
		_exitButton = GetNode<Button>("ExitButton");
	}


	public override void _Process(double delta)
	{
	}

	private void _on_exitLevel_button_pressed()
	{
		ConfirmationPopup _confirmationPopup = (ConfirmationPopup)GD.Load<PackedScene>("res://scene//ui//ConfirmationPopup.tscn").Instantiate();
		_confirmationPopup.Init("Willst du wirklich das Level verlassen?", "Level verlassen");
		_confirmationPopup.Confirmed += () => QueueFree();
		AddChild(_confirmationPopup);
	}

	private void _on_pause_button_pressed()
	{
		//DJs PAuse Screen einbinden
	}






}
