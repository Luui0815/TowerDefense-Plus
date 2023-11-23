using Godot;
using System;

public partial class title_screen : Node
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

	private void _on_beenden_button_pressed()
	{
		var confirmation_popup = ResourceLoader.Load<PackedScene>("res://scene//ui//confirmation_dialog.tscn").Instantiate();
		AddChild(confirmation_popup);

		confirmation_dialog sc = (confirmation_dialog)confirmation_popup;
		sc._init("Hello", "text");
		
	}
}
