using Godot;
using System;

public partial class MainMenu : Node
{
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
	}

	private void OnExitButtonPress()
	{
		ConfirmationDialog confirmationPopup = (ConfirmationDialog) GD.Load<PackedScene>("res://scene//ui//ConfirmationDialog.tscn").Instantiate();
		AddChild(confirmationPopup);

		confirmationPopup.Init("Test1", "Test2");
	}
}
