using Godot;
using System;

public partial class knight : Node2D
{
	AnimatedSprite2D _knight;
	public int Cost = 300;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_knight=GetNode<AnimatedSprite2D>("Knight");
		_knight.Play();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
