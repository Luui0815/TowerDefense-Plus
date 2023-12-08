using Godot;
using System;

public partial class knight : defender
{
	AnimatedSprite2D _knight;

	public override void Init()
	{
        _name = "knight";
		//_cost = get_Tower_cost(_name);
		_cost = 300;

        _knight = GetNode<AnimatedSprite2D>("Knight");
        _knight.Play();
    }

	public override void _Process(double delta)
	{
	}
}
